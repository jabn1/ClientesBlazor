using System;
using System.Collections.Generic;

#nullable disable

namespace Infraestructura.Entidades
{
    public partial class TblGrupo
    {
        public TblGrupo()
        {
            TblClientes = new HashSet<TblCliente>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<TblCliente> TblClientes { get; set; }
    }
}
