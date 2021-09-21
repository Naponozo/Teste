using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Teste.Models;
using Negocio.Cadastro;
using Teste.ViewModel;

namespace Teste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWaeatherForeDbSettings _settings;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWaeatherForeDbSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }


        [HttpGet("BuscarTodos")]
        public List<WeatherForecast> GetAll()
        {
            NegWeather negWather = new NegWeather(_settings);

            var book = negWather.Get();

            if (book == null)
            {
                return null;
            }

            return book;
        }

        [HttpGet("Buscar")]
        public WeatherForecast Get(string id)
        {

            NegWeather negWather = new NegWeather(_settings);
            var book = negWather.Get(id);

            if (book == null)
            {
                return null;
            }

            return book;
        }

        [HttpPost("Salvar")]
        public RetornoAcao<WeatherForecast> Create([FromBody, Required] WeatherForecastViewModel a)
        {
            RetornoAcao<WeatherForecast> retornoAcao = new RetornoAcao<WeatherForecast>();
            try
            {
                var b = new WeatherForecast();
                b.Summary = a.Summary;
                b.TemperatureC = a.TemperatureC;

                NegWeather negWather = new NegWeather(_settings);
                retornoAcao = negWather.Create(b);
            }
            catch (Exception ex)
            {
                retornoAcao.MensagemRetorno = "Ocorreu um erro, erro: " + ex.Message;
                retornoAcao.Tipo = TipoMensagem.Erro;
                //Salvar Log
            }

            return retornoAcao;
        }

        [HttpPut("Alterar")]
        public WeatherForecast Update(string id, string summary, int temperatura)
        {

            NegWeather negWather = new NegWeather(_settings);
            var weatherForecast = negWather.Get(id);
            weatherForecast.Summary = summary;
            weatherForecast.TemperatureC = temperatura;

            if (weatherForecast == null)
            {
                return null;
            }

            negWather.Update(id, weatherForecast);

            return null;
        }

        [HttpPut("AlterarCls")]
        public WeatherForecast UpdateCls([FromBody, Required] WeatherForecast a)
        {
            NegWeather negWather = new NegWeather(_settings);

            var weatherForecast = negWather.Get(a.Id);
            weatherForecast.Summary = a.Summary;
            weatherForecast.TemperatureC = a.TemperatureC;

            if (weatherForecast == null)
            {
                return null;
            }

            negWather.UpdateCls(weatherForecast);

            return null;
        }


        [HttpDelete("Apagar")]
        public JsonResult Delete(string id)
        {
            NegWeather negWather = new NegWeather(_settings);

            var book = negWather.Get(id);

            if (book == null)
            {
                return null;
            }

            negWather.Remove(book.Id);

            return null;
        }
    }
}

