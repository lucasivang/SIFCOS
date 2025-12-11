using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class PlanillaAsistenciaDTO
    {
        public String Nro_tramite { get; set; } // nro inscripcion
        public String Nombre_fantasia { get; set; } // nombre y apellido
        public String dni_rep_legal { get; set; } // cuil persona
        public String CUIT { get; set; }
        public String Razon_Social { get; set; }
        public String tipo_tramite { get; set; } // vip - invitado
        public String estado_tramite { get; set; } // presente - ausente
        public String celular { get; set; } // telefono contacto
        public string Email { get; set; }
    }
}
