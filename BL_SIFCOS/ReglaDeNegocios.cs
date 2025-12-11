using System;
using DA_SIFCOS.Models;
using System.Data;
using DA_SIFCOS;
using System.Collections.Generic;
using System.Linq;
using AppComunicacion.ApiModels;
using DA_SIFCOS.Entidades;


namespace BL_SIFCOS
{
    public class ReglaDeNegocios
    {
        public DataAccessMethods p_da;

        public ReglaDeNegocios() : base()
        {
            p_da = new DataAccessMethods();
        }

        public DataTable BlGetEmpresa(String pCuit)
        {
            try
            {
                DataTable dt = p_da.DaGetEmpresa(pCuit);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetDomEmpresa(String pCuit)
        {
            try
            {
                DataTable dt = p_da.DaGetDomEmpresa(pCuit);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetDireccionesSedes(String pCuit, Int64 pNrosifcos)
        {
            try
            {
                DataTable dt = p_da.DAGetDireccionesSedes(pCuit, pNrosifcos);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetTramitesSifcos(String pCuit, Int64 pNrosifcos)
        {
            try
            {
                DataTable dt = p_da.DAGetTramitesSifcos(pCuit, pNrosifcos);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Consulta todas las comunicaciones (mail, cel, tel, web page, facebook, etc) por cuit y/o sede.
        /// </summary>
        /// <param name="pNroTramite">El nro de tRamite es el id_entidad de la comunicación  que se desea conocer</param>
        /// <returns></returns>
        public DataTable BlGetComEmpresa(String pNroTramite)
        {
            try
            {
                DataTable dt = p_da.DaGetComEmpresa(pNroTramite);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetPersonasRcivil(String pDNI, String pSEXO)
        {
            try
            {
                DataTable dt = p_da.DaGetPersonasRcivil(pDNI, pSEXO);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetPersonasRcivil2(String pDNI, String pSEXO)
        {
            try
            {
                DataTable dt = p_da.DaGetPersonasRcivil2(pDNI, pSEXO);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetPersonasRcivil3(String pNombre, String pApellido)
        {
            try
            {
                DataTable dt = p_da.DaGetPersonasRcivil3(pNombre, pApellido);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public string BlGenerarCUIL(String pCUIL, Int64 pDNI, String pSEXO, Int64 pID_NUMERO, String pPais)
        {
            try
            {
                string resultado = p_da.DaGenerarCUIL(pCUIL, pDNI, pSEXO, pID_NUMERO, pPais);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public string BlGenerarRazonSocial(String pCUIT, String pRazonSocial, String pnro_ing_bruto)
        {
            try
            {
                string resultado = p_da.DaGenerarRazonSocial(pCUIT, pRazonSocial, pnro_ing_bruto);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public string BlCambiarTitularTRS(String Nro_Liq, String pIdSEXO, String pCUIL, Int64 pDNI)
        {
            try
            {
                string resultado = p_da.DaCambiarTitularTRS(Nro_Liq, pIdSEXO, pCUIL, pDNI);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<Persona> BlGetPersonasRcivil_CUIL2(String pCUIL)
        {
            try
            {
                List<Persona> Resultado = new List<Persona>();
                DataTable dt = p_da.DaGetPersonasRcivil_CUIL(pCUIL);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Resultado.Add(new Persona
                            {
                                Nombre = row["Nombre"].ToString(),
                                Apellido = row["Apellido"].ToString(),
                                Sexo = row["Sexo"].ToString()
                            }
                        );
                    }
                }


                return Resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }


        public DataTable BlGetDeptartamentos(String id_provincia)
        {
            try
            {
                return p_da.DaGetDepartamentos(id_provincia);
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetLocalidades(String id_Departamento)
        {
            try
            {
                DataTable dt = p_da.DaGetLocalidades(id_Departamento);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }



        public DataTable BlGetSedes(String pCuit)
        {
            try
            {
                DataTable dt = p_da.DaGetSedes(pCuit);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetInfoBoca(String pCuilUser)
        {
            try
            {
                DataTable dt = p_da.DaGetInfoBoca(pCuilUser);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public Domicilio BlGetDomEmpresaByIdVin(string idVinculacion)
        {
            Domicilio Dom = new Domicilio();
            var dt = p_da.DaGetDomEmpresaByIdVin(idVinculacion);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ID_PROVINCIA"].ToString() != "")
                    {
                        Dom.Provincia = new Provincia()
                        {
                            IdProvincia = row["ID_PROVINCIA"].ToString(),
                            Nombre = row["N_PROVINCIA"].ToString()
                        };
                    }
                    
                        
                    if (row["ID_DEPARTAMENTO"].ToString() != "")
                    {
                        Dom.Departamento = new Departamento()
                        {
                            IdDepartamento = int.Parse(row["ID_DEPARTAMENTO"].ToString()),
                            Nombre = row["N_DEPARTAMENTO"].ToString()
                        };
                    }

                    
                    if (row["ID_LOCALIDAD"].ToString() != "")
                    {
                        Dom.Localidad = new Localidad()
                        {
                            IdLocalidad = int.Parse(row["ID_LOCALIDAD"].ToString()),
                            Nombre = row["N_LOCALIDAD"].ToString()
                        };
                    }

                    
                    if (row["ID_BARRIO"].ToString() != "")
                    {
                        Dom.Barrio = new Barrio()
                        {
                            IdBarrio = int.Parse(row["ID_BARRIO"].ToString()),
                            Nombre = row["N_BARRIO"].ToString()
                        };
                    }

                    if (row["ID_CALLE"].ToString() != "")
                    {
                        Dom.Calle = new Calle()
                        {
                            IdCalle = int.Parse(row["ID_CALLE"].ToString()),
                            Nombre = row["N_CALLE"].ToString()
                        };
                    }

                    if (row["N_CALLE"].ToString() != "" && row["ID_CALLE"].ToString() == "")
                    {
                        Dom.Calle = new Calle()
                        {
                            IdCalle = 1,
                            Nombre = row["N_CALLE"].ToString()
                        };
                    }


                    Dom.Altura = row["ALTURA"].ToString();
                    Dom.CodigoPostal = row["CPA"].ToString();
                    Dom.Dpto = row["DEPTO"].ToString();
                    Dom.Lote = row["LOTE"].ToString();
                    Dom.Manzana = row["MZNA"].ToString();
                    Dom.Piso = row["PISO"].ToString();
                    Dom.Torre = row["TORRE"].ToString();
                    Dom.IdVin = int.Parse(row["ID_VIN"].ToString());
                    
                }
                
            }

            return Dom;
        }

        /// <summary>
        /// Devuelve un listado de productos asociados a un tramite.
        /// </summary>
        /// <param name="nroTramiteSifcos"></param>
        /// <returns></returns>
        public DataTable BlGetProductosTramite(string nroTramiteSifcos)
        {

            return p_da.DaGetProductosTramite(nroTramiteSifcos);
        }

        //public List<string> BlGetRubros(List<string> n_productos)
        //{
        //    try
        //    {   DataTable dt = new DataTable();
        //        for (int i = 0; i < n_productos.Count; i++)
        //        {
        //            dt = p_da.DaGetRubros(n_productos[i]);

        //        }

        //        List<string> Rubros = new List<string>();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            Rubros.Add(dt.Rows[i]["N_RUBRO"].ToString());
        //        }
        //        return Rubros;
        //    } 
        //    catch (BlException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new BlException(ex.Message);
        //    }
        //}



        /// <summary>
        /// Devuelve una tabla con las actividades relacionadas a la lista de actividades enviadas por parámetro
        /// </summary>
        /// <param name="idsProductos"></param>
        /// <returns></returns>
        public DataTable BlGetActividadesProducto(List<int> idsProductos)
        {
            try
            {
                return p_da.BlGetActividadesProducto(idsProductos);
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetProductosPorAct(String Producto)
        {
            try
            {
                return p_da.DaGetProductosPorAct(Producto);
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetProductosPorAct2(String Actividad)
        {
            try
            {
                return p_da.DaGetProductosPorAct2(Actividad);
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }



        public DataTable BlGetEstados_br()
        {
            try
            {

                return p_da.DaGetEstadosbr();
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }





        public DataTable BlGetSuperficeByNroTramite(Int64 NroTramite)
        {
            try
            {

                return p_da.DaGetSuperficeByNroTramite(NroTramite);
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetRoles()
        {
            try
            {

                return p_da.DaGetRoles();
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetOrganismosByIdOrganismos(String idOrganismos)
        {
            try
            {

                return p_da.DaGetOrganismosByIdOrganismos(idOrganismos);
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetOrganismosOne(String idOrganismo)
        {
            try
            {

                return p_da.DaGetOrganismosOne(idOrganismo);
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<Superficie> BlGetSuperficies(string pPrefijo)
        {
            try
            {
                DataTable dt = p_da.DaGetSuperficies(pPrefijo.ToUpper());
                List<Superficie> ListSup = new List<Superficie>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListSup.Add(new Superficie()
                    {
                        IdTipoSuperficie = string.IsNullOrEmpty(dt.Rows[i]["id_tipo_superficie"].ToString())
                            ? new int?()
                            : int.Parse(dt.Rows[i]["id_tipo_superficie"].ToString()),
                        NTipoSuperficie = dt.Rows[i]["n_tipo_superficie"].ToString()
                    });
                }

                return ListSup;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<Roles> BlGetListaRolesUsuarios(String rol, String cuil, String orden_consulta)
        {
            try
            {
                DataTable dt = p_da.DaGetListaRolesUsuarios(rol, cuil, orden_consulta);
                List<Roles> ListRoles = new List<Roles>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListRoles.Add(new Roles()
                    {
                        Id = Convert.ToInt64(dt.Rows[i]["id"].ToString()),
                        Cuil = dt.Rows[i]["id_usuario_cidi"].ToString(),
                        NomYApe = dt.Rows[i]["apeynom"].ToString(),
                        FecUltAcceso = dt.Rows[i]["Fec_Ult_Acceso"].ToString(),
                        Rol = dt.Rows[i]["Rol"].ToString()
                    });
                }

                return ListRoles;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<Organismos> BlGetListaOrganismosUsuarios(String Organismo, String cuil, String orden_consulta)
        {
            try
            {
                DataTable dt = p_da.DaGetListaOrganismosUsuarios(Organismo, cuil, orden_consulta);
                List<Organismos> ListORG = new List<Organismos>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListORG.Add(new Organismos()
                    {
                        Cuil = dt.Rows[i]["id_usuario_cidi"].ToString(),
                        NomYApe = dt.Rows[i]["apeynom"].ToString(),
                        FecUltAcceso = dt.Rows[i]["Fec_Ult_Acceso"].ToString(),
                        Organismo = dt.Rows[i]["Organismo"].ToString()
                    });
                }

                return ListORG;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }



        public List<Producto> BlGetProductos(string pPrefijo)
        {
            try
            {
                DataTable dt = p_da.DaGetProductos(pPrefijo.ToUpper());
                List<Producto> ListProd = new List<Producto>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListProd.Add(new Producto()
                    {
                        IdProducto = dt.Rows[i]["idproducto"].ToString(), NProducto = dt.Rows[i]["nproducto"].ToString()
                    });
                }

                return ListProd;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<Producto> BlGetProductosbeta(string pPrefijo)
        {
            try
            {
                DataTable dt = p_da.DaGetProductosBETA(pPrefijo.ToUpper());
                List<Producto> ListProd = new List<Producto>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListProd.Add(new Producto()
                    {
                        IdProducto = dt.Rows[i]["idproducto"].ToString(), NProducto = dt.Rows[i]["nproducto"].ToString()
                    });
                }

                ListProd.Add(new Producto() { IdProducto = "0", NProducto = "SIN ASIGNAR" });
                return ListProd;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<Organismo> BlGetOrganismosList(string pPrefijo)
        {
            try
            {
                DataTable dt = p_da.DaGetOrganismos(pPrefijo);
                List<Organismo> ListOrg = new List<Organismo>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListOrg.Add(new Organismo()
                    {
                        IdOrganismo = dt.Rows[i]["id_organismo"].ToString(),
                        NOrganismo = dt.Rows[i]["n_organismo"].ToString()
                    });
                }

                ListOrg.Add(new Organismo() { IdOrganismo = "0", NOrganismo = "SIN ASIGNAR" });
                return ListOrg;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetOrganismos()
        {
            try
            {
                DataTable dtOrganismos = p_da.DaGetOrganismos();
                return dtOrganismos;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }

        }



        public List<consultaTramite> BlGetTramites(String pCuit, Int64 NroTramite, String CuilUsuarioCidi)
        {
            try
            {
                string Nrotramite = NroTramite.ToString();
                List<consultaTramite> ListConsultaTramite = new List<consultaTramite>();
                DataTable dt = p_da.DaGetTramites(pCuit, NroTramite, CuilUsuarioCidi);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListConsultaTramite.Add(new consultaTramite()
                        {
                            //idEntidad = dt.Rows[i]["id_entidad"].ToString(), 
                            //id_sede_entidad = dt.Rows[i]["id_sede"].ToString(), 
                            CUIT = dt.Rows[i]["CUIT"].ToString(),
                            Razon_Social = dt.Rows[i]["RAZON_SOCIAL"].ToString(),
                            Nro_Sifcos = dt.Rows[i]["NRO_SIFCOS"].ToString(),
                            //latitud = dt.Rows[i]["latitud_ubi"].ToString(),
                            //longitud = dt.Rows[i]["longitud_ubi"].ToString(),
                            Nro_tramite = dt.Rows[i]["NRO_TRAMITE"].ToString(),
                            inicio_actividad = dt.Rows[i]["INICIO_ACTIVIDAD"].ToString(),
                            fec_alta = dt.Rows[i]["FEC_ALTA"].ToString(),
                            DomLocal = dt.Rows[i]["DOMICILIO"].ToString(),
                            Tipo_Tramite = dt.Rows[i]["TIPO_TRAMITE"].ToString(),
                            //Nombre_Fantasia = dt.Rows[i]["NOMBRE_FANTASIA"].ToString(), 
                            estado = dt.Rows[i]["ESTADO"].ToString(),
                            desc_estado = dt.Rows[i]["DESC_ESTADO"].ToString()
                        });
                    }

                    return ListConsultaTramite;

                }
                else
                {
                    ListConsultaTramite = BlGetTramitesBaja(Nrotramite, null, pCuit, null, 1, 0);
                    return ListConsultaTramite;
                }

            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<consultaTramite> BlGetTramitesBoca(String nroTramite, String nroSifcos, String cuit,
            String RazonSocial, Int64 OrdenConsulta, String FechaDesde, String FechaHasta, String IdEstado, String tipoTramite)
        {
            try
            {
                DataTable dt = p_da.DaGetTramitesBoca(nroTramite, nroSifcos, cuit, RazonSocial, OrdenConsulta,
                    FechaDesde, FechaHasta, IdEstado);
                List<consultaTramite> ListTramitesBoca = new List<consultaTramite>();
                string Estado = "";

                foreach (DataRow row in dt.Rows)
                {
                    if (tipoTramite != "0")
                    {
                        if (tipoTramite == row["Tipo_Tramite"].ToString())
                        {
                            ListTramitesBoca.Add(new consultaTramite()
                            {
                                Nro_tramite = row["NRO_TRAMITE"].ToString(),
                                Nro_Sifcos = row["NRO_SIFCOS"].ToString(),
                                Tipo_Tramite = row["Tipo_Tramite"].ToString(),
                                CUIT = row["CUIT"].ToString(),
                                Razon_Social = row["RAZON_SOCIAL"].ToString(),
                                DomLocal = row["DOMICILIO"].ToString(),
                                estado = row["ESTADO"].ToString(),
                                id_estado = row["ID_ESTADO"].ToString(),
                                Vto_Tramite = row["VTO_TRAMITE"].ToString()

                            });

                        }

                    }
                    else
                    {
                        ListTramitesBoca.Add(new consultaTramite()
                        {
                            Nro_tramite = row["NRO_TRAMITE"].ToString(),
                            Nro_Sifcos = row["NRO_SIFCOS"].ToString(),
                            Tipo_Tramite = row["Tipo_Tramite"].ToString(),
                            CUIT = row["CUIT"].ToString(),
                            Razon_Social = row["RAZON_SOCIAL"].ToString(),
                            DomLocal = row["DOMICILIO"].ToString(),
                            estado = row["ESTADO"].ToString(),
                            id_estado = row["ID_ESTADO"].ToString(),
                            Vto_Tramite = row["VTO_TRAMITE"].ToString()

                        });
                    }


                }

                return ListTramitesBoca;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<consultaTramite> BlGetTramitesBaja(String nroTramite, String nroSifcos, String cuit,
            String RazonSocial, Int64 OrdenConsulta, Int64 EstadoTramite)
        {
            try
            {
                DataTable dt = p_da.DaGetTramitesBaja(nroTramite, nroSifcos, cuit, RazonSocial, OrdenConsulta);
                List<consultaTramite> ListTramitesBoca = new List<consultaTramite>();
                string Estado = "";
                switch (EstadoTramite)
                {
                    case 1:
                        Estado = "CARGADO";
                        break;
                    case 2:
                        Estado = "VERIFICADO BOCA";
                        break;
                    case 3:
                        Estado = "RECHAZADO BOCA";
                        break;
                    case 4:
                        Estado = "VERIFICADO MINISTERIO";
                        break;
                    case 5:
                        Estado = "RECHAZADO MINISTERIO";
                        break;
                    case 6:
                        Estado = "AUTORIZADO POR MINISTERIO";
                        break;
                    case 7:
                        Estado = "RECHAZADO SIN TASA PAGA";
                        break;
                    case 10:
                        Estado = "ANULADO";
                        break;
                    case 11:
                        Estado = "RECHAZADO CON DEV DE TASA";
                        break;
                    case 12:
                        Estado = "REIMPRESION VERIFICADA";
                        break;
                    case 13:
                        Estado = "REIMPRESION AUTORIZADA";
                        break;
                    case 14:
                        Estado = "BAJA VERIFICADA";
                        break;
                    case 15:
                        Estado = "BAJA AUTORIZADA";
                        break;
                    default:
                        Estado = "SIN ASIGNAR";
                        break;
                }

                ListTramitesBoca.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    ListTramitesBoca.Add(new consultaTramite()
                    {
                        Nro_tramite = row["NRO_TRAMITE"].ToString(),
                        Nro_Sifcos = row["NRO_SIFCOS"].ToString(),
                        Tipo_Tramite = row["TIPO_TRAMITE"].ToString(),
                        fec_alta = row["FEC_ALTA"].ToString(),
                        inicio_actividad = row["INICIO_ACTIVIDAD"].ToString(),
                        DomLocal = row["DOMICILIO"].ToString(),
                        CUIT = row["CUIT"].ToString(),
                        Razon_Social = row["RAZON_SOCIAL"].ToString(),
                        estado = row["ESTADO"].ToString(),
                        desc_estado = row["DESC_ESTADO"].ToString(),
                        //id_estado = row["ID_ESTADO"].ToString(),
                        Vto_Tramite = row["VTO_TRAMITE"].ToString()

                    });


                }

                if (Estado != "SIN ASIGNAR")
                {
                    ListTramitesBoca.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["ESTADO"].ToString() == Estado)
                        {
                            ListTramitesBoca.Add(new consultaTramite()
                            {
                                Nro_tramite = row["NRO_TRAMITE"].ToString(),
                                Nro_Sifcos = row["NRO_SIFCOS"].ToString(),
                                Tipo_Tramite = row["Tipo_Tramite"].ToString(),
                                CUIT = row["CUIT"].ToString(),
                                Razon_Social = row["RAZON_SOCIAL"].ToString(),
                                estado = row["ESTADO"].ToString(),
                                Vto_Tramite = row["VTO_TRAMITE"].ToString()

                            });
                        }

                    }
                }

                return ListTramitesBoca;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<consultaTramite> BlGetTramitesBocaRecepcion(String nroTramite, String nroSifcos, String cuit,
            String RazonSocial, Int64 OrdenConsulta, String FechaDesde, String FechaHasta, String IdEstado,
            String IdOrganismo)
        {
            try
            {
                DataTable dt = p_da.DaGetTramitesBocaRecepcion(nroTramite, nroSifcos, cuit, RazonSocial, OrdenConsulta,
                    FechaDesde, FechaHasta, IdEstado, IdOrganismo);
                List<consultaTramite> ListTramitesBoca = new List<consultaTramite>();
                string Estado = "";

                foreach (DataRow row in dt.Rows)
                {
                    ListTramitesBoca.Add(new consultaTramite()
                    {
                        Nro_tramite = row["NRO_TRAMITE"].ToString(),
                        Nro_Sifcos = row["NRO_SIFCOS"].ToString(),
                        Tipo_Tramite = row["Tipo_Tramite"].ToString(),
                        CUIT = row["CUIT"].ToString(),
                        Razon_Social = row["RAZON_SOCIAL"].ToString(),
                        DomLocal = row["DOMICILIO"].ToString(),
                        estado = row["ESTADO"].ToString(),
                        id_estado = row["ID_ESTADO"].ToString(),
                        Vto_Tramite = row["VTO_TRAMITE"].ToString()
                    });


                }

                return ListTramitesBoca;

            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetHistEstados(Int64 NroTramite)
        {
            try
            {
                return p_da.DaGetHistEstados(NroTramite);

            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetInfoGralTramite(Int64 nroTramite)
        {
            try
            {
                DataTable dtListTramiteBoca = p_da.DaGetInfoGralTramite(nroTramite);
                return dtListTramiteBoca;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlGetDeudaTramite(Int64 nroTramite)
        {
            try
            {
                DataTable dtDeudaTramite = p_da.DaGetDeudaTramite(nroTramite);
                return dtDeudaTramite;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        //public DataTable BlGetTRSVencidas(Int64 nroTramite)
        //{
        //    try
        //    {
        //        DataTable dtDeudaTramite = p_da.DAGetTRSVencidas(nroTramite);
        //        return dtDeudaTramite;
        //    }
        //    catch (BlException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new BlException(ex.Message);
        //    }
        //}


        public String BlGetIdSuperficie()
        {
            try
            {
                DataTable dt = p_da.DaGetIdSuperficieProx();
                String maxId = dt.Rows[0]["ID_SUPERFICIE_MAX"].ToString();
                return maxId;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public bool BlGetAdeudaEntidad(Int64 nroTramite)
        {
            try
            {
                String EstadoMoroso = p_da.DaGetAdeudaEntidad(nroTramite);

                if (EstadoMoroso == "ADEUDA")
                    return true;
                return false;

            }

            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public void BlActualizarAccesoUsuario(String Cuit, out String pRol, out String pUltimoAcceso)
        {
            p_da.DaActualizarAccesoUsuario(Cuit, out pRol, out pUltimoAcceso);
        }

        public void BlExisteEnSifcos(String Cuit, out Int64 pExiste)
        {
            p_da.DaExisteEnSifcos(Cuit, out pExiste);
        }

        public String BlActualizarRolUsuario(String Cuit, Int64 Rol)
        {
            String resultado = p_da.DaActualizarRolUsuario(Cuit, Rol);
            return resultado;
        }

        public String BlActualizarOrgUsuario(String Cuit, Int64 IdOrganismo)
        {
            String resultado = p_da.DaActualizarOrgUsuario(Cuit, IdOrganismo);
            return resultado;
        }

        public String BlGetRolUsuario(String Cuit)
        {
            String resultado = p_da.DaGetRolUsuario(Cuit);
            return resultado;
        }

        public List<Roles> BlGetRelacionesUsuario(String Cuit)
        {
            var resultado = p_da.DaGetRelacionesUsuario(Cuit);
            return resultado;
        }

        public String BlEliminarRolUsuario(String Cuit)
        {
            String resultado = p_da.DaEliminarRolUsuario(Cuit);
            return resultado;
        }

        public String BlEliminarOrganismo(String Cuit)
        {
            String resultado = p_da.DaEliminarOrganismo(Cuit);
            return resultado;
        }

        public String BlEliminarRelProd(String Id_Producto)
        {
            String resultado = p_da.DaEliminarRelProd(Id_Producto);
            return resultado;
        }

        public String BlAgregarUsuario(String Cuit, Int64 Rol)
        {
            String resultado = p_da.DaAgregarUsuario(Cuit, Rol);
            return resultado;
        }

        public String BLAgregarUsuarioOrganismo(String Cuil, Int64 IdOrganismo)
        {
            String resultado = p_da.DaAgregarUsuarioOrganismo(Cuil, IdOrganismo);
            return resultado;
        }

        public String BlRegistrarSuperficie(String pN_superficie)
        {
            try
            {
                String resultado = p_da.DaRegistrarSuperficie(pN_superficie);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlRegistrarProducto(String pN_Producto)
        {
            try
            {
                String resultado = p_da.DaRegistrarProducto(pN_Producto);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// Devuelve "OK" si se guardó con éxito la inscripción. Devuelve "ERROR" si ocurrió un error en BD al insertar los datos. Devuelve ERROR PRODUCTO cuando no se pudo insertar un producto de la lista de productos del tramite pero si se insertó el tramite.
        /// </summary>
        /// <param name="Alta"></param>
        /// <returns></returns>
        public String BlRegistrarInscripcion(InscripcionSifcos Alta, out string nuevoNroTramite)
        {
            try
            {
                String resultado = p_da.DaRegistrarInscripcion(Alta, out nuevoNroTramite);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public String BlRegistrarTramiteParaBajaComercio(InscripcionSifcos Alta, out string nuevoNroTramite)
        {
            try
            {
                String resultado = p_da.DaRegistrarTramiteParaBajaComercio(Alta, out nuevoNroTramite);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public String BlModificarDomicilioLocal(Domicilio domicilio1,String IdEntidad, InscripcionSifcosDto Alta)
        {
            try
            {
                String resultado = p_da.DaModificarDomicilioLocal(domicilio1,IdEntidad, Alta);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlModificarDomicilioLocaldelTramite(InscripcionSifcosDto Alta)
        {
            try
            {
                String resultado = p_da.DaModificarDomicilioLocaldelTramite(Alta);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string BlModificarDomicilioLegal(Domicilio domicilio2,String IdEntidad, InscripcionSifcosDto tramiteDto)
        {
            try
            {
                String resultado = p_da.DaModificarDomicilioLegal(domicilio2,IdEntidad, tramiteDto);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlModificarDomicilioLegaldelTramite(InscripcionSifcosDto Alta)
        {
            try
            {
                String resultado = p_da.DaModificarDomicilioLegaldelTramite(Alta);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlModificarInfoGral(InscripcionSifcosDto Alta)
        {
            try
            {
                String resultado = p_da.DaModificarInfoGral(Alta);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlModificarProdAct(InscripcionSifcosDto Alta)
        {
            try
            {
                String resultado = p_da.DaModificarProdAct(Alta);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlModificarRepLegal(InscripcionSifcosDto Alta)
        {
            try
            {
                String resultado = p_da.DaModificarRepLegal(Alta);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlModificarContacto(InscripcionSifcosDto Alta)
        {
            try
            {
                String resultado = p_da.DaModificarContacto(Alta);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlModificarProducto(String IdProducto, String NProducto)
        {
            try
            {
                String resultado = p_da.DaModificarProducto(IdProducto, NProducto);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlModificarUbicacion(InscripcionSifcosDto tramiteDto)
        {
            try
            {
                String resultado = p_da.DaModificarUbicacion(tramiteDto);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public String BlNoConfirmarProducto(String idProducto, String nroTramite)
        {
            try
            {
                String resultado = p_da.DaNoConfirmarProducto(idProducto, nroTramite);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //sirve para registrar el contacto y modificar el contacto
        public String BlRegistrarContacto(Comunicacion Contacto)
        {
            try
            {
                String resultado = p_da.DaRegistrarContacto(Contacto);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String GenerarTransaccionTRS(String pCUIT, String pIdSexo, String pNroDocumento, String pPaiCodPais,
            Int64 pIdNumero, Int64 pId_concepto, DateTime pFecha_desde, String pCod_ente,
            Int64 pCantidad, float pImporte, String pNro_expediente, String pAnio_expediente,
            out string nroTransaccionTasa)
        {
            try
            {
                nroTransaccionTasa = null;
                var resultado = p_da.DaGetNroTasaTramite(pCUIT, pIdSexo, pNroDocumento, pPaiCodPais, pIdNumero,
                    pId_concepto, pFecha_desde, pCod_ente, pCantidad, pImporte, pNro_expediente, pAnio_expediente,
                    out nroTransaccionTasa);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Carga un domicilio dom_manager y devuelve el id_vinculo del nuevo domicilio
        /// </summary>
        /// <param name="idEntidad">Está compuesto por el CUIT + ID_APLICACIÓN. (id_aplicaion de SIFCOS es 152)</param>
        /// <param name="idVin">parametro de salida que se usa para cargar el id_vinculo del domicilio generado</param>
        /// <returns></returns>
        public string CargarDomicilio(string idEntidad, string idProvincia, string idDepartamento,
            string nombreLocalidad, string idTipoCalle,
            string nombreTipoCalle, string idCalle, string nombreCalle, string idBarrio, string nombreBarrio,
            string idPrecinto, string altura,
            string piso, string dpto, string torre, string idLocalidad, string codPostal, string manzana, string lote,
            out int? idVin)
        {
            return p_da.CargarDomicilio(idEntidad, idProvincia, idDepartamento, nombreLocalidad, idTipoCalle,
                nombreTipoCalle, idCalle, nombreCalle, idBarrio, nombreBarrio, idPrecinto, altura, piso, dpto, torre,
                idLocalidad, codPostal, manzana, lote, out idVin);
        }

        /// <summary>
        /// Consulta en base de datos si la TRS está paga. Busca el trámite y del mismo obtiene el idTransaccion para luego consulta en las Tasas si la misma es PAGADO="S" ó "N". 
        /// Retorna TRUE si está paga, y FALSE si no está paga.
        /// </summary>
        /// <param name="nroTramite">Clave primaria de la tabla T_TRAMITES_SIFCOS</param>
        public bool BlTRS_EstaPaga(Int64 nroTramite)
        {
            try
            {
                bool resultado = p_da.DaTRS_EstaPaga(nroTramite);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }

        }

        public string BlAsignarNrosifcos(Int64 nroTramite, String Descripcion, string cuilUsuarioCidi)
        {
            try
            {
                string resultado = p_da.DaAsignarNroSifcos(nroTramite, Descripcion, cuilUsuarioCidi);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string BlAsignarProductosAct(string idProducto, string idActividad)
        {
            try
            {
                string resultado = p_da.DaAsignarProductosAct(idProducto, idActividad);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string BlModificarRelacionProdAct(string idProducto, List<Actividad> Actividades)
        {
            try
            {
                string resultado = p_da.DaModificarRelacionProdAct(idProducto, Actividades);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public bool BlRegistrarEstado(Int64 nroTramite, Int64 idEstado, String Descripcion, String CuilCidi)
        {
            try
            {
                bool resultado = p_da.DaRegitrarEstado(nroTramite, idEstado, Descripcion, CuilCidi);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }

        }

        /// <summary>
        /// Guarda una entidad en el SIFCoS nuevo y devuelve el nro ID_ENTIDAD generada.
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns>Valor entero que representa el id_entidad generado.</returns>
        public String BlRegistrarEntidad(InscripcionSifcos Alta, out Int32 idEntidad)
        {
            try
            {
                String resultado = p_da.DaRegistrarEntidad(Alta, out idEntidad);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                idEntidad = 0;
                return ex.Message;
            }
        }

        public String BlRegistrarEntidadMigracion(InscripcionSifcos Alta, out Int32 idEntidad)
        {
            try
            {
                String resultado = p_da.DaRegistrarEntidadMigracion(Alta, out idEntidad);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                idEntidad = 0;
                return ex.Message;
            }
        }

        public bool BlActualizarTasas(Int64 nroTramite, string nroTransaccion, string pagada)
        {
            try
            {
                bool actualizarTasas = p_da.DaActualizarTasas(nroTramite, nroTransaccion, pagada);
                return actualizarTasas;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }

        }

        /// <summary>
        /// Autor: IB Sobrecarga por compatibilidad
        /// </summary>
        /// <param name="nroTramite"></param>
        /// <param name="nroTransaccion"></param>
        /// <param name="pagada"></param>
        /// <returns></returns>
        public String BlRegistrarTasas(Int64 nroTramite, string nroTransaccion, string pagada)
        {
            return BlRegistrarTasas(nroTramite, nroTransaccion, null);
        }

        public String BlRegistrarTasas(Int64 nroTramite, string nroTransaccion, string pagada,
            string nroLiquidacionOriginal)
        {
            try
            {
                String RegistrarTasas =
                    p_da.DaRegistrarTasas(nroTramite, nroTransaccion, pagada, nroLiquidacionOriginal);
                return RegistrarTasas;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }




        /// <summary>
        /// Consulta en BD por el id del tramite y trae los datos cargados en el DTO
        /// </summary>
        /// <param name="nroTramite"></param>
        /// <returns></returns>
        public List<InscripcionSifcosDto> GetInscripcionSifcosDto(Int64 nroTramite)
        {
            var objInscripcionDTO = new InscripcionSifcosDto();
            var lis = new List<InscripcionSifcosDto>();

            DataTable dt = p_da.DaConsultaTramite(nroTramite);
            foreach (DataRow row in dt.Rows)
            {
                //cargo el objeto dto desde el datatable
                objInscripcionDTO.NroTramiteSifcos = row["NRO_TRAMITE_SIFCOS"].ToString();
                //objInscripcionDTO.IdVinDomLegal = Int64.Parse(row["ID_VIN_DOM_LEGAL"].ToString());
                objInscripcionDTO.IdVinDomLegal = Convert.IsDBNull(row["ID_VIN_DOM_LEGAL"])
                    ? objInscripcionDTO.IdVinDomLegal
                    : Int64.Parse(row["ID_VIN_DOM_LEGAL"].ToString());

                //objInscripcionDTO.IdVinDomLocal = Int64.Parse(row["ID_VIN_DOM_LOCAL"].ToString());
                objInscripcionDTO.IdVinDomLocal = Convert.IsDBNull(row["ID_VIN_DOM_LOCAL"])
                    ? objInscripcionDTO.IdVinDomLocal
                    : Int64.Parse(row["ID_VIN_DOM_LOCAL"].ToString());

                //objInscripcionDTO.CapacUltAnio = row["CAPACITACION_ULT_ANIO"].ToString();
                objInscripcionDTO.CapacUltAnio = Convert.IsDBNull(row["CAPACITACION_ULT_ANIO"])
                    ? objInscripcionDTO.CapacUltAnio
                    : row["CAPACITACION_ULT_ANIO"].ToString();

                //objInscripcionDTO.CuilUsuarioCidi = row["CUIL_USUARIO_CIDI"].ToString();
                objInscripcionDTO.CuilUsuarioCidi = Convert.IsDBNull(row["CUIL_USUARIO_CIDI"])
                    ? objInscripcionDTO.CuilUsuarioCidi
                    : row["CUIL_USUARIO_CIDI"].ToString();

                objInscripcionDTO.IdTipoTramite = row["ID_TIPO_TRAMITE"].ToString();
                objInscripcionDTO.NombreTipoTramite = row["N_TIPO_TRAMITE"].ToString();
                objInscripcionDTO.FecIniTramite = Convert.ToDateTime(row["FEC_INI_TRAMITE"].ToString());
                // objInscripcionDTO.IdTransaccionTasa = row["IDTRASACCIONTASA"].ToString();
                // objInscripcionDTO.PagoTasa = row["PAGOTASA"].ToString();

                //objInscripcionDTO.CantTotalpers = row["CANT_PERS_TOTAL"].ToString();
                objInscripcionDTO.CantTotalpers = Convert.IsDBNull(row["CANT_PERS_TOTAL"])
                    ? objInscripcionDTO.CantTotalpers
                    : row["CANT_PERS_TOTAL"].ToString();

                //objInscripcionDTO.CantPersRelDep = row["CANT_PERS_REL_DEPENDENCIA"].ToString();
                objInscripcionDTO.CantPersRelDep = Convert.IsDBNull(row["CANT_PERS_REL_DEPENDENCIA"])
                    ? objInscripcionDTO.CantPersRelDep
                    : row["CANT_PERS_REL_DEPENDENCIA"].ToString();

                //objInscripcionDTO.NombreOrigenProveedor = row["N_ORIGEN_PROVEEDOR"].ToString();
                objInscripcionDTO.NombreOrigenProveedor = Convert.IsDBNull(row["N_ORIGEN_PROVEEDOR"])
                    ? objInscripcionDTO.NombreOrigenProveedor
                    : row["N_ORIGEN_PROVEEDOR"].ToString();

                objInscripcionDTO.NombreEstadoActual = row["N_ESTADO_TRAMITE"].ToString();
                objInscripcionDTO.IdEntidad = row["ID_ENTIDAD"].ToString();

                //objInscripcionDTO.RazonSocial = row["RAZON_SOCIAL"].ToString();
                objInscripcionDTO.RazonSocial = Convert.IsDBNull(row["RAZON_SOCIAL"])
                    ? objInscripcionDTO.RazonSocial
                    : row["RAZON_SOCIAL"].ToString();

                objInscripcionDTO.CUIT = row["CUIT"].ToString();

                //objInscripcionDTO.IdSede = row["ID_SEDE"].ToString();
                objInscripcionDTO.IdSede = Convert.IsDBNull(row["ID_SEDE"])
                    ? objInscripcionDTO.IdSede
                    : row["ID_SEDE"].ToString();

                //objInscripcionDTO.NombreSede = row["SEDES"].ToString();
                objInscripcionDTO.NombreSede = Convert.IsDBNull(row["SEDES"])
                    ? objInscripcionDTO.NombreSede
                    : row["SEDES"].ToString();

                //objInscripcionDTO.Local = row["LOCAL"].ToString();
                objInscripcionDTO.Local =
                    Convert.IsDBNull(row["LOCAL"]) ? objInscripcionDTO.Local : row["LOCAL"].ToString();

                //objInscripcionDTO.Oficina = row["OFICINA"].ToString();
                objInscripcionDTO.Oficina = Convert.IsDBNull(row["OFICINA"])
                    ? objInscripcionDTO.Oficina
                    : row["OFICINA"].ToString();

                //objInscripcionDTO.Stand = row["STAND"].ToString();
                objInscripcionDTO.Stand =
                    Convert.IsDBNull(row["STAND"]) ? objInscripcionDTO.Stand : row["STAND"].ToString();

                //objInscripcionDTO.Cobertura = row["COBERTURA_MEDICA"].ToString();
                objInscripcionDTO.Cobertura = Convert.IsDBNull(row["COBERTURA_MEDICA"])
                    ? objInscripcionDTO.Cobertura
                    : row["COBERTURA_MEDICA"].ToString();

                //objInscripcionDTO.Seguro = row["SEGURO_LOCAL"].ToString();
                objInscripcionDTO.Seguro = Convert.IsDBNull(row["SEGURO_LOCAL"])
                    ? objInscripcionDTO.Seguro
                    : row["SEGURO_LOCAL"].ToString();

                //objInscripcionDTO.Propietario = row["PROPIETARIO"].ToString();
                objInscripcionDTO.Propietario = Convert.IsDBNull(row["PROPIETARIO"])
                    ? objInscripcionDTO.Propietario
                    : row["PROPIETARIO"].ToString();

                //objInscripcionDTO.Latitud = row["LATITUD_UBI"].ToString();
                objInscripcionDTO.Latitud = Convert.IsDBNull(row["LATITUD_UBI"])
                    ? objInscripcionDTO.Latitud
                    : row["LATITUD_UBI"].ToString();

                //objInscripcionDTO.Longitud = row["LONGITUD_UBI"].ToString();
                objInscripcionDTO.Longitud = Convert.IsDBNull(row["LATITUD_UBI"])
                    ? objInscripcionDTO.Longitud
                    : row["LATITUD_UBI"].ToString();

                //objInscripcionDTO.ActividadPrimaria = row["ACTIVIDAD_PRI"].ToString();
                objInscripcionDTO.ActividadPrimaria = Convert.IsDBNull(row["ACTIVIDAD_PRI"])
                    ? objInscripcionDTO.ActividadPrimaria
                    : row["ACTIVIDAD_PRI"].ToString();

                //objInscripcionDTO.ActividadSecundaria = row["ACTIVIDAD_SEC"].ToString();
                objInscripcionDTO.ActividadSecundaria = Convert.IsDBNull(row["ACTIVIDAD_SEC"])
                    ? objInscripcionDTO.ActividadSecundaria
                    : row["ACTIVIDAD_SEC"].ToString();

                //objInscripcionDTO.NroHabMunicipal = row["nro_hab_municipal"].ToString();
                objInscripcionDTO.NroHabMunicipal = Convert.IsDBNull(row["nro_hab_municipal"])
                    ? objInscripcionDTO.NroHabMunicipal
                    : row["nro_hab_municipal"].ToString();

                //objInscripcionDTO.NroDGR = row["NRO_DGR"].ToString();
                objInscripcionDTO.NroDGR = Convert.IsDBNull(row["NRO_DGR"])
                    ? objInscripcionDTO.NroDGR
                    : row["NRO_DGR"].ToString();

                objInscripcionDTO.FecVencimiento = string.IsNullOrEmpty(row["FEC_VENCIMIENTO"].ToString())
                    ? new DateTime(2017, 1, 1)
                    : Convert.ToDateTime(row["FEC_VENCIMIENTO"].ToString());

                //gestor que realizó el tramite.
                //objInscripcionDTO.NombreYApellidoGestor = row["gestor"].ToString();
                objInscripcionDTO.NombreYApellidoGestor = Convert.IsDBNull(row["gestor"])
                    ? objInscripcionDTO.NombreYApellidoGestor
                    : row["gestor"].ToString();

                //objInscripcionDTO.DniGestor = row["dni_gestor"].ToString();
                objInscripcionDTO.DniGestor = Convert.IsDBNull(row["dni_gestor"])
                    ? objInscripcionDTO.DniGestor
                    : row["dni_gestor"].ToString();

                //responsable legal
                //objInscripcionDTO.NombreYApellidoRepLegal= row["rep_legal"].ToString();
                objInscripcionDTO.NombreYApellidoRepLegal = Convert.IsDBNull(row["rep_legal"])
                    ? objInscripcionDTO.NombreYApellidoRepLegal
                    : row["rep_legal"].ToString();

                //objInscripcionDTO.DniRepLegal = row["dni_rep_legal"].ToString();
                objInscripcionDTO.DniRepLegal = Convert.IsDBNull(row["dni_rep_legal"])
                    ? objInscripcionDTO.DniRepLegal
                    : row["dni_rep_legal"].ToString();

                //objInscripcionDTO.CelularRepLegal = row["celular_rep_legal"].ToString();
                objInscripcionDTO.CelularRepLegal = Convert.IsDBNull(row["celular_rep_legal"])
                    ? objInscripcionDTO.CelularRepLegal
                    : row["celular_rep_legal"].ToString();

                //objInscripcionDTO.NombreCargoRepLegal = row["n_cargo"].ToString();
                objInscripcionDTO.NombreCargoRepLegal = Convert.IsDBNull(row["n_cargo"])
                    ? objInscripcionDTO.NombreCargoRepLegal
                    : row["n_cargo"].ToString();

                objInscripcionDTO.NroSifcos = row["nro_sifcos"].ToString();
                //objInscripcionDTO.NombreFantasia = row["nombre_fantasia"].ToString();
                objInscripcionDTO.NombreFantasia = Convert.IsDBNull(row["nombre_fantasia"])
                    ? objInscripcionDTO.NombreFantasia
                    : row["nombre_fantasia"].ToString();

                //objInscripcionDTO.RangoAlquiler = row["rango_alquiler"].ToString();
                objInscripcionDTO.RangoAlquiler = Convert.IsDBNull(row["rango_alquiler"])
                    ? objInscripcionDTO.RangoAlquiler
                    : row["rango_alquiler"].ToString();


                //(IB) Agrego los ids de la actividad primaria y secundaria
                objInscripcionDTO.IdActividadPrimaria = Convert.IsDBNull(row["id_actividad_ppal"])
                    ? null
                    : Convert.ToString(row["id_actividad_ppal"]);
                objInscripcionDTO.IdActividadSecundaria = Convert.IsDBNull(row["id_actividad_sria"])
                    ? null
                    : Convert.ToString(row["id_actividad_sria"]);

                //(IB) Agrego los ids y descripción de rubros
                if (dt.Columns.Contains("id_rubro_pri"))
                    objInscripcionDTO.IdRubroPrimario = Convert.IsDBNull(row["id_rubro_pri"])
                        ? null
                        : (int?)Convert.ToInt32(row["id_rubro_pri"]);
                if (dt.Columns.Contains("id_rubro_sec"))
                    objInscripcionDTO.IdRubroSecundario = Convert.IsDBNull(row["id_rubro_sec"])
                        ? null
                        : (int?)Convert.ToInt32(row["id_rubro_sec"]);
                objInscripcionDTO.RubroPrimario =
                    Convert.IsDBNull(row["RUBRO_PRI"]) ? null : Convert.ToString(row["RUBRO_PRI"]);
                objInscripcionDTO.RubroSecundario =
                    Convert.IsDBNull(row["RUBRO_SEC"]) ? null : Convert.ToString(row["RUBRO_SEC"]);
            }

            lis.Add(objInscripcionDTO);

            return lis;

        }


        public DataTable BlGetEmpresaEnRentas(string pCuit)
        {
            return p_da.BlGetEmpresaEnRentas(pCuit);
        }


        /// <summary>
        /// Devuelve la cantidad de años y fecha del último tramite Autorizado(estado Verificado en Boca Secretaría) que se realizó para un CUIT.
        /// </summary>
        /// <param name="pCuit">CUIT de la Empresa que deseamos consultar su deuda.</param>
        /// <param name="pNroSifcos">Nro sifcos de la sucursal del cuit indicado</param>
        /// <returns></returns>
        public DataTable BlGetAniosDeudaTRS(string pCuit, string pNroSifcos)
        {
            return p_da.GetAniosDeudaTRS(pCuit, pNroSifcos);
        }


        /// <summary>
        /// Obtiene la cantidad de trs pagadas sin utilizar, es decir que no están asignadas a algun trámite.
        /// Nota: sólo se tiene en cuenta tasas pagas para el REEMPADRONAMIENTO.
        /// </summary>
        /// <param name="pCuit"></param>
        /// <returns></returns>
        public int BlGetCantTasasPagadas(string pCuit)
        {
            var dt = p_da.GetTasasPagadasSinUsar(pCuit);
            return dt.Rows.Count;
        }


        /// <summary>
        ///     TRS pagas sin utilizar en algun tramite para el cuit.
        /// </summary>
        /// <param name="pCuit"></param>
        /// <returns></returns>
        public List<Trs> BlGetTasasPagadasSinUsar(string pCuit)
        {
            var dt = p_da.GetTasasPagadasSinUsar(pCuit);
            var lstTrs = new List<Trs>();

            foreach (DataRow row in dt.Rows)
            {
                lstTrs.Add(new Trs
                {
                    AnioFiscal = string.IsNullOrEmpty(row["ANIO_FISCAL"].ToString())
                        ? new int?()
                        : int.Parse(row["ANIO_FISCAL"].ToString()),
                    NroLiquidacion = row["NROLIQUIDACIONORIGINAL"].ToString(),
                    NroTransaccion = row["NRO_TRANSACCION"].ToString(),
                    FechaCobro = row["FECHA_COBRO"].ToString()

                });
            }

            return lstTrs;
        }

        public List<Trs> BlGetTasasPagadasSinUsarAlta(string pCuit)
        {
            var dt = p_da.GetTasasPagadasSinUsarParaAlta(pCuit);
            var lstTrs = new List<Trs>();

            foreach (DataRow row in dt.Rows)
            {
                lstTrs.Add(new Trs
                {
                    AnioFiscal = string.IsNullOrEmpty(row["ANIO_FISCAL"].ToString())
                        ? new int?()
                        : int.Parse(row["ANIO_FISCAL"].ToString()),
                    NroLiquidacion = row["NROLIQUIDACIONORIGINAL"].ToString(),
                    NroTransaccion = row["NRO_TRANSACCION"].ToString(),
                    FechaCobro = row["FECHA_COBRO"].ToString()

                });
            }

            return lstTrs;
        }

        public List<Trs> BlGetTasasPagadasSinUsarCombo(string pCuit)
        {
            var dt = p_da.GetTasasPagadasSinUsarParaAlta(pCuit);
            var lstTrs = new List<Trs>();

            foreach (DataRow row in dt.Rows)
            {
                lstTrs.Add(new Trs
                {
                    NombreFormateado = string.IsNullOrEmpty(row["comboTRS"].ToString())
                        ? ""
                        : row["comboTRS"].ToString(),
                    NroTransaccion = row["NRO_TRANSACCION"].ToString()

                });
            }

            return lstTrs;
        }

        public DataTable BlGetParametrosGral()
        {
            return p_da.BlGetParametrosGral();
        }


        /// <summary>
        /// Devuelve las entidades y los tramite de un cuit. Cada Entidad tiene su id_entidad y su NroSifcos. 
        /// </summary>
        /// <param name="cuit"></param>
        /// <returns></returns>
        public List<consultaTramite> BlGetEntidadTramite(string cuit)
        {
            var dt = p_da.BlGetEntidadTramite(cuit);
            List<consultaTramite> ListConsultaTramite = new List<consultaTramite>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListConsultaTramite.Add(new consultaTramite()
                {
                    idEntidad = dt.Rows[i]["ID_ENTIDAD"].ToString(),
                    id_sede_entidad = dt.Rows[i]["id_sede_entidad"].ToString(),
                    CUIT = dt.Rows[i]["CUIT"].ToString(),
                    Nro_Sifcos = dt.Rows[i]["NRO_SIFCOS"].ToString(),
                    Nro_tramite = dt.Rows[i]["NRO_TRAMITE"].ToString(),
                    fec_alta = dt.Rows[i]["FECHA_ALTA"].ToString(),
                    Origen = dt.Rows[i]["ORIGEN"].ToString()

                });
            }

            return ListConsultaTramite;
        }


        public DataTable BlGetTramitesPorEstado(string fechaDesde, string fechaHasta)
        {
            return p_da.DaGetTramitesPorEstado(fechaDesde, fechaHasta);
        }

        public DataTable BlGetTramitesPorActividad(string fechaDesde, string fechaHasta)
        {
            return p_da.DaGetTramitesPorActividad(fechaDesde, fechaHasta);
        }

        public DataTable BlGetTramitesPorTipoTramite(string fechaDesde, string fechaHasta)
        {
            return p_da.DaGetTramitesPorTipoTramite(fechaDesde, fechaHasta);
        }

        public DataTable BlGetTramitesPorlocalidad(string fechaDesde, string fechaHasta)
        {
            return p_da.DaGetTramitesPorLocalidad(fechaDesde, fechaHasta);
        }

        public DateTime? BlGetFechaUltimoTramiteSifcosViejo(string nroSifcos)
        {
            return p_da.DaGetFechaUltimoTramite_SifcosViejo(nroSifcos);
        }

        public DateTime? BlGetFechaUltimoTramiteSifcosNuevo(string nroSifcos)
        {
            return p_da.DaGetFechaUltimoTramite_SifcosNuevo(nroSifcos);

        }

        public DateTime BlGetFechaUltimoTramite(string nroSifcos)
        {
            DateTime? fechaUlt_sifcosNuevo = p_da.DaGetFechaUltimoTramite_SifcosNuevo(nroSifcos);
            DateTime? fechaUlt_sifcosViejo = p_da.DaGetFechaUltimoTramite_SifcosViejo(nroSifcos);

            if (!fechaUlt_sifcosNuevo.HasValue)
            {
                if (fechaUlt_sifcosViejo.HasValue)
                    return fechaUlt_sifcosViejo.Value;
            }

            return fechaUlt_sifcosNuevo.Value;

        }

        public DataTable BlGetTasaByNroReferencia(string nroReferencia)
        {
            return p_da.BlGetTasaByNroReferencia(nroReferencia);
        }

        public string LiberarTasa(string nroReferencia)
        {
            return p_da.LiberarTasa(nroReferencia);
        }

        public string LiberarTasaNS(string nroTramite, string nroTransaccion)
        {
            return p_da.LiberarTasaNS(nroTramite, nroTransaccion);
        }

        public DataTable BlGetTasaByCUIT(string cuit)
        {
            return p_da.BlGetTasaByCUIT(cuit);
        }

        public DataTable BlGetTasaSifcoViejoByNroReferencia(string nroReferencia)
        {
            return p_da.DaGetTasaSifcosViejoByNroReferencia(nroReferencia);
        }

        public DataTable BlGetTasaSifcoNuevoByNroReferencia(string nroReferencia)
        {
            return p_da.DaGetTasaSifcosNuevoByNroReferencia(nroReferencia);
        }

        public string RegistrarEntidadPerJur(string cuit, string razonSocial, string nombreFantasia)
        {
            return p_da.RegistrarEntidadPerJur(cuit, razonSocial, nombreFantasia);
        }

        public string BlModificarInscripcion(InscripcionSifcosDto tramiteDto, Domicilio domicilio1,
            Domicilio domicilio2)
        {
            throw new NotImplementedException();
        }

        public DataTable BLConsultarDuplicados(string txtFechaDesde, string txtFechaHasta)
        {
            return p_da.DAConsultarDuplicados(txtFechaDesde, txtFechaHasta);
        }

        public bool BlExisteTramite(InscripcionSifcos objetoInscripcion)
        {
            DataTable dt = p_da.DaExisteTramite(objetoInscripcion);
            return dt.Rows.Count > 0;
        }

        //public DataTable BlConsultaSQL(String SQL)
        //{
        //    return p_da.DaConsultaSQL(SQL);
        //}

        public DataTable BlReporteGerencial(String pTipoTramite, String pIdEstado, String pFechaDesde,
            String pFechaHasta, String pCuit, String pRazonSocial, String pNroTramiteDesde, String pNroTramiteHasta,
            String pNroSifcosDesde, String pNroSifcosHasta, String pIdOrganismo, String pIdOrganismoPadre,
            String pIdDepto, String pIdLocalidad)
        {
            return p_da.DAReporteGerencial(pTipoTramite, pIdEstado, pFechaDesde, pFechaHasta, pCuit, pRazonSocial,
                pNroTramiteDesde, pNroTramiteHasta, pNroSifcosDesde, pNroSifcosHasta, pIdOrganismo, pIdOrganismoPadre,
                pIdDepto, pIdLocalidad);
        }

        public DataTable BlGetTasasAsignadas(String NroTramite)
        {
            return p_da.DaGetTasasAsignadas(NroTramite);
        }

        public DataTable BlGetUltimoTramiteSifcosNuevo(string nroSifcos)
        {
            DataTable dt = p_da.DaGetUltimoTramiteSifcosNuevo(nroSifcos);


            return dt;
        }

        public DataTable BlGetGestor(int id_gestor)
        {
            return p_da.DaGetGestor(id_gestor);
        }

        public DataTable BlGetActividades()
        {
            return p_da.BlGetActividades();
        }

        public DataTable BlGetProductosSinAsignar()
        {
            return p_da.BlGetProductosSinAsignar();
        }

        public string BlRegistrarBajaComercio(string nroSifcos, string cuil_usuario_cidi, DateTime fechaCese,
            int IdDocumentoCDD1, int IdDocumentoCDD2)
        {
            var resultado = "";

            //busco el comercio y los datos del ultimo tramite.

            resultado = p_da.DaRegistrarBajaComercioCese(nroSifcos, cuil_usuario_cidi, fechaCese, IdDocumentoCDD1,
                IdDocumentoCDD2);

            return resultado;
        }

        public DataTable BlGetTramitesDeBaja(string NroSifcos, string cuit)
        {
            var dt = p_da.BlGetTramitesDeBaja(NroSifcos, cuit);
            return dt;
        }

        public List<ReporteDto> GetDatosReporteDtos()
        {
            // método que solo se utiliza para cargar el dataset del reporte Reporte.rdlc.
            return new List<ReporteDto>();
        }

        public List<ReporteDto> ConvertirAReporteDto(DataTable resultadoConsulta)
        {
            var listaReporteDto = new List<ReporteDto>();
            foreach (DataRow row in resultadoConsulta.Rows)
            {
                ReporteDto reporetDto = new ReporteDto
                {
                    BOCA_RECEPCION = row["BOCA_RECEPCION"].ToString(),
                    NRO_TRAMITE = row["NRO_TRAMITE"].ToString(),
                    ESTADO_TRAMITE = row["ESTADO_TRAMITE"].ToString(),
                    TIPO_TRAMITE = row["TIPO_TRAMITE"].ToString(),
                    ANIO_OPERATIVO = row["ANIO_OPERATIVO"].ToString(),
                    NRO_SIFCOS = row["NRO_SIFCOS"].ToString(),
                    ACTIVIDAD_PRI = row["ACTIVIDAD_PRI"].ToString(),
                    ACTIVIDAD_SEC = row["ACTIVIDAD_SEC"].ToString(),
                    FEC_INI_ACT = row["FEC_INI_ACT"].ToString(),
                    CUIT = row["CUIT"].ToString(),
                    RAZON_SOCIAL = row["RAZON_SOCIAL"].ToString(),
                    NOMBRE_FANTASIA = row["NOMBRE_FANTASIA"].ToString(),
                    REP_LEGAL = row["REP_LEGAL"].ToString(),
                    DNI_REP_LEGAL = row["DNI_REP_LEGAL"].ToString(),
                    NRO_DGR = row["NRO_DGR"].ToString(),
                    NRO_HAB_MUNICIPAL = row["NRO_HAB_MUNICIPAL"].ToString(),
                    L_PROVINCIA = row["L_PROVINCIA"].ToString(),
                    L_DEPTO = row["L_DEPTO"].ToString(),
                    L_LOCALIDAD = row["L_LOCALIDAD"].ToString(),
                    L_BARRIO = row["L_BARRIO"].ToString(),
                    L_CALLE = row["L_CALLE"].ToString(),
                    L_ALTURA = row["L_ALTURA"].ToString(),
                    L_CODPOSTAL = row["L_CODPOSTAL"].ToString(),
                    L_PISO = row["L_PISO"].ToString(),
                    L_DPTO = row["L_DPTO"].ToString(),
                    R_PROVINCIA = row["R_PROVINCIA"].ToString(),
                    R_DEPTO = row["R_DEPTO"].ToString(),
                    R_LOCALIDAD = row["R_LOCALIDAD"].ToString(),
                    R_BARRIO = row["R_BARRIO"].ToString(),
                    R_CALLE = row["R_CALLE"].ToString(),
                    R_ALTURA = row["R_ALTURA"].ToString(),
                    R_CODPOSTAL = row["R_CODPOSTAL"].ToString(),
                    R_PISO = row["R_PISO"].ToString(),
                    R_DPTO = row["R_DPTO"].ToString(),
                    //TEL_PRINCIPAL = row["TEL_PRINCIPAL"].ToString(),
                    //CELULAR = row["CELULAR"].ToString(),
                    //EMAIL = row["EMAIL"].ToString(),
                    //PAG_WEB = row["PAG_WEB"].ToString(),
                    //FACEBOOK = row["FACEBOOK"].ToString(),
                    SUP_ADMIN = row["SUP_ADMIN"].ToString(),
                    SUP_VENTAS = row["SUP_VENTAS"].ToString(),
                    SUP_DEPOSITO = row["SUP_DEPOSITO"].ToString(),
                    CANT_PERS_TOTAL = row["CANT_PERS_TOTAL"].ToString(),
                    CANT_PERS_REL_DEPENDENCIA = row["CANT_PERS_REL_DEPENDENCIA"].ToString(),
                    CANT_REEMPADRONAMIENTO = row["CANT_REEMPADRONAMIENTO"].ToString(),
                    FEC_VENCIMIENTO = row["FEC_VENCIMIENTO"].ToString()
                };
                listaReporteDto.Add(reporetDto);
            }

            return listaReporteDto;
        }


        /// <summary>
        /// Asigna a un tramite el ID_ORGANISMO_ALTA , que es la Boca donde el contribuyente presenta el tramite de alta y la Boca lo recibe. Se asigna al tramita dicha boca.
        /// </summary>
        /// <param name="nro_tramite"></param>
        /// <returns></returns>
        public string BlActualizarOrganismoAlta(int nro_tramite, int id_organismo)
        {
            return p_da.DaActualizarOrganismoAlta(nro_tramite, id_organismo);
        }

        public List<ComercioDto> BlReporteComercios(string fechaDesde, string fechaHasta, string idDepartamento,
            string idLocalidad)
        {
            DataTable dt = p_da.DaReporteComercio(fechaDesde, fechaHasta, idDepartamento, idLocalidad);
            var listComercios = new List<ComercioDto>();
            foreach (DataRow row in dt.Rows)
            {
                var c = new ComercioDto
                {
                    NRO_SIFCOS = row["NRO_SIFCOS"].ToString(),
                    CUIT = row["CUIT"].ToString(),
                    RAZON_SOCIAL = row["RAZON_SOCIAL"].ToString(),
                    DEBE = row["DEBE"].ToString(),
                    ID_DEPARTAMENTO = row["ID_DEPARTAMENTO"].ToString(),
                    ID_LOCALIDAD = row["ID_LOCALIDAD"].ToString(),
                    DOMICILIO = row["DOMICILIO"].ToString(),
                    FEC_VTO = row["FEC_VTO"].ToString()
                };
                listComercios.Add(c);
            }

            return listComercios;
        }

        public string BlActualizarOrganismoAltaPorNroSifcos(int nroTramite, int nroSifcos)
        {
            return p_da.DaActualizarOrganismoAltaPorNroSifcos(nroTramite, nroSifcos);
        }

        public DataTable BLConsultarContacto(String nroSifcos)
        {
            return p_da.DaConsultarContacto(nroSifcos);
        }


        public List<ComercioDto> GetComerciosDtos(int IdOrganismo, int IdDepartamento, int IdLocalidad, int? IdProducto,
            int? IdRubro)
        {
            DataTable dt =
                p_da.DaGetComerciosSIFCoSNuevo(IdOrganismo, IdDepartamento, IdLocalidad, IdProducto, IdRubro);
            var ComerciosMapa = new List<ComercioDto>();
            foreach (DataRow row in dt.Rows)
            {
                var c = new ComercioDto
                {
                    RAZON_SOCIAL = row["RAZON_SOCIAL"].ToString(),
                    CUIT = row["CUIT"].ToString(),
                    LATITUD = row["LATITUD"].ToString(),
                    LONGITUD = row["LONGITUD"].ToString(),
                    DOMICILIO = row["DOMICILIO"].ToString(),
                    ID_ENTIDAD = int.Parse(row["ID_ENTIDAD"].ToString()),
                    NRO_SIFCOS = row["NRO_SIFCOS"].ToString()

                    //RUBRO_PRODUCTO = row["RUBRO"].ToString(),
                    //ID_PRODUCTO = row["ID_PRODUCTO"].ToString(),
                    //ID_LOCALIDAD = row["ID_LOCALIDAD"].ToString(),
                    //ID_DEPARTAMENTO = row["ID_DEPARTAMENTO"].ToString()
                };
                ComerciosMapa.Add(c);
            }

            return ComerciosMapa;
        }

        public DataTable BlGetUbicacionMapa(Int64 NroTramite)
        {
            return p_da.DAGetUbicacionMapa(NroTramite);
        }


        public String BlActEstadoVerificado(String NroTramite)
        {
            return p_da.DaActEstadoVerificado(NroTramite);
        }

        public String BlActEstadoRechazado(String NroTramite)
        {
            return p_da.DaActEstadoRechazado(NroTramite);
        }

        #region PanelDeControl

        /// <summary>
        /// Obtiene la cantidad de reempadronamientos pendientes
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public int BlObtenerCantReempaPendientes(int? IdOrganismo)
        {
            try
            {
                int resultado = p_da.DaObtenerCantReempaPendientes(IdOrganismo);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de reempadronamientos pendientes
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public List<tramitePanel> BlObtenerReempaPendientes(int? IdOrganismo, int pInicio, int pFinal, string pOrder)
        {
            try
            {
                DataTable resultado = p_da.DaObtenerReempaPendientes(IdOrganismo, pInicio, pFinal, pOrder);
                List<tramitePanel> ct = new List<tramitePanel>();

                foreach (DataRow row in resultado.Rows)
                {
                    var c = new tramitePanel();
                    c.Razon_Social = row["RAZON_SOCIAL"].ToString();
                    c.Nro_Sifcos = row["NRO_SIFCOS"].ToString();
                    c.CUIT = row["CUIT"].ToString();
                    c.Nro_tramite = row["NRO_TRAMITE_SIFCOS"].ToString();
                    c.Tipo_Tramite = row["N_TIPO_TRAMITE"].ToString();


                    if (row["FEC_VENCIMIENTO"] != System.DBNull.Value)
                    {
                        DateTime dParser;
                        bool esDatetime = DateTime.TryParse(row["FEC_VENCIMIENTO"].ToString(), out dParser);
                        if (esDatetime)
                        {
                            c.fec_vencimiento = dParser;
                        }
                    }

                    ct.Add(c);
                }

                return ct;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de altas históricas
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public List<tramitePanel> BlObtenerAltasHist(int? IdOrganismo, int pInicio, int pFinal, string pOrder)
        {
            try
            {
                DataTable resultado = p_da.DaObtenerAltasHist(IdOrganismo, pInicio, pFinal, pOrder);
                List<tramitePanel> ct = new List<tramitePanel>();

                foreach (DataRow row in resultado.Rows)
                {
                    var c = new tramitePanel();
                    c.Razon_Social = row["RAZON_SOCIAL"].ToString();
                    c.Nro_Sifcos = row["NRO_SIFCOS"].ToString();
                    c.CUIT = row["CUIT"].ToString();
                    c.Nro_tramite = row["NRO_TRAMITE_SIFCOS"].ToString();
                    c.Tipo_Tramite = row["N_TIPO_TRAMITE"].ToString();

                    if (row["FEC_ALTA"] != System.DBNull.Value)
                    {
                        DateTime dParser;
                        bool esDatetime = DateTime.TryParse(row["FEC_ALTA"].ToString(), out dParser);
                        if (esDatetime)
                        {
                            c.fec_alta = dParser;
                        }
                    }

                    ct.Add(c);
                }

                return ct;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la cantidad de altas históricas
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public int BlObtenerCantAltasHist(int? IdOrganismo)
        {
            try
            {
                int resultado = p_da.DaObtenerCantAltasHist(IdOrganismo);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la cantidad de bajas históricas
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public int BlObtenerCantBajasHist(int? IdOrganismo)
        {
            try
            {
                int resultado = p_da.DaObtenerCantBajasHist(IdOrganismo);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de raltas históricas
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public List<tramitePanel> BlObtenerBajasHist(int? IdOrganismo, int pInicio, int pFinal, string pOrder)
        {
            try
            {
                DataTable resultado = p_da.DaObtenerBajasHist(IdOrganismo, pInicio, pFinal, pOrder);
                List<tramitePanel> ct = new List<tramitePanel>();

                foreach (DataRow row in resultado.Rows)
                {
                    var c = new tramitePanel();
                    c.Razon_Social = row["RAZON_SOCIAL"].ToString();
                    c.Nro_Sifcos = row["NRO_SIFCOS"].ToString();
                    c.CUIT = row["CUIT"].ToString();
                    c.Nro_tramite = row["NRO_TRAMITE_SIFCOS"].ToString();
                    c.Tipo_Tramite = row["N_TIPO_TRAMITE"].ToString();

                    if (row["FEC_ALTA"] != System.DBNull.Value)
                    {
                        DateTime dParser;
                        bool esDatetime = DateTime.TryParse(row["FEC_ALTA"].ToString(), out dParser);
                        if (esDatetime)
                        {
                            c.fec_alta = dParser;
                        }
                    }

                    ct.Add(c);
                }

                return ct;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la cantidad de comercios activos
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public int BlObtenerCantComerciosAct(int? IdOrganismo)
        {
            try
            {
                int resultado = p_da.DaObtenerCantComerciosAct(IdOrganismo);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de comerios activos
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public List<tramitePanel> BlObtenerComerciosAct(int? IdOrganismo, int pInicio, int pFinal, string pOrder)
        {
            try
            {
                DataTable resultado = p_da.DaObtenerComerciosAct(IdOrganismo, pInicio, pFinal, pOrder);
                List<tramitePanel> ct = new List<tramitePanel>();

                foreach (DataRow row in resultado.Rows)
                {
                    var c = new tramitePanel();
                    c.Razon_Social = row["RAZON_SOCIAL"].ToString();
                    c.Nro_Sifcos = row["NRO_SIFCOS"].ToString();
                    c.CUIT = row["CUIT"].ToString();
                    c.Nro_tramite = row["NRO_TRAMITE_SIFCOS"].ToString();
                    c.Tipo_Tramite = row["N_TIPO_TRAMITE"].ToString();

                    if (row["FEC_VENCIMIENTO"] != System.DBNull.Value)
                    {
                        DateTime dParser;
                        bool esDatetime = DateTime.TryParse(row["FEC_VENCIMIENTO"].ToString(), out dParser);
                        if (esDatetime)
                        {
                            c.fec_vencimiento = dParser;
                        }
                    }

                    ct.Add(c);
                }

                return ct;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        #endregion

        #region exportaciones

        /// <summary>
        /// Obtiene el listado de reempadronamientos pendientes para exportar
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public List<tramitePanel> BlExportarReempaPendientes(int? IdOrganismo)
        {
            try
            {
                DataTable resultado = p_da.DaExportarReempaPendientes(IdOrganismo);
                List<tramitePanel> ct = new List<tramitePanel>();

                foreach (DataRow row in resultado.Rows)
                {
                    tramitePanel c = FillTramitePnael(row);
                    ct.Add(c);
                }

                return ct;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Asigna los resultados de un datarow a un trámite panel.
        /// Autor: (IB)
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static tramitePanel FillTramitePnael(DataRow row)
        {
            var c = new tramitePanel();
            c.Razon_Social = row["RAZON_SOCIAL"].ToString();
            c.Nro_Sifcos = row["NRO_SIFCOS"].ToString();
            c.CUIT = row["CUIT"].ToString();
            c.Nro_tramite = row["NRO_TRAMITE_SIFCOS"].ToString();
            c.calle = row["N_CALLE"].ToString();
            c.altura = row["ALTURA"].ToString();
            c.piso = row["PISO"].ToString();
            c.depto = row["DEPTO"].ToString();
            c.torre = row["TORRE"].ToString();
            c.mzna = row["MZNA"].ToString();
            c.lote = row["LOTE"].ToString();
            c.nLocalidad = row["N_LOCALIDAD"].ToString();
            c.cpa = row["cpa"].ToString();
            c.cuitBoca = row["CUIT_BOCA"].ToString();
            c.Boca = row["BOCA"].ToString();
            c.Tipo_Tramite = row["N_TIPO_TRAMITE"].ToString();

            if (row["FEC_VENCIMIENTO"] != System.DBNull.Value)
            {
                DateTime dParser;
                bool esDatetime = DateTime.TryParse(row["FEC_VENCIMIENTO"].ToString(), out dParser);
                if (esDatetime)
                {
                    c.fec_vencimiento = dParser;
                }
            }

            if (row["FEC_INI_TRAMITE"] != System.DBNull.Value)
            {
                DateTime dParser;
                bool esDatetime = DateTime.TryParse(row["FEC_INI_TRAMITE"].ToString(), out dParser);
                if (esDatetime)
                {
                    c.fec_ini_tramite = dParser;
                }
            }

            if (row["FEC_ALTA"] != System.DBNull.Value)
            {
                DateTime dParser;
                bool esDatetime = DateTime.TryParse(row["FEC_ALTA"].ToString(), out dParser);
                if (esDatetime)
                {
                    c.fec_alta = dParser;
                }
            }

            c.TelefonoPrincipal = row["Telefono_principal"].ToString();
            c.CodigoArea = row["COD_AREA"].ToString();
            c.Correo = row["Correo"].ToString();
            c.ActividadPri = row["ACTIVIDAD_PRI"].ToString();
            c.ActividadSec = row["ACTIVIDAD_SEC"].ToString();
            return c;
        }

        /// <summary>
        /// Obtiene el listado de altas históricas con fines de exportación
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public List<tramitePanel> BlExportarAltasHist(int? IdOrganismo)
        {
            try
            {
                DataTable resultado = p_da.DaExportarAltasHist(IdOrganismo);
                List<tramitePanel> ct = new List<tramitePanel>();

                foreach (DataRow row in resultado.Rows)
                {
                    tramitePanel c = FillTramitePnael(row);
                    /*
                    var c = new tramitePanel();
                    c.Razon_Social = row["RAZON_SOCIAL"].ToString();
                    c.Nro_Sifcos = row["NRO_SIFCOS"].ToString();
                    c.CUIT = row["CUIT"].ToString();
                    c.Nro_tramite = row["NRO_TRAMITE_SIFCOS"].ToString();

                    if (row["FEC_ALTA"] != System.DBNull.Value)
                    {
                        DateTime dParser;
                        bool esDatetime = DateTime.TryParse(row["FEC_ALTA"].ToString(), out dParser);
                        if (esDatetime)
                        {
                            c.fec_alta = dParser;
                        }
                    }*/
                    ct.Add(c);
                }

                return ct;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de raltas históricas para exportar
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public List<tramitePanel> BlExportarBajasHist(int? IdOrganismo)
        {
            try
            {
                DataTable resultado = p_da.DaExportarBajasHist(IdOrganismo);
                List<tramitePanel> ct = new List<tramitePanel>();

                foreach (DataRow row in resultado.Rows)
                {
                    tramitePanel c = FillTramitePnael(row);
                    /*
                    var c = new tramitePanel();
                    c.Razon_Social = row["RAZON_SOCIAL"].ToString();
                    c.Nro_Sifcos = row["NRO_SIFCOS"].ToString();
                    c.CUIT = row["CUIT"].ToString();
                    c.Nro_tramite = row["NRO_TRAMITE_SIFCOS"].ToString();

                    if (row["FEC_ALTA"] != System.DBNull.Value)
                    {
                        DateTime dParser;
                        bool esDatetime = DateTime.TryParse(row["FEC_ALTA"].ToString(), out dParser);
                        if (esDatetime)
                        {
                            c.fec_alta = dParser;
                        }
                    }*/
                    ct.Add(c);
                }

                return ct;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de comerios activos para exportar
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Es nulable para el caso que quiera ver todos</param>
        /// <returns></returns>
        public List<tramitePanel> BlExportarComerciosAct(int? IdOrganismo)
        {
            try
            {
                DataTable resultado = p_da.DaExportarComerciosAct(IdOrganismo);
                List<tramitePanel> ct = new List<tramitePanel>();

                foreach (DataRow row in resultado.Rows)
                {
                    tramitePanel c = FillTramitePnael(row);
                    /*
                    var c = new tramitePanel();
                    c.Razon_Social = row["RAZON_SOCIAL"].ToString();
                    c.Nro_Sifcos = row["NRO_SIFCOS"].ToString();
                    c.CUIT = row["CUIT"].ToString();
                    c.Nro_tramite = row["NRO_TRAMITE_SIFCOS"].ToString();

                    if (row["FEC_VENCIMIENTO"] != System.DBNull.Value)
                    {
                        DateTime dParser;
                        bool esDatetime = DateTime.TryParse(row["FEC_VENCIMIENTO"].ToString(), out dParser);
                        if (esDatetime)
                        {
                            c.fec_vencimiento = dParser;
                        }
                    }*/
                    ct.Add(c);
                }

                return ct;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// Autor: (IB)
        /// </summary>
        /// <param name="pPrefijo"></param>
        /// <returns></returns>
        public List<Rubro> BlGetRubrosComercio(string pPrefijo)
        {
            try
            {
                DataTable dt = p_da.DaGetRubrosComercio(pPrefijo.ToUpper());
                List<Rubro> ListProd = new List<Rubro>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListProd.Add(new Rubro()
                        { IdRubro = dt.Rows[i]["idRubro"].ToString(), NRubro = dt.Rows[i]["nRubro"].ToString() });
                }

                ListProd.Add(new Rubro() { IdRubro = "0", NRubro = "SIN ASIGNAR" });
                return ListProd;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Autor: (IB)
        /// </summary>
        /// <returns>Listado de la combinación rubros actividades</returns>
        public List<RubroActividad> BlGetRubrosActividad()
        {
            try
            {
                DataTable dt = p_da.DaGetRubrosActividad();
                List<RubroActividad> listRubroActividad = new List<RubroActividad>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listRubroActividad.Add(new RubroActividad()
                    {
                        IdRubro = Convert.ToInt32(dt.Rows[i]["id_rubro"]), NRubro = dt.Rows[i]["n_rubro"].ToString(),
                        IdActividadClanae = dt.Rows[i]["id_actividad_clanae"].ToString()
                    });
                }

                //listRubroActividad.Add(new Rubro() { IdRubro = "0", NRubro = "SIN ASIGNAR" });
                return listRubroActividad;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Autor: (IB)
        /// Intenta asociar un rubro a una actividad
        /// No es importante para el proceso si pudo o no hacerlo 
        /// </summary>
        /// <param name="IdRubro"></param>
        /// <param name="IdActividadClanae"></param>
        public void BlSetRubroActividad(int IdRubro, string IdActividadClanae)
        {
            try
            {
                p_da.DaInsertRubroActividad(IdRubro, IdActividadClanae);
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Autor: (IB)
        /// Actualiza el rubro primario y secundario para un trámite
        /// </summary>
        /// <param name="IdRubroPri"></param>
        /// <param name="IdRubroSec"></param>
        /// <param name="pNroTramite"></param>
        /// <returns></returns>
        public bool BlActualizarRubrosPriSec(int? IdRubroPri, int? IdRubroSec, int pNroTramite)
        {
            try
            {
                string Mensaje = p_da.DaActualizarRubrosPriSec(IdRubroPri, IdRubroSec, pNroTramite);

                if (Mensaje == "OK8")
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Autor: (IB)
        /// Indica si un mail ya fue utilizado para un cuit 
        /// distinto al que se envía por parámetro. Devuelve
        /// falso si el email no está libre
        /// </summary>
        /// <param name="Cuit"></param>
        /// <param name="Mail"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public bool BlEmailLibre(string Cuit, string Mail, out string mensaje)
        {
            return p_da.DaEmailLibre(Cuit, Mail, out mensaje);
        }

        public List<ConceptoTasa> BlGetConceptosAFecha(DateTime fecha)
        {
            List<ConceptoTasa> lconceptos = new List<ConceptoTasa>();

            DataTable dt = p_da.DaGetConceptosAFecha(fecha);

            if (dt != null)
            {
                foreach (DataRow rowconcepto in dt.Rows)
                {
                    ConceptoTasa concepto = new ConceptoTasa();
                    concepto.id_concepto = rowconcepto["ID_SIF_HIST_CONCEPTOS"].ToString();
                    concepto.fec_desde = Convert.ToDateTime(rowconcepto["FEC_DESDE"]);
                    concepto.fec_hasta = Convert.ToDateTime(rowconcepto["FEC_HASTA"]);
                    concepto.precio_base = rowconcepto["MONTO"].ToString();
                    concepto.IdTipoConcepto = Convert.ToInt32(rowconcepto["ID_TIPO_CONCEPTO"]);
                    concepto.NTipoConcepto = rowconcepto["N_TIPO_CONCEPTO"].ToString();

                    lconceptos.Add(concepto);
                }
            }


            return lconceptos;
        }

        /// <summary>
        /// Autor: (IB)
        /// Generar Tasa
        /// </summary>
        /// <param name="pCUIT"></param>
        /// <param name="pId_concepto"></param>
        /// <param name="pCod_ente"></param>
        /// <param name="pEmail"></param>
        /// <param name="CuilUsuarioCIDI"></param>
        /// <param name="pCantidad"></param>
        /// <param name="pImporte"></param>
        /// <param name="o_barra1"></param>
        /// <param name="o_barra2"></param>
        /// <param name="o_fecha_venc"></param>
        /// <param name="o_hash_trx"></param>
        /// <param name="o_id_Transaccion"></param>
        /// <param name="o_nro_liq_original"></param>
        /// <returns></returns>
        private String BlGenerarTRS(String pCUIT, Int64 pId_concepto, String pCod_ente, String pEmail,
            String CuilUsuarioCIDI, Int64 pCantidad, float pImporte,
            out string o_barra1, out string o_barra2, out string o_fecha_venc, out string o_hash_trx,
            out string o_id_Transaccion, out string o_nro_liq_original)
        {
            try
            {
                o_id_Transaccion = null;
                var resultado = p_da.DaGenerarTRS(pCUIT, pId_concepto, pCod_ente, pEmail, CuilUsuarioCIDI, pCantidad,
                    pImporte,
                    out o_barra1, out o_barra2, out o_fecha_venc, out o_hash_trx, out o_id_Transaccion,
                    out o_nro_liq_original);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public string BlSolicitarTrs(int IdTipoTramiteTrs, string cuitTramite, string cuitUsuarioCidiLogueado,
            out string oFechaVenc,
            out string oHashTrx, out string oIdTransaccion, out string oNroLiqOriginal, out string oIdConcepto,
            out string oFecDesdeConcepto,
            out string oMonto, out string oConcepto)
        {

            string strMonto = "";
            string strIdConcepto = "";
            //Parametros de salida
            string oBarra1 = "";
            string oBarra2 = "";
            oFechaVenc = "";
            oHashTrx = "";
            oIdTransaccion = "";
            oNroLiqOriginal = "";
            oIdConcepto = "";
            oFecDesdeConcepto = "";
            oMonto = "";
            oConcepto = "";

            if (IdTipoTramiteTrs == 1)
            {
                strMonto = SingletonParametroGeneral.GetInstance().MontoTasaAlta;
                strIdConcepto = SingletonParametroGeneral.GetInstance().IdConceptoTasaAlta;
                oIdConcepto = strIdConcepto;
                oFecDesdeConcepto = SingletonParametroGeneral.GetInstance().FecDesdeConceptoAlta;
                oMonto = strMonto;
                oConcepto = "Tasa de Alta";

            }
            else if (IdTipoTramiteTrs == 4)
            {
                strMonto = SingletonParametroGeneral.GetInstance().MontoTasaReempadronamiento;
                strIdConcepto = SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;
                oIdConcepto = strIdConcepto;
                oFecDesdeConcepto = SingletonParametroGeneral.GetInstance().FecDesdeConceptoReempadronamiento;
                oMonto = strMonto;
                oConcepto = "Tasa de Reepadronamiento";
            }
            else
            {
                return "Tipo de trámite no soportado";
            }

            //Parámetro monto
            float monto = 0;
            float montoOut;
            bool montoOK;
            montoOK = float.TryParse(strMonto, out montoOut);
            if (montoOK)
                monto = montoOut;
            else
                return "No se pudo parsear el monto " + strMonto;

            //Parámetro IdConcepto
            long IdConcepto = 0;
            long IdConceptoOut;
            bool IdConceptoOK;
            IdConceptoOK = long.TryParse(strIdConcepto, out IdConceptoOut);
            if (IdConceptoOK)
                IdConcepto = IdConceptoOut;
            else
                return "No se pudo parsear el IdConcepto " + strIdConcepto;

            string resultado = BlGenerarTRS(cuitTramite, IdConcepto, "057", null, cuitUsuarioCidiLogueado, 0, monto,
                out oBarra1, out oBarra2, out oFechaVenc, out oHashTrx, out oIdTransaccion, out oNroLiqOriginal);
            return resultado;
        }

        #region Liquidaciones

        public List<SifLiquidaciones> BlGetLiquidaciones(
            int? pIdLiquidacion,
            int? pNroSifcosDesde,
            int? pNroSifcosHasta,
            DateTime? pFecDesde,
            DateTime? pFecDesdeHasta,
            DateTime? pFecHasta,
            DateTime? pFecHastaHasta,
            int? pIdTipoTramite,
            string pIdUsuario,
            DateTime? pFecAlta,
            DateTime? pFecAltaHasta,
            string pNroExpediente,
            string pNroResolucion,
            DateTime? pFechaResolucion,
            DateTime? pFechaResolucionHasta,
            out List<Exception> Ex)
        {
            List<SifLiquidaciones> pRet = new List<SifLiquidaciones>();
            Ex = new List<Exception>();
            try
            {
                DataTable resultado = p_da.DaGetLiquidaciones(
                    pIdLiquidacion,
                    pNroSifcosDesde,
                    pNroSifcosHasta,
                    pFecDesde,
                    pFecDesdeHasta,
                    pFecHasta,
                    pFecHastaHasta,
                    pIdTipoTramite,
                    pIdUsuario,
                    pFecAlta,
                    pFecAltaHasta,
                    pNroExpediente,
                    pNroResolucion,
                    pFechaResolucion,
                    pFechaResolucionHasta,
                    out Ex);
                //return null;

                if (Ex.Count > 0)
                {
                    return pRet;
                }

                foreach (DataRow reader in resultado.Rows)
                {
                    SifLiquidaciones obj = new SifLiquidaciones();
                    //Campos de la tabla
                    obj.IdLiquidacion = Convert.ToInt32(reader["ID_LIQUIDACION"]);
                    if (!Convert.IsDBNull(reader["NRO_SIFCOS_DESDE"]))
                        obj.NroSifcosDesde = Convert.ToInt32(reader["NRO_SIFCOS_DESDE"]);
                    if (!Convert.IsDBNull(reader["NRO_SIFCOS_HASTA"]))
                        obj.NroSifcosHasta = Convert.ToInt32(reader["NRO_SIFCOS_HASTA"]);
                    if (!Convert.IsDBNull(reader["FEC_DESDE"])) obj.FecDesde = Convert.ToDateTime(reader["FEC_DESDE"]);
                    if (!Convert.IsDBNull(reader["FEC_HASTA"])) obj.FecHasta = Convert.ToDateTime(reader["FEC_HASTA"]);
                    obj.IdTipoTramite = Convert.ToInt32(reader["ID_TIPO_TRAMITE"]);
                    obj.IdUsuario = Convert.IsDBNull(reader["ID_USUARIO"])
                        ? null
                        : Convert.ToString(reader["ID_USUARIO"]);
                    if (!Convert.IsDBNull(reader["FEC_ALTA"])) obj.FecAlta = Convert.ToDateTime(reader["FEC_ALTA"]);
                    obj.NroExpediente = Convert.IsDBNull(reader["NRO_EXPEDIENTE"])
                        ? null
                        : Convert.ToString(reader["NRO_EXPEDIENTE"]);
                    obj.NroResolucion = Convert.IsDBNull(reader["NRO_RESOLUCION"])
                        ? null
                        : Convert.ToString(reader["NRO_RESOLUCION"]);
                    if (!Convert.IsDBNull(reader["FECHA_RESOLUCION"]))
                        obj.FechaResolucion = Convert.ToDateTime(reader["FECHA_RESOLUCION"]);

                    //N_TIPO_TRAMITE
                    if (!Convert.IsDBNull(reader["N_TIPO_TRAMITE"]))
                        obj.NTipoTramite = reader["N_TIPO_TRAMITE"].ToString();
                    pRet.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Ex.Add(ex);
            }

            return pRet;
        }

        /// <summary>
        /// Trae todas las liquidaciones por tipo de trámite
        /// Sobrecarga para simplificar el llamado en base a filtros frecuentres
        /// </summary>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public List<SifLiquidaciones> BlGetLiquidaciones(int idTipoTramite, int? idLiquidacion, out List<Exception> Ex)
        {
            List<SifLiquidaciones> pRet = new List<SifLiquidaciones>();
            Ex = new List<Exception>();
            return BlGetLiquidaciones(idLiquidacion, null, null, null, null, null, null, idTipoTramite, null, null,
                null, null, null, null, null, out Ex);
        }

        /// <summary>
        /// Trae todas las liquidaciones por Id
        /// Sobrecarga para simplificar el llamado en base a filtros frecuentres
        /// </summary>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public List<SifLiquidaciones> BlGetLiquidaciones(int? idLiquidacion, out List<Exception> Ex)
        {
            List<SifLiquidaciones> pRet = new List<SifLiquidaciones>();
            Ex = new List<Exception>();
            return BlGetLiquidaciones(idLiquidacion, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, out Ex);
        }

        /// <summary>
        /// Liquidaciones agrupadas por organismos
        /// </summary>
        /// <param name="pIdLiqOrganismo"></param>
        /// <param name="pIdOrganismo"></param>
        /// <param name="pIdLiquidacion"></param>
        /// <param name="pTotalLiquidado"></param>
        /// <param name="pCantidad"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public List<SifLiqOrganismos> BlLiqOrganismosGet(
            int? pIdLiqOrganismo,
            int? pIdOrganismo,
            int? pIdLiquidacion,
            decimal? pTotalLiquidado,
            int? pCantidad,
            int? pIdOrganismoSuperior,
            out List<Exception> Ex
        )
        {
            Ex = new List<Exception>();
            List<SifLiqOrganismos> pRet = new List<SifLiqOrganismos>();

            try
            {
                DataTable resultado = p_da.DaLiqOrganismosGet(
                    pIdLiqOrganismo,
                    pIdOrganismo,
                    pIdLiquidacion,
                    pTotalLiquidado,
                    pCantidad,
                    pIdOrganismoSuperior,
                    out Ex);

                if (Ex.Count > 0)
                {
                    return pRet;
                }

                foreach (DataRow reader in resultado.Rows)
                {
                    SifLiqOrganismos obj = new SifLiqOrganismos();
                    //Campos de la tabla
                    obj.IdLiqOrganismo = Convert.ToInt32(reader["ID_LIQ_ORGANISMO"]);
                    obj.IdOrganismo = Convert.ToInt32(reader["ID_ORGANISMO"]);
                    obj.IdLiquidacion = Convert.ToInt32(reader["ID_LIQUIDACION"]);
                    obj.TotalLiquidado = Convert.ToInt32(reader["TOTAL_LIQUIDADO"]);
                    obj.Cantidad = Convert.ToInt32(reader["CANTIDAD"]);
                    if (!Convert.IsDBNull(reader["ID_ORGANISMO_SUPERIOR"]))
                        obj.IdOrganismoSuperior = Convert.ToInt32(reader["ID_ORGANISMO_SUPERIOR"]);

                    //RAZON_SOCIAL
                    if (!Convert.IsDBNull(reader["RAZON_SOCIAL"])) obj.RazonSocial = reader["RAZON_SOCIAL"].ToString();

                    pRet.Add(obj);
                }
            }

            catch (Exception ex)
            {
                Ex.Add(ex);
            }

            return pRet;
        }

        /// <summary>
        /// Liquidaciones agrupadas por organismos
        /// Sobrecarga para simplificar el llamado en base a filtros frecuentres
        /// </summary>
        /// <param name="pIdLiquidacion"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public List<SifLiqOrganismos> BlLiqOrganismosGet(
            int? pIdLiquidacion,
            int? pIdOrganismoSup,
            out List<Exception> Ex
        )
        {
            Ex = new List<Exception>();
            List<SifLiqOrganismos> pRet = new List<SifLiqOrganismos>();

            try
            {
                pRet = BlLiqOrganismosGet(null, null, pIdLiquidacion, null, null, pIdOrganismoSup, out Ex);
            }

            catch (Exception ex)
            {
                Ex.Add(ex);
            }

            return pRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pIdLiquidacion"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public List<SifLiqOrganismos> BlLiqOrganismosSupGet(
            int? pIdLiquidacion,
            out List<Exception> Ex
        )
        {
            Ex = new List<Exception>();
            List<SifLiqOrganismos> pRet = new List<SifLiqOrganismos>();

            try
            {
                DataTable resultado = p_da.DaLiqOrganismosSupGet(
                    pIdLiquidacion,
                    out Ex);

                if (Ex.Count > 0)
                {
                    return pRet;
                }

                foreach (DataRow reader in resultado.Rows)
                {
                    SifLiqOrganismos obj = new SifLiqOrganismos();
                    //Campos de la tabla
                    obj.IdOrganismo = Convert.ToInt32(reader["ID_ORGANISMO"]);
                    obj.IdLiquidacion = Convert.ToInt32(reader["ID_LIQUIDACION"]);
                    obj.TotalLiquidado = Convert.ToInt32(reader["TOTAL_LIQUIDADO"]);
                    obj.Cantidad = Convert.ToInt32(reader["CANTIDAD"]);
                    //RAZON_SOCIAL
                    if (!Convert.IsDBNull(reader["RAZON_SOCIAL"])) obj.RazonSocial = reader["RAZON_SOCIAL"].ToString();

                    pRet.Add(obj);
                }
            }

            catch (Exception ex)
            {
                Ex.Add(ex);
            }

            return pRet;
        }

        public string BlLiqOrganismosInsert(int IdOrganismo, int IdLiquidacion, int TotalLiquidado, int Cantidad)
        {
            return p_da.DaInsertLiqOrganismo(IdOrganismo, IdLiquidacion, TotalLiquidado, Cantidad);
        }

        public bool BlSifLiquidacionesUpdate(SifLiquidaciones obj, out List<Exception> Ex)
        {
            return p_da.SifLiquidacionesUpdate(obj, out Ex);
        }

        /// <summary>
        /// Trae trámites liquidados en base a filtros
        /// </summary>
        /// <param name="pIdLiqTramite"></param>
        /// <param name="pIdLiqOrganismo"></param>
        /// <param name="pNroTramiteSifcos"></param>
        /// <param name="pIdOrganismo"></param>
        /// <param name="pMontoLiquidado"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public List<SifLiqTramites> BlLiqTramitesGet(
            int? pIdLiqTramite,
            int? pIdLiqOrganismo,
            int? pNroTramiteSifcos,
            int? pIdOrganismo,
            int? pIdOrganismoSup,
            int? pIdLiquidacion,
            decimal? pMontoLiquidado,
            out List<Exception> Ex)
        {
            Ex = new List<Exception>();
            List<SifLiqTramites> pRet = new List<SifLiqTramites>();

            try
            {
                DataTable resultado = p_da.DaLiqTramitesGet(
                    pIdLiqTramite,
                    pIdLiqOrganismo,
                    pNroTramiteSifcos,
                    pIdOrganismo,
                    pIdOrganismoSup,
                    pIdLiquidacion,
                    pMontoLiquidado,
                    out Ex);

                if (Ex.Count > 0)
                {
                    return pRet;
                }

                foreach (DataRow reader in resultado.Rows)
                {
                    SifLiqTramites obj = new SifLiqTramites();
                    //Campos de la tabla
                    obj.IdLiqTramite = Convert.ToInt32(reader["ID_LIQ_TRAMITE"]);
                    obj.IdLiqOrganismo = Convert.ToInt32(reader["ID_LIQ_ORGANISMO"]);
                    obj.NroTramiteSifcos = Convert.ToInt32(reader["NRO_TRAMITE_SIFCOS"]);
                    obj.IdOrganismo = Convert.ToInt32(reader["ID_ORGANISMO"]);
                    obj.MontoLiquidado = Convert.ToInt32(reader["MONTO_LIQUIDADO"]);


                    if (!Convert.IsDBNull(reader["CUIT"])) obj.Cuit = reader["CUIT"].ToString();
                    if (!Convert.IsDBNull(reader["NRO_SIFCOS"])) obj.NroSifcos = reader["NRO_SIFCOS"].ToString();
                    if (!Convert.IsDBNull(reader["local"])) obj.Local = reader["local"].ToString();
                    if (!Convert.IsDBNull(reader["RAZON_SOCIAL"])) obj.RazonSocial = reader["RAZON_SOCIAL"].ToString();
                    if (!Convert.IsDBNull(reader["stand"])) obj.Stand = reader["stand"].ToString();

                    if (!Convert.IsDBNull(reader["FEC_INI_TRAMITE"]))
                        obj.FecIniTramite = Convert.ToDateTime(reader["FEC_INI_TRAMITE"]);
                    if (!Convert.IsDBNull(reader["FEC_ALTA"])) obj.FecAlta = Convert.ToDateTime(reader["FEC_ALTA"]);
                    if (!Convert.IsDBNull(reader["FEC_VENCIMIENTO"]))
                        obj.FecVencimiento = Convert.ToDateTime(reader["FEC_VENCIMIENTO"]);

                    if (!Convert.IsDBNull(reader["N_CALLE"])) obj.Calle = reader["N_CALLE"].ToString();
                    if (!Convert.IsDBNull(reader["ALTURA"])) obj.Altura = Convert.ToInt32(reader["ALTURA"]);
                    if (!Convert.IsDBNull(reader["PISO"])) obj.Piso = reader["PISO"].ToString();
                    if (!Convert.IsDBNull(reader["DEPTO"])) obj.Depto = reader["DEPTO"].ToString();
                    if (!Convert.IsDBNull(reader["TORRE"])) obj.Torre = reader["TORRE"].ToString();
                    if (!Convert.IsDBNull(reader["MZNA"])) obj.Mzna = reader["MZNA"].ToString();
                    if (!Convert.IsDBNull(reader["LOTE"])) obj.Lote = reader["LOTE"].ToString();
                    if (!Convert.IsDBNull(reader["N_LOCALIDAD"])) obj.Localidad = reader["N_LOCALIDAD"].ToString();
                    if (!Convert.IsDBNull(reader["cpa"])) obj.Cpa = reader["cpa"].ToString();
                    if (!Convert.IsDBNull(reader["BOCA"])) obj.Boca = reader["BOCA"].ToString();
                    if (!Convert.IsDBNull(reader["CUIT_BOCA"])) obj.CuitBoca = reader["CUIT_BOCA"].ToString();
                    if (!Convert.IsDBNull(reader["BOCA_SUP"])) obj.BocaSuperior = reader["BOCA_SUP"].ToString();
                    if (!Convert.IsDBNull(reader["CUIT_BOCA_SUP"]))
                        obj.CuitBocaSuperior = reader["CUIT_BOCA_SUP"].ToString();
                    if (!Convert.IsDBNull(reader["NROLIQUIDACIONORIGINAL"]))
                        obj.NroLiquidacionOriginal = reader["NROLIQUIDACIONORIGINAL"].ToString();

                    pRet.Add(obj);
                }
            }

            catch (Exception ex)
            {
                Ex.Add(ex);
            }

            return pRet;
        }

        /// <summary>
        /// Trae trámites liquidados en base a filtros 
        /// Sobrecarga para simplificar el llamado en base a filtros frecuentres
        /// </summary>
        /// <param name="pIdLiqOrganismo"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public List<SifLiqTramites> BlLiqTramitesGet(
            int? pIdLiqOrganismo,
            int? pIdOrganismoSup,
            int? pIdLiquidacion,
            out List<Exception> Ex)
        {
            Ex = new List<Exception>();
            List<SifLiqTramites> pRet = new List<SifLiqTramites>();
            return BlLiqTramitesGet(null, pIdLiqOrganismo, null, null, pIdOrganismoSup, pIdLiquidacion, null, out Ex);
        }

        public string BlLiqTramitesInsert(int IdLiqOrganismo, int NroTramiteSifcos, int TotalLiquidado, int IdOrganismo)
        {
            return p_da.DaInsertLiqTramites(IdLiqOrganismo, NroTramiteSifcos, TotalLiquidado, IdOrganismo);
        }

        /// <summary>
        /// Obtiene el Nro Sifcos Hasta de la última liquidación realizada
        /// </summary>
        /// <returns></returns>
        public int BlLiquidacionesAltasGetUltima()
        {
            return p_da.DaLiquidacionesAltasObtenerUltima();
        }

        public int BlGenerarLiquidacionReempa(DateTime pFecHasta, string pIdUsuario, out String pMensaje)
        {
            return p_da.DaGenerarLiquidacionReempa(pFecHasta, pIdUsuario, out pMensaje);
        }

        public int BlGenerarLiquidacionAlta(int pNroSifcosDesde, int pNroSifcosHasta, string pIdUsuario,
            out String pMensaje)
        {
            //Validamos que se puedan liquidar todos los nro sifcos del rango
            bool liquidable = BlValidarLiqAlta(pNroSifcosDesde, pNroSifcosHasta, out pMensaje);
            if (!liquidable) return 0;

            //Liquidamos las altas
            return p_da.DaGenerarLiquidacionAlta(pNroSifcosDesde, pNroSifcosHasta, pIdUsuario, out pMensaje);
        }

        public bool BlValidarLiqAlta(int pNroSifcosDesde, int pNroSifcosHasta, out string pMensaje)
        {

            pMensaje = "";
            List<Exception> Ex = new List<Exception>();
            DataTable dt = p_da.DaLiqAltasPreviewGet(pNroSifcosDesde, pNroSifcosHasta, out Ex);
            if (dt.Rows.Count == 0)
            {
                pMensaje = "No se encontraron registros para liquidar";
                return false;
            }

            List<int> lNroSifcos = new List<int>();

            foreach (DataRow dr in dt.Rows)
            {
                lNroSifcos.Add(Convert.ToInt32(dr["NRO_SIFCOS"].ToString()));
            }

            //Armar una lista de ids

            List<int> faltantes = AnalizarRango(lNroSifcos, pNroSifcosDesde, pNroSifcosHasta);

            if (faltantes.Count == 0) return true;
            else
            {
                pMensaje =
                    "No es posible liquidar porque los siguientes números de sifcos no están disponibles para ser liquidados: " +
                    string.Join<int>(",", faltantes);
                ;
                return false;
            }
        }

        /// <summary>
        /// Devuelve un listado de faltantes
        /// </summary>
        /// <param name="rango"></param>
        /// <param name="minimo"></param>
        /// <param name="maximo"></param>
        /// <returns></returns>
        public List<int> AnalizarRango(List<int> rango, int minimo, int maximo)
        {
            rango = rango.OrderBy(x => x).ToList();
            return Enumerable.Range(minimo, maximo - minimo + 1).Except(rango).ToList();
        }

        public bool LiquidacionGrabarDatosExtras(int IdLiquidacion, string nroResolucion, DateTime? fechaResolucion,
            string nroExpediente, out List<Exception> Ex)
        {
            Ex = new List<Exception>();
            List<SifLiquidaciones> lliq = BlGetLiquidaciones(IdLiquidacion, out Ex);

            if (lliq.Count < 1)
            {
                Ex.Add(new Exception("No se encontró la liquidación código: " + IdLiquidacion.ToString()));
                return false;
            }

            SifLiquidaciones liq = lliq[0];

            liq.NroExpediente = nroExpediente;
            liq.NroResolucion = nroResolucion;
            liq.FechaResolucion = fechaResolucion;

            return BlSifLiquidacionesUpdate(liq, out Ex);

        }

        /// <summary>
        /// AUTOR: IB
        /// Indica si un id liquidación es la última generada de su tipo
        /// </summary>
        /// <param name="IdLiquidacion"></param>
        /// <param name="?"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public bool EsLaUltima(int IdLiquidacion, out List<Exception> Ex)
        {
            Ex = new List<Exception>();
            int ultimaLiquidacion = p_da.DaLiquidacionesObtenerUltima(IdLiquidacion, out Ex);

            if (Ex.Count > 0) return false;

            if (ultimaLiquidacion != IdLiquidacion) return false;
            return true;
        }

        /// <summary>
        /// AUTOR: IB
        /// Borra una liquidación validando que sea la última
        /// </summary>
        /// <param name="IdLiquidacion"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public bool BorrarLiquidacion(int IdLiquidacion, out List<Exception> Ex)
        {
            //Averiguo si es la última
            Ex = new List<Exception>();

            bool ultima = EsLaUltima(IdLiquidacion, out Ex);

            //Si no es la última de su tipo retorno con error
            if (!ultima) return false;

            //Borrado real de la liquidacion
            bool borrado = p_da.BorrarLiquidacionCompleta(IdLiquidacion, out Ex);

            return borrado;
        }

        #endregion

        #region Relevamientos

        public List<SifRelevamientos> BlGetRelevamientos(
            int? pIdRelevamiento,
            string pNRelevamiento,
            string pCodigo,
            string pNroSifcos,
            string pUsrAlta,
            DateTime? pFecAlta,
            DateTime? pFecAltaHasta,
            out List<Exception> Ex)
        {
            List<SifRelevamientos> pRet = new List<SifRelevamientos>();
            Ex = new List<Exception>();
            try
            {
                DataTable resultado = p_da.DaGetRelevamientos(
                    pIdRelevamiento,
                    pNRelevamiento,
                    pCodigo,
                    pNroSifcos,
                    pUsrAlta,
                    pFecAlta,
                    pFecAltaHasta,
                    out Ex);
                //return null;

                if (Ex.Count > 0)
                {
                    return pRet;
                }

                foreach (DataRow reader in resultado.Rows)
                {
                    SifRelevamientos obj = new SifRelevamientos();
                    //Campos de la tabla
                    obj.IdRelevamiento = Convert.ToInt32(reader["ID_RELEVAMIENTO"]);
                    obj.NRelevamiento = Convert.IsDBNull(reader["N_RELEVAMIENTO"])
                        ? null
                        : Convert.ToString(reader["N_RELEVAMIENTO"]);
                    obj.Codigo = Convert.IsDBNull(reader["CODIGO"]) ? null : Convert.ToString(reader["CODIGO"]);
                    obj.Observaciones = Convert.IsDBNull(reader["OBSERVACIONES"])
                        ? null
                        : Convert.ToString(reader["OBSERVACIONES"]);
                    obj.UsrAlta = Convert.IsDBNull(reader["USR_ALTA"]) ? null : Convert.ToString(reader["USR_ALTA"]);
                    if (!Convert.IsDBNull(reader["FEC_ALTA"])) obj.FecAlta = Convert.ToDateTime(reader["FEC_ALTA"]);
                    obj.UsrModif = Convert.IsDBNull(reader["USR_MODIF"]) ? null : Convert.ToString(reader["USR_MODIF"]);
                    if (!Convert.IsDBNull(reader["FEC_MODIF"])) obj.FecModif = Convert.ToDateTime(reader["FEC_MODIF"]);

                    pRet.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Ex.Add(ex);
            }

            return pRet;
        }

        /// <summary>
        /// Trae todos los relevamientos por codigo
        /// Sobrecarga para simplificar el llamado en base a filtros frecuentres
        /// </summary>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public List<SifRelevamientos> BlGetRelevamientos(string pCodigo, out List<Exception> Ex)
        {
            List<SifRelevamientos> pRet = new List<SifRelevamientos>();
            Ex = new List<Exception>();
            return BlGetRelevamientos(null, null, pCodigo, null, null, null, null, out Ex);
        }

        /// <summary>
        /// Trae todos los relevamientos por codigo de producto, nombre del producto, y/o nro de sifcos
        /// Sobrecarga para simplificar el llamado en base a filtros frecuentres
        /// </summary>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public List<SifRelevamientos> BlGetRelevamientos(string pCodigo, string pNProd, string pNroSifcos,
            out List<Exception> Ex)
        {
            List<SifRelevamientos> pRet = new List<SifRelevamientos>();
            Ex = new List<Exception>();
            return BlGetRelevamientos(null, pNProd, pCodigo, pNroSifcos, null, null, null, out Ex);
        }

        public List<SifRelevDetalle> BlGetRelevDetalle(
            int? pIdRelevDetalle,
            int? pIdRelevamiento,
            string pCodigo,
            int? pIdEntidad,
            int? pNroSifcos,
            int? pVigente,
            string pUsrAlta,
            DateTime? pFecAlta,
            DateTime? pFecAltaHasta,
            out List<Exception> Ex)
        {
            List<SifRelevDetalle> pRet = new List<SifRelevDetalle>();
            Ex = new List<Exception>();
            try
            {
                DataTable resultado = p_da.DaGetRelevDetalle(
                    pIdRelevDetalle,
                    pIdRelevamiento,
                    pCodigo,
                    pIdEntidad,
                    pNroSifcos,
                    pVigente,
                    pUsrAlta,
                    pFecAlta,
                    pFecAltaHasta,
                    out Ex);
                //return null;

                if (Ex.Count > 0)
                {
                    return pRet;
                }

                foreach (DataRow reader in resultado.Rows)
                {
                    SifRelevDetalle obj = new SifRelevDetalle();
                    //Campos de la tabla
                    obj.IdRelevDetalle = Convert.ToInt32(reader["ID_RELEV_DETALLE"]);
                    obj.IdRelevamiento = Convert.ToInt32(reader["ID_RELEVAMIENTO"]);
                    obj.NRelevamiento = Convert.IsDBNull(reader["N_RELEVAMIENTO"])
                        ? null
                        : Convert.ToString(reader["N_RELEVAMIENTO"]);
                    obj.IdEntidad = Convert.ToInt32(reader["ID_ENTIDAD"]);
                    obj.NEntidad = Convert.IsDBNull(reader["N_ENTIDAD"]) ? null : Convert.ToString(reader["N_ENTIDAD"]);
                    obj.NroSifcos = Convert.IsDBNull(reader["NRO_SIFCOS"])
                        ? null
                        : (int?)Convert.ToInt32(reader["NRO_SIFCOS"]);
                    obj.Codigo = Convert.IsDBNull(reader["CODIGO"]) ? null : Convert.ToString(reader["CODIGO"]);
                    obj.Precio = Convert.ToDecimal(reader["PRECIO"]);
                    if (!Convert.IsDBNull(reader["FEC_RELEVAMIENTO"]))
                        obj.FecRelevamiento = Convert.ToDateTime(reader["FEC_RELEVAMIENTO"]);
                    obj.Vigente = Convert.ToInt32(reader["VIGENTE"]);

                    obj.Observaciones = Convert.IsDBNull(reader["OBSERVACIONES"])
                        ? null
                        : Convert.ToString(reader["OBSERVACIONES"]);
                    obj.UsrAlta = Convert.IsDBNull(reader["USR_ALTA"]) ? null : Convert.ToString(reader["USR_ALTA"]);
                    if (!Convert.IsDBNull(reader["FEC_ALTA"])) obj.FecAlta = Convert.ToDateTime(reader["FEC_ALTA"]);
                    obj.UsrModif = Convert.IsDBNull(reader["USR_MODIF"]) ? null : Convert.ToString(reader["USR_MODIF"]);
                    if (!Convert.IsDBNull(reader["FEC_MODIF"])) obj.FecModif = Convert.ToDateTime(reader["FEC_MODIF"]);

                    pRet.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Ex.Add(ex);
            }

            return pRet;
        }

        /// <summary>
        /// Trae todos los detalles de un relevamiento relevamientos por idrelevamiento
        /// Sobrecarga para simplificar el llamado en base a filtros frecuentres
        /// </summary>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public List<SifRelevDetalle> BlGetRelevDetalle(int? pIdRelevamiento, out List<Exception> Ex)
        {
            List<SifRelevDetalle> pRet = new List<SifRelevDetalle>();
            Ex = new List<Exception>();
            return BlGetRelevDetalle(null, pIdRelevamiento, null, null, null, null, null, null, null, out Ex);
        }

        public bool RelevamientoGrabar(SifRelevamientos obj, out List<Exception> Ex)
        {
            Ex = new List<Exception>();

            if (obj.IdRelevamiento == 0)
            {
                return p_da.SifRelevamientoInsert(obj, out Ex);
            }
            else
            {
                return p_da.SifRelevamientoUpdate(obj, out Ex);
            }

        }

        public bool RelevDetalleGrabar(SifRelevDetalle obj, out List<Exception> Ex)
        {
            Ex = new List<Exception>();

            //Busco la ENTIDAD
            try
            {
                DataTable resultadoSifcos = p_da.DaGetEntidades(null, obj.NroSifcos, out Ex);
                if (Ex.Count > 0)
                {
                    return false;
                }

                if (resultadoSifcos.Rows.Count == 0)
                {
                    Ex.Add(new Exception("No se encuentra el nro de sifcos indicado"));
                    return false;
                }

                foreach (DataRow reader in resultadoSifcos.Rows)
                {
                    //busco el primer registro:
                    obj.IdEntidad = Convert.ToInt32(reader["ID_ENTIDAD"]);
                    obj.NEntidad = Convert.IsDBNull(reader["RAZON_SOCIAL"])
                        ? null
                        : Convert.ToString(reader["RAZON_SOCIAL"]);

                    break;
                }
            }
            catch (Exception ex)
            {
                Ex.Add(ex);
            }

            if (obj.IdEntidad == 0)
            {
                Ex.Add(new Exception("No se encuentra la entidad"));
                return false;
            }

            if (obj.IdRelevDetalle == 0)
            {
                return p_da.SifRelevDetalleInsert(obj, out Ex);
            }
            else
            {
                return p_da.SifRelevDetalleUpdate(obj, out Ex);
            }
        }

        public int? BuscarEntidad(int? pNroSifcos, out List<Exception> Ex)
        {
            Ex = new List<Exception>();
            int retIdEntidad = 0;
            //Busco la ENTIDAD
            try
            {
                DataTable resultadoSifcos = p_da.DaGetEntidades(null, pNroSifcos, out Ex);
                if (Ex.Count > 0)
                {
                    return null;
                }

                if (resultadoSifcos.Rows.Count == 0)
                {
                    Ex.Add(new Exception("No se encuentra el nro de sifcos indicado"));
                    return null;
                }

                foreach (DataRow reader in resultadoSifcos.Rows)
                {
                    //busco el primer registro:
                    retIdEntidad = Convert.ToInt32(reader["ID_ENTIDAD"]);
                    break;
                }
            }
            catch (Exception ex)
            {
                Ex.Add(ex);
            }

            if (retIdEntidad == 0)
            {
                Ex.Add(new Exception("No se encuentra la entidad"));
                return null;
            }

            return retIdEntidad;
        }

        #endregion

        /// <summary>
        /// Devuelve un listado con IDs de comercios activos filtrado por localidad y rubro. 
        /// </summary>
        /// <param name="idLocalidad"></param>
        /// <param name="idRubro">id de Rubro agrupados</param>
        /// <returns></returns>
        public List<string> GetIdsEntidad(string idLocalidad, string idRubro)
        {
            DataTable dt = p_da.DaGetIdsEntidad(idLocalidad, idRubro);
            List<string> lst = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(row["ID_ENTIDAD"].ToString());
            }

            return lst;
        }

        //--lt
        public int blGetIdDocumentoCDD(int pNroTramite, int opcion)
        {
            //var result = new ResultadoRule();

            try
            {

                var dtDocumentoCDD = p_da.DaGetIdDocumentoCDD(pNroTramite);
                if (dtDocumentoCDD.Rows.Count > 0)
                {
                    if (opcion == 1)
                    {
                        if (dtDocumentoCDD.Rows[0]["ID_DOCUMENTO1_CDD"] != null)
                            return int.Parse(dtDocumentoCDD.Rows[0]["ID_DOCUMENTO1_CDD"].ToString());
                    }

                    if (opcion == 2)
                    {
                        if (dtDocumentoCDD.Rows[0]["ID_DOCUMENTO2_CDD"] != null)
                            return int.Parse(dtDocumentoCDD.Rows[0]["ID_DOCUMENTO2_CDD"].ToString());
                    }

                    if (opcion == 3)
                    {
                        if (dtDocumentoCDD.Rows[0]["ID_DOCUMENTO3_CDD"] != null)
                            return int.Parse(dtDocumentoCDD.Rows[0]["ID_DOCUMENTO3_CDD"].ToString());
                    }

                    if (opcion == 4)
                    {
                        if (dtDocumentoCDD.Rows[0]["ID_DOCUMENTO4_CDD"] != null)
                            return int.Parse(dtDocumentoCDD.Rows[0]["ID_DOCUMENTO4_CDD"].ToString());
                    }
                }

            }

            catch (Exception e)
            {
                //result.OcurrioError = true;
                //result.MensajeError = e.Message;
            }

            return 0;
        }

        public void blActualizar_ID_DOCUMENTO_CDD_1(InscripcionSifcosDto Obj)
        {
            try
            {
                p_da.DAUpdate_ID_DOCUMENTO_CDD_1(Obj);

            }

            catch (Exception e)
            {

            }

        }

        public void blActualizar_ID_DOCUMENTO_CDD_2(InscripcionSifcosDto Obj)
        {
            try
            {
                p_da.DAUpdate_ID_DOCUMENTO_CDD_2(Obj);

            }

            catch (Exception e)
            {

            }

        }

        public DataTable BlGetTramitesSifcosViejo(string Cuit, string nroSifcos, string nroTramite)
        {
            try
            {
                DataTable dt = p_da.DAGetTramitesSifcosViejo(Cuit, nroSifcos, nroTramite);
                return dt;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }

        }

        public DataTable BlGetBocas()
        {
            throw new NotImplementedException();
        }

        public String BlEliminarTramite(string pNroTramite)
        {
            try
            {
                string resultado = p_da.DaEliminarTramite(pNroTramite);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlEliminarNotificaciones()
        {
            try
            {
                string resultado = p_da.DaEliminarNotificaciones();
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlAsignarNroSifcos(string pNroTramite, string pNroSifcos, string pCuilUsuarioCidi)
        {
            try
            {
                string resultado = p_da.DaAsignarNroSifcos(pNroTramite, pNroSifcos, pCuilUsuarioCidi);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlAsignarResponsable(string pCuitEmpresa, string pCuilResponsable, string pIdRol)
        {
            try
            {
                string resultado = p_da.DaAsignarResponsable(pCuitEmpresa, pCuilResponsable, pIdRol);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String BlEliminarResponsable(string pCuitEmpresa, string pCuilResponsable)
        {
            try
            {
                string resultado = p_da.DaEliminarResponsable(pCuitEmpresa, pCuilResponsable);
                return resultado;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string BlGetIdEntidad(string nroTramite)
        {
            DataTable dt = p_da.DAGetIdEntidad(nroTramite);
            string idEntidad = "0";
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    idEntidad = row["id_entidad"].ToString();
                }
            }

            return idEntidad;
        }

        public List<Contacto> BlConsultaContactosEstab(String id_domicilio_estab, out ResultadoRule result)
        {
            var dt = p_da.DAConsultaContactosEstablecimiento(id_domicilio_estab, out result);
            var lstContacto = new List<Contacto>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lstContacto.Add(new Contacto
                    {
                        IdTipoComunicacion = row["ID_TIPO_COMUNICACION"].ToString(),
                        NTipoComunicacion = row["N_TIPO_COMUNICACION"].ToString(),
                        NroMail = row["NRO_MAIL"].ToString(),
                        CodArea = row["COD_AREA"].ToString(),
                        TablaOrigen = row["TABLA_ORIGEN"].ToString(),


                    });
                }
            }
            else
            {
                return null;
            }


            return lstContacto;
        }

        public List<InscripcionEvento> BlConsultaInscripcionByNro(String pNroInscripcion)
        {
            try
            {
                DataTable dt = p_da.DaConsultaInscripcionByNro(pNroInscripcion);
                List<InscripcionEvento> ListTramites = new List<InscripcionEvento>();
                foreach (DataRow row in dt.Rows)
                {
                    ListTramites.Add(new InscripcionEvento()
                    {
                        NroInscripcion = row["ID_ENTIDAD"].ToString(),
                        CUIT = row["NRO_DGR"].ToString(),
                        Razon_Social = row["RAZON_SOCIAL"].ToString(),
                        Apellido = row["NOMBRE_FANTASIA"].ToString(),
                        CUIL = row["CUIT_PERS_JURIDICA"].ToString(),
                        Estado = row["observaciones"].ToString()

                    });
                }

                return ListTramites;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }

        }

        public ResultadoRule BlRegistrarInvitacion(InscripcionEvento Usu)
        {
            var res = new ResultadoRule();
            try
            {
                var result = "";
                if (Usu.VIP == "SI")
                {
                    result = p_da.DaRegistrarInvitacionVIP(Usu);
                }
                else
                {
                    result = p_da.DaRegistrarInvitacion(Usu);
                }

                if (result != "OK")
                {
                    res.OcurrioError = true;
                    res.MensajeError = result;

                }
                else
                {
                    res.OcurrioError = false;
                    res.MensajeExito = "Se registró con éxito.";
                }

            }

            catch (Exception e)
            {
                res.MensajeError = e.Message;
                res.OcurrioError = true;
                return res;
            }


            return res;
        }

        public ResultadoRule BlRegistrarAccesoEvento(InscripcionEvento Usu)
        {
            var res = new ResultadoRule();
            try
            {
                var result = p_da.DaRegistrarAccesoEvento(Usu);
                if (result != "OK")
                {
                    res.OcurrioError = true;
                    res.MensajeError = result;

                }
                else
                {
                    res.OcurrioError = false;
                    res.MensajeExito = "Se registró con éxito.";
                }
            }

            catch (Exception e)
            {
                res.MensajeError = e.Message;
                res.OcurrioError = true;
                return res;
            }


            return res;
        }

        public ResultadoRule BlRegistrarAccesoVIP(InscripcionEvento Usu)
        {
            var res = new ResultadoRule();
            try
            {

                {
                    var result = p_da.DaRegistrarAccesoVIP(Usu);
                    if (result != "OK")
                    {
                        res.OcurrioError = true;
                        res.MensajeError = result;

                    }
                    else
                    {
                        res.OcurrioError = false;
                        res.MensajeExito = "Se registró con éxito.";
                    }
                }

            }

            catch (Exception e)
            {
                res.MensajeError = e.Message;
                res.OcurrioError = true;
                return res;
            }


            return res;
        }

        public String BlGetUltimoNroInscripcion()
        {
            String NroInscripcion = "";
            DataTable dt = new DataTable();
            try
            {
                dt = p_da.DaGetUltimoNroInscripcion();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        NroInscripcion = row["Nro_Inscripcion"].ToString();
                    }

                    return NroInscripcion;
                }
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }

            return NroInscripcion;
        }

        public DataTable BlGetInscripcionByCUIL(String pCUIL)
        {
            try
            {
                var Inscripcion = p_da.DaGetInscripcionByCUIL(pCUIL);


                return Inscripcion;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }

            return null;
        }

        public List<PersonaJuridica> BlGetPersonaJuridica_byCUIT(string cuit)
        {
            try
            {
                var lista = p_da.DaGetPersonaJuridica(cuit);
                return lista;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<PersonaJuridica> BlGetRazonSocial(String pPrefijo)
        {
            try
            {
                DataTable dt = p_da.DaGetRazonSocial(pPrefijo);
                List<PersonaJuridica> Listcliente = new List<PersonaJuridica>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Listcliente.Add(new PersonaJuridica()
                    {
                        Cuit = dt.Rows[i]["cuit"].ToString(),
                        RazonSocial = dt.Rows[i]["razon_social"].ToString()
                    });
                }

                Listcliente.Add(new PersonaJuridica() { Cuit = "NO EXISTE", RazonSocial = "NO EXISTE" });
                return Listcliente;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public PersonaFisica BlGetPersonasRcivil_CUIL(String pCUIL)
        {
            try
            {
                DataTable dt = p_da.DaGetPersonasRcivil_CUIL(pCUIL);
                PersonaFisica PerFisica = new PersonaFisica();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        PerFisica.NroOtroDocumento = row["CUIL"].ToString();
                        PerFisica.Apellido = row["APELLIDO"].ToString();
                        PerFisica.Nombre = row["NOMBRE"].ToString();
                        PerFisica.FecNacimiento = DateTime.Parse(row["FEC_NACIMIENTO"].ToString());
                        PerFisica.IdTipoDocumento = row["ID_TIPO_DOCUMENTO"].ToString();
                        PerFisica.IdNumero = int.Parse(row["ID_NUMERO"].ToString());
                        PerFisica.IdSexo = row["ID_SEXO"].ToString();
                        PerFisica.NroDocumento = row["NRO_DOCUMENTO"].ToString();
                        PerFisica.PaiCodPais = row["PAI_COD_PAIS"].ToString();
                        PerFisica.PaiCodPaisOrigen = row["PAI_COD_PAIS_ORIGEN"].ToString();

                    }
                }

                return PerFisica;
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<InscripcionEvento> BlGetInscriptosDiaComercioPresentes()
        {
            try
            {
                List<InscripcionEvento> ListConsulta = new List<InscripcionEvento>();
                DataTable dt = p_da.DaGetInscriptosDiaComercioPresentes();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i]["CUIT"].ToString()))
                        {
                            ListConsulta.Add(new InscripcionEvento()
                            {
                                NroInscripcion = dt.Rows[i]["nro_inscripcion"].ToString(),
                                CUIL = dt.Rows[i]["CUIL"].ToString(),
                                Nombre = dt.Rows[i]["NOMBRE"].ToString(),
                                Estado = dt.Rows[i]["ESTADO"].ToString(),
                                VIP = dt.Rows[i]["TIPO_ENTRADA"].ToString()
                            });

                        }
                        else
                        {
                            var dtEmpresa = p_da.BlGetEmpresaEnRentas(dt.Rows[i]["CUIT"].ToString());
                            ListConsulta.Add(new InscripcionEvento()
                            {
                                NroInscripcion = dt.Rows[i]["nro_inscripcion"].ToString(),
                                CUIL = dt.Rows[i]["CUIL"].ToString(),
                                Nombre = dt.Rows[i]["NOMBRE"].ToString(),
                                CUIT = dt.Rows[i]["CUIT"].ToString(),
                                Razon_Social = dtEmpresa.Rows[0]["RAZON_SOCIAL"].ToString(),
                                Estado = dt.Rows[i]["ESTADO"].ToString(),
                                VIP = dt.Rows[i]["TIPO_ENTRADA"].ToString()
                            });
                        }

                    }

                    return ListConsulta;

                }
                else
                {
                    return null;
                }

            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<InscripcionEvento> BlGetInscriptosDiaComercioAusentes()
        {
            try
            {
                List<InscripcionEvento> ListConsulta = new List<InscripcionEvento>();
                DataTable dt = p_da.DaGetInscriptosDiaComercioAusentes();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i]["CUIT"].ToString()))
                        {
                            ListConsulta.Add(new InscripcionEvento()
                            {
                                NroInscripcion = dt.Rows[i]["nro_inscripcion"].ToString(),
                                CUIL = dt.Rows[i]["CUIL"].ToString(),
                                Nombre = dt.Rows[i]["NOMBRE"].ToString(),
                                Estado = dt.Rows[i]["ESTADO"].ToString(),
                                VIP = dt.Rows[i]["TIPO_ENTRADA"].ToString(),
                                Telefono = dt.Rows[i]["CONTACTO"].ToString()
                            });

                        }
                        else
                        {
                            var dtEmpresa = p_da.BlGetEmpresaEnRentas(dt.Rows[i]["CUIT"].ToString());
                            ListConsulta.Add(new InscripcionEvento()
                            {
                                NroInscripcion = dt.Rows[i]["nro_inscripcion"].ToString(),
                                CUIL = dt.Rows[i]["CUIL"].ToString(),
                                Nombre = dt.Rows[i]["NOMBRE"].ToString(),
                                CUIT = dt.Rows[i]["CUIT"].ToString(),
                                Razon_Social = dtEmpresa.Rows[0]["RAZON_SOCIAL"].ToString(),
                                Estado = dt.Rows[i]["ESTADO"].ToString(),
                                VIP = dt.Rows[i]["TIPO_ENTRADA"].ToString(),
                                Telefono = dt.Rows[i]["CONTACTO"].ToString()
                            });
                        }

                    }

                    return ListConsulta;

                }
                else
                {
                    return null;
                }

            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<PlanillaAsistenciaDTO> BlGetInscriptosExcel()
        {
            try
            {
                List<PlanillaAsistenciaDTO> ListConsulta = new List<PlanillaAsistenciaDTO>();
                DataTable dt = p_da.DaGetInscriptosExcel();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i]["CUIT"].ToString()))
                        {
                            ListConsulta.Add(new PlanillaAsistenciaDTO()
                            {
                                Nro_tramite = dt.Rows[i]["nro_inscripcion"].ToString(),
                                dni_rep_legal = dt.Rows[i]["CUIL"].ToString(),
                                Nombre_fantasia = dt.Rows[i]["NOMBRE"].ToString(),
                                estado_tramite = dt.Rows[i]["ESTADO"].ToString(),
                                tipo_tramite = dt.Rows[i]["TIPO_ENTRADA"].ToString(),
                                celular = dt.Rows[i]["CONTACTO"].ToString(),
                                Email = dt.Rows[i]["EMAIL"].ToString()

                            });

                        }
                        else
                        {
                            var dtEmpresa = p_da.BlGetEmpresaEnRentas(dt.Rows[i]["CUIT"].ToString());
                            ListConsulta.Add(new PlanillaAsistenciaDTO()
                            {
                                Nro_tramite = dt.Rows[i]["nro_inscripcion"].ToString(),
                                dni_rep_legal = dt.Rows[i]["CUIL"].ToString(),
                                Nombre_fantasia = dt.Rows[i]["NOMBRE"].ToString(),
                                CUIT = dt.Rows[i]["CUIT"].ToString(),
                                Razon_Social = dtEmpresa.Rows[0]["RAZON_SOCIAL"].ToString(),
                                estado_tramite = dt.Rows[i]["ESTADO"].ToString(),
                                tipo_tramite = dt.Rows[i]["TIPO_ENTRADA"].ToString(),
                                celular = dt.Rows[i]["CONTACTO"].ToString(),
                                Email = dt.Rows[i]["EMAIL"].ToString()
                            });
                        }

                    }

                    return ListConsulta;

                }
                else
                {
                    return null;
                }

            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DataTable BlConsultaSQL(String SQL, String BD, out ResultadoRule Res)
        {
            return p_da.DaConsultaSQL(SQL, BD, out Res);
        }
        public String BlGetDestinatario(String Cuit)
        {
            String Destinatario = string.Empty;
            DataTable dt = p_da.DaGetCUILRepLegal(Cuit);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Destinatario = row["cuil"].ToString();
                }
            }
            return Destinatario;
            
        }

        public String BlGetCuilGestor(String Cuit)
        {
            String CuilGestor = string.Empty;
            DataTable dt = p_da.DaGetCUILGestor(Cuit);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CuilGestor = row["cuil"].ToString();
                }
            }
            return CuilGestor;

        }

        public String BlgetNotificado(String Id_Entidad)
        {
            String Notificado = string.Empty;
            DataTable dt = p_da.DaGetVerificarNotificacion(Id_Entidad);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Notificado = row["notificado"].ToString();
                }
            }
            return Notificado;
        }
        public ResultadoRule BlRegistrarNotificacionCIDI(String IdEntidad)
        {
            var res = new ResultadoRule();
            try
            {

                {
                    var result = p_da.DaRegistrarNotificacionCIDI(IdEntidad);
                    if (result != "OK")
                    {
                        res.OcurrioError = true;
                        res.MensajeError = result;

                    }
                    else
                    {
                        res.OcurrioError = false;
                        res.MensajeExito = "Se registró con éxito.";
                    }
                }

            }

            catch (Exception e)
            {
                res.MensajeError = e.Message;
                res.OcurrioError = true;
                return res;
            }


            return res;
        }

        public String BlGetFechaVtoTramiteAltaSinNroSifcos(String Id_Entidad)
        {
            String FecVto = string.Empty;
            DataTable dt = p_da.DaGetFechaVtoTramiteAltaSinNroSifcos(Id_Entidad);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FecVto = row["vto_tramite_alta"].ToString();
                }
            }
            return FecVto;
        }

        
    }
}