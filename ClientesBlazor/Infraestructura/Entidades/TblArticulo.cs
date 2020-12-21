using System;
using System.Collections.Generic;

#nullable disable

namespace Infraestructura.Entidades
{
    public partial class TblArticulo
    {
        public TblArticulo()
        {
            TblClienteArticulos = new HashSet<TblClienteArticulo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public decimal? Precio { get; set; }

        public virtual ICollection<TblClienteArticulo> TblClienteArticulos { get; set; }
    }
}
