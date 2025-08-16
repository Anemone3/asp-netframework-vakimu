using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Models.Repositories;
using com.pe.vakimu.application.Models.Response;


namespace com.pe.vakimu.application.Services
{
    public class ClienteService
    {

        private readonly ClienteRepositoryImpl _clienteRepo;


        public ClienteService(VakimuDbContext _context)
        {
            _clienteRepo = new ClienteRepositoryImpl(_context);

        }
    
        
        // Crear un cliente
        public async Task<ClienteResponse> CrearCliente(ClienteDto dto)
        {
            return await _clienteRepo.Add(dto);
        }

        
        // Lista de clientes
        public async Task<IEnumerable<ClienteResponse>> ObtenerClientes()
        {
            return await _clienteRepo.GetAll();
        }
    }
}