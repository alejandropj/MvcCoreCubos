using Microsoft.EntityFrameworkCore;
using MvcCoreCubos.Models;

namespace MvcCoreCubos.Data
{
    public class CuboContext:DbContext
    {
        public CuboContext(DbContextOptions<CuboContext> options)
            : base(options) { }

        public DbSet<Cubo> Cubos { get; set; }
    }
}
