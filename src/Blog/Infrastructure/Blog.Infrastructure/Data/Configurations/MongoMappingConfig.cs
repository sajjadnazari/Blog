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
            if (!BsonClassMap.IsClassMapRegistered(typeof(Article)))
            {
                BsonClassMap.RegisterClassMap<Article>(cm =>
                {
                    cm.AutoMap(); // مپ کردن فیلدهای عادی
                                  // به مونگو می‌گیم Guid ما رو به عنوان یک String تو دیتابیس ذخیره کن تا خواناتر باشه
                    cm.MapIdProperty(c => c.Id)
                      .SetSerializer(new GuidSerializer(BsonType.String));

                    // برای فیلدهای private که از بیرون فقط خواندنی هستند
                    cm.MapField("_tags").SetElementName("Tags");
                    cm.MapField("_comments").SetElementName("Comments");
                });
            }
        }
    }
}
