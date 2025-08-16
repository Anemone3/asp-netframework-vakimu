using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Services;

namespace com.pe.vakimu.application.Controllers.Web
{
    public class HomeController : Controller
    {
        
        private readonly ProductoService _productoService;

        public HomeController()
        {
            var context = new VakimuDbContext();
            _productoService = new ProductoService(context);
        }
        // GET: Home
        public async Task<ActionResult> Index()
        {
            var productos = (await _productoService.ObtenerProductos()).ToList();
            return View(productos);
        }
    }
}