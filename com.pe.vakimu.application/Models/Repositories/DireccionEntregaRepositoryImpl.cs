using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Models.Entities;
using com.pe.vakimu.application.Models.Response;


namespace com.pe.vakimu.application.Models.Repositories{
    public class DireccionEntregaRepositoryImpl 
    {

        private readonly VakimuDbContext _context;

        public DireccionEntregaRepositoryImpl(VakimuDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DireccionEntregaResponse> Add(DireccionEntregaDto dto)
        {
            var direccionEntrega = new DireccionEntrega
            {
                OrdenId = dto.OrdenId,
                ClienteId = dto.ClienteId,
                DireccionLinea1 = dto.DireccionLinea1,
                DireccionLinea2 = dto.DireccionLinea2,
                Ciudad = dto.Ciudad,
                Distrito = dto.Distrito,
                CodigoPostal = dto.CodigoPostal,
                Referencia = dto.Referencia
            };

            _context.DireccionesEntrega.Add(direccionEntrega);
            await _context.SaveChangesAsync();
            return new DireccionEntregaResponse
            {
                Id = direccionEntrega.Id,
                OrdenId = direccionEntrega.OrdenId,
                ClienteId = direccionEntrega.ClienteId,
                DireccionLinea1 = direccionEntrega.DireccionLinea1,
                DireccionLinea2 = direccionEntrega.DireccionLinea2,
                Ciudad = direccionEntrega.Ciudad,
                Distrito = direccionEntrega.Distrito,
                CodigoPostal = direccionEntrega.CodigoPostal,
                Referencia = direccionEntrega.Referencia
            };

        }

        public async Task<bool> Delete(int id)
        {
            var direccionEntrega = await _context.DireccionesEntrega.FindAsync(id);
            if (direccionEntrega == null)
                return false;

            _context.DireccionesEntrega.Remove(direccionEntrega);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DireccionEntregaResponse>> GetAll()
        {
            var direcciones = await _context.DireccionesEntrega
                .Include(d => d.Orden)
                .Include(d => d.Cliente)
                .AsNoTracking()
                .ToListAsync();

            return direcciones.Select(d => new DireccionEntregaResponse
            {
                Id = d.Id,
                OrdenId = d.OrdenId,
                ClienteId = d.ClienteId,
                DireccionLinea1 = d.DireccionLinea1,
                DireccionLinea2 = d.DireccionLinea2,
                Ciudad = d.Ciudad,
                Distrito = d.Distrito,
                CodigoPostal = d.CodigoPostal,
                Referencia = d.Referencia
            });
        }


        public async Task<DireccionEntregaResponse> GetById(int id)
        {
            var direccionEntrega = await _context.DireccionesEntrega
                .Include("Orden")
                .Include("Cliente")
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (direccionEntrega == null)
                throw new KeyNotFoundException($"DireccionEntrega with id {id} not found.");

            return new DireccionEntregaResponse
            {
                Id = direccionEntrega.Id,
                OrdenId = direccionEntrega.OrdenId,
                ClienteId = direccionEntrega.ClienteId,
                DireccionLinea1 = direccionEntrega.DireccionLinea1,
                DireccionLinea2 = direccionEntrega.DireccionLinea2,
                Ciudad = direccionEntrega.Ciudad,
                Distrito = direccionEntrega.Distrito,
                CodigoPostal = direccionEntrega.CodigoPostal,
                Referencia = direccionEntrega.Referencia
            };
        }


        public async Task<bool> Update(int id, DireccionEntregaUpdateDto dto)
        {
            var direccionEntrega = await _context.DireccionesEntrega.FindAsync(id);
            if (direccionEntrega == null)
                return false;

            if (dto.OrdenId.HasValue) direccionEntrega.OrdenId = dto.OrdenId.Value;
            if (dto.ClienteId.HasValue) direccionEntrega.ClienteId = dto.ClienteId.Value;
            if(dto.DireccionLinea1 != null) direccionEntrega.DireccionLinea1 = dto.DireccionLinea1;
            if (dto.DireccionLinea2 != null) direccionEntrega.DireccionLinea2 = dto.DireccionLinea2;
            if (dto.Ciudad != null) direccionEntrega.Ciudad = dto.Ciudad;
            if (dto.Distrito != null) direccionEntrega.Distrito = dto.Distrito;
            if (dto.CodigoPostal != null) direccionEntrega.CodigoPostal = dto.CodigoPostal;
            if (dto.Referencia != null) direccionEntrega.Referencia = dto.Referencia;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}