using Microsoft.Extensions.DependencyInjection;
using SO.Server.FeedConsumer.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using SO.Server.Data;
using SO.Server.Data.Entities;
using System.Text;
using AutoMapper;

namespace SO.Server.FeedConsumer
{
    internal class Program
    {
        private static void Main(string[] args) {

			var serviceProvider = RegisterDependencies(new ServiceCollection());

			var a = Get("soccer.xml");
			var b = Get("soccer2.xml");

			var mapper = serviceProvider.GetService<IMapper>();
			var uow = serviceProvider.GetService<IUnitOfWork>();
			var sport = mapper.Map<Sport>(a.Sport);
			var sportsRepo = uow.GetRepository<Sport>();
			sportsRepo.Add(sport);
			uow.SaveChanges();
			//var result = a.Sport.Events.Except(b.Sport.Events);
			//var bxcv = 1;
		}

		private static XmlSports GetXmlSportObj(string fileName) {
			var doc = new XmlDocument();
			doc.Load(fileName);

			var serializer = new XmlSerializer(typeof(XmlSports));

			XmlSports a;

			using(var reader = new StringReader(doc.InnerXml)) {
				a = (XmlSports)serializer.Deserialize(reader);
			}

			return a;
		}

		private static XmlSports Get(string fileName) {
			XmlSerializer xsw = new XmlSerializer(typeof(XmlSports));
			FileStream fs = new FileStream(fileName, FileMode.Open);
			StreamReader stream = new StreamReader(fs, Encoding.UTF8);
			XmlSports config = (XmlSports)xsw.Deserialize(new XmlTextReader(stream));
			return config;
		}

		private static ServiceProvider RegisterDependencies(IServiceCollection services) {
			return services.AddDataLayerDependencies()
				.AddAutoMapper()
				.BuildServiceProvider();
		}
	}
}
