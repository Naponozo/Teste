using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste
{
    public class Validation : AbstractValidator<WeatherForecast>
    {
        public Validation()
        {
            RuleFor(x => x.Summary).NotEmpty().WithMessage("Sumario Necessario");
            RuleFor(x => x.TemperatureC).NotEmpty().WithMessage("Temperatura Necessaria"); ;
        }
    }
}