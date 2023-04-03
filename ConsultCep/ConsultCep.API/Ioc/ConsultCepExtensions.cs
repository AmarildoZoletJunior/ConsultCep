using ConsultCep.Domain.DTOs;
using ConsultCep.Domain.Interfaces;
using ConsultCep.Domain.Repositories;
using ConsultCep.Domain.Validador;
using FluentValidation;

namespace ConsultCep.API.Ioc
{
    public static class ConsultCepExtensions
    {
        
        public static IServiceCollection AddConsultCepExtensions(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddTransient<IValidator<CepDTORequest>, CepValidador>();

            services.AddScoped<ICepRepository, CepRepository>();

            services.AddHttpClient("ViaCepApi", configurationClient =>
            {
                configurationClient.BaseAddress = new Uri(configuration["UrlApiCep"]);
            });
            return services;
        }
    }
}
