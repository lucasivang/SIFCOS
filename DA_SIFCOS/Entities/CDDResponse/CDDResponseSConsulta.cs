using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_SIFCOS.Entities.CDDResponse
{
    public class CDDResponseSConsulta : CDDResponse
    {
        /// <summary>
        /// Objeto que contiene los datos de la documentación digitalizada.
        /// </summary>
        public Imagen.S_Imagen Documentacion { get; set; }

        /// <summary>
        //// <summary>
        /// Nombre del documento.
        /// </summary>
        public String N_Documento { get; set; }

        /// <summary>
        /// Dato que contiene la fecha de Vigencia del documento.
        /// </summary>
        public DateTime? Fec_Vigencia { get; set; }

        /// <summary>
        /// Dato que contiene el catálogo al que pertenece el documento.
        /// </summary>
        public String N_Catalogo { get; set; }
    }
}