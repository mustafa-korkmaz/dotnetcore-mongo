
using Domain.Aggregates;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistance.MongoDb
{
    public static class MongoDbPersistence
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Document>(x =>
            {
                x.AutoMap();
                x.MapIdMember(x => x.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            ProductMapping.Configure();

            OrderMapping.Configure();
        }
    }
}
