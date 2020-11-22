using System;
using Api.Data.Collection;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Api.Data
{
    public class DbContext
    {
        public IMongoDatabase DB { get; }

        public DbContext(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["NameDB"]);
                MapClasses();
            }
            catch(Exception ex)
            {
                throw new MongoException("Not was possible connection in Database", ex);
            }
        }
        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if(!BsonClassMap.IsClassMapRegistered(typeof(Infected)))
            {
                BsonClassMap.RegisterClassMap<Infected>(i => 
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }   
        }
    }
}