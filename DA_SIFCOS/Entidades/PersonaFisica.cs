using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class PersonaFisica
    {
        public int? IdAplicacion { get; set; }
        public int? IdNumero { get; set; }
        public string IdSexo { get; set; }
        public string NroDocumento { get; set; }
        public string PaiCodPais { get; set; }
        public string PaiCodPaisOrigen { get; set; }
        public string PaiCodPaisNacionalidad { get; set; }
        public string IdTipoDocumento { get; set; }
        public string OrganismoEmisorDoc { get; set; }
        public string PaisTipoDoc { get; set; }
        public int? Localidad { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public int? Clase { get; set; }
        public string Observaciones { get; set; }
        public DateTime? FecNacimiento { get; set; }
        public string IdGrupoFamiliar { get; set; }
        public string IdEstadoCivil { get; set; }
        public string Vinculo { get; set; }
        public DateTime? FechaDefuncion { get; set; }
        public DateTime? HoraDefuncion { get; set; }
        public string NroOtroDocumento { get; set; }
        public string IdTipoOtroDocumento { get; set; }
    }
}
