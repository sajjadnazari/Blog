using Blog.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Blog.Infrastructure.Data.Configurations
{
    public static class MongoMappingConfig
    {
        public static void RegisterMappings()
        {
            BsonSerializer.TryRegisterSerializer(new GuidSerializer(BsonType.String));

            if (!BsonClassMap.IsClassMapRegistered(typeof(Article)))
            {
                BsonClassMap.RegisterClassMap<Article>(cm =>
                {
                    cm.AutoMap();

                    // (چون در بالا قانون کلی را گذاشتیم، دیگر نیازی به نوشتن تنظیمات Guid برای فیلد Id نیست)

                    // ۲. فقط تنظیمات مربوط به فیلدهای Private را نگه می‌داریم
                    cm.MapField("_tags").SetElementName("Tags");
                    cm.MapField("_comments").SetElementName("Comments");
                });
            }
        }
    }
}
