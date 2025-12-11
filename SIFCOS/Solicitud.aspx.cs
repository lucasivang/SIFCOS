using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Diagnostics;
using System.Web;
using BL_SIFCOS;
using DA_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Models;



namespace SIFCOS
{
    public partial class Solicitud : System.Web.UI.Page
    {
        public Principal master;
        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public static DataAccessMethods PDa = new DataAccessMethods();

        public string cuit;
        public Usuario UsuarioLogueado = new Usuario();
        public Representado r = new Representado();

        protected void Page_Load(object sender, EventArgs e)
        {

            master = (Principal)Page.Master;


            if (!IsPostBack)
            {

                divInstrucciones1.Visible = false;
                divAprobado.Visible = false;
                divInstructivo.Visible = true;
                errorLabel.Visible = false;
                PrimerDiv.Visible = false;
                divNivel2.Visible = false;
                error.Visible = false;

                divMensajeError.Visible = false;
                divMensajeExito.Visible = false;

            }
            else
            {
                errorLabel.Visible = false;
                PrimerDiv.Visible = true;
                divNivel2.Visible = false;

                error.Visible = false;

                if (master != null) UsuarioLogueado = master.UsuarioCidiLogueado;
            }


            Debug.WriteLine(UsuarioLogueado.ToString());

        }
        public void IniciarSolicitud(object sender, EventArgs e)
        {
            var lstRolesAutorizados = new List<string>();
            List<Roles> Autorizados = new List<Roles>();
            lstRolesAutorizados.Add("Administrador General");

            if (lstRolesAutorizados.Contains(master.RolUsuario))
            {

                // Response.Redirect("Inscripcion.aspx?test=1");
            }

            // Tratamiento especial para Contadores que aun no pueden tener representacion legal de las empresas
            var ListaRoles = Bl.BlGetRelacionesUsuario(UsuarioLogueado.CUIL);

            if (Session["test"] == "1")
            {
                Response.Redirect("Inscripcion.aspx?test=1");
            }

            foreach (var Relacion in ListaRoles)
            {

                if (Relacion.Rol == "10" || Relacion.Rol == "11" || Relacion.Rol == "12" || Relacion.Rol == "13" || Relacion.Rol == "14"
                    || Relacion.Rol == "15" || Relacion.Rol == "16" || Relacion.Rol == "17" || Relacion.Rol == "18" || Relacion.Rol == "19")
                {
                    Autorizados.Add(new Roles
                    {
                        Rol = Relacion.Rol,
                        Permiso = Relacion.Permiso
                    });
                }

                if (Relacion.Permiso == "VARIOS")
                {
                    Session["test"] = "1";
                    Response.Redirect("Inscripcion.aspx?test=1");
                }
            }

            Session["CuitAutorizados"] = Autorizados;

            if (UsuarioLogueado.TieneRepresentados == "S")
            {
                //Si el objeto Representado no posee valores, significa que no se ha seleccionado ninguno.
                if (!String.IsNullOrEmpty(UsuarioLogueado.Representado.RdoCuilCuit))
                {
                    if (UsuarioLogueado.Id_Estado >= 2 && UsuarioLogueado.Id_Estado != null && UsuarioLogueado.Estado.Equals("Verificado"))
                    {
                        Session["RdoCuilCuit"] = UsuarioLogueado.Representado.RdoCuilCuit;
                        Session["TieneRepresentados"] = "S";
                        divInstructivo.Visible = false;
                        //PrimerDiv.Visible = true;
                        Response.Redirect("Inscripcion.aspx");

                    }
                    else // el usuario no es cidi nivel 2 o no esta verificado
                    {
                        PrimerDiv.Visible = false;
                        Debug.WriteLine("No posee cidi niv 2 / verificado");

                        divNivel2.Visible = true;
                        error.Visible = true;
                        errorLabel.Visible = true;
                        errorLabel.InnerText = "Usted no posee CiDi Nivel 2";


                    }

                }
                else
                {
                    Debug.WriteLine("el usuario tiene representados pero no se selecciono ninguno");

                    //Se redirecciona a la URL de CiDi pasando por QueryString el valor de la sesión del usuario.
                    //Una vez seleccionado el representado, el portal redirecciona a la URL principal del sitio, informada cuando se dio de alta el mismo.
                    HttpContext.Current.Response.Redirect(
                        Config.CiDiUrlRelacion + "?s=" + UsuarioLogueado.Respuesta.SesionHash);
                }

            }
            else if (UsuarioLogueado.TieneRepresentados.Equals("N"))
            {
                Session["TieneRepresentados"] = "N";
                Debug.WriteLine("el usuario logueado no posee representados");
                //PrimerDiv.Visible = false;

                //error.Visible = true;
                //errorLabel.Visible = true;

                //divInstructivo.Visible = false;
                //errorLabel.InnerText =
                //    "Usted no posee Representados o no está registrado como Representante Legal de una Empresa";

                Session["RdoCuilCuit"] = UsuarioLogueado.CUIL;
                divInstructivo.Visible = false;
                Response.Redirect("Inscripcion.aspx");
            }

        }
        public void IniciarTramite(object sender, EventArgs e)
        {
            cuit = txtCuit.Text;



            bool verificar = VerificarUsuario(UsuarioLogueado);
            if (verificar)
            {
                Debug.WriteLine("paso todas las verificaciones");
                divAprobado.Visible = true;
                divMensajeError.Visible = false;
                PrimerDiv.Visible = false;

            }

        }
        public bool VerificarUsuario(Usuario u)
        {
            if (u.Representado.RdoCuilCuit.Equals(cuit))
            {


            }
            else // si el cuit ingresado no es igual al cuit de la empresa representada
            {


                Debug.WriteLine("el cuit ingresado no es igual al cuit del representado");
                PrimerDiv.Visible = false;

                error.Visible = true;
                errorLabel.Visible = true;


                errorLabel.InnerText =
                    "El cuit que se ingresó no corresponde con el CUIT de la empresa que usted representa ";
            }
            return false;
        }
    }
}