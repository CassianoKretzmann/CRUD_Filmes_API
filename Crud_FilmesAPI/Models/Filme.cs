using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Crud_FilmesAPI.Models
{
    //Classe Filme com relacionamento com a classe Locação e Gênero
    public class Filme
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public char Ativo { get; set; }
        public int GeneroId { get; set; }
        [JsonIgnore]
        public virtual Genero Genero { get; set; }
        [JsonIgnore]
        public virtual ICollection<Locacao> Locacoes { get; set; }
    }
}
