
using ParcialJuanJoseHerreraQuinchia.DAL.Entities;

namespace ParcialJuanJoseHerreraQuinchia.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;
        public SeederDb(DataBaseContext context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            
            await _context.Database.EnsureCreatedAsync(); //Esta línea me ayuda a crear mi BD de forma automática
            await TicketsAsync();
        }

        private async Task TicketsAsync()
        {
            if (!_context.Tickets.Any())
            {
                for (int x = 0; x < 5000; x++)
                {
                        _context.Tickets.Add( new Tickets
                    {
                        Id = Guid.NewGuid(),
                        EntranceGate = null,
                        IsUsed= false,
                        UseDate = null
                    });
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
