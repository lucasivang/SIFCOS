using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_SIFCOS.Entities.CDDResponse
{
    public class CDDResponseDocumentacion : CDDResponse
    {
        /// <summary>
        /// Listado de información de documentación digitalizada.
        /// </summary>
        public List<Documentacion> Lista_Documentos { get; set; }

        public CDDResponseDocumentacion()
        {
            Lista_Documentos = new List<Documentacion>();
        }
    }
}