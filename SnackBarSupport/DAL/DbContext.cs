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


        ////Prawdopodbnie do usuniecia, jeśli GetCollection(T entity) zadziala 
        //public IMongoCollection<Ingredient> Ingredients
        //{
        //    get
        //    {
        //        return _database.GetCollection<Ingredient>(DbNames.Ingredients)
        //            .WithReadPreference(ReadPreference.SecondaryPreferred);
        //    }
        //}

        ////Prawdopodbnie do usuniecia, jeśli GetCollection(T entity) zadziala
        //public IMongoCollection<Recipe> Recipes
        //{
        //    get
        //    {
        //        return _database.GetCollection<Recipe>(DbNames.Recipes)
        //            .WithReadPreference(ReadPreference.SecondaryPreferred);
        //    }
        //}

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            string collectionName = typeof(TEntity).Name.ToLower() + "s";
            if (collectionName.Length == 1)
            {
                throw new ArgumentException();
            }
            collectionName = collectionName.First().ToString().ToUpper() + collectionName.Substring(1);

            return Database.GetCollection<TEntity>(collectionName)
                .WithReadPreference(ReadPreference.SecondaryPreferred);
        }
    }
}