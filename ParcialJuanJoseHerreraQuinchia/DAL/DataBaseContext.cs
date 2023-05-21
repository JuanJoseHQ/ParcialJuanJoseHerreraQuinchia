using Microsoft.EntityFrameworkCore;
using ParcialJuanJoseHerreraQuinchia.DAL.Entities;

namespace ParcialJuanJoseHerreraQuinchia.DAL

{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Tickets> Tickets { get; set; }
    }
}
