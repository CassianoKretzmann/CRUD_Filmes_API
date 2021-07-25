using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_FilmesAPI.Models;
using FluentValidation;

namespace Crud_FilmesAPI.Validador
{
    //Seta as regras para a inclusão de Gênero
    public class ValidadorGenero:AbstractValidator<Genero>
    {
        public ValidadorGenero()
        {
            RuleFor(g => g.Nome).NotNull().NotEmpty().WithMessage("O nome do gênero deve ser preenchido.")
                .MaximumLength(100).WithMessage("O tamanho do nome excede o máximo permitido");
            RuleFor(g => g.Ativo).Equal('0').Equal('1').WithMessage("O valor de 'Ativo' deve ser 0 ou 1.");

        }
    }
}
