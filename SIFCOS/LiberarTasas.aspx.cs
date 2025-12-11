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
    public partial class LiberarTasas : System.Web.UI.Page
    {
        public ReglaDeNegocios Bl = new ReglaDeNegocios();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                 
            }
        }

        protected void btnVolver_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Inscripcion.aspx");
        }

        
        protected void btnLiberarTasa_OnClick(object sender, EventArgs e)
        {
            txtResultado.Text = Bl.LiberarTasa(txtNroReferencia.Text.Trim());
        }

        protected void btnConsultarTasa_OnClick(object sender, EventArgs e)
        {
            txtResultado.Text = "";
            var dtTasa = Bl.BlGetTasaByNroReferencia(txtNroReferencia.Text.Trim());
            if (dtTasa.Rows.Count == 0)
            {
                txtResultado.Text = "No exite la tasa para ese nro de referencia.";
                return;
            }

            txtResultado.Text = "";
            //tasas del sifcos viejo
            var dtTasaSifcosViejo = Bl.BlGetTasaSifcoViejoByNroReferencia(txtNroReferencia.Text.Trim());
            if (dtTasaSifcosViejo.Rows.Count == 0)
            {
                txtResultado.Text = "Ése nro de referencia no es del sifcos viejo.";
            }
            else
            {
                var id_tram = dtTasaSifcosViejo.Rows[0]["ID_TRAMITE"].ToString();
                if (string.IsNullOrEmpty(id_tram))
                {
                    txtResultado.Text =
                        "La tasa existe en el sifcos viejo. Debe liberar la tasa. No se muestra el tramite asociado.";
                }
                else
                {
                    txtResultado.Text = "La tasa está en el sifcos viejo, asociado al trámite : " + id_tram  + "  .";
                }
            }


            //tasa del sifcos nuevo
            var dtTasaSifcosNuevo = Bl.BlGetTasaSifcoNuevoByNroReferencia(txtNroReferencia.Text.Trim());
            if (dtTasaSifcosNuevo.Rows.Count == 0)
            {
                txtResultado.Text = txtResultado.Text +  "Ése nro de referencia no es del sifcos Nuevo.";
            }
            else
            {
                var id_tram = dtTasaSifcosNuevo.Rows[0]["NRO_TRAMITE_SIFCOS"].ToString();
                if (string.IsNullOrEmpty(id_tram))
                {
                    txtResultado.Text = txtResultado.Text + 
                        "La tasa existe en el sifcos Nuevo. Debe liberar la tasa. No se muestra el tramite asociado.";
                }
                else
                {
                    txtResultado.Text = txtResultado.Text +  "La tasa está en el sifcos Nuevo, asociado al trámite : " + id_tram + "  .";
                }
            }
            

             

            gvResultado.DataSource = dtTasa;
            gvResultado.DataBind();
        }

        protected void btnConsultarTasaPorCuit_OnClick(object sender, EventArgs e)
        {
            var dtTasa = Bl.BlGetTasaByCUIT(txtCuit.Text.Trim());
            if (dtTasa.Rows.Count == 0)
            {
                txtResultado.Text = "No exite la tasa para ese CUIT.";
                return;
            }


            txtResultado.Text = "";
            gvResultado.DataSource = dtTasa;
            gvResultado.DataBind();
        }

        protected void btnConsultarTasaNS_OnClick(object sender, EventArgs e)
        {
            txtResultado.Text = "";
            var dtTasa = Bl.BlGetTasaByNroReferencia(txtNroReferenciaNS.Text.Trim());
            if (dtTasa.Rows.Count == 0)
            {
                txtResultado.Text = "No exite la tasa para ese nro de referencia.";
                return;
            }

            txtResultado.Text = "";
            //tasas del sifcos viejo
            var dtTasaSifcosViejo = Bl.BlGetTasaSifcoViejoByNroReferencia(txtNroReferenciaNS.Text.Trim());
            if (dtTasaSifcosViejo.Rows.Count == 0)
            {
                txtResultado.Text = "Ése nro de referencia no es del sifcos viejo.";
            }
            else
            {
                var id_tram = dtTasaSifcosViejo.Rows[0]["ID_TRAMITE"].ToString();
                if (string.IsNullOrEmpty(id_tram))
                {
                    txtResultado.Text =
                        "La tasa existe en el sifcos viejo. Debe liberar la tasa. No se muestra el tramite asociado.";
                }
                else
                {
                    txtResultado.Text = "La tasa está en el sifcos viejo, asociado al trámite : " + id_tram + "  .";
                }
            }


            //tasa del sifcos nuevo
            var dtTasaSifcosNuevo = Bl.BlGetTasaSifcoNuevoByNroReferencia(txtNroReferenciaNS.Text.Trim());
            if (dtTasaSifcosNuevo.Rows.Count == 0)
            {
                txtResultado.Text = txtResultado.Text + "Ése nro de referencia no es del sifcos Nuevo.";
            }
            else
            {
                var id_tram = dtTasaSifcosNuevo.Rows[0]["NRO_TRAMITE_SIFCOS"].ToString();
                if (string.IsNullOrEmpty(id_tram))
                {
                    txtResultado.Text = txtResultado.Text +
                        "La tasa existe en el sifcos Nuevo. Debe liberar la tasa. No se muestra el tramite asociado.";
                }
                else
                {
                    txtResultado.Text = txtResultado.Text + "La tasa está en el sifcos Nuevo, asociado al trámite : " + id_tram + "  .";
                }
            }




            gvResultado.DataSource = dtTasa;
            gvResultado.DataBind();
        }

        protected void btnLiberarTasaNS_OnClick(object sender, EventArgs e)
        {
            var dtTasaSifcosNuevo = Bl.BlGetTasaSifcoNuevoByNroReferencia(txtNroReferenciaNS.Text.Trim());
            if (dtTasaSifcosNuevo.Rows.Count != 0)
            {
                var id_tram = dtTasaSifcosNuevo.Rows[0]["NRO_TRAMITE_SIFCOS"].ToString();
                var id_transaccion = dtTasaSifcosNuevo.Rows[0]["NRO_TRANSACCION"].ToString();
                txtResultado.Text = Bl.LiberarTasaNS(id_tram,id_transaccion);
            }

        }
    }

 
}