using Microsoft.AspNetCore.Mvc;
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
                        Entrance = "North";
                        break;
                    case 2:
                        Entrance = "Eastern";
                        break;
                    case 3:
                        Entrance = "Western";
                        break;
                    case 4:
                        Entrance = "South";
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

        

    }
}
