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

        public TicketsController(IHttpClientFactory httpClient) 
        { 
            _httpCliente = httpClient;
        }
        public async Task<IActionResult> index()
        {   
            return View();
        
        }


        

    }
}
