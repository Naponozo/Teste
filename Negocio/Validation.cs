using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste;

namespace Negocio
{
    public class WeatherForecastValidation : AbstractValidator<WeatherForecast>
    {
        public WeatherForecastValidation()
        {
            RuleFor(x => x.Summary).NotEmpty().WithMessage("Sumario Necessario");
            RuleFor(x => x.TemperatureC).NotNull().WithMessage("Temperatura Necessaria"); ;
        }
    }

}
