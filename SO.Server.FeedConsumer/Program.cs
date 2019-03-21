using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SO.Server.Data;
using SO.Server.Data.Entities;
using SO.Server.FeedConsumer.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer
{
	//public class OddDto : IBaseDto<OddDto> {
	//	[XmlAttribute(AttributeName = "ID")]
	//	public int Id { get; set; }

	//	public bool Equals(OddDto other) {
	//		return Id.Equals(other.Id);
	//	}	
	//}

	//public interface IBaseDto<T> : IEquatable<T>, IHaveUniqueId {
	//}

	public interface IHaveUniqueId {
		int Id { get; set; }
	}

	public class DiffChecker{
		
		public static IEnumerable<int> ToDeleteIDs<T>(IEnumerable<T> current, IEnumerable<T> updated) where T : class, IEquatable<T>, IHaveUniqueId {
			return current.Select(x => x.Id).Except(updated.Select(x => x.Id));
		}

		public static IEnumerable<int> ToAddIDs<T>(IEnumerable<T> current, IEnumerable<T> updated) where T : class, IEquatable<T>, IHaveUniqueId {
			return updated.Select(x => x.Id).Except(current.Select(x => x.Id));
		}

		public static IEnumerable<int> ToUpdateIDs<T>(IEnumerable<T> current, IEnumerable<T> updated) where T : class, IEquatable<T>, IHaveUniqueId {
			return current.Except(updated).Select(x => x.Id);
		}
	}

    internal class Program
    {
        private static void Main(string[] args)
        {
			var serviceProvider = RegisterDependencies(new ServiceCollection());

            var current = Get("soccer.xml");
            var updated = Get("soccer2.xml");

			//var mapper = serviceProvider.GetService<IMapper>();
			//var uow = serviceProvider.GetService<IUnitOfWork>();
			//var sportsRepo = uow.GetRepository<Sport>();
			//var eventsRepo = uow.GetRepository<Event>();
			//var sport = mapper.Map<Sport>(currentXml.Sport);

			//delete

			//var aa = 

			var currentMatches = current.Sport.Events.SelectMany(x => x.Matches);
			var updatedMatches = updated.Sport.Events.SelectMany(x => x.Matches);
			var update = DiffChecker.ToUpdateIDs(currentMatches, updatedMatches);
			var delete = DiffChecker.ToDeleteIDs(currentMatches, updatedMatches);
			var add = DiffChecker.ToAddIDs(currentMatches, updatedMatches);

			//var deleteEvents = mapper.Map<IEnumerable<Event>>(delete);
			//eventsRepo.Delete(deleteEvents);
			//uow.SaveChanges();
			//update
			//var update = currentXml.Sport.Events.Intersect(newXml.Sport.Events);
			//add
			//var add = newXml.Sport.Events.Except(currentXml.Sport.Events);
			//var addEvents = mapper.Map<IEnumerable<Event>>(add);
			//eventsRepo.Add(addEvents);


			//example with odds
			var oddsA = updated.Sport.Events.Select(x => x.Matches.Select(y => y.Bets.Select(z => z.Odds.Select(m => m.Id))));
            //Think of generic service to return sets for delete,update, add
        }

        private static XmlSports GetXmlSportObj(string fileName)
        {
            var doc = new XmlDocument();
            doc.Load(fileName);

            var serializer = new XmlSerializer(typeof(XmlSports));

            XmlSports a;

            using (var reader = new StringReader(doc.InnerXml))
            {
                a = (XmlSports)serializer.Deserialize(reader);
            }

            return a;
        }

        private static XmlSports Get(string fileName)
        {
            XmlSerializer xsw = new XmlSerializer(typeof(XmlSports));
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader stream = new StreamReader(fs, Encoding.UTF8);
            XmlSports config = (XmlSports)xsw.Deserialize(new XmlTextReader(stream));
            return config;
        }

        private static ServiceProvider RegisterDependencies(IServiceCollection services)
        {
            return services.AddDataLayerDependencies()
                .AddAutoMapper()
                .BuildServiceProvider();
        }
    }
}
