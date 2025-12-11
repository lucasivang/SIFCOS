using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    /// <summary>
    /// (PC) 11/2019
    /// Representación t_sif_relevamientos
    /// </summary>
    public class SifRelevamientos
    {
        public int IdRelevamiento { get; set; }
        public string NRelevamiento { get; set; }
        public string Codigo { get; set; }
        public string Observaciones { get; set; }
        public string UsrAlta { get; set; }
        public DateTime? FecAlta { get; set; }
        public string UsrModif { get; set; }
        public DateTime? FecModif { get; set; }

        
    }
}
