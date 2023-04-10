using ConsultCep.API.Filters;
using ConsultCep.Domain.DTOs;
using ConsultCep.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ConsultCep.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CachedFilter]
    public class CepController : ControllerBase
    {
        private readonly ICepRepository _cepRepository;
        public CepController(ICepRepository cep) 
        {
            _cepRepository = cep;      
        }

        [CachedFilter]
        [HttpGet("/cep")]
        public async Task<IActionResult> GetAsync(string numero) 
        {
            var resultado = await _cepRepository.BuscarInfoCep(numero);

            if (!resultado.EstaValido)
            {
                return BadRequest(resultado.Mensagens);
            }
            return Ok(resultado);
        }
    }
}
