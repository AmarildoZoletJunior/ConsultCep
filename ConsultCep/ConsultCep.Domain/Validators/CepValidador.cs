using ConsultCep.Domain.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultCep.Domain.Validador
{
    public class CepValidador : AbstractValidator<CepDTORequest>
    {
        public CepValidador() 
        {
            RuleFor(x => x.CEP).Matches(@"^\d+$").WithMessage("O campo deve conter apenas números.").MinimumLength(8).WithMessage("O campo deve conter no mínimo 8 números.").MaximumLength(8).WithMessage("O campo deve conter apenas 8 números.").NotEmpty().WithMessage("O campo não pode ser vazio.").NotNull().WithMessage("O campo não pode ser nulo.");
        }
    }
}
