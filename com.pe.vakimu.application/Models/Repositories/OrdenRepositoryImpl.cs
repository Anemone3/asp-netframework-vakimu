using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using com.pe.vakimu.application.Models.Entities;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Models.Response;

namespace com.pe.vakimu.application.Models.Repositories
{
    public class OrdenRepositoryImpl
    {

        private readonly VakimuDbContext _context;

        public OrdenRepositoryImpl(VakimuDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
            
        
        // manejo para transacciones
        public DbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }


        public async Task<List<OrdenDetalleConCliente>> GetOrdensByClientId(int clientId)
        { 
            var ordenes = await _context.Ordenes
                .Include(o => o.Cliente)
                .Include(o => o.Sucursal)
                 .Include(o => o.Cliente.Departamento)
                .Include(o => o.MetodoPago)
                .Where(o => o.ClienteId == clientId)
                .AsNoTracking()
                .ToListAsync();

            return ordenes.Select(o => new OrdenDetalleConCliente
            {
                Id = o.Id,
                Tipo = o.Tipo,
                Estado = o.Estado,
                Descripcion = o.Descripcion,
                Ruc = o.Ruc,
                MetodoPagoId = o.MetodoPagoId,
                Cliente = new ClienteResponse
                {
                    Id = o.Cliente.Id,
                    Nombre = o.Cliente.Nombre,
                    Apellidos = o.Cliente.Apellidos,
                    Correo = o.Cliente.Correo,
                    Direccion = o.Cliente.Direccion,
                    DescripcionCasa = o.Cliente.DescripcionCasa,
                    Telefono = o.Cliente.Telefono,
                    Dni = o.Cliente.Dni,
                    Distrito = o.Cliente.Distrito,
                    Departamento = new DepartamentoResponse
                    {
                        Id = o.Cliente.Departamento.Id,
                        Nombre = o.Cliente.Departamento.Nombre
                    }
                },
                Sucursal = new SucursalResponse
                {
                    Id = o.Sucursal.Id,
                    Calle = o.Sucursal.Calle,
                    DepartamentoId = o.Sucursal.DepartamentoId,
                    Latitud = o.Sucursal.Latitud,
                    Longitud = o.Sucursal.Longitud,
                    Sede = o.Sucursal.Sede,
                    Ubicacion = o.Sucursal.Ubicacion
                }
   
            }).ToList();
        }

