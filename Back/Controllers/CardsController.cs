using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back.Data;
using desafio_backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace Back.Controllers
{
    [Authorize]
    [Route("cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly KanbanContext _context;

        public CardsController(KanbanContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCard()
        {
            return await _context.Card.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(Guid id)
        {
            var card = await _context.Card.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Card>> PutCard(Guid id, Card card)
        {
            if (id != card.Id || card.Titulo == "" || card.Conteudo == "")
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Console.WriteLine($"{DateTime.Now} - Card {id} - {card.Titulo} - Alterar");

            return card;
        }

        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(CardBody cardBody)
        {
            if (cardBody.Titulo == "" || cardBody.Conteudo == "")
            {
                return BadRequest();
            }
            Card card = new Card
            {
                Id = Guid.NewGuid(),
                Lista = cardBody.Lista,
                Titulo = cardBody.Titulo,
                Conteudo = cardBody.Conteudo
            };

            _context.Card.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Card>>> DeleteCard(Guid id)
        {
            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Card.Remove(card);
            await _context.SaveChangesAsync();

            Console.WriteLine($"{DateTime.Now} - Card {id} - {card.Titulo} - Remover");
            return await _context.Card.ToListAsync();
        }

        private bool CardExists(Guid id)
        {
            return _context.Card.Any(e => e.Id == id);
        }
    }
}
