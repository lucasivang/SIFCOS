using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    /// <summary>
    /// (IB) 05/2018
    /// Formato de trámite para panel de control
    /// </summary>
    public class tramitePanel
    {
        public String idEntidad { get; set; }
        public String CUIT { get; set; }
        public String id_sede_entidad { get; set; }
        public String Razon_Social { get; set; }
        public String Nombre_Fantasia { get; set; }
        public String Nro_Sifcos { get; set; }
        public String Nro_tramite { get; set; }
        public DateTime fec_ini_tramite { get; set; }
        public DateTime fec_alta { get; set; }
        public String Tramite { get; set; }
        public String estado { get; set; }
        public String id_estado { get; set; }
        public String desc_estado { get; set; }
        public DateTime fecha_estado { get; set; }
        public String DomLocal { get; set; }
        public String latitud { get; set; }
        public String longitud { get; set; }
        public String CuilUsuarioCidi { get; set; }
        public String Tipo_Tramite { get; set; }
        public String Domicilio { get; set; }
        public String calle { get; set; }
        public String altura { get; set; }
        public String piso { get; set; }
        public String depto { get; set; }
        public String torre { get; set; }
        public String mzna { get; set; }
        public String lote { get; set; }
        public String nLocalidad { get; set; }
        public String cpa { get; set; }
        public String cuitBoca { get; set; }
        public String Boca { get; set; }
        public DateTime fec_vencimiento { get; set; }
        public String TelefonoPrincipal { get; set; }
        public String CodigoArea { get; set; }
        public String Correo { get; set; }
        public String ActividadPri { get; set; }
        public String ActividadSec { get; set; } 
    }
}
