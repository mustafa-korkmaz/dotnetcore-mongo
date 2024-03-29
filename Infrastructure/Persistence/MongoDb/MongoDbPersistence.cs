﻿
using Domain.Aggregates;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence.MongoDb
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

    //        ConventionRegistry.Register("Ignore",
    //          new ConventionPack
    //            {
    //                new IgnoreIfNullConvention(true)
    //            },
    //           t => true);

            ProductMapping.Configure();

            OrderMapping.Configure();
        }
    }
}
