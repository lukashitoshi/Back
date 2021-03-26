using Back.Data;
using desafio_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Back.Controllers
{
    [Route("login")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly KanbanContext _context;
        public UsuarioController(IConfiguration config, KanbanContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario _user)
        {

            if (_user != null && _user.Login != null && _user.Senha != null)
            {
                var user = await GetUsuario(_user.Login, _user.Senha);

                if (user != null)
                {
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JwtConfig:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("Login", user.Login),
                    new Claim("Senha", user.Senha)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"], _configuration["JwtConfig:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<Usuario> GetUsuario(string login, string senha)
        {
            return _context.Usuario.FirstOrDefault(u => u.Login == login && u.Senha == senha);
        }
    }
}
