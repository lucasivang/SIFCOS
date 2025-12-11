using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_SIFCOS.Entities
{
    public class MetadataDocumentacionCDD
    {
        public Int32 Id_Documento { get; set; }

        public String N_Documento { get; set; }

        public String N_Descripcion { get; set; }

        public String N_Catalogo { get; set; }

        public String Extension { get; set; }

        public byte[] Vista_Previa { get; set; }

        public String Peso_MB { get; set; }

        public String Paginas { get; set; }

        public DateTime? Vigencia { get; set; }

        public String N_Usuario { get; set; }

        public String N_Constatado { get; set; }
    }
}