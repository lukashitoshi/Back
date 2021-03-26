using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_backend.Models
{
    [Table("Cards")]
    public class Card
    {
        [Key]
        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public string Lista { get; set; }

    }
}
