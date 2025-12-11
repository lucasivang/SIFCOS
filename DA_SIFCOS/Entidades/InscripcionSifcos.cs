using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class InscripcionSifcos
    {
        public String NroTramite { get; set; }

        public String IdEntidad { get; set; }
        public String CUIT { get; set; }
        public String NroSifcos { get; set; }
        public String IdSede { get; set; }
        public String Local { get; set; }
        public String Oficina { get; set; }
        public String Stand { get; set; }
        public String Cobertura { get; set; }
        public String Seguro { get; set; }
        public String Propietario { get; set; }
        public String Latitud { get; set; } 
        public String Longitud { get; set; } 
        public String CapacUltAnio { get; set; }
        public String CuilUsuarioCidi { get; set; }
        public Int64? TipoTramite { get; set; } 
        public DateTime FecIniTramite { get; set; } 
        public String NroHabilitacion { get; set; }
        public Int64? IdOrigenProveedor { get; set; } 
        //public string IdTramiteTasa { get; set; } 
        public Int64? IdVinDomLegal { get; set; }
        public Int64? IdVinDomLocal { get; set; }
        public String CantTotalpers { get; set; }
        public String CantPersRelDep { get; set; }
        public String idActividadPri { get; set; }
        public String idActividadSec { get; set; }
        public Int64? IdCargo { get; set; } 
        public String CuilRepLegal { get; set; }
        public String CuilGestor { get; set; }
        public String EmailGestor { get; set; }
        public String TelGestor { get; set; }
        public String NroMatricula { get; set; }
        public Int64? IdTipoGestor { get; set; } 
        public Int64? SupVentas { get; set; }
        public Int64? SupDeposito { get; set; }
        public String RangoAlquiler { get; set; }
        public Int64? SupAdministracion { get; set; }
        public String Resultado { get; set; }//NO LO CARGO
        public List<Producto> Productos { get; set; }
        public List<Trs> ListaTrs { get; set; }
        public TipoTramiteEnum TipoTramiteNum { get; set; }
        public Int64 IdEstado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public String Tipo_Doc_Reimpresion { get; set; }

        /// <summary>
        /// Representa cuantos reempadranamientos representa el trámite.
        /// </summary>
        public Int64 CantidadReempadranamientos { get; set; }
		/// <summary>
		/// NombreComercios es el campo que representa el NOMBRE DE FANTASÍA del comercio (cuit)
		/// </summary>
		public String NombreComercio { get; set; }
		public ComercioDto DomComercio { get; set; }

		// se agrega el id para digitalizar documentacion y guardar el id en la tabla t_tramites
		public int? Id_Documento1_CDD { get; set; }
		// se agrega el id para digitalizar documentacion y guardar el id en la tabla t_tramites
		public int? Id_Documento2_CDD { get; set; }

		// se agrega el id para digitalizar documentacion y guardar el id en la tabla t_tramites
		public int? Id_Documento3_CDD { get; set; }
		// se agrega el id para digitalizar documentacion y guardar el id en la tabla t_tramites
		public int? Id_Documento4_CDD { get; set; }

		public int IdOrganismo { get; set; } 

    }
    public enum TipoTramiteEnum
    {
        Instripcion_Sifcos = 1,
        Reempadronamiento,
        Modificacion,
        Baja_de_Comercio,
        Reimpresion_Oblea,
        Reempadronamiento_ViejoSifcos
    }

    

}




//txtApellidoCidi
//txtCalle
//txtCalleLegal
//txtCantPersRelDep
//txtCantPersTotal
//txtCelular
//txtCelularCodArea
//txtCodPos
//txtCodPosLegal
//txtCuilCidi
//txtCuilCidiRepresentante
//txtCuit
//txtCuitLeido
//txtCuit_FilteredTextBoxExtender
//txtDniConta
//txtEmailC
//txtEmailConta
//txtFechaIniAct
//txtLocal
//txtLocalLegal
//txtM2Admin
//txtM2Dep
//txtM2Venta
//txtNomApeConta
//txtNomFantasia
//txtNombreCidi
//txtNombreRepresentante
//txtNroCalle
//txtNroCalleLegal
//txtNroDGR
//txtNroDepto
//txtNroDeptoLegal
//txtNroHabMun
//txtNroMatricula
//txtNroSifcos
//txtOficina
//txtOficinaLegal
//txtPiso
//txtPisoLegal
//txtRazonSocial
//txtRedSocial
//txtStand
//txtStandLegal
//txtTelConta
//txtTelFijo
//txtTelFijoCodArea
//txtWebPage