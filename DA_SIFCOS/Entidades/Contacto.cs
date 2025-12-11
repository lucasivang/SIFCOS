using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class Contacto
    {
        public string IdTipoComunicacion { get; set; }
        public string IdEntidad { get; set; }
        public string NroMail { get; set; }
        public string CodArea { get; set; }
        public int IdAplicacion { get; set; }
        public string TablaOrigen { get; set; }
        
        public string NTipoComunicacion { get; set; }
    }

    public class TipoComunicacion
    {
        public string IdTipoComunicacion { get; set; }
        public string NTipoComunicacion { get; set; }
    }

    public class TipoContactoTramite
    {
        public int IdTipoContactoTramite { get; set; }
        public string NTipoContactoTramite { get; set; }
    }
}
