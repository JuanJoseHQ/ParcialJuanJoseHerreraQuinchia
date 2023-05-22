﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcialJuanJoseHerreraQuinchia.DAL;
using ParcialJuanJoseHerreraQuinchia.DAL.Entities;
using System.Net.Sockets;

namespace ParcialJuanJoseHerreraQuinchia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : Controller
    {
        private readonly DataBaseContext _context;

        public TicketsController(DataBaseContext context)
        {
            _context = context;
        }


        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Tickets>> GetTicketsById(Guid? id)
        {
            var Ticket = await _context.Tickets.FirstOrDefaultAsync(c => c.Id == id);

            if (Ticket == null) return NotFound();

            return Ok(Ticket);
        }


        [HttpPut, ActionName("Update")]
        [Route("Update/{id}")]
        public async Task<ActionResult> UpdateTicket(Guid? ticketId, Tickets ticket)
        {
            try
            {
                //if(ticketId != ticket.Id) return NotFound("Ticket not found");

                ticket.UseDate = DateTime.UtcNow;
                ticket.IsUsed = true;

                Random random = new Random();
                int numberRandom = random.Next(1, 5);

                string Entrance = "";

                switch (numberRandom)
                {
                    case 1:
                        Entrance = "Norte";
                        break;
                    case 2:
                        Entrance = "Oriental";
                        break;
                    case 3:
                        Entrance = "Occidental";
                        break;
                    case 4:
                        Entrance = "Sur";
                        break;
                    default:
                        break;
                }

                ticket.EntranceGate = Entrance;
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(ticket);
        }

        [HttpPost]
        [Route("ValidateIncomeTicket")]
        public async Task<IActionResult> ValidateIncomeTicket(Guid? ticketId)
        {
            try
            {
                var ticket = await _context.Tickets.FirstOrDefaultAsync(c => c.Id == ticketId);

                if (ticket == null)
                {
                    return Conflict("Boleta no válida");
                }
                else if (ticket.IsUsed)
                {
                    return Ok($"Boleta ya usada. Fecha de uso: {ticket.UseDate}, Portería: {ticket.EntranceGate}");
                }
                else
                {
                    UpdateTicket(ticket.Id, ticket);
                    await _context.SaveChangesAsync();
                    return Ok("Boleta válida, puede ingresar al concierto");
                }
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (DbUpdateException)
            {
                return Conflict("Error al actualizar la boleta. Por favor, inténtelo nuevamente más tarde.");
            }

            
        }

    }
}
