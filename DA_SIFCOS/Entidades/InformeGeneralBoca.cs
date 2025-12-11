using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class InformeGeneralBoca
    {
        //encabezado
        public String Nro_tramite { get; set; }
        public String CUIT { get; set; }
        public String Razon_Social { get; set; }
        public String Nro_Sifcos { get; set; }
        public String tipo_tramite { get; set; }

        //datos contacto
        public String email { get; set; }
        public String telefono { get; set; }
        public String celular { get; set; }
        public String pag_web { get; set; }

        //datos actividad y productos

        public String tipoActividad { get; set; }
        public String RubroPrincipal { get; set; }
        public String RubroSecundario { get; set; }
        public String ActividadPrincipal { get; set; }
        public String ActividadSecundaria { get; set; }
        public Producto Productos { get; set; }

        //info general
        public String anio_op { get; set; }
        public String fecha_presentacion { get; set; }
        public String inicio_actividad { get; set; }
        public String nro_hab { get; set; }
        public String nro_dgr { get; set; }
        
        //datos hoja ruta tramite
        public String estado { get; set; }
        public String fecha_estado { get; set; }
        public String observaciones { get; set; }
        public String Usuario { get; set; }
        public String Nombre { get; set; }
        public String Organismo { get; set; }


    }
}
