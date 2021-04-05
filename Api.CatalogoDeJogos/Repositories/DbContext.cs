using Api.CatalogoDeJogos.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CatalogoDeJogos.Data
{
    public class DbContext
    {
        public IMongoDatabase db { get; }
        public DbContext(IConfiguration configuration)
        {
            try
            {
                var client = new MongoClient(MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"])));
                db = client.GetDatabase(configuration["DataName"]);

                MapClass();
            }
            catch (Exception ex)
            {
                throw new MongoException("Não foi possivel se conectar ao banco de dados");
            }

        }

        private void MapClass()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("calmelcase", conventionPack, t => true);
            if (!BsonClassMap.IsClassMapRegistered(typeof(Game)))
            {
                BsonClassMap.RegisterClassMap<Game>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                    map.MapIdMember(x => x.GameId);
                    //map.MapMember(x => x.GameId).SetIsRequired(true);
                    map.MapMember(x => x.Name).SetIsRequired(true);
                    map.MapMember(x => x.Price).SetIsRequired(true);
                    map.MapMember(x => x.Production).SetIsRequired(true);
                });
            }
        }

    }
}
