using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_backend.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public Guid UserId { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }
    }
}
