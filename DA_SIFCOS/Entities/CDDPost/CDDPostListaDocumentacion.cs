using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DA_SIFCOS.Entities.CDDAutorizador;

namespace DA_SIFCOS.Entities.CDDPost
{
    public class CDDPostListaDocumentacion : CDDAutorizadorData
    {
        /// <summary>
        /// Cantidad de documentación a ser gestionada en la lista.
        /// </summary>
        public int CantidadDocumentacion { get; set; }

        /// <summary>
        /// Lista de identificadores de documento.
        /// </summary>
        public List<Int32> ListaIdDocumentos { get; set; }

        public CDDPostListaDocumentacion()
        {
            CantidadDocumentacion = 0;
            ListaIdDocumentos = new List<Int32>();
        }
    }
}