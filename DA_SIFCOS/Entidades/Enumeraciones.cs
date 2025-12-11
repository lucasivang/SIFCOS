using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_SIFCOS.Entidades
{
    public enum EstadoAbmcEnum
    {
        CONSULTANDO = 1,
        REGISTRANDO,
        EDITANDO,
        ELIMINANDO,
        VIENDO
    }

    public enum NumeroPasoEnum
    {
        PrimerPaso = 1,
        SegundoPaso,
        TercerPaso,
        CuartoPaso,
        QuintoPaso,
        SextoPaso,
        SeptimoPaso,
        OctavoPaso,
        PasoIniciarTramite
    }

    public enum PestaniaActivaEnum
    {
        DomicilioDelEstablecimiento = 1,
        ContactoDelEstablecimiento,
        DomicilioLegal,
        InformacionGeneral,
        CantidadPersonal_PromedioAnual,
        InformacionAdicional,
        ProductosActPrimariaSecundaria
    }

    public enum TipoArchivoEnum
    {
        Pdf = 1,
        Excel
    }

    public enum TipoMensajeMostrar
    {
        Mensaje_de_Error = 1,
        Mensaje_de_Exito,
        Mensaje_de_Alerta
    }

    public enum ConsultaInscripcion
    {
        NoExiste = 1,
        Existe

    }
}
