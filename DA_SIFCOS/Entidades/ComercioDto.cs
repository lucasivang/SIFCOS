using System;


namespace DA_SIFCOS.Entidades
{
    public class ComercioDto
    {
		public string NRO_SIFCOS { get; set; }
		public Int32 ID_ENTIDAD { get; set; }
		public Int32 idVin { get; set; }
		public string CUIT { get; set; }
		public string RAZON_SOCIAL { get; set; }
		public string DEBE { get; set; }
		public string ID_DEPARTAMENTO { get; set; }
		public string DEPARTAMENTO { get; set; }
		public string ID_LOCALIDAD { get; set; }
		public string LOCALIDAD { get; set; }
		public string BARRIO { get; set; }
		public string CALLE { get; set; }
		public string ALTURA { get; set; }
		public string CP { get; set; }
		public string DEPTO { get; set; }
		public string TORRE { get; set; }
		public string PISO { get; set; }
		public string DOMICILIO { get; set; }

		public string FEC_VTO { get; set; }

		public string LATITUD { get; set; }
		public string LONGITUD { get; set; }
		public string RUBRO_PRODUCTO { get; set; }
		public string ID_PRODUCTO { get; set; }
    }
}
