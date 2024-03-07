using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MvcCoreCubos.Models;
using MvcCoreCubos.Repositories;

namespace MvcCoreCubos.Controllers
{
    public class CuboController : Controller
    {
        private RepositoryCubos repo;
        private IMemoryCache memoryCache;
        public CuboController(RepositoryCubos repo, IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
        }
        public async Task<IActionResult> Index(int? idCarrito)
        {
            if (idCarrito != null)
            {
                List<Cubo> cubosCarrito;
                if (this.memoryCache.Get("CARRITO") == null)
                {
                    cubosCarrito = new List<Cubo>();
                }
                else
                {
                    cubosCarrito = this.memoryCache.Get<List<Cubo>>("CARRITO");
                }
                Cubo cubo = await this.repo.FindCuboAsync(idCarrito.Value);
                cubosCarrito.Add(cubo);
                this.memoryCache.Set("CARRITO", cubosCarrito);
            }
            List<Cubo> cubos = await this.repo.GetCubosAsync();
            return View(cubos);
        }
        public async Task<IActionResult> CarritoCubos(int? ideliminar)
        {
            if (ideliminar != null)
            {
                List<Cubo> cubos =
                    this.memoryCache.Get<List<Cubo>>("CARRITO");
                Cubo cubo =
                    cubos.Find(z => z.IdCubo == ideliminar.Value);

                //Eliminamos
                cubos.Remove(cubo);
                //PREGUNTAMOS SI NOS QUEDAN FAVORITOS
                if (cubos.Count == 0)
                {
                    //ELIMINAMOS LA KEY FAVORITOS
                    this.memoryCache.Remove("CARRITO");
                    this.memoryCache.Remove("TOTAL");
                }
                else
                {
                    //ACTUALIZAMOS MEMORYCACHE
                    this.memoryCache.Set("CARRITO", cubos);
                }
            }
            List<Cubo> carrito = this.memoryCache.Get<List<Cubo>>("CARRITO");
            if (carrito != null)
            {
                int suma = carrito.Sum(x => x.Precio);
                this.memoryCache.Set("TOTAL", suma);
            }
            return View();
        }
        public async Task<IActionResult> ComprarCubos()
        {

            return View();
        }
    }
}        

