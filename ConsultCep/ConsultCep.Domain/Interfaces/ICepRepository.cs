using ConsultCep.Domain.DTO;
using ConsultCep.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultCep.Domain.Interfaces
{
    public interface ICepRepository
    {
        Task<Response> BuscarInfoCep(CepDTORequest cep);
    }
}
