using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Crud_FilmesAPI.Models
{
    //Classe de locações com relacionamento com a classe Filme
    public class Locacao
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public virtual ICollection<Filme> Filmes { get; set; }
        [Required]
        [Column(TypeName = "varchar(14)")]
        public string CPF { get; set; }
        [Required]
        public DateTime DataLocacao { get; set; }
    }
}
