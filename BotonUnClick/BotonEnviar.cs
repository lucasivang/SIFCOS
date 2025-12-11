using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BotonUnClick
{
    [ToolboxData("<{0}:BotonEnviar runat=server></{0}:BotonEnviar>")]
    public class BotonEnviar : Button
    {
        private string _TextoEnviando = "Enviando...";
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("Enviando...")]
        [Description("Texto a mostrar cuando se pulse el botón.")]
        public string TextoEnviando
        {
            get
            {
                return _TextoEnviando;
            }

            set
            {
                _TextoEnviando = value;
            }
        }

        public BotonEnviar()
        {
            base.Text = "Enviar";
            base.CausesValidation = false;
        }

        protected override void OnPreRender(EventArgs e)
        {
            //Registramos la función de envío del botón
            Page.RegisterClientScriptBlock("fEnviar_" + ID, FunciónEnviarBotón());
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter output)
        {
            //Creamos el panel donde va el botón principal
            output.Write("<div id='div1_" + ID + "' style='display: inline'>");

            output.AddAttribute("onclick", "Enviar_" + ID + "('" + ID + "');");
            base.Render(output);

            output.Write("</div>");

            var estiloBoton = "class='btn btnBuscar  btn-circle' style = \"margin-bottom: 10px;margin-top: 10px;\" ";
            //Creamos el panel y el botón secundario de envío
            output.Write("<div id='div2_" + ID + "' style='display: none'>");
            output.Write("<input disabled type=submit value='" + TextoEnviando + "' " + estiloBoton + " />");
            output.Write("</div>");

        }

        private string FunciónEnviarBotón()
        {
            string txt = "<script language='javascript'>";

            txt += @"function Enviar_" + ID + @"(id) {
						if (typeof(Page_ClientValidate) == 'function') {
							if (Page_ClientValidate() == true ) { 
								document.getElementById('div1_' + id).style.display = 'none';
								document.getElementById('div2_' + id).style.display = 
							'inline';							return true;
							}
							else {
								return false;
							}
						}
						else {	
							document.getElementById('div1_' + id).style.display = 'none';
							document.getElementById('div2_' + id).style.display = 'inline';
							return true;
						}

					}</script>;";

            return txt;
        }
    }
}
