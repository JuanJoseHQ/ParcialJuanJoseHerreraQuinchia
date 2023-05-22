using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ParcialJuanJoseHerreraQuinchia.DAL.Entities;
using System.Text.Json.Serialization;

namespace WebPages.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpCliente;

        public TicketsController(IHttpClientFactory httpClient, IConfiguration configuration) 
        { 
            _httpCliente = httpClient;
        }
        public async Task<IActionResult> index()
        {   
            return View();
        
        }

        public async Task<ActionResult> Income(Guid id)
        {
            var url = String.Format("https://localhost:7099/api/Tickets/Get/{0}", id);
            var json = await _httpCliente.CreateClient().GetStringAsync(url);
            Tickets ticket = JsonConvert.DeserializeObject<Tickets>(json);
            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTicket(Guid? ticketId, Tickets ticket)
        {
            var url = String.Format("https://localhost:7099/api/Tickets/Update/{0}", ticketId);
            await _httpCliente.CreateClient().PutAsJsonAsync(url, ticket);
            return Ok("Ticket updated successfully");
        }
        

        [HttpPost]
        public async Task<IActionResult> ValidateIncomeTicket(Guid? ticketId)
        {
            try
            {
                var url = String.Format("https://localhost:7099/api/Tickets/Get/{0}", ticketId);
                var json = await _httpCliente.CreateClient().GetStringAsync(url);
                Tickets ticket = JsonConvert.DeserializeObject<Tickets>(json);

                if (ticket == null)
                {
                    ViewData["Message"] = "invalid ticket";
                }
                else if (ticket.IsUsed)
                {
                    ViewData["Message"] = $"Ticket already used. Date of use: {ticket.UseDate}, porter's lodge: {ticket.EntranceGate}";
                }
                else
                {
                    await UpdateTicket(ticket.Id, ticket);
                    ViewData["Message"] = "Valid ticket, you can enter the concert.";
                }
            }
            catch (InvalidOperationException ex)
            {
                ViewData["Message"] = ex.Message;
            }
            catch (DbUpdateException)
            {
                ViewData["Message"] = "Error updating the ticket. Please try again later.";
            }
            
            return View("Income", ticketId);
        }




    }
}
