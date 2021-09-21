using FluentValidation;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste;
using Teste.Models;

namespace Negocio.Cadastro
{
    public class NegWeather
    {
        private readonly IMongoCollection<WeatherForecast> _books;

        public NegWeather(IWaeatherForeDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<WeatherForecast>(settings.BooksCollectionName);
        }

        public List<WeatherForecast> Get() =>
            _books.Find(book => true).ToList();

        public WeatherForecast Get(string id) =>
            _books.Find<WeatherForecast>(book => book.Id == id).FirstOrDefault();

        public RetornoAcao<WeatherForecast> Create(WeatherForecast a)
        {
            RetornoAcao<WeatherForecast> retornoAcao = new RetornoAcao<WeatherForecast>();
            retornoAcao.Tipo = TipoMensagem.Ok;
            retornoAcao.MensagemRetorno = "";

            var validator = new WeatherForecastValidation();
            var validRes = validator.Validate(a);
            if (validRes.IsValid)
            {
                retornoAcao.MensagemRetorno = "Salvo com sucesso!";
                retornoAcao.Tipo = TipoMensagem.Ok;

                a.Date = DateTime.Now;
                _books.InsertOne(a);  
            }
            else
            {
                retornoAcao.MensagemRetorno = validRes.Errors[0].ErrorMessage;
                retornoAcao.Tipo = TipoMensagem.Atencao;
            }

            retornoAcao.Objeto = a;
            return retornoAcao;
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
