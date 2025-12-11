CREATE OR REPLACE PACKAGE "PCK_SIFCOS_CONSULTA" AS

 PROCEDURE pr_get_empresa (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_existe_en_sifcos (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pExiste OUT number
   );
   PROCEDURE pr_get_TramitesDeBaja (
    pCUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pNroSifcos  IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
    );
   PROCEDURE pr_get_esta_reempadronado (
    pNrosifcos  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pExiste OUT varchar2,
    pVencido OUT varchar2
   );
   PROCEDURE pr_get_entidades_deuda (
    PNroTramite  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   
   PROCEDURE pr_get_trs_vencidas (
    pNroTramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_trs_vigentes (
    pNroTramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_AniosDeuda_TRS (
    pCuit IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pNro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_InfoBoca (
    pCuilCidi  IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_tasasAsignadas (
    pNroTramite  IN sifcos.t_sif_tasas.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Personas_RCivil (
    pDNI  IN rcivil.vt_pk_persona.NRO_DOCUMENTO%TYPE DEFAULT NULL,
    pSEXO IN rcivil.vt_pk_persona.id_sexo%TYPE DEFAULT NULL,
    pResultado     OUT sys_refcursor
   );
   PROCEDURE pr_get_Personas_RCivil2 (
    pDNI  IN rcivil.vt_pk_persona.NRO_DOCUMENTO%TYPE DEFAULT NULL,
    pSEXO IN rcivil.vt_pk_persona.id_sexo%TYPE DEFAULT NULL,
    pResultado     OUT sys_refcursor
   );
   PROCEDURE pr_get_Personas_RCivil3 (
    pNombre  IN rcivil.vt_pk_persona.NOV_NOMBRE%TYPE DEFAULT NULL,
    pApellido IN rcivil.vt_pk_persona.NOV_APELLIDO%TYPE DEFAULT NULL,
    pResultado     OUT sys_refcursor
   );
   PROCEDURE pr_get_Personas_RCivil_CUIL (
    pCUIL  IN rcivil.vt_pk_persona_cuil.NRO_OTRO_DOCUMENTO%TYPE DEFAULT NULL,
    pResultado     OUT sys_refcursor
   );

   PROCEDURE pr_get_Dom_Empresa (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Dom_Empresa_by_idvin (
    pidvin IN dom_manager.vt_domicilios_todo_mzna_lote.ID_VIN%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Com_Empresa (
    pNroTramite  IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_productos_tramite (
    pNroTramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
    PROCEDURE pr_get_misTramites (
    P_Nro_Tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    P_CUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    p_cuilUsuarioCidi IN sifcos.t_Sif_Tramites_Sifcos.cuil_usuario_cidi%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_EntidadTramite (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_tramites_baja (
    P_Nro_Tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    P_Nro_Sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    P_CUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    P_RAZON_SOCIAL IN t_comunes.vt_pers_juridicas.razon_social%TYPE DEFAULT NULL,
    P_ORDEN_CONSULTA IN varchar2,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_tramites_boca (
    P_Nro_Tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    P_Nro_Sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    P_CUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    P_RAZON_SOCIAL IN t_comunes.vt_pers_juridicas.razon_social%TYPE DEFAULT NULL,
    P_ORDEN_CONSULTA IN varchar2,
    P_IDESTADO IN varchar2,
    p_FECHADESDE IN varchar2,
    p_FECHAHASTA IN varchar2,
    pResultado OUT sys_refcursor
   );
  PROCEDURE pr_get_tramites_boca_recepcion (
  P_Nro_Tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
  P_Nro_Sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
  P_CUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
  P_RAZON_SOCIAL IN t_comunes.vt_pers_juridicas.razon_social%TYPE DEFAULT NULL,
  P_ORDEN_CONSULTA IN varchar2,
  P_IDESTADO IN varchar2,
  p_FECHADESDE IN varchar2,
  p_FECHAHASTA IN varchar2,
  p_id_organismo IN varchar2,
  pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_info_gral_tramite (
  P_Nro_Tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
  pResultado OUT sys_refcursor
   );
   PROCEDURE pr_Consulta_Tramite (
    p_nroTramite  IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Entidad_Viejo_Sifcos (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Entidad_Nuevo_Sifcos (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Roles (
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Organismos (
    pPrefijo in varchar2,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Organismos_Filtrados (
    pOrganismos in varchar2,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Organismos_one (
    pIdOrganismo in varchar2,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Rol_Usuario (
    p_UsuarioCidi IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_get_Relaciones_Usuario (
    p_UsuarioCidi IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Org_Usuario (
    p_UsuarioCidi IN sifcos.t_sif_usuarios_organismo.cuil_usr_cidi%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );
   PROCEDURE pr_get_Roles_usuarios (
    p_rol IN sifcos.t_sif_Roles.n_rol%TYPE DEFAULT NULL,
    p_cuil IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    P_ORDEN_CONSULTA IN varchar2,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Organismos_usuarios (
    p_Organismo IN VARCHAR2,
    p_cuil IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    P_ORDEN_CONSULTA IN varchar2,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Superficies (
    pPrefijo IN sifcos.t_sif_tipos_superficie.n_tipo_superficie%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_SuperficiesByIdTramite (
    pNroTramite IN sifcos.t_sif_superficies_empresa.id_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   
   PROCEDURE pr_get_Productos (
    pPrefijo IN sifcos.t_sif_productos.n_producto%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
    PROCEDURE pr_get_Productos_sin_asignar (
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_actividades (
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_ProductosBETA (
    pPrefijo IN sifcos.t_sif_productos.n_producto%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
     PROCEDURE pr_get_ProductosBETA2 (
    pPrefijo IN sifcos.t_ca_productos.n_producto%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Productos_Actividad (
    pIDs IN VARCHAR2,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Productos_y_Act (
      pProd IN varchar2,
      pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Productos_y_Act_2 (
      pAct IN varchar2,
      pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_estados_br (
     pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_estados_bm (
     pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_HistEstados (
     pNroTramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%Type default null,
     pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Tipo_Cargo (
     pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Tipo_Gestor (
     pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_IDProducto (
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_IDSuperficie (
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_provincias (
    pProvincias  OUT   sys_refcursor
   );
  PROCEDURE pr_get_departamentos (
    pIdProvincia IN dom_manager.dom_provincias.id_provincia%TYPE DEFAULT NULL,
    pDepartamentos  OUT   sys_refcursor
   );
  PROCEDURE pr_get_localidades (
    pIdDepto IN dom_manager.dom_departamentos.id_departamento%TYPE DEFAULT NULL,
    pLocalidades  OUT   sys_refcursor
   );
  PROCEDURE pr_get_barrios (
    pIdLocalidad IN dom_manager.dom_localidades.id_localidad%TYPE DEFAULT NULL,
    pBarrios  OUT   sys_refcursor
   );
   PROCEDURE pr_get_calles (
    pIdProvincia IN dom_manager.dom_provincias.id_provincia%TYPE DEFAULT NULL,
    pIdDepto IN dom_manager.dom_departamentos.id_departamento%TYPE DEFAULT NULL,
    pIdLocalidad IN dom_manager.dom_localidades.id_localidad%TYPE DEFAULT NULL,
    pPrefijo IN VARCHAR2,
    pCalles  OUT   sys_refcursor
   );
   PROCEDURE pr_get_sedes (
    pCuit IN t_comunes.t_sedes_persjuridica.cuit%TYPE DEFAULT NULL,
    pSedes  OUT   sys_refcursor
   );
   /*PROCEDURE pr_get_id_transaccion (
    pNroTramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado  OUT   varchar2
   );*/
  /* PROCEDURE pr_get_pago_TRS (
    pId_transaccionTasa IN sifcos.t_sif_tramites_sifcos.idtrasacciontasa%TYPE DEFAULT NULL,
    pResultado  OUT varchar2
   );*/
   /*PROCEDURE pr_get_entidades_deuda (
    PNroTramite  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   );*/
   PROCEDURE pr_get_Codigo_postal (
    pIdDepartamento  IN dom_manager.dom_departamentos.id_departamento%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_ultima_sede (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_Get_TasasSinUsar (
    Pcuit in varchar2,
    pResultado  OUT   sys_refcursor
   );
   PROCEDURE pr_Get_TasasSinUsarAlta (
    Pcuit in varchar2,
    pResultado  OUT   sys_refcursor
   );
   PROCEDURE pr_Get_Tasas_by_cuit (
    Pcuit in varchar2,
    pResultado  OUT   sys_refcursor
   );
   PROCEDURE pr_get_tasas_Viejo_Sif_by_ref (
    PNroReferencia in varchar2,
    pResultado  OUT   sys_refcursor
   );
   PROCEDURE pr_get_tasas_Nuevo_Sif_by_ref (
    PNroReferencia in varchar2,
    pResultado  OUT   sys_refcursor
   );
   PROCEDURE pr_get_duplicados (
    P_FECHA_DESDE IN varchar2,
    P_FECHA_HASTA IN varchar2,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_tramite_existe (
   pId_vin_dom_legal IN varchar2,
   pId_vin_dom_local IN varchar2,
   p_fecha_alta IN varchar2,
   pResultado OUT sys_refcursor
   );
    PROCEDURE pr_get_tramite_existe_comercio (
   pId_vin_dom_local IN varchar2,
   p_fecha_alta IN varchar2,
   pResultado OUT sys_refcursor
   );
   
   
   PROCEDURE pr_get_ultimo_tramite (
    pNroSifcos  IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_fec_ult_tramite (
    pNroSifcos  IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_fec_ult_tramite_viejo (
    pNroSifcos  IN sifcos.t_tramites.nro_recsep%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_direcciones_sedes (
    P_Cuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    P_Nro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_tram_sifcos (
    P_Cuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    P_Nro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_ubicacion_mapa (
    pNroTramite  IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Gestor (
    pIdGestor  IN sifcos.t_sif_gestores.id_gestor%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   );
    PROCEDURE pr_get_Rubros (
    pPrefijo IN varchar2,
    pResultado OUT sys_refcursor
   ); 
   PROCEDURE pr_get_Rubros2 (
    
    pResultado OUT sys_refcursor
   ); 
   procedure pr_get_RubrosActividad (
    pResultado OUT sys_refcursor
    );
    PROCEDURE pr_get_EmailYaUtilizado (
	  pCuit IN VARCHAR2,
	  pNroMail IN VARCHAR2,
    pResultado OUT sys_refcursor
   );
   PROCEDURE pr_consulta_comercios_sifcos (
    P_RESULTADO OUT sys_refcursor
   );
PROCEDURE pr_get_conceptos_TRS (
       PCursor     OUT sys_refcursor
   ); 

PROCEDURE pr_get_conceptosAFecha (
        pFecha IN DATE,
       PCursor     OUT sys_refcursor
   );
   
PROCEDURE pr_get_Comercios_Vto (
        pCuit IN varchar2,
       pResultado OUT sys_refcursor
   );   
PROCEDURE pr_get_IdsEntidad (
    p_IdLocalidad varchar2 DEFAULT NULL,
    p_IdRubro varchar2 DEFAULT NULL,
    pResultado OUT sys_refcursor
   );   
   
PROCEDURE pr_get_IDDocumentoCDD (
    pNroTramite in varchar2,
    pResultado OUT sys_refcursor
   );
PROCEDURE pr_get_tramites_boca_ubi (
    p_idDepartamento IN varchar2,
    P_idLocalidad IN varchar2,
    pResultado OUT sys_refcursor
   );
PROCEDURE pr_CantComerciosByLocalidad (
    pIdDepartamento in varchar2,
    pResultado OUT sys_refcursor
   );   
   
   
END PCK_SIFCOS_CONSULTA;
/
CREATE OR REPLACE PACKAGE BODY "PCK_SIFCOS_CONSULTA" AS
--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina inscripcion.aspx.cs
PROCEDURE pr_get_empresa (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   
   BEGIN
      OPEN pResultado FOR
         SELECT  DISTINCT pj.cuit,
                 pj.id_sede,
                 pj.n_sede,
                 pj.razon_social,
                 e.observacion nombre_fantasia,
                 pj.nro_ingbruto,
                 pj.nro_hab_municipal
             FROM t_comunes.vt_pers_juridicas_completa pj
             LEFT JOIN sifcos.t_sif_entidades e on pj.cuit=e.cuit and pj.id_sede=e.id_sede
             --JOIN sifcos.t_sif_tramites_sifcos t on e.id_entidad=t.id_entidad
             WHERE pj.cuit=pCuit;
               --AND pj.nro_ingbruto is not null;

END pr_get_empresa;

--- edicion: Fernando Budassi --- fecha: 10/2016
--- utilizado en pagina inscripcion.aspx.cs para saber si se ha registrado antes 
--- o si la inscripcion esta vencida
PROCEDURE pr_get_existe_en_sifcos (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pExiste OUT number -- 1: existe -- 2: no existe
    )
   IS
   BEGIN
     /* 0: no hay tramites para ese cuit. 
       >0 : tiene cargado al menos 1 tramite de ALTA.*/
      SELECT  count(*)
      INTO pExiste
      FROM sifcos.t_sif_tramites_sifcos t 
      join  sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad
      WHERE e.cuit=pCuit
      and t.id_tipo_tramite=1
      and to_date(sysdate,'dd/mm/yyyy')>to_date(t.fec_alta+15,'dd/mm/yyyy');--ALTA
      
      if(pExiste=0)THEN
         pExiste:=1;-- ES INSCRIPCION
      else
        pExiste:=2; -- ES REEMPADRONAMIENTO
     end if;
      
     return;

END pr_get_existe_en_sifcos;

PROCEDURE pr_get_TramitesDeBaja (
    pCUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pNroSifcos  IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
    )
   IS
   BEGIN
     OPEN pResultado FOR
      SELECT  t.nro_tramite_sifcos nro_tramite
      FROM sifcos.t_sif_tramites_sifcos t 
      JOIN  sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad
      WHERE e.nro_sifcos=pNroSifcos and e.cuit=pCUIT
            and t.id_tipo_tramite=2
      UNION
      SELECT tr.id_tramite nro_tramite 
      FROM sifcos.t_tramites tr
      JOIN sifcos.t_entidades en on en.id_entidad=tr.id_entidad
      WHERE tr.id_tipo_tramite=2 and en.cuit_pers_juridica=pCUIT
            and tr.nro_recsep=pNroSifcos
      ;
      

END pr_get_TramitesDeBaja;

PROCEDURE pr_get_esta_reempadronado (
    pNroSifcos  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pExiste OUT varchar2,
    pVencido OUT varchar2
   )
   IS
   BEGIN
      SELECT  count(*)
      INTO pExiste
      FROM sifcos.t_sif_tramites_sifcos t 
      join  sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad
      WHERE e.nro_sifcos=pNroSifcos
      and e.nro_sifcos<>0
      and t.id_tipo_tramite=4;
      
      SELECT  count(*)
      INTO pVencido
      FROM sifcos.t_sif_tramites_sifcos t 
      join  sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad
      WHERE e.nro_sifcos=pNroSifcos
      and t.id_tipo_tramite=4
      and e.nro_sifcos=0
      and to_date(t.fec_alta,'dd/mm/yyyy')<=to_date(sysdate-15,'dd/mm/yyyy');
      if(pExiste=0)THEN
         pExiste:='No Existe';
      else
        pExiste:='Existe';
     end if;
      if(pVencido=0)THEN
         pVencido:='No Vencido';
      else
        pVencido:='Vencido';
     end if;
     return;

END pr_get_esta_reempadronado;

--IB 12/2018 ADAPTADO AL ESQUEMA TRS
   PROCEDURE pr_get_entidades_deuda (
    PNroTramite  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   IS
   v_adeuda varchar2(10);
   BEGIN
     SELECT COUNT(*) 
         INTO v_adeuda       
         from sifcos.t_sif_tasas ta 
         join (  select obligacion, nroliquidacionoriginal , fecha_vencimiento, fecha_emision,fecha_cobro, CUIT, codigo, pagado, HASH_TRX, ID_TRANSACCION, ID_CONCEPTO  FROM TRS.VT_TRANSACCIONES_VERTICALES 
                        union 
                        select  obligacion, nroliquidacionoriginal , fecha_vencimiento, fecha_emision,fecha_cobro, CUIT, codigo, pagado, HASH_TRX, ID_TRANSACCION, ID_CONCEPTO     FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) v  on ta.nro_transaccion = v.obligacion
         where ta.nro_tramite_sifcos = pNroTramite
         and to_date(sysdate, 'dd/mm/yyyy') > to_date(v.fecha_vencimiento, 'dd/mm/yyyy')
         and ta.pagada='N';
      
    IF v_adeuda = '0' THEN
       pResultado:= 'NO DEBE';
    ELSE pResultado:= 'ADEUDA';
    END IF;
      
    return;

END pr_get_entidades_deuda;

--- edicion: Fernando Budassi --- fecha: 07/12/2016
--- utilizado en pagina MisTramites.aspx.cs
--IB 12/2018 ADAPTADO AL ESQUEMA TRS. SIN USO DETECTADO
--- Retorna tasas vencidas para un tramite sin tasas disponibles
PROCEDURE pr_get_trs_vencidas (
    pNroTramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   v_tipo_tramite number(1);
   v_id_concepto varchar2(10);
   BEGIN
     SELECT t.id_tipo_tramite
     into v_tipo_tramite
     from sifcos.t_sif_tramites_sifcos t
     where t.nro_tramite_sifcos=pNroTramite;
      IF (v_tipo_tramite=1)THEN
        v_id_concepto:='76010000';
      ELSE v_id_concepto:='76020000';
      END IF;
      OPEN pResultado FOR
         SELECT ta.nro_tramite_sifcos,
                ta.nro_transaccion nro_trs,
                v.fecha_emision,
                (SELECT distinct c.n_concepto
                 FROM TRS.VT_CONCEPTOS_VERTICALES c
                 where c.id_concepto =  v_id_concepto
                 and c.fecha_desde=to_date('1/1/2016','dd/mm/yyyy')) concepto,
                 (SELECT distinct c.precio_base
                  FROM TRS.VT_CONCEPTOS_VERTICALES c
                  where c.id_concepto = v_id_concepto
                  and c.fecha_desde=to_date('1/1/2016','dd/mm/yyyy')) importe,
                  (SELECT distinct sifcos.fn_fecha_vto(v.fecha_emision,15)
                   FROM dual) vencimiento 
         from sifcos.t_sif_tasas ta 
         join TRS.VT_TRANSACCIONES_VERTICALES v on ta.nro_transaccion = v.obligacion
         where ta.nro_tramite_sifcos = pNroTramite
         and to_date(sysdate, 'dd/mm/yyyy') > to_date(v.fecha_vencimiento, 'dd/mm/yyyy')
  UNION       
    SELECT ta.nro_tramite_sifcos,
                ta.nro_transaccion nro_trs,
                v.fecha_emision,
                (SELECT distinct c.n_concepto
                 FROM TRS.VT_CONCEPTOS_VERTICALES c
                 where c.id_concepto =  v_id_concepto
                 and c.fecha_desde=to_date('1/1/2016','dd/mm/yyyy')) concepto,
                 (SELECT distinct c.precio_base
                  FROM TRS.VT_CONCEPTOS_VERTICALES c
                  where c.id_concepto = v_id_concepto
                  and c.fecha_desde=to_date('1/1/2016','dd/mm/yyyy')) importe,
                  (SELECT distinct sifcos.fn_fecha_vto(v.fecha_emision,15)
                   FROM dual) vencimiento 
         from sifcos.t_sif_tasas ta 
         join TRS.VT_TRANSACCIONES_VERTICALES v on ta.nro_transaccion = v.obligacion
         where ta.nro_tramite_sifcos = pNroTramite
         and to_date(sysdate, 'dd/mm/yyyy') <= to_date(v.fecha_vencimiento, 'dd/mm/yyyy');    

END pr_get_trs_vencidas;

--- edicion: Fernando Budassi --- fecha: 03/11/2025
--retorna todas las tasas asignadas al tramite

PROCEDURE pr_get_trs_vigentes (
    pNroTramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
  
   BEGIN
    
      OPEN pResultado FOR
         SELECT 
        ta.nro_tramite_sifcos,
        v.nroliquidacionoriginal nro_trs,
        v.fecha_emision,
        DECODE(V.PAGADO,'S','SI','N','NO') pagado,
        v.fecha_cobro,
        V.n_concepto CONCEPTO,
        V.importe_total IMPORTE,
        v.fecha_emision+15 vencimiento 
FROM sifcos.t_sif_tasas ta 
JOIN  TRS.VT_TRANSACCIONES_VERTICALES v on ta.nro_transaccion = v.obligacion
WHERE ta.nro_tramite_sifcos = pNroTramite  
--and v.pagado = 'S'
         ; 
         
END pr_get_trs_vigentes;


--- edicion: Fernando Budassi --- fecha: 10/2016
--- utilizado en pagina inscripcion.aspx.cs
PROCEDURE pr_get_AniosDeuda_TRS (
    pCuit IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pNro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT   e.cuit,  
                  e.nro_sifcos,  
                  DECODE (t.id_tipo_tramite, 1, 'ALTA', 4, 'REEMPADRONAMIENTO') as Tipo_tramite   ,  
                  est.n_estado_tramite as estado,  
                  max(his.fec_desde_estado) as fecha_ultima_autorizacion ,  
                  (trunc((trunc(SYSDATE) - trunc(to_date(his.fec_desde_estado)))/365)) as anios_deuda,
                  (SELECT distinct c.n_concepto
                 FROM tasas_servicio.vt_conceptos_micm c
                 where c.id_concepto = '76020000' 
                 and c.fecha_desde=to_date('1/1/2016','dd/mm/yyyy')) concepto,
                 (SELECT distinct c.precio_base
                  FROM tasas_servicio.vt_conceptos_micm c
                  where c.id_concepto = '76020000'
                  and c.fecha_desde=to_date('1/1/2016','dd/mm/yyyy')) importe   
         FROM sifcos.t_sif_tramites_sifcos t  
         JOIN sifcos.t_sif_entidades e  on e.id_entidad = t.id_entidad  
         JOIN SIFCOS.t_Sif_Hist_Estado his  on  his.id_tramite_sifcos = t.nro_tramite_sifcos  
         JOIN sifcos.t_sif_estados_tramite est   on his.id_estado_tramite = est.id_estado_tramite  
         WHERE      t.id_tipo_tramite in (1,4)  
         GROUP BY   e.cuit,         
                    e.nro_sifcos,         
                    DECODE (t.id_tipo_tramite, 1, 'ALTA', 4, 'REEMPADRONAMIENTO') ,         
                    est.n_estado_tramite ,         
                    (trunc((trunc(SYSDATE) - trunc(to_date(his.fec_desde_estado)))/365))  
         HAVING  e.cuit = pCuit  
             AND e.nro_sifcos = pNro_sifcos;
         

END pr_get_AniosDeuda_TRS;
--- edicion: Fernando Budassi --- fecha: 08/2016
--- utilizado en pagina bocaRecepcion.aspx.cs
PROCEDURE pr_get_InfoBoca (
    pCuilCidi  IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
                 SELECT distinct
                 o.id_organismo,
                 pj.razon_social boca_recepcion,
                 dom.localidad ||' - '||dom.departamento||' - '||dom.provincia localidad,
                (SELECT  distinct pj.razon_social
                 FROM sifcos.t_sif_organismos o1 
                 JOIN t_comunes.t_sedes_persjuridica spj ON spj.cuit=o1.cuit and spj.id_sede=o1.id_sede
                 JOIN t_comunes.vt_pers_juridicas_completa pj ON pj.cuit=spj.cuit             
                 WHERE o1.id_organismo=o.id_organismo_superior) dependencia
         FROM sifcos.t_sif_usuarios_cidi uc
         JOIN sifcos.t_sif_usuarios_organismo uo ON uc.id_usuario_cidi=uo.cuil_usr_cidi 
         JOIN sifcos.t_sif_organismos o ON o.id_organismo=uo.id_organismo 
         JOIN t_comunes.vt_pers_juridicas_completa pj ON pj.cuit=o.cuit and pj.id_sede=o.id_sede
         LEFT JOIN t_comunes.vt_domsolo_persjur dom ON dom.cuit=pj.cuit and dom.id_sede=pj.id_sede
         WHERE uo.cuil_usr_cidi=pCuilCidi;
         ---edicio : Facundo Alvarez --- fecha 28/12/2017. Nota: Agrego LEFT JOIN por si no tiene cargado en la vista vt_domsolo_persjur EL DOMICILIO. As? aparece igual el registro.
             

END pr_get_InfoBoca;

--- edicion: Fernando Budassi --- fecha: 03/03/2017
--- utilizado en pagina bocaMinisterioaspx.cs - se consultan las tasas asignadas a un tramite

PROCEDURE pr_get_tasasAsignadas (
    pNroTramite  IN sifcos.t_sif_tasas.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
          SELECT m.nroliquidacionoriginal nro_liquidacion,
                 ta.nro_transaccion,
                 m.pagado,
                 ta.fecha_alta 
          FROM sifcos.t_Sif_Tasas ta
          join TRS.VT_TRANSACCIONES_VERTICALES m on m.id_transaccion=ta.nro_transaccion
          WHERE ta.nro_tramite_sifcos=pNroTramite;
         
             

END pr_get_tasasAsignadas;
--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina misTramites.aspx.cs
PROCEDURE pr_get_Personas_RCivil (
    pDNI  IN rcivil.vt_pk_persona.NRO_DOCUMENTO%TYPE DEFAULT NULL,
    pSEXO IN rcivil.vt_pk_persona.id_sexo%TYPE DEFAULT NULL,
    pResultado     OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT  
         p.ID_NUMERO,
         p.NOV_NOMBRE nombre,
         p.NOV_APELLIDO apellido,
         s.TIPO_SEXO sexo
             FROM rcivil.vt_pk_persona p
             JOIN rcivil.vt_sexos s ON p.ID_SEXO=s.ID_SEXO
             WHERE p.NRO_DOCUMENTO=pDNI
               AND p.ID_SEXO=pSexo;
               

END pr_get_Personas_RCivil;

--- edicion: Fernando Budassi --- fecha: 23/02/2017

PROCEDURE pr_get_Personas_RCivil2 (
    pDNI  IN rcivil.vt_pk_persona.NRO_DOCUMENTO%TYPE DEFAULT NULL,
    pSEXO IN rcivil.vt_pk_persona.id_sexo%TYPE DEFAULT NULL,
    pResultado     OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT  
         *
             FROM rcivil.vt_pk_persona p
             JOIN rcivil.vt_sexos s ON p.ID_SEXO=s.ID_SEXO
             WHERE p.NRO_DOCUMENTO=pDNI
               AND p.ID_SEXO=pSexo;
               

END pr_get_Personas_RCivil2;

PROCEDURE pr_get_Personas_RCivil3 (
    pNombre  IN rcivil.vt_pk_persona.NOV_NOMBRE%TYPE DEFAULT NULL,
    pApellido IN rcivil.vt_pk_persona.NOV_APELLIDO%TYPE DEFAULT NULL,
    pResultado     OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT  
         *
             FROM rcivil.vt_pk_persona p
             WHERE p.NOV_NOMBRE=pNombre
               AND p.NOV_APELLIDO=pApellido;
               

END pr_get_Personas_RCivil3;


--- edicion: Fernando Budassi --- fecha: 09/2016
--- utilizado en pagina inscripcion.aspx.cs
PROCEDURE pr_get_Personas_RCivil_CUIL (
    pCUIL  IN rcivil.vt_pk_persona_cuil.NRO_OTRO_DOCUMENTO%TYPE DEFAULT NULL,
    pResultado     OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT
         p.NOV_NOMBRE nombre,
         p.NOV_APELLIDO apellido,
         s.TIPO_SEXO sexo
             FROM rcivil.vt_pk_persona_cuil p
             JOIN rcivil.vt_sexos s ON p.ID_SEXO=s.ID_SEXO
             WHERE p.NRO_OTRO_DOCUMENTO=pCUIL;

END pr_get_Personas_RCivil_CUIL;

--- edicion: Fernando Budassi --- fecha: 05/2016
--- utilizado en pagina inscripcion.aspx.cs
--- utilizado en pagina bocaRecepcion.aspx.cs
PROCEDURE pr_get_Dom_Empresa (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT distinct
            d.id_vin,
            d.id_entidad,
            d.id_tipodom,
            d.n_tipodom,
            d.id_dom_norm,
            d.id_provincia,
            d.n_provincia,
            d.id_departamento,
            d.n_departamento,
            d.id_localidad,
            d.n_localidad,
            d.id_barrio,
            d.n_barrio,
            d.id_tipocalle,
            d.n_tipocalle,
            d.id_calle,
            d.n_calle,
            d.altura,
            d.piso,
            d.depto,
            d.torre,
            d.MZNA,
            d.LOTE,
            (select c.cpa from dom_manager.vt_localidades_cpa c
             where c.id_localidad=d.id_localidad) cpa

            FROM dom_manager.vt_domicilios_todo_mzna_lote d
            join t_comunes.t_sedes_persjuridica s on d.id_vin=s.id_vin
           where s.cuit=pCuit and d.id_tipodom=3;

END pr_get_Dom_Empresa;
--- edicion: Fernando Budassi --- fecha: 05/2016
--- utilizado en pagina inscripcion.aspx.cs
PROCEDURE pr_get_Dom_Empresa_by_idvin (
    pidvin IN dom_manager.vt_domicilios_todo_mzna_lote.ID_VIN%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT 
            d.id_vin,
            d.id_entidad,
            d.id_tipodom,
            d.n_tipodom,
            d.id_dom_norm,
            d.id_provincia,
            d.n_provincia,
            d.id_departamento,
            d.n_departamento,
            d.id_localidad,
            d.n_localidad,
            d.id_barrio,
            d.n_barrio,
            d.id_tipocalle,
            d.n_tipocalle,
            d.id_calle,
            d.n_calle,
            d.altura,
            d.piso,
            d.depto,
            d.torre,
            d.MZNA,
            d.LOTE,
            nvl2(d.cpa,d.cpa,(select distinct c.cpa from dom_manager.vt_localidades_cpa c
             where c.id_localidad=d.id_localidad)) cpa

            FROM dom_manager.vt_domicilios_todo_mzna_lote d
            where d.id_vin=pidvin;


END pr_get_Dom_Empresa_by_idvin;
--- edicion: Fernando Budassi --- fecha: 07/2016 --- modificado 01/11/2016
--- utilizado en pagina inscripcion.aspx.cs
PROCEDURE pr_get_Com_Empresa (
    pNroTramite  IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS

 BEGIN
   
     OPEN pResultado FOR
       SELECT * 
       FROM t_comunes.t_comunicaciones t
       where t.id_entidad = to_char(pNroTramite) and t.tabla_origen='SIFCOS.T_SIF_TRAMITES_SIFCOS';
    
END pr_get_Com_Empresa;
--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina inscripcion.aspx.cs
PROCEDURE pr_get_productos_tramite (
    pNroTramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT pt.id_tramite_sifcos
                ,p.id_producto idproducto
                ,p.n_producto nproducto
         FROM sifcos.t_sif_productos_tramite pt
         JOIN sifcos.t_sif_productos p ON pt.id_producto=p.id_producto
         WHERE pt.id_tramite_sifcos=pNroTramite
           AND pt.confirmado='S';


END pr_get_productos_tramite;
--- edicion: Fernando Budassi --- fecha: 06/2016
--- utilizado en pagina misTramites.aspx.cs
PROCEDURE pr_get_misTramites (
    P_Nro_Tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    P_CUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    p_cuilUsuarioCidi IN sifcos.t_Sif_Tramites_Sifcos.cuil_usuario_cidi%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   v_SQL  varchar2(7000);
   v_SELECT varchar2(4000);
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   BEGIN
     v_SELECT :='SELECT 
         e.id_entidad,
         e.id_sede,
         pj.cuit,
         pj.razon_social,
         e.observacion nombre_fantasia,
         e.nro_sifcos,
         e.latitud_ubi,
         e.longitud_ubi,
         t.nro_tramite_sifcos nro_tramite,
         t.fec_ini_tramite INICIO_ACTIVIDAD,
         t.fec_alta,
         decode(d.N_CALLE,'''',d.N_LOCALIDAD,d.N_LOCALIDAD ||'' - ''||d.N_CALLE||'' ''||d.altura) DOMICILIO,
         et.n_estado_tramite ESTADO,
         he.descripcion DESC_ESTADO, 
         tt.n_tipo_tramite TIPO_TRAMITE ';
         
      v_FROM := ' FROM sifcos.t_sif_tramites_sifcos t
                  JOIN sifcos.t_sif_tipos_tramite tt on tt.id_tipo_tramite=t.id_tipo_tramite
                  JOIN sifcos.t_sif_entidades e ON e.id_entidad = t.id_entidad
                  JOIN sifcos.t_sif_tipos_tramite tt ON tt.id_tipo_tramite = t.id_tipo_tramite
                  JOIN sifcos.t_sif_hist_estado he on he.id_tramite_sifcos= t.nro_tramite_sifcos
                  JOIN sifcos.t_sif_estados_tramite et ON he.id_estado_tramite=et.id_estado_tramite
                  JOIN t_comunes.vt_pers_juridicas pj ON pj.cuit = e.cuit and pj.id_sede=e.id_sede
                  JOIN dom_manager.vt_domicilios d on d.id_vin=t.id_vin_dom_local  ';
      v_WHERE :=' WHERE he.id_estado_tramite<>9 and     
                  he.fec_desde_estado= (SELECT max(h.fec_desde_estado)
                                 FROM sifcos.t_sif_hist_estado h
                                 WHERE he.id_tramite_sifcos=h.id_tramite_sifcos
                                 ) and
                  t.nro_tramite_sifcos not in (SELECT distinct he.id_tramite_sifcos
                                 FROM sifcos.t_sif_hist_estado he
                                 WHERE he.id_estado_tramite=10
                                 and he.id_tramite_sifcos=t.nro_tramite_sifcos) ';
if(P_Nro_Tramite is not null) then
    v_WHERE:= v_WHERE ||' AND t.nro_tramite_sifcos ='||  P_Nro_Tramite ||' ';
  end if;
if(P_CUIT is not null) then
    v_WHERE:= v_WHERE || ' AND e.cuit ='||  P_CUIT ||' ';
  end if;  
                                           
 v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' order by t.nro_tramite_sifcos desc';
    OPEN pResultado FOR
      v_SQL;
      
END pr_get_misTramites;

--- edicion: Fernando Budassi --- fecha: 10/2016
--- utilizado en pagina inscripcion.aspx
--- para validar que no haya tramites cargados en el cuit ingresado
PROCEDURE pr_get_EntidadTramite (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
        SELECT e.id_entidad,  
      e.cuit, 
      to_char(e.nro_sifcos) nro_sifcos, 
      e.id_sede  id_sede_entidad, 
      t.nro_tramite_sifcos nro_tramite, 
      t.fec_alta  fecha_alta , 
      'SIFCOS_NUEVO' ORIGEN
      
FROM sifcos.t_sif_tramites_sifcos t
JOIN sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad
WHERE  t.id_tipo_tramite not in (5,6,7,8,9,10) and
       e.cuit = pCuit

UNION

select e2.id_entidad,  
      e2.cuit_pers_juridica cuit,
       t2.nro_recsep nro_sifcos,
       '' id_sede_entidad ,
      t2.id_tramite nro_tramite, 
      t2.fecha_alta ,
      'SIFCOS_VIEJO' ORIGEN
from sifcos.t_tramites t2
join sifcos.t_entidades e2 on t2.id_entidad = e2.id_entidad
where e2.cuit_pers_juridica = pCuit
     and  not exists  ( select * from sifcos.t_tramites tram where tram.id_tipo_tramite = 2 and tram.nro_recsep =t2.nro_recsep )
; /*valido que no este tomando sifcos dados de baja.*/

END pr_get_EntidadTramite;

--- edicion: Fernando Budassi --- fecha: 08/2016
--- utilizado en pagina bocaMinisterio.aspx.cs
PROCEDURE pr_get_tramites_baja (
    P_Nro_Tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    P_Nro_Sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    P_CUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    P_RAZON_SOCIAL IN t_comunes.vt_pers_juridicas.razon_social%TYPE DEFAULT NULL,
    P_ORDEN_CONSULTA IN varchar2,
    pResultado OUT sys_refcursor
   )
   IS
   v_SQL  varchar2(7000);
   v_SELECT varchar2(4000);
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   
                                                                                          
BEGIN
  
  v_SELECT :='SELECT t.nro_tramite_sifcos nro_tramite, 
                     pj.razon_social,
                     (select d.N_LOCALIDAD||'' - ''||d.N_CALLE||'' ''||d.altura 
                     from dom_manager.vt_domicilios d
                     where d.id_vin=id_vin_dom_local) DOMICILIO,
                     tt.n_tipo_tramite tipo_tramite,
                     t.fec_ini_tramite inicio_actividad,
                     t.fec_alta,
                     (SELECT et.n_estado_tramite
                     FROM sifcos.t_sif_hist_estado h
                     JOIN sifcos.t_sif_estados_tramite et
                     ON h.id_estado_tramite=et.id_estado_tramite
                     WHERE h.id_tramite_sifcos= t.nro_tramite_sifcos and
                     h.fec_desde_estado= (SELECT max(he.fec_desde_estado)
                                           FROM sifcos.t_sif_hist_estado he
                                           WHERE he.id_tramite_sifcos=h.id_tramite_sifcos
                                             and he.id_estado_tramite<>9)
                                           ) estado,
                     (SELECT h.descripcion
                     FROM sifcos.t_sif_hist_estado h
                     JOIN sifcos.t_sif_estados_tramite et ON h.id_estado_tramite=et.id_estado_tramite
                     WHERE h.id_tramite_sifcos= t.nro_tramite_sifcos and
                     h.fec_desde_estado= (SELECT max(he.fec_desde_estado)
                                           FROM sifcos.t_sif_hist_estado he
                                           WHERE he.id_tramite_sifcos=h.id_tramite_sifcos
                                             and he.id_estado_tramite<>9)
                                           )  DESC_ESTADO,
                     decode(e.nro_sifcos,''0'',''SIN ASIGNAR'',e.nro_sifcos) nro_sifcos,
                     e.cuit,
                     t.fec_vencimiento vto_tramite  ';
 v_FROM:=' FROM sifcos.t_sif_tramites_sifcos t
           JOIN sifcos.t_sif_entidades e ON e.id_entidad = t.id_entidad
           JOIN sifcos.t_sif_tipos_tramite tt ON tt.id_tipo_tramite = t.id_tipo_tramite
           JOIN t_comunes.vt_pers_juridicas pj ON pj.cuit = e.cuit and e.id_sede=pj.id_sede ';
v_WHERE:=' WHERE ROWNUM<=100 and 
                 t.nro_tramite_sifcos not in (SELECT distinct he.id_tramite_sifcos
                                         FROM sifcos.t_sif_hist_estado he
                                         WHERE he.id_estado_tramite=10
                                         and he.id_tramite_sifcos=t.nro_tramite_sifcos) ';

if(P_Nro_Tramite is not null) then
    v_WHERE:= v_WHERE ||' AND t.nro_tramite_sifcos ='||  P_Nro_Tramite ||' ';
  end if;
  if(P_Nro_Sifcos is not null) then
    v_WHERE:= v_WHERE || ' AND e.nro_sifcos like '||  P_Nro_Sifcos ||' ';
  end if;
  if(P_CUIT is not null) then
    v_WHERE:= v_WHERE || ' AND e.cuit ='||  P_CUIT ||' ';
  end if;
  if(P_RAZON_SOCIAL is not null) then
    v_WHERE:= v_WHERE || ' AND pj.razon_social like ''%'||  P_RAZON_SOCIAL ||'%''';
  end if;

v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' order by ' || P_ORDEN_CONSULTA;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_baja;
--- edicion: Fernando Budassi --- fecha: 08/2016
--- actualizado 07/04/2021
--- utilizado en pagina bocaMinisterio.aspx.cs
PROCEDURE pr_get_tramites_boca (
    P_Nro_Tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    P_Nro_Sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    P_CUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    P_RAZON_SOCIAL IN t_comunes.vt_pers_juridicas.razon_social%TYPE DEFAULT NULL,
    P_ORDEN_CONSULTA IN varchar2,
    P_IDESTADO IN varchar2,
    p_FECHADESDE IN varchar2,
    p_FECHAHASTA IN varchar2,
    pResultado OUT sys_refcursor
   )
   IS
   v_SQL  varchar2(7000);
   v_SELECT varchar2(4000);
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
    
                                                                                          
BEGIN
   V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
    
  v_SELECT :='SELECT t.nro_tramite_sifcos nro_tramite, 
                decode(e.nro_sifcos,''0'',''SIN ASIGNAR'',e.nro_sifcos) nro_sifcos,
                tt.n_tipo_tramite tipo_tramite,
                e.cuit,
                pj.razon_social,
                decode(d.N_CALLE,'''',d.N_LOCALIDAD,d.N_LOCALIDAD ||'' - ''||d.N_CALLE||'' ''||d.altura) DOMICILIO,
                et.n_estado_tramite estado,
                et.id_estado_tramite ID_ESTADO, 
                t.fec_vencimiento vto_tramite  ';
 v_FROM:=' FROM sifcos.t_sif_tramites_sifcos t
         JOIN sifcos.t_sif_entidades e ON e.id_entidad = t.id_entidad
         JOIN sifcos.t_sif_tipos_tramite tt ON tt.id_tipo_tramite = t.id_tipo_tramite
         JOIN sifcos.t_sif_hist_estado he on he.id_tramite_sifcos= t.nro_tramite_sifcos
         JOIN sifcos.t_sif_estados_tramite et ON he.id_estado_tramite=et.id_estado_tramite
         JOIN t_comunes.vt_pers_juridicas pj ON pj.cuit = e.cuit and pj.id_sede=e.id_sede
         JOIN dom_manager.vt_domicilios d on d.id_vin=t.id_vin_dom_local  ';
 v_WHERE:='WHERE     
           he.fec_desde_estado= (SELECT max(h.fec_desde_estado)
                                 FROM sifcos.t_sif_hist_estado h
                                 WHERE he.id_tramite_sifcos=h.id_tramite_sifcos
                                       
                                 ) '; 

if(P_Nro_Tramite is not null) then
    v_WHERE:= v_WHERE ||' AND t.nro_tramite_sifcos ='||  P_Nro_Tramite ||' ';
  end if;
  
  if(P_Nro_Sifcos is not null) then
    v_WHERE:= v_WHERE || ' AND e.nro_sifcos like '||  P_Nro_Sifcos ||' ';
  end if;
  
  if(P_CUIT is not null) then
    v_WHERE:= v_WHERE || ' AND e.cuit ='||  P_CUIT ||' ';
  end if;
  
  if(P_RAZON_SOCIAL is not null) then
    v_WHERE:= v_WHERE || ' AND pj.razon_social like ''%'||  P_RAZON_SOCIAL ||'%''';
  end if;
  
  if(P_IDESTADO is not null) then
    v_WHERE:= v_WHERE || ' AND he.id_estado_tramite ='||  P_IDESTADO ||' ';
  end if;
   if(P_FECHADESDE is not null AND P_FECHAHASTA is not null) then
    v_WHERE:= v_WHERE || ' AND t.fec_alta between '''||  P_FECHADESDE || ''' and ''' ||  P_FECHAHASTA ||''' ';
  end if;
  if(P_ORDEN_CONSULTA = 0) then
  v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' order by t.nro_tramite_sifcos desc' ;
  else
  v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' order by ' || P_ORDEN_CONSULTA;
  end if;
  OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_boca;
--- edicion: Fernando Budassi --- fecha: 08/2016 
--- actualizado 07/04/2021
--- utilizado en pagina bocaRecepcion.aspx.cs
PROCEDURE pr_get_tramites_boca_recepcion (
  P_Nro_Tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
  P_Nro_Sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
  P_CUIT IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
  P_RAZON_SOCIAL IN t_comunes.vt_pers_juridicas.razon_social%TYPE DEFAULT NULL,
  P_ORDEN_CONSULTA IN varchar2,
  P_IDESTADO IN varchar2,
  p_FECHADESDE IN varchar2,
  p_FECHAHASTA IN varchar2,
  p_id_organismo IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(10000);
   v_SELECT varchar2(4000);
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   v_WHERE_2 varchar2(2000);
   v_SQL_2 varchar2(10000);
 
                                                                                          
BEGIN
 
   V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
  v_SELECT :='SELECT t.nro_tramite_sifcos nro_tramite, 
                decode(e.nro_sifcos,''0'',''SIN ASIGNAR'',e.nro_sifcos) nro_sifcos,
                tt.n_tipo_tramite tipo_tramite,
                e.cuit,
                pj.razon_social,
                decode(d.N_CALLE,'''',d.N_LOCALIDAD,d.N_LOCALIDAD ||'' - ''||d.N_CALLE||'' ''||d.altura) DOMICILIO,
                et.n_estado_tramite estado,
                et.id_estado_tramite ID_ESTADO, 
                t.fec_vencimiento vto_tramite  ';
  v_FROM:=' FROM sifcos.t_sif_tramites_sifcos t
            JOIN sifcos.t_sif_entidades e ON e.id_entidad = t.id_entidad
            JOIN sifcos.t_sif_tipos_tramite tt ON tt.id_tipo_tramite = t.id_tipo_tramite
            JOIN sifcos.t_sif_hist_estado he on he.id_tramite_sifcos= t.nro_tramite_sifcos
            JOIN sifcos.t_sif_estados_tramite et ON he.id_estado_tramite=et.id_estado_tramite
            JOIN t_comunes.vt_pers_juridicas pj ON pj.cuit = e.cuit and pj.id_sede=e.id_sede
            JOIN dom_manager.vt_domicilios d on d.id_vin=t.id_vin_dom_local  ';

  v_WHERE:='WHERE      
           he.fec_desde_estado= (SELECT max(h.fec_desde_estado)
                                 FROM sifcos.t_sif_hist_estado h
                                 WHERE he.id_tramite_sifcos=h.id_tramite_sifcos
                                 ) and
           t.nro_tramite_sifcos not in (SELECT distinct he.id_tramite_sifcos
                                         FROM sifcos.t_sif_hist_estado he
                                         WHERE he.id_estado_tramite=10
                                         and he.id_tramite_sifcos=t.nro_tramite_sifcos)';          


  if(P_Nro_Tramite is not null) then
    v_WHERE:= v_WHERE ||' AND t.nro_tramite_sifcos ='||  P_Nro_Tramite ||' ';
  end if;
  if(P_Nro_Sifcos is not null) then
    v_WHERE:= v_WHERE || ' AND e.nro_sifcos like '||  P_Nro_Sifcos ||' ';
  end if;
  if(P_CUIT is not null) then
    v_WHERE:= v_WHERE || ' AND e.cuit ='||  P_CUIT ||' ';
  end if;
  if(P_RAZON_SOCIAL is not null) then
    v_WHERE:= v_WHERE || ' AND pj.razon_social like ''%'||  P_RAZON_SOCIAL ||'%''';
  end if;
 
  if(P_FECHADESDE is not null AND P_FECHAHASTA is not null) then
    v_WHERE:= v_WHERE || ' AND t.fec_alta between '''||  P_FECHADESDE || ''' and ''' ||  P_FECHAHASTA ||''' ';
  end if;
  if(P_IDESTADO is not null and p_id_organismo is not null) then
    v_WHERE:= v_WHERE || ' AND t.id_organismo_alta ='||  p_id_organismo ||' ';
    v_WHERE:= v_WHERE || ' AND he.id_estado_tramite ='||  P_IDESTADO ||' ';
  end if;
 
  

 if(P_ORDEN_CONSULTA = 0) then
  v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' order by t.nro_tramite_sifcos desc' ;
  else
  v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' order by ' || P_ORDEN_CONSULTA;
  end if;
  OPEN pResultado FOR
      v_SQL;

END pr_get_tramites_boca_recepcion;
--- edicion: Fernando Budassi --- fecha: 08/2016
--- utilizado en pagina bocaRecepcion.aspx.cs
PROCEDURE pr_get_info_gral_tramite (
  P_Nro_Tramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
  pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT t.nro_tramite_sifcos nro_tramite,
                to_char(t.fec_alta,'dd/mm/yyyy') fec_alta,
                nvl2(e.nro_sifcos,e.nro_sifcos||'/'||to_char(t.fec_alta,'YYYY'),'NO ASIGNADO') nro_sifcos,
                e.nro_sifcos ns,
                e.cuit,
                tc_pj.razon_social,
                t.fec_ini_tramite fec_inicio_act,
                tc_pj.nro_ingbruto,
                tc_pj.nro_hab_municipal,
                (SELECT distinct ap.N_ACTIVIDAD 
                 FROM t_comunes.vt_actividades ap
                 WHERE ap.id_actividad=t.id_actividad_ppal) ACTIVIDAD_PRI,
                 (SELECT distinct ap.N_ACTIVIDAD 
                 FROM t_comunes.vt_actividades ap
                 WHERE ap.id_actividad=t.id_actividad_sria) ACTIVIDAD_SEC,
                 /*(select sum(importe_total)
                  from sifcos.t_sif_tramites_sifcos t
                  join sifcos.t_sif_tasas ta on t.nro_tramite_sifcos=ta.nro_tramite_sifcos
                  join tasas_servicio.vt_transacciones_micm m on m.obligacion=ta.nro_transaccion
                  where m.pagado='N'
                  ) deuda,*/
                (SELECT et.n_estado_tramite
                FROM sifcos.t_sif_hist_estado h
                JOIN sifcos.t_sif_estados_tramite et
                ON h.id_estado_tramite=et.id_estado_tramite
                WHERE  h.id_tramite_sifcos=t.nro_tramite_sifcos and
                       h.fec_desde_estado= (SELECT max(h2.fec_desde_estado)
                                           FROM sifcos.t_sif_hist_estado h2
                                           WHERE h2.id_tramite_sifcos=t.nro_tramite_sifcos)) estado,
               (SELECT h.fec_desde_estado
                FROM sifcos.t_sif_hist_estado h
                JOIN sifcos.t_sif_estados_tramite et
                ON h.id_estado_tramite=et.id_estado_tramite
                WHERE h.id_tramite_sifcos=t.nro_tramite_sifcos and
                      h.fec_desde_estado= (SELECT max(h2.fec_desde_estado)
                                           FROM sifcos.t_sif_hist_estado h2
                                           WHERE h2.id_tramite_sifcos=t.nro_tramite_sifcos)) fecha_estado,
                                           
                rc1.NOV_APELLIDO||', '||rc1.NOV_NOMBRE rep_legal,
                rc1.NRO_DOCUMENTO dni_rep_legal,
                tc.n_cargo cargo_rep_legal,
                rc.NOV_APELLIDO||', '||rc.NOV_NOMBRE  gestor,
                rc.NRO_DOCUMENTO  dni_gestor
                
                 
                                      
         FROM sifcos.t_sif_tramites_sifcos t
         JOIN sifcos.t_sif_entidades e ON e.id_entidad = t.id_entidad
         JOIN sifcos.t_sif_tipos_tramite tt on tt.id_tipo_tramite = t.id_tipo_tramite
         JOIN t_comunes.vt_pers_juridicas_completa tc_pj on tc_pj.cuit = e.cuit
                                                        and tc_pj.id_sede = e.id_sede
         JOIN sifcos.t_sif_gestores g on g.id_gestor=t.id_gestor_entidad                                                        
         JOIN rcivil.vt_pk_persona rc on rc.ID_SEXO=g.id_sexo 
                                           and rc.NRO_DOCUMENTO=g.nro_documento 
                                           and rc.PAI_COD_PAIS=g.pai_cod_pais 
                                           and rc.ID_NUMERO=g.id_numero     
         JOIN sifcos.t_sif_rep_legal r on r.id_rep_legal=t.id_cargo_entidad 
         JOIN rcivil.vt_pk_persona rc1 on rc1.ID_SEXO=r.id_sexo 
                                           and rc1.NRO_DOCUMENTO=r.nro_documento 
                                           and rc1.PAI_COD_PAIS=r.pai_cod_pais 
                                           and rc1.ID_NUMERO=r.id_numero  
         JOIN sifcos.t_sif_tipos_cargo tc on tc.id_cargo=r.id_cargo  
         WHERE t.nro_tramite_sifcos = P_Nro_Tramite
         ;

END pr_get_info_gral_tramite;
--- edicion: Fernando Budassi --- fecha: 06/2016
--- utilizado en pagina Inscripcion.aspx.cs
--- utilizado en pagina bocaRecepcion.aspx.cs
PROCEDURE pr_Consulta_Tramite (
    p_nroTramite  IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT distinct t.nro_tramite_sifcos,
         t.id_vin_dom_legal,
         t.id_vin_dom_local,
         t.capacitacion_ult_anio,
         t.cuil_usuario_cidi,
         t.id_tipo_tramite,
         tt.n_tipo_tramite,
         t.fec_ini_tramite,
         t.fec_vencimiento,
         t.rango_alq rango_alquiler,
         t.cant_pers_total,
         t.cant_pers_rel_dependencia,
         t.id_localidad_certifica_resp,
         o.n_origen_proveedor,
         (SELECT et.n_estado_tramite
                FROM sifcos.t_sif_hist_estado h
                JOIN sifcos.t_sif_estados_tramite et
                ON h.id_estado_tramite=et.id_estado_tramite
                WHERE h.id_tramite_sifcos= t.nro_tramite_sifcos and
                h.fec_desde_estado= (SELECT max(he.fec_desde_estado)
                                           FROM sifcos.t_sif_hist_estado he
                                           WHERE he.id_tramite_sifcos=h.id_tramite_sifcos
                                             and he.id_estado_tramite<>9)
                                           ) n_estado_tramite,
         (SELECT h.descripcion
                FROM sifcos.t_sif_hist_estado h
                JOIN sifcos.t_sif_estados_tramite et
                ON h.id_estado_tramite=et.id_estado_tramite
                WHERE h.id_tramite_sifcos= t.nro_tramite_sifcos and
                h.fec_desde_estado= (SELECT max(he.fec_desde_estado)
                                           FROM sifcos.t_sif_hist_estado he
                                           WHERE he.id_tramite_sifcos=h.id_tramite_sifcos
                                             and he.id_estado_tramite<>9)
                                           ) desc_estado_tramite,
         e.id_entidad,
         e.cuit,
         pj.razon_social,
         e.observacion nombre_fantasia,
         e.id_sede,
         DECODE((SELECT distinct s.id_sede||' - '||s.n_sede||' - '||d.N_CALLE||' '||d.altura||', '||d.n_localidad
            FROM dom_manager.vt_domicilios_repat d,t_comunes.t_sedes_persjuridica s
            WHERE d.id_vin=s.id_vin
            AND s.cuit=e.cuit and s.id_sede=e.id_sede),
            (SELECT distinct s.id_sede||' - '||s.n_sede||' - '||d.N_CALLE||' '||d.altura||', '||d.n_localidad
            FROM dom_manager.vt_domicilios_repat d,t_comunes.t_sedes_persjuridica s
            WHERE d.id_vin=s.id_vin
            AND s.cuit=e.cuit and s.id_sede=e.id_sede),'00 - CENTRAL') sedes,
         e.local,
         e.oficina,
         e.stand,
         e.cobertura_medica,
         e.seguro_local,
         e.latitud_ubi,
         e.longitud_ubi,
         e.propietario,
         e.nro_sifcos,
         apri.N_ACTIVIDAD ACTIVIDAD_PRI,
         asec.N_ACTIVIDAD ACTIVIDAD_SEC,
/*
         (SELECT distinct ap.N_ACTIVIDAD 
          FROM t_comunes.vt_actividades ap
          WHERE ap.id_actividad=t.id_actividad_ppal) ACTIVIDAD_PRI,
         (SELECT distinct ap.N_ACTIVIDAD 
          FROM t_comunes.vt_actividades ap
          WHERE ap.id_actividad=t.id_actividad_sria) ACTIVIDAD_SEC,
*/
         (SELECT tc.n_cargo 
          FROM sifcos.t_sif_tramites_sifcos t1
          JOIN sifcos.t_sif_rep_legal r on r.id_rep_legal=t1.id_cargo_entidad
          JOIN sifcos.t_sif_tipos_cargo tc on tc.id_cargo=r.id_cargo
          WHERE t1.nro_tramite_sifcos=t.nro_tramite_sifcos) n_cargo,
         (SELECT distinct rc.NOV_APELLIDO||', '||rc.NOV_NOMBRE
          FROM sifcos.t_sif_tramites_sifcos t1
          JOIN sifcos.t_sif_rep_legal rl on rl.id_rep_legal=t1.id_cargo_entidad 
          JOIN rcivil.vt_pk_persona rc on rc.ID_SEXO=rl.id_sexo 
                                           and rc.NRO_DOCUMENTO=rl.nro_documento 
                                           and rc.PAI_COD_PAIS=rl.pai_cod_pais 
                                           and rc.ID_NUMERO=rl.id_numero
          where t.id_cargo_entidad=rl.id_rep_legal
            and t1.nro_tramite_sifcos=t.nro_tramite_sifcos) rep_legal,
                 
         (SELECT distinct rc.NRO_DOCUMENTO
          FROM sifcos.t_sif_tramites_sifcos t1
          JOIN sifcos.t_sif_rep_legal rl on rl.id_rep_legal=t1.id_cargo_entidad 
          JOIN rcivil.vt_pk_persona rc on rc.ID_SEXO=rl.id_sexo 
                                           and rc.NRO_DOCUMENTO=rl.nro_documento 
                                           and rc.PAI_COD_PAIS=rl.pai_cod_pais 
                                           and rc.ID_NUMERO=rl.id_numero
          where t.id_cargo_entidad=rl.id_rep_legal
            and t1.nro_tramite_sifcos=t.nro_tramite_sifcos) dni_rep_legal,
          
         
           '' celular_rep_legal,
       
         (select distinct rc.NOV_APELLIDO||', '||rc.NOV_NOMBRE 
          from sifcos.t_sif_tramites_sifcos t1
          join sifcos.t_sif_gestores g on g.id_gestor=t1.id_gestor_entidad
          JOIN rcivil.vt_pk_persona rc on rc.ID_SEXO=g.id_sexo 
                                           and rc.NRO_DOCUMENTO=g.nro_documento 
                                           and rc.PAI_COD_PAIS=g.pai_cod_pais 
                                           and rc.ID_NUMERO=g.id_numero
          where t.id_gestor_entidad=g.id_gestor
            and t1.nro_tramite_sifcos=t.nro_tramite_sifcos) gestor,
                 
         (SELECT distinct rc.NRO_DOCUMENTO
          FROM sifcos.t_sif_tramites_sifcos t1
          JOIN sifcos.t_sif_gestores g1 on g1.id_gestor=t1.id_gestor_entidad 
          JOIN rcivil.vt_pk_persona rc on rc.ID_SEXO=g1.id_sexo 
                                           and rc.NRO_DOCUMENTO=g1.nro_documento 
                                           and rc.PAI_COD_PAIS=g1.pai_cod_pais 
                                           and rc.ID_NUMERO=g1.id_numero
          where t.id_gestor_entidad=g1.id_gestor
            and t1.nro_tramite_sifcos=t.nro_tramite_sifcos) dni_gestor,
          
          pj.nro_ingbruto nro_dgr,
          pj.nro_hab_municipal,
         (select distinct se1.superficie from sifcos.t_sif_superficies_empresa se1
          where se1.id_tramite_sifcos=t.nro_tramite_sifcos
            and se1.id_tipo_superficie=1) sup_admin,
         (select distinct se2.superficie from sifcos.t_sif_superficies_empresa se2
          where se2.id_tramite_sifcos=t.nro_tramite_sifcos
            and se2.id_tipo_superficie=2) sup_ventas,
         (select distinct se3.superficie from sifcos.t_sif_superficies_empresa se3
          where se3.id_tramite_sifcos=t.nro_tramite_sifcos
            and se3.id_tipo_superficie=3) sup_deposito,
        --Ids de actividades 
        t.id_actividad_ppal,
        t.id_actividad_sria,
        --datos de rubro
        t.id_rubro_pri,
        t.id_rubro_sec,
        rupri.n_rubro RUBRO_PRI,
        rusec.n_rubro RUBRO_SEC
         FROM sifcos.t_sif_entidades e
         left join t_comunes.vt_pers_juridicas_completa pj on e.cuit=pj.cuit  and NVL(e.id_sede,'00')=pj.id_sede 
         join sifcos.t_sif_tramites_sifcos t on t.id_entidad=e.id_entidad
         join sifcos.t_sif_tipos_tramite tt on t.id_tipo_tramite=tt.id_tipo_tramite
         left join sifcos.t_sif_origenes_proveedor o on o.id_origen_proveedor=t.id_origen_proveedor
        --Rubro Primario 
         LEFT JOIN t_sif_rubros rupri
         ON rupri.id_rubro = t.id_rubro_pri
         --Rubro secundario
         LEFT JOIN t_sif_rubros rusec
         ON rusec.id_rubro =t.id_rubro_sec
         --Actividad primaria
        LEFT JOIN t_comunes.vt_actividades apri
        ON apri.id_actividad=t.id_actividad_ppal
         --Actividad secundaria
        LEFT JOIN t_comunes.vt_actividades asec 
        ON asec.id_actividad=t.id_actividad_sria         
         WHERE t.nro_tramite_sifcos=p_nroTramite;
         

END pr_Consulta_Tramite;
--- edicion: Fernando Budassi --- fecha: 04/2016
--- temporal hasta migracion total de datos de sifcos viejo
PROCEDURE pr_get_Entidad_Viejo_Sifcos (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS

   BEGIN
     OPEN pResultado FOR
           SELECT e.cuit cuit
                  ,t.nro_recsep nro_Sifcos
           FROM   sifcos.t_tramites_sifcos t,sifcos.t_sif_entidades e
           WHERE  t.id_entidad=e.id_entidad
             AND  e.cuit=pCuit --'30708020229'
             AND  t.id_tipo_tramite=1;

END pr_get_Entidad_Viejo_Sifcos;
--- edicion: Fernando Budassi --- fecha: 04/2016
--- temporal hasta migracion total de datos de sifcos viejo luego sera este solo
PROCEDURE pr_get_Entidad_Nuevo_Sifcos (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS

   BEGIN
     OPEN pResultado FOR
           SELECT e.cuit
                  ,e.nro_sifcos
           FROM   sifcos.t_sif_entidades e
           WHERE  e.cuit=pCuit
              and e.nro_sifcos is not null;

END pr_get_Entidad_Nuevo_Sifcos;
--- edicion: Fernando Budassi --- fecha: 04/2016
--- utilizado en pagina AbmRoles.aspx.cs
--- obtencion de todos los roles
PROCEDURE pr_get_Roles (
    pResultado OUT sys_refcursor
   )

   IS
    
   BEGIN
     OPEN pResultado FOR
          SELECT r.id_rol
                ,r.n_rol
          FROM sifcos.t_sif_roles r
          WHERE r.n_rol<>'Sin Asignar';
    
END pr_get_Roles;

--- edicion: Fernando Budassi --- fecha: 04/2016
--- utilizado en pagina AbmOrganismos.aspx.cs
--- obtencion de todos los organismos
  PROCEDURE pr_get_Organismos (
    pPrefijo in varchar2,
    pResultado OUT sys_refcursor
   )

   IS
   v_prefijo  varchar2(200) := '%' ||pPrefijo|| '%' ;
    
   BEGIN
     OPEN pResultado FOR
          SELECT distinct
                 o.id_organismo,
                 pj.razon_social n_organismo
          FROM sifcos.t_sif_organismos o
         join t_comunes.vt_pers_juridicas pj on pj.cuit = o.cuit and o.id_sede = pj.id_sede 
         where pj.razon_social like upper(v_prefijo)
         order by 2;
    
END pr_get_Organismos;
--- edicion: Fernando Budassi --- fecha: 07/2017
--- utilizado en pagina reporte gerencial
--- obtencion de todos los organismos filtrados
PROCEDURE pr_get_Organismos_Filtrados (
    pOrganismos in varchar2,
    pResultado OUT sys_refcursor
   )

   IS
    V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(3000);
   v_SELECT varchar2(1000);
   v_FROM varchar2(1000);
   v_WHERE varchar2(1000);
   
 
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
  
  v_SELECT :='SELECT distinct
                 o.id_organismo,
                 pj.razon_social n_organismo ';
                
 v_FROM:=' FROM sifcos.t_sif_organismos o 
           join t_comunes.vt_pers_juridicas pj on pj.cuit = o.cuit ';
         
v_WHERE:=' where rownum < 1000 ';

if(pOrganismos is not null) then
    v_WHERE:= v_WHERE ||' AND o.id_organismo_superior = '||pOrganismos;
  end if;   
     
v_SQL:=v_SELECT || v_FROM ||v_WHERE||' order by 1';
    OPEN pResultado FOR
      v_SQL;
 
END pr_get_Organismos_Filtrados;

PROCEDURE pr_get_Organismos_one (
    pIdOrganismo in varchar2,
    pResultado OUT sys_refcursor
   )

   IS
    V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(3000);
   v_SELECT varchar2(1000);
   v_FROM varchar2(1000);
   v_WHERE varchar2(1000);
   
 
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
  
  v_SELECT :='SELECT distinct
                 o.id_organismo,
                 pj.razon_social n_organismo ';
                
 v_FROM:=' FROM sifcos.t_sif_organismos o 
           join t_comunes.vt_pers_juridicas pj on pj.cuit = o.cuit ';
         
v_WHERE:=' where rownum < 1000 ';

if(pIdOrganismo is not null) then
    v_WHERE:= v_WHERE ||' AND o.id_organismo = '||pIdOrganismo;
  end if;   
     
v_SQL:=v_SELECT || v_FROM ||v_WHERE||' order by 1';
    OPEN pResultado FOR
      v_SQL;
 
END pr_get_Organismos_one;
--- edicion: Fernando Budassi --- fecha: 11/2016
--- utilizado en pagina AbmRoles.aspx.cs
--- sirve para determinar el rol de acceso al usuario
PROCEDURE pr_get_Rol_Usuario (
    p_UsuarioCidi IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS
   v_existe  varchar2(1);
   v_rol_especial varchar2(1);
   BEGIN
     SELECT count(*)
     INTO v_existe
     FROM sifcos.t_sif_usuarios_cidi u
     WHERE u.id_usuario_cidi=p_UsuarioCidi AND U.N_APLICACION='SIFCOS';
     
     IF (v_existe!=0) THEN
          SELECT u.id_rol
          INTO pResultado
          FROM sifcos.t_sif_usuarios_cidi u
          where u.id_usuario_cidi=p_UsuarioCidi  AND U.N_APLICACION='SIFCOS';
         
     ELSE
          pResultado:=0;       

     END IF;
      
          
   return;          
          
    
END pr_get_Rol_Usuario;

--- edicion: Fernando Budassi --- fecha: 09/2021
--- sirve para consultar las relaciones que posee cada usuario
PROCEDURE pr_get_Relaciones_Usuario (
    p_UsuarioCidi IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )

   IS
   v_existe  varchar2(1);
   BEGIN
     OPEN pResultado FOR
          SELECT *
          FROM sifcos.t_sif_usuarios_cidi u
          where u.id_usuario_cidi=p_UsuarioCidi  AND U.N_APLICACION='RELACION';
     
END pr_get_Relaciones_Usuario;


--- edicion: Fernando Budassi --- fecha: 11/2016
--- utilizado en pagina AbmUsuariosYOrganismos.aspx.cs
--- sirve para determinar el organismo al que pertenece el usuario
PROCEDURE pr_get_Org_Usuario (
    p_UsuarioCidi IN sifcos.t_sif_usuarios_organismo.cuil_usr_cidi%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )

   IS
   v_existe  varchar2(1);
   BEGIN
     SELECT count(*)
     INTO v_existe
     FROM sifcos.t_sif_usuarios_organismo uo
     WHERE uo.cuil_usr_cidi=p_UsuarioCidi;
     
     IF (v_existe!=0) THEN
          SELECT uo.id_organismo
          INTO pResultado
          FROM sifcos.t_sif_usuarios_organismo uo
          where uo.cuil_usr_cidi=p_UsuarioCidi;
     ELSE 
         pResultado:=0;
     END IF;     
          
   return;          
          
    
END pr_get_Org_Usuario;

--- edicion: Fernando Budassi --- fecha: 09/2016
--- utilizado en pagina AbmRoles.aspx.cs
--- sirve para determinar el rol de acceso al usuario
PROCEDURE pr_get_Roles_usuarios (
    p_rol IN sifcos.t_sif_Roles.n_rol%TYPE DEFAULT NULL,
    p_cuil IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    P_ORDEN_CONSULTA IN varchar2,
    pResultado OUT sys_refcursor
   )

   IS
      v_SQL  varchar2(6000);
      v_SELECT varchar2(2000);
      v_FROM varchar2(2000);
      v_WHERE varchar2(2000);
   BEGIN
      v_SELECT :='SELECT rownum id
                ,u.id_usuario_cidi
                ,r.n_rol rol
                ,u.fec_ult_acceso
                ,NVL2((SELECT p.NOV_APELLIDO ||'', '' ||p.NOV_NOMBRE 
                 FROM rcivil.vt_pk_persona_cuil p
                 WHERE p.NRO_OTRO_DOCUMENTO=u.id_usuario_cidi),
                 (SELECT p.NOV_APELLIDO ||'', '' ||p.NOV_NOMBRE 
                 FROM rcivil.vt_pk_persona_cuil p
                 WHERE p.NRO_OTRO_DOCUMENTO=u.id_usuario_cidi)
                 ,''No Encontrado''
                 )apeynom ';
 v_FROM:=' FROM sifcos.t_sif_roles r
     join sifcos.t_sif_usuarios_cidi u on r.id_rol=u.id_rol ';
v_WHERE:=' WHERE ROWNUM<=10000 ';

if(p_rol is not null) then
    v_WHERE:= v_WHERE ||' AND r.n_rol ='''||  p_rol ||''' ';
  end if;  
if(p_cuil is not null) then
    v_WHERE:= v_WHERE ||' AND u.id_usuario_cidi ='||  p_cuil ||' ';
  end if;              
         

v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' order by ' || P_ORDEN_CONSULTA;
    OPEN pResultado FOR
      v_SQL;
 
END pr_get_Roles_usuarios;

--- edicion: Fernando Budassi --- fecha: 11/2016
--- utilizado en pagina AbmUsuariosYOrganismo.aspx.cs
--- sirve para determinar el organismo de acceso al usuario
PROCEDURE pr_get_Organismos_usuarios (
    p_Organismo IN VARCHAR2,
    p_cuil IN sifcos.t_sif_usuarios_cidi.id_usuario_cidi%TYPE DEFAULT NULL,
    P_ORDEN_CONSULTA IN varchar2,
    pResultado OUT sys_refcursor
   )

   IS
      v_SQL  varchar2(6000);
      v_SELECT varchar2(2000);
      v_FROM varchar2(2000);
      v_WHERE varchar2(2000);
   BEGIN
      v_SELECT :='SELECT distinct 
                u.id_usuario_cidi
                ,pj.razon_social organismo
                ,u.fec_ult_acceso
                ,NVL2((SELECT p.NOV_APELLIDO ||'', '' ||p.NOV_NOMBRE 
                 FROM rcivil.vt_pk_persona_cuil p
                 WHERE p.NRO_OTRO_DOCUMENTO=u.id_usuario_cidi),
                 (SELECT p.NOV_APELLIDO ||'', '' ||p.NOV_NOMBRE 
                 FROM rcivil.vt_pk_persona_cuil p
                 WHERE p.NRO_OTRO_DOCUMENTO=u.id_usuario_cidi)
                 ,''No encontrado ''
                 )apeynom ';
 v_FROM:=' FROM sifcos.t_sif_usuarios_organismo uo
     join sifcos.t_sif_usuarios_cidi u on u.id_usuario_cidi=uo.cuil_usr_cidi 
     join sifcos.t_sif_organismos o on o.id_organismo = uo.id_organismo
     join t_comunes.vt_pers_juridicas pj on pj.cuit = o.cuit ';
v_WHERE:=' WHERE ROWNUM<=10000 and uo.fec_hasta is null ';

if(p_Organismo is not null) then
    v_WHERE:= v_WHERE ||' AND pj.razon_social ='''||  p_Organismo ||''' ';
  end if;  
if(p_cuil is not null) then
    v_WHERE:= v_WHERE ||' AND u.id_usuario_cidi ='||  p_cuil ||' ';
  end if;        
         

v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' order by ' || P_ORDEN_CONSULTA;
    OPEN pResultado FOR
      v_SQL;
 
END pr_get_Organismos_usuarios;
--- edicion: Fernando Budassi --- fecha: 04/2016
--- utilizado en pagina Inscripcion.aspx.cs
--- utilizado en pagina bocaRecepcion.aspx.cs
PROCEDURE pr_get_Superficies (
    pPrefijo IN sifcos.t_sif_tipos_superficie.n_tipo_superficie%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )

   IS
   v_prefijo  varchar2(100) := '%'||pPrefijo||'%' ;

   BEGIN
     OPEN pResultado FOR
          SELECT ts.id_tipo_superficie,ts.n_tipo_superficie
          FROM sifcos.t_sif_tipos_superficie ts
          WHERE upper(ts.n_tipo_superficie) like v_prefijo
          and rownum<10 order by ts.n_tipo_superficie;


END pr_get_Superficies;
--- edicion: Fernando Budassi --- fecha: 04/2016
--- utilizado en pagina Inscripcion.aspx.cs
PROCEDURE pr_get_SuperficiesByIdTramite (
    pNroTramite IN sifcos.t_sif_superficies_empresa.id_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )

   IS

   BEGIN
     OPEN pResultado FOR
          SELECT se.id_tramite_sifcos,ts.id_tipo_superficie,ts.n_tipo_superficie,se.superficie
          FROM sifcos.t_sif_superficies_empresa se
          join sifcos.t_sif_tipos_superficie ts on se.id_tipo_superficie=ts.id_tipo_superficie
          WHERE se.id_tramite_sifcos=pNroTramite;



END pr_get_SuperficiesByIdTramite;
--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina Inscripcion.aspx.cs
--- utilizado en pagina bocaRecepcion.aspx.cs
PROCEDURE pr_get_Productos (
    pPrefijo IN sifcos.t_sif_productos.n_producto%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
    IS
    v_prefijo  varchar2(100) := pPrefijo|| '%' ;

   BEGIN
     OPEN pResultado FOR
          SELECT p.id_producto idproducto
                ,p.n_producto nproducto
          FROM sifcos.t_sif_productos p
          WHERE p.n_producto like v_prefijo
          order by p.n_producto;


END pr_get_Productos;

--- edicion: Fernando Budassi --- fecha: 27/03/2017
--- utilizado en pagina ProductosYActividades.aspx
--- se buscan todos los productos sin relacion con alguna actividad
PROCEDURE pr_get_Productos_sin_asignar (
     pResultado OUT sys_refcursor
   )
    IS
   

   BEGIN
     OPEN pResultado FOR
          SELECT p.id_producto,p.n_producto from sifcos.t_sif_productos p
          order by p.n_producto;
          /*WHERE not exists
          (SELECT * from sifcos.t_sif_productos_actividad pa
                  where pa.id_producto=p.id_producto)*/--modificacion temporal


END pr_get_Productos_sin_asignar;

--- edicion: Fernando Budassi --- fecha: 27/03/2017
--- utilizado en pagina ProductosYActividades.aspx
--- se buscan todos los productos sin relacion con alguna actividad
PROCEDURE pr_get_actividades (
     pResultado OUT sys_refcursor
   )
    IS
   

   BEGIN
     OPEN pResultado FOR
          SELECT a.id_actividad,a.n_actividad 
          FROM t_comunes.t_actividades a
          WHERE a.cod_clanae is not null;


END pr_get_actividades;

--- edicion: Fernando Budassi --- fecha: 11/2016
--- utilizado en pagina Inscripcion.aspx.cs
--- utilizado en pagina bocaRecepcion.aspx.cs
PROCEDURE pr_get_ProductosBETA (
    pPrefijo IN sifcos.t_sif_productos.n_producto%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
    IS
   
   v_prefijo  varchar2(100) := pPrefijo|| '%' ;
   
   BEGIN
     OPEN pResultado FOR
          SELECT p.id_producto idproducto
                ,p.n_producto nproducto
          FROM sifcos.t_sif_productos p
          WHERE p.id_producto in (select distinct prod_act.id_producto  from sifcos.t_sif_productos_actividad prod_act)
            AND p.n_producto like v_prefijo
          order by p.n_producto;


END pr_get_ProductosBETA;

PROCEDURE pr_get_ProductosBETA2 (
    pPrefijo IN sifcos.t_ca_productos.n_producto%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
    IS
   
   v_prefijo  varchar2(100) := pPrefijo|| '%' ;
   
   BEGIN
     OPEN pResultado FOR
          SELECT p.id_producto idproducto
                ,p.n_producto nproducto
          FROM sifcos.t_ca_productos p
          WHERE  p.n_producto like v_prefijo
          order by p.n_producto;


END pr_get_ProductosBETA2;
--- edicion: Fernando Budassi --- fecha: 04/2016 modificado 09/2016
--- se cambia el nombre de rubros por actividad
PROCEDURE pr_get_Productos_Actividad (
    pIDs IN VARCHAR2,
    pResultado OUT sys_refcursor
   )
    IS
    
   BEGIN 
    OPEN pResultado FOR
        SELECT distinct a.id_actividad , a.n_actividad   
                FROM t_comunes.t_actividades a  
                JOIN sifcos.t_sif_productos_actividad pa on a.id_actividad = pa.id_actividad_clanae  
                JOIN sifcos.t_sif_productos p on pa.id_producto = p.id_producto  
                WHERE p.id_producto in (pIDs)
                ORDER BY a.n_actividad;
   
END pr_get_Productos_Actividad;

--- edicion: Fernando Budassi --- fecha: 15/03/2017 
--- consulta de productos y actividades cargadas recibiendo productos por parametro
PROCEDURE pr_get_Productos_y_Act (
      pProd IN varchar2,
      pResultado OUT sys_refcursor
   )
    IS
      v_SQL  varchar2(4000);
      v_SELECT varchar2(1000);
      v_FROM varchar2(1000);
      v_WHERE varchar2(1000);
   BEGIN 
     v_SELECT :=' SELECT p.id_producto,
                p.n_producto,
                a.id_actividad,
                a.n_actividad';
     v_FROM :=' FROM sifcos.t_sif_productos p
         JOIN sifcos.t_sif_productos_actividad pa on pa.id_producto=p.id_producto
         JOIN t_comunes.t_actividades a on a.id_actividad=pa.id_actividad_clanae 
                                       and a.fecha_inicio_act=pa.fecha_inicio_act ';
     v_WHERE:=' WHERE ROWNUM<=7000 ';
     if (pProd is not null) then
     v_WHERE :=v_WHERE ||' and p.n_producto like ''%%'||UPPER(pProd)||'%''';
     end if;
     v_SQL := v_SELECT || v_FROM || v_WHERE || ' order by p.n_producto';
    OPEN pResultado FOR
         v_SQL;
         
END pr_get_Productos_y_Act;

--- edicion: Fernando Budassi --- fecha: 15/03/2017 
--- consulta de productos y actividades cargadas recibiendo una act por parametro
PROCEDURE pr_get_Productos_y_Act_2 (
      pAct IN varchar2,
      pResultado OUT sys_refcursor
   )
    IS
      v_SQL  varchar2(4000);
      v_SELECT varchar2(1000);
      v_FROM varchar2(1000);
      v_WHERE varchar2(1000);
   BEGIN 
     v_SELECT :=' SELECT p.id_producto,
                p.n_producto,
                a.id_actividad,
                a.n_actividad';
     v_FROM :=' FROM sifcos.t_sif_productos p
         JOIN sifcos.t_sif_productos_actividad pa on pa.id_producto=p.id_producto
         JOIN t_comunes.t_actividades a on a.id_actividad=pa.id_actividad_clanae 
                                       and a.fecha_inicio_act=pa.fecha_inicio_act ';
     v_WHERE:=' WHERE ROWNUM<=7000 ';
     if (pAct is not null) then
     v_WHERE :=v_WHERE ||' and a.n_actividad like ''%%'||UPPER(pAct)||'%''';
     end if;
     v_SQL := v_SELECT || v_FROM || v_WHERE || ' order by p.n_producto';
    OPEN pResultado FOR
         v_SQL;
         
END pr_get_Productos_y_Act_2;
--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina bocaRecepcion.aspx.cs
--- solo estados de un tramite en boca de recepcion
PROCEDURE pr_get_estados_br (
     pResultado OUT sys_refcursor
   )
    IS

   BEGIN
     OPEN pResultado FOR
           SELECT e.id_estado_tramite,e.n_estado_tramite
           FROM sifcos.t_sif_estados_tramite e
           /*WHERE e.id_estado_tramite in (1,2,3,12)*/
           ORDER BY e.n_estado_tramite
           ;


END pr_get_estados_br;
--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina bocaRecepcion.aspx.cs
--- solo estados de un tramite en boca de ministerio
PROCEDURE pr_get_estados_bm (
     pResultado OUT sys_refcursor
   )
    IS

   BEGIN
     OPEN pResultado FOR
              SELECT e.id_estado_tramite,e.n_estado_tramite
           FROM sifcos.t_sif_estados_tramite e
           /*WHERE e.id_estado_tramite in (1,2,4,7,11,12,13)*/
           ORDER BY e.n_estado_tramite
           ;


END pr_get_estados_bm;
--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina bocaRecepcion.aspx.cs
--- utilizado en pagina misTramites.aspx.cs
PROCEDURE pr_get_HistEstados (
     pNroTramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%Type default null,
     pResultado OUT sys_refcursor
   )
    IS

   BEGIN
     OPEN pResultado FOR
           SELECT 
         e.n_estado_tramite estado
        ,h.descripcion
        ,h.fec_desde_estado ult_cambio
        ,h.cuil_usr_cidi usuario
        ,(SELECT distinct pj.razon_social 
          FROM sifcos.t_sif_usuarios_organismo uo
          JOIN sifcos.t_sif_organismos o on o.id_organismo=uo.id_organismo
          JOIN t_comunes.vt_pers_juridicas_completa pj on pj.cuit=o.cuit 
                                                      and pj.id_sede=o.id_sede
          WHERE uo.cuil_usr_cidi=h.cuil_usr_cidi) organismo
                  
           FROM sifcos.t_sif_estados_tramite e
           JOIN sifcos.t_sif_hist_estado h ON h.id_estado_tramite=e.id_estado_tramite
           JOIN sifcos.t_sif_tramites_sifcos t ON t.nro_tramite_sifcos=h.id_tramite_sifcos
           WHERE t.nro_tramite_sifcos=pNroTramite
           order by h.fec_desde_estado desc
           ;


END pr_get_HistEstados;
--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina Inscripcion.aspx.cs
PROCEDURE pr_get_Tipo_Cargo (
     pResultado OUT sys_refcursor
   )
    IS

   BEGIN
     OPEN pResultado FOR
           SELECT DISTINCT c.id_cargo ,c.n_cargo cargo
           FROM sifcos.t_sif_tipos_cargo c
           ;


END pr_get_Tipo_Cargo;
--- edicion: Fernando Budassi --- fecha: 07/2016
--- utilizado en pagina Inscripcion.aspx.cs
PROCEDURE pr_get_Tipo_Gestor (
     pResultado OUT sys_refcursor
   )
    IS

   BEGIN
     OPEN pResultado FOR
           SELECT DISTINCT g.id_tipo_gestor,g.n_tipo_gestor gestor
           FROM sifcos.t_sif_tipos_gestor g

           ;


END pr_get_Tipo_Gestor;
--- edicion: Fernando Budassi --- fecha: 06/2016
PROCEDURE pr_get_IDProducto (
    pResultado OUT sys_refcursor
   )
    IS

   BEGIN
     OPEN pResultado FOR
          SELECT max(p.id_producto)+1 id_producto_max
          FROM sifcos.t_sif_productos p  ;

END pr_get_IDProducto;
--- edicion: Fernando Budassi --- fecha: 06/2016
PROCEDURE pr_get_IDSuperficie (
    pResultado OUT sys_refcursor
   )
    IS

   BEGIN
     OPEN pResultado FOR
          SELECT max(s.id_tipo_superficie)+1 id_superficie_max
          FROM sifcos.t_sif_tipos_superficie s  ;

END pr_get_IDSuperficie;
--- edicion: Fernando Budassi --- fecha: 05/2016
--- utilizado en todas las paginas para buscar las provincias
PROCEDURE pr_get_provincias (
    pProvincias  OUT   sys_refcursor
   )
   IS
   BEGIN
      OPEN pProvincias FOR
         SELECT p.id_provincia,p.n_provincia
         FROM dom_manager.dom_provincias p
         WHERE p.id_pais='ARG' OR p.id_provincia=0
         ORDER BY  p.n_provincia;

END pr_get_provincias;
--- edicion: Fernando Budassi --- fecha: 05/2016
--- utilizado en todas las paginas para buscar los departamentos de cada provincia
PROCEDURE pr_get_departamentos (
    pIdProvincia IN dom_manager.dom_provincias.id_provincia%TYPE DEFAULT NULL,
    pDepartamentos  OUT   sys_refcursor
   )
   IS
   BEGIN
      OPEN pDepartamentos FOR
         SELECT d.id_departamento,d.n_departamento
         FROM dom_manager.dom_departamentos d
         WHERE  d.id_provincia=pIdProvincia or d.id_departamento=0
         ORDER BY  d.n_Departamento;


END pr_get_departamentos;
--- edicion: Fernando Budassi --- fecha: 05/2016
--- utilizado en todas las paginas para buscar las localidades de cada depto
PROCEDURE pr_get_localidades (
    pIdDepto IN dom_manager.dom_departamentos.id_departamento%TYPE DEFAULT NULL,
    pLocalidades  OUT   sys_refcursor
   )
   IS
   v_SQL  varchar2(3000);

   BEGIN
        if(pIdDepto=1) then
           v_SQL:='SELECT l.id_localidad,l.n_localidad
                   FROM dom_manager.dom_localidades l
                   WHERE l.id_localidad = 0 OR  l.id_departamento='||pIdDepto||
                   ' and l.oficial=''S''
                   ORDER BY l.n_localidad';
          else
            v_SQL:='SELECT l.id_localidad,l.n_localidad
                    FROM dom_manager.dom_localidades l
                    WHERE l.id_localidad = 0 OR  l.id_departamento='||pIdDepto||
                    ' ORDER BY l.n_localidad';
          end if;
      
      OPEN pLocalidades FOR
       v_SQL;
      
END pr_get_localidades;
--- edicion: Fernando Budassi --- fecha: 05/2016
--- utilizado en todas las paginas para buscar los barrios de cada localidad
PROCEDURE pr_get_barrios (
    pIdLocalidad IN dom_manager.dom_localidades.id_localidad%TYPE DEFAULT NULL,
    pBarrios  OUT   sys_refcursor
   )
   IS
   BEGIN
      OPEN pBarrios FOR
         SELECT  b.id_barrio,b.n_barrio
         FROM  dom_manager.dom_barrios b
         WHERE  b.id_barrio=0 OR ( b.id_localidad=pIdLocalidad)
         order by b.n_barrio;

END pr_get_barrios;
--- edicion: Fernando Budassi --- fecha: 08/2016 --- modificado 06/02/2017
--- utilizado en todas las paginas para buscar las calles de cada localidad,depto y provincia
--- devuelve las calles que han sido cargadas para esa localidad pasando un prefijo
PROCEDURE pr_get_calles (
    pIdProvincia IN dom_manager.dom_provincias.id_provincia%TYPE DEFAULT NULL,
    pIdDepto IN dom_manager.dom_departamentos.id_departamento%TYPE DEFAULT NULL,
    pIdLocalidad IN dom_manager.dom_localidades.id_localidad%TYPE DEFAULT NULL,
    pPrefijo IN VARCHAR2,
    pCalles  OUT   sys_refcursor
   )
   IS
   v_prefijo  varchar2(100) := pPrefijo|| '%' ;
   BEGIN
      OPEN pCalles FOR
         SELECT distinct
                c.id_calle,
                c.n_calle
         FROM dom_manager.dom_calles c
         JOIN dom_manager.dom_barrios b on b.id_localidad=c.id_localidad
         WHERE upper(c.n_calle) like upper(v_prefijo)
           and c.id_provincia=pIdProvincia
           and c.id_departamento=pIdDepto
           and c.id_localidad=pIdLocalidad
         ORDER BY c.n_calle;

END pr_get_calles;
--- edicion: Fernando Budassi --- fecha: 06/2016 
--- utilizado para obtener todos los datos de la carga inicial del tramite de cada sucursal
--- de una entidad
PROCEDURE pr_get_sedes (
    pCuit IN t_comunes.t_sedes_persjuridica.cuit%TYPE DEFAULT NULL,
    pSedes  OUT   sys_refcursor
   )
   IS
  /* v_pCuitSede  varchar2(20) := pCuit || pSedes;
   v_pCuit varchar2(20) := pCuit ||'%';
   v_Filtro varchar2(20);*/
BEGIN
  /*IF pIDSede IS NOT NULL THEN
     v_Filtro:= v_pCuitSede;
   ELSE
     v_Filtro:= v_pCuit;
   END IF;
  */


   BEGIN
      OPEN pSedes FOR
            SELECT distinct s.id_sede,
            s.id_sede||' - '||s.n_sede||' - '||d.N_CALLE||' '||d.altura||', '||d.n_localidad sedes,
            d.id_vin,
            d.id_entidad,
            d.id_tipodom,
            d.n_tipodom,
            d.id_dom_norm,
            d.id_provincia,
            d.n_provincia,
            d.id_departamento,
            d.n_departamento,
            d.id_localidad,
            d.n_localidad,
            d.id_barrio,
            d.n_barrio,
            d.id_tipocalle,
            d.n_tipocalle,
            d.id_calle,
            d.n_calle,
            d.altura,
            d.piso,
            d.depto,
            d.torre,
            d.MZNA,
            d.LOTE,
            (select distinct c.cpa from dom_manager.vt_localidades_cpa c
             where c.id_localidad=d.id_localidad) cpa
            /*(SELECT com.cod_area1
             FROM sifcos.vt_com_sifcos com
             WHERE c.id_entidad LIKE v_Filtro)*/


            FROM dom_manager.vt_domicilios_todo_mzna_lote d
            join t_comunes.t_sedes_persjuridica s on d.id_vin=s.id_vin
            WHERE s.cuit=pCuit
            and (select c.cpa from dom_manager.vt_localidades_cpa c
             where c.id_localidad=d.id_localidad) is not null
            ORDER BY sedes;

   END;

   END pr_get_sedes;
--- edicion: Fernando Budassi --- fecha: 07/2016 
/*PROCEDURE pr_get_id_transaccion (
    pNroTramite IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado  OUT varchar2
   )
   IS
   v_id_transaccion_tasa varchar2(50):='0';

  BEGIN

   SELECT t.idtrasacciontasa
   INTO v_id_transaccion_tasa
   FROM sifcos.t_sif_tramites_sifcos t
   where t.nro_tramite_sifcos=pNroTramite;

  IF v_id_transaccion_tasa IS NOT NULL THEN
      pResultado:=v_id_transaccion_tasa;
      return;
  ELSE pResultado:='Tramite no encontrado';
       return;
  END IF;


END pr_get_id_transaccion;*/
--- edicion: Fernando Budassi --- fecha: 08/2016
--- consulta de tasas abonadas
/*PROCEDURE pr_get_pago_TRS (
    pId_transaccionTasa IN sifcos.t_sif_tramites_sifcos.idtrasacciontasa%TYPE DEFAULT NULL,
    pResultado  OUT varchar2
   )
   IS

  BEGIN

   SELECT t.pagado
   INTO pResultado
   FROM tasas_servicio.vt_transacciones_micm t
   where t.obligacion=pId_transaccionTasa;
  return;

END pr_get_pago_TRS;*/
--- edicion: Fernando Budassi --- fecha: 08/2016 
--- consulta de tramites que tienen sus tasas pagas
/*PROCEDURE pr_get_entidades_deuda (
    PNroTramite  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT varchar2
   )
   
   IS
   v_adeuda varchar2(10);
   BEGIN
     SELECT COUNT(*) 
              \*t.nro_tramite_sifcos nro_tramite,
                e.nro_sifcos,
                e.cuit,
                tc_pj.razon_social*\
         INTO v_adeuda       
         FROM sifcos.t_sif_tramites_sifcos t
         JOIN tasas_servicio.vt_transacciones_micm trs ON trs.obligacion=t.idtrasacciontasa
         JOIN sifcos.t_sif_entidades e ON e.id_entidad = t.id_entidad
         JOIN t_comunes.vt_pers_juridicas_completa tc_pj on tc_pj.cuit = e.cuit
                                                        and tc_pj.id_sede = e.id_sede
         WHERE (t.nro_tramite_sifcos = PNroTramite OR PNroTramite is null)
              AND trs.pagado='N';
      
    IF v_adeuda = '0' THEN
       pResultado:= 'NO DEBE';
    ELSE pResultado:= 'ADEUDA';
    END IF;
      
    return;

END pr_get_entidades_deuda;*/
--- edicion: Mariano Ceneri --- fecha: 09/2016 
--- consulta de codigo postales por id departamento
PROCEDURE pr_get_Codigo_postal (
    pIdDepartamento  IN dom_manager.dom_departamentos.id_departamento%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
      select cp.id_localidad, cp.cpa from dom_manager.vt_localidades_cpa cp
      where cp.id_departamento = pIdDepartamento;


END pr_get_Codigo_postal;

--- edicion: Fernando Budassi --- fecha: 10/2016 
--- consulta las sedes existentes en una empresa
PROCEDURE pr_get_ultima_sede (
    pCuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT  max(pj.id_sede) ultima_sede
         FROM t_comunes.vt_pers_juridicas_completa pj
         WHERE pj.cuit=pCuit;

END pr_get_ultima_sede;

--- edicion: Fernando Budassi --- fecha: 05/2017
--- tasas sin usar para reempadronamiento
---IB Adaptado al nuevo esquema TRS
PROCEDURE pr_Get_TasasSinUsar (
    Pcuit in varchar2,
    pResultado  OUT   sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
          /*parametros : cuit , id_concepto_reempadronamiento y de _ID_CONCEPTO_ALTA*/
                 SELECT   t.obligacion nro_transaccion, t.nroliquidacionoriginal , t.fecha_vencimiento , '0' anio_fiscal ,t.fecha_emision,t.fecha_cobro ,  
                'Tasa de Reempadronamiento - ' || ID_SIF_HIST_CONCEPTOS ||  'Nro Liquidacion: '||t.nroliquidacionoriginal || ' - Fecha de pago:'|| t.fecha_cobro comboTRS   
                FROM (  select obligacion, nroliquidacionoriginal , fecha_vencimiento, fecha_emision,fecha_cobro, CUIT, codigo, pagado, HASH_TRX, ID_TRANSACCION, ID_CONCEPTO  FROM TRS.VT_TRANSACCIONES_VERTICALES 
                        union 
                        select  obligacion, nroliquidacionoriginal , fecha_vencimiento, fecha_emision,fecha_cobro, CUIT, codigo, pagado, HASH_TRX, ID_TRANSACCION, ID_CONCEPTO     FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) t
                JOIN sifcos.T_SIF_HIST_CONCEPTOS hc ON hc.ID_SIF_HIST_CONCEPTOS = t.ID_CONCEPTO AND hc.ID_TIPO_CONCEPTO=4
               WHERE t.CUIT = Pcuit 
                 and t.CODIGO = '057'  
                 and t.pagado = 'S'   
                 and t.obligacion   
                   not in (   
                           select tas.nro_transaccion   
                            from sifcos.t_sif_tasas tas  
                            where tas.pagada = 'S' 
                          )   
                  
               and NOT EXISTS (   
                           select *
                            from sifcos.t_sif_tasas tas  
                            where tas.pagada = 'S' and tas.nro_transaccion = t.obligacion
                          )   
                      /*valido que la tasa no est? usanda en el sifcos viejo.*/
              and  NOT EXISTS ( select * 
                                from sifcos.t_pagos_trs tas2 
                                where tas2.nro_referencia = t.nroliquidacionoriginal
                                  and tas2.id_tramite is not null )
                   /*valisod que la tasa no se use en eSIFCoS*/
              and not exists (
                    select *
                    FROM sifcos.T_ESIF_TASAS etas
                    WHERE etas.nro_transaccion = t.obligacion
                )                                                       
              ;
END pr_Get_TasasSinUsar;

--- edicion: Fernando Budassi --- fecha: 05/2017
--- tasas sin usar de alta
--- IB: Adaptado al nuevo esquema de TRS
PROCEDURE pr_Get_TasasSinUsarAlta (
    Pcuit in varchar2,
    pResultado  OUT   sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR


          /*parametros : cuit , id_concepto_reempadronamiento y de _ID_CONCEPTO_ALTA*/
                 SELECT   t.obligacion nro_transaccion, t.nroliquidacionoriginal , t.fecha_vencimiento , '0' anio_fiscal ,t.fecha_emision,t.fecha_cobro ,  

                'Tasa de Alta - ' || ID_SIF_HIST_CONCEPTOS ||  'Nro Liquidacion: '||t.nroliquidacionoriginal || ' - Fecha de pago:'|| t.fecha_cobro comboTRS   
                FROM (  select obligacion, nroliquidacionoriginal , fecha_vencimiento, fecha_emision,fecha_cobro, CUIT, codigo, pagado, HASH_TRX, ID_TRANSACCION, ID_CONCEPTO  FROM TRS.VT_TRANSACCIONES_VERTICALES 
                        union 
                        select  obligacion, nroliquidacionoriginal , fecha_vencimiento, fecha_emision,fecha_cobro, CUIT, codigo, pagado, HASH_TRX, ID_TRANSACCION, ID_CONCEPTO     FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) t
                JOIN T_SIF_HIST_CONCEPTOS hc
                ON hc.ID_SIF_HIST_CONCEPTOS = t.ID_CONCEPTO
                AND hc.ID_TIPO_CONCEPTO=1
               WHERE t.CUIT = Pcuit
 
                 and t.CODIGO = '057'  
                 and t.pagado = 'S'
                -- tomo solo las tasas de que se vencen en los ultimos 15 d?as.  
                --and t.fecha_vencimiento > (SYSDATE - 15 )  
                 and t.obligacion   
                   not in (   
                           select tas.nro_transaccion   
                            from sifcos.t_sif_tasas tas  
                            where tas.pagada = 'S' 
                          )   
                  
               and NOT EXISTS (   
                           select *
                            from sifcos.t_sif_tasas tas  
                            where tas.pagada = 'S' and tas.nro_transaccion = t.obligacion
                          )   
                      /*valido que la tasa no est? usanda en el sifcos viejo.*/
              and  NOT EXISTS ( select * 
                                from sifcos.t_pagos_trs tas2 
                                where tas2.nro_referencia = t.nroliquidacionoriginal
                                  and tas2.id_tramite is not null ) 
               and not exists (
                    select *
                    FROM sifcos.T_ESIF_TASAS etas
                    WHERE etas.nro_transaccion = t.obligacion
                )                                    
              ;
END pr_Get_TasasSinUsarAlta;

PROCEDURE pr_Get_Tasas_by_cuit (
    Pcuit in varchar2,
    pResultado  OUT   sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR

                 SELECT   t.obligacion, 
          t.nroliquidacionoriginal , 
          t.cuit,
          t.n_concepto,
          t.fecha_vencimiento,
          t.fecha_cobro , 
          t.Importe_Total,
          t.pagado,
          t.ente_recaudador
FROM (  select obligacion, nroliquidacionoriginal , fecha_vencimiento, fecha_emision,fecha_cobro, CUIT, codigo, pagado, HASH_TRX, ID_TRANSACCION, N_CONCEPTO, ID_CONCEPTO,IMPORTE_TOTAL , ENTE_RECAUDADOR  FROM TRS.VT_TRANSACCIONES_VERTICALES 
        union 
        select  obligacion, nroliquidacionoriginal , fecha_vencimiento, fecha_emision,fecha_cobro, CUIT, codigo, pagado, HASH_TRX, ID_TRANSACCION, N_CONCEPTO ,ID_CONCEPTO, IMPORTE_TOTAL  , ENTE_RECAUDADOR   FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) t
        JOIN SIFCOS.T_SIF_HIST_CONCEPTOS hc ON hc.ID_SIF_HIST_CONCEPTOS = t.ID_CONCEPTO
WHERE t.CUIT = Pcuit
      and t.CODIGO = '057'  
      and t.pagado = 'S';
      
END pr_Get_Tasas_by_cuit;

PROCEDURE pr_get_tasas_Viejo_Sif_by_ref (
    PNroReferencia in varchar2,
    pResultado  OUT   sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR

                SELECT * from sifcos.t_pagos_trs t
                WHERE t.NRO_REFERENCIA = PNroReferencia;
      
END pr_get_tasas_Viejo_Sif_by_ref;

PROCEDURE pr_get_tasas_Nuevo_Sif_by_ref (
    PNroReferencia in varchar2,
    pResultado  OUT   sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR

      select tas.* 
      from (  select obligacion,nroliquidacionoriginal,numeroreferencia,id_transaccion, 
           id_concepto, n_concepto, cuit , fecha_emision, fecha_vencimiento, fecha_cobro, fecha_rendicion, importe_total, n_ente, pagado, ente_recaudador   
           FROM TRS.VT_TRANSACCIONES_VERTICALES 
           union 
           select  obligacion,nroliquidacionoriginal,numeroreferencia, id_transaccion, 
           id_concepto,n_concepto, cuit , fecha_emision, fecha_vencimiento, fecha_cobro, fecha_rendicion, importe_total, n_ente, pagado, ente_recaudador
           FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) vt 
           join sifcos.t_sif_tasas tas on vt.obligacion  = tas.nro_transaccion 
           WHERE vt.nroliquidacionoriginal = PNroReferencia ;
      
END pr_get_tasas_Nuevo_Sif_by_ref;

PROCEDURE pr_get_duplicados (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(5000);
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
 
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
  
 v_SELECT :='SELECT 
              e.cuit,
              decode(t.id_tipo_tramite,4,''REEMPADRONAMIENTO'',''ALTA'') TIPO_TRAMITE,
              count(*) cantidad '; 
  v_FROM:=' FROM sifcos.t_sif_tramites_sifcos t
            JOIN sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad  ';
               
          
v_WHERE:=' WHERE  (TO_DATE(t.fec_alta, ''dd/mm/yyyy'') >= TO_DATE('''||P_FECHA_DESDE||''', ''dd/mm/yyyy'') OR '''||P_FECHA_DESDE||''' IS NULL)
           AND (TO_DATE(t.fec_alta, ''dd/mm/yyyy'') <= TO_DATE('''||P_FECHA_HASTA||''', ''dd/mm/yyyy'') OR '''||P_FECHA_HASTA||''' IS NULL)';
           
           
V_GROUP_BY:=' having count(*)>1 group by e.cuit,t.id_tipo_tramite order by cantidad desc ';
  
v_SQL:=v_SELECT || v_FROM ||v_WHERE || V_GROUP_BY ;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_duplicados;

PROCEDURE pr_get_tramite_existe (
  pId_vin_dom_legal IN varchar2,
  pId_vin_dom_local IN varchar2,
  p_fecha_alta IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
 
OPEN pResultado FOR 
 SELECT  *
 FROM sifcos.t_sif_tramites_sifcos t 
 WHERE  t.id_vin_dom_legal=pId_vin_dom_legal 
    and t.id_vin_dom_local=pId_vin_dom_local 
    and to_date(t.fec_alta,'dd/mm/yyyy') = to_date(p_fecha_alta,'dd/mm/yyyy'); 
    

END pr_get_tramite_existe;


PROCEDURE pr_get_tramite_existe_comercio (
 
  pId_vin_dom_local IN varchar2,
  p_fecha_alta IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
 
OPEN pResultado FOR 
 SELECT  *
 FROM sifcos.t_ca_tramites t 
 WHERE t.id_vin_dom_local=pId_vin_dom_local 
    and to_date(t.fec_alta,'dd/mm/yyyy') = to_date(p_fecha_alta,'dd/mm/yyyy'); 
    

END pr_get_tramite_existe_comercio;



--- edicion: Fernando Budassi --- fecha: 10/03/2017
--- consulta el ultimo tramite de un nro_sifcos cargado en el nuevo sistema
PROCEDURE pr_get_ultimo_tramite (
    pNroSifcos  IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT * 
         FROM sifcos.t_sif_tramites_sifcos t
         JOIN sifcos.t_sif_entidades e on t.id_entidad = e.id_entidad
         WHERE e.nro_sifcos = pNroSifcos and
         t.fec_alta = (
         SELECT max(tram.fec_alta) 
         FROM sifcos.t_sif_tramites_sifcos tram 
         JOIN sifcos.t_sif_entidades enti on tram.id_entidad = enti.id_entidad 
         WHERE enti.nro_sifcos = pNroSifcos);

END pr_get_ultimo_tramite;

--- edicion: Fernando Budassi --- fecha: 27/06/2017
--- consulta la ultima fecha de vencimiento de un nro_sifcos cargado en el nuevo sistema
PROCEDURE pr_get_fec_ult_tramite (
    pNroSifcos  IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT max(t.fec_vencimiento) FEC_ULTIMO_VENCIMIENTO
         FROM sifcos.t_sif_tramites_sifcos t 
         JOIN sifcos.t_sif_entidades e on t.id_entidad = e.id_entidad 
         WHERE e.nro_sifcos =  pNroSifcos
         and t.id_tipo_tramite not in (5,6,7,8,9,10);

END pr_get_fec_ult_tramite;

--- edicion: Fernando Budassi --- fecha: 27/06/2017
--- consulta la ultima fecha de vencimiento de un nro_sifcos cargado en el viejo sistema
PROCEDURE pr_get_fec_ult_tramite_viejo (
    pNroSifcos  IN sifcos.t_tramites.nro_recsep%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
         SELECT max(t1.fecha_presentacion)  FEC_ULTIMO_VENCIMIENTO 
         FROM sifcos.t_tramites t1 
         JOIN sifcos.t_entidades e1 on t1.id_entidad=e1.id_entidad 
         WHERE t1.nro_recsep =  pNroSifcos;
         

END pr_get_fec_ult_tramite_viejo;

--- edicion: Fernando Budassi --- fecha: 16/05/2017
--- consulta las sedes existentes por numero de cuit y opcionalmente nro de sifcos
PROCEDURE pr_get_direcciones_sedes (
    P_Cuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    P_Nro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   v_SQL  varchar2(5000);
   v_SELECT varchar2(1000);
   v_SELECT_2 varchar2(1000);
   v_FROM varchar2(1000);
   v_FROM_2 varchar2(1000);
   v_WHERE varchar2(1000);
   v_WHERE_2 varchar2(1000);
   v_GROUP_BY varchar2(1000);
   v_GROUP_BY_2 varchar2(1000);
   BEGIN
         v_SELECT :='SELECT MAX(t.id_tramite) nro_tramite,
                     t.nro_recsep nro_sifcos,
	                   vt.N_DEPARTAMENTO||'' - ''||vt.N_LOCALIDAD||'' - ''||vt.N_BARRIO||'' - ''||vt.N_CALLE||'' ''||vt.altura domicilio ';

         v_FROM:='FROM sifcos.t_tramites t
               	  join sifcos.t_entidades e on e.id_entidad=t.id_entidad
	                join sifcos.t_domicilios_tramite dt on dt.id_tramite=t.id_tramite
	                join dom_manager.vt_domicilios vt on dt.id_vin=vt.id_vin ';

         v_WHERE:='WHERE not exists (select * from sifcos.t_tramites t1
                                     WHERE t1.id_tipo_tramite=2
                                     and t1.nro_recsep=t.nro_recsep) and
                         not exists (select * from sifcos.t_sif_tramites_sifcos tr1
                                     join sifcos.t_sif_entidades ent1 on ent1.id_entidad=tr1.id_entidad
                                     WHERE tr1.id_tipo_tramite=2
                                     and t.nro_recsep=ent1.nro_sifcos
                                     ) and 
                    e.cuit_pers_juridica='''||P_Cuit||''' and
                    dt.id_tipodom=3 and
                    not exists (select * from sifcos.t_sif_tramites_sifcos t1
                                join sifcos.t_sif_entidades e1 on e1.id_entidad=t1.id_entidad
                                where e1.nro_sifcos=t.nro_recsep) ';

         if(P_Nro_sifcos is not null) then
         v_WHERE:= v_WHERE ||' and t.nro_recsep ='||  P_Nro_sifcos ||' ';
         end if;

         v_GROUP_BY :=' GROUP BY t.nro_recsep,vt.N_DEPARTAMENTO,vt.N_LOCALIDAD,vt.N_BARRIO,vt.N_CALLE,vt.altura';

         v_SELECT_2 :=' SELECT max(tr.nro_tramite_sifcos) nro_tramite,
                        to_char(ent.nro_sifcos),
	                     vt.N_DEPARTAMENTO||'' - ''||vt.N_LOCALIDAD||'' - ''||vt.N_BARRIO||'' - ''||vt.N_CALLE||'' ''||vt.altura domicilio ';

         v_FROM_2:=' FROM sifcos.t_sif_tramites_sifcos tr
	                   join sifcos.t_sif_entidades ent on ent.id_entidad=tr.id_entidad
	                   join dom_manager.vt_domicilios vt on tr.id_vin_dom_local=vt.id_vin ';

          v_WHERE_2:='WHERE not exists (SELECT * FROM sifcos.t_sif_tramites_sifcos tr1
                                        JOIN sifcos.t_sif_entidades ent1 on ent1.id_entidad=tr1.id_entidad
                                        WHERE tr1.id_tipo_tramite=2
                                        and ent1.nro_sifcos=ent.nro_sifcos
                                        ) and ent.nro_sifcos<>0 and 
                            ent.cuit=' ||P_Cuit;

          if(P_Nro_sifcos is not null) then
          v_WHERE_2:= v_WHERE_2 ||' and ent.nro_sifcos ='||  P_Nro_sifcos ||' ';
          end if;

          v_GROUP_BY_2 :=' GROUP BY ent.nro_sifcos,vt.N_DEPARTAMENTO,vt.N_LOCALIDAD,vt.N_BARRIO,vt.N_CALLE,vt.altura';

v_SQL:=v_SELECT || v_FROM ||v_WHERE || v_GROUP_BY || ' UNION ' || v_SELECT_2 || v_FROM_2 ||v_WHERE_2 || v_GROUP_BY_2;

OPEN pResultado FOR
v_SQL;

END pr_get_direcciones_sedes;

--- edicion: Fernando Budassi --- fecha: 05/09/2017
--- consulta de todos los tramites existentes por numero de cuit
--- me muestra nro_tramite,nro_sifcos,boca_recepcion,fecha_vto,domicilio 
PROCEDURE pr_get_tram_sifcos (
    P_Cuit  IN sifcos.t_sif_entidades.cuit%TYPE DEFAULT NULL,
    P_Nro_sifcos IN sifcos.t_sif_entidades.nro_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   v_SQL  varchar2(5000);
   v_SELECT varchar2(1000);
   v_SELECT_2 varchar2(1000);
   v_FROM varchar2(1000);
   v_FROM_2 varchar2(1000);
   v_WHERE varchar2(1000);
   v_WHERE_2 varchar2(1000);
   
   BEGIN
         v_SELECT :='SELECT t.id_tramite nro_tramite,
                     e.cuit_pers_juridica cuit,
                     t.nro_recsep nro_sifcos,
                     (select pj.razon_social
                      from sifcos.t_tramites t1 
                      join sifcos.t_organismos o on o.id_organismo=t1.id_organismo_alta
                      join t_comunes.vt_pers_juridicas_completa pj on pj.cuit=o.cuit and pj.id_sede=o.id_sede
                      where t1.id_tipo_tramite=1
                      and t1.id_organismo_alta is not null
                      and t1.id_tramite=t.id_tramite)Boca_recepcion,
                     t.fecha_presentacion+365 fec_vencimiento,
                     vt.N_DEPARTAMENTO||'' - ''||vt.N_LOCALIDAD||'' - ''||vt.N_BARRIO||'' - ''||vt.N_CALLE||'' ''||vt.altura domicilio '
                     ;

         v_FROM:=' FROM sifcos.t_tramites t
               	  join sifcos.t_entidades e on e.id_entidad=t.id_entidad
	                join sifcos.t_domicilios_tramite dt on dt.id_tramite=t.id_tramite
	                join dom_manager.vt_domicilios vt on dt.id_vin=vt.id_vin ';

         v_WHERE:=' WHERE not exists (select * from sifcos.t_tramites t1
                                     WHERE t1.id_tipo_tramite=2
                                     and t1.nro_recsep=t.nro_recsep) and
                         not exists (select * from sifcos.t_sif_tramites_sifcos tr1
                                     join sifcos.t_sif_entidades ent1 on ent1.id_entidad=tr1.id_entidad
                                     WHERE tr1.id_tipo_tramite=2
                                     and t.nro_recsep=ent1.nro_sifcos
                                     ) and 
                    dt.id_tipodom=3 and
                    not exists (select * from sifcos.t_sif_tramites_sifcos t1
                                join sifcos.t_sif_entidades e1 on e1.id_entidad=t1.id_entidad
                                where e1.nro_sifcos=t.nro_recsep) ';
                                
         if(P_Cuit <> '0') then
         v_WHERE:= v_WHERE ||' and e.cuit_pers_juridica='''||P_Cuit||''' ';
         end if;                                

         if(P_Nro_sifcos is not null) then
         v_WHERE:= v_WHERE ||' and t.nro_recsep ='''||  P_Nro_sifcos ||''' ';
         end if;

         v_SELECT_2 :=' SELECT tr.nro_tramite_sifcos nro_tramite,
                        ent.cuit,
                        to_char(ent.nro_sifcos),
                        (select pj.razon_social
                         from sifcos.t_sif_tramites_sifcos tr1 
                         join sifcos.t_organismos o on o.id_organismo=tr1.id_organismo_alta
                         join t_comunes.vt_pers_juridicas_completa pj on pj.cuit=o.cuit and pj.id_sede=o.id_sede
                         where tr1.id_tipo_tramite=1
                         and tr1.id_organismo_alta is not null
                         and tr1.nro_tramite_sifcos=tr.nro_tramite_sifcos)Boca_recepcion,
                        tr.fec_vencimiento,
                        vt.N_DEPARTAMENTO||'' - ''||vt.N_LOCALIDAD||'' - ''||vt.N_BARRIO||'' - ''||vt.N_CALLE||'' ''||vt.altura domicilio ';

         v_FROM_2:=' FROM sifcos.t_sif_tramites_sifcos tr
	                   join sifcos.t_sif_entidades ent on ent.id_entidad=tr.id_entidad
	                   join dom_manager.vt_domicilios vt on tr.id_vin_dom_local=vt.id_vin ';

          v_WHERE_2:=' WHERE not exists (SELECT * FROM sifcos.t_sif_tramites_sifcos tr1
                                        JOIN sifcos.t_sif_entidades ent1 on ent1.id_entidad=tr1.id_entidad
                                        WHERE tr1.id_tipo_tramite=2
                                        and ent1.nro_sifcos=ent.nro_sifcos
                                        ) ';
                            
                    
         if(P_Cuit <> '0') then
         v_WHERE_2:= v_WHERE_2 ||' and ent.cuit= '''||P_Cuit||''' ';
         end if;                                

          if(P_Nro_sifcos is not null) then
          v_WHERE_2:= v_WHERE_2 ||' and ent.nro_sifcos ='||  P_Nro_sifcos ||' ';
          end if;

          

v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' UNION ' || v_SELECT_2 || v_FROM_2 ||v_WHERE_2 || 'order by 3';

OPEN pResultado FOR
v_SQL;

END pr_get_tram_sifcos;

-- Devuelva las coordenadas de una ubicacion en el mapa de un comercio
-- desarrollado por Fernando Budassi 23-08-2017
PROCEDURE pr_get_ubicacion_mapa (
    pNroTramite  IN sifcos.t_sif_tramites_sifcos.nro_tramite_sifcos%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
        SELECT e.id_entidad,
               e.latitud_ubi latitud,
               e.longitud_ubi longitud,
               e.cuit
        FROM sifcos.t_sif_entidades e
        join sifcos.t_sif_tramites_sifcos t on t.id_entidad=e.id_entidad
        WHERE t.nro_tramite_sifcos=pNroTramite
        and e.nro_sifcos<>0;

END pr_get_ubicacion_mapa;


--- edicion: Facundo Alvarez --- fecha: 14/03/2017
--- devuelve los campos de un gestor obtenido por el id_gestor
PROCEDURE pr_get_Gestor (
    pIdGestor  IN sifcos.t_sif_gestores.id_gestor%TYPE DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
        SELECT g.id_gestor , rc.cuil , rc.NOV_APELLIDO , rc.NOV_NOMBRE , rc.ID_SEXO
        FROM sifcos.t_sif_gestores g
        JOIN rcivil.vt_pk_persona rc on rc.ID_SEXO=g.id_sexo 
                                    and rc.NRO_DOCUMENTO=g.nro_documento 
                                    and rc.PAI_COD_PAIS=g.pai_cod_pais 
                                    and rc.ID_NUMERO=g.id_numero
        WHERE g.id_gestor=pIdGestor;

END pr_get_Gestor;

--- Autor (IB) --- fecha: 03/2018
--- Utilizado en geolocalizacion.aspx

PROCEDURE pr_get_Rubros (
    pPrefijo IN varchar2,
    pResultado OUT sys_refcursor
   )
    IS
   
   
   
   BEGIN
     OPEN pResultado FOR
          SELECT r.id_Rubro idRubro
                ,r.n_Rubro nRubro
          FROM sifcos.t_sif_Rubros r
          WHERE r.n_Rubro like '%' || pPrefijo || '%'
          order by r.n_Rubro;


END pr_get_Rubros;


PROCEDURE pr_get_Rubros2 (
   
    pResultado OUT sys_refcursor
   )
    IS
   
   
   
   BEGIN
     OPEN pResultado FOR
          SELECT r.id_Rubro 
                ,r.n_Rubro 
          FROM sifcos.t_sif_Rubros r
          where r.id_rubro in (2,48,110,78,75,137,109,108,43,74,120,81,58,57,76,135,88,34,89,131,49,86,44,85,122,1, --aliemntos y bebidas
                               79,91,17,94,24,134,60,97,80,69,  -- higiene y salud
                               111,90,129,7,55,15,100,47,136,133,45,101,18,22,63,41,53,99,116,71,25,31,113,13) -- Ferreteria
       
          order by r.n_Rubro;


END pr_get_Rubros2;

PROCEDURE pr_consulta_comercios_sifcos (
  P_RESULTADO OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
                                                                                          
BEGIN

    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

OPEN P_RESULTADO FOR
      SELECT DISTINCT     E.CUIT, 
                          E.NRO_SIFCOS, 
                          PE.RAZON_SOCIAL, 
                          T.FEC_VENCIMIENTO,
                          T.NRO_TRAMITE_SIFCOS,
                          T.FEC_INI_TRAMITE,
                          T.FEC_ALTA,
                          D.N_CALLE, d.ALTURA, D.PISO, d.DEPTO, d.TORRE, d.MZNA, d.LOTE, d.N_LOCALIDAD, d.cpa,
                          po.CUIT CUIT_BOCA, po.RAZON_SOCIAL BOCA , 
                          tt.N_TIPO_TRAMITE,
                          apri.N_ACTIVIDAD AS ACTIVIDAD_PRI, 
                          asec.N_ACTIVIDAD AS ACTIVIDAD_SEC
      FROM T_SIF_TRAMITES_SIFCOS t 
      JOIN T_SIF_TIPOS_TRAMITE tt ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
      LEFT JOIN T_SIF_ENTIDADES E ON E.ID_ENTIDAD = T.ID_ENTIDAD
      LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS pe ON e.CUIT = pe.CUIT AND E.ID_SEDE=pe.ID_SEDE
      --Domicilio
      LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
      --Boca TRAMITE
      LEFT JOIN T_SIF_ORGANISMOS o ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
      LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po ON o.CUIT = po.CUIT AND o.ID_SEDE= po.ID_SEDE
      --ACTIVIDADES
      LEFT JOIN  t_comunes.VT_ACTIVIDADES apri ON apri.id_actividad=t.id_actividad_ppal
      LEFT JOIN  t_comunes.VT_ACTIVIDADES asec ON asec.id_actividad=t.id_actividad_ppal
      WHERE T.ID_TIPO_TRAMITE IN (1,4) 
      AND  E.NRO_SIFCOS <>0
      AND T.FEC_VENCIMIENTO > SYSDATE;
    

END pr_consulta_comercios_sifcos;

--- Autor (IB) --- fecha: 03/2018
--- Utilizado en BocaMinisterio.aspx

PROCEDURE pr_get_RubrosActividad (
    pResultado OUT sys_refcursor
   )
    IS
   
   
   
   BEGIN
     OPEN pResultado FOR
		SELECT r.id_rubro, r.n_rubro, ra.id_actividad_clanae 
		FROM t_sif_rubros r   
		LEFT JOIN t_sif_rubros_actividad ra
		ON r.id_rubro = ra.id_rubro;


END pr_get_RubrosActividad;
--- Autor (IB) --- fecha: 08/2018
--- Utilizado en Inscripcion
--- Indica si un mail ya fue utilizado para un cuit 
---distinto al que se envia por parametro. Devuelve
---el cuit que ya lo utilizo
PROCEDURE pr_get_EmailYaUtilizado (
	pCuit IN VARCHAR2,
	pNroMail IN VARCHAR2,
    pResultado OUT sys_refcursor
   )
    IS
   
   BEGIN
     OPEN pResultado FOR
		select e.cuit, c.NRO_MAIL
		from sifcos.t_sif_tramites_sifcos t
		join t_comunes.vt_comunicaciones c
		on to_char(t.nro_tramite_sifcos) = c.id_entidad
		join sifcos.T_SIF_ENTIDADES e
		on e.ID_ENTIDAD = t.ID_ENTIDAD
		and c.id_aplicacion=98
		and id_tipo_comunicacion=11
		where e.CUIT <> pCuit
		and upper(c.NRO_MAIL) = upper(pNroMail);


END pr_get_EmailYaUtilizado;

PROCEDURE pr_get_conceptos_TRS (
       PCursor     OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN PCursor FOR
           SELECT c.id_concepto,
                  c.n_concepto,
                  c.precio_base precio,
                  c.cod_ente,
                  c.fecha_desde
                  
           from trs.vt_conceptos_verticales c
            where c.cod_ente in ('057')
                            and c.precio_base<>0
            ;
           
END pr_get_conceptos_TRS;

--Autor: IB Fecha: 12/2018
--Trae los conceptos vigentes a cierta fecha para cada tipo de tramite
PROCEDURE pr_get_conceptosAFecha (
        pFecha IN DATE,
       PCursor     OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN PCursor FOR
SELECT  C.ID_SIF_HIST_CONCEPTOS,
        C.MONTO,
        C.FEC_DESDE,
        C.FEC_HASTA,
        C.ID_TIPO_CONCEPTO,
        TC.N_TIPO_CONCEPTO
FROM T_SIF_HIST_CONCEPTOS C
LEFT JOIN T_SIF_TIPOS_CONCEPTO TC
ON C.ID_TIPO_CONCEPTO = TC.ID_TIPO_CONCEPTO
WHERE FN_COMPARA_FECHAS_MAYOR(pFecha,FEC_DESDE)=1
AND FN_COMPARA_FECHAS_MENOR(pFecha,FEC_HASTA)=1;
           
END pr_get_conceptosAFecha;

PROCEDURE pr_get_Comercios_Vto (
        pCuit IN varchar2,
       pResultado OUT sys_refcursor
   )
   IS
   BEGIN
      OPEN pResultado FOR
/*Consultar por un CUIT una empresa en SIFCOS. Si existe, devuelve la cantidad de reemp que debe, 
con eso se puede determinar si esta al dia o no.*/

SELECT 
       TABLA_GRAL.cuit,
       TABLA_GRAL.nro_sifcos , 
       decode(TABLA_GRAL.FEC_VTO_SIFCOS_NUEVO,null,TABLA_GRAL.FEC_VTO_SIFCOS_VIEJO,TABLA_GRAL.FEC_VTO_SIFCOS_NUEVO ) FEC_VTO,
        
       (TABLA_GRAL.DEBE_SIFCOS_VIEJO - decode(TABLA_GRAL.DEBE_SIFCOS_NVO,null,0,TABLA_GRAL.DEBE_SIFCOS_NVO) ) DEBE ,
      TABLA_GRAL.ID_VIN_DOM ,
       TABLA_GRAL.DEPARTAMENTO , 
       TABLA_GRAL.LOCALIDAD , 
       TABLA_GRAL.DIRECCION ,
       TABLA_GRAL.BARRIO
FROM (
 SELECT TABLA.cuit , TABLA.nro_sifcos , /* 1? campo */
                    (SELECT MAX(T1.FEC_VENCIMIENTO) FEC_VENCIMIENTO 
                      FROM SIFCOS.T_SIF_TRAMITES_SIFCOS T1 JOIN SIFCOS.T_SIF_ENTIDADES E1 ON T1.ID_ENTIDAD = E1.ID_ENTIDAD
                      WHERE E1.NRO_SIFCOS =    TABLA.nro_sifcos
                    ) FEC_VTO_SIFCOS_NUEVO ,/* 2 ? campo */
                    
                    add_months( TABLA.ult_fecha_presentacion ,12) FEC_VTO_SIFCOS_VIEJO,/* 3 ? campo */ 
                    (select trunc(trunc(1+sysdate-(max(trm.fecha_presentacion)))/365)   
                                      from sifcos.t_tramites trm 
                                      where trm.nro_recsep= TABLA.nro_sifcos
                    ) DEBE_SIFCOS_VIEJO ,/* 4 ? campo */ 
                    ( select sum(t2.cant_reemp)   from sifcos.t_sif_tramites_sifcos t2
                                      join sifcos.t_sif_entidades ent on t2.id_entidad = ent.id_entidad 
                                      where ent.nro_sifcos = TABLA.nro_sifcos  and t2.id_tipo_tramite = 4                            
                    ) DEBE_SIFCOS_NVO , /* 5 ? campo */ 
                    (select d.id_vin from dom_manager.vt_domicilios d where d.id_vin = (
                                      select dt.id_vin from sifcos.t_domicilios_tramite dt
                                      where 
                                      dt.id_tipodom = 3 and
                                      dt.id_tramite = (
                                      select max(trami.id_tramite) from sifcos.t_tramites trami
                                      where 
                                      trami.nro_recsep = TABLA.nro_sifcos
                                      and
                                      trami.fecha_presentacion = (select max(t2.fecha_presentacion)
                                                                       from sifcos.t_tramites t2 
                                                                       where t2.nro_recsep= TABLA.nro_sifcos )
                                      )
                        )
                  ) ID_VIN_DOM ,/* 6 ? campo */ 
                   (select d.N_DEPARTAMENTO from dom_manager.vt_domicilios d where d.id_vin = (
                                      select dt.id_vin from sifcos.t_domicilios_tramite dt
                                      where 
                                      dt.id_tipodom = 3 and
                                      dt.id_tramite = (
                                      select max(trami.id_tramite) from sifcos.t_tramites trami
                                      where 
                                      trami.nro_recsep = TABLA.nro_sifcos
                                      and
                                      trami.fecha_presentacion = (select max(t2.fecha_presentacion)
                                                                       from sifcos.t_tramites t2 
                                                                       where t2.nro_recsep= TABLA.nro_sifcos )
                                      )
                        )
                  ) DEPARTAMENTO , /* 7 ? campo */ 
                   (select d.N_LOCALIDAD from dom_manager.vt_domicilios d where d.id_vin = (
                                      select dt.id_vin from sifcos.t_domicilios_tramite dt
                                      where 
                                      dt.id_tipodom = 3 and
                                      dt.id_tramite = (
                                     select max(trami.id_tramite) from sifcos.t_tramites trami
                                      where 
                                      trami.nro_recsep = TABLA.nro_sifcos
                                      and
                                      trami.fecha_presentacion = (select max(t2.fecha_presentacion)
                                                                       from sifcos.t_tramites t2 
                                                                       where t2.nro_recsep= TABLA.nro_sifcos )
                                      )
                        )
                  ) LOCALIDAD ,/* 8 ? campo */ 
                  
                  (select d.N_CALLE  || '  ' ||  d.altura from dom_manager.vt_domicilios d where d.id_vin = (
                                      select dt.id_vin from sifcos.t_domicilios_tramite dt
                                      where 
                                      dt.id_tipodom = 3 and
                                      dt.id_tramite = (
                                      select max(trami.id_tramite) from sifcos.t_tramites trami
                                      where 
                                      trami.nro_recsep = TABLA.nro_sifcos
                                      and
                                      trami.fecha_presentacion = (select max(t2.fecha_presentacion)
                                                                       from sifcos.t_tramites t2 
                                                                       where t2.nro_recsep= TABLA.nro_sifcos )
                                      )
                        )
                  ) DIRECCION , /* 9 ? campo */ 
                  (select d.N_BARRIO from dom_manager.vt_domicilios d where d.id_vin = (
                                      select dt.id_vin from sifcos.t_domicilios_tramite dt
                                      where 
                                      dt.id_tipodom = 3 and
                                      dt.id_tramite = (
                                      select max(trami.id_tramite) from sifcos.t_tramites trami
                                      where 
                                      trami.nro_recsep = TABLA.nro_sifcos
                                      and
                                      trami.fecha_presentacion = (select max(t2.fecha_presentacion)
                                                                       from sifcos.t_tramites t2 
                                                                       where t2.nro_recsep= TABLA.nro_sifcos )
                                      )
                        )
                  ) BARRIO /* 10 ? campo */ 
                   
 FROM 
 (
 select  t.nro_recsep nro_sifcos ,  e.cuit_pers_juridica cuit ,
                max(t.fecha_presentacion) ult_fecha_presentacion
 
  from sifcos.t_entidades e 
join sifcos.t_tramites t on e.id_entidad = t.id_entidad
where 
 t.nro_recsep is not null
group by  t.nro_recsep ,  e.cuit_pers_juridica 
having not exists ( select * from sifcos.t_tramites tram where tram.nro_recsep = t.nro_recsep and tram.id_tipo_tramite = 2)
 ) TABLA  
 
 ) TABLA_GRAL

WHERE  
        tabla_gral.cuit = pCuit
       
 UNION ALL
 
 /*Tramites de ALTA en el nvo sifcos. */

select 
        se.cuit,
        TO_CHAR(se.nro_sifcos) NRO_SIFCOS,   
        str.fec_vencimiento FEC_VTO, 
        0 DEBE ,
        d.id_vin id_vin_dom ,
        d.N_DEPARTAMENTO DEPARTAMENTO,
        d.N_LOCALIDAD LOCALIDAD, 
        d.N_CALLE  || '  ' ||  d.altura DIRECCION ,
        d.N_BARRIO BARRIO
from sifcos.t_sif_entidades se
join sifcos.t_sif_tramites_sifcos str on se.id_entidad=str.id_entidad
join dom_manager.vt_domicilios d on d.id_vin=str.id_vin_dom_local
where  
str.id_tipo_tramite=1
/*valido que se le haya asignado un nro_sifcos con el estado AUTORIZADO EN MINISTERIO*/
and exists ( select * from sifcos.t_sif_hist_estado h where h.id_tramite_sifcos = str.nro_tramite_sifcos 
and h.id_estado_tramite = 6)
/*valido que el tramite no se haya dado de BAJA si este en RECHAZADO SIN TASAS PAGAS. */
and not exists ( select * from sifcos.t_sif_hist_estado h where h.id_tramite_sifcos = str.nro_tramite_sifcos 
and h.id_estado_tramite in (7,10))
AND se.nro_sifcos <> 0
 
  and     se.cuit = pCuit;
           
END pr_get_Comercios_Vto;

PROCEDURE pr_get_IdsEntidad (
    p_IdLocalidad varchar2 DEFAULT NULL,
    p_IdRubro varchar2 DEFAULT NULL,
    pResultado OUT sys_refcursor
   )
   IS
   v_SQL  varchar2(7000);
   v_SELECT varchar2(4000);
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   BEGIN
    
     v_SELECT :=' select distinct
                  se.id_entidad ';
      v_FROM := ' FROM (select tse.* from sifcos.t_sif_entidades tse where tse.latitud_ubi is not null) se
                        join sifcos.vt_sif_tram_com_activos VT on se.id_entidad=VT.id_entidad ';
      v_WHERE :=' WHERE   se.nro_sifcos <> 0  ';
      
if(p_IdLocalidad is not null) then
   v_WHERE:= v_WHERE ||' AND  VT.id_localidad = '||  p_IdLocalidad ||' ';
end if;


if(p_IdRubro is not null) then
    
  if(p_IdRubro = '1') then/*Educacion-Deportes-Espectaculos*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (125,9,12,126,65,61,117,28,128,82,32) ';
  end if;               
  if(p_IdRubro = '2') then/*Salud-Higiene-Estetica*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (79,91,17,94,24,134,60,97,80,69) ';
  end if;               
  if(p_IdRubro = '3') then/*Electrodomesticos-Informatica*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (20,10,21,11,14) ';
  end if;               
  if(p_IdRubro = '4') then/*Servicios Varios*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (95,23,30,124,33,8,119,6,115,35,106,130,93,36,73,72,37,39,70,59,121,27) ';
  end if;               
  if(p_IdRubro = '5') then/*Polirrubro*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (4,77,16,105,50,98,52,62,68,67,66,87,84,51) ';
  end if;               
  if(p_IdRubro = '6') then/*Textil-Cuero-Accesorios*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (123,5,92,56,46,38,64,102) ';
  end if;               
  if(p_IdRubro = '7') then/*Financiero-Cobranza-Instituciones*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (114,107,83) ';
  end if;               
  if(p_IdRubro = '8') then/*Alimentos-Bebidas*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (2,48,110,78,75,137,109,108,43,74,120,81,58,57,76,135,88,34,89,131,49,86,44,85,122,1) ';
  end if;               
  if(p_IdRubro = '9') then/*Automotores-Maquinaria*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (3,103,42,127,54,96,26,118,29,132,104) ';
  end if;               
  if(p_IdRubro = '10') then/*Ferreteria-Construccion-Jardin*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (111,90,129,7,55,15,100,47,136,133,45,101,18,22,63,41,53,99,116,71,25,31,113,13) ';
  end if;               
  if(p_IdRubro = '11') then/*Turismo*/
    v_WHERE:= v_WHERE || ' AND VT.ID_RUBRO_PRI in (112,19,40) ';
  end if;               
end if;  
 
                                           
 v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' order by se.id_entidad';
    OPEN pResultado FOR
      v_SQL;
      
END pr_get_IdsEntidad;

PROCEDURE pr_get_IDDocumentoCDD (
    pNroTramite in varchar2,
    pResultado OUT sys_refcursor
   )
    IS
   
   BEGIN
     OPEN pResultado FOR
         SELECT t.id_documento1_cdd, t.id_documento2_cdd, t.id_documento3_cdd, t.id_documento4_cdd 
        -- t.id_documento3_cdd, t.id_documento4_cdd
          FROM sifcos.t_sif_tramites_sifcos t
--          WHERE (pNroTramite IS NULL OR t.nro_tramite_sifcos = pNroTramite) 
                  where t.nro_tramite_sifcos = pNroTramite
        --  and t.id_documento1_cdd is not null and t.id_documento2_cdd is not null
         --  and t.id_documento3_cdd is not null and t.id_documento4_cdd is not null
          ;


END pr_get_IDDocumentoCDD;

--- edicion: Fernando Budassi --- fecha: 07/04/2021
--- utilizado en pagina de SIIC
PROCEDURE pr_get_tramites_boca_ubi (
    p_idDepartamento IN varchar2,
    P_idLocalidad IN varchar2,
    pResultado OUT sys_refcursor
   )
   IS
   v_SQL  varchar2(7000);
   v_SELECT varchar2(4000);
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
    
                                                                                          
BEGIN
   V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
    
  v_SELECT :='SELECT  
                distinct
                decode(e.nro_sifcos,''0'',''SIN ASIGNAR'',e.nro_sifcos) nro_sifcos,
                e.id_entidad,
                e.cuit,
                pj.razon_social,
                decode(d.N_CALLE,'''',d.N_LOCALIDAD,d.N_LOCALIDAD ||'' - ''||d.N_CALLE||'' ''||d.altura) DOMICILIO,
                et.n_estado_tramite estado,
                et.id_estado_tramite ID_ESTADO, 
                t.fec_vencimiento vto_tramite,
                d.N_DEPARTAMENTO departamento,
                d.N_LOCALIDAD localidad,
                d.id_departamento,
                d.id_localidad  ';
 v_FROM:=' FROM sifcos.t_sif_tramites_sifcos t
         JOIN sifcos.t_sif_entidades e ON e.id_entidad = t.id_entidad
         JOIN sifcos.t_sif_tipos_tramite tt ON tt.id_tipo_tramite = t.id_tipo_tramite
         JOIN sifcos.t_sif_hist_estado he on he.id_tramite_sifcos= t.nro_tramite_sifcos
         JOIN sifcos.t_sif_estados_tramite et ON he.id_estado_tramite=et.id_estado_tramite
         JOIN t_comunes.vt_pers_juridicas pj ON pj.cuit = e.cuit and pj.id_sede=e.id_sede
         JOIN dom_manager.vt_domicilios d on d.id_vin=t.id_vin_dom_local  ';
v_WHERE:='WHERE 
            
          t.id_tipo_tramite in (1,2) and 
          he.id_estado_tramite <> 10 and 
          e.nro_sifcos <> 0 and    
          he.fec_desde_estado= (SELECT max(h.fec_desde_estado)
                                 FROM sifcos.t_sif_hist_estado h
                                 WHERE he.id_tramite_sifcos=h.id_tramite_sifcos
                                 ) '; 

   if(p_idDepartamento is not null) then
    v_WHERE:= v_WHERE || ' AND d.id_departamento ='||  p_idDepartamento ||' ';
  end if;
  if(P_idLocalidad is not null) then
    v_WHERE:= v_WHERE || ' AND d.id_localidad ='||  P_idLocalidad ||' ';
  end if;
v_SQL:=v_SELECT || v_FROM ||v_WHERE || ' order by 2' ;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_boca_ubi;

PROCEDURE pr_CantComerciosByLocalidad (
    pIdDepartamento in varchar2,
    pResultado OUT sys_refcursor
   )
    IS
   
   BEGIN
     OPEN pResultado FOR
     SELECT 
       
       d.id_localidad 
       ,count(e.nro_sifcos) CANT_COMERCIOS
       
      FROM sifcos.t_sif_tramites_sifcos t
         JOIN sifcos.t_sif_entidades e ON e.id_entidad = t.id_entidad
         JOIN sifcos.t_sif_hist_estado he on he.id_tramite_sifcos= t.nro_tramite_sifcos
         JOIN dom_manager.vt_domicilios d on d.id_vin=t.id_vin_dom_local

      WHERE 
          t.id_tipo_tramite in (1,2) and 
          he.id_estado_tramite <> 10 and 
          e.nro_sifcos <> 0 and  
           he.fec_desde_estado= (SELECT max(h.fec_desde_estado)
                                 FROM sifcos.t_sif_hist_estado h
                                 WHERE he.id_tramite_sifcos=h.id_tramite_sifcos
                                 ) and
           d.id_departamento=pIdDepartamento                                
      group by   d.id_localidad ;

END pr_CantComerciosByLocalidad;

/*PROCEDURE pr_get_Pedanias (
    pResultado OUT sys_refcursor
   )
    IS
   
   BEGIN
     OPEN pResultado FOR
          SELECT id_pedania,n_pedania 
          FROM dom_manager.vt_pedanias;

END pr_get_Pedanias;

PROCEDURE pr_get_Pedanias_by_id_departamento (
    pIdDepartamento in varchar2,
    pResultado OUT sys_refcursor
   )
    IS
   
   BEGIN
     OPEN pResultado FOR
          SELECT p.id_pedania,p.n_pedania 
          FROM dom_manager.vt_pedanias p
          JOIN dom_manager.vt_pedanias p ON p.id_pedania=pd.id_pedania
          WHERE pd.id_departamento = pIdDepartamento;

END pr_get_Pedanias_by_id_departamento;*/

END PCK_SIFCOS_CONSULTA;
/
