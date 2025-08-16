using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Models.Response;
using com.pe.vakimu.application.Services;

namespace com.pe.vakimu.application.Controllers.Web
{


    public class CheckoutController: Controller
    {
        private readonly DepartamentoService _departamentoService;
        private readonly SucursalService _sucursalService;
        private readonly MetodoPagoService _metodoPagoService;
        private readonly ClienteService _clienteService;
        private readonly OrdenService _ordenService;
        
        public CheckoutController()
        {
            var context = new VakimuDbContext();
            _departamentoService = new DepartamentoService(context);
            _sucursalService = new SucursalService(context);
            _metodoPagoService = new MetodoPagoService(context);
            _clienteService = new ClienteService(context);
            _ordenService = new OrdenService(context);
            
        }
        
        public async Task<ActionResult> Index()
        {

            var model = new CheckoutView
            {
                Departamentos = (await _departamentoService.ObtenerDepartamentos()).ToList(),
                Sucursales = (await _sucursalService.ObtenerSucursales()).ToList(),
                MetodosPago = (await _metodoPagoService.ObtenerMetodosPago()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> OrdenarPedido(CheckoutRequest data)
        {
            try
            {
                
                var clienteCreado = await _clienteService.CrearCliente(data.Cliente);

                data.OrdenDetalle.Orden.ClienteId = clienteCreado.Id;
                if (data.OrdenDetalle.Orden.Tipo == "delivery")
                {
                    data.OrdenDetalle.Orden.SucursalId = null;
                }
                await _ordenService.CrearOrdenConDetalles(
                    data.OrdenDetalle.Orden,
                    data.OrdenDetalle.Detalles,
                    data.OrdenDetalle.Direccion
                );

                return Json(new { success = true });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        

        }



        
    }
}