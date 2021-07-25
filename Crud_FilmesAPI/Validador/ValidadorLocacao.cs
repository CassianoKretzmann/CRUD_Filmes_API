using Crud_FilmesAPI.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_FilmesAPI.Validador
{
    //Seta as regras para a inclusão de Locação
    public class ValidadorLocacao:AbstractValidator<Locacao>
    {
        public ValidadorLocacao() 
        {
            RuleFor(l => l.Filmes).NotNull().WithMessage("Deve haver pelo menos um filme para locação.");
            RuleFor(l => l.CPF.Length).Equal(14).WithMessage("O CPF do locador é inválido.");
            RuleFor(l => l.DataLocacao).NotNull().WithMessage("A data de locação deve ser informada.");
        }
    }
}
