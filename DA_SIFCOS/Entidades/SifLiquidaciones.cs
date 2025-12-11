using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS
{
    /// <summary>
    /// (IB) 03/2019
    /// Representación t_sif_liquidaciones
    /// </summary>
    public class SifLiquidaciones
    {
        public int IdLiquidacion { get; set; }
        public int? NroSifcosDesde { get; set; }
        public int? NroSifcosHasta { get; set; }
        public DateTime? FecDesde { get; set; }
        public DateTime? FecHasta { get; set; }
        public int IdTipoTramite { get; set; }
        public string IdUsuario { get; set; }
        public DateTime FecAlta { get; set; }
        public string NroExpediente { get; set; }
        public string NroResolucion { get; set; }
        public DateTime? FechaResolucion { get; set; }

        //Otros datos
        public string strFechaResolucion { get; set; }
        //Datos Maestros
        public string NTipoTramite { get; set; }//N_TIPO_TRAMITE

    }
}
