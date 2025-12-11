using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class SifLiqOrganismos
    {
        public int IdLiqOrganismo { get; set; }
        public int IdOrganismo { get; set; }
        public int IdOrganismoSuperior { get; set; }
        public int IdLiquidacion { get; set; }
        public decimal TotalLiquidado { get; set; }
        public int Cantidad { get; set; }

        //RAZON_SOCIAL
        public string RazonSocial { get; set; }

    }
}
