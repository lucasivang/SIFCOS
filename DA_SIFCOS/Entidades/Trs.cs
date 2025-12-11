using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class Trs
    {
        public int? AnioFiscal { get; set; }
        public string Importe { get; set; }
        public string Concepto { get; set; }
        public string NombreFormateado { get; set; }
        

        /// <summary>
        /// NroTransaccion es el valor devuelto al procesar la TRS. 
        /// NroTRansaccion (20 digitos) = id_transaccion (16 digitos) + anio_fiscal (4 digitos)
        /// </summary>
        public string NroTransaccion { get; set; }
        public string NroLiquidacion { get; set; }
        public string FechaCobro { get; set; }

        /// <summary>
        /// Representa la dirección a donde se obtiene el pdf de la tasa.
        /// </summary>
        public string Link { get; set; }
        
    }
}
