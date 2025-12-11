using System;
using System.Collections.Generic;

namespace DA_SIFCOS.Entities.Ciudadano_Digital
{
    public class Entrada
    {
        public int IdAplicacion { get; set; }
        public String Contrasenia { get; set; }
        public String HashCookie { get; set; }
        public String TokenValue { get; set; }
        public String TimeStamp { get; set; }
        public String Cuil { get; set; }
    }

    public class EntradaDoc : Entrada
    {
        public String CuilOperador { get; set; }
        public byte[] SharedKey { get; set; }
        public String Identificador { get; set; }
        public Documentacion Documentacion { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public int CantidadRegistros { get; set; }

        public EntradaDoc()
        {
            Documentacion = new Documentacion();
            Ubicacion = new Ubicacion();
        }
    }      
}