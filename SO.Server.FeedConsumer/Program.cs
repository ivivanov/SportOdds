using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SO.Server.Data;
using SO.Server.Data.Entities;
using SO.Server.FeedConsumer.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var serviceProvider = RegisterDependencies(new ServiceCollection());

            var currentXml = Get("soccer.xml");
            var newXml = Get("soccer2.xml");

            var mapper = serviceProvider.GetService<IMapper>();
            var uow = serviceProvider.GetService<IUnitOfWork>();
            var sportsRepo = uow.GetRepository<Sport>();
            var eventsRepo = uow.GetRepository<Event>();
            //var sport = mapper.Map<Sport>(currentXml.Sport);

            //delete
            var delete = currentXml.Sport.Events.Except(newXml.Sport.Events);
            var deleteEvents = mapper.Map<IEnumerable<Event>>(delete);
            eventsRepo.Delete(deleteEvents);
            uow.SaveChanges();
            //update
            //var update = currentXml.Sport.Events.Intersect(newXml.Sport.Events);
            //add
            var add = newXml.Sport.Events.Except(currentXml.Sport.Events);
            var addEvents = mapper.Map<IEnumerable<Event>>(add);
            eventsRepo.Add(addEvents);


            //example with odds
            var oddsA = newXml.Sport.Events.Select(x => x.Matches.Select(y => y.Bets.Select(z => z.Odds.Select(m => m.Id))));
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
