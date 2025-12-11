using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class consultaTramite
    {
        public String idEntidad { get; set; }
        public String CUIT { get; set; }
        public String id_sede_entidad { get; set; }
        public String Razon_Social { get; set; }
        public String Nombre_Fantasia { get; set; }
        public String Nro_Sifcos { get; set; }
        public String Nro_tramite { get; set; }
        public String inicio_actividad { get; set; }
        public String fec_alta { get; set; }
        public String estado { get; set; }
        public String id_estado { get; set; }
        public String desc_estado { get; set; }
        public String fecha_estado { get; set; }
        public String DomLocal { get; set; }
        public String latitud { get; set; }
        public String longitud { get; set; }
        public String CuilUsuarioCidi { get; set; }
        public String Tipo_Tramite { get; set; }
        public String Vto_Tramite { get; set; }
        

        /// <summary>
        /// Campo que indica si es del sifcos viejo el tramite o del sifcos nuevo. valores : "SIFCOS_VIEJO" Ó "SIFCOS_NUEVO"
        /// </summary>
        public String Origen { get; set; }

        /// Fecha de vencimiento del trámite
        /// Autor: (IB)
        public DateTime fec_vencimiento { get; set; }
    }
}
