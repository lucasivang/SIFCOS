using System;
using System.Collections.Generic;
using System.Web.UI;
using DA_SIFCOS.Entidades;

namespace SIFCOS
{
    public partial class ListaTrsReempa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RefrescarGrilla();
            }
        }

        public void RefrescarGrilla()
        {
            var lista = (List<Trs>) Session["ListaTrs"];
            gvResultado.DataSource = lista;
            gvResultado.DataBind();
            Session["ImprimirTasa"] = 1;
        }

         

        protected void btnVolver_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Inscripcion.aspx");
        }
    }

 
}