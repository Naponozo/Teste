using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Teste.Models;

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
        private readonly WeatherForecastService _service;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }


        [HttpGet("BuscarTodos")]
        public List<WeatherForecast> GetAll()
        {
            var book = _service.Get();

            if (book == null)
            {
                return null;
            }

            return book;
        }

        [HttpGet("Buscar")]
        public WeatherForecast Get(string id)
        {
            var book = _service.Get(id);

            if (book == null)
            {
                return null;
            }

            return book;
        }

        [HttpPost("Salvar")]
        public RetornoAcao<WeatherForecast> Create([FromBody, Required] WeatherForecast a)
        {
            RetornoAcao<WeatherForecast> retornoAcao = new RetornoAcao<WeatherForecast>();
            try
            {
                
                retornoAcao.Tipo = TipoMensagem.Ok;
                retornoAcao.MensagemRetorno = "";

                a.Date = DateTime.Now;
                retornoAcao.Objeto = a;

                var validator = new Validation();
                var validRes = validator.Validate(a);
                if (validRes.IsValid)
                {
                    retornoAcao.MensagemRetorno = "Salvo com sucesso!";
                    retornoAcao.Tipo = TipoMensagem.Ok;

                    _service.Create(a);
                }
                else
                {
                    retornoAcao.MensagemRetorno = validRes.Errors[0].ErrorMessage;
                    retornoAcao.Tipo = TipoMensagem.Atencao;
                }
            }
            catch (Exception ex)
            {
                retornoAcao.MensagemRetorno = "Ocorreu um erro, erro: "+ ex.Message;
                retornoAcao.Tipo = TipoMensagem.Erro;
            }


            return retornoAcao;
        }

        [HttpPut("Alterar")]
        public WeatherForecast Update(string id, string summary, int temperatura)
        {
            var weatherForecast = _service.Get(id);
            weatherForecast.Summary = summary;
            weatherForecast.TemperatureC = temperatura;

            if (weatherForecast == null)
            {
                return null;
            }

            _service.Update(id, weatherForecast);

            return null;
        }

        [HttpPut("AlterarCls")]
        public WeatherForecast UpdateCls([FromBody, Required] WeatherForecast a)
        {
            var weatherForecast = _service.Get(a.Id);
            weatherForecast.Summary =  a.Summary;
            weatherForecast.TemperatureC = a.TemperatureC;

            if (weatherForecast == null)
            {
                return null;
            }

            _service.UpdateCls(weatherForecast);

            return null;
        }


        [HttpDelete("Apagar")]
        public JsonResult Delete(string id)
        {
            var book = _service.Get(id);

            if (book == null)
            {
                return null;
            }

            _service.Remove(book.Id);

            return null;
        }
    }
}

