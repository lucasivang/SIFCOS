using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_SIFCOS.Entities.CDDResponse
{
    public class CDDResponseAplicacionPermisosCatalogos: CDDResponse
    {
        public List<Permiso> Lista_Catalogos_Permisos { get; set; }
    }

    public class Permiso
    {
        public Int32 Id_Catalogo { get; set; }
        public String N_Catalogo { get; set; }
        public String N_Permiso { get; set; }
    }

}