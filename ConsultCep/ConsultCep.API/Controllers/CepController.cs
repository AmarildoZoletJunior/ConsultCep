﻿using ConsultCep.Domain.DTOs;
using ConsultCep.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ConsultCep.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CepController : ControllerBase
    {
        private readonly ICepRepository _cepRepository;
        public CepController(ICepRepository cep) 
        {
            _cepRepository = cep;      
        }

        [HttpPost]
        public async Task<IActionResult> GetAsync([Required]CepDTORequest cep) 
        {
            var resultado = await _cepRepository.BuscarInfoCep(cep);

            if (!resultado.EstaValido)
            {
                return BadRequest(resultado.Mensagens);
            }
            return Ok(resultado);
        }
    }
}