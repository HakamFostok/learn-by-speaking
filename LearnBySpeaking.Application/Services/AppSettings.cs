using LearnBySpeaking.Domain.Interfaces.Core;

namespace LearnBySpeaking.Application.Services
{
    public class JwtOptions : IJwtOptions
    {
        public string Key { get; set; }
    }

    public class ConnectionStrings : IConnectionStrings
    {
        public string DefaultConnection { get; set; }
        public string LocalConnection { get; set; }
    }

    public class MongodbDatabaseSettings : IMongodbDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string EventCollectionName { get; set; }
    }

    public class AppSettings : IAppSettings
    {
        public IJwtOptions JwtOptions { get; set; }
        public IConnectionStrings ConnectionStrings { get; set; }
        public IMongodbDatabaseSettings MongodbDatabaseSettings { get; set; }

        public AppSettings(IJwtOptions jwtOptions, IConnectionStrings connectionStrings, IMongodbDatabaseSettings mongodbDatabaseSettings)
        {
            JwtOptions = jwtOptions;
            ConnectionStrings = connectionStrings;
            MongodbDatabaseSettings = mongodbDatabaseSettings;
        }
    }
}