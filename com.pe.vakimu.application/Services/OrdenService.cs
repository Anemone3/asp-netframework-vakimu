using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Models.Repositories;
using com.pe.vakimu.application.Models.Response;


namespace com.pe.vakimu.application.Services
{
    public class OrdenService
    {
        private readonly OrdenRepositoryImpl _ordenRepo;
        private readonly ClienteRepositoryImpl _clienteRepo;
        private readonly DetalleOrdenRepositoryImpl _detalleRepo;
        private readonly ProductoRepositoryImpl _productoRepo;
        private readonly DireccionEntregaRepositoryImpl _direccionEntregaRepo;
        
        public OrdenService(VakimuDbContext context)
        {
            _ordenRepo = new OrdenRepositoryImpl(context);
            _clienteRepo = new ClienteRepositoryImpl(context);
            _detalleRepo = new DetalleOrdenRepositoryImpl(context);
            _productoRepo = new ProductoRepositoryImpl(context);
            _direccionEntregaRepo = new DireccionEntregaRepositoryImpl(context);
        }
    
        
        
        // Inicializar una Orden
        public async Task<OrdenConDetalleResponse> CrearOrdenConDetalles(OrdenDto ordenDto, List<DetalleOrdenDto> detalles, DireccionEntregaDto direccionEntrega)
        {
            if (detalles == null || !detalles.Any())
                throw new ArgumentException("La orden debe tener al menos un detalle.");

            var cliente = await _clienteRepo.GetById(ordenDto.ClienteId);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente con id {ordenDto.ClienteId} no encontrado.");




            if (ordenDto.Tipo == "retiro") ordenDto.SucursalId = null;
            
            using (var transaction = _ordenRepo.BeginTransaction())
            {
                try
                {
                    // Crear la orden base
                    var ordenCreada = await _ordenRepo.Add(ordenDto);

                    foreach (var detalle in detalles)
                    {
                        Console.WriteLine($"🛒 Producto ID: {detalle.ProductoId}, Cantidad: {detalle.Cantidad}");
                        if (detalle.Cantidad <= 0)
                            throw new ArgumentException($"Cantidad inválida para el producto con id {detalle.ProductoId}.");

                        // Validar producto
                        var producto = await _productoRepo.GetById(detalle.ProductoId);
                        if (producto == null)
                            throw new KeyNotFoundException($"Producto con id {detalle.ProductoId} no encontrado.");

                        if (!producto.Disponible)
                            throw new InvalidOperationException(
                                $"Producto no dispoible {producto.Nombre} (status: {producto.Disponible}");
                        // Validar stock
                        if (producto.Stock < detalle.Cantidad)
                            throw new InvalidOperationException($"Stock insuficiente para el producto {producto.Nombre} (disponible: {producto.Stock}).");

                        // Descontar stock
                        producto.Stock -= detalle.Cantidad;
                        
                        await _productoRepo.Update(producto.Id, new ProductoUpdateDto
                        {
                            Stock = producto.Stock
                        });

                        // Agregar detalle
                        detalle.OrdenId = ordenCreada.Id;
                        await _detalleRepo.Add(detalle);
                    }

                    transaction.Commit();
                    
                    

                    direccionEntrega.ClienteId = cliente.Id;
                    direccionEntrega.OrdenId = ordenCreada.Id;
                    
                    await _direccionEntregaRepo.Add(direccionEntrega);

                    return await _ordenRepo.GetByOrderId(ordenCreada.Id);
                }
                catch (InvalidOperationException ex)
                {
                    transaction.Rollback();
                     Console.Write(ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.Write(ex.Message);
                    throw new Exception("Ocurrió un error inesperado al crear la orden.", ex);
                }

            }
        }

        
        // Lista de los productos comprados segun el orden Id, con info de Cliente
        public async Task<OrdenConDetalleResponse> ObtenerDetallesPorOrdenId(int orderId)
        {
            return await _ordenRepo.GetByOrderId(orderId);
        }

        // Lista de ordenes hechas por un cliente Id
        public async Task<List<OrdenDetalleConCliente>> ObtenerOrdenesPorClienteId(int clienteId)
        {
            var client = await _clienteRepo.GetById(clienteId);
            if (client == null)
            {
                throw new KeyNotFoundException($"Cliente with id {clienteId} not found.");
            }
            return await _ordenRepo.GetOrdensByClientId(client.Id);

        }
       
        
        // Lista de ordenes
        public async Task<IEnumerable<OrdenDetalleConCliente>> ObtenerOrdenes()
        {
            var ordenes = await _ordenRepo.GetAll();

            return ordenes;
        }
        
        
        // Cambiar estado de la orden a pendiente | pagado | entregado | cancelado
        public async Task<bool> CambiarOrdenStatus( int ordenId, string status)
        {

            string[] statusValues = { "pendiente", "pagado", "entregado", "cancelado" };

            if(status == null || !statusValues.Contains(status.ToLower()))
            {
                throw new ArgumentException($"Estado inválido. Debe ser uno de los siguientes: {string.Join(", ", statusValues)}");
            }

            var orden = await _ordenRepo.GetByOrderId(ordenId);

            if (orden == null) throw new KeyNotFoundException($"Orden with id {ordenId} not found.");
            

            var ordenUpdateDto = new OrdenUpdateDto
            {
                Estado = status
            };

            return await _ordenRepo.Update(ordenId, ordenUpdateDto);
        }

        
        // Lista de detalles: todos los productos de la ordenes
        public async Task<IEnumerable<DetalleOrdenResponse>> ObtenerDetallesOrdenesTodo()
        {
            return await _detalleRepo.GetAll();
        }
        
        
        // Lista de detalles segun orden Id: todos las ordenes hechas
        public async Task<IEnumerable<DetalleOrdenResponse>> ObtenerOrdenesConDetalles(int orderId)
        {
            return await _detalleRepo.GetByOrderId(orderId);
        }



    }
}