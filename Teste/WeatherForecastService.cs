using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Models;

namespace Teste
{
    public class WeatherForecastService
    {

        private readonly IMongoCollection<WeatherForecast> _books;

        public WeatherForecastService(IWaeatherForeDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<WeatherForecast>(settings.BooksCollectionName);
        }

        public List<WeatherForecast> Get() =>
            _books.Find(book => true).ToList();

        public WeatherForecast Get(string id) =>
            _books.Find<WeatherForecast>(book => book.Id == id).FirstOrDefault();

        public WeatherForecast Create(WeatherForecast book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, WeatherForecast a) =>
            _books.ReplaceOne(book => book.Id == id, a);

        public void UpdateCls(WeatherForecast a) =>
          _books.ReplaceOne(book => book.Id == a.Id, a);

        public void Remove(WeatherForecast bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);

    }
}
