using System;
using System.Collections.Generic;

#nullable disable

namespace Infraestructura.Entidades
{
    public partial class TblClienteArticulo
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdArticulo { get; set; }

        public virtual TblArticulo IdArticuloNavigation { get; set; }
        public virtual TblCliente IdClienteNavigation { get; set; }
    }
}
