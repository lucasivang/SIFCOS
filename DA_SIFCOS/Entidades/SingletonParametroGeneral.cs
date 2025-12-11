using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class SingletonParametroGeneral
    {
        /// <summary>
        /// P: PRODUCCIÓN , D: DESARROLLO
        /// </summary>
        public string EsquemaActual{get; set;}
        
        /// <summary>
        /// ID_CONCEPTO de la TRS para Alta (inscripción)
        /// </summary>
        public string IdConceptoTasaAlta{get; set;}

        /// <summary>
        /// ID_CONCEPTO de la TRS para Reempadronamiento
        /// </summary>
        public string IdConceptoTasaReempadronamiento { get; set; }
        

        /// <summary>
        /// FECHA_DESDE: campo primario de los conceptos en tasas_servicio.vt_conceptos...
        /// Corresponde a la fecha desde para el concepto realzcionado al ALTA (INSCRIPCION)
        /// </summary>
        public string FecDesdeConceptoAlta { get; set; }

        /// <summary>
        /// FECHA_DESDE: campo primario de los conceptos en tasas_servicio.vt_conceptos...
        /// Corresponde a la fecha desde para el concepto realzcionado al REEMPADRONAMIENTO
        /// </summary>
        public string FecDesdeConceptoReempadronamiento { get; set; }

        /// <summary>
        /// Autor: IB. URL de generación de trs modalidad 2019 al cual debe agregarse /[o_hash_trx]/[o_id_transaccion]
        /// </summary>
        public string UrlGeneracionTrs { get; set; }

        /// <summary>
        /// Autor: IB. Monto de las tasas de alta
        /// </summary>
        public string MontoTasaAlta { get; set; }

        /// <summary>
        /// Autor: IB. Monto de las tasas de reempa
        /// </summary>
        public string MontoTasaReempadronamiento { get; set; }
        
        //propiedad estática donde se guarda la única instancia de la clase ...
        private static SingletonParametroGeneral instance;

        //Constructor por defecto privado para que no se pueda instanciar a menos que se use el metodo de clase "GetInstance" ...
        private SingletonParametroGeneral() { }
        
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="esquemaActual"> P: PRODUCCIÓN , D: DESARROLLO</param>
        /// <param name="idConceptoTasaAlta">ID_CONCEPTO de la TRS para Alta (inscripción)</param>
        /// <param name="idConceptoTasaReempadronamiento">ID_CONCEPTO de la TRS para Reempadronamiento</param>
        /// <param name="fecDesdeConceptoAlta">FECHA_DESDE: campo primario de los conceptos en tasas_servicio.vt_conceptos...
        /// Corresponde a la fecha desde para el concepto realzcionado al ALTA (INSCRIPCION)</param>
        /// <param name="fecDesdeConceptoReempadronamiento">FECHA_DESDE: campo primario de los conceptos en tasas_servicio.vt_conceptos...
        /// Corresponde a la fecha desde para el concepto realzcionado al REEMPADRONAMIENTO</param>
        private SingletonParametroGeneral(string esquemaActual, string idConceptoTasaAlta, string idConceptoTasaReempadronamiento, string fecDesdeConceptoAlta, string fecDesdeConceptoReempadronamiento)
        {
            this.EsquemaActual = esquemaActual;
            this.IdConceptoTasaAlta = idConceptoTasaAlta;
            this.IdConceptoTasaReempadronamiento = idConceptoTasaReempadronamiento;
            this.FecDesdeConceptoAlta = fecDesdeConceptoAlta;
            this.FecDesdeConceptoReempadronamiento = fecDesdeConceptoReempadronamiento;
        }
        //Sobrecarga de constructor para version con datos de trs 2019 en adelante
        private SingletonParametroGeneral(string esquemaActual, string idConceptoTasaAlta, string idConceptoTasaReempadronamiento, string fecDesdeConceptoAlta, string fecDesdeConceptoReempadronamiento, string urlGeneracionTrs, string montoTasaAlta, string montoTasaReempadronamiento)
        {
            this.EsquemaActual = esquemaActual;
            this.IdConceptoTasaAlta = idConceptoTasaAlta;
            this.IdConceptoTasaReempadronamiento = idConceptoTasaReempadronamiento;
            this.FecDesdeConceptoAlta = fecDesdeConceptoAlta;
            this.FecDesdeConceptoReempadronamiento = fecDesdeConceptoReempadronamiento;
            this.UrlGeneracionTrs = urlGeneracionTrs;
            this.MontoTasaAlta = montoTasaAlta;
            this.MontoTasaReempadronamiento = montoTasaReempadronamiento;

        }

        //Metodo estático que devuelve una unica instancia de "Singleton" ...
        public static SingletonParametroGeneral GetInstance()
        {
            if (instance == null) 
            {
                instance = new SingletonParametroGeneral();
            }
            return instance;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="esquemaActual"> P: PRODUCCIÓN , D: DESARROLLO</param>
        /// <param name="idConceptoTasaAlta">ID_CONCEPTO de la TRS para Alta (inscripción)</param>
        /// <param name="idConceptoTasaReempadronamiento">ID_CONCEPTO de la TRS para Reempadronamiento</param>
        /// <param name="fecDesdeConceptoAlta">FECHA_DESDE: campo primario de los conceptos en tasas_servicio.vt_conceptos...
        /// Corresponde a la fecha desde para el concepto realzcionado al ALTA (INSCRIPCION)</param>
        /// <param name="fecDesdeConceptoReempadronamiento">FECHA_DESDE: campo primario de los conceptos en tasas_servicio.vt_conceptos...
        /// Corresponde a la fecha desde para el concepto realzcionado al REEMPADRONAMIENTO</param>
        public static SingletonParametroGeneral GetInstance(string esquemaActual, string idConceptoTasaAlta, string idConceptoTasaReempadronamiento, string fecDesdeConceptoAlta, string fecDesdeConceptoReempadronamiento)
        { 
            //Metodo estático "sobrecargado" que devuelve una única instancia de "Singleton" ...

            if (instance == null)
            {
                instance = new SingletonParametroGeneral( esquemaActual,  idConceptoTasaAlta,  idConceptoTasaReempadronamiento,  fecDesdeConceptoAlta,  fecDesdeConceptoReempadronamiento);
            }
            return instance;
        }
        //Sobrecarga para version con datos de trs 2019 en adelante
        public static SingletonParametroGeneral GetInstance(string esquemaActual, string idConceptoTasaAlta, string idConceptoTasaReempadronamiento, string fecDesdeConceptoAlta, string fecDesdeConceptoReempadronamiento, string urlGeneracionTrs, string montoTasaAlta, string montoTasaReempadronamiento)
        {
            //Metodo estático "sobrecargado" que devuelve una única instancia de "Singleton" ...

            if (instance == null)
            {
                instance = new SingletonParametroGeneral(esquemaActual, idConceptoTasaAlta, idConceptoTasaReempadronamiento, fecDesdeConceptoAlta, fecDesdeConceptoReempadronamiento, urlGeneracionTrs, montoTasaAlta, montoTasaReempadronamiento);
            }
            return instance;
        }
    }
}
