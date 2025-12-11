using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class PersonaJuridica
    {        
        public string Cuit { get; set; }
        public string IdSede { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public string IdFormaJuridica { get; set; }
        public string IdCondicionIva { get; set; }
        public string NroIngresoBruto { get; set; }
        public string NroHabMunicipal { get; set; }
        public string IdCondicionIngresoBruto { get; set; }
        public int IdAplicacion { get; set; }
        public DateTime FecAlta { get; set; }
        public string UsuarioAlta { get; set; }
        public DateTime? FecModif { get; set; }
        public string UsuarioModif { get; set; }
        public string NAbreviado { get; set; }
        
    }
}
