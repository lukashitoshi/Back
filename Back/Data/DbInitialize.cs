using Back.Data;
using desafio_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back
{
    public static class DbInitialize
    {

        public static void Initialize(KanbanContext context)
        {
            Seed(context);
        }

        private static void Seed(KanbanContext context)
        {

            if (context.Usuario.FirstOrDefault(x => x.Login == "letscode") == null)
            {
                context.Usuario.Add(new Usuario
                {
                    UserId = Guid.NewGuid(),
                    Login = "letscode",
                    Senha = "lets@123"
                });

                context.SaveChanges();
            }
        }
    }
}
