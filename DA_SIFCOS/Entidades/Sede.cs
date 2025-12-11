using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
    
namespace DA_SIFCOS.Entidades
{
    public class Sede
    {
        public String IdSede { get; set; }
        public String NombreSede { get; set; }
        public String IdVin_Sede { get; set; }
        
        /// <summary>
        /// TRUE: indica que la sede ya está cargada en Sifcos. FALSE: que no existe en sifcos.
        /// </summary>
        public bool PerteneceSifcos { get; set; }
      

    }
   
}
