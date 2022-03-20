﻿using Domain.Aggregates.Order;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistance.MongoDb
{
    public class OrderMapping
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Order>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<OrderItem>(map =>
            {
                map.AutoMap();
                map.MapMember(x => x.ProductId)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}
