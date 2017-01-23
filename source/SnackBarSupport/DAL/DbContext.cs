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
        private const int WriteConcernCount = 2; //Ilość potwierdzeń od baz danych, 
                                                 //na które aplikacja czeka przy zapisie

        public MongoDbContext()
        {
            // Pobranie connection stringa z ustawień aplikacji
            string mongoHost = ConfigurationManager.ConnectionStrings["Default"].ConnectionString; 
            // Stworzenie ustawień dla bazy MongoDB na podstawie connection stringa
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoHost));
            // Ustawienie ilości potwierdzeń od baz danych
            settings.WriteConcern = new WriteConcern(WriteConcernCount);
            // Utworzenie klienta i pobranie z niego bazy danych
            Client = new MongoClient(settings);
            Database = Client.GetDatabase(Settings.Default.MainDbName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            // Przygotowanie nazwy dla kolekcji
            string collectionName = PrepareCollectionName(typeof(T).Name.ToLower());
            
            try
            {
                // Próba dostępu do danej kolekcji z preferowanym odczytem z bazy Secondary
                return Database.GetCollection<T>(collectionName)
                    .WithReadPreference(ReadPreference.SecondaryPreferred) as IMongoCollection<T>;
            }
            catch (TimeoutException)
            {
                var message = "A Timeout Exception occured./nCheck your database connection./nCheck if almost "
                             + WriteConcernCount + " of your databases are working.";
                throw new TimeoutException(message);
            }
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