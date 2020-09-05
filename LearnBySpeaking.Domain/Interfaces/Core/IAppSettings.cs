namespace LearnBySpeaking.Domain.Interfaces.Core
{
    public interface IJwtOptions
    {
        string Key { get; set; }
    }

    public interface IConnectionStrings
    {
        string DefaultConnection { get; set; }
    }

    public interface IMongodbDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string EventCollectionName { get; set; }
    }

    public interface IAppSettings
    {
        IJwtOptions JwtOptions { get; set; }
        IConnectionStrings ConnectionStrings { get; set; }
        IMongodbDatabaseSettings MongodbDatabaseSettings { get; set; }
    }
}