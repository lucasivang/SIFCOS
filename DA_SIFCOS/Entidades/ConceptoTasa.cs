using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class ConceptoTasa
    {
        /// <summary>
        /// ID_CONCEPTO : Clave Primaria del Concepto de la Tasa
        /// </summary>
        public String id_concepto { get; set; }
        /// <summary>
        /// Fecha Desde : Clave Primaria del Concepto de la Tasa
        /// </summary>
        public String fecha_desde { get; set; } 
        public String n_concepto { get; set; }
        public String precio_base { get; set; }
        public String cantidad_base { get; set; }
        public String precio_extra { get; set; }
        public String  item { get; set; }
        public String id_concepto_padre { get; set; }
        public String fec_desde_padre { get; set; }
        public String id_unidad { get; set; }
        public DateTime fec_desde { get; set; }
        public String observaciones { get; set; }
        public String cantidad_desde { get; set; }
        public String cantidad_hasta { get; set; }
        public String fecha_hasta { get; set; }
        public String cod_ente { get; set; }
        //Autor IB: Datos agregados para nuevo esquema trs
        public DateTime fec_hasta { get; set; }
        public int IdTipoConcepto { get; set; }
        public string NTipoConcepto { get; set; }
        public string nro_liquidacion_original { get; set; }
    }
}
