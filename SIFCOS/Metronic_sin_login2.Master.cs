using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIFCOS
{
    public partial class Metronic_sin_login2 : System.Web.UI.MasterPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
           

        }


        protected void Page_Load(object sender, EventArgs e)
        {

        
        }

        public string EntornoDeEjecucion
        {
            get
            {
                return ConfigurationManager.AppSettings["Entorno"].ToString();
            }
        }

        public string VersionProducto
        {
            get
            {
                return ConfigurationManager.AppSettings["VersionProducto"].ToString();
            }
        }
        
       
        
       

       
        
    }
}