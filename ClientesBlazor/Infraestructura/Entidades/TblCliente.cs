using System;
using System.Collections.Generic;

#nullable disable

namespace Infraestructura.Entidades
{
    public partial class TblCliente
    {
        public TblCliente()
        {
            TblClienteArticulos = new HashSet<TblClienteArticulo>();
        }

        public int Id { get; set; }
        public int IdPais { get; set; }
        public int IdGrupo { get; set; }
        public string Nombre { get; set; }
        public string Rnc { get; set; }

        public virtual TblGrupo IdGrupoNavigation { get; set; }
        public virtual TblPai IdPaisNavigation { get; set; }
        public virtual ICollection<TblClienteArticulo> TblClienteArticulos { get; set; }
    }
}
