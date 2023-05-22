using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParcialJuanJoseHerreraQuinchia.DAL.Entities;
using System.Text.Json.Serialization;

namespace WebPages.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpCliente;

        public TicketsController(IHttpClientFactory httpClient) 
        { 
            _httpCliente = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7099/Get/27d6389b-4245-4a47-8d2e-0017ee89d4c0";
            var json = await _httpCliente.CreateClient().GetStringAsync(url);
            Tickets Ticket = JsonConvert.DeserializeObject<Tickets>(json);      
            return View(Ticket);
        
        }


    }
}
