﻿using ClientesBlazor.Shared.DTOs;
using Infraestructura.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesBlazor.Server.Controllers
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
                .ToListAsync();

            if (clientes == null) return StatusCode(StatusCodes.Status400BadRequest, "Failed to get clientes");

            var clientesDTO = new List<ClienteDTO>();
            foreach (var cliente in clientes)
            {
                var clienteDTO = new ClienteDTO()
                {
                    Id = cliente.Id,
                    IdPais = cliente.IdPais,
                    Pais = cliente.IdPaisNavigation.Nombre,
                    IdGrupo = cliente.IdGrupo,
                    Grupo = cliente.IdGrupoNavigation.Nombre,
                    Nombre = cliente.Nombre,
                    Rnc = cliente.Rnc
                };
                clientesDTO.Add(clienteDTO);
            }
            return clientesDTO;
        }

        [HttpGet("cliente/{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var cliente = await context.TblClientes
                .Include(g => g.IdGrupoNavigation)
                .Include(p => p.IdPaisNavigation)
                .Include(a => a.TblClienteArticulos)
                .ThenInclude(a => a.IdArticuloNavigation)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return StatusCode(StatusCodes.Status400BadRequest, "Invalid parameters");

            var articulosDTO = new List<ArticuloDTO>();
            foreach (var articulo in cliente.TblClienteArticulos)
            {
                var articuloDTO = new ArticuloDTO()
                {
                    Id = articulo.IdArticuloNavigation.Id,
                    Nombre = articulo.IdArticuloNavigation.Nombre,
                    Codigo = articulo.IdArticuloNavigation.Codigo,
                    Precio = articulo.IdArticuloNavigation.Precio
                };
                articulosDTO.Add(articuloDTO);
            }
            var clienteDTO = new ClienteDTO()
            {
                Id = cliente.Id,
                IdPais = cliente.IdPais,
                Pais = cliente.IdPaisNavigation.Nombre,
                IdGrupo = cliente.IdGrupo,
                Grupo = cliente.IdGrupoNavigation.Nombre,
                Nombre = cliente.Nombre,
                Rnc = cliente.Rnc,
                Articulos = articulosDTO
            };

            return clienteDTO;
        }

        [HttpGet("articulos")]
        public async Task<ActionResult<List<ArticuloDTO>>> GetArticulos()
        {
            var articulos = await context.TblArticulos.ToListAsync();

            if (articulos == null) return StatusCode(StatusCodes.Status400BadRequest, "Failed to get articulos");

            var articulosDTO = new List<ArticuloDTO>();
            foreach (var articulo in articulos)
            {
                articulosDTO.Add(new ArticuloDTO()
                {
                    Id = articulo.Id,
                    Nombre = articulo.Nombre,
                    Precio = articulo.Precio,
                    Codigo = articulo.Codigo
                });
            }
            return articulosDTO;
        }

        [HttpPost("articulo/{idcliente}/{idarticulo}")]
        public async Task<ActionResult> AddArticuloToCliente(int idarticulo, int idcliente)
        {
            var articulos = new List<TblArticulo>();

            var articulo = await context.TblArticulos.FindAsync(idarticulo);
            if (articulo == null) return StatusCode(StatusCodes.Status400BadRequest, "Invalid parameters");
            articulos.Add(articulo);

            var cliente = await context.TblClientes.FindAsync(idcliente);
            if (cliente == null) return StatusCode(StatusCodes.Status400BadRequest, "Invalid parameters");


            cliente.TblClienteArticulos.Add(new TblClienteArticulo() { IdArticuloNavigation = articulo });

            if (await context.SaveChangesAsync() == 0) return StatusCode(StatusCodes.Status400BadRequest, "Failed to add articulos to cliente");

            return Ok();
        }

        [HttpDelete("articulo/{idcliente}/{idarticulo}")]
        public async Task<ActionResult> DeleteArticuloFromCliente(int idarticulo, int idcliente)
        {
            var cliente = await context.TblClientes
                .Include(a => a.TblClienteArticulos)
                .ThenInclude(a => a.IdArticuloNavigation)
                .FirstOrDefaultAsync(c => c.Id == idcliente);
            if (cliente == null) return StatusCode(StatusCodes.Status400BadRequest, "Invalid parameters");

            var articulosCliente = cliente.TblClienteArticulos.Where(a => a.IdArticulo == idarticulo);
            if (articulosCliente.Count() == 0) return StatusCode(StatusCodes.Status400BadRequest, "Invalid parameters");

            context.RemoveRange(articulosCliente);

            if (await context.SaveChangesAsync() == 0) return StatusCode(StatusCodes.Status400BadRequest, "Failed to remove articulo from cliente");


            return Ok();
        }

    }
}