        public async Task<OrdenConDetalleResponse> GetByOrderId(int orderId)
        {
            var orden = await _context.Ordenes.Include(o => o.Detalles.Select(c => c.Producto)).Where(x=> x.Id == orderId).FirstOrDefaultAsync();

            if (orden == null)
            {
                throw new KeyNotFoundException($"Orden with id {orderId} not found.");
            }

            var ordenResponse = new OrdenConDetalleResponse
            {
                Id = orden.Id,
                ClienteId = orden.ClienteId,
                SucursalId = orden.SucursalId,
                Tipo = orden.Tipo,
                Estado = orden.Estado,
                Descripcion = orden.Descripcion,
                Ruc = orden.Ruc,
                MetodoPagoId = orden.MetodoPagoId,
                DetalleOrdenes = orden.Detalles.Select(d => new DetalleOrdenResponse
                {
                    Id = d.Id,
                    ProductoId = d.ProductoId,
                    Descripcion = d.Descripcion,
                    Descuento = d.Descuento,
                    Cantidad = d.Cantidad,
                    Subtotal = d.Subtotal,
                    Total = d.Total
                }).ToList()
            };

            return ordenResponse;
        }
        public async Task<OrdenResponse> Add(OrdenDto dto)
        {
            try
            {
                var order = new Orden
                {
                    ClienteId = dto.ClienteId,
                    SucursalId = dto.SucursalId,
                    Tipo = dto.Tipo,
                    Estado = dto.Estado,
                    Descripcion = dto.Descripcion,
                    Ruc = dto.Ruc,
                    MetodoPagoId = dto.MetodoPagoId
                };

                _context.Ordenes.Add(order);
                await _context.SaveChangesAsync();

                return new OrdenResponse
                {
                    Id = order.Id,
                    ClienteId = order.ClienteId,
                    SucursalId = order.SucursalId,
                    Tipo = order.Tipo,
                    Estado = order.Estado,
                    Descripcion = order.Descripcion,
                    Ruc = order.Ruc,
                    MetodoPagoId = order.MetodoPagoId
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw ;
            }
        }


        public async Task<bool> Delete(int id)
        {
            var orden = await _context.Ordenes.FindAsync(id);

            if (orden == null)
                return false;
            _context.Ordenes.Remove(orden);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrdenDetalleConCliente>> GetAll()
        {
            var ordenes = await _context.Ordenes
                .Include(o => o.Cliente)
                .Include(o => o.Cliente.Departamento)
                .Include(o => o.Sucursal)
                .Include(o => o.MetodoPago)
                .AsNoTracking()
                .ToListAsync();

            return ordenes.Select(o => new OrdenDetalleConCliente
            {
                Id = o.Id,
                Tipo = o.Tipo,
                Estado = o.Estado,
                Descripcion = o.Descripcion,
                Ruc = o.Ruc,
                MetodoPagoId = o.MetodoPagoId,
                Cliente = new ClienteResponse
                {
                    Id = o.Cliente.Id,
                    Nombre = o.Cliente.Nombre,
                    Apellidos = o.Cliente.Apellidos,
                    Correo = o.Cliente.Correo,
                    Direccion = o.Cliente.Direccion,
                    DescripcionCasa = o.Cliente.DescripcionCasa,
                    Telefono = o.Cliente.Telefono,
                    Dni = o.Cliente.Dni,
                    Distrito = o.Cliente.Distrito,
                    Departamento = new DepartamentoResponse
                    {
                        Id = o.Cliente.Departamento.Id,
                        Nombre = o.Cliente.Departamento.Nombre
                    }
                },
                Sucursal = new SucursalResponse
                {
                    Id = o.Sucursal.Id,
                    Calle = o.Sucursal.Calle,
                    DepartamentoId = o.Sucursal.DepartamentoId,
                    Latitud = o.Sucursal.Latitud,
                    Longitud = o.Sucursal.Longitud,
                    Sede = o.Sucursal.Sede,
                    Ubicacion = o.Sucursal.Ubicacion
                }
            });
        }


        public async Task<OrdenDetalleConCliente> GetById(int id)
        {
            var orden = await _context.Ordenes
                .Include(o => o.Cliente)
                .Include(o => o.Cliente.Departamento)
                .Include(o => o.Sucursal)
                .Include(o => o.MetodoPago)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            return orden == null
                ? throw new KeyNotFoundException($"Orden with id {id} not found.")
                : new OrdenDetalleConCliente
            {
                Id = orden.Id,
                Tipo = orden.Tipo,
                Estado = orden.Estado,
                Descripcion = orden.Descripcion,
                Ruc = orden.Ruc,
                MetodoPagoId = orden.MetodoPagoId,
                Cliente = new ClienteResponse
                {
                    Id = orden.Cliente.Id,
                    Nombre = orden.Cliente.Nombre,
                    Apellidos = orden.Cliente.Apellidos,
                    Correo = orden.Cliente.Correo,
                    Direccion = orden.Cliente.Direccion,
                    DescripcionCasa = orden.Cliente.DescripcionCasa,
                    Telefono = orden.Cliente.Telefono,
                    Dni = orden.Cliente.Dni,
                    Distrito = orden.Cliente.Distrito,
                    Departamento = new DepartamentoResponse
                    {
                        Id = orden.Cliente.Departamento.Id,
                        Nombre = orden.Cliente.Departamento.Nombre
                    }
                },
                Sucursal = new SucursalResponse
                {
                    Id = orden.Sucursal.Id,
                    Calle = orden.Sucursal.Calle,
                    DepartamentoId = orden.Sucursal.DepartamentoId,
                    Latitud = orden.Sucursal.Latitud,
                    Longitud = orden.Sucursal.Longitud,
                    Sede = orden.Sucursal.Sede,
                    Ubicacion = orden.Sucursal.Ubicacion
                }
            };
        }


        public async Task<bool> Update(int id, OrdenUpdateDto dto)
        {
            var orden = await _context.Ordenes.FindAsync(id);
            if (orden == null)
            {
                return false;
            }
            if(dto.ClienteId.HasValue) orden.ClienteId = dto.ClienteId.Value;
            if (dto.SucursalId.HasValue) orden.SucursalId = dto.SucursalId.Value;
            if(dto.Estado != null) orden.Estado = dto.Estado;
            if (dto.Tipo != null && (dto.Tipo == "retiro" || dto.Tipo == "delivery"))  orden.Tipo = dto.Tipo;
            if (dto.Descripcion != null) orden.Descripcion = dto.Descripcion;
            if (dto.Ruc != null) orden.Ruc = dto.Ruc;
            if (dto.MetodoPagoId.HasValue) orden.MetodoPagoId = dto.MetodoPagoId.Value;
            
            await _context.SaveChangesAsync();
            return true;

        }
    }
}