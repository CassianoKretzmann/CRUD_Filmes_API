using Crud_FilmesAPI.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_FilmesAPI.Validador
{
    //Seta as regras para a inclusão de Filme
    public class ValidadorFilme:AbstractValidator<Filme>
    {
        public ValidadorFilme() 
        {
            RuleFor(f => f.Nome).NotNull().NotEmpty().WithMessage("O nome do filme deve ser preenchido.")
                .MaximumLength(200).WithMessage("O tamanho do nome excede o máximo permitido");
            RuleFor(f => f.Ativo).LessThanOrEqualTo('1').WithMessage("O valor de 'Ativo' deve ser 0 ou 1.");
        }
    }
}
