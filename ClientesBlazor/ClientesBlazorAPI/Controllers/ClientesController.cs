using ClientesBlazorAPI.DTOs;
using Infraestructura.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClientesBlazorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesDBContext context;

        public ClientesController(ClientesDBContext context)
        {
            this.context = context;
        }

        [HttpGet("clientes")]
        public async Task<ActionResult<List<ClienteDTO>>> GetClientes()
        {
            var clientes = await context.TblClientes
                .Include(g => g.IdGrupoNavigation)
                .Include(p => p.IdPaisNavigation)
                .Include(a => a.TblClienteArticulos)
                .ToListAsync();

            var clientesDTO = new List<ClienteDTO>();
            foreach (var cliente in clientes)
            {
                var articulosDTO = new List<ArticuloDTO>();
                foreach (var articulo in cliente.TblClienteArticulos)
                {
                    var articuloDTO = new ArticuloDTO() { 
                        Id = articulo.IdArticuloNavigation.Id, 
                        Nombre = articulo.IdArticuloNavigation.Nombre, 
                        Codigo = articulo.IdArticuloNavigation.Codigo, 
                        Precio = articulo.IdArticuloNavigation.Precio 
                    };
                    articulosDTO.Add(articuloDTO);
                }
                var clienteDTO = new ClienteDTO() { 
                    Id = cliente.Id, 
                    IdPais = cliente.IdPais,
                    Pais = cliente.IdPaisNavigation.Nombre, 
                    IdGrupo = cliente.IdGrupo, 
                    Grupo = cliente.IdGrupoNavigation.Nombre, 
                    Nombre = cliente.Nombre,
                    Rnc = cliente.Rnc,
                    Articulos = articulosDTO
                };
                clientesDTO.Add(clienteDTO);
            }
            return clientesDTO;
        }

        //[HttpGet("articulos")]
        //public async Task<ActionResult<List<ArticuloDTO>>> GetArticulos()
        //{

        //}

        //[HttpPost("articulos")]
        //public async Task<ActionResult> AddArticulosToCliente(List<int> articulos)
        //{

        //}

        //[HttpDelete("articulo")]
        //public async Task<ActionResult> DeleteArticuloFromCliente(int articulo)
        //{

        //}
    }
}
