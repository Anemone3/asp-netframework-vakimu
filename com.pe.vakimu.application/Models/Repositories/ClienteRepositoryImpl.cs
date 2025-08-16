using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Models.Entities;

using com.pe.vakimu.application.Models.Response;


namespace com.pe.vakimu.application.Models.Repositories
{
    public class ClienteRepositoryImpl
    {

        private readonly VakimuDbContext _context;

        public ClienteRepositoryImpl(VakimuDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public  async Task<ClienteResponse> Add(ClienteDto dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Apellidos = dto.Apellidos,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Contrasena = dto.Contrasena,
                DepartamentoId = dto.DepartamentoId,
                DescripcionCasa = dto.DescripcionCasa,
                Dni = dto.Dni,
                Distrito = dto.Distrito
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            var departamento = await _context.Departamentos
                              .AsNoTracking()
                              .FirstOrDefaultAsync(d => d.Id == cliente.DepartamentoId);

            return new ClienteResponse
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Correo = cliente.Correo,
                Direccion = cliente.Direccion,
                DescripcionCasa = cliente.DescripcionCasa,
                Telefono = cliente.Telefono,
                Dni = cliente.Dni,
                Distrito = cliente.Distrito,
                Departamento = departamento != null
                    ? new DepartamentoResponse
                    {
                        Id = departamento.Id,
                        Nombre = departamento.Nombre
                    }
                    : null
            };
        }

        public async Task<bool> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ClienteResponse>> GetAll()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Departamento)
                .ToListAsync();

            return clientes.Select(c => new ClienteResponse
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellidos = c.Apellidos,
                Correo = c.Correo,
                Direccion = c.Direccion,
                DescripcionCasa = c.DescripcionCasa,
                Telefono = c.Telefono,
                Dni = c.Dni,
                Distrito = c.Distrito,
                Departamento = c.Departamento != null
                    ? new DepartamentoResponse
                    {
                        Id = c.Departamento.Id,
                        Nombre = c.Departamento.Nombre
                    }
                    : null
            });
        }

        public async Task<ClienteResponse> GetById(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Departamento)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                return null; 

            return new ClienteResponse
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Correo = cliente.Correo,
                Direccion = cliente.Direccion,
                DescripcionCasa = cliente.DescripcionCasa,
                Telefono = cliente.Telefono,
                Dni = cliente.Dni,
                Distrito = cliente.Distrito,
                Departamento = cliente.Departamento != null
                    ? new DepartamentoResponse
                    {
                        Id = cliente.Departamento.Id,
                        Nombre = cliente.Departamento.Nombre
                    }
                    : null
            };
        }


        public async Task<bool> Update(int id, ClienteUpdateDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return false;

            if (dto.DepartamentoId.HasValue) cliente.DepartamentoId = dto.DepartamentoId.Value;
            if(dto.Nombre != null) cliente.Nombre = dto.Nombre;
            if (dto.Apellidos != null) cliente.Apellidos = dto.Apellidos;
            if (dto.Correo != null) cliente.Correo = dto.Correo;
            if (dto.Telefono != null) cliente.Telefono = dto.Telefono;
            if (dto.Direccion != null) cliente.Direccion = dto.Direccion;
            if (dto.DescripcionCasa != null) cliente.DescripcionCasa = dto.DescripcionCasa;
            if (dto.Dni != null) cliente.Dni = dto.Dni;
            if (dto.Distrito != null) cliente.Distrito = dto.Distrito;

  

            await _context.SaveChangesAsync();
            return true;
        }
    }
}