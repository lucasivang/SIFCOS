using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class Persona
    {
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Sexo { get; set; }
        public long? DNI { get; set; }
        public Int64 id_numero { get; set; }
        public String Pai_cod_pais { get; set; }
        public String Cuil { get; set; }
        public String celular { get; set; } 
        public String email { get; set; }

        
    }
}
