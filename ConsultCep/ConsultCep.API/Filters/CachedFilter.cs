using ConsultCep.Domain.DTO;
using ConsultCep.Domain.DTOs;
using ConsultCep.Services.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Text;

namespace ConsultCep.API.Filters
{
    public class CachedFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Chama o serviço
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            //Busca se tiver disponivel
            var cacheFind = await cacheService.GetAsync(context.HttpContext.Request.QueryString.Value);

            //Verifica se encontrou
            if(!string.IsNullOrEmpty(cacheFind) && !string.IsNullOrWhiteSpace(cacheFind))
            {
                //Se conter no banco, ele deserializa ele organizando como um JSON para enviar
                var desi = JsonConvert.DeserializeObject<Response>(cacheFind);
                //Atribui o resultado encontrado
                context.Result = new OkObjectResult(desi);
                //passa
                return;
            }

            //Da um passo adiante no software
           var exec =  await next();

            //Verifica se o resultado foi ok
            if(exec.Result is OkObjectResult obj)
            {
                //Atribui ao cache por que não caiu na condição de encontrado no banco
                await cacheService.SetAsync(context.HttpContext.Request.QueryString.Value, JsonConvert.SerializeObject(obj.Value));
            }
        }
    }
}
