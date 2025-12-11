using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AppComunicacion.ApiModels;
using DA_SIFCOS.Entidades;
using Oracle.ManagedDataAccess.Client;

namespace DA_SIFCOS
{
    public class DataAccessMethods: daBase
    {
        public DataAccessMethods()
            : base()
        {
        }
        public DataTable DaGetEmpresa(String pCuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_empresa ", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pCUIT", OracleDbType.Varchar2, 12);
                if (pCuit != null)
                    com.Parameters["pCUIT"].Value = pCuit;
                else
                    com.Parameters["pCUIT"].Value = DBNull.Value;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetDomEmpresa(String pCuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Dom_Empresa", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pCUIT", OracleDbType.Varchar2, 11);
                if (pCuit != null)
                    com.Parameters["pCUIT"].Value = pCuit;
                else
                    com.Parameters["pCUIT"].Value = DBNull.Value;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetDomEmpresaByIdVin(String pIdVin)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Dom_Empresa_by_idvin", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pidvin", OracleDbType.Int64, 20);
                if (Int64.Parse(pIdVin) != 0)
                    com.Parameters["pidvin"].Value = Int64.Parse(pIdVin);
                else
                    com.Parameters["pidvin"].Value = DBNull.Value;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Devuelve un listado de productos asociados a un tramite.
        /// </summary>
        /// <param name="pNroTramite"></param>
        /// <returns></returns>
        public DataTable DaGetProductosTramite(String pNroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_productos_tramite", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                if (Int64.Parse(pNroTramite) != 0)
                    com.Parameters["pNroTramite"].Value = Int64.Parse(pNroTramite);
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Consulta todas las comunicaciones (mail, cel, tel, web page, facebook, etc) por cuit y/o sede. Consulta a la vista vt_com_sifcos.
        /// </summary>
        /// <param name="pNroTramite">Nro tramite de Sifcos es el ID_ENTIDAD de la Comunicación , es la Clave Primaria(PK) para identificarla.</param>
        /// <returns></returns>
        public DataTable DaGetComEmpresa(String pNroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Com_Empresa", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 11);
                if (pNroTramite != null)
                    com.Parameters["pNroTramite"].Value = pNroTramite;
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetPersonasRcivil(String pDNI, String pSEXO)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Personas_RCivil", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pDNI", OracleDbType.Varchar2, 12);
                if (pDNI != null)
                    com.Parameters["pDNI"].Value = pDNI;
                else
                    com.Parameters["pDNI"].Value = DBNull.Value;

                com.Parameters.Add("pSEXO", OracleDbType.Varchar2, 2);
                if (pSEXO != "")
                    com.Parameters["pSEXO"].Value = pSEXO;
                else
                    com.Parameters["pSEXO"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetPersonasRcivil2(String pDNI, String pSEXO)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Personas_RCivil2", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pDNI", OracleDbType.Varchar2, 12);
                if (pDNI != null)
                    com.Parameters["pDNI"].Value = pDNI;
                else
                    com.Parameters["pDNI"].Value = DBNull.Value;

                com.Parameters.Add("pSEXO", OracleDbType.Varchar2, 2);
                if (pSEXO != "")
                    com.Parameters["pSEXO"].Value = pSEXO;
                else
                    com.Parameters["pSEXO"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetPersonasRcivil3(String pNombre, String pApellido)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Personas_RCivil3", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pNombre", OracleDbType.Varchar2, 100);
                if (pNombre != null)
                    com.Parameters["pNombre"].Value = pNombre;
                else
                    com.Parameters["pNombre"].Value = DBNull.Value;

                com.Parameters.Add("pApellido", OracleDbType.Varchar2, 100);
                if (pApellido != "")
                    com.Parameters["pApellido"].Value = pApellido;
                else
                    com.Parameters["pApellido"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaGenerarCUIL(String pCUIL, Int64 pDNI, String pSEXO, Int64 pID_NUMERO, String pPais)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("rcivil.pack_persona.pr_inserta_cuil", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("p_cuil", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_id_aplicacion", OracleDbType.Int64, 2);
                com.Parameters.Add("p_id_sexo", OracleDbType.Varchar2, 2);
                com.Parameters.Add("p_nro_documento", OracleDbType.Int64, 10);
                com.Parameters.Add("p_pais", OracleDbType.Varchar2, 3);
                com.Parameters.Add("p_id_numero", OracleDbType.Varchar2, 2);

                com.Parameters.Add("o_mensaje", OracleDbType.Varchar2, 100);

                com.Parameters["p_cuil"].Value = pCUIL;
                com.Parameters["p_id_aplicacion"].Value = 98;
                com.Parameters["p_id_sexo"].Value = pSEXO;
                com.Parameters["p_nro_documento"].Value = pDNI;
                com.Parameters["p_pais"].Value = pPais;
                com.Parameters["p_id_numero"].Value = pID_NUMERO;

                com.Parameters["o_mensaje"].Direction = ParameterDirection.Output;

                conn.Open();
                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["o_mensaje"].Value.ToString();

                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaGenerarRazonSocial(String pCUIT, String pRazonSocial, String pnro_ing_bruto)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try

            {
                OracleCommand com = new OracleCommand("t_comunes.pack_persona_juridica.inserta_persjur", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("p_cuit", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_razon_social", OracleDbType.Varchar2, 100);
                com.Parameters.Add("p_nom_fan", OracleDbType.Varchar2, 100);
                com.Parameters.Add("p_id_for_jur", OracleDbType.Varchar2, 10);
                com.Parameters.Add("p_id_cond_iva", OracleDbType.Varchar2, 10);
                com.Parameters.Add("p_id_aplicacion", OracleDbType.Int64, 2);
                com.Parameters.Add("p_fec_inicio_act", OracleDbType.Date, 20);
                com.Parameters.Add("p_nro_ing_bruto", OracleDbType.Varchar2, 10);
                com.Parameters.Add("p_id_cond_ingbruto", OracleDbType.Varchar2, 10);

                com.Parameters.Add("o_id_sede", OracleDbType.Varchar2, 2);
                com.Parameters.Add("o_resultado", OracleDbType.Varchar2, 100);


                com.Parameters["p_cuit"].Value = pCUIT;
                com.Parameters["p_razon_social"].Value = pRazonSocial;
                com.Parameters["p_nom_fan"].Value = DBNull.Value;
                com.Parameters["p_id_for_jur"].Value = DBNull.Value;
                com.Parameters["p_id_cond_iva"].Value = DBNull.Value;
                com.Parameters["p_id_aplicacion"].Value = 98;
                com.Parameters["p_fec_inicio_act"].Value = DBNull.Value;
                com.Parameters["p_nro_ing_bruto"].Value = pnro_ing_bruto;
                com.Parameters["p_id_cond_ingbruto"].Value = DBNull.Value;

                com.Parameters["o_id_sede"].Direction = ParameterDirection.Output;
                com.Parameters["o_resultado"].Direction = ParameterDirection.Output;

                conn.Open();
                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["o_resultado"].Value.ToString();

                conn.Close();

                return pResultado;

            }

            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaCambiarTitularTRS(String Nro_Liq, String pIdSEXO, String pCUIL, Int64 pDNI)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("TRS.sp_cambiar_titular", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("p_nroliquidacionoriginal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("p_id_sexo", OracleDbType.Varchar2, 2);
                com.Parameters.Add("p_nro_documento", OracleDbType.Int64, 10);
                com.Parameters.Add("p_cuil", OracleDbType.Varchar2, 11);

                com.Parameters.Add("o_resultado", OracleDbType.Varchar2, 100);

                com.Parameters["p_nroliquidacionoriginal"].Value = Nro_Liq;
                com.Parameters["p_id_sexo"].Value = pIdSEXO;
                com.Parameters["p_nro_documento"].Value = pDNI;
                com.Parameters["p_cuil"].Value = pCUIL;

                com.Parameters["o_resultado"].Direction = ParameterDirection.Output;

                conn.Open();
                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["o_resultado"].Value.ToString();

                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }



        public DataTable DaGetDepartamentos(String id_provincia)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_departamentos", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pIdProvincia", OracleDbType.Varchar2, 6);
                if (id_provincia != null)
                    com.Parameters["pIdProvincia"].Value = id_provincia;
                else
                    com.Parameters["pIdProvincia"].Value = DBNull.Value;
                com.Parameters.Add("pDepartamentos", OracleDbType.RefCursor);
                com.Parameters["pDepartamentos"].Direction = ParameterDirection.Output;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetLocalidades(String id_departamento)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_localidades", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pIdDepto", OracleDbType.Int32, 6);
                if (id_departamento != null)
                    com.Parameters["pIdDepto"].Value = id_departamento;
                else
                    com.Parameters["pIdDepto"].Value = DBNull.Value;
                com.Parameters.Add("pLocalidades", OracleDbType.RefCursor);
                com.Parameters["pLocalidades"].Direction = ParameterDirection.Output;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }



        public DataTable DaGetSedes(String pCuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_sedes", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pCuit", OracleDbType.Int32, 6);
                if (pCuit != null)
                    com.Parameters["pCuit"].Value = pCuit;
                else
                    com.Parameters["pCuit"].Value = DBNull.Value;
                com.Parameters.Add("pSedes", OracleDbType.RefCursor);
                com.Parameters["pSedes"].Direction = ParameterDirection.Output;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetSuperficies(String prefijo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Superficies", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pPrefijo", OracleDbType.Varchar2, 100);
                if (prefijo != null)
                    com.Parameters["pPrefijo"].Value = prefijo;
                else
                    com.Parameters["pPrefijo"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetProductos(String prefijo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Productos", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pPrefijo", OracleDbType.Varchar2, 100);
                if (prefijo != null)
                    com.Parameters["pPrefijo"].Value = prefijo;
                else
                    com.Parameters["pPrefijo"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetProductosBETA(String prefijo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_ProductosBETA", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pPrefijo", OracleDbType.Varchar2, 100);
                if (prefijo != null)
                    com.Parameters["pPrefijo"].Value = prefijo;
                else
                    com.Parameters["pPrefijo"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// consulta de todas las calles dependiendo de la provincia, localidad y departamento
        /// </summary>
        /// <param name="prefijo"></param>
        /// <returns></returns>




        public DataTable DaGetOrganismos()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" SELECT distinct o.id_organismo,pj.razon_social n_organismo ");
                sql.Append(" FROM sifcos.t_sif_organismos o ");
                sql.Append(" join t_comunes.vt_pers_juridicas pj on pj.cuit = o.cuit and o.id_sede = pj.id_sede ");
                sql.Append(" order by 1 ");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public DataTable DaGetEstadosbr()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Estados_br", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }



        /// <summary>
        /// Se obtienen los datos basicos del tramite para la consulta de cada usuario web en la pagina mistramites.aspx
        /// </summary>
        /// <param name="CUIT"></param>
        /// <returns>
        /// id_entidad,cuit,id_sede_entidad,razon_social,nombre_fantasia,NRO_SIFCOS,nro_tramite,fec_ini_tramite,n_tipo_tramite,estado,desc_estado
        /// </returns>
        public DataTable DaGetTramites(String CUIT, Int64 NroTramite, String CuilUsuarioCidi)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_misTramites", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_nro_tramite", OracleDbType.Int64, 10);

                if (NroTramite != 0)
                    com.Parameters["p_nro_tramite"].Value = NroTramite;
                else
                    com.Parameters["p_nro_tramite"].Value = DBNull.Value;

                com.Parameters.Add("p_cuit", OracleDbType.Varchar2, 20);

                if (CUIT != null)
                    com.Parameters["p_cuit"].Value = CUIT;
                else
                    com.Parameters["p_cuit"].Value = DBNull.Value;

                com.Parameters.Add("p_cuilUsuarioCidi", OracleDbType.Varchar2, 20);

                if (CuilUsuarioCidi != null)
                    com.Parameters["p_cuilUsuarioCidi"].Value = CuilUsuarioCidi;
                else
                    com.Parameters["p_cuilUsuarioCidi"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// consulta por el nro de tramite si la entidad realizo el pago de la TRS 
        /// devuelve "ADEUDA" si la entidad tiene alguna tasa impaga
        /// devuelve "NO DEBE" si la entidad pago todas sus tasas
        /// </summary>z 
        /// <param name="NroTramite"></param>
        /// <returns></returns>
        public string DaGetAdeudaEntidad(Int64 NroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_entidades_deuda", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);

                if (NroTramite != 0)
                    com.Parameters["pNroTramite"].Value = NroTramite;
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DaActualizarAccesoUsuario(String Cuit, out String pRol, out String pUltimoAcceso)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Acceso_Usuarios", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdUsuario", OracleDbType.Varchar2, 11);

                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["pIdUsuario"].Value = Cuit;

                com.Parameters.Add("pUltimoAcceso", OracleDbType.Varchar2, 100);
                com.Parameters["pUltimoAcceso"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pRol", OracleDbType.Varchar2, 100);
                com.Parameters["pRol"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                pUltimoAcceso = com.Parameters["pUltimoAcceso"].Value.ToString();
                pRol = com.Parameters["pRol"].Value.ToString();
                //pUltimoAcceso = "LLL";
                //pRol = "1";
                conn.Close();


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Verificar existencia de cuit
        /// </summary>
        /// <param name="Cuit"></param>
        /// <param name="pExiste"></param>
        public void DaExisteEnSifcos(String Cuit, out Int64 pExiste)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_existe_en_sifcos", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pCuit", OracleDbType.Varchar2, 11);

                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["pCuit"].Value = Cuit;

                com.Parameters.Add("pExiste", OracleDbType.Int64, 10);
                com.Parameters["pExiste"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                pExiste = Int64.Parse(com.Parameters["pExiste"].Value.ToString());

                conn.Close();


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Actualizar roles a los usuarios
        /// </summary>
        /// <param name="Cuit"></param>
        /// <returns></returns>
        public string DaActualizarRolUsuario(String Cuit, Int64 Rol)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Rol_Usuario", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdUsuario", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pRolUsuario", OracleDbType.Int64, 5);

                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["pIdUsuario"].Value = Cuit;

                if (Rol != 0)
                    com.Parameters["pRolUsuario"].Value = Rol;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaActualizarOrgUsuario(String Cuit, Int64 IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Org_Usuario", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdUsuario", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pIdOrganismo", OracleDbType.Int64, 5);

                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["pIdUsuario"].Value = Cuit;

                if (IdOrganismo != 0)
                    com.Parameters["pIdOrganismo"].Value = IdOrganismo;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// sirve para buscar el rol del usuario
        /// </summary>
        /// <param name="Cuit"></param>
        /// <returns></returns>
        public string DaGetRolUsuario(String Cuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Rol_Usuario", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_UsuarioCidi", OracleDbType.Varchar2, 11);

                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["p_UsuarioCidi"].Value = Cuit;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Roles> DaGetRelacionesUsuario(String Cuit)
        {
            var lista = new List<Roles>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Relaciones_Usuario", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_UsuarioCidi", OracleDbType.Varchar2, 11);

                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["p_UsuarioCidi"].Value = Cuit;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();
                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new Roles()
                    {
                        Cuil = resultado["ID_USUARIO_CIDI"].ToString(),
                        FecUltAcceso = resultado["FEC_ULT_ACCESO"].ToString(),
                        Rol = resultado["ID_ROL"].ToString(),
                        Aplicacion = resultado["N_APLICACION"].ToString(),
                        Permiso = resultado["PERMISO"].ToString(),


                    };

                    lista.Add(obj);
                }

                conn.Close();
                return lista;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Elimina el Rol asignado a un usuario y le asigna el rol por defecto sin asignar (usuario bloqueado sin acceso al sistema)
        /// </summary>
        /// <param name="Cuit"></param>
        /// <returns></returns>
        public string DaEliminarRolUsuario(String Cuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Eliminar_Rol_Usuario", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdUsuario", OracleDbType.Varchar2, 11);

                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["pIdUsuario"].Value = Cuit;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// eliminar la asignacion de un organismo a un usuario
        /// </summary>
        /// <param name="Cuit"></param>
        /// <returns></returns>
        public string DaEliminarOrganismo(String Cuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Eliminar_Org_Usuario", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdUsuario", OracleDbType.Varchar2, 11);

                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["pIdUsuario"].Value = Cuit;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Actualizado ultimo Estado asignado a verificado 
        /// </summary>
        /// <param name="NroTramite"></param>
        /// <returns></returns>
        public string DaActEstadoVerificado(String NroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Estado_Verificado", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);

                if (!String.IsNullOrEmpty(NroTramite))
                    com.Parameters["pNroTramite"].Value = Int64.Parse(NroTramite);

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();

                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Actualizado ultimo Estado asignado a rechazado
        /// </summary>
        /// <param name="NroTramite"></param>
        /// <returns></returns>
        public string DaActEstadoRechazado(String NroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Estado_Rechazado", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 11);

                if (!String.IsNullOrEmpty(NroTramite))
                    com.Parameters["pNroTramite"].Value = NroTramite;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// eliminar las relaciones de un producto
        /// </summary>
        /// <param name="id_producto"></param>
        /// <returns></returns>
        public string DaEliminarRelProd(String Id_producto)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Delete_Rel_Prod_Act", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pId_Producto", OracleDbType.Varchar2, 11);

                if (!String.IsNullOrEmpty(Id_producto))
                    com.Parameters["pId_Producto"].Value = Id_producto;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaAgregarUsuario(String Cuit, Int64 Rol)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_Rol_Usuario", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdUsuario", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pRolUsuario", OracleDbType.Int64, 10);

                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["pIdUsuario"].Value = Cuit;

                if (Rol != 0)
                    com.Parameters["pRolUsuario"].Value = Rol;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaAgregarUsuarioOrganismo(String Cuil, Int64 IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_Org_Usuario", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdUsuario", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pIdOrganismo", OracleDbType.Int64, 10);

                if (!String.IsNullOrEmpty(Cuil))
                    com.Parameters["pIdUsuario"].Value = Cuil;

                if (IdOrganismo != 0)
                    com.Parameters["pIdOrganismo"].Value = IdOrganismo;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                //OracleDataAdapter da = new OracleDataAdapter(com);
                //DataSet ds = new DataSet();
                //da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Consulta en las bocas por todos los datos de un tramite pasando el nro de tramite
        /// Se usa en bocaRecepcion.aspx
        /// </summary>
        /// <param name="NroTramite"></param>
        /// <returns>
        /// nro_tramite,nro_sifcos,cuit,razon_social,fec_inicio_act,nro_ingbruto,fec_ini_tramite,deuda,estado,fecha_estado,rep_legal, dni_rep_legal
        /// </returns>
        public DataTable DaGetInfoGralTramite(Int64 NroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_info_gral_tramite", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);

                if (NroTramite != 0)
                    com.Parameters["pNroTramite"].Value = NroTramite;
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Consulta a todos los estados de cada tramite
        /// </summary>
        /// <param name="NroTramite"></param>
        /// <returns>estado,descripcion,ult_cambio,usuario,organismo</returns>
        public DataTable DaGetHistEstados(Int64 NroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_HistEstados", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);

                if (NroTramite != 0)
                    com.Parameters["pNroTramite"].Value = NroTramite;
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Consulta de los tramites de cada Boca, con filtros de busqueda
        /// se usa en la pagina de consulta bocaRecepcion.aspx
        /// </summary>
        /// <param name="nroTramite"></param>
        /// <param name="nroSifcos"></param>
        /// <param name="cuit"></param>
        /// <param name="razonSocial"></param>
        /// <param name="OrdenConsulta"></param>
        /// <returns>
        /// nro_tramite,nro_sifcos,cuit,razon_social,estado,fecha_estado
        /// </returns>
        public DataTable DaGetTramitesBoca(String nroTramite, String nroSifcos, String cuit, String razonSocial,
            Int64 OrdenConsulta, String FechaDesde, String FechaHasta, String idEstado)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_tramites_boca", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_Nro_Tramite", OracleDbType.Varchar2, 10);
                com.Parameters.Add("P_Nro_Sifcos", OracleDbType.Varchar2, 10);
                com.Parameters.Add("P_CUIT", OracleDbType.Varchar2, 11);
                com.Parameters.Add("P_RAZON_SOCIAL", OracleDbType.Varchar2, 100);
                com.Parameters.Add("P_ORDEN_CONSULTA", OracleDbType.Int64, 2);
                com.Parameters.Add("P_IDESTADO", OracleDbType.Varchar2, 10);
                com.Parameters.Add("P_FECHADESDE", OracleDbType.Varchar2, 20);
                com.Parameters.Add("P_FECHAHASTA", OracleDbType.Varchar2, 20);

                com.Parameters["P_Nro_Tramite"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(nroTramite))
                {
                    com.Parameters["P_Nro_Tramite"].Value = nroTramite;
                }

                com.Parameters["P_Nro_Sifcos"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(nroSifcos))
                {
                    com.Parameters["P_Nro_Sifcos"].Value = nroSifcos;
                }

                com.Parameters["P_CUIT"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(cuit))
                {
                    com.Parameters["P_CUIT"].Value = cuit;
                }

                com.Parameters["P_RAZON_SOCIAL"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(razonSocial))
                {
                    com.Parameters["P_RAZON_SOCIAL"].Value = razonSocial;
                }

                com.Parameters["P_IDESTADO"].Value = DBNull.Value;
                if (idEstado != "0")
                {
                    com.Parameters["P_IDESTADO"].Value = idEstado;
                }

                com.Parameters["P_FECHADESDE"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(FechaDesde))
                {
                    com.Parameters["P_FECHADESDE"].Value = FechaDesde;
                }

                com.Parameters["P_FECHAHASTA"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(FechaHasta))
                {
                    com.Parameters["P_FECHAHASTA"].Value = FechaHasta;
                }

                if (OrdenConsulta != 0)
                {
                    com.Parameters["P_ORDEN_CONSULTA"].Value = OrdenConsulta;
                }
                else
                {
                    com.Parameters["P_ORDEN_CONSULTA"].Value = 1;
                }

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                //conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Consulta de los tramites de Baja, con filtros de busqueda
        /// se usa en la pagina de consulta bocaMinisterio.aspx
        /// </summary>
        /// <param name="nroTramite"></param>
        /// <param name="nroSifcos"></param>
        /// <param name="cuit"></param>
        /// <param name="razonSocial"></param>
        /// <param name="OrdenConsulta"></param>
        /// <returns>
        /// nro_tramite,nro_sifcos,cuit,razon_social,estado,fecha_estado
        /// </returns>
        public DataTable DaGetTramitesBaja(String nroTramite, String nroSifcos, String cuit, String razonSocial,
            Int64 OrdenConsulta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_tramites_baja", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_Nro_Tramite", OracleDbType.Varchar2, 10);
                com.Parameters.Add("P_Nro_Sifcos", OracleDbType.Varchar2, 10);
                com.Parameters.Add("P_CUIT", OracleDbType.Varchar2, 11);
                com.Parameters.Add("P_RAZON_SOCIAL", OracleDbType.Varchar2, 100);
                com.Parameters.Add("P_ORDEN_CONSULTA", OracleDbType.Int64, 2);

                com.Parameters["P_Nro_Tramite"].Value = nroTramite;
                com.Parameters["P_Nro_Sifcos"].Value = nroSifcos;
                com.Parameters["P_CUIT"].Value = cuit;
                com.Parameters["P_RAZON_SOCIAL"].Value = razonSocial;
                if (OrdenConsulta == 0)
                {
                    OrdenConsulta = 1;
                }
                com.Parameters["P_ORDEN_CONSULTA"].Value = OrdenConsulta;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                //conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetTramitesBocaRecepcion(String nroTramite, String nroSifcos, String cuit, String razonSocial,
            Int64 OrdenConsulta, String FechaDesde, String FechaHasta, String idEstado, String IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_tramites_boca_recepcion", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_Nro_Tramite", OracleDbType.Varchar2, 10);
                com.Parameters.Add("P_Nro_Sifcos", OracleDbType.Varchar2, 10);
                com.Parameters.Add("P_CUIT", OracleDbType.Varchar2, 11);
                com.Parameters.Add("P_RAZON_SOCIAL", OracleDbType.Varchar2, 100);
                com.Parameters.Add("P_ORDEN_CONSULTA", OracleDbType.Int64, 2);
                com.Parameters.Add("P_IDESTADO", OracleDbType.Varchar2, 10);
                com.Parameters.Add("P_FECHADESDE", OracleDbType.Varchar2, 20);
                com.Parameters.Add("P_FECHAHASTA", OracleDbType.Varchar2, 20);
                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Varchar2, 10);


                com.Parameters["P_Nro_Tramite"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(nroTramite))
                {
                    com.Parameters["P_Nro_Tramite"].Value = nroTramite;
                }

                com.Parameters["P_Nro_Sifcos"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(nroSifcos))
                {
                    com.Parameters["P_Nro_Sifcos"].Value = nroSifcos;
                }

                com.Parameters["P_CUIT"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(cuit))
                {
                    com.Parameters["P_CUIT"].Value = cuit;
                }

                com.Parameters["P_RAZON_SOCIAL"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(razonSocial))
                {
                    com.Parameters["P_RAZON_SOCIAL"].Value = razonSocial;
                }

                com.Parameters["P_IDESTADO"].Value = DBNull.Value;
                if (idEstado != "0")
                {
                    com.Parameters["P_IDESTADO"].Value = idEstado;
                }

                com.Parameters["P_FECHADESDE"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(FechaDesde))
                {
                    com.Parameters["P_FECHADESDE"].Value = FechaDesde;
                }

                com.Parameters["P_FECHAHASTA"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(FechaHasta))
                {
                    com.Parameters["P_FECHAHASTA"].Value = FechaHasta;
                }

                com.Parameters["P_ID_ORGANISMO"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(IdOrganismo))
                {
                    com.Parameters["P_ID_ORGANISMO"].Value = IdOrganismo;
                }

                if (OrdenConsulta == 0)
                {
                    OrdenConsulta = 1;
                }
                com.Parameters["P_ORDEN_CONSULTA"].Value = OrdenConsulta;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                //conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// AGREGAR PROCEDIMIENTO A BD
        /// </summary>
        /// <param name="rol"></param>
        /// <param name="cuil"></param>
        /// <param name="orden_consulta"></param>
        /// <returns></returns>
        public DataTable DaGetListaRolesUsuarios(String rol, String cuil, String orden_consulta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" SELECT rownum id  ");
                sql.Append(" ,u.id_usuario_cidi  ");
                sql.Append(" ,r.n_rol rol  ");
                sql.Append(" ,u.fec_ult_acceso  ");
                sql.Append(" ,  (SELECT p.NOV_APELLIDO ||', ' ||p.NOV_NOMBRE   ");
                sql.Append(" FROM rcivil.vt_pk_persona_cuil p  ");
                sql.Append(" WHERE p.NRO_OTRO_DOCUMENTO=u.id_usuario_cidi and ROWNUM<=1) apeynom  ");
                sql.Append(" FROM sifcos.t_sif_roles r  ");
                sql.Append(" join sifcos.t_sif_usuarios_cidi u on r.id_rol=u.id_rol   ");
                sql.Append(" WHERE ROWNUM<=10000   ");
                if (!string.IsNullOrEmpty(rol))
                    sql.Append(" AND r.n_rol = '" + rol + "'  ");

                if (!string.IsNullOrEmpty(cuil))
                    sql.Append(" AND u.id_usuario_cidi ='" + cuil + "'");

                sql.Append(" order by " + orden_consulta);

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];

                /*
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Roles_usuarios", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("p_Rol", OracleDbType.Varchar2, 20);
                com.Parameters.Add("p_cuil", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_orden_consulta", OracleDbType.Varchar2, 2);
                

                if (!String.IsNullOrEmpty(rol))
                    com.Parameters["p_Rol"].Value = rol;
                else
                    com.Parameters["p_Rol"].Value = DBNull.Value;

                if (!String.IsNullOrEmpty(cuil))
                    com.Parameters["p_cuil"].Value = cuil;
                else
                    com.Parameters["p_cuil"].Value = DBNull.Value;

                if (!String.IsNullOrEmpty(orden_consulta))
                    com.Parameters["p_orden_consulta"].Value = orden_consulta;
                else
                    com.Parameters["p_orden_consulta"].Value = "1";

                com.Parameters.Add("presultado", OracleDbType.RefCursor);
                com.Parameters["presultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;*/
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetListaOrganismosUsuarios(String Organismo, String cuil, String orden_consulta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_organismos_usuarios", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("p_Organismo", OracleDbType.Varchar2, 100);
                com.Parameters.Add("p_cuil", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_orden_consulta", OracleDbType.Varchar2, 2);


                if (!String.IsNullOrEmpty(Organismo))
                    com.Parameters["p_Organismo"].Value = Organismo;
                else
                    com.Parameters["p_Organismo"].Value = "";

                if (!String.IsNullOrEmpty(cuil))
                    com.Parameters["p_cuil"].Value = cuil;
                else
                    com.Parameters["p_cuil"].Value = DBNull.Value;

                if (!String.IsNullOrEmpty(orden_consulta))
                    com.Parameters["p_orden_consulta"].Value = orden_consulta;
                else
                    com.Parameters["p_orden_consulta"].Value = "1";

                com.Parameters.Add("presultado", OracleDbType.RefCursor);
                com.Parameters["presultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Consulta en BocaRecepcion al tramite para ver todos sus datos
        /// </summary>
        /// <param name="nroTramite"></param>
        /// <returns>
        /// nro_tramite_sifcos, id_vin_dom_legal,id_vin_dom_local,capacitacion_ult_anio,cuil_usuario_cidi,id_tipo_tramite,n_tipo_tramite,
        /// fec_ini_tramite,idtrasacciontasa,pagotasa,cant_pers_total,cant_pers_rel_dependencia,id_localidad_certifica_resp,n_origen_proveedor
        /// n_estado_tramite,desc_estado_tramite,id_entidad,cuit,razon_social,id_sede,sedes,local,oficina,stand,cobertura_medica,seguro_local,
        /// latitud_ubi,longitud_ubi,n_cargo,nom_rep_legal,ape_rep_legal,nro_dgr,sup_admin,sup_ventas,sup_deposito
        /// </returns>
        public DataTable DaConsultaTramite(Int64 nroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_consulta_tramite", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("p_nrotramite", OracleDbType.Int64, 100);
                if (nroTramite != 0)
                    com.Parameters["p_nrotramite"].Value = nroTramite;
                else
                    com.Parameters["p_nrotramite"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// informacion que se obtiene de donde es el usuario logueado para la cabecera de la bocaRecepcion.aspx
        /// </summary>
        /// <param name="pCuilUser"></param>
        /// <returns>
        /// boca_recepcion,localidad,dependencia
        /// </returns>
        public DataTable DaGetInfoBoca(String pCuilUser)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_InfoBoca", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pCuilCidi", OracleDbType.Varchar2, 11);
                if (pCuilUser != "")
                    com.Parameters["pCuilCidi"].Value = pCuilUser;
                else
                    com.Parameters["pCuilCidi"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Informacion de la deuda que posee cada tramite
        /// </summary>
        /// <param name="pNroTramite"></param>
        /// <returns>
        /// nro_tramite_sifcos,obligacion nro_trs,fecha_emision,importe_total deuda
        /// </returns>
        public DataTable DaGetDeudaTramite(Int64 pNroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_trs_vigentes", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                if (pNroTramite != 0)
                    com.Parameters["pNroTramite"].Value = pNroTramite;
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }



        public DataTable DaGetIdSuperficieProx()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_IDSuperficie", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String DaRegistrarSuperficie(String N_Superficie)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_insert_superficie", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_N_Superficie", OracleDbType.Varchar2, 50);
                if (N_Superficie != null)
                    com.Parameters["p_N_Superficie"].Value = N_Superficie;
                else
                    com.Parameters["p_N_Superficie"].Value = DBNull.Value;
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                    return "OK";
                if (pResultado == "REGISTRO EXISTENTE")
                    return "1";
                return "ERROR";
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String DaRegistrarProducto(String N_Producto)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_Producto", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pN_Producto", OracleDbType.Varchar2, 100);
                if (N_Producto != null)
                    com.Parameters["pN_Producto"].Value = N_Producto;
                else
                    com.Parameters["pN_Producto"].Value = DBNull.Value;
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();

                if (pResultado == "OK")
                    return "OK";
                if (pResultado == "REGISTRO EXISTENTE")
                    return "1";
                return "ERROR";
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String DaNoConfirmarProducto(String idProducto, String nroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_No_Conf_Prod", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNro_Tramite", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pId_producto", OracleDbType.Varchar2, 10);


                com.Parameters["pNro_Tramite"].Value = nroTramite;
                com.Parameters["pId_producto"].Value = idProducto;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                    return "OK";

                return "ERROR";
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public String DaModificarDomicilioLocal(Domicilio domicilio1,String IdEntidad, InscripcionSifcosDto Alta)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Domlocal", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                //entidad
                com.Parameters.Add("pLocal", OracleDbType.Varchar2, 5);
                com.Parameters.Add("pOficina", OracleDbType.Varchar2, 5);
                com.Parameters.Add("pStand", OracleDbType.Varchar2, 5);

                //parametros del tramite
                com.Parameters.Add("pCuilUsuarioCIDI", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pIdEntidad", OracleDbType.Varchar2, 20);

                //domicilio local
                com.Parameters.Add("pIdDepartamento_Local", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pIdLocalidad_Local", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pIdBarrio_Local", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pBarrio_Local", OracleDbType.Varchar2, 50);
                com.Parameters.Add("pCalle_Local", OracleDbType.Varchar2, 50);
                com.Parameters.Add("pNroKm_Local", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pCodPostal_Local", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pPiso_Local", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pDpto_Local", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);

                //entidad
                com.Parameters["pLocal"].Value = Alta.Local;
                com.Parameters["pOficina"].Value = Alta.Oficina;
                com.Parameters["pStand"].Value = Alta.Stand;

                //tramite
                com.Parameters["pCuilUsuarioCIDI"].Value = Alta.CuilUsuarioCidi;
                com.Parameters["pNroTramite"].Value = Alta.NroTramiteSifcos;
                com.Parameters["pIdEntidad"].Value = IdEntidad;


                //domicilio local
                com.Parameters["pIdDepartamento_Local"].Value = domicilio1.Departamento.IdDepartamento;
                com.Parameters["pIdLocalidad_Local"].Value = domicilio1.Localidad.IdLocalidad;
                com.Parameters["pIdBarrio_Local"].Value = domicilio1.Barrio.IdBarrio;
                com.Parameters["pBarrio_Local"].Value = domicilio1.Barrio;
                com.Parameters["pCalle_Local"].Value = domicilio1.Calle;
                com.Parameters["pNroKm_Local"].Value = domicilio1.Altura;
                com.Parameters["pCodPostal_Local"].Value = domicilio1.CodigoPostal;
                com.Parameters["pPiso_Local"].Value = domicilio1.Piso;
                com.Parameters["pDpto_Local"].Value = domicilio1.Dpto;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();


                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public String DaModificarDomicilioLocaldelTramite(InscripcionSifcosDto Alta)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Domlocal_Tramite", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                com.Parameters.Add("pCuilUsuarioCIDI", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pIdVin_Local", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pLat", OracleDbType.Varchar2, 50);
                com.Parameters.Add("pLong", OracleDbType.Varchar2, 50);
                com.Parameters.Add("pid_entidad", OracleDbType.Int64, 10);

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);


                com.Parameters["pCuilUsuarioCIDI"].Value = Alta.CuilUsuarioCidi;
                com.Parameters["pNroTramite"].Value = Alta.NroTramiteSifcos;
                com.Parameters["pIdVin_Local"].Value = Alta.IdVinDomLocal;
                com.Parameters["pLat"].Value = Alta.Latitud;
                com.Parameters["pLong"].Value = Alta.Longitud;
                com.Parameters["pid_entidad"].Value = Alta.IdEntidad;

                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();


                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaModificarDomicilioLegal(Domicilio domicilio2, String IdEntidad,InscripcionSifcosDto tramiteDto)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_DomLegal", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();
                //parametros del tramite
                com.Parameters.Add("pCuilUsuarioCIDI", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pIdEntidad", OracleDbType.Varchar2, 20);
                //domicilio legal
                com.Parameters.Add("pIdProvincia_Legal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pIdDepartamento_Legal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pIdLocalidad_Legal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pIdBarrio_Legal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pBarrio_Legal", OracleDbType.Varchar2, 50);
                com.Parameters.Add("pCalle_Legal", OracleDbType.Varchar2, 50);
                com.Parameters.Add("pNroKm_Legal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pCodPostal_Legal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pPiso_Legal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pDpto_Legal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);




                //tramite
                com.Parameters["pCuilUsuarioCIDI"].Value = tramiteDto.CuilUsuarioCidi;
                com.Parameters["pNroTramite"].Value = tramiteDto.NroTramiteSifcos;
                com.Parameters["pIdEntidad"].Value = IdEntidad;

                //domicilio legal
                com.Parameters["pIdProvincia_Legal"].Value = domicilio2.Provincia.IdProvincia;
                com.Parameters["pIdDepartamento_Legal"].Value = domicilio2.Departamento.IdDepartamento;
                com.Parameters["pIdLocalidad_Legal"].Value = domicilio2.Localidad.IdLocalidad;
                com.Parameters["pIdBarrio_Legal"].Value = domicilio2.Barrio.IdBarrio;
                com.Parameters["pBarrio_Legal"].Value = domicilio2.Barrio;
                com.Parameters["pCalle_Legal"].Value = domicilio2.Calle;
                com.Parameters["pNroKm_Legal"].Value = domicilio2.Altura;
                com.Parameters["pCodPostal_Legal"].Value = domicilio2.CodigoPostal;
                com.Parameters["pPiso_Legal"].Value = domicilio2.Piso;
                com.Parameters["pDpto_Legal"].Value = domicilio2.Dpto;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();

                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String DaModificarDomicilioLegaldelTramite(InscripcionSifcosDto Alta)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Domlegal_Tramite", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                com.Parameters.Add("pCuilUsuarioCIDI", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pIdVin_Legal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pid_entidad", OracleDbType.Int64, 10);

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);

                com.Parameters["pCuilUsuarioCIDI"].Value = Alta.CuilUsuarioCidi;
                com.Parameters["pNroTramite"].Value = Alta.NroTramiteSifcos;
                com.Parameters["pIdVin_Legal"].Value = Alta.IdVinDomLegal;

                com.Parameters["pid_entidad"].Value = Alta.IdEntidad;

                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();


                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String DaModificarInfoGral(InscripcionSifcosDto Alta)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_InfoGral", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();
                //entidad
                com.Parameters.Add("pCobertura", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pSeguro", OracleDbType.Varchar2, 2);
                //datos grales tramite
                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                com.Parameters.Add("pCuilUsuarioCIDI", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pCuit", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pCantTotalPers", OracleDbType.Int64, 10);
                com.Parameters.Add("pCantPersRelDependencia", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pCapacitacionUltAnio", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pNombreFantasia", OracleDbType.Varchar2, 50);
                com.Parameters.Add("pFechaIniActividad", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pNroHabMunicipal", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pNroDGR", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pSup_Admin", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pSup_Venta", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pSup_Dep", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pPropietario", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pIdOrigenProv", OracleDbType.Int64, 10);
                com.Parameters.Add("pRangoAlq", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                //entidad
                com.Parameters["pCobertura"].Value = Alta.Cobertura;
                com.Parameters["pSeguro"].Value = Alta.Seguro;
                //datos grales tramite
                com.Parameters["pCuilUsuarioCIDI"].Value = Alta.CuilUsuarioCidi;
                com.Parameters["pCuit"].Value = Alta.CUIT;
                com.Parameters["pNroTramite"].Value = Alta.NroTramiteSifcos;
                com.Parameters["pCantTotalPers"].Value = Alta.CantTotalpers;
                com.Parameters["pCantPersRelDependencia"].Value = Alta.CantPersRelDep;
                com.Parameters["pCapacitacionUltAnio"].Value = Alta.CapacUltAnio;
                com.Parameters["pNombreFantasia"].Value = Alta.NombreFantasia;
                com.Parameters["pFechaIniActividad"].Value = Alta.FechaIniActividad;
                com.Parameters["pNroHabMunicipal"].Value = Alta.NroHabMunicipal;
                com.Parameters["pNroDGR"].Value = Alta.NroDGR;
                com.Parameters["pSup_Admin"].Value = Alta.SupAdministracion;
                com.Parameters["pSup_Venta"].Value = Alta.SupVentas;
                com.Parameters["pSup_Dep"].Value = Alta.SupDeposito;
                com.Parameters["pPropietario"].Value = Alta.Propietario;
                com.Parameters["pIdOrigenProv"].Value = Alta.IdOrigenProveedor;
                com.Parameters["pRangoAlq"].Value = Alta.RangoAlquiler;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                //OracleDataAdapter da = new OracleDataAdapter(com);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();

                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaModificarProdAct(InscripcionSifcosDto Alta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Act", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();
                //datos tramite
                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                com.Parameters.Add("pCuilUsuarioCIDI", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pIdActividadPRI", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pIdActividadSEC", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);

                com.Parameters["pNroTramite"].Value = Alta.NroTramiteSifcos;
                com.Parameters["pCuilUsuarioCIDI"].Value = Alta.CuilUsuarioCidi;
                com.Parameters["pIdActividadPRI"].Value = Alta.ActividadPrimaria;
                com.Parameters["pIdActividadSEC"].Value = Alta.ActividadSecundaria;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();

                string pResultadoProducto = "";

                if (pResultado == "OK4")
                {
                    foreach (Producto producto in Alta.Productos)
                    {
                        com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_prod_tra", conn);
                        com.CommandType = CommandType.StoredProcedure;

                        com.Parameters.Add("pId_Producto", OracleDbType.Varchar2, 11);
                        com.Parameters["pId_Producto"].Value = producto.IdProducto;

                        com.Parameters.Add("pNro_tramite", OracleDbType.Int64, 10);
                        com.Parameters["pNro_tramite"].Value = Int64.Parse(Alta.NroTramiteSifcos);

                        com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                        com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                        com.ExecuteNonQuery();
                        pResultadoProducto = com.Parameters["pResultado"].Value.ToString();
                        if (pResultadoProducto != "OK")
                        {
                            return "ERROR2";
                        }
                    }


                }
                else
                {
                    return "ERROR";
                }
                return "OK4";

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public String DaModificarRepLegal(InscripcionSifcosDto Alta)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_RepLegal", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();
                //datos grales tramite
                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                //parametros de carga representante
                com.Parameters.Add("pRepLegal_cuil", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pRepLegal_id_cargo", OracleDbType.Int64, 10);
                //parametro usuario CIDI
                com.Parameters.Add("pCuilUsuarioCIDI", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                //datos grales tramite
                com.Parameters["pNroTramite"].Value = Alta.NroTramiteSifcos;
                //parametros de carga representante
                com.Parameters["pRepLegal_id_cargo"].Value = Alta.IdCargo != 0 ? (object)Alta.IdCargo : DBNull.Value;
                com.Parameters["pRepLegal_cuil"].Value = Alta.CuilRepLegal != null ? (object)Alta.CuilRepLegal : DBNull.Value;
                //parametro usuario CIDI
                com.Parameters["pCuilUsuarioCIDI"].Value = Alta.CuilUsuarioCidi;

                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                //OracleDataAdapter da = new OracleDataAdapter(com);
                //DataSet ds = new DataSet();
                //da.Fill(ds);

                com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();

                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String DaModificarContacto(InscripcionSifcosDto Alta)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Contacto", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();
                //datos tramite
                com.Parameters.Add("pCuilUsuarioCIDI", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                //comunicacion
                com.Parameters.Add("p_entidad", OracleDbType.Varchar2, 20);
                com.Parameters.Add("p_Facebook", OracleDbType.Varchar2, 100);
                com.Parameters.Add("p_pagWeb", OracleDbType.Varchar2, 100);
                com.Parameters.Add("p_email", OracleDbType.Varchar2, 100);
                com.Parameters.Add("p_CodAreaTelFijo", OracleDbType.Varchar2, 50);
                com.Parameters.Add("p_nroTelFijo", OracleDbType.Varchar2, 50);
                com.Parameters.Add("p_CodAreaCel", OracleDbType.Varchar2, 50);
                com.Parameters.Add("p_nroCel", OracleDbType.Varchar2, 50);

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);

                //datos grales tramite
                com.Parameters["pCuilUsuarioCIDI"].Value = Alta.CuilUsuarioCidi;
                com.Parameters["pNroTramite"].Value = Alta.NroTramiteSifcos;
                //comunicacion
                com.Parameters["p_entidad"].Value = Alta.NroTramiteSifcos;
                com.Parameters["p_Facebook"].Value = Alta.Facebook;
                com.Parameters["p_pagWeb"].Value = Alta.WebPage;
                com.Parameters["p_email"].Value = Alta.EmailEstablecimiento;
                com.Parameters["p_CodAreaTelFijo"].Value = Alta.CodAreaTelFijo;
                com.Parameters["p_nroTelFijo"].Value = Alta.TelFijo;
                com.Parameters["p_CodAreaCel"].Value = Alta.CodAreaCelular;
                com.Parameters["p_nroCel"].Value = Alta.Celular;

                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                //OracleDataAdapter da = new OracleDataAdapter(com);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                //string pNroTramite = com.Parameters["pNroTramite"].Value.ToString();

                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String DaModificarProducto(String IdProducto, String NProducto)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Producto", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();
                com.Parameters.Add("pIDProducto", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pNProducto", OracleDbType.Varchar2, 50);

                com.Parameters["pIDProducto"].Value = IdProducto;
                com.Parameters["pNProducto"].Value = NProducto;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();

                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String DaModificarUbicacion(InscripcionSifcosDto tramiteDto)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_ubicacion", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();
                //parametros del tramite
                com.Parameters.Add("pLatitud", OracleDbType.Varchar2, 50);
                com.Parameters.Add("pLongitud", OracleDbType.Varchar2, 50);
                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                com.Parameters.Add("pCuilUsuarioCIDI", OracleDbType.Varchar2, 20);

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);

                com.Parameters["pLatitud"].Value = tramiteDto.Latitud;
                com.Parameters["pLongitud"].Value = tramiteDto.Longitud;
                com.Parameters["pNroTramite"].Value = tramiteDto.NroTramiteSifcos;
                com.Parameters["pCuilUsuarioCIDI"].Value = tramiteDto.CuilUsuarioCidi;

                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();

                conn.Close();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaRegistrarInscripcion(InscripcionSifcos Alta, out string nroTramiteNuevo)
        {
            string cuitFormat = Alta.CUIT.Substring(0, 2) + "-" + Alta.CUIT.Substring(2, Alta.CUIT.Length - 3) + "-" +
                                Alta.CUIT.Substring(Alta.CUIT.Length - 1, 1);
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_insert_inscripcion", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                //parametros de carga entidad- OJO EL ORDEN ES IMPORTANTE!
                com.Parameters.Add("pEnt_CUIT_FORMAT", OracleDbType.Varchar2, 15);
                com.Parameters.Add("pEnt_CUIT", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pEnt_Id_Sede", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pEnt_Local", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pEnt_Oficina", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pEnt_Stand", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pEnt_Cobertura", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pEnt_Propietario", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pEnt_Seguro", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pEnt_Latitud", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pEnt_Longitud", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pEnt_Nro_Sifcos", OracleDbType.Int64, 10);
                //parametros de carga tramite
                com.Parameters.Add("pTram_Capac_ult_anio", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pTram_Cuil_Usu_CIDI", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pTram_Tipo_Tramite", OracleDbType.Int64, 10);
                com.Parameters.Add("pTram_Id_origen_prov", OracleDbType.Int64, 10);
                com.Parameters.Add("pTram_Id_vin_dom_legal", OracleDbType.Int64, 20);
                com.Parameters.Add("pTram_Id_vin_dom_local", OracleDbType.Int64, 20);
                com.Parameters.Add("pTram_Rango_alquiler", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pTram_Id_Act_Pri", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pTram_Id_Act_Sec", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pTram_Cant_total_pers", OracleDbType.Varchar2, 5);
                com.Parameters.Add("pTram_CantPersRelDep", OracleDbType.Varchar2, 5);
                com.Parameters.Add("pTram_FechaVencimiento", OracleDbType.Date);
                com.Parameters.Add("pTram_cant_reemp", OracleDbType.Int64, 10);

                //parametros de carga representante
                com.Parameters.Add("pRepr_id_cargo", OracleDbType.Int64, 10);
                com.Parameters.Add("pRepr_Cuil_RepLegal", OracleDbType.Varchar2, 11);
                //parametros de carga gestor 
                com.Parameters.Add("pGest_Cuil_Gestor", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pGest_id_tipo_gestor", OracleDbType.Int64, 10);
                com.Parameters.Add("pGest_CodAreaCelConta", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pGest_nroCelConta", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pGest_email_conta", OracleDbType.Varchar2, 50);
                //parametros de carga superficies
                com.Parameters.Add("pSup_Ventas", OracleDbType.Int64, 10);
                com.Parameters.Add("pSup_Administracion", OracleDbType.Int64, 10);
                com.Parameters.Add("pSup_Deposito", OracleDbType.Int64, 10);
                com.Parameters.Add("pHest_Id_estado", OracleDbType.Int64, 10);
                //parametros para agregar en t_comunes 
                com.Parameters.Add("p_nro_habMunicipal", OracleDbType.Varchar2, 15);
                com.Parameters.Add("p_fecha_ini_act", OracleDbType.Date);
                //-lt DOCUMENTACION CDD
                com.Parameters.Add("p_Id_Documento1_CDD", OracleDbType.Int64, 10);
                com.Parameters.Add("p_Id_Documento2_CDD", OracleDbType.Int64, 10);
                com.Parameters.Add("p_Id_Documento3_CDD", OracleDbType.Int64, 10);
                com.Parameters.Add("p_Id_Documento4_CDD", OracleDbType.Int64, 10);
                //-lt ID_ENTIDAD CREADA
                com.Parameters.Add("p_Id_Entidad", OracleDbType.Int64, 10);
                //-lt BOCA RECEPCION
                com.Parameters.Add("p_Id_Organismo", OracleDbType.Int64, 10);

                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);

                com.Parameters["pEnt_CUIT_FORMAT"].Value = cuitFormat != "" ? (object)cuitFormat : DBNull.Value;
                com.Parameters["pEnt_CUIT"].Value = Alta.CUIT != null ? (object)Alta.CUIT : DBNull.Value;
                com.Parameters["pEnt_Id_Sede"].Value = Alta.IdSede != null ? (object)Alta.IdSede : DBNull.Value;
                com.Parameters["pEnt_Local"].Value = Alta.Local != null ? (object)Alta.Local : DBNull.Value;
                com.Parameters["pEnt_Oficina"].Value = Alta.Oficina != null ? (object)Alta.Oficina : DBNull.Value;
                com.Parameters["pEnt_Stand"].Value = Alta.Stand != null ? (object)Alta.Stand : DBNull.Value;
                com.Parameters["pEnt_Cobertura"].Value = Alta.Cobertura != null ? (object)Alta.Cobertura : DBNull.Value;

                string inmueble = Alta.Propietario != null ? Alta.Propietario : "S";
                com.Parameters["pEnt_Propietario"].Value = inmueble;

                com.Parameters["pEnt_Seguro"].Value = Alta.Seguro != null ? (object)Alta.Seguro : DBNull.Value;
                com.Parameters["pEnt_Latitud"].Value = Alta.DomComercio.LATITUD != null ? (object)Alta.DomComercio.LATITUD : DBNull.Value;
                com.Parameters["pEnt_Longitud"].Value = Alta.DomComercio.LONGITUD != null ? (object)Alta.DomComercio.LONGITUD : DBNull.Value;
                com.Parameters["pEnt_Nro_Sifcos"].Value = Alta.NroSifcos != null ? (object)Alta.NroSifcos : DBNull.Value;
                com.Parameters["pTram_Capac_ult_anio"].Value = Alta.CapacUltAnio != null ? (object)Alta.CapacUltAnio : DBNull.Value;
                com.Parameters["pTram_Cuil_Usu_CIDI"].Value = Alta.CuilUsuarioCidi != null
                    ? (object)Alta.CuilUsuarioCidi
                    : DBNull.Value;
                com.Parameters["pTram_Tipo_Tramite"].Value = Alta.TipoTramite != null ? (object)Alta.TipoTramite : DBNull.Value;
                com.Parameters["pTram_Id_origen_prov"].Value = Alta.IdOrigenProveedor != 0
                    ? (object)Alta.IdOrigenProveedor
                    : DBNull.Value;
                com.Parameters["pTram_Id_vin_dom_legal"].Value = Alta.IdVinDomLegal != 0
                    ? (object)Alta.IdVinDomLegal
                    : DBNull.Value;
                com.Parameters["pTram_Id_vin_dom_local"].Value = Alta.IdVinDomLocal != 0
                    ? (object)Alta.IdVinDomLocal
                    : DBNull.Value;
                com.Parameters["pTram_Rango_alquiler"].Value = Alta.RangoAlquiler != null
                    ? (object)Alta.RangoAlquiler
                    : DBNull.Value;
                com.Parameters["pTram_Cant_total_pers"].Value = Alta.CantTotalpers != null
                    ? (object)Alta.CantTotalpers
                    : DBNull.Value;
                com.Parameters["pTram_CantPersRelDep"].Value = Alta.CantPersRelDep != null
                    ? (object)Alta.CantPersRelDep
                    : DBNull.Value;
                com.Parameters["pTram_Id_Act_Pri"].Value = Alta.idActividadPri != null ? (object)Alta.idActividadPri : DBNull.Value;
                com.Parameters["pTram_Id_Act_Sec"].Value = Alta.idActividadSec != null ? (object)Alta.idActividadSec : DBNull.Value;
                com.Parameters["pTram_FechaVencimiento"].Value = Alta.FechaVencimiento.Date;
                // Alta.FechaVencimiento != null ? Alta.FechaVencimiento : DBNull.Value;
                com.Parameters["pTram_cant_reemp"].Value = Alta.CantidadReempadranamientos;
                // Alta.FechaVencimiento != null ? Alta.FechaVencimiento : DBNull.Value;

                com.Parameters["pRepr_id_cargo"].Value = Alta.IdCargo != 0 ? (object)Alta.IdCargo : DBNull.Value;
                com.Parameters["pRepr_Cuil_RepLegal"].Value = Alta.CuilRepLegal != null ? (object)Alta.CuilRepLegal : DBNull.Value;
                com.Parameters["pGest_Cuil_Gestor"].Value = Alta.CuilGestor != null ? (object)Alta.CuilGestor : DBNull.Value;
                com.Parameters["pGest_id_tipo_gestor"].Value = Alta.IdTipoGestor != 0 ? (object)Alta.IdTipoGestor : DBNull.Value;
                com.Parameters["pSup_Ventas"].Value = Alta.SupVentas != 0 ? (object)Alta.SupVentas : DBNull.Value;
                com.Parameters["pSup_Administracion"].Value = Alta.SupAdministracion != 0
                    ? (object)Alta.SupAdministracion
                    : DBNull.Value;
                com.Parameters["pSup_Deposito"].Value = Alta.SupDeposito != 0 ? (object)Alta.SupDeposito : DBNull.Value;
                com.Parameters["pHest_Id_estado"].Value = Alta.IdEstado != 0 ? (object)Alta.IdEstado : DBNull.Value;
                com.Parameters["p_fecha_ini_act"].Value = Alta.FecIniTramite.Date;
                // Alta.FecIniTramite != null ? (object)Alta.FecIniTramite.ToShortDateString() : DBNull.Value; 
                com.Parameters["p_nro_habMunicipal"].Value = Alta.NroHabilitacion != null
                    ? (object)Alta.NroHabilitacion
                    : DBNull.Value;
                com.Parameters["p_Id_Documento1_CDD"].Value = Alta.Id_Documento1_CDD != null ? (object)Alta.Id_Documento1_CDD : DBNull.Value;
                com.Parameters["p_Id_Documento2_CDD"].Value = Alta.Id_Documento2_CDD != null ? (object)Alta.Id_Documento2_CDD : DBNull.Value;
                com.Parameters["p_Id_Documento3_CDD"].Value = Alta.Id_Documento3_CDD != null ? (object)Alta.Id_Documento3_CDD : DBNull.Value;
                com.Parameters["p_Id_Documento4_CDD"].Value = Alta.Id_Documento4_CDD != null ? (object)Alta.Id_Documento4_CDD : DBNull.Value;
                com.Parameters["p_Id_Entidad"].Value = Alta.DomComercio.ID_ENTIDAD != 0 ? (object)Alta.DomComercio.ID_ENTIDAD : DBNull.Value;
                com.Parameters["p_Id_Organismo"].Value = Alta.IdOrganismo != 0 ? (object)Alta.IdOrganismo : DBNull.Value;

                com.Parameters["pNroTramite"].Direction = ParameterDirection.Output;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var cantFilas = com.ExecuteNonQuery();

                if (cantFilas == 0)
                {
                    nroTramiteNuevo = null;
                    return "ERROR, NO SE GUARDÓ LA INSCRIPCIÓN";
                }
                string pResultado = com.Parameters["pResultado"].Value.ToString();
                string pNroTramite = com.Parameters["pNroTramite"].Value.ToString();

                nroTramiteNuevo = pNroTramite;
                string pResultadoProducto = "";
                string pResultadoTrs = "";
                if (pResultado == "OK")
                {
                    foreach (Producto producto in Alta.Productos)
                    {
                        com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_Productos_Tramites", conn);
                        com.CommandType = CommandType.StoredProcedure;

                        com.Parameters.Add("pId_Producto", OracleDbType.Varchar2, 11);
                        com.Parameters["pId_Producto"].Value = producto.IdProducto;

                        com.Parameters.Add("pNro_tramite", OracleDbType.Int64, 10);
                        com.Parameters["pNro_tramite"].Value = Int64.Parse(pNroTramite);

                        com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                        com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                        com.ExecuteNonQuery();
                        pResultadoProducto = com.Parameters["pResultado"].Value.ToString();
                    }

                    if (Alta.TipoTramite != 6 && Alta.TipoTramite != 8 && Alta.TipoTramite != 10)
                    {
                        //IB: No evalúo las tasas si el trámite es 6,8,10
                        foreach (Trs trs in Alta.ListaTrs)
                        {
                            com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_Tasas_Tramite", conn);
                            com.CommandType = CommandType.StoredProcedure;

                            com.Parameters.Add("pNro_transaccion", OracleDbType.Varchar2, 20);
                            com.Parameters["pNro_transaccion"].Value = trs.NroTransaccion;

                            com.Parameters.Add("pNro_tramite", OracleDbType.Int64, 10);
                            com.Parameters["pNro_tramite"].Value = Int64.Parse(pNroTramite);


                            /*Si es un ALTA, la trs se imprime al último y se guarda como NO pagada. */
                            /*Si es un REEMPADRONAMIENTO, la trs ya se imprimio al principio y se pagó en rentas, se guarda como pagada='S' */
                            com.Parameters.Add("pPagada", OracleDbType.Varchar2, 2);
                            com.Parameters["pPagada"].Value = Alta.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos ? "N" : "S";

                            com.Parameters.Add("pNroLiquidacionOriginal", OracleDbType.Int64, 10);
                            com.Parameters["pNroLiquidacionOriginal"].Value = DBNull.Value;
                            if (!string.IsNullOrEmpty(trs.NroLiquidacion))
                                com.Parameters["pNroLiquidacionOriginal"].Value = trs.NroLiquidacion;

                            com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                            com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                            com.ExecuteReader();

                            pResultadoTrs = com.Parameters["pResultado"].Value.ToString();
                        }
                    }
                }


                //if (pResultadoProducto == "OK" && pResultadoTrs == "SE INSERTO LA TASA")
                //{
                //    pResultado = "OK";
                //}
                //else
                //{
                //    pResultado = "Error en registrar las tasas y los productos";
                //}
                return pResultado;




            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaRegistrarTramiteParaBajaComercio(InscripcionSifcos Alta, out string nroTramiteNuevo)
        {
            string cuitFormat = Alta.CUIT.Substring(0, 2) + "-" + Alta.CUIT.Substring(2, Alta.CUIT.Length - 3) + "-" +
                                Alta.CUIT.Substring(Alta.CUIT.Length - 1, 1);
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_insert_inscripcion", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                //parametros de carga entidad
                com.Parameters.Add("pEnt_CUIT_FORMAT", OracleDbType.Varchar2, 15);
                com.Parameters.Add("pEnt_CUIT", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pEnt_Id_Sede", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pEnt_Local", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pEnt_Oficina", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pEnt_Stand", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pEnt_Cobertura", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pEnt_Propietario", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pEnt_Seguro", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pEnt_Latitud", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pEnt_Longitud", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pEnt_Nro_Sifcos", OracleDbType.Int64, 10);
                //parametros de carga tramite
                com.Parameters.Add("pTram_Capac_ult_anio", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pTram_Cuil_Usu_CIDI", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pTram_Tipo_Tramite", OracleDbType.Int64, 10);
                com.Parameters.Add("pTram_Id_origen_prov", OracleDbType.Int64, 10);
                com.Parameters.Add("pTram_Id_vin_dom_legal", OracleDbType.Int64, 20);
                com.Parameters.Add("pTram_Id_vin_dom_local", OracleDbType.Int64, 20);
                com.Parameters.Add("pTram_Rango_alquiler", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pTram_Id_Act_Pri", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pTram_Id_Act_Sec", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pTram_Cant_total_pers", OracleDbType.Varchar2, 5);
                com.Parameters.Add("pTram_CantPersRelDep", OracleDbType.Varchar2, 5);
                com.Parameters.Add("pTram_FechaVencimiento", OracleDbType.Date);
                com.Parameters.Add("pTram_cant_reemp", OracleDbType.Int64, 10);

                //parametros de carga representante
                com.Parameters.Add("pRepr_id_cargo", OracleDbType.Int64, 10);
                com.Parameters.Add("pRepr_Cuil_RepLegal", OracleDbType.Varchar2, 11);
                //parametros de carga gestor 
                com.Parameters.Add("pGest_Cuil_Gestor", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pGest_id_tipo_gestor", OracleDbType.Int64, 10);
                com.Parameters.Add("pGest_CodAreaCelConta", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pGest_nroCelConta", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pGest_email_conta", OracleDbType.Varchar2, 50);
                //parametros de carga superficies
                com.Parameters.Add("pSup_Ventas", OracleDbType.Int64, 10);
                com.Parameters.Add("pSup_Administracion", OracleDbType.Int64, 10);
                com.Parameters.Add("pSup_Deposito", OracleDbType.Int64, 10);
                com.Parameters.Add("pHest_Id_estado", OracleDbType.Int64, 10);
                //parametros para agregar en t_comunes 
                com.Parameters.Add("p_nro_habMunicipal", OracleDbType.Varchar2, 15);
                com.Parameters.Add("p_fecha_ini_act", OracleDbType.Date);
                //-lt DOCUMENTACION CDD
                com.Parameters.Add("p_Id_Documento1_CDD", OracleDbType.Int64, 10);
                com.Parameters.Add("p_Id_Documento2_CDD", OracleDbType.Int64, 10);
                com.Parameters.Add("p_Id_Documento3_CDD", OracleDbType.Int64, 10);
                com.Parameters.Add("p_Id_Documento4_CDD", OracleDbType.Int64, 10);
                //-
                //-lt ID_ENTIDAD CREADA
                com.Parameters.Add("p_Id_Entidad", OracleDbType.Int64, 10);

                com.Parameters.Add("p_Id_Organismo", OracleDbType.Int64, 10);
                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);

                com.Parameters["pEnt_CUIT_FORMAT"].Value = cuitFormat != "" ? (object)cuitFormat : DBNull.Value;
                com.Parameters["pEnt_CUIT"].Value = Alta.CUIT != null ? (object)Alta.CUIT : DBNull.Value;
                com.Parameters["pEnt_Id_Sede"].Value = Alta.IdSede != null ? (object)Alta.IdSede : DBNull.Value;
                com.Parameters["pEnt_Local"].Value = Alta.Local != null ? (object)Alta.Local : DBNull.Value;
                com.Parameters["pEnt_Oficina"].Value = Alta.Oficina != null ? (object)Alta.Oficina : DBNull.Value;
                com.Parameters["pEnt_Stand"].Value = Alta.Stand != null ? (object)Alta.Stand : DBNull.Value;
                com.Parameters["pEnt_Cobertura"].Value = Alta.Cobertura != null ? (object)Alta.Cobertura : DBNull.Value;

                com.Parameters["pEnt_Propietario"].Value = null;

                com.Parameters["pEnt_Seguro"].Value = DBNull.Value;
                com.Parameters["pEnt_Latitud"].Value = DBNull.Value;
                com.Parameters["pEnt_Longitud"].Value = DBNull.Value;
                com.Parameters["pEnt_Nro_Sifcos"].Value = Alta.NroSifcos != null ? (object)Alta.NroSifcos : DBNull.Value;
                com.Parameters["pTram_Capac_ult_anio"].Value = DBNull.Value;
                com.Parameters["pTram_Cuil_Usu_CIDI"].Value = Alta.CuilUsuarioCidi != null
                    ? (object)Alta.CuilUsuarioCidi
                    : DBNull.Value;
                com.Parameters["pTram_Tipo_Tramite"].Value = Alta.TipoTramite != null ? (object)Alta.TipoTramite : DBNull.Value;
                com.Parameters["pTram_Id_origen_prov"].Value = DBNull.Value;
                com.Parameters["pTram_Id_vin_dom_legal"].Value = DBNull.Value;
                com.Parameters["pTram_Id_vin_dom_local"].Value = DBNull.Value;
                com.Parameters["pTram_Rango_alquiler"].Value = DBNull.Value;
                com.Parameters["pTram_Cant_total_pers"].Value = DBNull.Value;
                com.Parameters["pTram_CantPersRelDep"].Value = DBNull.Value;
                com.Parameters["pTram_Id_Act_Pri"].Value = DBNull.Value;
                com.Parameters["pTram_Id_Act_Sec"].Value = DBNull.Value;
                com.Parameters["pTram_FechaVencimiento"].Value = Alta.FechaVencimiento.Date;
                // Alta.FechaVencimiento != null ? Alta.FechaVencimiento : DBNull.Value;
                com.Parameters["pTram_cant_reemp"].Value = Alta.CantidadReempadranamientos;
                // Alta.FechaVencimiento != null ? Alta.FechaVencimiento : DBNull.Value;

                com.Parameters["pRepr_id_cargo"].Value = Alta.IdCargo != 0 ? (object)Alta.IdCargo : DBNull.Value;
                com.Parameters["pRepr_Cuil_RepLegal"].Value = Alta.CuilRepLegal != null ? (object)Alta.CuilRepLegal : DBNull.Value;
                com.Parameters["pGest_Cuil_Gestor"].Value = Alta.CuilGestor != null ? (object)Alta.CuilGestor : DBNull.Value;
                com.Parameters["pGest_id_tipo_gestor"].Value = Alta.IdTipoGestor != 0 ? (object)Alta.IdTipoGestor : DBNull.Value;
                com.Parameters["pSup_Ventas"].Value = DBNull.Value;
                com.Parameters["pSup_Administracion"].Value = DBNull.Value;
                com.Parameters["pSup_Deposito"].Value = DBNull.Value;
                com.Parameters["pHest_Id_estado"].Value = 1; //cargado
                com.Parameters["p_fecha_ini_act"].Value = Alta.FecIniTramite.Date;
                // Alta.FecIniTramite != null ? (object)Alta.FecIniTramite.ToShortDateString() : DBNull.Value; 
                com.Parameters["p_nro_habMunicipal"].Value = DBNull.Value;

                com.Parameters["p_Id_Documento1_CDD"].Value = Alta.Id_Documento1_CDD != null ? (object)Alta.Id_Documento1_CDD : DBNull.Value;
                com.Parameters["p_Id_Documento2_CDD"].Value = Alta.Id_Documento2_CDD != null ? (object)Alta.Id_Documento2_CDD : DBNull.Value;
                com.Parameters["p_Id_Documento3_CDD"].Value = Alta.Id_Documento3_CDD != null ? (object)Alta.Id_Documento3_CDD : DBNull.Value;
                com.Parameters["p_Id_Documento4_CDD"].Value = Alta.Id_Documento4_CDD != null ? (object)Alta.Id_Documento4_CDD : DBNull.Value;
                com.Parameters["p_Id_Entidad"].Value = Alta.DomComercio.ID_ENTIDAD != 0 ? (object)Alta.DomComercio.ID_ENTIDAD : DBNull.Value;
                com.Parameters["p_Id_Organismo"].Value = Alta.IdOrganismo != 0 ? (object)Alta.IdOrganismo : DBNull.Value;

                com.Parameters["pNroTramite"].Direction = ParameterDirection.Output;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var cantFilas = com.ExecuteNonQuery();

                if (cantFilas == 0)
                {
                    nroTramiteNuevo = null;
                    return "ERROR, NO SE GUARDÓ LA INSCRIPCIÓN";
                }
                string pResultado = com.Parameters["pResultado"].Value.ToString();
                string pNroTramite = com.Parameters["pNroTramite"].Value.ToString();

                nroTramiteNuevo = pNroTramite;

                return pResultado;




            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Consulta de la tasa que tiene asignado cada tramite para verificar si pago
        /// </summary>
        /// <param name="nroTramite"></param>
        /// <returns>
        /// S: la tasa esta paga
        /// N: la tasa no se pagó
        /// </returns>
        public bool DaTRS_EstaPaga(Int64 nroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_id_transaccion", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();


                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                if (nroTramite != 0)
                    com.Parameters["pNroTramite"].Value = nroTramite;
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                String pId_transaccion = com.Parameters["pResultado"].Value.ToString();

                if (pId_transaccion != "")
                    com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_pago_TRS", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pId_transaccionTasa", OracleDbType.Int64, 10);
                if (pId_transaccion != "")
                    com.Parameters["pId_transaccionTasa"].Value = pId_transaccion;
                else
                    com.Parameters["pId_transaccionTasa"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                da = new OracleDataAdapter(com);

                ds = new DataSet();
                da.Fill(ds);

                String pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "S")
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool DaRegitrarEstado(Int64 nroTramite, Int64 idEstado, String Descripcion, String CUIL_Cidi)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_Estado_Tramite", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                com.Parameters.Add("pNroTramite", OracleDbType.Double, 10);
                if (nroTramite != 0)
                    com.Parameters["pNroTramite"].Value = nroTramite;
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;

                com.Parameters.Add("pDescripcion", OracleDbType.Varchar2, 50);
                if (Descripcion != "")
                    com.Parameters["pDescripcion"].Value = Descripcion;
                else
                    com.Parameters["pDescripcion"].Value = DBNull.Value;

                com.Parameters.Add("pidEstado", OracleDbType.Int64, 10);
                if (idEstado != 0)
                    com.Parameters["pidEstado"].Value = idEstado;
                else
                    com.Parameters["pidEstado"].Value = DBNull.Value;

                com.Parameters.Add("pCuilCidi", OracleDbType.Varchar2, 11);
                if (CUIL_Cidi != "")
                    com.Parameters["pCuilCidi"].Value = CUIL_Cidi;
                else
                    com.Parameters["pCuilCidi"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                String pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Devuelve TRUE si se actualizó con éxito la tasa en T_SIF_TASAS. y FALSE si ocurrió un error.
        /// </summary>
        /// <param name="nroTramite"></param>
        /// <param name="nroTransaccion"></param>
        /// <param name="pagada"></param>
        /// <returns></returns>
        public bool DaActualizarTasas(Int64 nroTramite, String nroTransaccion, string pagada)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_update_Tasas_Tramite", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNro_transaccion", OracleDbType.Varchar2, 20);
                com.Parameters["pNro_transaccion"].Value = nroTransaccion;

                com.Parameters.Add("pNro_tramite", OracleDbType.Int64, 10);
                com.Parameters["pNro_tramite"].Value = nroTramite;

                com.Parameters.Add("pPagada", OracleDbType.Varchar2, 1);
                com.Parameters["pPagada"].Value = pagada;


                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                var resultado = com.Parameters["pResultado"].Value.ToString();

                conn.Close();

                return resultado != "ERROR";


            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Autor: IB Sobrecarga por compatibilidad 
        /// </summary>
        /// <param name="nroTramite"></param>
        /// <param name="nroTransaccion"></param>
        /// <param name="pagada"></param>
        /// <returns></returns>
        public String DaRegistrarTasas(Int64 nroTramite, String nroTransaccion, string pagada)
        {
            return DaRegistrarTasas(nroTramite, nroTransaccion, pagada, null);
        }

        public String DaRegistrarTasas(Int64 nroTramite, String nroTransaccion, string pagada, string nroLiquidacionOriginal)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_Tasas_Tramite", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNro_transaccion", OracleDbType.Varchar2, 20);
                com.Parameters["pNro_transaccion"].Value = nroTransaccion;

                com.Parameters.Add("pNro_tramite", OracleDbType.Int64, 10);
                com.Parameters["pNro_tramite"].Value = nroTramite;


                /*Si es un ALTA, la trs se imprime al último y se guarda como NO pagada. */
                com.Parameters.Add("pPagada", OracleDbType.Varchar2, 2);
                com.Parameters["pPagada"].Value = pagada;

                com.Parameters.Add("pNroLiquidacionOriginal", OracleDbType.Int64, 10);
                com.Parameters["pNroLiquidacionOriginal"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(nroLiquidacionOriginal))
                    com.Parameters["pNroLiquidacionOriginal"].Value = nroLiquidacionOriginal;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                com.ExecuteReader();

                string pResultadoTrs = com.Parameters["pResultado"].Value.ToString();
                return pResultadoTrs;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String DaRegistrarTasas(Int64 nroTramite, String nroTransaccion, TipoTramiteEnum tipoTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_Tasas_Tramite", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNro_transaccion", OracleDbType.Varchar2, 20);
                com.Parameters["pNro_transaccion"].Value = nroTransaccion;

                com.Parameters.Add("pNro_tramite", OracleDbType.Int64, 10);
                com.Parameters["pNro_tramite"].Value = nroTramite;


                /*Si es un ALTA, la trs se imprime al último y se guarda como NO pagada. 
                         En el REEMPADRONAMIENTO la trs debe estar pagada para iniciar el tramite.. y se se guarda como PAGADA= "S" 
                         Además el procedimiento valida si exite el Nro_transacción en vez de insert hace un update*/
                com.Parameters.Add("pPagada", OracleDbType.Varchar2, 2);
                com.Parameters["pPagada"].Value = tipoTramite == TipoTramiteEnum.Instripcion_Sifcos
                    ? "N"
                    : "S"; //  SI ES REEMPADRONAMIENTO DEBE HACER UNA ACTUALIZACIÓN DE T_SIF_TASAS DE LAS TRS ENVIADAS a PAGADAS=S;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                com.ExecuteReader();

                string pResultadoTrs = com.Parameters["pResultado"].Value.ToString();
                return pResultadoTrs;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }



        public string DaAsignarProductosAct(string idProducto, string idActividad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_prod_act", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdProducto", OracleDbType.Varchar2, 10);
                if (idProducto != "")
                    com.Parameters["pIdProducto"].Value = idProducto;
                else
                    com.Parameters["pIdProducto"].Value = DBNull.Value;

                com.Parameters.Add("pIdActividad", OracleDbType.Varchar2, 10);
                if (idActividad != "")
                    com.Parameters["pIdActividad"].Value = idActividad;
                else
                    com.Parameters["pIdActividad"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var r = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();

                conn.Close();

                return pResultado;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaModificarRelacionProdAct(string idProducto, List<Actividad> Actividades)
        {
            string pResultado = "ERROR";
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                foreach (Actividad Act in Actividades)
                {
                    OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_prod_act", conn);
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.Add("pIdProducto", OracleDbType.Varchar2, 10);
                    if (idProducto != "")
                        com.Parameters["pIdProducto"].Value = idProducto;
                    else
                        com.Parameters["pIdProducto"].Value = DBNull.Value;

                    com.Parameters.Add("pIdActividad", OracleDbType.Varchar2, 10);
                    if (Act.Id_Actividad != "")
                        com.Parameters["pIdActividad"].Value = Act.Id_Actividad;
                    else
                        com.Parameters["pIdActividad"].Value = DBNull.Value;

                    com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                    com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                    var r = com.ExecuteNonQuery();
                    pResultado = com.Parameters["pResultado"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();

            }
            return pResultado;
        }

        public DataTable DaGetSuperficeByNroTramite(Int64 pNroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_SuperficiesByIdTramite", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                if (pNroTramite != 0)
                    com.Parameters["pNroTramite"].Value = pNroTramite;
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetRoles()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Roles", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetOrganismos(String prefijo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Organismos", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pPrefijo", OracleDbType.Varchar2, 200);
                if (prefijo != null)
                    com.Parameters["pPrefijo"].Value = prefijo;
                else
                    com.Parameters["pPrefijo"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetOrganismosByIdOrganismos(String IDOrganismos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Organismos_Filtrados", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pOrganismos", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(IDOrganismos))
                    com.Parameters["pOrganismos"].Value = IDOrganismos;
                else
                    com.Parameters["pOrganismos"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetOrganismosOne(String IDOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Organismos_one", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pIdOrganismo", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(IDOrganismo))
                    com.Parameters["pIdOrganismo"].Value = IDOrganismo;
                else
                    com.Parameters["pIdOrganismo"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// registro el contacto pasandole todos los datos y dentro del procedimiento determina que tipo de comunicacion es
        /// </summary>
        /// <param name="Contacto"></param>
        /// <returns></returns>
        public String DaRegistrarContacto(Comunicacion Contacto)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_insert_comunicacion", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_entidad", OracleDbType.Varchar2, 50);
                if (Contacto.IdEntidad != "")
                    com.Parameters["p_entidad"].Value = Contacto.IdEntidad;
                else
                    com.Parameters["p_entidad"].Value = DBNull.Value;

                com.Parameters.Add("p_email", OracleDbType.Varchar2, 50);
                if (Contacto.EMail != "")
                    com.Parameters["p_email"].Value = Contacto.EMail;
                else
                    com.Parameters["p_email"].Value = DBNull.Value;

                com.Parameters.Add("p_pagWeb", OracleDbType.Varchar2, 50);
                if (Contacto.PagWeb != "")
                    com.Parameters["p_pagWeb"].Value = Contacto.PagWeb;
                else
                    com.Parameters["p_pagWeb"].Value = DBNull.Value;

                com.Parameters.Add("p_nroCel", OracleDbType.Varchar2, 50);
                if (Contacto.NroCel != "")
                    com.Parameters["p_nroCel"].Value = Contacto.NroCel;
                else
                    com.Parameters["p_nroCel"].Value = DBNull.Value;

                com.Parameters.Add("p_CodAreaCel", OracleDbType.Varchar2, 50);
                if (Contacto.CodAreaCel != "")
                    com.Parameters["p_CodAreaCel"].Value = Contacto.CodAreaCel;
                else
                    com.Parameters["p_CodAreaCel"].Value = DBNull.Value;

                com.Parameters.Add("p_nroTelFijo", OracleDbType.Varchar2, 50);
                if (Contacto.NroTelfijo != "")
                    com.Parameters["p_nroTelFijo"].Value = Contacto.NroTelfijo;
                else
                    com.Parameters["p_nroTelFijo"].Value = DBNull.Value;

                com.Parameters.Add("p_CodAreaTelFijo", OracleDbType.Varchar2, 50);
                if (Contacto.CodAreaTelFijo != "")
                    com.Parameters["p_CodAreaTelFijo"].Value = Contacto.CodAreaTelFijo;
                else
                    com.Parameters["p_CodAreaTelFijo"].Value = DBNull.Value;

                com.Parameters.Add("p_Facebook", OracleDbType.Varchar2, 50);
                if (Contacto.Facebook != "")
                    com.Parameters["p_Facebook"].Value = Contacto.Facebook;
                else
                    com.Parameters["p_Facebook"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                    return "OK";
                return "ERROR EN LA INSERCION: " + pResultado;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        public String DaGetNroTasaTramite(String pCUIT, String pIdSexo, String pNroDocumento, String pPaiCodPais,
            Int64 pIdNumero, Int64 pId_concepto, DateTime pFecha_desde, String pCod_ente,
            Int64 pCantidad, float pImporte, String pNro_expediente, String pAnio_expediente, out string nroTransaccionTasa)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                nroTransaccionTasa = null;


                /*Not: el ID_SEXO, NRO_DOC, PAI_COD_PAIS, ID_NUMERO --> NO SE VAN A USAR
                 POQUE SIEMPRE SE VA A IMRPIMIR CON EL CUIT DE LA EMPRESA.*/

                OracleCommand com = new OracleCommand("tasas_servicio.SP_GENERAR_TRANSACCION", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_cuit", OracleDbType.Varchar2, 11);
                if (pCUIT != null)
                    com.Parameters["p_cuit"].Value = pCUIT;
                else
                    return "Valor del CUIT en null.";


                /* Si la tasa se imprime para una persona física.*/
                com.Parameters.Add("p_id_sexo", OracleDbType.Varchar2, 11);
                com.Parameters["p_id_sexo"].Value = "1"; // DBNull.Value;

                com.Parameters.Add("p_nro_documento", OracleDbType.Varchar2, 11);
                com.Parameters["p_nro_documento"].Value = "11222333"; //DBNull.Value;

                com.Parameters.Add("p_pai_cod_pais", OracleDbType.Varchar2, 11);
                com.Parameters["p_pai_cod_pais"].Value = "ARG"; // DBNull.Value;

                com.Parameters.Add("p_id_numero", OracleDbType.Int64, 10);
                com.Parameters["p_id_numero"].Value = 0; //DBNull.Value;


                /*Campos  obligatorios*/
                com.Parameters.Add("p_id_concepto", OracleDbType.Int64, 11);
                com.Parameters["p_id_concepto"].Value = pId_concepto;

                com.Parameters.Add("p_fecha_desde", OracleDbType.Date, 20);
                com.Parameters["p_fecha_desde"].Value = pFecha_desde.Date;

                com.Parameters.Add("p_cod_ente", OracleDbType.Varchar2, 11);
                com.Parameters["p_cod_ente"].Value = pCod_ente;


                /*Campos no obligatorios*/
                com.Parameters.Add("p_cantidad", OracleDbType.Int64, 10);
                if (pCantidad != 0)
                    com.Parameters["p_cantidad"].Value = pCantidad;
                else
                    com.Parameters["p_cantidad"].Value = DBNull.Value;

                com.Parameters.Add("p_importe", OracleDbType.Int64, 10);
                if (float.Parse(pImporte.ToString()) != 0)
                    com.Parameters["p_importe"].Value = pImporte;
                else
                    com.Parameters["p_importe"].Value = DBNull.Value;

                com.Parameters.Add("p_nro_expediente", OracleDbType.Varchar2, 11);
                if (pNro_expediente != null)
                    com.Parameters["p_nro_expediente"].Value = pNro_expediente;
                else
                    com.Parameters["p_nro_expediente"].Value = DBNull.Value;

                com.Parameters.Add("p_anio_expediente", OracleDbType.Varchar2, 10);
                if (pAnio_expediente != null)
                    com.Parameters["p_anio_expediente"].Value = pAnio_expediente;
                else
                    com.Parameters["p_anio_expediente"].Value = DBNull.Value;


                /* Parámetros de salida */
                com.Parameters.Add("o_nro_transaccion", OracleDbType.Varchar2, 20);
                com.Parameters["o_nro_transaccion"].Direction = ParameterDirection.Output;

                com.Parameters.Add("o_resultado", OracleDbType.Varchar2, 11);
                com.Parameters["o_resultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["o_resultado"].Value.ToString();
                conn.Close();
                switch (pResultado)
                {
                    case "OK":
                        nroTransaccionTasa = com.Parameters["o_nro_transaccion"].Value.ToString();
                        return "OK";
                    case "TRS_NUL":
                        return "Debe especificar una persona jurídica o una persona física.";
                    case "JUR_NOT":
                        return "La persona jurídica no es válida.";
                    case "FIS_NOT":
                        return "La persona física no es válida.";
                    case "TRS_CON":
                        return "El concepto especificado no es válido o no esta vigente.";
                    case "TRS_ORG":
                        return "El código de ente especificado es inválido.";
                    case "TRS_SUB":
                        return "El concepto especificado no es aplicable ya que posee subconceptos.";
                    case "TRS_IMP":
                        return "Para el concepto seleccionado debe especificar un importe válido.";
                    case "TRS_CAN":
                        return "Para el concepto seleccionado debe especificar una cantidad válida.";
                    case "TRS_BAS":
                        return "Para el concepto seleccionado la cantidad debe ser mayor o igual a la cantidad base del mismo.";
                    case "TRS_RAN":
                        return "Para el concepto seleccionado debe estar en el rango establecido para el mismo.";
                    case "TRS_COR":
                        return "El concepto seleccionado no pertenece al ente especificado.";
                    case "TRS_SEQ":
                        return "Error al obtener la secuencia del ente emisor.";
                    case "TRS_INS":
                        return "Error desconocido al intentar generar la transacción.";
                }

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return "ERROR";
        }

        /// <summary>
        /// Carga un domicilio dom_manager y devuelve el id_vinculo del nuevo domicilio
        /// </summary>
        /// <param name="idEntidad">Está compuesto por el CUIT + ID_SEDE.</param>
        /// <param name="idVin">parametro de salida que se usa para cargar el id_vinculo del domicilio generado</param>
        /// <returns></returns>
        public string CargarDomicilio(string idEntidad, string idProvincia, string idDepartamento, string nombreLocalidad,
            string idTipoCalle,
            string nombreTipoCalle, string idCalle, string nombreCalle, string idBarrio, string nombreBarrio, string idPrecinto,
            string altura,
            string piso, string dpto, string torre, string idLocalidad, string codPostal, string manzana, string lote,
            out int? idVin)
        {

            var sql = new StringBuilder();
            sql.Append("DOM_MANAGER.DOM_DOMICILIO.insert_domicilio_cp_mzna_lote");

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                var com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_id_aplicacion", OracleDbType.Int32, 11); //
                com.Parameters.Add("p_id_tipodom", OracleDbType.Int32, 8);
                com.Parameters.Add("p_id_entidad", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_id_provincia", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_id_departamento", OracleDbType.Int32, 11);
                com.Parameters.Add("p_id_localidad", OracleDbType.Int32, 11);
                com.Parameters.Add("p_localidad", OracleDbType.Varchar2, 30);
                com.Parameters.Add("p_id_tipocalle", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_tipo_calle", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_id_calle", OracleDbType.Int32, 11);
                com.Parameters.Add("p_calle", OracleDbType.Varchar2, 50);
                com.Parameters.Add("p_id_barrio", OracleDbType.Int32, 11); //
                com.Parameters.Add("p_barrio", OracleDbType.Varchar2, 50); //
                com.Parameters.Add("p_id_precinto", OracleDbType.Int32, 11);
                com.Parameters.Add("p_cpa", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_altura", OracleDbType.Int32, 11);
                com.Parameters.Add("p_piso", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_depto", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_torre", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_mzna", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_lote", OracleDbType.Varchar2, 11);

                com.Parameters["p_id_aplicacion"].Value = 98; //id_app :> sifcos en BD de gobierno. el 152 es de CiDi.
                com.Parameters["p_id_tipodom"].Value = Convert.ToInt32(3); //real   
                com.Parameters["p_id_entidad"].Value = idEntidad; //CUIT + SEDE.
                com.Parameters["p_id_provincia"].Value = idProvincia;
                com.Parameters["p_id_departamento"].Value = Convert.ToInt32(idDepartamento);
                com.Parameters["p_id_localidad"].Value = Convert.ToInt32(idLocalidad);
                com.Parameters["p_localidad"].Value = nombreLocalidad;
                com.Parameters["p_id_tipocalle"].Value = idTipoCalle;
                com.Parameters["p_tipo_calle"].Value = "CALLE";

                if (!string.IsNullOrEmpty(idCalle))
                {
                    com.Parameters["p_id_calle"].Value = Convert.ToInt32(idCalle);
                }
                else
                {
                    com.Parameters["p_id_calle"].Value = DBNull.Value;
                }
                com.Parameters["p_calle"].Value = nombreCalle;

                if (!string.IsNullOrEmpty(idBarrio))
                {
                    com.Parameters["p_id_barrio"].Value = Convert.ToInt32(idBarrio);
                }
                else
                {
                    com.Parameters["p_id_barrio"].Value = DBNull.Value;
                }

                com.Parameters["p_barrio"].Value = nombreBarrio;
                com.Parameters["p_id_precinto"].Value = 0;
                com.Parameters["p_cpa"].Value = codPostal;
                com.Parameters["p_altura"].Value = Convert.ToInt32(altura);
                com.Parameters["p_piso"].Value = piso;
                com.Parameters["p_depto"].Value = dpto;
                com.Parameters["p_torre"].Value = torre;
                com.Parameters["p_mzna"].Value = manzana;
                com.Parameters["p_lote"].Value = lote;


                //parametros de salida
                com.Parameters.Add("o_id_vin", OracleDbType.Int32, 11);
                com.Parameters.Add("o_tipo_dom", OracleDbType.Int32, 11);
                com.Parameters.Add("o_mensaje", OracleDbType.Varchar2, 20);

                com.Parameters["o_id_vin"].Direction = ParameterDirection.Output;
                com.Parameters["o_tipo_dom"].Direction = ParameterDirection.Output;
                com.Parameters["o_mensaje"].Direction = ParameterDirection.Output;


                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                var value = com.Parameters["o_id_vin"].Value;
                var mensaje = com.Parameters["o_mensaje"].Value;
                idVin = value != null ? (int?)int.Parse(value.ToString()) : null;
                conn.Close();
                if (mensaje.ToString() == "OK")
                {
                    return mensaje.ToString();
                }
                else
                {
                    return "ERROR DE BASE DE DATOS: " + mensaje.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            idVin = null;
            return null;
        }


        /// <summary>
        /// AGREGAR PROCEDIMIENTO A BD
        /// </summary>
        /// <param name="pCuit"></param>
        /// <returns></returns>
        public DataTable BlGetEmpresaEnRentas(string pCuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                string cuitFormat = pCuit.Substring(0, 2) + "-" + pCuit.Substring(2, pCuit.Length - 3) + "-" +
                                    pCuit.Substring(pCuit.Length - 1, 1);

                var sql = new StringBuilder();

                sql.Append(" select t.* from tax.vw_inscriptos_industria t ");
                sql.Append(" where t.cuit = '" + cuitFormat + "' ");


                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DAGetIdEntidad(string NroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select t.id_entidad from sifcos.t_sif_tramites_sifcos t  ");

                sql.Append(" where t.nro_tramite_sifcos = " + NroTramite);


                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        


        /// <summary>
        /// Devuelve la cantidad de años y fecha del último tramite Autorizado(estado Verificado en Boca Secretaría) que se realizó para un Nro Sifcos.
        /// </summary>
        /// <param name="cuit"> CUIT de la Empresa que deseamos consultar su deuda.</param>
        /// <param name="nro_sifcos"> Nro de Sifcos de la sucursal del cuit indicado.</param>
        /// <returns></returns>
        public DataTable GetAniosDeudaTRS(string cuit, string nro_sifcos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_AniosDeuda_TRS", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pCuit", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pNro_sifcos", OracleDbType.Int64, 10);
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);

                com.Parameters["pCuit"].Value = cuit;
                com.Parameters["pNro_sifcos"].Value = nro_sifcos;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }

        /// <summary>
        /// obtiene listado de tasas (pagadas en rentas) sin utilizar para reempadronamiento
        /// Sin utilizar: significa que son tasas sin asignar a un tramite.
        /// </summary>
        /// <param name="pCuit"></param>
        /// <returns></returns>
        public DataTable GetTasasPagadasSinUsar(string pCuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_Get_TasasSinUsar", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pCuit", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pCuit"].Value = pCuit;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// obtiene listado de tasas (pagadas en rentas) sin utilizar para alta
        /// Sin utilizar: significa que son tasas sin asignar a un tramite.
        /// </summary>
        /// <param name="pCuit"></param>
        /// <returns></returns>
        public DataTable GetTasasPagadasSinUsarParaAlta(string pCuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_Get_TasasSinUsarAlta", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pCuit", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pCuit"].Value = pCuit;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable BlGetParametrosGral()
        {
            var sql = new StringBuilder();

            sql.Append("  select * from sifcos.T_SIF_PARAMETROS_GRAL   ");

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            OracleCommand com = new OracleCommand(sql.ToString(), conn);
            com.CommandType = CommandType.Text;

            conn.Open();
            com.ExecuteReader();
            OracleDataAdapter da = new OracleDataAdapter(com);

            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            return ds.Tables[0];

        }

        /// <summary>
        /// Consulta si para el cuit existe al menos un tramite realizado. Trae todos los tramites del CUIT excepto los dados de baja. Es decir si el cuit tuvo un nro_sifcos dado de baja no va a traer los tramites de dicho nro sifcos ni el nro sifcos tampoco.
        /// </summary>
        /// <param name="cuit"></param>
        /// <returns></returns>
        public DataTable BlGetEntidadTramite(string cuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_EntidadTramite", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pCUIT", OracleDbType.Varchar2, 20);

                if (cuit != null)
                    com.Parameters["pCUIT"].Value = cuit;
                else
                    com.Parameters["pCUIT"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// devuelve una tabla con las direcciones del comercio que se necesita para solicitar 
        /// reimpresion de oblea y/o certificado.
        /// </summary>
        /// <param name="pCuit"></param>
        /// <param name="NroSifcos"></param>
        /// <returns></returns>
        public DataTable DAGetDireccionesSedes(String pCuit, Int64 NroSifcos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_direcciones_sedes", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_Cuit", OracleDbType.Varchar2, 20);

                if (pCuit != null)
                    com.Parameters["P_Cuit"].Value = pCuit;
                else
                    com.Parameters["P_Cuit"].Value = DBNull.Value;

                com.Parameters.Add("P_Nro_sifcos", OracleDbType.Int64, 10);

                if (NroSifcos != 0)
                    com.Parameters["P_Nro_sifcos"].Value = NroSifcos;
                else
                    com.Parameters["P_Nro_sifcos"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// devuelve una tabla los tramites que efectuarion en un numero de CUIT
        /// opcional se carga el nro_sifcos
        /// </summary>
        /// <param name="pCuit"></param>
        /// <param name="NroSifcos"></param>
        /// <returns></returns>
        public DataTable DAGetTramitesSifcos(String pCuit, Int64 NroSifcos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_tram_sifcos", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_Cuit", OracleDbType.Varchar2, 20);

                if (pCuit != null)
                    com.Parameters["P_Cuit"].Value = pCuit;
                else
                    com.Parameters["P_Cuit"].Value = DBNull.Value;

                com.Parameters.Add("P_Nro_sifcos", OracleDbType.Int64, 10);

                if (NroSifcos != 0)
                    com.Parameters["P_Nro_sifcos"].Value = NroSifcos;
                else
                    com.Parameters["P_Nro_sifcos"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Devuelve una tabla con las actividades relacionadas a la lista de actividades enviadas por parámetro
        /// AGREGAR PROCEDIMIENTO A BD
        /// </summary>
        /// <param name="idsProductos"></param>
        /// <returns></returns>
        public DataTable BlGetActividadesProducto(List<int> idsProductos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                string ids = string.Join(",", idsProductos);


                sql.Append("  select distinct a.id_actividad , a.n_actividad   ");
                sql.Append("  FROM t_comunes.t_actividades a   ");
                sql.Append("  join sifcos.t_sif_productos_actividad pa on a.id_actividad = pa.id_actividad_clanae  ");
                sql.Append("  join sifcos.t_sif_productos p on pa.id_producto = p.id_producto  ");
                if (idsProductos.Count == 0)
                {
                    sql.Append("  where 1=0 "); //obligo a q traiga vacio.
                }
                else
                {
                    sql.Append("  where p.id_producto in (" + ids + ")  ");
                }


                sql.Append("  order by a.n_actividad asc  ");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// devuelve todas las actividades que tiene asociados los productos que paso por parametro
        /// </summary>
        /// <param name="Producto"></param>
        /// <returns></returns>
        public DataTable DaGetProductosPorAct(String Producto)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Productos_y_Act", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pProd", OracleDbType.Varchar2, 100);

                if (!string.IsNullOrEmpty(Producto))
                    com.Parameters["pProd"].Value = Producto;
                else
                    com.Parameters["pProd"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// devuelve todos los productos que tiene la actividad que se ingreso por parametro
        /// </summary>
        /// <param name="Actividad"></param>
        /// <returns></returns>
        public DataTable DaGetProductosPorAct2(String Actividad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Productos_y_Act_2", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pAct", OracleDbType.Varchar2, 100);

                if (!string.IsNullOrEmpty(Actividad))
                    com.Parameters["pAct"].Value = Actividad;
                else
                    com.Parameters["pAct"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        public DataTable DaPoseeDeuda(long nroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_trs_vigentes", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 20);

                if (nroTramite != 0)
                    com.Parameters["pNroTramite"].Value = nroTramite;
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetTramitesPorEstado(string fechaDesde, string fechaHasta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_tramites_tot_estados", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_fecha_desde", OracleDbType.Varchar2, 10);

                if (!string.IsNullOrEmpty(fechaDesde))
                    com.Parameters["p_fecha_desde"].Value = fechaDesde;
                else
                    com.Parameters["p_fecha_desde"].Value = DBNull.Value;

                com.Parameters.Add("p_fecha_hasta", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(fechaHasta))
                    com.Parameters["p_fecha_hasta"].Value = fechaHasta;
                else
                    com.Parameters["p_fecha_hasta"].Value = DBNull.Value;


                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetTramitesPorActividad(string fechaDesde, string fechaHasta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_tramites_tot_act", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_fecha_desde", OracleDbType.Varchar2, 10);

                if (!string.IsNullOrEmpty(fechaDesde))
                    com.Parameters["p_fecha_desde"].Value = fechaDesde;
                else
                    com.Parameters["p_fecha_desde"].Value = DBNull.Value;

                com.Parameters.Add("p_fecha_hasta", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(fechaHasta))
                    com.Parameters["p_fecha_hasta"].Value = fechaHasta;
                else
                    com.Parameters["p_fecha_hasta"].Value = DBNull.Value;


                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetTramitesPorTipoTramite(string fechaDesde, string fechaHasta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_tramites_tot_tipo_tram", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_fecha_desde", OracleDbType.Varchar2, 10);

                if (!string.IsNullOrEmpty(fechaDesde))
                    com.Parameters["p_fecha_desde"].Value = fechaDesde;
                else
                    com.Parameters["p_fecha_desde"].Value = DBNull.Value;

                com.Parameters.Add("p_fecha_hasta", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(fechaHasta))
                    com.Parameters["p_fecha_hasta"].Value = fechaHasta;
                else
                    com.Parameters["p_fecha_hasta"].Value = DBNull.Value;


                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetTramitesPorLocalidad(string fechaDesde, string fechaHasta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_tramites_tot_loc", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_fecha_desde", OracleDbType.Varchar2, 10);

                if (!string.IsNullOrEmpty(fechaDesde))
                    com.Parameters["p_fecha_desde"].Value = fechaDesde;
                else
                    com.Parameters["p_fecha_desde"].Value = DBNull.Value;

                com.Parameters.Add("p_fecha_hasta", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(fechaHasta))
                    com.Parameters["p_fecha_hasta"].Value = fechaHasta;
                else
                    com.Parameters["p_fecha_hasta"].Value = DBNull.Value;


                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Busca en la base de datos del SIFCoS nuevo la fecha que se va a vencer un tramite de ALTA O REEMPADRONAMIENTO.
        /// </summary>
        /// <param name="nroSifcos"></param>
        /// <returns></returns>
        public DateTime? DaGetFechaUltimoTramite_SifcosNuevo(string nroSifcos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_fec_ult_tramite ", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pNroSifcos", OracleDbType.Varchar2, 12);
                if (nroSifcos != null)
                    com.Parameters["pNroSifcos"].Value = nroSifcos;
                else
                    com.Parameters["pNroSifcos"].Value = DBNull.Value;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows[0]["FEC_ULTIMO_VENCIMIENTO"].ToString() != "")
                        return DateTime.Parse(ds.Tables[0].Rows[0]["FEC_ULTIMO_VENCIMIENTO"].ToString());

                return null;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }



        }

        /// <summary>
        /// Retorna la fecha que se va a vencer el tramite del sifcos viejo
        /// </summary>
        /// <param name="nroSifcos"></param>
        /// <returns></returns>
        public DateTime? DaGetFechaUltimoTramite_SifcosViejo(string nroSifcos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_fec_ult_tramite_viejo ", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pNroSifcos", OracleDbType.Varchar2, 12);
                if (nroSifcos != null)
                    com.Parameters["pNroSifcos"].Value = nroSifcos;
                else
                    com.Parameters["pNroSifcos"].Value = DBNull.Value;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows[0]["FEC_ULTIMO_VENCIMIENTO"].ToString() != "")
                        return DateTime.Parse(ds.Tables[0].Rows[0]["FEC_ULTIMO_VENCIMIENTO"].ToString());

                return null;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Guarda una nueva entidad en el sifcos nuevo. Retorna el id_entidad generado . Retorna 0 (cero) si no se guardó y ocurrió un error.
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string DaRegistrarEntidad(InscripcionSifcos Alta, out Int32 idEntidad)
        {
            string cuitFormat = Alta.CUIT.Substring(0, 2) + "-" + Alta.CUIT.Substring(2, Alta.CUIT.Length - 3) + "-" +
                                 Alta.CUIT.Substring(Alta.CUIT.Length - 1, 1);
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_insert_entidad", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                //parametros de carga entidad
                com.Parameters.Add("pCUIT_FORMAT", OracleDbType.Varchar2, 15);
                com.Parameters.Add("pCUIT", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pId_sede", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pLocal", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pOficina", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pStand", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pCobertura", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pSeguro", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pPropietario", OracleDbType.Varchar2, 3);
                com.Parameters.Add("p_Latitud", OracleDbType.Varchar2, 20);
                com.Parameters.Add("p_Longitud", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pNro_sifcos", OracleDbType.Int64, 10);
                com.Parameters.Add("pResenia", OracleDbType.Varchar2, 100);
                com.Parameters.Add("pNombre_fantasia", OracleDbType.Varchar2, 100);
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);

                com.Parameters["pCUIT_FORMAT"].Value = cuitFormat != "" ? (object)cuitFormat : DBNull.Value;
                com.Parameters["pCUIT"].Value = Alta.CUIT != null ? (object)Alta.CUIT : DBNull.Value;
                com.Parameters["pId_sede"].Value = Alta.IdSede != null ? (object)Alta.IdSede : DBNull.Value;
                com.Parameters["pLocal"].Value = Alta.Local != null ? (object)Alta.Local : DBNull.Value;
                com.Parameters["pOficina"].Value = Alta.Oficina != null ? (object)Alta.Oficina : DBNull.Value;
                com.Parameters["pStand"].Value = Alta.Stand != null ? (object)Alta.Stand : DBNull.Value;
                com.Parameters["pCobertura"].Value = Alta.Cobertura != null ? (object)Alta.Cobertura : DBNull.Value;
                com.Parameters["pSeguro"].Value = Alta.Seguro != null ? (object)Alta.Seguro : DBNull.Value;
                com.Parameters["pPropietario"].Value = Alta.Propietario != null ? (object)Alta.Propietario : DBNull.Value;
                com.Parameters["p_Latitud"].Value = Alta.Latitud != null ? (object)Alta.Latitud : DBNull.Value;
                com.Parameters["p_Longitud"].Value = Alta.Longitud != null ? (object)Alta.Longitud : DBNull.Value;
                com.Parameters["pNro_sifcos"].Value = Alta.NroSifcos != null ? (object)Alta.NroSifcos : DBNull.Value;
                com.Parameters["pResenia"].Value = DBNull.Value;
                com.Parameters["pNombre_fantasia"].Value = Alta.NombreComercio != null ? (object)Alta.NombreComercio : DBNull.Value;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var cantFilas = com.ExecuteNonQuery();


                if (cantFilas == 0)
                {
                    idEntidad = 0;
                    return "ERROR, NO SE GUARDÓ LA ENTIDAD";
                }

                idEntidad = Convert.ToInt32(com.Parameters["pResultado"].Value.ToString());
                return "OK";

            }
            catch (Exception ex)
            {
                idEntidad = 0;
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaRegistrarEntidadMigracion(InscripcionSifcos Alta, out Int32 idEntidad)
        {
            string cuitFormat = Alta.CUIT.Substring(0, 2) + "-" + Alta.CUIT.Substring(2, Alta.CUIT.Length - 3) + "-" +
                                 Alta.CUIT.Substring(Alta.CUIT.Length - 1, 1);
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_insert_entidad_Migracion", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                //parametros de carga entidad
                com.Parameters.Add("pCUIT_FORMAT", OracleDbType.Varchar2, 15);
                com.Parameters.Add("pCUIT", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pId_sede", OracleDbType.Varchar2, 3);
                com.Parameters.Add("pCobertura", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pSeguro", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pPropietario", OracleDbType.Varchar2, 3);
                com.Parameters.Add("p_Latitud", OracleDbType.Varchar2, 20);
                com.Parameters.Add("p_Longitud", OracleDbType.Varchar2, 20);
                com.Parameters.Add("pNro_sifcos", OracleDbType.Int64, 10);
                com.Parameters.Add("pNombre_fantasia", OracleDbType.Varchar2, 100);
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);

                com.Parameters["pCUIT_FORMAT"].Value = cuitFormat != "" ? (object)cuitFormat : DBNull.Value;
                com.Parameters["pCUIT"].Value = Alta.CUIT != null ? (object)Alta.CUIT : DBNull.Value;
                com.Parameters["pId_sede"].Value = Alta.IdSede != null ? (object)Alta.IdSede : DBNull.Value;
                com.Parameters["pCobertura"].Value = Alta.Cobertura != null ? (object)Alta.Cobertura : DBNull.Value;
                com.Parameters["pSeguro"].Value = Alta.Seguro != null ? (object)Alta.Seguro : DBNull.Value;
                com.Parameters["pPropietario"].Value = Alta.Propietario != null ? (object)Alta.Propietario : DBNull.Value;
                com.Parameters["p_Latitud"].Value = Alta.Latitud != null ? (object)Alta.Latitud : DBNull.Value;
                com.Parameters["p_Longitud"].Value = Alta.Longitud != null ? (object)Alta.Longitud : DBNull.Value;
                com.Parameters["pNro_sifcos"].Value = Alta.NroSifcos != null ? (object)Alta.NroSifcos : DBNull.Value;
                com.Parameters["pNombre_fantasia"].Value = Alta.NombreComercio != null ? (object)Alta.NombreComercio : DBNull.Value;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var cantFilas = com.ExecuteNonQuery();


                if (cantFilas == 0)
                {
                    idEntidad = 0;
                    return "ERROR, NO SE GUARDÓ LA ENTIDAD";
                }

                idEntidad = Convert.ToInt32(com.Parameters["pResultado"].Value.ToString());
                return "OK";

            }
            catch (Exception ex)
            {
                idEntidad = 0;
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// AGREGAR PROCEDIMIENTO A BD
        /// </summary>
        /// <param name="nroReferencia"></param>
        /// <returns></returns>
        public DataTable BlGetTasaByNroReferencia(string nroReferencia)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                /*
                sql.Append(" select * from trs.vt_transacciones_verticales vt ");
                sql.Append(" where vt.nroliquidacionoriginal = '" + nroReferencia   +"' ");
                */

                sql.Append(@" select * from  (  select obligacion,nroliquidacionoriginal,numeroreferencia,id_transaccion, 
id_concepto, n_concepto, cuit , fecha_emision, fecha_vencimiento, fecha_cobro, fecha_rendicion, importe_total, n_ente, pagado, ente_recaudador   
FROM TRS.VT_TRANSACCIONES_VERTICALES 
union 
select  obligacion,nroliquidacionoriginal,numeroreferencia, id_transaccion, 
id_concepto,n_concepto, cuit , fecha_emision, fecha_vencimiento, fecha_cobro, fecha_rendicion, importe_total, n_ente, pagado, ente_recaudador
FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) vt ");
                sql.Append(" where vt.nroliquidacionoriginal = '" + nroReferencia + "' ");




                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// AGREGAR PROCEDIMIENTO A BD
        /// </summary>
        /// <param name="nroReferencia"></param>
        /// <returns></returns>
        public string LiberarTasa(string nroReferencia)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" delete from sifcos.t_pagos_trs t ");
                sql.Append(" WHERE T.NRO_REFERENCIA = '" + nroReferencia + "' ");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;
                conn.Open();


                var r = com.ExecuteNonQuery();
                conn.Close();

                return "Se Liberó con éxito la tasa " + nroReferencia;

            }
            catch (Exception ex)
            {
                conn.Close();
                return "No se pudo liberar la tasa " + nroReferencia;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// AGREGAR PROCEDIMIENTO A BD
        /// </summary>
        /// <param name="nroReferencia"></param>
        /// <returns></returns>
        public string LiberarTasaNS(string nroTramite, string nroTransaccion)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_update_tasas_mal_asig", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNro_transaccion", OracleDbType.Varchar2, 20);
                com.Parameters["pNro_transaccion"].Value = nroTransaccion;

                com.Parameters.Add("pNro_tramite", OracleDbType.Int64, 10);
                com.Parameters["pNro_tramite"].Value = nroTramite;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                var resultado = com.Parameters["pResultado"].Value.ToString();

                conn.Close();

                return resultado;

            }
            catch (Exception ex)
            {
                return "Error";
                conn.Close();

            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// procedimiento para consultar todas las tasas pagadas por el cuit ingresado
        /// </summary>
        /// <param name="cuit"></param>
        /// <returns></returns>
        public DataTable BlGetTasaByCUIT(string cuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_tasas_by_cuit ", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pCuit", OracleDbType.Varchar2, 12);
                com.Parameters["pCuit"].Value = DBNull.Value;
                if (cuit != null)
                    com.Parameters["pCuit"].Value = cuit;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// PROCEDIMIENTO QUE BUSCA LAS TASAS POR NUMERO DE REFERENCIA EN EL SISTEMA VIEJO DE SIFCOS
        /// </summary>
        /// <param name="nroReferencia"></param>
        /// <returns></returns>
        public DataTable DaGetTasaSifcosViejoByNroReferencia(string nroReferencia)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_tasas_Viejo_Sif_by_ref", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pNroReferencia", OracleDbType.Varchar2, 50);
                com.Parameters["pNroReferencia"].Value = DBNull.Value;
                if (nroReferencia != null)
                    com.Parameters["pNroReferencia"].Value = nroReferencia;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// PROCEDIMIENTO QUE BUSCA LAS TASAS QUE SE ENCUENTRAN EN EL NUEVO SISTEMA POR NUMERO DE LIQUIDACION ORIGINAL
        /// </summary>
        /// <param name="nroReferencia"></param>
        /// <returns></returns>
        public DataTable DaGetTasaSifcosNuevoByNroReferencia(string nroReferencia)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_tasas_Nuevo_Sif_by_ref", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pNroReferencia", OracleDbType.Varchar2, 50);
                com.Parameters["pNroReferencia"].Value = DBNull.Value;
                if (nroReferencia != null)
                    com.Parameters["pNroReferencia"].Value = nroReferencia;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        public string RegistrarEntidadPerJur(string cuit, string razonSocial, string nombreFantasia)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("T_COMUNES.PACK_PERSONA_JURIDICA.INSERTA_PERSJUR", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();


                com.Parameters.Add("p_cuit", OracleDbType.Varchar2, 11);
                com.Parameters["p_cuit"].Value = cuit;

                com.Parameters.Add("p_razon_social", OracleDbType.Varchar2, 100);
                com.Parameters["p_razon_social"].Value = razonSocial;

                com.Parameters.Add("p_nom_fan", OracleDbType.Varchar2, 50);
                com.Parameters["p_nom_fan"].Value = nombreFantasia;

                com.Parameters.Add("p_id_for_jur", OracleDbType.Varchar2, 50);
                com.Parameters["p_id_for_jur"].Value = DBNull.Value;

                com.Parameters.Add("p_id_cond_iva", OracleDbType.Varchar2, 50);
                com.Parameters["p_id_cond_iva"].Value = DBNull.Value;

                com.Parameters.Add("p_id_aplicacion", OracleDbType.Int64, 10);
                com.Parameters["p_id_aplicacion"].Value = 98;

                com.Parameters.Add("p_fec_inicio_act", OracleDbType.Date);
                com.Parameters["p_fec_inicio_act"].Value = DBNull.Value;

                com.Parameters.Add("p_nro_ing_bruto", OracleDbType.Varchar2, 50);
                com.Parameters["p_nro_ing_bruto"].Value = DBNull.Value;

                com.Parameters.Add("p_id_cond_ingbruto", OracleDbType.Varchar2, 50);
                com.Parameters["p_id_cond_ingbruto"].Value = DBNull.Value;


                com.Parameters.Add("o_id_sede", OracleDbType.Varchar2, 6);
                com.Parameters["o_id_sede"].Direction = ParameterDirection.Output;

                com.Parameters.Add("o_resultado", OracleDbType.Varchar2, 6);
                com.Parameters["o_resultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["o_resultado"].Value.ToString();
                conn.Close();
                return pResultado;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        public DataTable DAConsultarDuplicados(string PFechaDesde, string PFechaHasta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_duplicados", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_fecha_desde", OracleDbType.Varchar2, 20);
                com.Parameters["p_fecha_desde"].Value = PFechaDesde;

                com.Parameters.Add("p_fecha_hasta", OracleDbType.Varchar2, 20);
                com.Parameters["p_fecha_hasta"].Value = PFechaHasta;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public DataTable DaExisteTramite(InscripcionSifcos objetoInscripcion)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_tramite_existe", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pId_vin_dom_local", OracleDbType.Varchar2, 20);
                com.Parameters["pId_Vin_dom_local"].Value = objetoInscripcion.IdVinDomLocal.Value.ToString();

                com.Parameters.Add("pId_vin_dom_legal", OracleDbType.Varchar2, 20);
                com.Parameters["pId_vin_dom_legal"].Value = objetoInscripcion.IdVinDomLegal.Value.ToString();

                com.Parameters.Add("pFecha_Alta", OracleDbType.Varchar2, 20);
                com.Parameters["pFecha_Alta"].Value = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //public DataTable DaConsultaSQL(String SQL)
        //{
        //    OracleConnection conn = new OracleConnection(CadenaDeConexionPersonal());
        //    try
        //    {
        //        var sql = new StringBuilder();

        //        sql.Append(SQL);

        //        OracleCommand com = new OracleCommand(sql.ToString(), conn);
        //        com.CommandType = CommandType.Text;

        //        conn.Open();
        //        com.ExecuteReader();
        //        OracleDataAdapter da = new OracleDataAdapter(com);

        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        conn.Close();
        //        if (ds.Tables.Count > 0)
        //            return ds.Tables[0];

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new daException(ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        public DataTable DaGetTasasAsignadas(String NroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                //String SQL ="select v.nroliquidacionoriginal nro_liquidacion,ta.nro_transaccion,ta.pagada pagado,ta.fecha_alta ";
                //SQL += " from sifcos.t_sif_tasas ta join trs.vt_transacciones_verticales v on ta.nro_transaccion=v.obligacion ";
                //SQL += "WHERE ta.nro_tramite_sifcos= " + NroTramite;

                String SQL = "select v.nroliquidacionoriginal nro_liquidacion,ta.nro_transaccion,v.pagado,ta.fecha_alta ";
                SQL += " from sifcos.t_sif_tasas ta join trs.vt_transacciones_verticales v on v.obligacion=ta.nro_transaccion ";
                SQL += " WHERE ta.nro_tramite_sifcos= " + NroTramite;

                sql.Append(SQL);

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetUltimoTramiteSifcosNuevo(string nroSifcos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_ultimo_tramite", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroSifcos", OracleDbType.Int64, 10);
                com.Parameters["pNroSifcos"].Value = int.Parse(nroSifcos);

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }


        public DataTable DaGetGestor(int id_gestor)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Gestor", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdGestor", OracleDbType.Int64, 10);
                com.Parameters["pIdGestor"].Value = id_gestor;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable BlGetActividades()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_actividades", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        public DataTable BlGetProductosSinAsignar()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Productos_sin_asignar", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaRegistrarBajaComercio(string nroSifcos, string cuil_usuario_cidi)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_insert_baja_tramite", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                //parametros de carga entidad
                com.Parameters.Add("pEnt_Nro_sifcos", OracleDbType.Varchar2, 15);
                com.Parameters["pEnt_Nro_sifcos"].Value = nroSifcos;

                com.Parameters.Add("pTram_Cuil_Usu_CIDI", OracleDbType.Varchar2, 11);
                com.Parameters["pTram_Cuil_Usu_CIDI"].Value = cuil_usuario_cidi;

                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                com.Parameters["pNroTramite"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var cantFilas = com.ExecuteNonQuery();

                if (cantFilas == 0)
                {

                    return "ERROR";
                }
                string pResultado = com.Parameters["pResultado"].Value.ToString();
                string pNroTramite = com.Parameters["pNroTramite"].Value.ToString();



                return pResultado;




            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Autor (IB)
        /// Registra la baja de un comercio pero con la fecha cese como parámetro
        /// </summary>
        /// <param name="nroSifcos"></param>
        /// <param name="cuil_usuario_cidi"></param>
        /// <param name="fechaCese"></param>
        /// <returns></returns>
        public string DaRegistrarBajaComercioCese(string nroSifcos, string cuil_usuario_cidi, DateTime fechaCese, int IdDocumentoCDD1, int IdDocumentoCDD2)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_insert_baja_tramite_cese", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                //parametros de carga entidad
                com.Parameters.Add("pEnt_Nro_sifcos", OracleDbType.Varchar2, 15);
                com.Parameters["pEnt_Nro_sifcos"].Value = nroSifcos;

                com.Parameters.Add("pTram_Cuil_Usu_CIDI", OracleDbType.Varchar2, 11);
                com.Parameters["pTram_Cuil_Usu_CIDI"].Value = cuil_usuario_cidi;

                //pFechaCese
                com.Parameters.Add("pFechaCese", OracleDbType.Date);
                com.Parameters["pFechaCese"].Value = fechaCese;

                com.Parameters.Add("pDoc1", OracleDbType.Int64);
                com.Parameters["pDoc1"].Value = IdDocumentoCDD1;

                com.Parameters.Add("pDoc2", OracleDbType.Int64);
                com.Parameters["pDoc2"].Value = IdDocumentoCDD2;

                com.Parameters.Add("pNroTramite", OracleDbType.Int64, 10);
                com.Parameters["pNroTramite"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var cantFilas = com.ExecuteNonQuery();

                if (cantFilas == 0)
                {

                    return "ERROR";
                }
                string pResultado = com.Parameters["pResultado"].Value.ToString();
                string pNroTramite = com.Parameters["pNroTramite"].Value.ToString();



                return pResultado;




            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable BlGetTramitesDeBaja(string nroSifcos, string cuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_TramitesDeBaja", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pCUIT", OracleDbType.Varchar2, 11);
                com.Parameters["pCUIT"].Value = cuit;

                com.Parameters.Add("pNroSifcos", OracleDbType.Varchar2, 10);
                com.Parameters["pNroSifcos"].Value = nroSifcos;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DAReporteGerencial(String pTipoTramite, String pIdEstadoTramite, String pFechaDesde,
            String pFechaHasta, String pCuit,
            String pRazonSocial, String pNroTramiteDesde, String pNroTramiteHasta, String pNroSifcosDesde,
            String pNroSifcosHasta, String pIdOrganismo, String pIdOrganismoPadre, String pIdDepto, String pIdLocalidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_rpt_gerencial", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_TIPO_TRAMITE", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(pTipoTramite) && pTipoTramite != "0")
                    com.Parameters["P_TIPO_TRAMITE"].Value = pTipoTramite;
                else
                    com.Parameters["P_TIPO_TRAMITE"].Value = DBNull.Value;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(pFechaDesde))
                    com.Parameters["P_FECHA_DESDE"].Value = pFechaDesde;
                else
                    com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_FECHA_HASTA", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(pFechaHasta))
                    com.Parameters["P_FECHA_HASTA"].Value = pFechaHasta;
                else
                    com.Parameters["P_FECHA_HASTA"].Value = DBNull.Value;

                com.Parameters.Add("P_CUIT", OracleDbType.Varchar2, 11);
                if (!string.IsNullOrEmpty(pCuit))
                    com.Parameters["P_CUIT"].Value = pCuit;
                else
                    com.Parameters["P_CUIT"].Value = DBNull.Value;

                com.Parameters.Add("P_RAZON_SOCIAL", OracleDbType.Varchar2, 30);
                if (!string.IsNullOrEmpty(pRazonSocial))
                    com.Parameters["P_RAZON_SOCIAL"].Value = pRazonSocial;
                else
                    com.Parameters["P_RAZON_SOCIAL"].Value = DBNull.Value;

                com.Parameters.Add("P_NRO_TRAM_DESDE", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pNroTramiteDesde))
                    com.Parameters["P_NRO_TRAM_DESDE"].Value = pNroTramiteDesde;
                else
                    com.Parameters["P_NRO_TRAM_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_NRO_TRAM_HASTA", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pNroTramiteHasta))
                    com.Parameters["P_NRO_TRAM_HASTA"].Value = pNroTramiteHasta;
                else
                    com.Parameters["P_NRO_TRAM_HASTA"].Value = DBNull.Value;

                com.Parameters.Add("P_NRO_SIFCOS_DESDE", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pNroSifcosDesde))
                    com.Parameters["P_NRO_SIFCOS_DESDE"].Value = pNroSifcosDesde;
                else
                    com.Parameters["P_NRO_SIFCOS_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_NRO_SIFCOS_HASTA", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pNroSifcosHasta))
                    com.Parameters["P_NRO_SIFCOS_HASTA"].Value = pNroSifcosHasta;
                else
                    com.Parameters["P_NRO_SIFCOS_HASTA"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pIdOrganismo) && pIdOrganismo != "0" && pIdOrganismo != "1" && pIdOrganismo != "2" &&
                    pIdOrganismo != "3")
                    com.Parameters["P_ID_ORGANISMO"].Value = pIdOrganismo;
                else
                    com.Parameters["P_ID_ORGANISMO"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_DEPARTAMENTO", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pIdDepto) && pIdDepto != "0")
                    com.Parameters["P_ID_DEPARTAMENTO"].Value = pIdDepto;
                else
                    com.Parameters["P_ID_DEPARTAMENTO"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_LOCALIDAD", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pIdLocalidad) && pIdLocalidad != "0")
                    com.Parameters["P_ID_LOCALIDAD"].Value = pIdLocalidad;
                else
                    com.Parameters["P_ID_LOCALIDAD"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ORG_PADRE", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pIdOrganismoPadre) && pIdOrganismoPadre != "0")
                    com.Parameters["P_ID_ORG_PADRE"].Value = pIdOrganismoPadre;
                else
                    com.Parameters["P_ID_ORG_PADRE"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ESTADO", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pIdEstadoTramite) && pIdEstadoTramite != "0")
                    com.Parameters["P_ID_ESTADO"].Value = pIdEstadoTramite;
                else
                    com.Parameters["P_ID_ESTADO"].Value = DBNull.Value;



                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DAReporteComercios(String pFechaDesde, String pFechaHasta, String pIdDepto, String pIdLocalidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_rpt_comercios", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(pFechaDesde))
                    com.Parameters["P_FECHA_DESDE"].Value = pFechaDesde;
                else
                    com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_FECHA_HASTA", OracleDbType.Varchar2, 10);
                if (!string.IsNullOrEmpty(pFechaHasta))
                    com.Parameters["P_FECHA_HASTA"].Value = pFechaHasta;
                else
                    com.Parameters["P_FECHA_HASTA"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_DEPARTAMENTO", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pIdDepto) && pIdDepto != "0")
                    com.Parameters["P_ID_DEPARTAMENTO"].Value = pIdDepto;
                else
                    com.Parameters["P_ID_DEPARTAMENTO"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_LOCALIDAD", OracleDbType.Varchar2, 20);
                if (!string.IsNullOrEmpty(pIdLocalidad) && pIdLocalidad != "0")
                    com.Parameters["P_ID_LOCALIDAD"].Value = pIdLocalidad;
                else
                    com.Parameters["P_ID_LOCALIDAD"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaActualizarOrganismoAlta(int nroTramite, int idOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_update_id_organismo_alta", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pId_organismo", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pNro_tramite", OracleDbType.Int64, 10);


                com.Parameters["pId_organismo"].Value = idOrganismo.ToString();
                com.Parameters["pNro_tramite"].Value = nroTramite;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteNonQuery();

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Actualiza el ID_ORGANISMO_ALTA del tramite enviado, pero busca el ID_ORGANISMO en la base de datos vieja de sifcos y en la nueva por el NRO_SIFCOS.
        /// </summary>
        /// <param name="nroTramite"></param>
        /// <param name="nroSifcos"></param>
        /// <returns></returns>
        public string DaActualizarOrganismoAltaPorNroSifcos(int nroTramite, int nroSifcos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_update_id_org_nroSifcos", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNro_sifcos", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pNro_tramite", OracleDbType.Int64, 10);


                com.Parameters["pNro_sifcos"].Value = nroSifcos.ToString();
                com.Parameters["pNro_tramite"].Value = nroTramite;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteNonQuery();

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaReporteComercio(string fechaDesde, string fechaHasta, string idDepartamento, string idLocalidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_rpt_comercios", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Varchar2, 12);
                com.Parameters["P_FECHA_DESDE"].Value = fechaDesde;

                com.Parameters.Add("P_FECHA_HASTA", OracleDbType.Varchar2, 12);
                com.Parameters["P_FECHA_HASTA"].Value = fechaHasta;

                com.Parameters.Add("P_ID_DEPARTAMENTO", OracleDbType.Varchar2, 10);
                com.Parameters["P_ID_DEPARTAMENTO"].Value = idDepartamento;

                com.Parameters.Add("P_ID_LOCALIDAD", OracleDbType.Varchar2, 10);
                com.Parameters["P_ID_LOCALIDAD"].Value = idLocalidad;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaConsultarContacto(string nroSifcos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_Consulta_Contacto", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_NRO_SIFCOS", OracleDbType.Varchar2, 20);
                com.Parameters["P_NRO_SIFCOS"].Value = nroSifcos;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Modificado por (IB) 03/2018
        /// Se agregan parámetros para filtrar directamente en el stored 
        /// </summary>
        /// <param name="IdOrganismo">Organismo logueado. Para nivel de visibilidad</param>
        /// <param name="IdDepartamento">Id departemento ("0" no filtra)</param>
        /// <param name="IdLocalidad">Id departemento ("0" no filtra)</param> 
        /// <param name="IdProducto">Id Producto. Null no filtra</param>
        /// <returns></returns>
        public DataTable DaGetComerciosSIFCoSNuevo(int IdOrganismo, int IdDepartamento, int IdLocalidad, int? IdProducto,
            int? IdRubro)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_mapa", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Int32);
                com.Parameters["P_ID_ORGANISMO"].Value = IdOrganismo;

                com.Parameters.Add("P_ID_DEPARTAMENTO", OracleDbType.Int32);
                com.Parameters["P_ID_DEPARTAMENTO"].Value = IdDepartamento;

                com.Parameters.Add("P_ID_LOCALIDAD", OracleDbType.Int32);
                com.Parameters["P_ID_LOCALIDAD"].Value = IdLocalidad;

                com.Parameters.Add("P_ID_PRODUCTO", OracleDbType.Int32);
                if (IdProducto.HasValue)
                    com.Parameters["P_ID_PRODUCTO"].Value = IdProducto;
                else
                    com.Parameters["P_ID_PRODUCTO"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_RUBRO", OracleDbType.Int32);
                if (IdRubro.HasValue)
                    com.Parameters["P_ID_RUBRO"].Value = IdRubro;
                else
                    com.Parameters["P_ID_RUBRO"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DAGetUbicacionMapa(Int64 NroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_ubicacion_mapa", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 20);
                com.Parameters["pNroTramite"].Value = NroTramite;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        #region Panel de Control

        /// <summary>
        /// Obtiene la cantidad de reempadronamientos pendientes
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Organismo al que pertenece para determinar el nivel de visualización</p aram>
        /// <returns></returns>
        public int DaObtenerCantReempaPendientes(int? IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_tramites_reempapen_tot", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("ID_ORGANISMO_LOGUEADO", OracleDbType.Int32);
                com.Parameters["ID_ORGANISMO_LOGUEADO"].Value = IdOrganismo;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                {
                    int n;
                    DataRow row = ds.Tables[0].Rows[0];

                    bool isNumeric = int.TryParse(row[0].ToString(), out n);
                    if (isNumeric) return n;
                }
                return -1;

                //return null; ;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene el listado de reempadronamientos pendientes
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Organismo al que pertenece para determinar el nivel de visualización</p aram>
        /// <returns></returns>
        public DataTable DaObtenerReempaPendientes(int? IdOrganismo, int pInicio, int pFinal, string pOrder)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_get_tramites_reempapen", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Int32);
                com.Parameters["P_ID_ORGANISMO"].Value = IdOrganismo;

                com.Parameters.Add("P_INICIO", OracleDbType.Int32);
                com.Parameters["P_INICIO"].Value = pInicio;

                com.Parameters.Add("P_FINAL", OracleDbType.Int32);
                com.Parameters["P_FINAL"].Value = pFinal;

                com.Parameters.Add("P_ORDER", OracleDbType.Varchar2);
                com.Parameters["P_ORDER"].Value = pOrder;

                com.Parameters.Add("P_RESULTADO", OracleDbType.RefCursor);
                com.Parameters["P_RESULTADO"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene el listado de ALTAS históricas
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Organismo al que pertenece para determinar el nivel de visualización</p aram>
        /// <returns></returns>
        public DataTable DaObtenerAltasHist(int? IdOrganismo, int pInicio, int pFinal, string pOrder)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.PR_GET_TRAMITES_ALTASHIST", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Int32);
                com.Parameters["P_ID_ORGANISMO"].Value = IdOrganismo;

                com.Parameters.Add("P_INICIO", OracleDbType.Int32);
                com.Parameters["P_INICIO"].Value = pInicio;

                com.Parameters.Add("P_FINAL", OracleDbType.Int32);
                com.Parameters["P_FINAL"].Value = pFinal;

                com.Parameters.Add("P_ORDER", OracleDbType.Varchar2);
                com.Parameters["P_ORDER"].Value = pOrder;

                com.Parameters.Add("P_RESULTADO", OracleDbType.RefCursor);
                com.Parameters["P_RESULTADO"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene la cantidad de altas históricas
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo"></p aram>
        /// <returns></returns>
        public int DaObtenerCantAltasHist(int? IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.PR_GET_TRAMITES_ALTASHIST_TOT", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("ID_ORGANISMO_LOGUEADO", OracleDbType.Int32);
                com.Parameters["ID_ORGANISMO_LOGUEADO"].Value = IdOrganismo;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                {
                    int n;
                    DataRow row = ds.Tables[0].Rows[0];

                    bool isNumeric = int.TryParse(row[0].ToString(), out n);
                    if (isNumeric) return n;
                }
                return -1;

                //return null; ;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene el listado de BAJAS históricas
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Organismo al que pertenece para determinar el nivel de visualización</p aram>
        /// <returns></returns>
        public DataTable DaObtenerBajasHist(int? IdOrganismo, int pInicio, int pFinal, string pOrder)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.PR_GET_TRAMITES_BAJASHIST", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Int32);
                com.Parameters["P_ID_ORGANISMO"].Value = IdOrganismo;

                com.Parameters.Add("P_INICIO", OracleDbType.Int32);
                com.Parameters["P_INICIO"].Value = pInicio;

                com.Parameters.Add("P_FINAL", OracleDbType.Int32);
                com.Parameters["P_FINAL"].Value = pFinal;

                com.Parameters.Add("P_ORDER", OracleDbType.Varchar2);
                com.Parameters["P_ORDER"].Value = pOrder;

                com.Parameters.Add("P_RESULTADO", OracleDbType.RefCursor);
                com.Parameters["P_RESULTADO"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene la cantidad de bajas históricas
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo"></p aram>
        /// <returns></returns>
        public int DaObtenerCantBajasHist(int? IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.PR_GET_TRAMITES_BAJASHIST_TOT", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("ID_ORGANISMO_LOGUEADO", OracleDbType.Int32);
                com.Parameters["ID_ORGANISMO_LOGUEADO"].Value = IdOrganismo;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                {
                    int n;
                    DataRow row = ds.Tables[0].Rows[0];

                    bool isNumeric = int.TryParse(row[0].ToString(), out n);
                    if (isNumeric) return n;
                }
                return -1;

                //return null; ;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene el listado de comercios activos
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Organismo al que pertenece para determinar el nivel de visualización</p aram>
        /// <returns></returns>
        public DataTable DaObtenerComerciosAct(int? IdOrganismo, int pInicio, int pFinal, string pOrder)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.PR_GET_TRAMITES_COM_ACT", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Int32);
                com.Parameters["P_ID_ORGANISMO"].Value = IdOrganismo;

                com.Parameters.Add("P_INICIO", OracleDbType.Int32);
                com.Parameters["P_INICIO"].Value = pInicio;

                com.Parameters.Add("P_FINAL", OracleDbType.Int32);
                com.Parameters["P_FINAL"].Value = pFinal;

                com.Parameters.Add("P_ORDER", OracleDbType.Varchar2);
                com.Parameters["P_ORDER"].Value = pOrder;

                com.Parameters.Add("P_RESULTADO", OracleDbType.RefCursor);
                com.Parameters["P_RESULTADO"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene la cantidad de comercios activos
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo"></p aram>
        /// <returns></returns>
        public int DaObtenerCantComerciosAct(int? IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.PR_GET_TRAMITES_COM_ACT_TOT", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("ID_ORGANISMO_LOGUEADO", OracleDbType.Int32);
                com.Parameters["ID_ORGANISMO_LOGUEADO"].Value = IdOrganismo;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                {
                    int n;
                    DataRow row = ds.Tables[0].Rows[0];

                    bool isNumeric = int.TryParse(row[0].ToString(), out n);
                    if (isNumeric) return n;
                }
                return -1;

                //return null; ;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region exportaciones

        /// <summary>
        /// Obtiene el listado de reempadronamientos pendientes con fines de exportación
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Organismo al que pertenece para determinar el nivel de visualización</p aram>
        /// <returns></returns>
        public DataTable DaExportarReempaPendientes(int? IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.pr_exp_tramites_reempapen", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Int32);
                com.Parameters["P_ID_ORGANISMO"].Value = IdOrganismo;

                com.Parameters.Add("P_RESULTADO", OracleDbType.RefCursor);
                com.Parameters["P_RESULTADO"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene el listado de ALTAS históricas con fines de exportación
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Organismo al que pertenece para determinar el nivel de visualización</p aram>
        /// <returns></returns>
        public DataTable DaExportarAltasHist(int? IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.PR_EXP_TRAMITES_ALTASHIST", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Int32);
                com.Parameters["P_ID_ORGANISMO"].Value = IdOrganismo;

                com.Parameters.Add("P_RESULTADO", OracleDbType.RefCursor);
                com.Parameters["P_RESULTADO"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene el listado de comercios activos con fines de exportación
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Organismo al que pertenece para determinar el nivel de visualización</p aram>
        /// <returns></returns>
        public DataTable DaExportarComerciosAct(int? IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.PR_EXP_TRAMITES_COM_ACT", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Int32);
                com.Parameters["P_ID_ORGANISMO"].Value = IdOrganismo;



                com.Parameters.Add("P_RESULTADO", OracleDbType.RefCursor);
                com.Parameters["P_RESULTADO"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Obtiene el listado de BAJAS históricas  con fines de exportación
        /// Autor: (IB)
        /// </summary>
        /// <param name="IdOrganismo">Organismo al que pertenece para determinar el nivel de visualización</p aram>
        /// <returns></returns>
        public DataTable DaExportarBajasHist(int? IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_REPORTES.PR_EXP_TRAMITES_BAJASHIST", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_FECHA_DESDE", OracleDbType.Date);
                com.Parameters["P_FECHA_DESDE"].Value = DBNull.Value;

                com.Parameters.Add("P_ID_ORGANISMO", OracleDbType.Int32);
                com.Parameters["P_ID_ORGANISMO"].Value = IdOrganismo;



                com.Parameters.Add("P_RESULTADO", OracleDbType.RefCursor);
                com.Parameters["P_RESULTADO"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        /// <summary>
        /// Autor: (IB) 
        /// Obtiene rubroscomercio
        /// </summary>
        /// <param name="prefijo"></param>
        /// <returns></returns>
        public DataTable DaGetRubrosComercio(String prefijo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_Rubros", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pPrefijo", OracleDbType.Varchar2, 100);
                if (prefijo != null)
                    com.Parameters["pPrefijo"].Value = prefijo;
                else
                    com.Parameters["pPrefijo"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Autor: (IB) 
        /// Obtiene rubros por actividad
        /// </summary>
        /// <returns>LIstado de la combinación rubros-actividades</returns>
        public DataTable DaGetRubrosActividad()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_RubrosActividad", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Autor: (IB)
        /// Intenta asociar un rubro a una actividad
        /// No es importante para el proceso si pudo o no hacerlo 
        /// </summary>
        /// <param name="IdRubro"></param>
        /// <param name="IdActividadClanae"></param>
        /// <returns></returns>
        public bool DaInsertRubroActividad(int IdRubro, string IdActividadClanae)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insert_Rubro_Actividad", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdRubro", OracleDbType.Int64, 10);
                com.Parameters.Add("pActividadClanae", OracleDbType.Varchar2, 10);


                com.Parameters["pIdRubro"].Value = IdRubro;
                com.Parameters["pActividadClanae"].Value = IdActividadClanae;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Autor: (IB)
        /// Dado un número de trámite, actualiza rubro primario y secundario
        /// </summary>
        /// <param name="IdRubroPri"></param>
        /// <param name="IdRubroSec"></param>
        /// <param name="pNroTramite"></param>
        /// <returns></returns>
        public string DaActualizarRubrosPriSec(int? IdRubroPri, int? IdRubroSec, int pNroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_update_rubrosPriSec", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdRubroPri", OracleDbType.Int64, 10);
                com.Parameters.Add("pIdRubroSec", OracleDbType.Int64, 10);
                com.Parameters.Add("pNro_tramite", OracleDbType.Int64, 10);

                if (IdRubroPri.HasValue)
                    com.Parameters["pIdRubroPri"].Value = IdRubroPri;
                if (IdRubroSec.HasValue)
                    com.Parameters["pIdRubroSec"].Value = IdRubroSec;

                com.Parameters["pNro_tramite"].Value = pNroTramite;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
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
        public bool DaEmailLibre(string Cuit, string Mail, out string mensaje)
        {
            mensaje = "";
            bool retorno = true;
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_EmailYaUtilizado", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pCuit", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pNroMail", OracleDbType.Varchar2, 60);

                com.Parameters["pCuit"].Value = Cuit;
                com.Parameters["pNroMail"].Value = Mail;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    mensaje = "El mail " + Mail + " ya se encuentra utilizado para el cuit " + ds.Tables[0].Rows[0][0].ToString();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return retorno;
        }

        /// <summary>
        /// Autor: (IB)
        /// Obtiene los conceptos vigentes para una fecha discriminados por tipo de trámite
        /// </summary>
        /// <param name="IdRubroPri"></param>
        /// <returns></returns>
        public DataTable DaGetConceptosAFecha(DateTime fecha)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_conceptosAFecha", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pFecha", OracleDbType.Date, 10);
                com.Parameters["pFecha"].Value = fecha;
                com.Parameters.Add("PCursor", OracleDbType.RefCursor);
                com.Parameters["PCursor"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
                //conn.Close();
                //return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Autor: (IB)
        /// 
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
        public String DaGenerarTRS(String pCUIT, Int64 pId_concepto, String pCod_ente, String pEmail, String CuilUsuarioCIDI,
            Int64 pCantidad, float pImporte,

            out string o_barra1, out string o_barra2, out string o_fecha_venc, out string o_hash_trx, out string o_id_Transaccion,
            out string o_nro_liq_original)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            string pResultado = "";
            try
            {
                o_id_Transaccion = null;

                OracleCommand com = new OracleCommand("TRS.SP_GENERAR_TRANSACCION", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("i_cuit", OracleDbType.Varchar2, 11);
                if (pCUIT != null)
                    com.Parameters["i_cuit"].Value = pCUIT;

                /* Si la tasa se imprime para una persona física.*/
                com.Parameters.Add("i_id_sexo", OracleDbType.Varchar2, 2);
                com.Parameters["i_id_sexo"].Value = DBNull.Value;

                com.Parameters.Add("i_nro_documento", OracleDbType.Varchar2, 15);
                com.Parameters["i_nro_documento"].Value = DBNull.Value;

                com.Parameters.Add("i_pai_cod_pais", OracleDbType.Varchar2, 5);
                com.Parameters["i_pai_cod_pais"].Value = DBNull.Value;

                com.Parameters.Add("i_id_numero", OracleDbType.Int64, 2);
                com.Parameters["i_id_numero"].Value = DBNull.Value;

                /*campos opcionales*/

                com.Parameters.Add("i_titular", OracleDbType.Varchar2, 50);
                com.Parameters["i_titular"].Value = DBNull.Value;

                com.Parameters.Add("i_domicilio", OracleDbType.Varchar2, 100);
                com.Parameters["i_domicilio"].Value = DBNull.Value;

                com.Parameters.Add("i_id_provincia", OracleDbType.Int64, 5);
                com.Parameters["i_id_provincia"].Value = DBNull.Value;

                /*campo no obligatorio si se especifica el i_cuil_cidi */

                com.Parameters.Add("i_email", OracleDbType.Varchar2, 100);
                if (pEmail != null)
                    com.Parameters["i_email"].Value = pEmail;
                else
                    com.Parameters["i_email"].Value = DBNull.Value;

                /*Campos  obligatorios*/

                com.Parameters.Add("i_cod_ente", OracleDbType.Varchar2, 20);
                com.Parameters["i_cod_ente"].Value = pCod_ente;

                com.Parameters.Add("i_id_concepto", OracleDbType.Int64, 20);
                com.Parameters["i_id_concepto"].Value = pId_concepto;

                /*Campos no obligatorios*/

                com.Parameters.Add("i_cantidad", OracleDbType.Int64, 2);
                if (pCantidad != 0)
                    com.Parameters["i_cantidad"].Value = pCantidad;
                else
                    com.Parameters["i_cantidad"].Value = DBNull.Value;

                com.Parameters.Add("i_importe", OracleDbType.Int64, 10);
                if (float.Parse(pImporte.ToString()) != 0)
                    com.Parameters["i_importe"].Value = pImporte;
                else
                    com.Parameters["i_importe"].Value = DBNull.Value;

                /* campo no obligatorio si se especifica i_email */

                com.Parameters.Add("i_cuil_cidi", OracleDbType.Varchar2, 11);
                if (CuilUsuarioCIDI != null)
                    com.Parameters["i_cuil_cidi"].Value = CuilUsuarioCIDI;
                else
                    com.Parameters["i_cuil_cidi"].Value = DBNull.Value;

                com.Parameters.Add("i_valor1", OracleDbType.Varchar2, 100);
                com.Parameters["i_valor1"].Value = DBNull.Value;

                com.Parameters.Add("i_valor2", OracleDbType.Varchar2, 100);
                com.Parameters["i_valor2"].Value = DBNull.Value;

                com.Parameters.Add("i_valor3", OracleDbType.Varchar2, 100);
                com.Parameters["i_valor3"].Value = DBNull.Value;

                com.Parameters.Add("i_valor4", OracleDbType.Varchar2, 100);
                com.Parameters["i_valor4"].Value = DBNull.Value;

                com.Parameters.Add("i_datos_turno", OracleDbType.Varchar2, 100);
                com.Parameters["i_datos_turno"].Value = DBNull.Value;

                /* Parámetros de salida */
                com.Parameters.Add("o_barra1", OracleDbType.Varchar2, 50);
                com.Parameters["o_barra1"].Direction = ParameterDirection.Output;

                com.Parameters.Add("o_barra2", OracleDbType.Varchar2, 50);
                com.Parameters["o_barra2"].Direction = ParameterDirection.Output;

                com.Parameters.Add("o_fecha_venc", OracleDbType.Varchar2, 15);
                com.Parameters["o_fecha_venc"].Direction = ParameterDirection.Output;

                com.Parameters.Add("o_hash_trx", OracleDbType.Varchar2, 20);
                com.Parameters["o_hash_trx"].Direction = ParameterDirection.Output;

                com.Parameters.Add("o_id_transaccion", OracleDbType.Varchar2, 20);
                com.Parameters["o_id_transaccion"].Direction = ParameterDirection.Output;

                com.Parameters.Add("o_nro_liq_original", OracleDbType.Varchar2, 20);
                com.Parameters["o_nro_liq_original"].Direction = ParameterDirection.Output;

                com.Parameters.Add("o_resultado", OracleDbType.Varchar2, 20);
                com.Parameters["o_resultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                pResultado = com.Parameters["o_resultado"].Value.ToString();
                conn.Close();
                o_barra1 = com.Parameters["o_barra1"].Value.ToString();
                o_barra2 = com.Parameters["o_barra2"].Value.ToString();
                o_fecha_venc = com.Parameters["o_fecha_venc"].Value.ToString();
                o_hash_trx = com.Parameters["o_hash_trx"].Value.ToString();
                o_id_Transaccion = com.Parameters["o_id_transaccion"].Value.ToString();
                o_nro_liq_original = com.Parameters["o_nro_liq_original"].Value.ToString();

                switch (pResultado)
                {
                    case "OK":
                        pResultado = "OK";
                        break;
                    case "TRS_CON":
                        pResultado = "El concepto especificado no es válido o no está vigente.";
                        break;
                    case "TRS_ORG":
                        pResultado = "El ente emisor especificado no es válido.";
                        break;
                    case "USU_NOT":
                        pResultado = "El usuario informado es inválido.";
                        break;
                    case "TIT_MAC":
                        pResultado = "La dirección de correo electrónico es obligatoria si no se informa usuario CiDi.";
                        break;
                    case "TRS_NUL":
                        pResultado = "Debe especificar un titular para la tasa retributiva.";
                        break;
                    case "JUR_NOT":
                        pResultado = "La persona jurídica titular especificada no es válida.";
                        break;
                    case "FIS_NOT":
                        pResultado = "La persona física titular especificada no es válida.";
                        break;
                    case "TIT_NOT":
                        pResultado = "El titular especificado para la tasa no es válido.";
                        break;
                    case "PRO_NOT":
                        pResultado = "El identificador de provincia informado no es válido.";
                        break;
                    case "TIT_MAI":
                        pResultado = "La dirección de correo electrónico especificada no es válida.";
                        break;
                    case "TRS_SUB":
                        pResultado = "El concepto especificado no es aplicable ya que posee subconceptos.";
                        break;
                    case "TRS_IMP":
                        pResultado = "El concepto seleccionado requiere de un importe válido.";
                        break;
                    case "TRS_CAN":
                        pResultado = "El concepto seleccionado requiere especificar una cantidad válida.";
                        break;
                    case "TRS_BAS":
                        pResultado =
                            "El concepto seleccionado requiere una cantidad mayor o igual a la cantidad base del mismo.";
                        break;
                    case "TRS_RAN":
                        pResultado = "El concepto seleccionado requiere que la cantidad debe estar en el rango establecido para el mismo.";
                        break;
                    case "TRS_COR":
                        pResultado = "El concepto seleccionado no pertenece al ente especificado.";
                        break;
                    case "TRS_SEQ":
                        pResultado = "Error al obtener la secuencia correspondiente al ente emisor.";
                        break;
                    case "TRS_FVE":
                        pResultado = "Error al establecer la fecha de vencimiento para la tasa a emitir.";
                        break;
                    case "TRS_GET":
                        pResultado = "Error desconocido al obtener los datos de la tasa retributiva generada.";
                        break;
                    case "TRS_INS":
                        pResultado = "Error desconocido al intentar generar la tasa retributiva.";
                        break;
                }

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return pResultado;
        }

        #region Liquidaciones

        /// <summary>
        /// (IB) 032019
        /// Obtiene las liquidaciones por varios filtros
        /// </summary>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public DataTable DaGetLiquidaciones(
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

            Ex = new List<Exception>();
            List<SifLiquidaciones> obj = new List<SifLiquidaciones>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQUIDACIONES_GET", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pIdLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdLiquidacion"].Value = DBNull.Value;
                if (pIdLiquidacion != null)
                    com.Parameters["pIdLiquidacion"].Value = pIdLiquidacion;

                com.Parameters.Add("pNroSifcosDesde", OracleDbType.Int32, 9);
                com.Parameters["pNroSifcosDesde"].Value = DBNull.Value;
                if (pNroSifcosDesde != null)
                    com.Parameters["pNroSifcosDesde"].Value = pNroSifcosDesde;

                com.Parameters.Add("pNroSifcosHasta", OracleDbType.Int32, 9);
                com.Parameters["pNroSifcosHasta"].Value = DBNull.Value;
                if (pNroSifcosHasta != null)
                    com.Parameters["pNroSifcosHasta"].Value = pNroSifcosHasta;

                com.Parameters.Add("pFecDesde", OracleDbType.Date);
                com.Parameters["pFecDesde"].Value = DBNull.Value;
                if (pFecDesde != null)
                    com.Parameters["pFecDesde"].Value = pFecDesde;
                com.Parameters.Add("pFecDesdeHasta", OracleDbType.Date);
                com.Parameters["pFecDesdeHasta"].Value = DBNull.Value;
                if (pFecDesdeHasta != null)
                    com.Parameters["pFecDesdeHasta"].Value = pFecDesdeHasta;

                com.Parameters.Add("pFecHasta", OracleDbType.Date);
                com.Parameters["pFecHasta"].Value = DBNull.Value;
                if (pFecHasta != null)
                    com.Parameters["pFecHasta"].Value = pFecHasta;
                com.Parameters.Add("pFecHastaHasta", OracleDbType.Date);
                com.Parameters["pFecHastaHasta"].Value = DBNull.Value;
                if (pFecHastaHasta != null)
                    com.Parameters["pFecHastaHasta"].Value = pFecHastaHasta;

                com.Parameters.Add("pIdTipoTramite", OracleDbType.Int32, 9);
                com.Parameters["pIdTipoTramite"].Value = DBNull.Value;
                if (pIdTipoTramite != null)
                    com.Parameters["pIdTipoTramite"].Value = pIdTipoTramite;

                com.Parameters.Add("pIdUsuario", OracleDbType.NVarchar2, 30);
                com.Parameters["pIdUsuario"].Value = DBNull.Value;
                if (!String.IsNullOrEmpty(pIdUsuario))
                    com.Parameters["pIdUsuario"].Value = pIdUsuario;

                com.Parameters.Add("pFecAlta", OracleDbType.Date);
                com.Parameters["pFecAlta"].Value = DBNull.Value;
                if (pFecAlta != null)
                    com.Parameters["pFecAlta"].Value = pFecAlta;

                com.Parameters.Add("pFecAltaHasta", OracleDbType.Date);
                com.Parameters["pFecAltaHasta"].Value = DBNull.Value;
                if (pFecAltaHasta != null)
                    com.Parameters["pFecAltaHasta"].Value = pFecAltaHasta;

                com.Parameters.Add("pNroExpediente", OracleDbType.NVarchar2, 50);
                com.Parameters["pNroExpediente"].Value = DBNull.Value;
                if (!String.IsNullOrEmpty(pNroExpediente))
                    com.Parameters["pNroExpediente"].Value = pNroExpediente;

                com.Parameters.Add("pNroResolucion", OracleDbType.NVarchar2, 50);
                com.Parameters["pNroResolucion"].Value = DBNull.Value;
                if (!String.IsNullOrEmpty(pNroResolucion))
                    com.Parameters["pNroResolucion"].Value = pNroResolucion;

                com.Parameters.Add("pFechaResolucion", OracleDbType.Date);
                com.Parameters["pFechaResolucion"].Value = DBNull.Value;
                if (pFechaResolucion != null)
                    com.Parameters["pFechaResolucion"].Value = pFechaResolucion;

                com.Parameters.Add("pFechaResolucionHasta", OracleDbType.Date);
                com.Parameters["pFechaResolucionHasta"].Value = DBNull.Value;
                if (pFechaResolucionHasta != null)
                    com.Parameters["pFechaResolucionHasta"].Value = pFechaResolucionHasta;


                com.Parameters.Add("pCursor", OracleDbType.RefCursor);
                com.Parameters["pCursor"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }

        /// <summary>
        /// (IB) 201903
        /// Obtiene las liquidaciones-organismos por varios filtros
        /// </summary>
        /// <param name="pIdLiqOrganismo"></param>
        /// <param name="pIdOrganismo"></param>
        /// <param name="pIdLiquidacion"></param>
        /// <param name="pTotalLiquidado"></param>
        /// <param name="pCantidad"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public DataTable DaLiqOrganismosGet(
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
            List<SifLiqOrganismos> obj = new List<SifLiqOrganismos>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQ_ORGANISMOS_GET", conn);
                com.CommandType = CommandType.StoredProcedure;

                //Parámetros
                com.Parameters.Add("pIdLiqOrganismo", OracleDbType.Int32, 9);
                com.Parameters["pIdLiqOrganismo"].Value = DBNull.Value;
                if (pIdLiqOrganismo != null)
                    com.Parameters["pIdLiqOrganismo"].Value = pIdLiqOrganismo;

                com.Parameters.Add("pIdOrganismo", OracleDbType.Int32, 9);
                com.Parameters["pIdOrganismo"].Value = DBNull.Value;
                if (pIdOrganismo != null)
                    com.Parameters["pIdOrganismo"].Value = pIdOrganismo;

                com.Parameters.Add("pIdLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdLiquidacion"].Value = DBNull.Value;
                if (pIdLiquidacion != null)
                    com.Parameters["pIdLiquidacion"].Value = pIdLiquidacion;

                com.Parameters.Add("pTotalLiquidado", OracleDbType.Int32, 9);
                com.Parameters["pTotalLiquidado"].Value = DBNull.Value;
                if (pTotalLiquidado != null)
                    com.Parameters["pTotalLiquidado"].Value = pTotalLiquidado;

                com.Parameters.Add("pCantidad", OracleDbType.Int32, 9);
                com.Parameters["pCantidad"].Value = DBNull.Value;
                if (pCantidad != null)
                    com.Parameters["pCantidad"].Value = pCantidad;

                com.Parameters.Add("pIdOrganismoSuperior", OracleDbType.Int32, 9);
                com.Parameters["pIdOrganismoSuperior"].Value = DBNull.Value;
                if (pIdOrganismoSuperior != null)
                    com.Parameters["pIdOrganismoSuperior"].Value = pIdOrganismoSuperior;

                com.Parameters.Add("pCursor", OracleDbType.RefCursor);
                com.Parameters["pCursor"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Obtiene las liquidaciones por organismo superior
        /// </summary>
        /// <param name="pIdLiquidacion"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public DataTable DaLiqOrganismosSupGet(
            int? pIdLiquidacion,
            out List<Exception> Ex
            )
        {
            Ex = new List<Exception>();
            List<SifLiqOrganismos> obj = new List<SifLiqOrganismos>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQ_ORGANISMOSUP_GET", conn);
                com.CommandType = CommandType.StoredProcedure;

                //Parámetros
                com.Parameters.Add("pIdLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdLiquidacion"].Value = DBNull.Value;
                if (pIdLiquidacion != null)
                    com.Parameters["pIdLiquidacion"].Value = pIdLiquidacion;

                com.Parameters.Add("pCursor", OracleDbType.RefCursor);
                com.Parameters["pCursor"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Actualiza una liquidación
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public bool SifLiquidacionesUpdate(SifLiquidaciones obj, out List<Exception> Ex)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            Ex = new List<Exception>();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQUIDACIONES_UPDATE", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdLiquidacion"].Value = obj.IdLiquidacion;

                com.Parameters.Add("pNroSifcosDesde", OracleDbType.Int32, 9);
                com.Parameters["pNroSifcosDesde"].Value = obj.NroSifcosDesde;

                com.Parameters.Add("pNroSifcosHasta", OracleDbType.Int32, 9);
                com.Parameters["pNroSifcosHasta"].Value = obj.NroSifcosHasta;

                com.Parameters.Add("pFecDesde", OracleDbType.Date);
                com.Parameters["pFecDesde"].Value = obj.FecDesde;

                com.Parameters.Add("pFecHasta", OracleDbType.Date);
                com.Parameters["pFecHasta"].Value = obj.FecHasta;

                com.Parameters.Add("pIdTipoTramite", OracleDbType.Int32, 9);
                com.Parameters["pIdTipoTramite"].Value = obj.IdTipoTramite;

                com.Parameters.Add("pIdUsuario", OracleDbType.NVarchar2, 30);
                com.Parameters["pIdUsuario"].Value = obj.IdUsuario;

                com.Parameters.Add("pNroExpediente", OracleDbType.NVarchar2, 50);
                com.Parameters["pNroExpediente"].Value = obj.NroExpediente;

                com.Parameters.Add("pNroResolucion", OracleDbType.NVarchar2, 50);
                com.Parameters["pNroResolucion"].Value = obj.NroResolucion;

                com.Parameters.Add("pFechaResolucion", OracleDbType.Date);
                com.Parameters["pFechaResolucion"].Value = obj.FechaResolucion;



                //conn.Open();
                var res = com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Ex.Add(ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Inserta una liquidacino agrupada por organismos
        /// </summary>
        /// <param name="IdOrganismo"></param>
        /// <param name="IdLiquidacion"></param>
        /// <param name="TotalLiquidado"></param>
        /// <param name="Cantidad"></param>
        /// <returns></returns>
        public String DaInsertLiqOrganismo(int IdOrganismo, int IdLiquidacion, int TotalLiquidado, int Cantidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQ_ORGANISMOS_INSERT", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdLiqOrganismo", OracleDbType.Int32, 9);
                com.Parameters["pIdLiqOrganismo"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pIdOrganismo", OracleDbType.Int32, 9);
                com.Parameters["pIdOrganismo"].Value = IdOrganismo;

                com.Parameters.Add("pIdLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdLiquidacion"].Value = IdLiquidacion;

                com.Parameters.Add("pTotalLiquidado", OracleDbType.Int64, 15);
                com.Parameters["pTotalLiquidado"].Value = TotalLiquidado;

                com.Parameters.Add("pCantidad", OracleDbType.Int32, 9);
                com.Parameters["pCantidad"].Value = Cantidad;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pIdLiqOrganismo"].ToString();

                return pResultado;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// (IB) 201903
        /// Obtiene los trámites liquiddos por varios filtros
        /// <param name="Ex"></param>
        /// <returns></returns>
        public DataTable DaLiqTramitesGet(
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
            List<SifLiqOrganismos> obj = new List<SifLiqOrganismos>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQ_TRAMITES_GET", conn);
                com.CommandType = CommandType.StoredProcedure;

                //Parámetros
                com.Parameters.Add("pIdLiqTramite", OracleDbType.Int32, 9);
                com.Parameters["pIdLiqTramite"].Value = DBNull.Value;
                if (pIdLiqTramite != null)
                    com.Parameters["pIdLiqTramite"].Value = pIdLiqTramite;

                com.Parameters.Add("pIdLiqOrganismo", OracleDbType.Int32, 9);
                com.Parameters["pIdLiqOrganismo"].Value = DBNull.Value;
                if (pIdLiqOrganismo != null)
                    com.Parameters["pIdLiqOrganismo"].Value = pIdLiqOrganismo;

                com.Parameters.Add("pNroTramiteSifcos", OracleDbType.Int32, 9);
                com.Parameters["pNroTramiteSifcos"].Value = DBNull.Value;
                if (pNroTramiteSifcos != null)
                    com.Parameters["pNroTramiteSifcos"].Value = pNroTramiteSifcos;

                com.Parameters.Add("pIdOrganismo", OracleDbType.Int32, 9);
                com.Parameters["pIdOrganismo"].Value = DBNull.Value;
                if (pIdOrganismo != null)
                    com.Parameters["pIdOrganismo"].Value = pIdOrganismo;

                com.Parameters.Add("pIdOrganismoSup", OracleDbType.Int32, 9);
                com.Parameters["pIdOrganismoSup"].Value = DBNull.Value;
                if (pIdOrganismoSup != null)
                    com.Parameters["pIdOrganismoSup"].Value = pIdOrganismoSup;

                com.Parameters.Add("pIdLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdLiquidacion"].Value = DBNull.Value;
                if (pIdLiquidacion != null)
                    com.Parameters["pIdLiquidacion"].Value = pIdLiquidacion;


                com.Parameters.Add("pMontoLiquidado", OracleDbType.Int32, 9);
                com.Parameters["pMontoLiquidado"].Value = DBNull.Value;
                if (pMontoLiquidado != null)
                    com.Parameters["pMontoLiquidado"].Value = pMontoLiquidado;


                com.Parameters.Add("pCursor", OracleDbType.RefCursor);
                com.Parameters["pCursor"].Direction = ParameterDirection.Output;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Inserta trámites correspondientes a una liquidación
        /// </summary>
        /// <param name="IdLiqOrganismo"></param>
        /// <param name="NroTramiteSifcos"></param>
        /// <param name="TotalLiquidado"></param>
        /// <param name="IdOrganismo"></param>
        /// <returns></returns>
        public String DaInsertLiqTramites(int IdLiqOrganismo, int NroTramiteSifcos, int TotalLiquidado, int IdOrganismo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQ_TRAMITES_INSERT", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdLiqTramites", OracleDbType.Int32, 9);
                com.Parameters["pIdLiqTramites"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pIdLiqOrganismo", OracleDbType.Int32, 9);
                com.Parameters["pIdLiqOrganismo"].Value = IdLiqOrganismo;

                com.Parameters.Add("pNroTramiteSifcos", OracleDbType.Int32, 9);
                com.Parameters["pNroTramiteSifcos"].Value = NroTramiteSifcos;

                com.Parameters.Add("pIdOrganismo", OracleDbType.Int32, 9);
                com.Parameters["pIdOrganismo"].Value = IdOrganismo;

                com.Parameters.Add("pMontoLiquidado", OracleDbType.Int32, 9);
                com.Parameters["pMontoLiquidado"].Value = TotalLiquidado;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pIdLiqTramites"].ToString();

                return pResultado;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public int DaLiquidacionesAltasObtenerUltima()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQ_ALTAS_GET_ULTIMA", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pMaxNroSifcos", OracleDbType.Varchar2, 50);
                com.Parameters["pMaxNroSifcos"].Direction = ParameterDirection.Output;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                int MaxNroSifcos = Convert.ToInt32(com.Parameters["pMaxNroSifcos"].Value.ToString());

                return MaxNroSifcos;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Genera las liquidacoines de alta
        /// </summary>
        /// <param name="pNroSifcosDesde"></param>
        /// <param name="pNroSifcosHasta"></param>
        /// <param name="pMensaje"></param>
        /// <returns></returns>
        public int DaGenerarLiquidacionAlta(int pNroSifcosDesde, int pNroSifcosHasta, string Cuit, out String pMensaje)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQ_ALTAS", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdLiquidacion"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pNroSifcosDesde", OracleDbType.Int32, 9);
                com.Parameters["pNroSifcosDesde"].Value = pNroSifcosDesde;

                com.Parameters.Add("pNroSifcosHasta", OracleDbType.Int32, 9);
                com.Parameters["pNroSifcosHasta"].Value = pNroSifcosHasta;

                com.Parameters.Add("pMensaje", OracleDbType.Varchar2, 500);
                com.Parameters["pMensaje"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pIdUsuario", OracleDbType.Varchar2, 11);
                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["pIdUsuario"].Value = Cuit;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                int IdLiquidacion = 0;
                IdLiquidacion = Convert.ToInt32(com.Parameters["pIdLiquidacion"].Value.ToString());
                pMensaje = com.Parameters["pMensaje"].Value.ToString();


                return IdLiquidacion;
            }
            catch (Exception ex)
            {
                pMensaje = ex.Message;
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        public int DaGenerarLiquidacionReempa(DateTime pFecHasta, string Cuit, out String pMensaje)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQ_REEMPA", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdLiquidacion"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pFecHasta", OracleDbType.Date);
                com.Parameters["pFecHasta"].Value = pFecHasta;

                com.Parameters.Add("pMensaje", OracleDbType.Varchar2, 500);
                com.Parameters["pMensaje"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pIdUsuario", OracleDbType.Varchar2, 11);
                if (!String.IsNullOrEmpty(Cuit))
                    com.Parameters["pIdUsuario"].Value = Cuit;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                int IdLiquidacion = 0;
                IdLiquidacion = Convert.ToInt32(com.Parameters["pIdLiquidacion"].Value.ToString());
                pMensaje = com.Parameters["pMensaje"].Value.ToString();


                return IdLiquidacion;
            }
            catch (Exception ex)
            {
                pMensaje = ex.Message;
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// (IB) 201906
        /// Obtiene los tramites de alta que se van a liquidar
        /// <param name="Ex"></param>
        /// <returns></returns>
        public DataTable DaLiqAltasPreviewGet(
            int pNroSifcosDesde,
            int pNroSifcosHasta,
            out List<Exception> Ex)
        {
            Ex = new List<Exception>();
            List<SifLiqOrganismos> obj = new List<SifLiqOrganismos>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_PREV_LIQ_ALTAS", conn);
                com.CommandType = CommandType.StoredProcedure;

                //Parámetros
                com.Parameters.Add("pNroSifcosDesde", OracleDbType.Int32, 9);
                com.Parameters["pNroSifcosDesde"].Value = pNroSifcosDesde;

                com.Parameters.Add("pNroSifcosHasta", OracleDbType.Int32, 9);
                com.Parameters["pNroSifcosHasta"].Value = pNroSifcosHasta;

                com.Parameters.Add("pCursor", OracleDbType.RefCursor);
                com.Parameters["pCursor"].Direction = ParameterDirection.Output;
                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Autor: IB
        /// Obtener el ultimo id liquidación en base al tipo de liquidación de la que se envía por parámetro
        /// </summary>
        /// <param name="IdLiquidacion"></param>
        /// <returns></returns>
        public int DaLiquidacionesObtenerUltima(int IdLiquidacion,
            out List<Exception> Ex)
        {
            Ex = new List<Exception>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQ_GET_ULTIMA", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdLiquidacion"].Value = IdLiquidacion;

                com.Parameters.Add("pIdUltimaLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdUltimaLiquidacion"].Direction = ParameterDirection.Output;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                int IdUltimaLiquidacion = Convert.ToInt32(com.Parameters["pIdUltimaLiquidacion"].Value.ToString());

                return IdUltimaLiquidacion;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Borra una liquidación completa
        /// </summary>
        /// <param name="IdLiquidacion"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public bool BorrarLiquidacionCompleta(int IdLiquidacion, out List<Exception> Ex)
        {
            Ex = new List<Exception>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_LIQUIDACIONES.PR_SIF_LIQ_DEEPDEL", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdLiquidacion", OracleDbType.Int32, 9);
                com.Parameters["pIdLiquidacion"].Value = IdLiquidacion;

                com.Parameters.Add("pMensaje", OracleDbType.Varchar2, 9);
                com.Parameters["pMensaje"].Direction = ParameterDirection.Output;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                string strMensaje = com.Parameters["pMensaje"].Value.ToString();

                if (strMensaje != "OK") return false;

                return true;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region Relevamientos

        /// <summary>
        /// (PC) 112019
        /// Obtiene los relevamientos por varios filtros
        /// </summary>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public DataTable DaGetRelevamientos(
            int? pIdRelevamiento,
            string pNRelevamiento,
            string pCodigo,
            string pNroSifcos,
            string pUsrAlta,
            DateTime? pFecAlta,
            DateTime? pFecAltaHasta,
            out List<Exception> Ex)
        {

            Ex = new List<Exception>();
            List<SifRelevamientos> obj = new List<SifRelevamientos>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {


#if DEBUGxxx
	////SIMULO busqueda de datos:
                DataTable dt1 = new DataTable("t_sif_relevamientos");
                dt1.Columns.Add("ID_RELEVAMIENTO", typeof(Int32));
                dt1.Columns.Add("N_RELEVAMIENTO", typeof(String));
                dt1.Columns.Add("CODIGO", typeof(String));
                dt1.Columns.Add("OBSERVACIONES", typeof(String));
                dt1.Columns.Add("USR_ALTA", typeof(String));
                dt1.Columns.Add("FEC_ALTA", typeof(DateTime));
                dt1.Columns.Add("USR_MODIF", typeof(String));
                dt1.Columns.Add("FEC_MODIF", typeof(DateTime));
                //dt1.Columns.Add("Salary", typeof(Double));

                object[] o1 = { 1, "nrelev 1", "7796853569", "observ 1", "usralta1", DateTime.Now, null, null };
                object[] o2 = { 2, "nrelev 2", "7796853520", "observ 2", "usralta1", DateTime.Now, null, null };
                object[] o3 = { 3, "nrelev 3", "7796853570", "observ 3", "usralta1", DateTime.Now, null, null };

                dt1.Rows.Add(o1);
                dt1.Rows.Add(o2);
                dt1.Rows.Add(o3);

                return dt1;

#else

                OracleCommand com = new OracleCommand("SIFCOS.PCK_RELEVAMIENTOS.PR_SIF_RELEVAMIENTOS_GET", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pIdRelevamiento", OracleDbType.Int32, 9);
                com.Parameters["pIdRelevamiento"].Value = DBNull.Value;
                if (pIdRelevamiento != null)
                    com.Parameters["pIdRelevamiento"].Value = pIdRelevamiento;

                com.Parameters.Add("pNRelevamiento", OracleDbType.NVarchar2, 200);
                com.Parameters["pNRelevamiento"].Value = DBNull.Value;
                if (!String.IsNullOrEmpty(pNRelevamiento))
                    com.Parameters["pNRelevamiento"].Value = pNRelevamiento;

                com.Parameters.Add("pCodigo", OracleDbType.NVarchar2, 25);
                com.Parameters["pCodigo"].Value = DBNull.Value;
                if (!String.IsNullOrEmpty(pCodigo))
                    com.Parameters["pCodigo"].Value = pCodigo;


                //com.Parameters.Add("pNroSifcos", OracleDbType.NVarchar2, 25);
                //com.Parameters["pNroSifcos"].Value = DBNull.Value;
                //if (!String.IsNullOrEmpty(pNroSifcos))
                //    com.Parameters["pNroSifcos"].Value = pNroSifcos;

                com.Parameters.Add("pUsrAlta", OracleDbType.NVarchar2, 30);
                com.Parameters["pUsrAlta"].Value = DBNull.Value;
                if (!String.IsNullOrEmpty(pUsrAlta))
                    com.Parameters["pUsrAlta"].Value = pUsrAlta;

                com.Parameters.Add("pFecAlta", OracleDbType.Date);
                com.Parameters["pFecAlta"].Value = DBNull.Value;
                if (pFecAlta != null)
                    com.Parameters["pFecAlta"].Value = pFecAlta;

                com.Parameters.Add("pFecAltaHasta", OracleDbType.Date);
                com.Parameters["pFecAltaHasta"].Value = DBNull.Value;
                if (pFecAltaHasta != null)
                    com.Parameters["pFecAltaHasta"].Value = pFecAltaHasta;


                com.Parameters.Add("pCursor", OracleDbType.RefCursor);
                com.Parameters["pCursor"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                return ds.Tables[0];
#endif

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }


        /// <summary>
        /// (PC) 112019
        /// Obtiene los detalles de un relevamiento por varios filtros
        /// </summary>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public DataTable DaGetRelevDetalle(
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

            Ex = new List<Exception>();
            List<SifRelevDetalle> obj = new List<SifRelevDetalle>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {


#if DEBUGxxxx
	////SIMULO busqueda de datos:
                DataTable dt1 = new DataTable("t_sif_relevdetalle");
                dt1.Columns.Add("ID_RELEV_DETALLE", typeof(Int32));
                dt1.Columns.Add("ID_RELEVAMIENTO", typeof(Int32));
                dt1.Columns.Add("N_RELEVAMIENTO", typeof(String));
                dt1.Columns.Add("ID_ENTIDAD", typeof(Int32));
                dt1.Columns.Add("N_ENTIDAD", typeof(String));
                dt1.Columns.Add("NRO_SIFCOS", typeof(Int32));
                
                dt1.Columns.Add("CODIGO", typeof(String));
                dt1.Columns.Add("PRECIO", typeof(Double));
                dt1.Columns.Add("FEC_RELEVAMIENTO", typeof(DateTime));
                dt1.Columns.Add("VIGENTE", typeof(Int32));

                dt1.Columns.Add("OBSERVACIONES", typeof(String));
                dt1.Columns.Add("USR_ALTA", typeof(String));
                dt1.Columns.Add("FEC_ALTA", typeof(DateTime));
                dt1.Columns.Add("USR_MODIF", typeof(String));
                dt1.Columns.Add("FEC_MODIF", typeof(DateTime));
                //dt1.Columns.Add("Salary", typeof(Double));

                if (pIdRelevamiento == 1)
                {
                    object[] o1 = { 1, 1, "nrelev 1", 123, "Entidad 123", 151515, "7796853569", 145.23, DateTime.Now, 1, "observ det 1", "usralta1", DateTime.Now, null, null };
                    object[] o2 = { 4, 1, "nrelev 1", 489, "Entidad 489", 474747, "7796853569", 152.23, DateTime.Now, 1, "observ det 4", "usralta1", DateTime.Now, null, null };
                    object[] o3 = { 5, 1, "nrelev 1", 147, "Entidad 147", 159159, "7796853569", 125.78, DateTime.Now, 1, "observ det 5", "usralta1", DateTime.Now, null, null };
                    dt1.Rows.Add(o1);
                    dt1.Rows.Add(o2);
                    dt1.Rows.Add(o3);
                }
                if (pIdRelevamiento == 2)
                {
                    object[] o2 = { 2, 2, "nrelev 2", 234, "Entidad 234", 565656, "7796853569", 256.98, DateTime.Now, 1, "observ det 2", "usralta1", DateTime.Now, null, null };
                    dt1.Rows.Add(o2);
                }
                if (pIdRelevamiento == 3)
                {
                    object[] o2 = { 3, 3, "nrelev 3", 489, "Entidad 489", 474747, "7796853570", 14.25, DateTime.Now, 1, "observ det 3", "usralta1", DateTime.Now, null, null };
                    dt1.Rows.Add(o2);
                }



                return dt1;
               

#else


                OracleCommand com = new OracleCommand("SIFCOS.PCK_RELEVAMIENTOS.PR_SIF_RELEV_DETALLE_GET", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdRelevDetalle", OracleDbType.Int32, 9);
                com.Parameters["pIdRelevDetalle"].Value = DBNull.Value;
                if (pIdRelevDetalle != null)
                    com.Parameters["pIdRelevDetalle"].Value = pIdRelevDetalle;

                com.Parameters.Add("pIdRelevamiento", OracleDbType.Int32, 9);
                com.Parameters["pIdRelevamiento"].Value = DBNull.Value;
                if (pIdRelevamiento != null)
                    com.Parameters["pIdRelevamiento"].Value = pIdRelevamiento;

                com.Parameters.Add("pCodigo", OracleDbType.NVarchar2, 25);
                com.Parameters["pCodigo"].Value = DBNull.Value;
                if (!String.IsNullOrEmpty(pCodigo))
                    com.Parameters["pCodigo"].Value = pCodigo;

                com.Parameters.Add("pIdEntidad", OracleDbType.Int32, 9);
                com.Parameters["pIdEntidad"].Value = DBNull.Value;
                if (pIdEntidad != null)
                    com.Parameters["pIdEntidad"].Value = pIdEntidad;

                com.Parameters.Add("pNroSifcos", OracleDbType.Int32, 9);
                com.Parameters["pNroSifcos"].Value = DBNull.Value;
                if (pNroSifcos != null)
                    com.Parameters["pNroSifcos"].Value = pNroSifcos;

                com.Parameters.Add("pVigente", OracleDbType.Int32, 1);
                com.Parameters["pVigente"].Value = DBNull.Value;
                if (pVigente != null)
                    com.Parameters["pVigente"].Value = pVigente;

                com.Parameters.Add("pUsrAlta", OracleDbType.NVarchar2, 30);
                com.Parameters["pUsrAlta"].Value = DBNull.Value;
                if (!String.IsNullOrEmpty(pUsrAlta))
                    com.Parameters["pUsrAlta"].Value = pUsrAlta;

                com.Parameters.Add("pFecAlta", OracleDbType.Date);
                com.Parameters["pFecAlta"].Value = DBNull.Value;
                if (pFecAlta != null)
                    com.Parameters["pFecAlta"].Value = pFecAlta;

                com.Parameters.Add("pFecAltaHasta", OracleDbType.Date);
                com.Parameters["pFecAltaHasta"].Value = DBNull.Value;
                if (pFecAltaHasta != null)
                    com.Parameters["pFecAltaHasta"].Value = pFecAltaHasta;

                com.Parameters.Add("pCursor", OracleDbType.RefCursor);
                com.Parameters["pCursor"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                return ds.Tables[0];
#endif

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }

        /// <summary>
        /// Actualiza un Relevamiento
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public bool SifRelevamientoInsert(SifRelevamientos obj, out List<Exception> Ex)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            Ex = new List<Exception>();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_RELEVAMIENTOS.PR_SIF_RELEVAMIENTOS_INS", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdRelevamiento", OracleDbType.Int32, 9);
                com.Parameters["pIdRelevamiento"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pNRelevamiento", OracleDbType.Varchar2, 200);
                com.Parameters["pNRelevamiento"].Value = obj.NRelevamiento;

                com.Parameters.Add("pCodigo", OracleDbType.Varchar2, 25);
                com.Parameters["pCodigo"].Value = obj.Codigo;

                com.Parameters.Add("pObservaciones", OracleDbType.Varchar2, 1000);
                com.Parameters["pObservaciones"].Value = obj.Observaciones;

                com.Parameters.Add("pUsrAlta", OracleDbType.NVarchar2, 30);
                com.Parameters["pUsrAlta"].Value = obj.UsrAlta;

                com.Parameters.Add("pFecAlta", OracleDbType.Date);
                com.Parameters["pFecAlta"].Value = obj.FecAlta;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Ex.Add(ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Actualiza un Relevamiento
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public bool SifRelevamientoUpdate(SifRelevamientos obj, out List<Exception> Ex)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            Ex = new List<Exception>();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_RELEVAMIENTOS.PR_SIF_RELEVAMIENTOS_UPD", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdRelevamiento", OracleDbType.Int32, 9);
                com.Parameters["pIdRelevamiento"].Value = obj.IdRelevamiento;

                com.Parameters.Add("pNRelevamiento", OracleDbType.Varchar2, 200);
                com.Parameters["pNRelevamiento"].Value = obj.NRelevamiento;

                com.Parameters.Add("pCodigo", OracleDbType.Varchar2, 25);
                com.Parameters["pCodigo"].Value = obj.Codigo;

                com.Parameters.Add("pObservaciones", OracleDbType.Varchar2, 1000);
                com.Parameters["pObservaciones"].Value = obj.Observaciones;

                com.Parameters.Add("pUsrModif", OracleDbType.NVarchar2, 30);
                com.Parameters["pUsrModif"].Value = obj.UsrModif;

                com.Parameters.Add("pFecModif", OracleDbType.Date);
                com.Parameters["pFecModif"].Value = obj.FecModif;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Ex.Add(ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Actualiza un Relevamiento Detalle
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public bool SifRelevDetalleInsert(SifRelevDetalle obj, out List<Exception> Ex)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            Ex = new List<Exception>();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_RELEVAMIENTOS.PR_SIF_RELEV_DETALLE_INS", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdRelevDetalle", OracleDbType.Int32, 9);
                com.Parameters["pIdRelevDetalle"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pIdEntidad", OracleDbType.Int32, 9);
                com.Parameters["pIdEntidad"].Value = obj.IdEntidad;

                com.Parameters.Add("pIdRelevamiento", OracleDbType.Int32, 9);
                com.Parameters["pIdRelevamiento"].Value = obj.IdRelevamiento;

                com.Parameters.Add("pPrecio", OracleDbType.Decimal, 15);
                com.Parameters["pPrecio"].Value = obj.Precio;

                com.Parameters.Add("pFecRelevamiento", OracleDbType.Date);
                com.Parameters["pFecRelevamiento"].Value = obj.FecRelevamiento;

                com.Parameters.Add("pObservaciones", OracleDbType.Varchar2, 1000);
                com.Parameters["pObservaciones"].Value = obj.Observaciones;

                com.Parameters.Add("pVigente", OracleDbType.Int32, 1);
                com.Parameters["pVigente"].Value = obj.Vigente;

                com.Parameters.Add("pUsrAlta", OracleDbType.NVarchar2, 30);
                com.Parameters["pUsrAlta"].Value = obj.UsrAlta;

                com.Parameters.Add("pFecAlta", OracleDbType.Date);
                com.Parameters["pFecAlta"].Value = obj.FecAlta;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Ex.Add(ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Actualiza un Relevamiento
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public bool SifRelevDetalleUpdate(SifRelevDetalle obj, out List<Exception> Ex)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            conn.Open();
            Ex = new List<Exception>();
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.PCK_RELEVAMIENTOS.PR_SIF_RELEV_DETALLE_UPD", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdRelevDetalle", OracleDbType.Int32, 9);
                com.Parameters["pIdRelevDetalle"].Value = obj.IdRelevDetalle;

                com.Parameters.Add("pIdEntidad", OracleDbType.Int32, 9);
                com.Parameters["pIdEntidad"].Value = obj.IdEntidad;

                com.Parameters.Add("pIdRelevamiento", OracleDbType.Int32, 9);
                com.Parameters["pIdRelevamiento"].Value = obj.IdRelevamiento;

                com.Parameters.Add("pPrecio", OracleDbType.Decimal, 15);
                com.Parameters["pPrecio"].Value = obj.Precio;

                com.Parameters.Add("pFecRelevamiento", OracleDbType.Date);
                com.Parameters["pFecRelevamiento"].Value = obj.FecRelevamiento;

                com.Parameters.Add("pObservaciones", OracleDbType.Varchar2, 1000);
                com.Parameters["pObservaciones"].Value = obj.Observaciones;

                com.Parameters.Add("pVigente", OracleDbType.Int32, 1);
                com.Parameters["pVigente"].Value = obj.Vigente;

                com.Parameters.Add("pUsrModif", OracleDbType.NVarchar2, 30);
                com.Parameters["pUsrModif"].Value = obj.UsrModif;

                com.Parameters.Add("pFecModif", OracleDbType.Date);
                com.Parameters["pFecModif"].Value = obj.FecModif;

                //conn.Open();
                var res = com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Ex.Add(ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// (PC) 112019
        /// Obtiene los datos de entidades a traves de id?entidad o nro_sifcos (tabla t_sif_entidades)
        /// </summary>
        /// <param name="Ex"></param>
        /// <returns></returns>
        public DataTable DaGetEntidades(
            int? pIdEntidad,
            int? pNroSifcos,
            out List<Exception> Ex)
        {

            Ex = new List<Exception>();
            List<SifRelevDetalle> obj = new List<SifRelevDetalle>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_RELEVAMIENTOS.PR_SIF_ENTIDADES_GET", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdEntidad", OracleDbType.Int32, 9);
                com.Parameters["pIdEntidad"].Value = DBNull.Value;
                if (pIdEntidad != null)
                    com.Parameters["pIdEntidad"].Value = pIdEntidad;

                com.Parameters.Add("pNroSifcos", OracleDbType.Int32, 9);
                com.Parameters["pNroSifcos"].Value = DBNull.Value;
                if (pNroSifcos != null)
                    com.Parameters["pNroSifcos"].Value = pNroSifcos;

                com.Parameters.Add("pCursor", OracleDbType.RefCursor);
                com.Parameters["pCursor"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                return ds.Tables[0];


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }

        #endregion

        public DataTable DaGetIdsEntidad(string idLocalidad, string idRubro)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {


                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_IdsEntidad ", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("p_idLocalidad", OracleDbType.Varchar2, 12);
                if (string.IsNullOrEmpty(idLocalidad))
                    com.Parameters["p_idLocalidad"].Value = DBNull.Value;
                else
                    com.Parameters["p_idLocalidad"].Value = idLocalidad;

                com.Parameters.Add("p_idRubro", OracleDbType.Varchar2, 12);

                if (string.IsNullOrEmpty(idRubro))
                    com.Parameters["p_idRubro"].Value = DBNull.Value;
                else
                    com.Parameters["p_idRubro"].Value = idRubro;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                //com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //--lt
        public DataTable DaGetIdDocumentoCDD(int pNroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_CONSULTA.pr_get_IDDocumentoCDD", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pNroTramite", OracleDbType.Int32, 10);
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);

                com.Parameters["pNroTramite"].Value = pNroTramite;
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        public void DAUpdate_ID_DOCUMENTO_CDD_1(InscripcionSifcosDto Obj)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.PR_ID_DOCUMENTO1_CDD_UPDATE", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pidtramite", OracleDbType.Int64, 10);
                com.Parameters.Add("pIdDocumentoCDD1", OracleDbType.Int64, 10);
                com.Parameters.Add("pusrmodif", OracleDbType.Varchar2, 11);
                com.Parameters.Add("presultado", OracleDbType.Varchar2, 100);

                com.Parameters["pidtramite"].Value = Obj.NroTramiteSifcos;

                if (Obj.Id_Documento1_CDD != null)
                {
                    com.Parameters["pIdDocumentoCDD1"].Value = Obj.Id_Documento1_CDD;

                }

                com.Parameters["pusrmodif"].Value = Obj.USR_MODIF;
                com.Parameters["presultado"].Direction = ParameterDirection.Output;
                conn.Open();
                var cantFilas = com.ExecuteNonQuery();

                var Resultado = com.Parameters["presultado"].Value.ToString();

                if (Resultado != "OK")
                {


                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

        }
        public void DAUpdate_ID_DOCUMENTO_CDD_2(InscripcionSifcosDto Obj)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.PR_ID_DOCUMENTO2_CDD_UPDATE", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pidtramite", OracleDbType.Int64, 10);
                com.Parameters.Add("pIdDocumentoCDD2", OracleDbType.Int64, 10);
                com.Parameters.Add("pusrmodif", OracleDbType.Varchar2, 11);
                com.Parameters.Add("presultado", OracleDbType.Varchar2, 100);

                com.Parameters["pidtramite"].Value = Obj.NroTramiteSifcos;

                if (Obj.Id_Documento2_CDD != null)
                {
                    com.Parameters["pIdDocumentoCDD2"].Value = Obj.Id_Documento2_CDD;

                }

                com.Parameters["pusrmodif"].Value = Obj.USR_MODIF;
                com.Parameters["presultado"].Direction = ParameterDirection.Output;
                conn.Open();
                var cantFilas = com.ExecuteNonQuery();

                var Resultado = com.Parameters["presultado"].Value.ToString();

                if (Resultado != "OK")
                {


                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

        }

        public DataTable DAGetTramitesSifcosViejo(String pCuit, string pNroSifcos, string pNroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                OracleCommand com = new OracleCommand("SIFCOS.SP_TRAMITESEG2_LIST_CUIT", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("P_Cuit", OracleDbType.Varchar2, 20);

                com.Parameters["P_Cuit"].Value = DBNull.Value;
                if (pCuit != null)
                    com.Parameters["P_Cuit"].Value = pCuit;

                com.Parameters.Add("P_NroSifcos", OracleDbType.Varchar2, 20);

                com.Parameters["P_NroSifcos"].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(pNroSifcos))
                    com.Parameters["P_NroSifcos"].Value = pNroSifcos;

                com.Parameters.Add("p_NroTramite", OracleDbType.Int64, 100);

                com.Parameters["p_NroTramite"].Value = DBNull.Value; ;
                if (!string.IsNullOrEmpty(pNroTramite))
                    com.Parameters["p_NroTramite"].Value = int.Parse(pNroTramite);

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaEliminarTramite(string nroTramite)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Eliminar_Tramite", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 15);
                com.Parameters["pNroTramite"].Value = nroTramite;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var cantFilas = com.ExecuteNonQuery();

                if (cantFilas == 0)
                {

                    return com.Parameters["pResultado"].Value.ToString();
                }
                string pResultado = com.Parameters["pResultado"].Value.ToString();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaAsignarNroSifcos(Int64 nroTramite, String Descripcion, string cuilUsuarioCidi)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_update_NroSifcos", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroTramite", OracleDbType.Double, 10);
                if (nroTramite != 0)
                    com.Parameters["pNroTramite"].Value = nroTramite;
                else
                    com.Parameters["pNroTramite"].Value = DBNull.Value;

                com.Parameters.Add("pDescripcion", OracleDbType.Varchar2, 50);
                if (Descripcion != "")
                    com.Parameters["pDescripcion"].Value = Descripcion;
                else
                    com.Parameters["pDescripcion"].Value = DBNull.Value;

                com.Parameters.Add("pCuilUsuarioCIDI", OracleDbType.Varchar2, 11);
                if (!string.IsNullOrEmpty(cuilUsuarioCidi))
                    com.Parameters["pCuilUsuarioCIDI"].Value = cuilUsuarioCidi;
                else
                    com.Parameters["pCuilUsuarioCIDI"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pNroSifcos", OracleDbType.Varchar2, 50);
                com.Parameters["pNroSifcos"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                string pNroSifcos = com.Parameters["pNroSifcos"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                    return pNroSifcos;
                return pResultado;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaAsignarNroSifcos(string pNroTramite, string pNroSifcos, string pCuilUsuarioCidi)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Asignar_NroSifcos", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 15);
                com.Parameters["pNroTramite"].Value = pNroTramite;

                com.Parameters.Add("pNroSifcos", OracleDbType.Varchar2, 15);
                com.Parameters["pNroSifcos"].Value = pNroSifcos;

                com.Parameters.Add("pCuilUsuarioCidi", OracleDbType.Varchar2, 11);
                com.Parameters["pCuilUsuarioCidi"].Value = pCuilUsuarioCidi;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var cantFilas = com.ExecuteNonQuery();

                if (cantFilas == 0)
                {

                    return com.Parameters["pResultado"].Value.ToString();
                }
                string pResultado = com.Parameters["pResultado"].Value.ToString();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaAsignarResponsable(string pCuitEmpresa, string pCuilResponsable, string pIdRol)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Insertar_Relacion", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                com.Parameters.Add("pCuitEmpresa", OracleDbType.Varchar2, 11);
                com.Parameters["pCuitEmpresa"].Value = pCuitEmpresa;

                com.Parameters.Add("pCuilResp", OracleDbType.Varchar2, 11);
                com.Parameters["pCuilResp"].Value = pCuilResponsable;

                com.Parameters.Add("pIdRol", OracleDbType.Varchar2, 11);
                com.Parameters["pIdRol"].Value = pIdRol;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var cantFilas = com.ExecuteNonQuery();

                if (cantFilas == 0)
                {

                    return com.Parameters["pResultado"].Value.ToString();
                }
                string pResultado = com.Parameters["pResultado"].Value.ToString();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaEliminarResponsable(string pCuitEmpresa, string pCuilResponsable)
        {

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {

                OracleCommand com = new OracleCommand("SIFCOS.PCK_SIFCOS_INSERCIONES.pr_Eliminar_Relacion", conn);
                com.CommandType = CommandType.StoredProcedure;
                conn.Open();

                com.Parameters.Add("pCuitEmpresa", OracleDbType.Varchar2, 11);
                com.Parameters["pCuitEmpresa"].Value = pCuitEmpresa;

                com.Parameters.Add("pCuilResp", OracleDbType.Varchar2, 11);
                com.Parameters["pCuilResp"].Value = pCuilResponsable;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var cantFilas = com.ExecuteNonQuery();

                if (cantFilas == 0)
                {

                    return com.Parameters["pResultado"].Value.ToString();
                }
                string pResultado = com.Parameters["pResultado"].Value.ToString();

                return pResultado;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// metodo utilizado para el modulo de promocion comercial
        /// </summary>
        /// <param name="pCuitEmpresa"></param>
        /// <param name="pCuilResponsable"></param>
        /// <returns></returns>
        //public DataTable DaGetEmpleo(string pCuitEmpresa, string pCuilResponsable)
        //{

        //    OracleConnection conn = new OracleConnection(CadenaDeConexionPromoComercial());
        //    try
        //    {

        //        OracleCommand com = new OracleCommand("doc_laboral.pr_empresas_altas_bajas_x_periodo", conn);
        //        com.CommandType = CommandType.StoredProcedure;
        //        conn.Open();

        //        com.Parameters.Add("pcuit", OracleDbType.Varchar2, 11);
        //        com.Parameters["pcuit"].Value = pCuitEmpresa;

        //        com.Parameters.Add("pdesdefecha", OracleDbType.Varchar2, 11);
        //        com.Parameters["pdesdefecha"].Value = pCuilResponsable;

        //        com.Parameters.Add("phastafecha", OracleDbType.Varchar2, 11);
        //        com.Parameters["phastafecha"].Value = pCuilResponsable;

        //        com.Parameters.Add("pcursor", OracleDbType.RefCursor);
        //        com.Parameters["pcursor"].Direction = ParameterDirection.Output;

        //        com.ExecuteNonQuery();

        //        OracleDataAdapter da = new OracleDataAdapter(com);

        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        conn.Close();
        //        if (ds.Tables.Count > 0)
        //            return ds.Tables[0];

        //        return null;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new daException(ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        public DataTable DAConsultaContactosEstablecimiento(String id_domicilio_estab, out ResultadoRule result)
        {
            result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexionIndustria());
            try
            {

                OracleCommand com = new OracleCommand("INDUSTRIA.pr_contactos_get", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pidaplicacion", OracleDbType.Int32, 10);
                com.Parameters.Add("pidentidad", OracleDbType.Varchar2, 30);
                com.Parameters.Add("ptablaorigen", OracleDbType.Varchar2, 30);
                com.Parameters.Add("pidtipocomunicacion", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pCursor", OracleDbType.RefCursor);

                com.Parameters["pidaplicacion"].Value = 116;
                com.Parameters["pidentidad"].Value = id_domicilio_estab;
                com.Parameters["ptablaorigen"].Value = DBNull.Value;
                com.Parameters["pidtipocomunicacion"].Value = DBNull.Value;

                com.Parameters["pcursor"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                result.MensajeError = ex.Message;
                result.OcurrioError = true;
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        #region DIA_COMERCIO

        //OBSERVACIONES: AUSENTE1,AUSENTE2,AUSENTE3, PRESENTE1, PRESENTE2,PRESENTE3
        //PRIORIDAD: 1,2,3 (TIPO DE PERSONA INVITADA)
        //NOMBRE FANTASIA: NOMBRE Y APELLIDO
        //NRO_BAJA_MUNICIPAL: EVENTOPYMES
        //NRO_HAB_MUNICIPAL: FECHA ALTA
        //CUIT_PERS_JURIDICA: CUIL PERSONA
        //NRO_DGR: CUIT EMPRESA
        //RAZON_SOCIAL: NOMBRE EMPRESA
        //ID_ENTIDAD: NRO_ENTRADA
        public DataTable DaConsultaInscripcionByNro(string pNroInscripcion)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select * from sifcos.t_entidades e where e.nro_baja_municipal like 'EVENTOPYMES' and e.id_entidad= " + pNroInscripcion);

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return null;
        }

        public string DaRegistrarInvitacionVIP(InscripcionEvento Obj)
        {
            string resultado = "";
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var FechaActual = DateTime.Now;
                var sql = new StringBuilder();
                String NombreYApellido = Obj.Apellido + ", " + Obj.Nombre;

                sql.Append(" INSERT INTO SIFCOS.T_ENTIDADES (id_entidad,nombre_fantasia,cuit_pers_juridica,nro_dgr,razon_social,nro_hab_municipal,observaciones,nro_baja_municipal,prioridad) ");
                sql.Append(" values('" + Obj.NroInscripcion + "','" + NombreYApellido + "','" + Obj.CUIL + "','" + Obj.CUIT + "','" + Obj.Razon_Social + "','" + FechaActual.ToShortDateString() + "','AUSENTE','DIACOMERCIO','1')");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;
                conn.Open();


                var r = com.ExecuteNonQuery();
                conn.Close();

                resultado = "OK";

            }
            catch (Exception ex)
            {
                conn.Close();
                resultado = "No se pudo Registrar la persona " + Obj.Apellido + ", " + Obj.Nombre;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }

        public string DaRegistrarInvitacion(InscripcionEvento Obj)
        {
            string resultado = "";
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var FechaActual = DateTime.Now;
                var sql = new StringBuilder();
                String NombreYApellido = Obj.Apellido + ", " + Obj.Nombre;

                sql.Append(" INSERT INTO SIFCOS.T_ENTIDADES (id_entidad,nombre_fantasia,cuit_pers_juridica,nro_dgr,razon_social,nro_hab_municipal,observaciones,nro_baja_municipal,prioridad) ");
                sql.Append(" values('" + Obj.NroInscripcion + "','" + NombreYApellido + "','" + Obj.CUIL + "','" + Obj.CUIT + "','" + Obj.Razon_Social + "','" + FechaActual.ToShortDateString() + "','AUSENTE','DIACOMERCIO','2')");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;
                conn.Open();


                var r = com.ExecuteNonQuery();
                conn.Close();

                resultado = "OK";

            }
            catch (Exception ex)
            {
                conn.Close();
                resultado = "No se pudo Registrar la persona " + Obj.Apellido + ", " + Obj.Nombre;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }

        public string DaRegistrarAccesoEvento(InscripcionEvento Obj)
        {
            string resultado = "";
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" UPDATE sifcos.t_entidades e set observaciones='PRESENTE' ");
                sql.Append(" WHERE e.nro_baja_municipal like 'DIACOMERCIO' and e.id_entidad =" + Obj.NroInscripcion);

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;
                conn.Open();


                var r = com.ExecuteNonQuery();
                conn.Close();

                resultado = "OK";

            }
            catch (Exception ex)
            {
                conn.Close();
                resultado = "No se pudo Registrar la persona " + Obj.Nombre;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }

        public string DaRegistrarAccesoVIP(InscripcionEvento Obj)
        {
            string resultado = "";
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" UPDATE sifcos.t_entidades e set observaciones='PRESENTE_VIP' ");
                sql.Append(" WHERE e.nro_baja_municipal like 'DIACOMERCIO' and e.id_entidad =" + Obj.NroInscripcion);

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;
                conn.Open();


                var r = com.ExecuteNonQuery();
                conn.Close();

                resultado = "OK";

            }
            catch (Exception ex)
            {
                conn.Close();
                resultado = "No se pudo Registrar la persona " + Obj.Nombre;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }

        public DataTable DaGetUltimoNroInscripcion()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select nvl(max(e.id_entidad),400001) nro_inscripcion from sifcos.t_entidades e where e.nro_baja_municipal like 'DIACOMERCIO' ");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return null;




        }

        public DataTable DaGetInscripcionByCUIL(String pCUIL)
        {
            var sql = new StringBuilder();

            sql.Append(" select * from sifcos.t_entidades e where e.nro_baja_municipal like 'DIACOMERCIO' and e.CUIT_PERS_JURIDICA='" + pCUIL + "'");

            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());

            OracleCommand com = new OracleCommand(sql.ToString(), conn);
            com.CommandType = CommandType.Text;

            conn.Open();
            com.ExecuteReader();
            OracleDataAdapter da = new OracleDataAdapter(com);

            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            return ds.Tables[0];

        }
        public DataTable DaGetInscriptosDiaComercioPresentes()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select DISTINCT e.id_entidad nro_inscripcion,e.cuit_pers_juridica cuil,e.nombre_fantasia nombre,e.nro_dgr cuit,observaciones estado,decode(e.prioridad,1,'VIP',2,'INVITADO') TIPO_ENTRADA ");
                sql.Append(" FROM sifcos.t_entidades e ");
                sql.Append(" where e.nro_baja_municipal='DIACOMERCIO' and e.observaciones<>'AUSENTE' ORDER BY e.nombre_fantasia ");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public DataTable DaGetInscriptosDiaComercioAusentes()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select DISTINCT e.id_entidad nro_inscripcion,e.cuit_pers_juridica cuil,e.nombre_fantasia nombre,e.nro_dgr cuit,observaciones estado,decode(e.prioridad,1,'VIP',2,'INVITADO') TIPO_ENTRADA, ");
                sql.Append("(select '('||v.cod_area ||')'|| v.nro_mail from t_comunes.vt_comunicaciones v where v.id_entidad=to_char(e.id_entidad) and v.id_tipo_comunicacion='07') contacto ");
                sql.Append(" FROM sifcos.t_entidades e ");
                sql.Append(" where e.nro_baja_municipal='DIACOMERCIO' and e.observaciones='AUSENTE' ORDER BY 6 desc,e.nombre_fantasia asc ");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public DataTable DaGetInscriptosExcel()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select DISTINCT e.id_entidad nro_inscripcion,e.cuit_pers_juridica cuil,e.nombre_fantasia nombre,e.nro_dgr cuit,observaciones estado,decode(e.prioridad,1,'VIP',2,'INVITADO') TIPO_ENTRADA, ");
                sql.Append("(select '('||v.cod_area ||')'|| v.nro_mail from t_comunes.vt_comunicaciones v where v.id_entidad=to_char(e.id_entidad) and v.id_tipo_comunicacion='07') contacto, ");
                sql.Append("(select v.nro_mail from t_comunes.vt_comunicaciones v where v.tabla_origen LIKE 'SIFCOS.T_SIF_TRAMITES_SIFCOS' and v.id_tipo_comunicacion='11' AND TO_CHAR(V.id_entidad)=to_char(e.id_entidad)) EMAIL ");
                sql.Append(" FROM sifcos.t_entidades e ");
                sql.Append(" where e.nro_baja_municipal='DIACOMERCIO' and e.observaciones='AUSENTE' ORDER BY 6 desc,e.nombre_fantasia asc ");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        #endregion


        public List<PersonaJuridica> DaGetPersonaJuridica(String pCuit)
        {
            var lista = new List<PersonaJuridica>();
            OracleConnection conn = new OracleConnection(CadenaDeConexionIndustria());
            try
            {

                OracleCommand com = new OracleCommand("INDUSTRIA.PCK_SIIC_CONSULTA.pr_get_Persona_Juridica", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pCUIT", OracleDbType.Varchar2, 12);
                if (pCuit != null)
                    com.Parameters["pCUIT"].Value = pCuit;
                else
                    com.Parameters["pCUIT"].Value = DBNull.Value;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new PersonaJuridica()
                    {

                        Cuit = resultado["cuit"].ToString(),
                        RazonSocial = resultado["RAZON_SOCIAL"].ToString(),

                    };



                    lista.Add(obj);
                }
                com.ExecuteReader();



                return lista;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable DaGetRazonSocial(String prefijo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionIndustria());
            try
            {

                OracleCommand com = new OracleCommand("INDUSTRIA.PCK_SIIC_CONSULTA.pr_get_razonSocial", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pPrefijo", OracleDbType.Varchar2, 100);
                if (prefijo != null)
                    com.Parameters["pPrefijo"].Value = prefijo;
                else
                    com.Parameters["pPrefijo"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public DataTable DaGetPersonasRcivil_CUIL(String pCUIL)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionIndustria());
            try
            {


                OracleCommand com = new OracleCommand("INDUSTRIA.PCK_SIIC_CONSULTA.pr_get_Personas_RCivil_byCuit", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pCUIL", OracleDbType.Varchar2, 12);
                if (pCUIL != null)
                    com.Parameters["pCUIL"].Value = pCUIL;
                else
                    com.Parameters["pCUIL"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        

        public DataTable DaConsultaSQL(String SQL, String BD, out ResultadoRule Result)
        {

            Result = new ResultadoRule();

            switch (BD)
            {
                case "1":
                    BD = CadenaDeConexionIndustria();
                    break;
                case "2":
                    BD = CadenaDeConexionSifcos();
                    break;
                case "3":
                    BD = CadenaDeConexionRuami();
                    break;
                case "4":
                    BD = CadenaDeConexionPromInd();
                    break;
                case "5":
                    BD = CadenaDeConexionAlimentos();
                    break;
                case "6":
                    BD = CadenaDeConexionPersonal1();
                    break;
                case "7":
                    BD = CadenaDeConexionPersonal2();
                    break;
                case "8":
                    BD = CadenaDeConexionControlMicym();
                    break;

            }

            OracleConnection conn = new OracleConnection(BD);

            try
            {
                Result.OcurrioError = false;
                Result.MensajeError = "";
                Result.MensajeExito = "OK";

                var sql = new StringBuilder();

                sql.Append(SQL);

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                Result.OcurrioError = true;
                Result.MensajeError = ex.Message;
                Result.MensajeExito = "ERROR";
                return null;
            }
            finally
            {

                conn.Close();
            }
        }
        public string DaEliminarNotificaciones()
        {
            string resultado = "";
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" update  sifcos.t_sif_entidades e set e.resenia='' ");
                sql.Append(" where e.resenia='.SIFCOS.'");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;
                conn.Open();


                var r = com.ExecuteNonQuery();
                conn.Close();

                resultado = "OK";

            }
            catch (Exception ex)
            {
                conn.Close();
                resultado = "No se pudo eliminar las notificaciones";
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }


        #region METODOS_NOTIFICACION_CIDI
        public DataTable DaGetCUILRepLegal(String Cuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select p.cuil ");
                sql.Append(" from sifcos.t_sif_tramites_sifcos t join sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad ");
                sql.Append(" join sifcos.t_sif_rep_legal rp on rp.id_rep_legal=t.id_cargo_entidad ");
                sql.Append(" join rcivil.vt_pk_persona p on rp.id_sexo=p.id_sexo and rp.nro_documento=p.nro_documento ");
                sql.Append(" and rp.pai_cod_pais=p.pai_cod_pais and rp.id_numero=p.id_numero ");
                sql.Append(" where  e.cuit='" + Cuit + "'");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public DataTable DaGetCUILGestor(String Cuit)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select p.cuil ");
                sql.Append(" from sifcos.t_sif_tramites_sifcos t join sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad ");
                sql.Append(" join sifcos.t_sif_gestores g on g.id_gestor=t.id_gestor_entidad ");
                sql.Append(" join rcivil.vt_pk_persona p on g.id_sexo=p.id_sexo and g.nro_documento=p.nro_documento ");
                sql.Append(" and g.pai_cod_pais=p.pai_cod_pais and g.id_numero=p.id_numero ");
                sql.Append(" where  e.cuit='" + Cuit + "'");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public DataTable DaGetSifcosVencido(String Cuit, String NroSifcos)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select t.nro_tramite_sifcos,e.cuit,e.nro_sifcos,t.fec_vencimiento  ");
                sql.Append(" from sifcos.t_sif_tramites_sifcos t ");
                sql.Append(" join sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad ");
                sql.Append(" join sifcos.t_sif_hist_estado h on h.id_tramite_sifcos=t.nro_tramite_sifcos ");
                sql.Append(" where e.cuit=" + Cuit + " and e.nro_sifcos=" + NroSifcos + " and ");
                sql.Append(" h.fec_desde_estado=(select max(h2.fec_desde_estado) from sifcos.t_sif_hist_estado h2 ");
                sql.Append(" where h2.id_tramite_sifcos=h.id_tramite_sifcos) and t.nro_tramite_sifcos= ");
                sql.Append(" (select max(t2.nro_tramite_sifcos) from sifcos.t_sif_tramites_sifcos t2 ");
                sql.Append(" where t2.id_entidad=e.id_entidad) and h.id_estado_tramite=6 ");
                sql.Append(" order by t.fec_vencimiento");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        public DataTable DaGetVerificarNotificacion(String Id_Entidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select e.resenia notificado ");
                sql.Append(" from sifcos.t_sif_entidades e  ");
                sql.Append(" where  e.id_entidad='" + Id_Entidad + "'");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        public string DaRegistrarNotificacionCIDI(String IdEntidad)
        {
            string resultado = "";
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" update sifcos.t_sif_entidades e ");
                sql.Append(" set e.resenia=sysdate ");
                sql.Append(" where e.id_entidad= " + IdEntidad );

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;
                conn.Open();


                var r = com.ExecuteNonQuery();
                conn.Close();

                resultado = "OK";

            }
            catch (Exception ex)
            {
                conn.Close();
                resultado = "No se pudo Registrar la notificacion: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }
        public string DaActualizarNotificacionCIDI(String Destinatario, String IdEntidad)
        {
            string resultado = "";
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" update sifcos.t_sif_entidades e ");
                sql.Append(" set e.resenia= ");
                sql.Append(" where e.nro_sifcos=(select distinct e.nro_sifcos from sifcos.t_sif_tramites_sifcos t ");
                sql.Append(" join sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad ");
                sql.Append(" join sifcos.t_sif_rep_legal rp on rp.id_rep_legal=t.id_cargo_entidad ");
                sql.Append(" join rcivil.vt_pk_persona p on p.pai_cod_pais = rp.pai_cod_pais and p.nro_documento=rp.nro_documento and p.id_numero = rp.id_numero and p.id_sexo = rp.id_sexo ");
                sql.Append(" where p.cuil=" + Destinatario + " and e.id_entidad= " + IdEntidad);

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;
                conn.Open();


                var r = com.ExecuteNonQuery();
                conn.Close();

                resultado = "OK";

            }
            catch (Exception ex)
            {
                conn.Close();
                resultado = "No se pudo Registrar la notificacion: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }
        public DataTable DaGetFechaVtoTramiteAltaSinNroSifcos(String Id_Entidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexionSifcos());
            try
            {
                var sql = new StringBuilder();

                sql.Append(" select t.fec_alta+18 vto_tramite_alta ");
                sql.Append(" from sifcos.t_sif_tramites_sifcos t join sifcos.t_sif_entidades e on t.id_entidad=e.id_entidad");
                sql.Append(" where t.id_tipo_tramite=1 and e.id_entidad='" + Id_Entidad + "'");

                OracleCommand com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.Text;

                conn.Open();
                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;


            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        #endregion



    }
}
