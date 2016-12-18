using System.Configuration;
using MongoDB.Driver;
using SnackBarSupport.Dto.Models;
using SnackBarSupport.Properties;

namespace SnackBarSupport.DAL
{
    public class DbContext
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public DbContext()
        {
            string mongoHost = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoHost));
            settings.WriteConcern = new WriteConcern(1);
            _client = new MongoClient(settings);
            _database = _client.GetDatabase(Settings.Default.MainDbName);
        }

        public IMongoCollection<Ingredient> Ingredients
        {
            get
            {
                return _database.GetCollection<Ingredient>(DbNames.Ingredients)
                    .WithReadPreference(ReadPreference.SecondaryPreferred);
            }
        }

        public IMongoCollection<Recipe> Recipes
        {
            get
            {
                return _database.GetCollection<Recipe>(DbNames.Recipes)
                    .WithReadPreference(ReadPreference.SecondaryPreferred);
            }
        }
    }
}