using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.ViewModel;

namespace Teste.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<WeatherForecastViewModel, WeatherForecast>();
            CreateMap<WeatherForecast, WeatherForecastViewModel>();
        }
    }
}
