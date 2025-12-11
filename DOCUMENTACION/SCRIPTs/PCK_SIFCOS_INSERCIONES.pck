CREATE OR REPLACE PACKAGE "PCK_SIFCOS_INSERCIONES" AS

  PROCEDURE pr_insert_inscripcion (
    pEnt_CUIT_format IN varchar2,
    pEnt_CUIT                IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pEnt_Id_Sede             IN sifcos.t_sif_entidades.id_sede%TYPE DEFAULT NULL,
    pEnt_Local               IN sifcos.t_sif_entidades.local%TYPE DEFAULT NULL,
    pEnt_Oficina             IN sifcos.t_sif_entidades.oficina%TYPE DEFAULT NULL,
    pEnt_Stand               IN sifcos.t_sif_entidades.stand%TYPE DEFAULT NULL,
    pEnt_Cobertura           IN sifcos.t_sif_entidades.cobertura_medica%TYPE DEFAULT NULL,
    pEnt_Propietario         IN sifcos.t_sif_entidades.propietario%TYPE DEFAULT NULL,
    pEnt_Seguro              IN sifcos.t_sif_entidades.seguro_local%TYPE DEFAULT NULL,
    pEnt_Latitud             IN sifcos.t_sif_entidades.latitud_ubi%TYPE DEFAULT NULL,
    pEnt_Longitud            IN sifcos.t_sif_entidades.longitud_ubi%TYPE DEFAULT NULL,
    pEnt_Nro_sifcos          IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pTram_Capac_ult_anio     IN sifcos.t_sif_tramites_sifcos.capacitacion_ult_anio%TYPE DEFAULT NULL,
    pTram_Cuil_Usu_CIDI      IN sifcos.t_sif_tramites_sifcos.cuil_usuario_cidi%TYPE DEFAULT NULL,
    pTram_Tipo_Tramite       IN sifcos.t_sif_tramites_sifcos.id_tipo_tramite%TYPE DEFAULT NULL,
    pTram_Id_origen_prov     IN sifcos.t_sif_tramites_sifcos.id_origen_proveedor%TYPE DEFAULT NULL,
    pTram_Id_vin_dom_legal   IN sifcos.t_sif_tramites_sifcos.id_vin_dom_legal%TYPE DEFAULT NULL,
    pTram_Id_vin_dom_local   IN sifcos.t_sif_tramites_sifcos.id_vin_dom_local%TYPE DEFAULT NULL,
    pTram_Rango_alquiler     IN VARCHAR2, 
    pTram_Id_Act_Pri         IN sifcos.t_sif_tramites_sifcos.id_actividad_ppal%TYPE DEFAULT NULL,
    pTram_Id_Act_Sec         IN sifcos.t_sif_tramites_sifcos.id_actividad_sria%TYPE DEFAULT NULL,
    pTram_Cant_total_pers    IN sifcos.t_sif_tramites_sifcos.cant_pers_total%TYPE DEFAULT NULL,
    pTram_CantPersRelDep     IN sifcos.t_sif_tramites_sifcos.cant_pers_rel_dependencia%TYPE DEFAULT NULL,
    pTram_FechaVencimiento   IN sifcos.t_sif_tramites_sifcos.fec_vencimiento%TYPE DEFAULT NULL,
    pTram_cant_reemp         IN number,
    pRepr_id_cargo           IN sifcos.t_sif_rep_legal.id_cargo%TYPE DEFAULT NULL,
    pRepr_Cuil_RepLegal      IN varchar2,
    --pRepr_DNI_RepLegal       IN varchar2,
    --pRepr_ID_NUMERO_RepLegal IN varchar2,
    pGest_Cuil_Gestor        IN varchar2,
    pGest_id_tipo_gestor     IN sifcos.t_sif_gestores.id_tipo_gestor%TYPE DEFAULT NULL,
    pGest_CodAreaCelConta    IN t_comunes.t_comunicaciones.cod_area%TYPE DEFAULT NULL,
    pGest_nroCelConta        IN t_comunes.t_comunicaciones.nro_mail%TYPE DEFAULT NULL,
    pGest_email_conta        IN t_comunes.t_comunicaciones.nro_mail%TYPE DEFAULT NULL,
    pSup_Ventas              IN  sifcos.t_sif_superficies_empresa.superficie%TYPE DEFAULT NULL,
    pSup_Administracion      IN  sifcos.t_sif_superficies_empresa.superficie%TYPE DEFAULT NULL,
    pSup_Deposito            IN  sifcos.t_sif_superficies_empresa.superficie%TYPE DEFAULT NULL,
    pHest_Id_estado          IN sifcos.t_sif_hist_estado.id_estado_tramite%TYPE DEFAULT NULL,
    p_nro_habMunicipal       IN t_comunes.vt_pers_juridicas_completa.nro_hab_municipal%TYPE DEFAULT NULL,
    p_fecha_ini_act          IN t_comunes.vt_pers_juridicas_completa.fec_inicio_act%TYPE DEFAULT NULL,
    p_Id_Documento1_CDD      IN NUMBER, 
    p_Id_Documento2_CDD      IN NUMBER,
    p_Id_Documento3_CDD      IN NUMBER, 
    p_Id_Documento4_CDD      IN NUMBER,   
    p_Id_Entidad             IN NUMBER,
    p_Id_Organismo           IN NUMBER,
    pNroTramite              OUT number,
    pResultado               OUT varchar2
   );


  /* PROCEDURE pr_Insert_Entidad (
    pCUIT_format IN varchar2,
    pCUIT       IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pLocal      IN sifcos.t_sif_entidades.local%TYPE DEFAULT NULL,
    pOficina    IN sifcos.t_sif_entidades.oficina%TYPE DEFAULT NULL,
    pStand      IN sifcos.t_sif_entidades.stand%TYPE DEFAULT NULL,
    pPropietario IN sifcos.t_sif_entidades.propietario%TYPE DEFAULT NULL,
    p_Latitud   IN sifcos.t_sif_entidades.latitud_ubi%TYPE DEFAULT NULL,
    p_Longitud  IN sifcos.t_sif_entidades.longitud_ubi%TYPE DEFAULT NULL,
    pNombreComercio in varchar2,
    pEnt_Nro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT varchar2,
    pIdEntidad OUT number
   );*/
   
    PROCEDURE pr_Insert_Entidad (
    pCUIT_format IN varchar2,
    pCUIT       IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pId_Sede    IN sifcos.t_sif_entidades.id_sede%TYPE DEFAULT NULL,
    pLocal      IN sifcos.t_sif_entidades.local%TYPE DEFAULT NULL,
    pOficina    IN sifcos.t_sif_entidades.oficina%TYPE DEFAULT NULL,
    pStand      IN sifcos.t_sif_entidades.stand%TYPE DEFAULT NULL,
    pCobertura  IN sifcos.t_sif_entidades.cobertura_medica%TYPE DEFAULT NULL,
    pSeguro     IN sifcos.t_sif_entidades.seguro_local%TYPE DEFAULT NULL,
    pPropietario IN sifcos.t_sif_entidades.propietario%TYPE DEFAULT NULL,
    p_Latitud   IN sifcos.t_sif_entidades.latitud_ubi%TYPE DEFAULT NULL,
    p_Longitud  IN sifcos.t_sif_entidades.longitud_ubi%TYPE DEFAULT NULL,
    pNro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResenia IN sifcos.t_sif_entidades.resenia%TYPE DEFAULT NULL,
    pNombre_fantasia IN sifcos.t_sif_entidades.observacion%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Insert_Entidad_migracion (
    pCUIT_format IN varchar2,
    pCUIT       IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pId_Sede    IN sifcos.t_sif_entidades.id_sede%TYPE DEFAULT NULL,
    pCobertura  IN sifcos.t_sif_entidades.cobertura_medica%TYPE DEFAULT NULL,
    pSeguro     IN sifcos.t_sif_entidades.seguro_local%TYPE DEFAULT NULL,
    pPropietario IN sifcos.t_sif_entidades.propietario%TYPE DEFAULT NULL,
    p_Latitud   IN sifcos.t_sif_entidades.latitud_ubi%TYPE DEFAULT NULL,
    p_Longitud  IN sifcos.t_sif_entidades.longitud_ubi%TYPE DEFAULT NULL,
    pNro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pNombre_fantasia IN sifcos.t_sif_entidades.observacion%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );

   PROCEDURE pr_insert_baja_tramite (
    pEnt_Nro_sifcos          IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pTram_Cuil_Usu_CIDI      IN sifcos.t_sif_tramites_sifcos.cuil_usuario_cidi%TYPE DEFAULT NULL,
    pNroTramite              OUT number,
    pResultado               OUT varchar2
   );
   PROCEDURE pr_insert_baja_tramite_cese (
    pEnt_Nro_sifcos          IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pTram_Cuil_Usu_CIDI      IN sifcos.t_sif_tramites_sifcos.cuil_usuario_cidi%TYPE DEFAULT NULL,
    pFechaCese               IN date,
    pDoc1                    IN sifcos.t_sif_tramites_sifcos.id_documento1_cdd%TYPE DEFAULT NULL,
    pDoc2                    IN sifcos.t_sif_tramites_sifcos.id_documento2_cdd%TYPE DEFAULT NULL,
    pNroTramite              OUT number,
    pResultado               OUT varchar2
   );
  PROCEDURE pr_Insert_Comunicacion (
    p_entidad IN VARCHAR2,
    p_email IN VARCHAR2,
    p_pagWeb IN VARCHAR2,
    p_nroCel IN VARCHAR2,
    p_CodAreaCel IN VARCHAR2,
    p_nroTelFijo IN VARCHAR2,
    p_CodAreaTelFijo IN VARCHAR2,
    p_Facebook IN VARCHAR2,
    --p_CodAreaCelConta IN t_comunes.t_comunicaciones.cod_area%TYPE DEFAULT NULL,
    --p_nroCelConta IN t_comunes.t_comunicaciones.nro_mail%TYPE DEFAULT NULL,
    --p_email_conta IN t_comunes.t_comunicaciones.nro_mail%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
  PROCEDURE pr_Insert_Tipo_Superficie (
    pN_Superficie IN sifcos.t_sif_tipos_superficie.n_tipo_superficie%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
  PROCEDURE pr_Insert_Superficies_Empresa (
    pNro_Tramite IN sifcos.t_sif_superficies_empresa.id_tramite_sifcos%TYPE DEFAULT NULL,
    pId_Tipo_superficie IN sifcos.t_sif_tipos_superficie.id_tipo_superficie%TYPE DEFAULT NULL,
    pCantidad IN sifcos.t_sif_superficies_empresa.superficie%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Insert_Rep_Legal (
    pGest_cuil               IN varchar2,
    pGest_id_cargo     IN sifcos.t_sif_rep_legal.id_cargo%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
  /*PROCEDURE pr_Insert_Rep_Legal (
    pRepLegal_dni      IN varchar2,
    pRepLegal_id_numero IN varchar2,
    pRepLegal_id_cargo  IN sifcos.t_sif_rep_legal.id_cargo%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );*/
  PROCEDURE pr_Insert_Gestores (
    pGest_cuil               IN varchar2,
    pGest_id_tipo_gestor     IN sifcos.t_sif_gestores.id_tipo_gestor%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
 
  PROCEDURE pr_Insert_Producto (
    pN_Producto IN sifcos.t_sif_productos.n_producto%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
  PROCEDURE pr_Insert_Rol (
    pN_Rol IN sifcos.t_sif_Roles.n_rol%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Insert_Rol_Usuario (
    pIdUsuario IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    pRolUsuario IN sifcos.t_sif_usuarios_cidi.id_rol%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Insert_Org_Usuario (
    pIdUsuario IN sifcos.t_sif_usuarios_organismo.cuil_usr_cidi%TYPE DEFAULT NULL,
    pIdOrganismo IN sifcos.t_sif_usuarios_organismo.id_organismo%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Insert_prod_act (
    pIdProducto IN sifcos.t_sif_productos_actividad.id_producto%TYPE DEFAULT NULL,
    pIdActividad IN sifcos.t_sif_productos_actividad.id_actividad_clanae%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Insert_prod_tra (
    pId_Producto IN sifcos.t_sif_productos_tramite.id_producto%TYPE DEFAULT NULL,
    pNro_tramite IN sifcos.t_sif_productos_tramite.id_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
  PROCEDURE pr_Insert_Productos_Tramites (
    pId_Producto IN sifcos.t_sif_productos.id_producto%TYPE DEFAULT NULL,
    pNro_tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
 /*  PROCEDURE pr_Insert_Ofertas_Tramites (
    pId_Producto IN sifcos.t_sif_ofertas.id_producto%TYPE DEFAULT NULL,
    pNro_tramite IN sifcos.t_sif_tramites_sifcos_ca.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pPrecio IN sifcos.t_sif_ofertas_tramite.precio%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );*/
   
   PROCEDURE pr_Insert_Tasas_Tramite (
    pNro_transaccion IN sifcos.t_sif_tasas.nro_transaccion%TYPE DEFAULT NULL,
    pNro_tramite IN sifcos.t_sif_tasas.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pPagada IN sifcos.t_sif_tasas.pagada%TYPE DEFAULT NULL,
    pNroLiquidacionOriginal IN sifcos.t_sif_tasas.nroliquidacionoriginal%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
  PROCEDURE pr_Insert_Estado_Tramite (
    pNro_tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pDescripcion IN sifcos.t_sif_hist_estado.descripcion%TYPE DEFAULT NULL,
    pidEstado IN sifcos.t_sif_estados_tramite.id_estado_tramite%TYPE DEFAULT NULL,
    pCuilCidi IN sifcos.t_sif_hist_estado.cuil_usr_cidi%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Insert_HistEstado_Tramite (
    pNro_tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pidEstado IN sifcos.t_sif_estados_tramite.id_estado_tramite%TYPE DEFAULT NULL,
    pCuilCidi IN sifcos.t_sif_hist_estado.cuil_usr_cidi%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Insert_HistEstado_Descrip (
    pNro_tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pidEstado IN sifcos.t_sif_estados_tramite.id_estado_tramite%TYPE DEFAULT NULL,
    pCuilCidi IN sifcos.t_sif_hist_estado.cuil_usr_cidi%TYPE DEFAULT NULL,
    pDescripcion IN sifcos.t_sif_hist_estado.descripcion%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Insert_Rubro (
    pN_RUBRO IN sifcos.T_SIF_RUBROS.N_RUBRO%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Delete_Superficie (
    pId_Superficie IN sifcos.t_sif_tipos_superficie.id_tipo_superficie%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Delete_Rel_Prod_Act (
    pId_Producto IN sifcos.t_sif_productos_actividad.id_producto%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_rollback_tramite (
    p_CUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    P_ENTIDAD IN sifcos.t_sif_entidades.id_entidad%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_Insert_Rubro_Actividad (
    pIdRubro IN NUMBER,
    pActividadClanae IN VARCHAR2,
    pResultado OUT varchar2
   );
    PROCEDURE PR_SIF_HIST_CONCEPTOS_INSERT
    (
        pIdSifHistConceptos IN VARCHAR2, 
        pNConcepto IN VARCHAR2, 
        pMonto IN NUMBER, 
        pFecDesde IN DATE, 
        pFecHasta IN DATE,
    pIdTipoConcepto IN NUMBER
    );
    PROCEDURE PR_SIF_HIST_CONCEPTOS_DELETE
    (
            pIdSifHistConceptos IN VARCHAR2 

     );
 PROCEDURE PR_SIF_TIPOS_CONCEPTOS_INSERT
(
    pIdTipoConcepto IN NUMBER,
    pNTipoConcepto IN VARCHAR2
);
 PROCEDURE PR_PARAMETROS_GRAL_INSERT
(
    pIdParametroGeneral IN NUMBER,
    pValor IN VARCHAR2,
    pDescripcion IN VARCHAR2
);
 PROCEDURE PR_PARAMETROS_GRAL_DELETE
(
    pIdParametroGeneral IN NUMBER
);
  PROCEDURE pr_Update_Entidad (
    pIdEntidad IN number,
    /*pLocal IN varchar2,
    pOficina IN varchar2,
    pStand IN varchar2,*/
    pCobertura IN varchar2,
    pSeguro IN varchar2,
    pPropietario IN varchar2,
    pLongitud IN varchar2,
    pLatitud IN varchar2,
    /*pNombreComercio IN varchar2,*/
    pResultado OUT varchar2
   );
PROCEDURE pr_Insertar_Relacion (
    pCuitEmpresa IN varchar2,
    pCuilResp IN varchar2,
    pIdRol IN varchar2,
    pResultado OUT varchar2
   ); 
PROCEDURE pr_Eliminar_Relacion (
    pCuitEmpresa IN varchar2,
    pCuilResp IN varchar2,
    pResultado OUT varchar2
   );     
END PCK_SIFCOS_INSERCIONES;
/
CREATE OR REPLACE PACKAGE BODY "PCK_SIFCOS_INSERCIONES" AS

--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina inscripcion.aspx.cs
PROCEDURE pr_insert_inscripcion (
    pEnt_CUIT_format IN varchar2,
    pEnt_CUIT                IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pEnt_Id_Sede             IN sifcos.t_sif_entidades.id_sede%TYPE DEFAULT NULL,
    pEnt_Local               IN sifcos.t_sif_entidades.local%TYPE DEFAULT NULL,
    pEnt_Oficina             IN sifcos.t_sif_entidades.oficina%TYPE DEFAULT NULL,
    pEnt_Stand               IN sifcos.t_sif_entidades.stand%TYPE DEFAULT NULL,
    pEnt_Cobertura           IN sifcos.t_sif_entidades.cobertura_medica%TYPE DEFAULT NULL,
    pEnt_Propietario         IN sifcos.t_sif_entidades.propietario%TYPE DEFAULT NULL,
    pEnt_Seguro              IN sifcos.t_sif_entidades.seguro_local%TYPE DEFAULT NULL,
    pEnt_Latitud             IN sifcos.t_sif_entidades.latitud_ubi%TYPE DEFAULT NULL,
    pEnt_Longitud            IN sifcos.t_sif_entidades.longitud_ubi%TYPE DEFAULT NULL,
    pEnt_Nro_sifcos          IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pTram_Capac_ult_anio     IN sifcos.t_sif_tramites_sifcos.capacitacion_ult_anio%TYPE DEFAULT NULL,
    pTram_Cuil_Usu_CIDI      IN sifcos.t_sif_tramites_sifcos.cuil_usuario_cidi%TYPE DEFAULT NULL,
    pTram_Tipo_Tramite       IN sifcos.t_sif_tramites_sifcos.id_tipo_tramite%TYPE DEFAULT NULL,
    pTram_Id_origen_prov     IN sifcos.t_sif_tramites_sifcos.id_origen_proveedor%TYPE DEFAULT NULL,
    pTram_Id_vin_dom_legal   IN sifcos.t_sif_tramites_sifcos.id_vin_dom_legal%TYPE DEFAULT NULL,
    pTram_Id_vin_dom_local   IN sifcos.t_sif_tramites_sifcos.id_vin_dom_local%TYPE DEFAULT NULL,
    pTram_Rango_alquiler     IN VARCHAR2, 
    pTram_Id_Act_Pri         IN sifcos.t_sif_tramites_sifcos.id_actividad_ppal%TYPE DEFAULT NULL,
    pTram_Id_Act_Sec         IN sifcos.t_sif_tramites_sifcos.id_actividad_sria%TYPE DEFAULT NULL,
    pTram_Cant_total_pers    IN sifcos.t_sif_tramites_sifcos.cant_pers_total%TYPE DEFAULT NULL,
    pTram_CantPersRelDep     IN sifcos.t_sif_tramites_sifcos.cant_pers_rel_dependencia%TYPE DEFAULT NULL,
    pTram_FechaVencimiento   IN sifcos.t_sif_tramites_sifcos.fec_vencimiento%TYPE DEFAULT NULL,
    pTram_cant_reemp         IN number,
    pRepr_id_cargo           IN sifcos.t_sif_rep_legal.id_cargo%TYPE DEFAULT NULL,
    pRepr_Cuil_RepLegal      IN varchar2,
    --pRepr_DNI_RepLegal       IN varchar2,
    --pRepr_ID_NUMERO_RepLegal IN varchar2,
    pGest_Cuil_Gestor        IN varchar2,
    pGest_id_tipo_gestor     IN sifcos.t_sif_gestores.id_tipo_gestor%TYPE DEFAULT NULL,
    pGest_CodAreaCelConta    IN t_comunes.t_comunicaciones.cod_area%TYPE DEFAULT NULL,
    pGest_nroCelConta        IN t_comunes.t_comunicaciones.nro_mail%TYPE DEFAULT NULL,
    pGest_email_conta        IN t_comunes.t_comunicaciones.nro_mail%TYPE DEFAULT NULL,
    pSup_Ventas              IN  sifcos.t_sif_superficies_empresa.superficie%TYPE DEFAULT NULL,
    pSup_Administracion      IN  sifcos.t_sif_superficies_empresa.superficie%TYPE DEFAULT NULL,
    pSup_Deposito            IN  sifcos.t_sif_superficies_empresa.superficie%TYPE DEFAULT NULL,
    pHest_Id_estado          IN sifcos.t_sif_hist_estado.id_estado_tramite%TYPE DEFAULT NULL,
    p_nro_habMunicipal       IN t_comunes.vt_pers_juridicas_completa.nro_hab_municipal%TYPE DEFAULT NULL,
    p_fecha_ini_act          IN t_comunes.vt_pers_juridicas_completa.fec_inicio_act%TYPE DEFAULT NULL,
  
    p_Id_Documento1_CDD      IN NUMBER, 
    p_Id_Documento2_CDD      IN NUMBER,
    p_Id_Documento3_CDD      IN NUMBER, 
    p_Id_Documento4_CDD      IN NUMBER,
    p_Id_Entidad             IN number,
    p_Id_Organismo            IN number,
  
    pNroTramite              OUT number,
    pResultado               OUT varchar2
   ) IS

v_ResCom varchar2(5);
v_bandera  varchar2(1) := 'S' ;
v_Resultado varchar2(25);
o_resultado_pj varchar2 (25);
v_id_Entidad varchar2(10);
v_id_Gestor varchar2(10);
v_id_Rep_Legal varchar2(10);
v_ResHist varchar2(5);
v_existe number(3);
--v_CEnt varchar2(5);
v_IdApp number(4);
v_idTipoCom varchar2(2);
--v_OrigenTabla varchar2(30);
v_nro_tramite sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%type;
v_Fecha_Ini_Act_pri date;
v_Fecha_Ini_Act_sec date;
v_Origen_tabla varchar2(30):='sifcos.t_sif_tramites_sifcos'; 
v_resultado_Entidad varchar2(50);

BEGIN
    
  IF (pEnt_Nro_sifcos<> null) THEN
  SELECT e.id_entidad 
  INTO v_id_Entidad
  FROM sifcos.t_sif_entidades e
  WHERE e.nro_sifcos=pEnt_Nro_sifcos;
  END IF;
  
  SELECT pj.id_aplicacion 
  INTO v_IdApp
  FROM t_comunes.vt_pers_juridicas_completa pj
  WHERE pj.cuit=pEnt_CUIT AND PJ.id_sede=00;
  SELECT SEQ_TRAMITES.NEXTVAL
  INTO v_nro_tramite
  FROM DUAL;
  IF(pTram_Id_Act_Pri<>0)THEN
     SELECT a.fecha_inicio_act
     INTO v_Fecha_Ini_Act_pri
     FROM t_comunes.t_actividades a
     WHERE a.id_actividad=pTram_Id_Act_Pri
       AND a.fecha_fin_act is null;
  END IF;
    IF(pTram_Id_Act_Sec<>0)THEN
     SELECT a.fecha_inicio_act
     INTO v_Fecha_Ini_Act_sec
     FROM t_comunes.t_actividades a
     WHERE a.id_actividad=pTram_Id_Act_Sec
       AND a.fecha_fin_act is null;
  END IF;
  BEGIN
  
/*  sifcos.pck_sifcos_inserciones.pr_Insert_Entidad(pEnt_CUIT_format
                                      ,pEnt_CUIT
                                      ,pEnt_Id_Sede
                                      ,pEnt_Local
                                      ,pEnt_Oficina
                                      ,pEnt_Stand
                                      ,pEnt_Cobertura
                                      ,pEnt_Propietario
                                      ,pEnt_Seguro
                                      ,pEnt_Latitud
                                      ,pEnt_Longitud
                                      ,pEnt_Nro_sifcos
                                      ,v_id_Entidad
                                      );
                                      */
     sifcos.pck_sifcos_inserciones.pr_update_Entidad(p_Id_Entidad
                                     /*,pEnt_Local
                                      ,pEnt_Oficina
                                      ,pEnt_Stand*/
                                      ,pEnt_Cobertura
                                      ,pEnt_Propietario
                                      ,pEnt_Seguro
                                      ,pEnt_Latitud
                                      ,pEnt_Longitud
                                     /* ,pNombreComercio*/
                                      ,v_resultado_Entidad 
                                      );
                                                  
                                              
                                          
                                        
EXCEPTION
  WHEN OTHERS THEN
  Rollback;                                      
END;   
-- INSERCION DE REPRESENTANTE DE MANERA TEMPORAL LUEGO SE CAMBIA EL METODO POR EL COMENTADO                                   
sifcos.pck_sifcos_inserciones.pr_Insert_Rep_Legal( pRepr_Cuil_RepLegal
                                       ,pRepr_id_cargo
                                       ,v_id_Rep_Legal
                                       ); 
/* sifcos.pck_sifcos_inserciones.pr_Insert_Rep_Legal( pRepr_DNI_RepLegal
                                       ,pRepr_ID_NUMERO_RepLegal
                                       ,pRepr_id_cargo
                                       ,v_id_Rep_Legal
                                       );
*/ 
sifcos.pck_sifcos_inserciones.pr_Insert_Gestores( pGest_Cuil_Gestor
                                       ,pGest_id_tipo_gestor
                                       ,v_id_Gestor
                                       );
                                       
-- inserto email contador
     IF (pGest_email_conta IS NOT NULL ) THEN
       v_idTipoCom:='13';
      t_comunes.pack_comunicaciones.INSERTA_COMUNICACIONES(v_idTipoCom
                                                           ,pEnt_CUIT
                                                           ,pGest_email_conta
                                                           ,null
                                                           ,98
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
     END IF;

-- inserto telefono contador
BEGIN
     IF (pGest_nroCelConta IS NOT NULL and pGest_CodAreaCelConta IS NOT NULL) THEN
      t_comunes.pack_comunicaciones.INSERTA_COMUNICACIONES('03'
                                                           ,pEnt_CUIT
                                                           ,pGest_nroCelConta
                                                           ,pGest_CodAreaCelConta
                                                           ,98
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
     END IF;
EXCEPTION
 WHEN OTHERS THEN
  Rollback;
END;

BEGIN
  IF (pSup_Ventas IS NOT NULL) THEN
      pck_sifcos_inserciones.pr_Insert_Superficies_Empresa(v_nro_tramite,
                                               2,
                                                pSup_Ventas,
                                                v_Resultado);
  END IF;
  IF (pSup_Administracion IS NOT NULL ) THEN
      pck_sifcos_inserciones.pr_Insert_Superficies_Empresa(v_nro_tramite,
                                    1,
                                    pSup_Administracion,
                                    v_Resultado);
  END IF;
  IF (pSup_Deposito IS NOT NULL) THEN
      pck_sifcos_inserciones.pr_Insert_Superficies_Empresa(v_nro_tramite,
                                    3,
                                    pSup_Deposito,
                                    v_Resultado);
  END IF;
 
    
  IF v_id_Rep_Legal='ERROR' then
    v_id_Rep_Legal:=0;
  END IF;
  IF v_id_Gestor='ERROR' then
    v_id_Gestor:=0;
  END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
  Rollback;
 END;

BEGIN
  sifcos.pck_sifcos_inserciones.pr_insert_histEstado_Tramite(v_nro_tramite
                                                             ,pHest_Id_estado
                                                             ,pTram_Cuil_Usu_CIDI
                                                             ,v_ResHist
                                                           );
  EXCEPTION
  WHEN OTHERS THEN
    v_bandera := 'N' ;
  ROLLBACK;
  RAISE_APPLICATION_ERROR(-20001,'Error al insertar Estado: '||sqlerrm);
  END;

BEGIN
  /*SELECT count(*)
  INTO v_existe
  FROM sifcos.t_sif_tramites_sifcos t
  WHERE t.id_tipo_tramite=pTram_Tipo_Tramite
    and t.id_vin_dom_legal=pTram_Id_vin_dom_legal
    and t.id_vin_dom_local=pTram_Id_vin_dom_local
    and t.fec_ini_tramite = to_date(p_fecha_ini_act,'dd/mm/yyyy');*/
    
    IF ( v_resultado_Entidad='OK') THEN
    INSERT INTO sifcos.t_sif_tramites_sifcos
    ( fec_alta
     ,nro_tramite_sifcos
     ,capacitacion_ult_anio
     ,id_entidad
     ,cuil_usuario_cidi
     ,id_tipo_tramite
     ,id_vin_dom_legal
     ,id_vin_dom_local
     ,fec_ini_tramite
     ,id_cargo_entidad
     ,id_gestor_entidad
     ,id_origen_proveedor
     ,cant_pers_total
     ,cant_pers_rel_dependencia
     ,fec_vencimiento
     ,fec_ini_act_ppal
     ,id_actividad_ppal
     ,fec_ini_act_sria
     ,id_actividad_sria
     ,RANGO_ALQ
     ,CANT_REEMP
     ,ID_DOCUMENTO1_CDD
     ,ID_DOCUMENTO2_CDD
     ,ID_DOCUMENTO3_CDD
     ,ID_DOCUMENTO4_CDD
     ,id_organismo_alta
     )
  VALUES   (
    sysdate
    ,v_nro_tramite
    ,pTram_Capac_ult_anio
    ,p_Id_Entidad
    ,pTram_Cuil_Usu_CIDI
    ,pTram_Tipo_Tramite
    ,pTram_Id_vin_dom_legal
    ,pTram_Id_vin_dom_local
    ,p_fecha_ini_act
    ,v_id_Rep_Legal
    ,v_id_Gestor
    ,pTram_Id_origen_prov
    ,pTram_Cant_total_pers
    ,pTram_CantPersRelDep
    ,pTram_FechaVencimiento
    ,v_Fecha_Ini_Act_pri
    ,pTram_Id_Act_Pri
    ,v_Fecha_Ini_Act_sec
    ,pTram_Id_Act_Sec
    ,pTram_Rango_alquiler
    ,pTram_cant_reemp
    ,p_Id_Documento1_CDD
    ,p_Id_Documento2_CDD
    ,p_Id_Documento3_CDD
    ,p_Id_Documento4_CDD
    ,p_id_organismo
    );
    ELSE v_bandera :='E';
    END IF;
 EXCEPTION
 WHEN OTHERS THEN
  v_bandera := 'N' ;
  ROLLBACK;
  RAISE_APPLICATION_ERROR(-20001,'Error al insertar Tramite: '||sqlerrm);
END;

BEGIN
  
  
  t_comunes.pack_persona_juridica.modifica_persjur_unif(p_cuit => pEnt_CUIT,
                                                      p_razon_social => null,
                                                      p_nom_fan => null,
                                                      p_id_for_jur => null,
                                                      p_id_cond_iva => null,
                                                      p_id_aplicacion => v_IdApp,
                                                      p_nro_ing_bruto => null,
                                                      p_id_cond_ingbruto => null,
                                                      p_abreviado => null,
                                                      p_fec_inscripcion => null,
                                                      p_id_estado => null,
                                                      p_gravamen => null,
                                                      p_intervencion => null,
                                                      p_nro_matricula => null,
                                                      p_nro_hab_muni => p_nro_habMunicipal,
                                                      o_resultado => o_resultado_pj
                                                      );  
                                                   
EXCEPTION
  WHEN OTHERS THEN
    v_bandera := 'N' ;

  RAISE_APPLICATION_ERROR(-20001,'Error al insertar Estado: '||sqlerrm);
END;

BEGIN
 CASE v_bandera
   WHEN 'S' THEN
      commit;
      pResultado:='OK';
      pNroTramite:= v_nro_tramite;
   WHEN 'E' THEN
      pResultado:='EXISTE';
      ROLLBACK;
   WHEN 'N' THEN
      pResultado:='ERROR';
      ROLLBACK;
 END CASE;

END;
 return;

END pr_insert_inscripcion;


/*PROCEDURE pr_Insert_Entidad (
    pCUIT_format IN varchar2,
    pCUIT       IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pLocal      IN sifcos.t_sif_entidades.local%TYPE DEFAULT NULL,
    pOficina    IN sifcos.t_sif_entidades.oficina%TYPE DEFAULT NULL,
    pStand      IN sifcos.t_sif_entidades.stand%TYPE DEFAULT NULL,
    pPropietario IN sifcos.t_sif_entidades.propietario%TYPE DEFAULT NULL,
    p_Latitud   IN sifcos.t_sif_entidades.latitud_ubi%TYPE DEFAULT NULL,
    p_Longitud  IN sifcos.t_sif_entidades.longitud_ubi%TYPE DEFAULT NULL,
    pNombreComercio in varchar2,
    pEnt_Nro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT varchar2,
    pIdEntidad OUT number
   )
   IS

   v_bandera  varchar2(1) := 'S' ;
   v_id_entidad sifcos.t_sif_entidades.id_entidad%type;
   v_CPerJur number (4) :=0;
   
   v_cuit_format varchar2(15):=pCUIT_format;
   v_Nro_sifcos number(10):=0;
   v_razon_social varchar2(80);
   v_DGR number (12);
   v_fecha_ini_act date;
   v_id_sede varchar2(2):='00';
   v_res_ent varchar2(100);


   BEGIN
    
   
   SELECT max(e.id_entidad)+1
     INTO v_ID_ENTIDAD
     FROM sifcos.t_sif_entidades e;
     
     
      SELECT COUNT(*)
   INTO v_CPerJur
   from t_comunes.vt_pers_juridicas p
   where p.cuit=pCUIT and p.id_sede='00';
   
   
     IF v_CPerJur = 0 THEN
     SELECT
     ( SELECT distinct i.razon_social
       FROM tax.vw_Inscriptos_Industria i
       WHERE i.cuit=v_cuit_format  and i.FECHA_FIN is null) razon_social,
     ( SELECT distinct i.numero_inscripcion
       FROM tax.vw_Inscriptos_Industria i
       WHERE i.cuit=v_cuit_format  and i.FECHA_FIN is null) DGR,
     ( SELECT distinct to_date(to_char(i.fecha_inicio_actividades,'dd/mm/yyyy') , 'dd/mm/yyyy')
       FROM tax.vw_Inscriptos_Industria i
       WHERE i.cuit=v_cuit_format  and i.FECHA_FIN is null) fec_ini_act
       INTO v_razon_social,v_DGR,v_fecha_ini_act
     FROM dual;
     
     IF v_razon_social IS NULL THEN
       SELECT p.NOV_NOMBRE ||' ' ||p.NOV_APELLIDO
       into v_razon_social
       FROM rcivil.vt_pk_persona_cuil p
       where p.NRO_OTRO_DOCUMENTO=pCUIT;
     END IF;
     
       t_comunes.pack_persona_juridica.INSERTA_PERSJUR(pCUIT
                                                     ,v_razon_social
                                                     ,null
                                                     ,null
                                                     ,null
                                                     ,98
                                                     ,v_fecha_ini_act
                                                     ,v_DGR
                                                     ,null
                                                     ,v_id_sede
                                                     ,v_res_ent
                                                     );
     END IF;
     
     
     
     
          
    IF v_Nro_sifcos IS NULL THEN
     v_Nro_sifcos:=0;
    ELSE 
     v_Nro_sifcos:=pEnt_Nro_sifcos;
    END IF;
       
    
    BEGIN
    
         INSERT INTO sifcos.t_sif_entidades
           (id_entidad
            ,cuit
            ,id_sede
            ,local
            ,oficina
            ,stand
            ,cobertura_medica
            ,propietario
            ,seguro_local
            ,nro_sifcos
            ,Latitud_ubi
            ,Longitud_ubi
            ,resenia
            ,observacion
           )
           VALUES
           (V_ID_ENTIDAD
            ,pCUIT
            ,v_id_sede
            ,pLocal
            ,pOficina
            ,pStand
            ,'S'
            ,pPropietario
            ,'S'
            ,v_Nro_sifcos
            ,p_Latitud
            ,p_Longitud
            ,'.SIFCOS.'
            ,pNombreComercio
           );
      

     EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar la Entidad: '||sqlerrm);
      END;
      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           pIdEntidad:=V_ID_ENTIDAD;
           commit;

      ELSE
           pResultado:='ERROR';
           ROLLBACK; 
     END IF ;


    return;

END pr_Insert_Entidad;*/

--- edicion: Fernando Budassi --- fecha: 06/2016 
--- utilizado en pagina Inscripcion.aspx.cs
 PROCEDURE pr_Insert_Entidad (
    pCUIT_format IN varchar2,
    pCUIT       IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pId_Sede    IN sifcos.t_sif_entidades.id_sede%TYPE DEFAULT NULL,
    pLocal      IN sifcos.t_sif_entidades.local%TYPE DEFAULT NULL,
    pOficina    IN sifcos.t_sif_entidades.oficina%TYPE DEFAULT NULL,
    pStand      IN sifcos.t_sif_entidades.stand%TYPE DEFAULT NULL,
    pCobertura  IN sifcos.t_sif_entidades.cobertura_medica%TYPE DEFAULT NULL,
    pSeguro     IN sifcos.t_sif_entidades.seguro_local%TYPE DEFAULT NULL,
    pPropietario IN sifcos.t_sif_entidades.propietario%TYPE DEFAULT NULL,
    p_Latitud   IN sifcos.t_sif_entidades.latitud_ubi%TYPE DEFAULT NULL,
    p_Longitud  IN sifcos.t_sif_entidades.longitud_ubi%TYPE DEFAULT NULL,
    pNro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResenia IN sifcos.t_sif_entidades.resenia%TYPE DEFAULT NULL,
    pNombre_fantasia IN sifcos.t_sif_entidades.observacion%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS

   v_bandera  varchar2(1) := 'S' ;
   v_id_entidad sifcos.t_sif_entidades.id_entidad%type;
  
   v_CPerJur number (4) :=0;
   v_cuit_format varchar2(15):=pCUIT_format;
   v_Nro_sifcos number(10):=pNro_sifcos;
   v_razon_social varchar2(80);
   v_DGR number (12);
   v_fecha_ini_act date;
   v_id_sede varchar2(2);
   v_res_ent varchar2(100);
   v_Resenia varchar2(100) := pResenia;
   v_Nombre_fantasia varchar2(100) := pNombre_fantasia;


   BEGIN
    
   SELECT e.id_entidad
     INTO v_ID_ENTIDAD
     FROM sifcos.t_sif_entidades e
     WHERE  e.nro_sifcos = pNro_sifcos
        and (e.latitud_ubi is null or (e.latitud_ubi<>'ENTIDAD NO VALIDA'))
        and e.id_entidad=(select max(e1.id_entidad) 
                         from sifcos.t_sif_entidades e1
                         where e1.nro_sifcos=e.nro_sifcos);

 IF pNro_sifcos = 0 THEN
     SELECT max (e.id_entidad)+1 
     INTO v_ID_ENTIDAD
     FROM sifcos.t_sif_entidades e;
 END IF; 
    
    BEGIN
   -- VERIFICA SI EST? EN IPJ
   SELECT COUNT(*)
   INTO v_CPerJur
   from t_comunes.vt_pers_juridicas p
   where p.cuit=pCUIT and p.id_sede='00';
        
   IF v_CPerJur = 0 THEN
     SELECT
     ( SELECT distinct i.razon_social
       FROM tax.vw_Inscriptos_Industria i
       WHERE i.cuit=v_cuit_format  and i.FECHA_FIN is null) razon_social,
     ( SELECT distinct i.numero_inscripcion
       FROM tax.vw_Inscriptos_Industria i
       WHERE i.cuit=v_cuit_format  and i.FECHA_FIN is null) DGR,
     ( SELECT distinct to_date(to_char(i.fecha_inicio_actividades,'dd/mm/yyyy') , 'dd/mm/yyyy')
       FROM tax.vw_Inscriptos_Industria i
       WHERE i.cuit=v_cuit_format  and i.FECHA_FIN is null) fec_ini_act
       INTO v_razon_social,v_DGR,v_fecha_ini_act
     FROM dual;
     
     IF v_razon_social = null THEN
       SELECT p.NOV_NOMBRE ||' ' ||p.NOV_APELLIDO
       into v_razon_social
       FROM rcivil.vt_pk_persona_cuil p
       where p.NRO_OTRO_DOCUMENTO=pCUIT;
     END IF;
     
       t_comunes.pack_persona_juridica.INSERTA_PERSJUR(pCUIT
                                                     ,v_razon_social
                                                     ,null
                                                     ,null
                                                     ,null
                                                     ,98
                                                     ,v_fecha_ini_act
                                                     ,v_DGR
                                                     ,null
                                                     ,v_id_sede
                                                     ,v_res_ent
                                                     );
     END IF;
   
   /*
   1- pNro_sifcos(tiene un valor) REEMPADRONAMIENTO CON NRO_SIFCOS VIEJO no esta en t_sif_entidades CEnt = 0  --> INSERT ENTIDAD 
   2- pNro_sifcos(tiene un valor)REEMPADRONAMIENTO CON NRO_SIFCOS NUEVO esta en t_sif_entidades  CEnt > 1 --> UPDATE ENTIDAD 
   3- pNro_sifcos(es null) ALTA no est? en t_sif_entidades CEnt=0 --> INSERT ENTIDAD
   */
   
     IF v_Nro_sifcos is null  THEN
       v_Nro_sifcos := 0;
     ELSE
       v_Nro_sifcos := pNro_sifcos;  
     END IF;
     IF v_Nro_sifcos = 0 THEN
     INSERT INTO sifcos.t_sif_entidades
     (id_entidad
      ,cuit
      ,id_sede
      ,local
      ,oficina
      ,stand
      ,cobertura_medica
      ,propietario
      ,seguro_local
      ,nro_sifcos
      ,Latitud_ubi
      ,Longitud_ubi
      ,resenia
      ,observacion
     )
     VALUES
     (V_ID_ENTIDAD
      ,pCUIT
      ,pID_SEDE
      ,pLocal
      ,pOficina
      ,pStand
      ,pCobertura
      ,pPropietario
      ,pSeguro
      ,v_Nro_sifcos
      ,p_Latitud
      ,p_Longitud
      ,v_Resenia
      ,v_Nombre_fantasia
     );
   ELSE
      PCK_SIFCOS_ACTUALIZACIONES.pr_Update_Entidad(
                                                    V_ID_ENTIDAD
                                                    ,pLocal
                                                    ,pOficina
                                                    ,pStand
                                                    ,pCobertura
                                                    ,pSeguro
                                                    ,pPropietario
                                                    ,p_Longitud
                                                    ,p_Latitud
                                                    ,v_Resenia
                                                    ,v_Nombre_fantasia
                                                    ,pResultado
                                                    );
             
   END IF;    

     EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar la Entidad: '||sqlerrm);
      END;
      IF  v_bandera = 'S'   THEN
           pResultado:=V_ID_ENTIDAD;
           commit;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Entidad;

 PROCEDURE pr_Insert_Entidad_migracion (
    pCUIT_format IN varchar2,
    pCUIT       IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pId_Sede    IN sifcos.t_sif_entidades.id_sede%TYPE DEFAULT NULL,
    pCobertura  IN sifcos.t_sif_entidades.cobertura_medica%TYPE DEFAULT NULL,
    pSeguro     IN sifcos.t_sif_entidades.seguro_local%TYPE DEFAULT NULL,
    pPropietario IN sifcos.t_sif_entidades.propietario%TYPE DEFAULT NULL,
    p_Latitud   IN sifcos.t_sif_entidades.latitud_ubi%TYPE DEFAULT NULL,
    p_Longitud  IN sifcos.t_sif_entidades.longitud_ubi%TYPE DEFAULT NULL,
    pNro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pNombre_fantasia IN sifcos.t_sif_entidades.observacion%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS

   v_bandera  varchar2(1) := 'S' ;
   v_id_entidad sifcos.t_sif_entidades.id_entidad%type;
  
   v_CPerJur number (4) :=0;
   v_cuit_format varchar2(15):=pCUIT_format;
   v_Nro_sifcos number(10):=pNro_sifcos;
   v_razon_social varchar2(80);
   v_DGR number (12);
   v_fecha_ini_act date;
   v_id_sede varchar2(2);
   v_res_ent varchar2(100);
   v_Resenia varchar2(100) := '';
   v_Nombre_fantasia varchar2(100) := pNombre_fantasia;


   BEGIN
     SELECT max (e.id_entidad)+1 
     INTO v_ID_ENTIDAD
     FROM sifcos.t_sif_entidades e;
 
    
    BEGIN
   -- VERIFICA SI EST? EN IPJ
   SELECT COUNT(*)
   INTO v_CPerJur
   from t_comunes.vt_pers_juridicas p
   where p.cuit=pCUIT and p.id_sede='00';
        
   IF v_CPerJur = 0 THEN
     SELECT
     ( SELECT distinct i.razon_social
       FROM tax.vw_Inscriptos_Industria i
       WHERE i.cuit=v_cuit_format  and i.FECHA_FIN is null) razon_social,
     ( SELECT distinct i.numero_inscripcion
       FROM tax.vw_Inscriptos_Industria i
       WHERE i.cuit=v_cuit_format  and i.FECHA_FIN is null) DGR,
     ( SELECT distinct to_date(to_char(i.fecha_inicio_actividades,'dd/mm/yyyy') , 'dd/mm/yyyy')
       FROM tax.vw_Inscriptos_Industria i
       WHERE i.cuit=v_cuit_format  and i.FECHA_FIN is null) fec_ini_act
       INTO v_razon_social,v_DGR,v_fecha_ini_act
     FROM dual;
     
     IF v_razon_social = null THEN
       SELECT p.NOV_NOMBRE ||' ' ||p.NOV_APELLIDO
       into v_razon_social
       FROM rcivil.vt_pk_persona_cuil p
       where p.NRO_OTRO_DOCUMENTO=pCUIT;
     END IF;
     
       t_comunes.pack_persona_juridica.INSERTA_PERSJUR(pCUIT
                                                     ,v_razon_social
                                                     ,null
                                                     ,null
                                                     ,null
                                                     ,98
                                                     ,v_fecha_ini_act
                                                     ,v_DGR
                                                     ,null
                                                     ,v_id_sede
                                                     ,v_res_ent
                                                     );
     END IF;
   
   /*
   1- pNro_sifcos(tiene un valor) REEMPADRONAMIENTO CON NRO_SIFCOS VIEJO no esta en t_sif_entidades CEnt = 0  --> INSERT ENTIDAD 
   2- pNro_sifcos(tiene un valor)REEMPADRONAMIENTO CON NRO_SIFCOS NUEVO esta en t_sif_entidades  CEnt > 1 --> UPDATE ENTIDAD 
   3- pNro_sifcos(es null) ALTA no est? en t_sif_entidades CEnt=0 --> INSERT ENTIDAD
   */
   
     INSERT INTO sifcos.t_sif_entidades
     (id_entidad
      ,cuit
      ,id_sede
      ,cobertura_medica
      ,propietario
      ,seguro_local
      ,nro_sifcos
      ,Latitud_ubi
      ,Longitud_ubi
      ,resenia
      ,observacion
     )
     VALUES
     (V_ID_ENTIDAD
      ,pCUIT
      ,pID_SEDE
      ,pCobertura
      ,pPropietario
      ,pSeguro
      ,pNro_sifcos
      ,p_Latitud
      ,p_Longitud
      ,v_Resenia
      ,v_Nombre_fantasia
     );
   
     EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar la Entidad migraci?n: '||sqlerrm);
      END;
      IF  v_bandera = 'S'   THEN
           pResultado:=V_ID_ENTIDAD;
           commit;

      ELSE
           pResultado:='ERROR: '||sqlerrm;
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Entidad_migracion;

--- edicion: Fernando Budassi --- fecha: 07/2016 -- modificado 09-05-2017
--- utilizado en pagina inscripcion.aspx.cs
 PROCEDURE pr_insert_baja_tramite (
    pEnt_Nro_sifcos          IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pTram_Cuil_Usu_CIDI      IN sifcos.t_sif_tramites_sifcos.cuil_usuario_cidi%TYPE DEFAULT NULL,
    pNroTramite              OUT number,
    pResultado               OUT varchar2
   ) IS

v_bandera  varchar2(1) := 'S' ;
v_ResHist varchar2(5);
v_nro_tramite sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%type;



BEGIN
  SELECT SEQ_TRAMITES.NEXTVAL
  INTO v_nro_tramite
  FROM DUAL;
  
  
  
BEGIN
  sifcos.pck_sifcos_inserciones.pr_Insert_HistEstado_Descrip(v_nro_tramite
                                                             ,1
                                                             ,pTram_Cuil_Usu_CIDI
                                                             ,'TRAMITE BAJA DE COMERCIO'
                                                             ,v_ResHist
                                                           );
  EXCEPTION
  WHEN OTHERS THEN
    v_bandera := 'N' ;
  ROLLBACK;
  RAISE_APPLICATION_ERROR(-20001,'Error al insertar Estado: '||sqlerrm);
  END;

BEGIN
/*Inserta el ?ltimo tramite del nro_sifcos enviado por parametro*/                                        
    INSERT INTO sifcos.t_sif_tramites_sifcos t
    ( fec_alta
     ,nro_tramite_sifcos
     ,capacitacion_ult_anio
     ,id_entidad
     ,cuil_usuario_cidi
     ,id_tipo_tramite
     ,id_vin_dom_legal
     ,id_vin_dom_local
     ,fec_ini_tramite
     ,id_cargo_entidad
     ,id_gestor_entidad
     ,id_origen_proveedor
     ,cant_pers_total
     ,cant_pers_rel_dependencia
     ,fec_vencimiento
     ,fec_ini_act_ppal
     ,id_actividad_ppal
     ,fec_ini_act_sria
     ,id_actividad_sria
     ,RANGO_ALQ
     ,cant_reemp
     ,id_organismo_alta
     )
  SELECT sysdate fec_alta,
          v_nro_tramite nro_tramite_sifcos,
          t.capacitacion_ult_anio,
          t.id_entidad,
          pTram_Cuil_Usu_CIDI cuil_usuario_cidi,
          2 id_tipo_tramite,
          t.id_vin_dom_legal,
          t.id_vin_dom_local,
          t.fec_ini_tramite,
          t.id_cargo_entidad,
          t.id_gestor_entidad,
          1 id_origen_proveedor,
          t.cant_pers_total,
          t.cant_pers_rel_dependencia,
          t.fec_vencimiento,
          t.fec_ini_act_ppal,
          t.id_actividad_ppal,
          t.fec_ini_act_sria,
          t.id_actividad_sria,
          t.rango_alq,
          t.cant_reemp,
          t.id_organismo_alta
          
          
   from sifcos.t_sif_tramites_sifcos t
   join sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad
   where e.nro_sifcos=pEnt_Nro_sifcos and 
         t.fec_alta = (select max(t2.fec_alta) 
                       from sifcos.t_sif_tramites_sifcos t2
                       join sifcos.t_sif_entidades e2 on t2.id_entidad=e2.id_entidad
                       where e2.nro_sifcos=pEnt_Nro_sifcos)
   ;
   CASE v_bandera
   WHEN 'S' THEN
      commit;
      pResultado:='OK';
      pNroTramite:= v_nro_tramite;
   WHEN 'N' THEN
      pResultado:='ERROR';
      ROLLBACK;
 END CASE; 
 
 EXCEPTION
 WHEN OTHERS THEN
  ROLLBACK;
  RAISE_APPLICATION_ERROR(-20001,'Error al insertar Tramite de baja: '||sqlerrm);

 


 return;
END;
END pr_insert_baja_tramite;

 PROCEDURE pr_insert_baja_tramite_cese (
    pEnt_Nro_sifcos          IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pTram_Cuil_Usu_CIDI      IN sifcos.t_sif_tramites_sifcos.cuil_usuario_cidi%TYPE DEFAULT NULL,
    pFechaCese               IN date,
    pDoc1                    IN sifcos.t_sif_tramites_sifcos.id_documento1_cdd%TYPE DEFAULT NULL,
    pDoc2                    IN sifcos.t_sif_tramites_sifcos.id_documento2_cdd%TYPE DEFAULT NULL,
    pNroTramite              OUT number,
    pResultado               OUT varchar2
   ) IS

v_bandera  varchar2(1) := 'S' ;
v_ResHist varchar2(5);
v_nro_tramite sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%type;



BEGIN
  SELECT SEQ_TRAMITES.NEXTVAL
  INTO v_nro_tramite
  FROM DUAL;
  
  
  
BEGIN
  sifcos.pck_sifcos_inserciones.pr_Insert_HistEstado_Descrip(v_nro_tramite
                                                             ,1
                                                             ,pTram_Cuil_Usu_CIDI
                                                             ,'TRAMITE BAJA DE COMERCIO'
                                                             ,v_ResHist
                                                           );
  EXCEPTION
  WHEN OTHERS THEN
    v_bandera := 'N' ;
  ROLLBACK;
  RAISE_APPLICATION_ERROR(-20001,'Error al insertar Estado: '||sqlerrm);
  END;

BEGIN
/*Inserta el ?ltimo tramite del nro_sifcos enviado por parametro*/                                        
    INSERT INTO sifcos.t_sif_tramites_sifcos t
    ( fec_alta
     ,nro_tramite_sifcos
     ,capacitacion_ult_anio
     ,id_entidad
     ,cuil_usuario_cidi
     ,id_tipo_tramite
     ,id_vin_dom_legal
     ,id_vin_dom_local
     ,fec_ini_tramite
     ,id_cargo_entidad
     ,id_gestor_entidad
     ,id_origen_proveedor
     ,cant_pers_total
     ,cant_pers_rel_dependencia
     ,fec_vencimiento
     ,fec_ini_act_ppal
     ,id_actividad_ppal
     ,fec_ini_act_sria
     ,id_actividad_sria
     ,RANGO_ALQ
     ,cant_reemp
     ,id_organismo_alta
     ,ID_DOCUMENTO1_CDD
     ,ID_DOCUMENTO2_CDD
     )
  SELECT sysdate fec_alta,
          v_nro_tramite nro_tramite_sifcos,
          t.capacitacion_ult_anio,
          t.id_entidad,
          pTram_Cuil_Usu_CIDI cuil_usuario_cidi,
          2 id_tipo_tramite,
          t.id_vin_dom_legal,
          t.id_vin_dom_local,
          pFechaCese,
          t.id_cargo_entidad,
          t.id_gestor_entidad,
          1 id_origen_proveedor,
          t.cant_pers_total,
          t.cant_pers_rel_dependencia,
          pFechaCese,
          t.fec_ini_act_ppal,
          t.id_actividad_ppal,
          t.fec_ini_act_sria,
          t.id_actividad_sria,
          t.rango_alq,
          t.cant_reemp,
          t.id_organismo_alta,
          pDoc1,
          pDoc2
          
          
   from sifcos.t_sif_tramites_sifcos t
   join sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad
   where e.nro_sifcos=pEnt_Nro_sifcos and 
         t.fec_alta = (select max(t2.fec_alta) 
                       from sifcos.t_sif_tramites_sifcos t2
                       join sifcos.t_sif_entidades e2 on t2.id_entidad=e2.id_entidad
                       where e2.nro_sifcos=pEnt_Nro_sifcos)
   ;
   CASE v_bandera
   WHEN 'S' THEN
      commit;
      pResultado:='OK';
      pNroTramite:= v_nro_tramite;
   WHEN 'N' THEN
      pResultado:='ERROR';
      ROLLBACK;
 END CASE; 
 
 EXCEPTION
 WHEN OTHERS THEN
  ROLLBACK;
  RAISE_APPLICATION_ERROR(-20001,'Error al insertar Tramite de baja: '||sqlerrm);

 


 return;
END;
END pr_insert_baja_tramite_cese;


--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina inscripcion.aspx.cs
--- se agrega un control para verificar si el registro ya ha sido cargado 12-09-2016
 PROCEDURE pr_Insert_Comunicacion (
    p_entidad IN VARCHAR2,
    p_email IN VARCHAR2,
    p_pagWeb IN VARCHAR2,
    p_nroCel IN VARCHAR2,
    p_CodAreaCel IN VARCHAR2,
    p_nroTelFijo IN VARCHAR2,
    p_CodAreaTelFijo IN VARCHAR2,
    p_Facebook IN VARCHAR2,
    --p_CodAreaCelConta IN t_comunes.t_comunicaciones.cod_area%TYPE DEFAULT NULL,
    --p_nroCelConta IN t_comunes.t_comunicaciones.nro_mail%TYPE DEFAULT NULL,
    --p_email_conta IN t_comunes.t_comunicaciones.nro_mail%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS

   v_ResCom varchar2(5);
   v_Tel varchar2(3);
   v_Cel varchar2(3);
   v_Mail varchar2(3);
   v_PagWeb varchar2(3);
   v_Facebook varchar2(3);
   v_idApp number(2):=98;
   v_idTipoCom varchar(2);
   v_Origen_tabla varchar2(30):='sifcos.t_sif_tramites_sifcos';
  


   BEGIN
     --verifico si existen registros anteriores
     select 
     (select count(*) from t_comunes.vt_comunicaciones c
     where c.id_tipo_comunicacion=01
     and c.id_entidad=p_entidad
     and c.id_aplicacion=v_idApp) tel,
     (select count(*) from t_comunes.vt_comunicaciones c
     where c.id_tipo_comunicacion=07
     and c.id_entidad=p_entidad
     and c.id_aplicacion=v_idApp) cel,
     (select count(*) from t_comunes.vt_comunicaciones c
     where c.id_tipo_comunicacion=11
     and c.id_entidad=p_entidad
     and c.id_aplicacion=v_idApp) mail,
     (select count(*) from t_comunes.vt_comunicaciones c
     where c.id_tipo_comunicacion=12
     and c.id_entidad=p_entidad
     and c.id_aplicacion=v_idApp) pagweb,
     (select count(*) from t_comunes.vt_comunicaciones c
     where c.id_tipo_comunicacion=17
     and c.id_entidad=p_entidad
     and c.id_aplicacion=v_idApp) facebook
     into v_Tel,v_Cel,v_Mail,v_PagWeb,v_Facebook
     from dual;
     
     --inserto telefono fijo principal
     IF (p_nroTelFijo IS NOT NULL and p_CodAreaTelFijo IS NOT NULL and v_Tel=0) THEN
      v_idTipoCom:='01';
      t_comunes.pack_comunicaciones.INSERTA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_nroTelFijo
                                                           ,p_CodAreaTelFijo
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
     ELSE IF (p_nroTelFijo IS NOT NULL and p_CodAreaTelFijo IS NOT NULL) then
             v_idTipoCom:='01';
             t_comunes.pack_comunicaciones.MODIFICA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_nroTelFijo
                                                           ,p_CodAreaTelFijo
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);                                                                  
            END IF;
     END IF;

     --inserto telefono celular
     IF (p_nroCel IS NOT NULL and p_CodAreaCel IS NOT NULL and v_Cel=0) THEN
      v_idTipoCom:='07';
      t_comunes.pack_comunicaciones.INSERTA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_nroCel
                                                           ,p_CodAreaCel
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
     ELSE IF (p_nroCel IS NOT NULL and p_CodAreaCel IS NOT NULL) THEN
       v_idTipoCom:='07';
       t_comunes.pack_comunicaciones.MODIFICA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_nroCel
                                                           ,p_CodAreaCel
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
       END IF;                                                           
     END IF;
     
     --inserto telefono contador
     /*IF (p_nroCelConta IS NOT NULL and p_CodAreaCelConta IS NOT NULL) THEN
      v_idTipoCom:='03';
      t_comunes.pack_comunicaciones.INSERTA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_nroCelConta
                                                           ,p_CodAreaCelConta
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
     END IF;*/

     --inserto email contador
     /*IF (p_email_conta IS NOT NULL ) THEN
     v_idTipoCom:='11';
      t_comunes.pack_comunicaciones.INSERTA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad+'c'
                                                           ,p_email_conta
                                                           ,null
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
     END IF;*/

     --inserto email
     IF (p_email IS NOT NULL and v_Mail=0) THEN
      v_idTipoCom:='11';
      t_comunes.pack_comunicaciones.INSERTA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_email
                                                           ,null
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
     ELSE IF (p_email IS NOT NULL) THEN
       v_idTipoCom:='11';
       t_comunes.pack_comunicaciones.MODIFICA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_email
                                                           ,null
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
       END IF;                                                           
     END IF;

     --inserto pagina web
     IF (p_pagWeb IS NOT NULL and v_PagWeb=0 ) THEN
      v_idTipoCom:='12';
      t_comunes.pack_comunicaciones.INSERTA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_pagWeb
                                                           ,null
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
     ELSE IF (p_pagWeb IS NOT NULL) THEN
        v_idTipoCom:='12';
        t_comunes.pack_comunicaciones.MODIFICA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_pagWeb
                                                           ,null
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
       END IF;                                                      
                                                           
     END IF;


     --inserto FACEBOOK
     IF (p_Facebook IS NOT NULL and v_Facebook=0) THEN
      v_idTipoCom:='17';
      t_comunes.pack_comunicaciones.INSERTA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_Facebook
                                                           ,null
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
     ELSE IF (p_Facebook IS NOT NULL) THEN
            v_idTipoCom:='17';
            t_comunes.pack_comunicaciones.MODIFICA_COMUNICACIONES(v_idTipoCom
                                                           ,p_entidad
                                                           ,p_Facebook
                                                           ,null
                                                           ,v_idApp
                                                           ,v_Origen_tabla
                                                           ,v_ResCom);
       END IF;                                                           
     END IF;

     IF v_ResCom = 'OK' THEN
       pResultado := 'OK';
     ELSE pResultado:= 'ERROR';

     END IF;


    return;

END pr_Insert_Comunicacion;

-- edicion: Fernando Budassi --- fecha: 03/2016
--- utilizado en pagina ABMSuperficie.aspx.cs
PROCEDURE pr_Insert_Tipo_Superficie (
    pN_Superficie IN sifcos.t_sif_tipos_superficie.n_tipo_superficie%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS

   v_bandera  varchar2(1) := 'S' ;
   v_RegEnc number(2) :=0;
   v_IdSuperficie sifcos.t_sif_tipos_superficie.id_tipo_superficie%TYPE;

   BEGIN
     SELECT NVL(max(id_tipo_superficie)+1,1)
     INTO v_IdSuperficie
     FROM sifcos.t_sif_tipos_superficie;

     SELECT count(*)
     INTO v_RegEnc
     FROM sifcos.t_sif_tipos_superficie s
     WHERE s.n_tipo_superficie=upper(pN_Superficie);

    BEGIN
   IF v_RegEnc=0 THEN
     INSERT INTO sifcos.t_sif_tipos_superficie
     (ID_TIPO_SUPERFICIE
      ,N_TIPO_SUPERFICIE
     )
     VALUES
     (v_IdSuperficie
      ,pN_Superficie
     );
     ELSE pResultado:='REGISTRO EXISTENTE';
          return;
     END IF;

      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar tipo de superficie: '||sqlerrm);
      END;
      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Tipo_Superficie;

--- edicion: Fernando Budassi --- fecha: 07/2016 
--- utilizado en pagina Inscripcion.aspx.cs
PROCEDURE pr_Insert_Superficies_Empresa (
    pNro_Tramite IN sifcos.t_sif_superficies_empresa.id_tramite_sifcos%TYPE DEFAULT NULL,
    pId_Tipo_superficie IN sifcos.t_sif_tipos_superficie.id_tipo_superficie%TYPE DEFAULT NULL,
    pCantidad IN sifcos.t_sif_superficies_empresa.superficie%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS

   v_bandera  varchar2(1) := 'S' ;

   BEGIN


    BEGIN
    INSERT INTO sifcos.t_sif_superficies_empresa
     (ID_TRAMITE_SIFCOS
      ,id_tipo_superficie
      ,superficie
     )
     VALUES
     (pNro_Tramite
      ,pId_Tipo_superficie
      ,pCantidad
     );
      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar Superficies de Empresa: '||sqlerrm);
      END;
      IF  v_bandera = 'S'   THEN
           pResultado:='OK';



      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Superficies_Empresa;

--- edicion: Fernando Budassi --- fecha: 07/2016 
--- utilizado en pagina Inscripcion.aspx.cs procedimiento temporal
PROCEDURE pr_Insert_Rep_Legal (
    pGest_cuil      IN varchar2,
    pGest_id_cargo  IN sifcos.t_sif_rep_legal.id_cargo%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_id_sexo sifcos.t_sif_rep_legal.id_sexo%type;
   v_nro_documento sifcos.t_sif_rep_legal.nro_documento%type;
   v_pai_cod_pais sifcos.t_sif_rep_legal.pai_cod_pais%type;
   v_id_numero sifcos.t_sif_rep_legal.id_numero%type;
   v_reg varchar2(5);
   v_existe varchar2(1):='N';
   v_id_repLegal varchar2(10);
   v_bandera  varchar2(1) := 'S' ;
   v_dig_verif  varchar2(5);
   v_DNI  varchar2(10);
   v_ID_REP_LEGAL  sifcos.t_sif_rep_legal.id_rep_legal%type;
  BEGIN
  
  SELECT SEQ_SIF_REP_LEGAL.NEXTVAL
     INTO v_ID_REP_LEGAL
     FROM DUAL;
  
  SELECT SUBSTR(pGest_cuil,3,1)
  INTO v_dig_verif
  FROM DUAL;
  
  IF v_dig_verif = 0 THEN
    SELECT SUBSTR(pGest_cuil,4,7)
    INTO v_DNI
    FROM DUAL;
  ELSE
    SELECT SUBSTR(pGest_cuil,3,8)
    INTO v_DNI
    FROM DUAL;
  END IF;

   DECLARE CURSOR persona IS
        SELECT p.ID_SEXO,
               p.NRO_DOCUMENTO,
               p.PAI_COD_PAIS,
               p.ID_NUMERO
        FROM rcivil.vt_pk_persona p
        WHERE p.NRO_DOCUMENTO = v_DNI;

    BEGIN
      OPEN persona;
      FETCH persona INTO v_id_sexo,v_nro_documento,v_pai_cod_pais,v_id_numero;
      
      SELECT  count(*) 
      INTO v_reg
      FROM sifcos.t_sif_rep_legal r
      WHERE r.ID_SEXO=v_id_sexo
        AND r.NRO_DOCUMENTO=v_nro_documento
        AND r.PAI_COD_PAIS=v_pai_cod_pais
        AND r.ID_NUMERO=v_id_numero;
      
      IF(v_reg<>0)THEN  
      SELECT r.id_rep_legal 
      INTO v_id_repLegal
      FROM sifcos.t_sif_rep_legal r
      WHERE r.ID_SEXO=v_id_sexo
        AND r.NRO_DOCUMENTO=v_nro_documento
        AND r.PAI_COD_PAIS=v_pai_cod_pais
        AND r.ID_NUMERO=v_id_numero;
      
      END IF;
           
      
      IF(v_reg=0 and v_nro_documento is not null) THEN
      INSERT INTO sifcos.t_sif_rep_legal
     (id_rep_legal
      ,id_cargo
      ,id_sexo
      ,nro_documento
      ,pai_cod_pais
      ,id_numero
     )
      VALUES
     (v_ID_REP_LEGAL
      ,pGest_id_cargo
      ,v_id_sexo
      ,v_nro_documento
      ,v_pai_cod_pais
      ,v_id_numero
     );
     ELSE
       UPDATE sifcos.t_sif_rep_legal  
       SET id_cargo=pGest_id_cargo
       WHERE id_rep_legal = v_id_repLegal;
       v_existe:='S';
     END IF;

      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar Representante Legal: '||sqlerrm);
      END;
      IF(v_existe='S')THEN
        pResultado:=v_id_repLegal;
        ELSE
        pResultado:=v_ID_REP_LEGAL;
      END IF;
      
      IF  v_bandera = 'S'   THEN
           commit;
      ELSE
          pResultado:='ERROR';
           ROLLBACK;
      END IF ;


    return;

END pr_Insert_Rep_Legal;
/*PROCEDURE pr_Insert_Rep_Legal (
    pRepLegal_dni      IN varchar2,
    pRepLegal_id_numero IN varchar2,
    pRepLegal_id_cargo  IN sifcos.t_sif_rep_legal.id_cargo%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_id_sexo sifcos.t_sif_rep_legal.id_sexo%type;
   v_nro_documento sifcos.t_sif_rep_legal.nro_documento%type;
   v_pai_cod_pais sifcos.t_sif_rep_legal.pai_cod_pais%type;
   v_id_numero sifcos.t_sif_rep_legal.id_numero%type;
   v_reg varchar2(5);
   v_existe varchar2(1):='N';
   v_id_repLegal varchar2(10);
   v_bandera  varchar2(1) := 'S' ;
   v_ID_REP_LEGAL  sifcos.t_sif_rep_legal.id_rep_legal%type;
  BEGIN
  
  SELECT SEQ_SIF_REP_LEGAL.NEXTVAL
     INTO v_ID_REP_LEGAL
     FROM DUAL;

   DECLARE CURSOR persona IS
        SELECT p.ID_SEXO,
               p.NRO_DOCUMENTO,
               p.PAI_COD_PAIS,
               p.ID_NUMERO
        FROM rcivil.vt_pk_persona p
        WHERE p.NRO_DOCUMENTO = pRepLegal_dni
          and p.ID_NUMERO = pRepLegal_id_numero;

    BEGIN
      OPEN persona;
      FETCH persona INTO v_id_sexo,v_nro_documento,v_pai_cod_pais,v_id_numero;
      
      SELECT  count(*) 
      INTO v_reg
      FROM sifcos.t_sif_rep_legal r
      WHERE r.ID_SEXO=v_id_sexo
        AND r.NRO_DOCUMENTO=v_nro_documento
        AND r.PAI_COD_PAIS=v_pai_cod_pais
        AND r.ID_NUMERO=v_id_numero;
      
      IF(v_reg<>0)THEN  
      SELECT r.id_rep_legal 
      INTO v_id_repLegal
      FROM sifcos.t_sif_rep_legal r
      WHERE r.ID_SEXO=v_id_sexo
        AND r.NRO_DOCUMENTO=v_nro_documento
        AND r.PAI_COD_PAIS=v_pai_cod_pais
        AND r.ID_NUMERO=v_id_numero;
      
      END IF;
           
      
      IF(v_reg=0 and v_nro_documento is not null) THEN
      INSERT INTO sifcos.t_sif_rep_legal
     (id_rep_legal
      ,id_cargo
      ,id_sexo
      ,nro_documento
      ,pai_cod_pais
      ,id_numero
     )
      VALUES
     (v_ID_REP_LEGAL
      ,pRepLegal_id_cargo
      ,v_id_sexo
      ,v_nro_documento
      ,v_pai_cod_pais
      ,v_id_numero
     );
     ELSE
       UPDATE sifcos.t_sif_rep_legal  
       SET id_cargo=pRepLegal_id_cargo
       WHERE id_rep_legal = v_id_repLegal;
       v_existe:='S';
     END IF;

      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar Representante Legal: '||sqlerrm);
      END;
      IF(v_existe='S')THEN
        pResultado:=v_id_repLegal;
        ELSE
        pResultado:=v_ID_REP_LEGAL;
      END IF;
      
      IF  v_bandera = 'S'   THEN
           commit;
      ELSE
          pResultado:='ERROR';
           ROLLBACK;
      END IF ;


    return;

END pr_Insert_Rep_Legal;*/

--- edicion: Fernando Budassi --- fecha: 07/2016 
--- utilizado en pagina Inscripcion.aspx.cs
PROCEDURE pr_Insert_Gestores (
    pGest_cuil               IN varchar2,
    pGest_id_tipo_gestor     IN sifcos.t_sif_gestores.id_tipo_gestor%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_id_sexo sifcos.t_sif_gestores.id_sexo%type;
   v_nro_documento sifcos.t_sif_gestores.nro_documento%type;
   v_pai_cod_pais sifcos.t_sif_gestores.pai_cod_pais%type;
   v_id_numero sifcos.t_sif_gestores.id_numero%type;
   v_bandera  varchar2(1):='S';
   v_idGestor varchar2(10);
   v_ID_GESTOR  sifcos.t_sif_Gestores.id_gestor%type;
   v_reg varchar2(5);
   v_existe varchar2(1):='N';
   
  BEGIN

    SELECT SEQ_SIF_GESTORES.NEXTVAL
     INTO v_ID_GESTOR
     FROM DUAL;

   DECLARE CURSOR persona IS
        SELECT p.ID_SEXO,
               p.NRO_DOCUMENTO,
               p.PAI_COD_PAIS,
               p.ID_NUMERO
        FROM rcivil.vt_pk_persona_cuil p
        WHERE p.NRO_OTRO_DOCUMENTO = pGest_cuil;

    BEGIN
      OPEN persona;
      FETCH persona INTO v_id_sexo,v_nro_documento,v_pai_cod_pais,v_id_numero;
      
      SELECT  count(*) 
      INTO v_reg
      FROM sifcos.t_sif_gestores g
      WHERE g.ID_SEXO=v_id_sexo
        AND g.NRO_DOCUMENTO=v_nro_documento
        AND g.PAI_COD_PAIS=v_pai_cod_pais
        AND g.ID_NUMERO=v_id_numero;
      
      IF(v_reg<>0)THEN  
      SELECT g.id_gestor 
      INTO v_idGestor
      FROM sifcos.t_sif_gestores g
      WHERE g.ID_SEXO=v_id_sexo
        AND g.NRO_DOCUMENTO=v_nro_documento
        AND g.PAI_COD_PAIS=v_pai_cod_pais
        AND g.ID_NUMERO=v_id_numero;
      END IF;
           
      
      IF(v_reg=0 and v_nro_documento is not null) THEN
      INSERT INTO sifcos.t_sif_Gestores
     (id_gestor
      ,id_tipo_gestor
      ,id_sexo
      ,nro_documento
      ,pai_cod_pais
      ,id_numero
     )
     VALUES
     (v_ID_GESTOR
      ,pGest_id_tipo_gestor
      ,v_id_sexo
      ,v_nro_documento
      ,v_pai_cod_pais
      ,v_id_numero
     );
     ELSE IF v_reg<>0 THEN
       UPDATE sifcos.t_sif_gestores g  
       SET g.id_tipo_gestor=pGest_id_tipo_gestor
       WHERE g.id_gestor = v_idGestor;
        v_existe:='S';
       END IF;
       
     END IF;

      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar Gestor: '||sqlerrm);
      END;
      
      IF(v_existe='S')THEN
        pResultado:=v_idGestor;
        ELSE
        pResultado:=v_ID_GESTOR;
      END IF;
      
      IF  v_bandera = 'S'   THEN
          commit;
      ELSE
          pResultado:='ERROR';
           ROLLBACK;
      END IF ;
    return;

END pr_Insert_Gestores;


--- edicion: Fernando Budassi --- fecha: 03/2016 
--- utilizado en pagina ABMProductos.aspx.cs

PROCEDURE pr_Insert_Producto (
    pN_Producto IN sifcos.t_sif_productos.n_producto%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS

   v_bandera  varchar2(1) := 'S' ;
   v_RegEnc number(2) :=0;
   v_IdProducto sifcos.t_sif_productos.id_producto%TYPE;
   BEGIN
     SELECT NVL(max(id_producto+1),1)
     INTO v_IdProducto
     FROM sifcos.t_sif_productos;

     SELECT count(*)
     INTO v_RegEnc
     FROM sifcos.t_sif_Productos p
     WHERE p.n_producto=upper(pN_Producto);

    BEGIN
   IF v_RegEnc=0 THEN
     INSERT INTO sifcos.t_sif_productos
     (ID_PRODUCTO
      ,N_PRODUCTO
      )
     VALUES
     (v_IdProducto
      ,pN_Producto
      );
     ELSE pResultado:='REGISTRO EXISTENTE';
          return;
     END IF;

      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar Producto: '||sqlerrm);

     END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Producto;

--- edicion: Fernando Budassi --- fecha: 03/2016 
--- utilizado en pagina ABMRoles.aspx.cs
PROCEDURE pr_Insert_Rol (
    pN_Rol IN sifcos.t_sif_Roles.n_rol%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS

   v_bandera  varchar2(1) := 'S' ;
   v_RegEnc number(2) :=0;
   v_IdRol sifcos.t_sif_roles.id_rol%TYPE;
   BEGIN
     SELECT NVL(max(id_rol+1),1)
     INTO v_IdRol
     FROM sifcos.t_sif_roles;

     SELECT count(*)
     INTO v_RegEnc
     FROM sifcos.t_sif_roles r
     WHERE r.n_rol=upper(pN_Rol);

    BEGIN
   IF v_RegEnc=0 THEN
     INSERT INTO sifcos.t_sif_roles
     (ID_ROL
      ,N_ROL
     )
     VALUES
     (v_IdRol
      ,pN_Rol
     );
     ELSE pResultado:='REGISTRO EXISTENTE';
          return;
     END IF;

      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar Producto: '||sqlerrm);

     END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Rol;

--- edicion: Fernando Budassi --- fecha: 10/2016 
--- utilizado para insertar nuevo usuario con su rol en pagina ABMRoles.aspx.cs
PROCEDURE pr_Insert_Rol_Usuario (
    pIdUsuario IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    pRolUsuario IN sifcos.t_sif_usuarios_cidi.id_rol%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_bandera  varchar2(1) := 'S' ;
   v_ExisteUsuario varchar2(5);
   --v_UsuarioDeBaja varchar2(5);

   BEGIN
     SELECT count(*)
     INTO v_ExisteUsuario
     FROM sifcos.t_sif_usuarios_cidi c
     WHERE c.id_usuario_cidi=pIdUsuario;
       
     BEGIN
    IF (v_ExisteUsuario=0) THEN
        INSERT INTO sifcos.t_sif_usuarios_cidi
        (ID_USUARIO_CIDI
         ,ID_ROL
         ,N_APLICACION
         )
         values
         (pIdUsuario
         ,pRolUsuario
         ,'SIFCOS'
        );
    ELSE 
      v_bandera :='N';
    END IF;


      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en insertar un nuevo Estado al Tramite: '||sqlerrm);

     END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='USUARIO YA TIENE ROL ASIGNADO';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Rol_Usuario;

--- edicion: Fernando Budassi --- fecha: 11/2016 
--- utilizado para asignar un organismo a un usuario en ABMUsuariosYOrganismos.aspx.cs
PROCEDURE pr_Insert_Org_Usuario (
    pIdUsuario IN sifcos.t_sif_usuarios_organismo.cuil_usr_cidi%TYPE DEFAULT NULL,
    pIdOrganismo IN sifcos.t_sif_usuarios_organismo.id_organismo%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_bandera  varchar2(1) := 'S' ;
   v_ExisteUsuario varchar2(5);
   v_UsuarioDeBaja varchar2(5);

   BEGIN
     SELECT count(*)
     INTO v_ExisteUsuario
     FROM sifcos.t_sif_usuarios_organismo uo
     WHERE uo.cuil_usr_cidi=pIdUsuario
       AND uo.fec_hasta is null;
    
     SELECT count(*)
     INTO v_UsuarioDeBaja
     FROM sifcos.t_sif_usuarios_organismo uo
     WHERE uo.cuil_usr_cidi=pIdUsuario
       AND uo.fec_hasta is not null;
       
     BEGIN
    IF v_ExisteUsuario=0 THEN
       IF v_UsuarioDeBaja=0 THEN
        INSERT INTO sifcos.t_sif_usuarios_organismo
        (cuil_usr_cidi
         ,id_organismo
         ,fec_desde
         )
         values
         (pIdUsuario
         ,pIdOrganismo
         ,sysdate
        );
        v_bandera:='I';
        ELSE
        UPDATE sifcos.t_sif_usuarios_organismo
        SET id_organismo = pIdOrganismo
            ,fec_desde = sysdate
            ,fec_hasta = null
        WHERE cuil_usr_cidi = pIdUsuario;
        v_bandera:='U';
        END IF; 
    ELSE
      v_bandera:='E';
    END IF;


      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en insertar un nuevo Estado al Tramite: '||sqlerrm);

     END;
BEGIN
 CASE v_bandera
   WHEN 'I' THEN
      commit;
      pResultado:='ORGANISMO ASIGNADO';
   WHEN 'U' THEN
      pResultado:='RELACION ACTUALIZADA';
      commit;
   WHEN 'E' THEN
      pResultado:='USUARIO YA EXISTE';
   WHEN 'N' THEN
      pResultado:='ERROR';
      ROLLBACK;
 END CASE;

END;
      
    return;

END pr_Insert_Org_Usuario;

--- edicion: Fernando Budassi --- fecha: 27/03/2017 
--- utilizado para asignar un producto a una actividad existente
PROCEDURE pr_Insert_prod_act (
    pIdProducto IN sifcos.t_sif_productos_actividad.id_producto%TYPE DEFAULT NULL,
    pIdActividad IN sifcos.t_sif_productos_actividad.id_actividad_clanae%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_bandera  varchar2(1) := 'S' ;
   v_ExisteRelacion varchar2(5);
  -- v_UsuarioDeBaja varchar2(5);
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);

   BEGIN
     V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;  
    
       
   
     SELECT count(*)
     INTO v_ExisteRelacion
     FROM sifcos.t_sif_productos_actividad pa
     WHERE pa.id_producto=pIdProducto
       AND pa.id_actividad_clanae=pIdActividad;
    
     BEGIN
    IF v_ExisteRelacion=0 THEN
          INSERT INTO sifcos.t_sif_productos_actividad
        (id_producto
         ,id_actividad_clanae
         ,FECHA_INICIO_ACT
         )
         values
         (pIdProducto
         ,pIdActividad
         ,'01/11/2013'
        );
        
    END IF; 
    
      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en insertar un nuevo Estado al Tramite: '||sqlerrm);

     END;
BEGIN
 if v_bandera = 'S' then
     pResultado:='OK';
      commit;
 else 
      ROLLBACK;
      pResultado:='YA EXISTE';
 END IF;

END;
      
    return;

END pr_Insert_Prod_act;

--- edicion: Fernando Budassi --- fecha: 17/04/2017 
--- utilizado para modificar los productos de un tramite
PROCEDURE pr_Insert_prod_tra (
    pId_Producto IN sifcos.t_sif_productos_tramite.id_producto%TYPE DEFAULT NULL,
    pNro_tramite IN sifcos.t_sif_productos_tramite.id_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_bandera  varchar2(1) := 'S' ;
   v_ExisteRelacion varchar2(5);
  -- v_UsuarioDeBaja varchar2(5);
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);

   BEGIN
     V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;  
    
       
   
     SELECT count(*)
     INTO v_ExisteRelacion
     FROM sifcos.t_sif_productos_tramite pt
     WHERE pt.id_producto=pId_Producto
       AND pt.id_tramite_sifcos=pNro_tramite;
    
     BEGIN
    IF v_ExisteRelacion=0 THEN
         INSERT INTO sifcos.t_sif_productos_tramite
        (id_tramite_sifcos
         ,id_producto
         ,CONFIRMADO
         )
         values
         (pNro_tramite
         ,pId_Producto
         ,'S'
        );
        
    END IF; 
    
      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en insertar los productos al tramite: '||sqlerrm);

     END;
BEGIN
 if v_bandera = 'S' then
     pResultado:='OK';
      commit;
 else 
      ROLLBACK;
      pResultado:='YA EXISTE';
 END IF;

END;
      
    return;

END pr_Insert_Prod_tra;

--- edicion: Fernando Budassi --- fecha: 06/2016 
--- utilizado en pagina Inscripcion.aspx.cs
PROCEDURE pr_Insert_Productos_Tramites (
    pId_Producto IN sifcos.t_sif_productos.id_producto%TYPE DEFAULT NULL,
    pNro_tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS

   v_bandera  varchar2(1) := 'S' ;
   v_Existe varchar2(5);
   BEGIN
     
     SELECT count(*)
     INTO v_Existe
     FROM sifcos.t_sif_productos_tramite t
     WHERE t.id_tramite_sifcos=pNro_tramite
       AND t.id_producto= pId_Producto;
       
      BEGIN
      IF v_Existe=1 THEN
        UPDATE sifcos.t_sif_productos_tramite t
        SET t.confirmado='S'
        WHERE t.id_tramite_sifcos=pNro_tramite
       AND t.id_producto= pId_Producto;
       
      ELSE
        INSERT INTO sifcos.t_sif_productos_tramite
        (id_tramite_sifcos
         ,id_producto
         ,CONFIRMADO
        )
        VALUES
        (pNro_tramite
         ,pId_Producto
         ,'S'
         );
      END IF;
      
      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar Productos por tramites: '||sqlerrm);

     END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Productos_Tramites;

/*PROCEDURE pr_Insert_Ofertas_Tramites (
    pId_Producto IN sifcos.t_sif_OFERTAS.id_producto%TYPE DEFAULT NULL,
    pNro_tramite IN sifcos.t_sif_tramites_sifcos_CA.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pPrecio IN sifcos.t_sif_ofertas_tramite.precio%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS

   v_bandera  varchar2(1) := 'S' ;
   
   BEGIN
     
     delete t_sif_ofertas_tramite ot
     WHERE ot.id_tramite_sifcos=pNro_tramite;
       
      BEGIN
        INSERT INTO sifcos.t_sif_productos
        (id_tramite_sifcos
         ,id_producto
         ,precio
        )
        VALUES
        (pNro_tramite
         ,pId_Producto
         ,pPrecio
         );
    
     
      
      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar ofertas por tramites: '||sqlerrm);

   END;


      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;

 
    return;
 
END pr_Insert_ofertas_Tramites;*/




--- edicion: Fernando Budassi --- fecha: 10/2016 --modifcado: 15/12/2016
--- utilizado en pagina Inscripcion.aspx.cs
PROCEDURE pr_Insert_Tasas_Tramite (
    pNro_transaccion IN sifcos.t_sif_tasas.nro_transaccion%TYPE DEFAULT NULL,
    pNro_tramite IN sifcos.t_sif_tasas.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pPagada IN sifcos.t_sif_tasas.pagada%TYPE DEFAULT NULL,
    pNroLiquidacionOriginal IN sifcos.t_sif_tasas.nroliquidacionoriginal%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS
   v_existe varchar2(2);
  -- v_existe2 varchar2(2);
   v_bandera  varchar2(1) := 'S' ;
    BEGIN
     SELECT count(*) 
     INTO v_existe
     FROM sifcos.t_sif_tramites_sifcos t
     WHERE t.nro_tramite_sifcos=pNro_tramite;
     
       
      BEGIN
     IF (v_existe>0)THEN
        INSERT INTO sifcos.t_sif_tasas
        (nro_tramite_sifcos
         ,nro_transaccion
         ,pagada
         ,fecha_alta
         ,nroliquidacionoriginal
        )
        VALUES
        (pNro_tramite
         ,pNro_transaccion
         ,pPagada
         ,sysdate
         ,pNroLiquidacionOriginal
         );
         pResultado:='SE INSERTO LA TASA';
       ELSE 
         pResultado:='NRO TRAMITE INEXISTENTE';
         
       END IF;

      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar Tasas por Tramite: '||sqlerrm);

     END;

      IF  v_bandera = 'S'   THEN
          COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Tasas_Tramite;

--- edicion: Fernando Budassi --- fecha: 08/2016
--- utlizado en pagina BocaRecepcion.aspx.cs 
PROCEDURE pr_Insert_Estado_Tramite (
    pNro_tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pDescripcion IN sifcos.t_sif_hist_estado.descripcion%TYPE DEFAULT NULL,
    pidEstado IN sifcos.t_sif_estados_tramite.id_estado_tramite%TYPE DEFAULT NULL,
    pCuilCidi IN sifcos.t_sif_hist_estado.cuil_usr_cidi%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_bandera  varchar2(1) := 'S' ;
  

   BEGIN
  
     BEGIN

     INSERT INTO sifcos.t_sif_hist_estado
     (fec_desde_estado
      ,descripcion
      ,id_estado_tramite
      ,id_tramite_sifcos
      ,cuil_usr_cidi
      
      )
      values
      (sysdate
      ,pDescripcion
      ,pidEstado
      ,pNro_tramite
      ,pCuilCidi
      );


      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en insertar un nuevo Estado al Tramite: '||sqlerrm);

     END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Estado_Tramite;

--- edicion: Fernando Budassi --- fecha: 09/2016
--- utlizado en pr_insert_inscripcion 
PROCEDURE pr_Insert_HistEstado_Tramite (
    pNro_tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pidEstado IN sifcos.t_sif_estados_tramite.id_estado_tramite%TYPE DEFAULT NULL,
    pCuilCidi IN sifcos.t_sif_hist_estado.cuil_usr_cidi%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_bandera  varchar2(1) := 'S' ;
   v_desc_hist_estado varchar2(50);

   BEGIN
     IF pidEstado = 1 THEN
    v_desc_hist_estado:= 'INSCRIPTO SIN VERIFICAR';
  ELSIF  pidEstado = 4 THEN
    v_desc_hist_estado:= 'REEMPADRONAMIENTO';
  ELSIF  pidEstado = 10 THEN
    v_desc_hist_estado:= 'BAJA DE TRAMITE';
  END IF;

     BEGIN

     INSERT INTO sifcos.t_sif_hist_estado
     (fec_desde_estado
      ,descripcion
      ,id_estado_tramite
      ,id_tramite_sifcos
      ,cuil_usr_cidi
      
      )
      values
      (sysdate
      ,v_desc_hist_estado
      ,pidEstado
      ,pNro_tramite
      ,pCuilCidi
      );


      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en insertar un nuevo Estado al Tramite: '||sqlerrm);

     END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_HistEstado_Tramite;



PROCEDURE pr_Insert_HistEstado_Descrip (
    pNro_tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pidEstado IN sifcos.t_sif_estados_tramite.id_estado_tramite%TYPE DEFAULT NULL,
    pCuilCidi IN sifcos.t_sif_hist_estado.cuil_usr_cidi%TYPE DEFAULT NULL,
    pDescripcion IN sifcos.t_sif_hist_estado.descripcion%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_bandera  varchar2(1) := 'S' ; 

   BEGIN
      

     BEGIN

     INSERT INTO sifcos.t_sif_hist_estado
     (fec_desde_estado
      ,descripcion
      ,id_estado_tramite
      ,id_tramite_sifcos
      ,cuil_usr_cidi
      
      )
      values
      (sysdate
      ,pDescripcion
      ,pidEstado
      ,pNro_tramite
      ,pCuilCidi
      );


      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en insertar un nuevo Estado al Tramite: '||sqlerrm);

     END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_HistEstado_Descrip;

-- edicion: (IB) --- fecha: 03/2018
--- 
PROCEDURE pr_Insert_Rubro (
    pN_RUBRO IN sifcos.T_SIF_RUBROS.N_RUBRO%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS

   v_bandera  varchar2(1) := 'S' ;
   v_RegEnc number(2) :=0;
   v_IdRubro sifcos.T_SIF_RUBROS.ID_RUBRO%TYPE;

   BEGIN
     SELECT NVL(max(ID_RUBRO)+1,1)
     INTO v_IdRubro
     FROM sifcos.T_SIF_RUBROS;

     SELECT count(*)
     INTO v_RegEnc
     FROM sifcos.T_SIF_RUBROS s
     WHERE s.N_RUBRO=upper(pN_Rubro);

    BEGIN
   IF v_RegEnc=0 THEN
     INSERT INTO sifcos.T_SIF_RUBROS
     (ID_RUBRO
      ,N_RUBRO
     )
     VALUES
     (v_IdRubro
      ,pN_Rubro
     );
     ELSE pResultado:='REGISTRO EXISTENTE';
          return;
     END IF;

      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error al insertar RUBRO: '||sqlerrm);
      END;
      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Rubro;



--- edicion: Fernando Budassi --- fecha: 05/2016 
--- procedimiento provisorio no se utilizara en produccion
PROCEDURE pr_Delete_Superficie (
    pId_Superficie IN sifcos.t_sif_tipos_superficie.id_tipo_superficie%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS

   v_bandera  varchar2(1) := 'S' ;


BEGIN


    BEGIN
    DELETE FROM sifcos.t_sif_tipos_superficie
    WHERE pId_Superficie=id_tipo_superficie;

    EXCEPTION
    WHEN OTHERS THEN
    v_bandera := 'N' ;
    RAISE_APPLICATION_ERROR(-20001,'Error en eliminar el registro: '||sqlerrm);
    END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;

END pr_Delete_Superficie;

--- edicion: Fernando Budassi --- fecha: 10/04/2017 
--- borrar relaciones de productos y actividades
PROCEDURE pr_Delete_Rel_Prod_Act (
    pId_Producto IN sifcos.t_sif_productos_actividad.id_producto%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS

   v_bandera  varchar2(1) := 'S' ;


BEGIN


    BEGIN
    DELETE FROM sifcos.t_sif_productos_actividad
    WHERE pId_Producto=id_producto;

    EXCEPTION
    WHEN OTHERS THEN
    v_bandera := 'N' ;
    RAISE_APPLICATION_ERROR(-20001,'Error en eliminar el registro: '||sqlerrm);
    END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;

END pr_Delete_Rel_Prod_Act;

--- edicion: Fernando Budassi --- fecha: 05/2016 
--- solo se usara el procedimiento para desarrollo
PROCEDURE pr_rollback_tramite (
    p_CUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    P_ENTIDAD IN sifcos.t_sif_entidades.id_entidad%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS

   v_bandera  varchar2(1) := 'S' ;
   --v_id_entidad varchar(10);

BEGIN
  
   DELETE FROM sifcos.t_sif_productos_tramite pt
    WHERE pt.id_tramite_sifcos in (select t.nro_tramite_sifcos from sifcos.t_sif_tramites_sifcos t
                                   join sifcos.t_sif_entidades e on t.id_entidad=e.id_entidad
                                   where e.cuit=p_CUIT
                                   and e.id_entidad <> P_ENTIDAD
                                   );

    DELETE FROM sifcos.t_sif_tramites_sifcos t
    WHERE t.nro_tramite_sifcos in (select t.nro_tramite_sifcos from sifcos.t_sif_tramites_sifcos t
                                   join sifcos.t_sif_entidades e on t.id_entidad=e.id_entidad
                                   where e.cuit=p_CUIT
                                   and e.id_entidad <> P_ENTIDAD
                                   );

    DELETE FROM sifcos.t_sif_superficies_empresa s
    WHERE s.id_tramite_sifcos in (select t.nro_tramite_sifcos from sifcos.t_sif_tramites_sifcos t
                                   join sifcos.t_sif_entidades e on t.id_entidad=e.id_entidad
                                   where e.cuit=p_CUIT
                                   and e.id_entidad <> P_ENTIDAD
                                   );

    DELETE FROM sifcos.t_sif_hist_estado h
    WHERE h.id_tramite_sifcos in (select t.nro_tramite_sifcos from sifcos.t_sif_tramites_sifcos t
                                   join sifcos.t_sif_entidades e on t.id_entidad=e.id_entidad
                                   where e.cuit=p_CUIT
                                   and e.id_entidad <> P_ENTIDAD
                                   );

    DELETE FROM sifcos.t_sif_entidades e
    WHERE e.id_entidad in (select e.id_entidad from sifcos.t_sif_tramites_sifcos t
                                   join sifcos.t_sif_entidades e on t.id_entidad=e.id_entidad
                                   where e.cuit=p_CUIT
                                   and e.id_entidad <> P_ENTIDAD
                                   );

    DELETE FROM sifcos.t_sif_tasas ta
    where ta.nro_tramite_sifcos in (select t.nro_tramite_sifcos from sifcos.t_sif_tramites_sifcos t
                                   join sifcos.t_sif_entidades e on t.id_entidad=e.id_entidad
                                   where e.cuit=p_CUIT
                                   and e.id_entidad <> P_ENTIDAD
                                   );                                   

    v_bandera := 'S' ;

    EXCEPTION
    WHEN OTHERS THEN
    v_bandera := 'N' ;
    RAISE_APPLICATION_ERROR(-20001,'Error en eliminar el registro: '||sqlerrm);
    

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;

END pr_rollback_tramite;

--- Creacion: (IB) --- fecha: 08/2018 
--- utilizado para asociar un rubro con una actividad en pagina BocaMinisterio.aspx.cs
PROCEDURE pr_Insert_Rubro_Actividad (
    pIdRubro IN NUMBER,
    pActividadClanae IN VARCHAR2,
    pResultado OUT varchar2
   )
   IS
   v_bandera  varchar2(1) := 'S' ;
   vExiste varchar2(5);
   --v_UsuarioDeBaja varchar2(5);

   BEGIN
     SELECT count(*)
     INTO vExiste
     FROM sifcos.t_sif_rubros_actividad c
     WHERE c.id_rubro=pIdRubro
	 and c.id_actividad_clanae=pActividadClanae;
       
     BEGIN
    IF (vExiste=0) THEN
        INSERT INTO sifcos.t_sif_rubros_actividad
        (id_rubro
         ,id_actividad_clanae
        ,fecha_inicio_act
         )
         values
         (pIdRubro
         ,pActividadClanae
         ,SYSDATE
        );
    ELSE 
      v_bandera :='N';
    END IF;


      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en insertar un nuevo RUBRO ACTIVIDAD: '||sqlerrm);

     END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='RUBRO YA TIENE LA ACTIVIDAD ASIGNADA';
           ROLLBACK;


     END IF ;


    return;

END pr_Insert_Rubro_Actividad;
--Creacion: IB ---Fecha 12/12/2018
PROCEDURE PR_SIF_HIST_CONCEPTOS_INSERT
(
	pIdSifHistConceptos IN VARCHAR2, 
	pNConcepto IN VARCHAR2, 
	pMonto IN NUMBER, 
	pFecDesde IN DATE, 
	pFecHasta IN DATE,
    pIdTipoConcepto IN NUMBER
) IS

BEGIN
   
INSERT INTO SIFCOS.T_SIF_HIST_CONCEPTOS
(
	ID_SIF_HIST_CONCEPTOS, 
	N_CONCEPTO, 
	MONTO, 
	FEC_DESDE, 
	FEC_HASTA,
    ID_TIPO_CONCEPTO
)   
VALUES 
(
				 pIdSifHistConceptos, 
				 pNConcepto, 
				 pMonto, 
				 pFecDesde, 
				 pFecHasta,
                pIdTipoConcepto
);
    

end PR_SIF_HIST_CONCEPTOS_INSERT;
--Creacion: IB ---Fecha 12/12/2018
PROCEDURE PR_SIF_HIST_CONCEPTOS_DELETE
(
		pIdSifHistConceptos IN VARCHAR2 
		
 ) IS
BEGIN

    DELETE FROM T_SIF_HIST_CONCEPTOS WHERE ID_SIF_HIST_CONCEPTOS= pIdSifHistConceptos;
end PR_SIF_HIST_CONCEPTOS_DELETE;

--Creacion: IB ---Fecha 12/12/2018
 PROCEDURE PR_SIF_TIPOS_CONCEPTOS_INSERT
(
    pIdTipoConcepto IN NUMBER,
    pNTipoConcepto IN VARCHAR2
) IS
BEGIN
INSERT INTO T_SIF_TIPOS_CONCEPTO
(ID_TIPO_CONCEPTO,N_TIPO_CONCEPTO)
VALUES
(pIdTipoConcepto, pNTipoConcepto);

END PR_SIF_TIPOS_CONCEPTOS_INSERT;

--Creacion: IB ---Fecha 12/12/2018
 PROCEDURE PR_PARAMETROS_GRAL_INSERT
(
    pIdParametroGeneral IN NUMBER,
    pValor IN VARCHAR2,
    pDescripcion IN VARCHAR2
) IS
BEGIN

INSERT INTO T_SIF_PARAMETROS_GRAL
(ID_PARAMETRO_GRAL,
VALOR,
DESCRIPCION)
VALUES
(pIdParametroGeneral, pValor,pDescripcion);

END PR_PARAMETROS_GRAL_INSERT;

--Creacion: IB ---Fecha 12/12/2018
 PROCEDURE PR_PARAMETROS_GRAL_DELETE
(
    pIdParametroGeneral IN NUMBER
) IS
BEGIN

DELETE FROM  T_SIF_PARAMETROS_GRAL
WHERE ID_PARAMETRO_GRAL = pIdParametroGeneral;
END PR_PARAMETROS_GRAL_DELETE;


PROCEDURE pr_Update_Entidad (
    pIdEntidad IN number,
   /* pLocal IN varchar2,
    pOficina IN varchar2,
    pStand IN varchar2,*/
    pCobertura IN varchar2,
    pSeguro IN varchar2,
    pPropietario IN varchar2,
    pLongitud IN varchar2,
    pLatitud IN varchar2,
   /* pNombreComercio IN varchar2,*/
    pResultado OUT varchar2
   )

   IS

   v_bandera  varchar2(1) := 'S' ;
 /*  v_Local varchar2(5) := pLocal;
   v_Oficina varchar2(5) := pOficina;
   v_Stand varchar2(5) := pStand;*/
   v_Cobertura varchar2(5) := pCobertura;
   v_Seguro varchar2(5) := pSeguro;
   v_Longitud varchar2(50) := pLongitud;
   v_Latitud varchar2(50) := pLatitud;
   v_Propietario varchar2(2) := pPropietario;
   v_SQL varchar2(5000);



   BEGIN


    BEGIN
   v_SQL:= 'UPDATE sifcos.t_sif_entidades e 
            SET e.id_sede = ''00''
               ,e.resenia = '''' ';

   -- parametro cobertura_medica
   IF v_Cobertura is not null THEN
     v_SQL:= v_SQL || ',e.cobertura_medica = '''|| v_Cobertura||'''';
   END IF;
   -- parametro seguro_local
   IF v_Seguro is not null THEN
     v_SQL:= v_SQL || ',e.seguro_local = '''|| v_Seguro||'''';
  END IF;
/*   -- parametro local
   IF v_Local is not null THEN
     v_SQL:= v_SQL || ',e.local = '''|| v_Local||'''';
   END IF;
   -- parametro oficinal
   IF v_Oficina is not null THEN
     v_SQL:= v_SQL || ',e.oficina = '''|| v_Oficina||'''';
   END IF;
   -- parametro stand
   IF v_Stand is not null THEN
     v_SQL:= v_SQL || ' ,e.stand = '''|| v_Stand||'''';
   END IF;*/
   -- parametro propietario
   IF v_Propietario is not null THEN
     v_SQL:= v_SQL || ' ,e.propietario = '''|| v_Propietario||'''';
   END IF;
   --parametro longitud ubicacion
   IF v_Longitud is not null THEN
     v_SQL:= v_SQL || ' ,e.longitud_ubi = '''||v_Longitud||'''';
   END IF;
   --parametro latitud ubicacion
   IF v_Latitud is not null THEN
     v_SQL:= v_SQL || ' ,e.latitud_ubi = '''||v_Latitud||'''';
   END IF;
   --parametro nombre comrecio 
/*   IF pNombreComercio is not null THEN
     v_SQL:= v_SQL || ' ,e.observacion = '''||pNombreComercio||'''';
   END IF;*/
   
   -- completamos UPDATE
   v_SQL:= v_SQL || ' WHERE e.id_entidad = '||pIdEntidad;

EXECUTE IMMEDIATE v_SQL;




      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en actualizar la entidad: '||sqlerrm);

     END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;

      ELSE
           pResultado:='ERROR';
           ROLLBACK;


     END IF ;


    return;

END pr_Update_Entidad;

--- edicion: Fernando Budassi --- fecha: 10/2016 
--- utilizado para insertar nuevo usuario con su rol en pagina ABMRoles.aspx.cs
PROCEDURE pr_Insertar_Relacion (
    pCuitEmpresa IN varchar2,
    pCuilResp IN varchar2,
    pIdRol IN varchar2,
    pResultado OUT varchar2
   )
   IS
   v_bandera  varchar2(1) := 'S' ;
   v_ExisteRelacion number(1):=0;
   

   BEGIN
     SELECT count(*)
     INTO v_ExisteRelacion
     FROM sifcos.t_sif_usuarios_cidi c
     WHERE c.id_usuario_cidi=pCuilResp and c.permiso=pCuitEmpresa and c.id_rol=pIdRol;
       
     BEGIN
    IF (v_ExisteRelacion=0) THEN
        INSERT INTO sifcos.t_sif_usuarios_cidi
        (ID_USUARIO_CIDI
         ,ID_ROL
         ,N_APLICACION
         ,PERMISO
         ,FEC_ULT_ACCESO
         )
         values
         (pCuilResp
         ,pIdRol
         ,'RELACION'
         ,pCuitEmpresa
         ,SYSDATE
        );
    ELSE 
      
      v_bandera :='E';
    END IF;


      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en insertar la relacion: '||sqlerrm);
           

     END;

      IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;
      END IF ;           

      IF   v_bandera = 'N'   THEN
           pResultado:= 'ERROR: '||sqlerrm;
           ROLLBACK;

      END IF;
     
     
     IF  v_bandera = 'E'   THEN
           pResultado:='YA EXISTE RELACION';
           ROLLBACK;
     END IF;           


    return;

END pr_Insertar_Relacion;

PROCEDURE pr_Eliminar_Relacion (
    pCuitEmpresa IN varchar2,
    pCuilResp IN varchar2,
    pResultado OUT varchar2
   )
   IS
   v_bandera  varchar2(1) := 'S' ;
   v_ExisteUsuario number(1):=0;
   

   BEGIN
     SELECT count(*)
     INTO v_ExisteUsuario
     FROM sifcos.t_sif_usuarios_cidi c
     WHERE c.id_usuario_cidi=pCuilResp and c.permiso=pCuitEmpresa and c.id_rol=15;
       
     BEGIN
    IF (v_ExisteUsuario>0) THEN
        DELETE FROM sifcos.t_sif_usuarios_cidi c
        WHERE c.id_usuario_cidi=pCuilResp and c.permiso=pCuitEmpresa and c.id_rol=15;
    ELSE 
      v_bandera :='E';
    END IF;


      EXCEPTION
         WHEN OTHERS THEN
           v_bandera := 'N' ;
           RAISE_APPLICATION_ERROR(-20005,'Error en insertar la relaci?n: '||sqlerrm);

     END;

       IF  v_bandera = 'S'   THEN
           pResultado:='OK';
           COMMIT;
      END IF ;           

      IF   v_bandera = 'N'   THEN
           pResultado:= 'ERROR: '||sqlerrm;
           ROLLBACK;

      END IF;
     
     
     IF  v_bandera = 'E'   THEN
           pResultado:='NO EXISTE RELACION';
           ROLLBACK;
     END IF;           



    return;

END pr_Eliminar_Relacion;

END PCK_SIFCOS_INSERCIONES;
/
