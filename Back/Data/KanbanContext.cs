using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using desafio_backend.Models;

namespace Back.Data
{
    public class KanbanContext : DbContext
    {
        public KanbanContext (DbContextOptions<KanbanContext> options)
            : base(options)
        {
        }

        public DbSet<desafio_backend.Models.Card> Card { get; set; }
        public DbSet<desafio_backend.Models.Usuario> Usuario { get; set; }
    }
}
