using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_SIFCOS
{
    public abstract class daBase
    {
        protected static String CadenaDeConexionIndustria()//INDUSTRIA
        {
            //cadena en DESARROLLO
            //return ConfigurationManager.ConnectionStrings["ConnectionString.INDUSTRIA_CBA1D"].ConnectionString;
            //cadena en PRODUCCION
            return ConfigurationManager.ConnectionStrings["ConnectionString.INDUSTRIA_CBA1"].ConnectionString;
        }

        protected static String CadenaDeConexionRuami()//RUAMI
        {
            //cadena en DESARROLLO
            return ConfigurationManager.ConnectionStrings["ConnectionString.RUAMI_CBA1D"].ConnectionString;

            //cadena en PRODUCCION
            //return ConfigurationManager.ConnectionStrings["ConnectionString.RUAMI_CBA1"].ConnectionString;
        }

        protected static String CadenaDeConexionSifcos()//SIFCOS
        {
            //return ConfigurationManager.ConnectionStrings["ConnectionString.SIFCOS_CBA1D"].ConnectionString;
            //cadena en PRODUCCION
            return ConfigurationManager.ConnectionStrings["ConnectionString.SIFCOS_CBA1"].ConnectionString;
        }
        protected static String CadenaDeConexionPersonal1()//PERSONAL
        {
            //cadena en PRODUCCION
            return ConfigurationManager.ConnectionStrings["ConnectionString.PERSONAL1_CBA1"].ConnectionString;
        }
        protected static String CadenaDeConexionPersonal2()//PERSONAL
        {
            //cadena en PRODUCCION
            return ConfigurationManager.ConnectionStrings["ConnectionString.PERSONAL2_CBA1"].ConnectionString;
        }
        protected static String CadenaDeConexionPromInd()//PROMIND
        {
            //return ConfigurationManager.ConnectionStrings["ConnectionString.RIP_CBA1_DESA"].ConnectionString;
            //cadena en PRODUCCION
            return ConfigurationManager.ConnectionStrings["ConnectionString.PROMIND_CBA1"].ConnectionString;
        }
        protected static String CadenaDeConexionAlimentos()//ALIMENTOS
        {
            //return ConfigurationManager.ConnectionStrings["ConnectionString.ALIMENTOS_CBA1D"].ConnectionString;
            //cadena en PRODUCCION
            return ConfigurationManager.ConnectionStrings["ConnectionString.ALIMENTOS_CBA1"].ConnectionString;
        }
        protected static String CadenaDeConexionCaldera()//CALDERA
        {
            //return ConfigurationManager.ConnectionStrings["ConnectionString.CALDERAS_CBA1D"].ConnectionString;
            //cadena en PRODUCCION
            return ConfigurationManager.ConnectionStrings["ConnectionString.CALDERAS_CBA1"].ConnectionString;
        }

        protected static String CadenaDeConexionControlMicym()//PANELCONTROL
        {
            //cadena en PRODUCCION
            return ConfigurationManager.ConnectionStrings["ConnectionString.CONTROL_MICYM_CBA1"].ConnectionString;
        }
    }
}
