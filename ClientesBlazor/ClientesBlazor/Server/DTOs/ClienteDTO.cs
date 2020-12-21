using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesBlazor.Server.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int IdGrupo { get; set; }
        public string Grupo { get; set; }
        public string Nombre { get; set; }
        public string Rnc { get; set; }
        public List<ArticuloDTO> Articulos { get; set; }
    }
}
