using System;
using System.Configuration;
using System.Linq;
using MongoDB.Driver;
using SnackBarSupport.Properties;

namespace SnackBarSupport.DAL
{
    public class MongoDbContext
    {
        protected static IMongoClient Client;
        protected static IMongoDatabase Database;

        public MongoDbContext()
        {
            string mongoHost = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoHost));
            settings.WriteConcern = new WriteConcern(1);
            Client = new MongoClient(settings);
            Database = Client.GetDatabase(Settings.Default.MainDbName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            string collectionName = PrepareCollectionName(typeof(T).Name.ToLower());          

            return Database.GetCollection<T>(collectionName)
                .WithReadPreference(ReadPreference.SecondaryPreferred) as IMongoCollection<T>;
        }

        private string PrepareCollectionName(string typeName)
        {
            string collectionName = typeName + "s";
            if (collectionName.Length == 1)
            {
                throw new ArgumentException();
            }
            collectionName = collectionName.First().ToString().ToUpper() + collectionName.Substring(1);

            var removeString = "dto";
            int index = collectionName.IndexOf(removeString, StringComparison.Ordinal);
            string cleanPath = (index < 0)
                ? collectionName
                : collectionName.Remove(index, removeString.Length);

            return cleanPath;
        }
    }
}