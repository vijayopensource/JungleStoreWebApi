using Microsoft.EntityFrameworkCore;

namespace JungleEntities.Data
{
    public class JungleContext:DbContext
    {
        public JungleContext(DbContextOptions<JungleContext> options):base(options)
        {

        }

        public DbSet<Animal> Animals { get; set; }

    }
}
