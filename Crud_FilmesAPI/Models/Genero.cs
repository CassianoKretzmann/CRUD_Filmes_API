using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Crud_FilmesAPI.Models
{
    //Classe Gênero com relacionamento com a classe Filme
    public class Genero
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public char Ativo { get; set; }
        [JsonIgnore]
        public virtual ICollection<Filme> Filmes { get; set; }
    }
}
