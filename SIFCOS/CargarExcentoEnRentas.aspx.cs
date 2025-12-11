using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Configuration;
 
using BL_SIFCOS;

namespace SIFCOS
{
    public partial class CargarExcentoEnRentas : System.Web.UI.Page
    {
        public ReglaDeNegocios Bl = new ReglaDeNegocios();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                divMensajeExito.Visible = false;
                divMensajeError.Visible = false;
            }
        }

        protected void btnVolver_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Inscripcion.aspx");
        }

        
        

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {   
            divMensajeExito.Visible = false;
            divMensajeError.Visible = false;

            var res = Bl.RegistrarEntidadPerJur(txtCuit.Text.Trim(), txtRazonSocial.Text, txtNombreFantasia.Text);
            if (res == "OK")
            {
                lblMensajeExito.Text = "Se Guardó la entidad en Persona Juridica de Gobierno con éxito.";
            divMensajeExito.Visible = true;
                return;
            }

            lblMensajeError.Text = res;
            divMensajeError.Visible = true;
            
        }
    }

 
}