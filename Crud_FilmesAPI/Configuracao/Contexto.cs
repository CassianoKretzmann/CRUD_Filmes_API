using Crud_FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_FilmesAPI.Configuracao
{
    //Define o contexto utilizado para as ações da API.
    public class Contexto : DbContext 
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) 
        {
        
        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
    }
}
