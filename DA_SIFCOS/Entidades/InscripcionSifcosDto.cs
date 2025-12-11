using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class InscripcionSifcosDto
    {
        //Datos Tramite

        
        
        public string NroTramiteSifcos { get; set; }

        public Int64? IdVinDomLegal { get; set; }
        public Int64? IdVinDomLocal { get; set; }
        public string CapacUltAnio { get; set; }
        public string CuilUsuarioCidi { get; set; }
        public string IdTipoTramite { get; set; }
        public string NombreTipoTramite { get; set; }
        
        public DateTime FecIniTramite { get; set; }
        public DateTime FecVencimiento { get; set; }
        
        //public string IdTransaccionTasa { get; set; }
        public string PagoTasa { get; set; } 

        public string CantTotalpers { get; set; }
        public string CantPersRelDep { get; set; }
        public string FechaIniActividad { get; set; }
        public string NroHabMunicipal { get; set; }
        public string NroDGR { get; set; }
        public string ActividadPrimaria { get; set; }
        public string ActividadSecundaria { get; set; }
        
        
        /// <summary>
        /// Nombre de la Localidad donde se certifica el trámite.
        /// </summary>
        public string LocalidadCertificacion { get; set; }
        public DateTime? FechaCertificacion { get; set; }



        //Datos de Origen Proveedor
        public string NombreOrigenProveedor { get; set; }
        public Int64? IdOrigenProveedor { get; set; } 
        
        //Datos de Estado Actual
        public string NombreEstadoActual { get; set; }
        

        //Datos Entidad
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public string CUIT { get; set; }
        public string NroSifcos { get; set; }
        public string IdSede { get; set; }
        public string NombreSede { get; set; }
        public string Local { get; set; }
        public string Oficina { get; set; }
        public string Stand { get; set; }
        public string Cobertura { get; set; }
        public string Seguro { get; set; }
        public string Propietario { get; set; }
        public string RangoAlquiler { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        
        //Datos Gestor Tramite


        /// <summary>
        /// CUIL del Usuario Cidi logueado y es el usuario que realizó el trámite vía web.
        /// </summary>
        public string CuilGestor { get; set; }
        public string CelularGestor { get; set; }

        /// <summary>
        /// Si es Titular, Gestor ó Contador. Se relaciona con la tabla T_TIPO_GESTOR.
        /// </summary>
        public string NombreTipoGestor { get; set; }
        public string NombreYApellidoGestor { get; set; }
        public string EmailGestor { get; set; }
        public string DniGestor { get; set; }
        
        //Datos Representante Legal
        public string NombreYApellidoRepLegal { get; set; }
        public string DniRepLegal { get; set; }
        public string CelularRepLegal { get; set; }
        /// <summary>
        /// Se relaciona con la tabla T_TIPOS_CARGO de un Representante Legal.
        /// </summary>
        public string NombreCargoRepLegal { get; set; }
        public Int64? IdCargo { get; set; }
        public String CuilRepLegal { get; set; }


        //Datos de Superficie
        public Int64? SupVentas { get; set; }
        public Int64? SupDeposito { get; set; }
        public Int64? SupAdministracion { get; set; }

        //Datos de Comunicacion
        public string IdEntidad { get; set; }
        public string EmailEstablecimiento { get; set; }
        public string CodAreaCelular { get; set; }
        public string Celular { get; set; }
        public string CodAreaTelFijo { get; set; }
        public string TelFijo { get; set; }
        public string WebPage { get; set; }
        public string Facebook { get; set; }

        //Se agrega una lista para la modificacion
        public List<Producto> Productos { get; set; } 

        public string Resultado { get; set; }

        //(IB) Se agrega el rubro primario y el secundario
        public string RubroPrimario { get; set; }
        public string RubroSecundario { get; set; }

        public int? IdRubroPrimario { get; set; }
        public int? IdRubroSecundario { get; set; }

        public string IdActividadPrimaria { get; set; }
        public string IdActividadSecundaria { get; set; }

		//--CDD

		public int? Id_Documento1_CDD { get; set; }
		public int? Id_Documento2_CDD { get; set; }
		public string USR_MODIF { get; set; }
    }
   

}

 