using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    /// <summary>
    /// (PC) 11/2019
    /// Representación t_sif_relev_detalle
    /// </summary>
    public class SifRelevDetalle
    {
        public int IdRelevDetalle { get; set; }
        public int IdEntidad { get; set; }
        public int IdRelevamiento { get; set; }                
        public decimal Precio { get; set; }
        public DateTime FecRelevamiento { get; set; }
        
        public string Observaciones { get; set; }
        public int Vigente { get; set; }
        public string UsrAlta { get; set; }
        public DateTime? FecAlta { get; set; }
        public string UsrModif { get; set; }
        public DateTime? FecModif { get; set; }

        //Agregados (para reportes)
        public int? NroSifcos { get; set; }
        public string NEntidad { get; set; }
        public string NRelevamiento { get; set; }
        public string Codigo { get; set; }
        public string strFechaRelevamiento { get; set; }


    }
}
