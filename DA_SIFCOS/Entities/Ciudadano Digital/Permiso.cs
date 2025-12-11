using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_SIFCOS.Entities.Ciudadano_Digital
{
    public class Permiso
    {
        public Int16 IdTipoDocumentacion { get; set; }

        public String NombreTipoDocumentacion { get; set; }

        public String Upload { get; set; }

        public String Discard { get; set; }

        public String Acumulable { get; set; }
    }
}