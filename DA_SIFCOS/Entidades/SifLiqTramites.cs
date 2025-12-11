using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS
{
    public class SifLiqTramites
    {
        public int IdLiqTramite { get; set; }
        public int IdLiqOrganismo { get; set; }
        public int NroTramiteSifcos { get; set; }
        public int IdOrganismo { get; set; }
        public decimal MontoLiquidado { get; set; }

        //Datos Maestros
        public string Cuit { get; set; }
        public string NroSifcos { get; set; }
        public string Local { get; set; }
        public string Stand { get; set; }
        public string RazonSocial { get; set; }
        public DateTime FecIniTramite { get; set; }
        public DateTime FecAlta { get; set; }
        public DateTime FecVencimiento { get; set; }

        public string Calle { get; set; }
        public int? Altura { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string Torre { get; set; }
        public string Mzna { get; set; }
        public string Lote { get; set; }
        public string Localidad { get; set; }
        public string Cpa { get; set; }
        
        public string Boca { get; set; }
        public string CuitBoca { get; set; }
        public string BocaSuperior { get; set; }
        public string CuitBocaSuperior { get; set; }
        public string NroLiquidacionOriginal { get; set; }


    }
}
