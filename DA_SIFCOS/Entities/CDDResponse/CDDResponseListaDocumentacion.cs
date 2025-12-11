using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_SIFCOS.Entities.CDDResponse
{
    public class CDDResponseListaDocumentacion : CDDResponse
    {
        public List<MetadataDocumentacionCDD> ListaMetadataCDD { get; set; }

        public CDDResponseListaDocumentacion()
        {
            ListaMetadataCDD = new List<MetadataDocumentacionCDD>();
        }
    }
}