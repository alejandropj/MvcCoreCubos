using Microsoft.EntityFrameworkCore;
using MvcCoreCubos.Data;
using MvcCoreCubos.Models;

namespace MvcCoreCubos.Repositories
{
    public class RepositoryCubos
    {
        private CuboContext context;
        public RepositoryCubos(CuboContext context)
        {
            this.context = context;
        }
        public async Task<List<Cubo>> GetCubosAsync()
        {
            var cubos = await this.context.Cubos.ToListAsync();
            return cubos;
        }
        public async Task<Cubo> FindCuboAsync(int idCubo)
        {
            var cubo = await this.context.Cubos.FirstOrDefaultAsync(x => x.IdCubo == idCubo);
            return cubo;
        }
    }
}
