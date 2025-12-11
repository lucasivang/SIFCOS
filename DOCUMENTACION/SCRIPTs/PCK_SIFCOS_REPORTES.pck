CREATE OR REPLACE PACKAGE "PCK_SIFCOS_REPORTES" AS
  PROCEDURE pr_get_tramites_tot_loc (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   );
 
PROCEDURE pr_get_tramites_tot_tipo_tram (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   );
PROCEDURE pr_get_tramites_tot_act (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_tramites_tot_estados (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   );   
   PROCEDURE pr_get_Consulta_Emp_Reemp (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   ); 
  PROCEDURE pr_get_rpt_gerencial (
 P_TIPO_TRAMITE IN varchar2,
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  P_CUIT IN varchar2,
  P_RAZON_SOCIAL IN varchar2,
  P_NRO_TRAM_DESDE IN varchar2,
  P_NRO_TRAM_HASTA IN varchar2,
  P_NRO_SIFCOS_DESDE IN varchar2,
  P_NRO_SIFCOS_HASTA IN varchar2,
  P_ID_ORGANISMO IN varchar2,
  P_ID_DEPARTAMENTO IN varchar2,
  P_ID_LOCALIDAD IN varchar2,
  P_ID_ORG_PADRE IN varchar2,
  P_ID_ESTADO IN varchar2,
  pResultado OUT sys_refcursor
   );  
  PROCEDURE pr_get_rpt_comercios (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  P_ID_DEPARTAMENTO IN varchar2,
  P_ID_LOCALIDAD IN varchar2,
  pResultado OUT sys_refcursor
   );
   PROCEDURE pr_get_Consulta_Contacto (
  P_NRO_SIFCOS IN varchar2,
  pResultado OUT sys_refcursor
   );
 PROCEDURE pr_get_mapa (
  P_ID_ORGANISMO IN NUMBER,
  P_ID_DEPARTAMENTO IN NUMBER,
  P_ID_LOCALIDAD IN NUMBER,
  P_ID_PRODUCTO IN NUMBER,
  P_ID_RUBRO IN NUMBER,
        pResultado OUT sys_refcursor
   );

PROCEDURE pr_get_tramites_reempapen_tot (
  P_FECHA_DESDE IN date,
  ID_ORGANISMO_LOGUEADO IN NUMBER,
  pResultado OUT sys_refcursor
   ); 
PROCEDURE pr_get_tramites_reempapen (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
    P_INICIO in number ,
    P_FINAL in number ,
    P_ORDER in varchar2 ,
  P_RESULTADO OUT sys_refcursor
);
PROCEDURE pr_get_tramites_altashist_tot (
  P_FECHA_DESDE IN date,
  ID_ORGANISMO_LOGUEADO IN NUMBER,
  pResultado OUT sys_refcursor
   );
PROCEDURE pr_get_tramites_altashist (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
    P_INICIO in number ,
    P_FINAL in number ,
    P_ORDER in varchar2 ,
  P_RESULTADO OUT sys_refcursor
   );
PROCEDURE pr_get_tramites_bajashist_tot (
  P_FECHA_DESDE IN date,
  ID_ORGANISMO_LOGUEADO IN NUMBER,
  pResultado OUT sys_refcursor
   );
PROCEDURE pr_get_tramites_bajashist (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
    P_INICIO in number ,
    P_FINAL in number ,
    P_ORDER in varchar2 ,
  P_RESULTADO OUT sys_refcursor
   );
PROCEDURE pr_get_tramites_com_act_tot (
  P_FECHA_DESDE IN date,
  ID_ORGANISMO_LOGUEADO IN NUMBER,
  pResultado OUT sys_refcursor
   );
PROCEDURE pr_get_tramites_com_act (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
    P_INICIO in number ,
    P_FINAL in number ,
    P_ORDER in varchar2 ,
  P_RESULTADO OUT sys_refcursor
   );
PROCEDURE pr_exp_tramites_reempapen (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
  P_RESULTADO OUT sys_refcursor
); 
PROCEDURE pr_exp_tramites_altashist (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
  P_RESULTADO OUT sys_refcursor
   );
PROCEDURE pr_exp_tramites_bajashist (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
  P_RESULTADO OUT sys_refcursor
   );
PROCEDURE pr_exp_tramites_com_act (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
  P_RESULTADO OUT sys_refcursor
   );
 
END PCK_SIFCOS_REPORTES;
/
CREATE OR REPLACE PACKAGE BODY "PCK_SIFCOS_REPORTES" AS
--- edicion: Fernando Budassi --- fecha: 08/2016
--- calcula la cantidad de tramites por localidad de las entidades
PROCEDURE pr_get_tramites_tot_loc (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000);
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
  
  v_SELECT :=' SELECT nvl(d.N_LOCALIDAD,''SIN LOCALIDAD'') descripcion,
               count(*) cantidad  ';
                
 v_FROM:=' FROM sifcos.t_sif_tramites_sifcos t
join dom_manager.vt_domicilios d on d.id_vin=t.id_vin_dom_local  
JOIN sifcos.t_sif_hist_estado he on he.id_tramite_sifcos=t.nro_tramite_sifcos ';
         
v_WHERE:=' where t.nro_tramite_sifcos=(select max(h.id_tramite_sifcos) 
                            from sifcos.t_sif_hist_estado h 
                            where h.id_estado_tramite=6 
                            and h.id_tramite_sifcos=t.nro_tramite_sifcos) and (TO_DATE(t.fec_alta, ''dd/mm/yyyy'') >= TO_DATE('''||P_FECHA_DESDE||''', ''dd/mm/yyyy'') OR '''||P_FECHA_DESDE||''' IS NULL)
           AND (TO_DATE(t.fec_alta, ''dd/mm/yyyy'') <= TO_DATE('''||P_FECHA_HASTA||''', ''dd/mm/yyyy'') OR '''||P_FECHA_HASTA||''' IS NULL)
           AND he.fec_desde_estado = (select max(h.fec_desde_estado) from sifcos.t_sif_hist_estado h
                                   where h.id_tramite_sifcos=t.nro_tramite_sifcos
                                    and h.id_estado_tramite not in (9))
           AND t.id_tipo_tramite<>2';
           
V_GROUP_BY:=' group by d.n_localidad ';
  
v_SQL:=v_SELECT || v_FROM ||v_WHERE || V_GROUP_BY ;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_tot_loc;

--- edicion: Fernando Budassi --- fecha: 08/2016
--- calcula la cantidad de tramites por tipo de tramite
PROCEDURE pr_get_tramites_tot_tipo_tram (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000);
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
                      tt.n_tipo_tramite descripcion , 
                      count(t.nro_tramite_sifcos) cantidad ';
                
 v_FROM:=' FROM sifcos.t_sif_tramites_sifcos t
join sifcos.t_sif_tipos_tramite tt on tt.id_tipo_tramite=t.id_tipo_tramite  ';
         
v_WHERE:=' WHERE t.nro_tramite_sifcos=(select max(h.id_tramite_sifcos) 
                            from sifcos.t_sif_hist_estado h 
                            where h.id_estado_tramite=6 
                            and h.id_tramite_sifcos=t.nro_tramite_sifcos) and  (TO_DATE(t.fec_alta, ''dd/mm/yyyy'') >= TO_DATE('''||P_FECHA_DESDE||''', ''dd/mm/yyyy'') OR '''||P_FECHA_DESDE||''' IS NULL)
           AND (TO_DATE(t.fec_alta, ''dd/mm/yyyy'') <= TO_DATE('''||P_FECHA_HASTA||''', ''dd/mm/yyyy'') OR '''||P_FECHA_HASTA||''' IS NULL)
           ';
           
V_GROUP_BY:=' group by tt.n_tipo_tramite';
  
v_SQL:=v_SELECT || v_FROM ||v_WHERE || V_GROUP_BY ;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_tot_tipo_tram;

--- edicion: Fernando Budassi --- fecha: 08/2016
--- calcula la cantidad de tramites por actividad
PROCEDURE pr_get_tramites_tot_act (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000);
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
              a.n_actividad descripcion,
              count(*) cantidad ';

 v_FROM:=' FROM sifcos.t_sif_tramites_sifcos t
           JOIN t_comunes.t_actividades a on a.id_actividad=t.id_actividad_ppal';                
        
v_WHERE:=' WHERE  t.nro_tramite_sifcos=(select max(h.id_tramite_sifcos) 
                            from sifcos.t_sif_hist_estado h 
                            where h.id_estado_tramite=6 
                            and h.id_tramite_sifcos=t.nro_tramite_sifcos) and (TO_DATE(t.fec_alta, ''dd/mm/yyyy'') >= TO_DATE('''||P_FECHA_DESDE||''', ''dd/mm/yyyy'') OR '''||P_FECHA_DESDE||''' IS NULL)
           AND (TO_DATE(t.fec_alta, ''dd/mm/yyyy'') <= TO_DATE('''||P_FECHA_HASTA||''', ''dd/mm/yyyy'') OR '''||P_FECHA_HASTA||''' IS NULL)
           AND t.id_tipo_tramite<>2';

V_GROUP_BY:=' group by a.n_actividad ';           

  
v_SQL:=v_SELECT || v_FROM ||v_WHERE || V_GROUP_BY ;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_tot_act;

--- edicion: Fernando Budassi --- fecha: 08/2016
--- calcula la cantidad de tramites por estados
PROCEDURE pr_get_tramites_tot_estados (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000);
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
              et.n_estado_tramite descripcion,
              count(*) cantidad '; 
  v_FROM:=' FROM sifcos.t_sif_tramites_sifcos t
        
         JOIN sifcos.t_sif_hist_estado he on he.id_tramite_sifcos=t.nro_tramite_sifcos
         JOIN sifcos.t_sif_estados_tramite et on he.id_estado_tramite=et.id_estado_tramite  ';
               
          
v_WHERE:=' WHERE  (TO_DATE(t.fec_alta, ''dd/mm/yyyy'') >= TO_DATE('''||P_FECHA_DESDE||''', ''dd/mm/yyyy'') OR '''||P_FECHA_DESDE||''' IS NULL)
           AND (TO_DATE(t.fec_alta, ''dd/mm/yyyy'') <= TO_DATE('''||P_FECHA_HASTA||''', ''dd/mm/yyyy'') OR '''||P_FECHA_HASTA||''' IS NULL)
           AND he.fec_desde_estado = (select max(h.fec_desde_estado) from sifcos.t_sif_hist_estado h
                                   where h.id_tramite_sifcos=t.nro_tramite_sifcos
                                    and h.id_estado_tramite not in (9))
           AND t.id_tipo_tramite<>2';
           
V_GROUP_BY:=' group by et.n_estado_tramite order by et.n_estado_tramite';
  
v_SQL:=v_SELECT || v_FROM ||v_WHERE || V_GROUP_BY ;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_tot_estados;

PROCEDURE pr_get_Consulta_Emp_Reemp (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000);
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   
 
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
  
 v_SELECT :='SELECT DISTINCT tr.nro_recsep nro_sifcos, 
                 e.cuit_pers_juridica cuit,
                 e.razon_social,
                 d.N_DEPARTAMENTO,
                 d.N_LOCALIDAD,
                 d.N_BARRIO,
                 d.N_CALLE ||'' ''|| d.altura direccion,
                 d.depto,
                 d.piso,
                 (select u.email 
                  from sifcos.t_usuarios_web u
                  where u.ultimo_acceso is not null
                  and u.ultimo_acceso = (select max (uw.ultimo_acceso) 
                                         from sifcos.t_usuarios_web uw
                                         where uw.email=u.email)
                  and u.cuit=e.cuit_pers_juridica) email,
                                        
                 
                  (select trunc(trunc(1+sysdate-(max(t.fecha_presentacion)))/365)
                  from sifcos.t_tramites t 
                  join sifcos.t_entidades e on t.id_entidad=e.id_entidad 
                  where t.nro_recsep= tr.nro_recsep) Deb_Reempadronar,
                  
                  (select trunc(max(t.fecha_presentacion))
                  from sifcos.t_tramites t 
                  join sifcos.t_entidades e on t.id_entidad=e.id_entidad 
                  where t.nro_recsep= tr.nro_recsep) fecha_ult_presentacion
                   '; 
  v_FROM:=' FROM sifcos.t_tramites tr
            join sifcos.t_entidades e on e.id_entidad=tr.id_entidad
            join sifcos.t_domicilios_tramite dt on dt.id_tramite=tr.id_tramite
            join dom_manager.vt_domicilios d on dt.id_vin=d.id_vin ';
  v_WHERE:=' WHERE not exists (select * 
                               from sifcos.t_sif_entidades ent
                               where ent.nro_sifcos=tr.nro_recsep
                               )
             and e.nro_baja_municipal is null
             and tr.nro_recsep is not null
             and e.razon_social is not null
             and tr.fecha_presentacion=(select trunc(max(t.fecha_presentacion))
                                        from sifcos.t_tramites t 
                                        join sifcos.t_entidades e on t.id_entidad=e.id_entidad 
                                        where t.nro_recsep= tr.nro_recsep)
             and dt.id_tipodom=3
             and (TO_DATE(tr.fecha_presentacion, ''dd/mm/yyyy'') >= TO_DATE('''||P_FECHA_DESDE||''', ''dd/mm/yyyy'') OR '''||P_FECHA_DESDE||''' IS NULL)
             and (TO_DATE(tr.fecha_presentacion, ''dd/mm/yyyy'') <= TO_DATE('''||P_FECHA_HASTA||''', ''dd/mm/yyyy'') OR '''||P_FECHA_HASTA||''' IS NULL)
             order by fecha_ult_presentacion';
           
v_SQL:=v_SELECT || v_FROM ||v_WHERE;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_Consulta_Emp_Reemp;

PROCEDURE pr_get_rpt_gerencial (
  P_TIPO_TRAMITE IN varchar2,
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  P_CUIT IN varchar2,
  P_RAZON_SOCIAL IN varchar2,
  P_NRO_TRAM_DESDE IN varchar2,
  P_NRO_TRAM_HASTA IN varchar2,
  P_NRO_SIFCOS_DESDE IN varchar2,
  P_NRO_SIFCOS_HASTA IN varchar2,
  P_ID_ORGANISMO IN varchar2,
  P_ID_DEPARTAMENTO IN varchar2,
  P_ID_LOCALIDAD IN varchar2,
  P_ID_ORG_PADRE IN varchar2,
  P_ID_ESTADO IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(25000);
   v_SELECT varchar2(10000);
   
   v_FROM varchar2(5000);
   v_WHERE varchar2(5000);
   
 
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
  
  v_SELECT :='SELECT distinct
              (select pj2.razon_social from sifcos.t_sif_organismos o
               join t_comunes.vt_pers_juridicas_completa pj2 on pj2.cuit=o.cuit and pj2.id_sede=o.id_sede
               where o.id_organismo=tr.id_organismo_alta)BOCA_RECEPCION,
              tr.nro_tramite_sifcos nro_tramite,
               et.n_estado_tramite estado_tramite,    
                tt.n_tipo_tramite tipo_tramite,
                to_char(tr.fec_alta,''YYYY'') anio_operativo,
                nvl2(e.nro_sifcos,e.nro_sifcos||''/''||to_char(tr.fec_alta,''YYYY''),''NO ASIGNADO'') nro_sifcos,
               (SELECT distinct ap.N_ACTIVIDAD 
                FROM t_comunes.vt_actividades ap
                WHERE ap.id_actividad=tr.id_actividad_ppal) ACTIVIDAD_PRI,
                (SELECT distinct ap.N_ACTIVIDAD 
                FROM t_comunes.vt_actividades ap
                WHERE ap.id_actividad=tr.id_actividad_sria) ACTIVIDAD_SEC,
                tr.fec_ini_tramite fec_ini_act,
                pj.cuit,
                pj.razon_social,
                pj.nombre_fantasia,
                (select p.NOV_NOMBRE||'', ''||p.NOV_APELLIDO from rcivil.vt_pk_persona p 
                where p.NRO_DOCUMENTO=r.nro_documento 
                and p.ID_SEXO=r.id_sexo 
                and p.ID_NUMERO=r.id_numero 
                and p.PAI_COD_PAIS=r.pai_cod_pais) rep_legal,
                r.nro_documento DNI_rep_Legal,
                pj.nro_ingbruto NRO_DGR,
                pj.nro_hab_municipal,
                domlegal.N_PROVINCIA L_provincia,
                domlegal.N_DEPARTAMENTO L_Depto,
                domlegal.N_LOCALIDAD L_localidad,
                domlegal.N_BARRIO L_barrio,
                domlegal.N_CALLE L_calle,
                domlegal.altura L_altura,
                domlegal.cp L_CodPostal,
                domlegal.piso L_piso,
                domlegal.depto L_dpto,
                domlocal.N_PROVINCIA R_provincia,
                domlocal.N_DEPARTAMENTO R_Depto,
                domlocal.N_LOCALIDAD R_localidad,
                domlocal.N_BARRIO R_barrio,
                domlocal.N_CALLE R_calle,
                domlocal.altura R_altura,
                domlocal.cp R_CodPostal,
                domlocal.piso R_piso,
                domlocal.depto R_dpto,
                (select distinct se1.superficie from sifcos.t_sif_superficies_empresa se1
                 where se1.id_tramite_sifcos=tr.nro_tramite_sifcos
                 and se1.id_tipo_superficie=1) sup_admin,
                (select distinct se2.superficie from sifcos.t_sif_superficies_empresa se2
                 where se2.id_tramite_sifcos=tr.nro_tramite_sifcos
                 and se2.id_tipo_superficie=2) sup_ventas,
                (select distinct se3.superficie from sifcos.t_sif_superficies_empresa se3
                 where se3.id_tramite_sifcos=tr.nro_tramite_sifcos
                 and se3.id_tipo_superficie=3) sup_deposito,
                 tr.cant_pers_total,
                 tr.cant_pers_rel_dependencia,
                 tr.cant_reemp cant_reempadronamiento,
                 tr.fec_alta,
                 tr.fec_vencimiento ';
                
 v_FROM:=' FROM sifcos.t_sif_tramites_sifcos tr
           join sifcos.t_sif_entidades e on e.id_entidad=tr.id_entidad
           join sifcos.t_sif_superficies_empresa s on s.id_tramite_sifcos=tr.nro_tramite_sifcos
           join sifcos.t_sif_rep_legal r on r.id_rep_legal=tr.id_cargo_entidad
           join t_comunes.vt_pers_juridicas_completa pj on e.cuit=pj.cuit  and e.id_sede=pj.id_sede
           join sifcos.t_sif_tipos_tramite tt on tr.id_tipo_tramite=tt.id_tipo_tramite
           join dom_manager.vt_domicilios domLegal on domLegal.id_vin=tr.id_vin_dom_legal
           join dom_manager.vt_domicilios domLocal on domLocal.id_vin=tr.id_vin_dom_local 
           join sifcos.t_sif_hist_estado h on h.id_tramite_sifcos=tr.nro_tramite_sifcos  
           join sifcos.t_sif_estados_tramite et on h.id_estado_tramite = et.id_estado_tramite ';
         
v_WHERE:=' WHERE  h.fec_desde_estado=(select max(h2.fec_desde_estado) 
                                      from sifcos.t_sif_hist_estado h2 
                                      where h2.id_tramite_sifcos=tr.nro_tramite_sifcos) ';

if(P_FECHA_DESDE is not null) then
    v_WHERE:= v_WHERE ||' AND TO_DATE(tr.fec_alta, ''dd/mm/yyyy'') >= TO_DATE('''||P_FECHA_DESDE||''', ''dd/mm/yyyy'') ';
  end if;   
if(P_FECHA_HASTA is not null) then
    v_WHERE:= v_WHERE ||' AND TO_DATE(tr.fec_alta, ''dd/mm/yyyy'') <= TO_DATE('''||P_FECHA_HASTA||''', ''dd/mm/yyyy'') ';
  end if;          
if(P_TIPO_TRAMITE is not null) then
    v_WHERE:= v_WHERE ||' AND tr.id_tipo_tramite ='||  P_TIPO_TRAMITE ||' ';
  end if;
if(P_CUIT is not null) then
    v_WHERE:= v_WHERE ||' AND e.cuit ='||  P_CUIT ||' ';
  end if;
if(P_RAZON_SOCIAL is not null) then
    v_WHERE:= v_WHERE ||' AND pj.razon_social like '''||  P_RAZON_SOCIAL ||'%''';
  end if;
if(P_NRO_TRAM_DESDE is not null) then
    v_WHERE:= v_WHERE ||' AND tr.nro_tramite_sifcos >= '|| P_NRO_TRAM_DESDE;
  end if;  
if(P_NRO_TRAM_HASTA is not null) then
    v_WHERE:= v_WHERE ||' AND tr.nro_tramite_sifcos <= '|| P_NRO_TRAM_HASTA;
  end if;
if(P_NRO_SIFCOS_DESDE is not null) then
    v_WHERE:= v_WHERE ||' AND e.nro_sifcos >= '|| P_NRO_SIFCOS_DESDE;
  end if;  
if(P_NRO_SIFCOS_HASTA is not null) then
    v_WHERE:= v_WHERE ||' AND e.nro_sifcos <= '|| P_NRO_SIFCOS_HASTA;
  end if;   
if(P_ID_DEPARTAMENTO is not null) then
    v_WHERE:= v_WHERE ||' AND domlocal.id_departamento = '|| P_ID_DEPARTAMENTO;
  end if;   
if(P_ID_LOCALIDAD is not null) then
    v_WHERE:= v_WHERE ||' AND domlocal.id_localidad = '|| P_ID_LOCALIDAD;
  end if; 
IF(P_ID_ORG_PADRE is not null) THEN
   IF( P_ID_ORGANISMO IS NOT NULL) THEN
       v_WHERE:= v_WHERE ||' AND tr.id_organismo_alta = '|| P_ID_ORGANISMO;
   ELSE
      IF (P_ID_ORG_PADRE = 2) THEN
         v_WHERE:= v_WHERE ||' AND tr.id_organismo_alta in (2,4,5,6,7,9,10,13,14,15,16,19,23,26,30,32,
         34,35,36,37,40,41,43,44,45,46,48,51,53,54,60,24,18,55,42,62,21,27,38,58,63,64,50,33,31,52,56,
         47,25,12,8,49,57,39,20,22,28,29,11,17) ';
       END IF;
       IF (P_ID_ORG_PADRE = 3) THEN
         v_WHERE:= v_WHERE ||' AND tr.id_organismo_alta in (3,61) ';
       END IF;
   END IF;  
ELSE
  IF (P_ID_ORGANISMO <> 3 AND P_ID_ORGANISMO<>2 AND P_ID_ORGANISMO<>1) THEN
     v_WHERE:= v_WHERE ||' AND tr.id_organismo_alta = '|| P_ID_ORGANISMO;
  END IF;
END IF;


if(P_ID_ESTADO is not null) then
    v_WHERE:= v_WHERE ||' AND   et.id_estado_tramite = '|| P_ID_ESTADO;
  end if;      
v_SQL:=v_SELECT || v_FROM ||v_WHERE;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_rpt_gerencial;

PROCEDURE pr_get_rpt_comercios (
  P_FECHA_DESDE IN varchar2,
  P_FECHA_HASTA IN varchar2,
  P_ID_DEPARTAMENTO IN varchar2,
  P_ID_LOCALIDAD IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(20000);
   v_SELECT varchar2(20000);
   
   v_FROM varchar2(5000);
   v_WHERE varchar2(5000);
   
 
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
  
  v_SELECT :='SELECT 
                     TABLA_GRAL.nro_sifcos , 
                     TABLA_GRAL.CUIT,
                     TABLA_GRAL.RAZON_SOCIAL,
                     decode(TABLA_GRAL.FEC_VTO_SIFCOS_NUEVO,null,TABLA_GRAL.FEC_VTO_SIFCOS_VIEJO,TABLA_GRAL.FEC_VTO_SIFCOS_NUEVO ) FEC_VTO,
                     (TABLA_GRAL.DEBE_SIFCOS_VIEJO - decode(TABLA_GRAL.DEBE_SIFCOS_NVO,null,0,TABLA_GRAL.DEBE_SIFCOS_NVO) ) DEBE  ,
                     TABLA_GRAL.DOMICILIO,
                     TABLA_GRAL.ID_DEPARTAMENTO ,
                     TABLA_GRAL.ID_LOCALIDAD
                      
              FROM (
               SELECT TABLA.nro_sifcos , TABLA.CUIT , TABLA.RAZON_SOCIAL ,
                                  (SELECT MAX(T1.FEC_VENCIMIENTO) FEC_VENCIMIENTO 
                                    FROM SIFCOS.T_SIF_TRAMITES_SIFCOS T1 JOIN SIFCOS.T_SIF_ENTIDADES E1 ON T1.ID_ENTIDAD = E1.ID_ENTIDAD
                                    WHERE to_char(E1.NRO_SIFCOS) = to_char(TABLA.nro_sifcos)
                                  ) FEC_VTO_SIFCOS_NUEVO , 
                                  
                                  add_months( TABLA.ult_fecha_presentacion ,12) FEC_VTO_SIFCOS_VIEJO, 
                                  (select trunc(trunc(1+sysdate-(max(trm.fecha_presentacion)))/365)   
                                                    from sifcos.t_tramites trm 
                                                    where to_char(trm.nro_recsep)= to_char(TABLA.nro_sifcos)
                                  )  DEBE_SIFCOS_VIEJO , 
                                  ( select sum(t2.cant_reemp)   from sifcos.t_sif_tramites_sifcos t2
                                                    join sifcos.t_sif_entidades ent on t2.id_entidad = ent.id_entidad 
                                                    where to_char(ent.nro_sifcos) = to_char(TABLA.nro_sifcos)
                                  )  DEBE_SIFCOS_NVO ,  
                                (select d.N_DEPARTAMENTO ||'' - ''||d.N_LOCALIDAD||'' - ''||d.N_CALLE||'' ''||d.altura||'' CP: ''||d.cp from dom_manager.vt_domicilios d where d.id_vin = (
                                                    select dt.id_vin from sifcos.t_domicilios_tramite dt
                                                    where 
                                                    dt.id_tipodom = 3 and
                                                    dt.id_tramite = (
                                                    select max(trami.id_tramite) from sifcos.t_tramites trami
                                                    where 
                                                    to_char(trami.nro_recsep) = to_char(TABLA.nro_sifcos)
                                                    and
                                                    trami.fecha_presentacion = (select max(t2.fecha_presentacion)
                                                                                     from sifcos.t_tramites t2 
                                                                                     where to_char(t2.nro_recsep)= to_char(TABLA.nro_sifcos) )
                                                    )
                                      )
                                ) DOMICILIO ,  
                                  (select d.id_departamento from dom_manager.vt_domicilios d where d.id_vin = (
                                                    select dt.id_vin from sifcos.t_domicilios_tramite dt
                                                    where 
                                                    dt.id_tipodom = 3 and
                                                    dt.id_tramite = (
                                                    select max(trami.id_tramite) from sifcos.t_tramites trami
                                                    where 
                                                    to_char(trami.nro_recsep) = to_char(TABLA.nro_sifcos)
                                                    and
                                                    trami.fecha_presentacion = (select max(t2.fecha_presentacion)
                                                                                     from sifcos.t_tramites t2 
                                                                                     where to_char(t2.nro_recsep)= to_char(TABLA.nro_sifcos) )
                                                    )
                                      )
                                ) ID_DEPARTAMENTO , 
                                
                                (select d.id_localidad from dom_manager.vt_domicilios d where d.id_vin = (
                                                    select dt.id_vin from sifcos.t_domicilios_tramite dt
                                                    where 
                                                    dt.id_tipodom = 3 and
                                                    dt.id_tramite = (
                                                   select max(trami.id_tramite) from sifcos.t_tramites trami
                                                    where 
                                                    to_char(trami.nro_recsep) = to_char(TABLA.nro_sifcos)
                                                    and
                                                    trami.fecha_presentacion = (select max(t2.fecha_presentacion)
                                                                                     from sifcos.t_tramites t2 
                                                                                     where to_char(t2.nro_recsep) = to_char(TABLA.nro_sifcos) )
                                                    )
                                      )
                                ) ID_LOCALIDAD
               FROM 
               (
                    select  t.nro_recsep nro_sifcos , e.cuit_pers_juridica cuit, e.razon_social , max(t.fecha_presentacion) ult_fecha_presentacion
                    from sifcos.t_entidades e 
                    join sifcos.t_tramites t on e.id_entidad = t.id_entidad
                    where t.nro_recsep is not null
                    group by  t.nro_recsep , e.cuit_pers_juridica , e.razon_social 
                    having not exists ( select * from sifcos.t_tramites tram where to_char(tram.nro_recsep) = to_char(t.nro_recsep) and tram.id_tipo_tramite = 2)
               ) TABLA   
               
               ) TABLA_GRAL

              UNION ALL
               

              select 
                      TO_CHAR(se.nro_sifcos) NRO_SIFCOS, 
                      se.cuit , 
                      (SELECT distinct pj.razon_social 
                       from t_comunes.vt_pers_juridicas_completa pj
                       where pj.cuit=se.cuit and pj.id_sede=se.id_sede) razon_social,
                      str.fec_vencimiento FEC_VTO, 
                      0 DEBE ,
                      d.N_DEPARTAMENTO ||'' - ''||d.N_LOCALIDAD||'' - ''||d.N_CALLE||'' ''||d.altura||'' CP: ''||d.cp DOMICILIO,
                      d.id_departamento ID_DEPARTAMENTO,
                      d.id_localidad ID_LOCALIDAD
              from sifcos.t_sif_entidades se
              join sifcos.t_sif_tramites_sifcos str on se.id_entidad=str.id_entidad
              join dom_manager.vt_domicilios d on d.id_vin=str.id_vin_dom_local
              where  
              str.id_tipo_tramite=1
              and exists ( select * from sifcos.t_sif_hist_estado h where h.id_tramite_sifcos = str.nro_tramite_sifcos 
              and h.id_estado_tramite = 6)
              and not exists ( select * from sifcos.t_sif_hist_estado h where h.id_tramite_sifcos = str.nro_tramite_sifcos 
              and h.id_estado_tramite in (7,10))
              AND se.nro_sifcos <> 0 ';
                
   
v_SQL:=v_SELECT;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_rpt_comercios;

PROCEDURE pr_get_Consulta_Contacto (
  P_NRO_SIFCOS IN varchar2,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   V_Entidad NUMBER(2);
   v_SQL  varchar2(7000);
   v_SELECT varchar2(2000);
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
                                                                                          
BEGIN
    SELECT COUNT(*)
    INTO V_Entidad
    FROM sifcos.t_sif_tramites_sifcos t
    join sifcos.t_sif_entidades e on e.id_entidad=t.id_entidad
    WHERE e.nro_sifcos=P_NRO_SIFCOS;
    
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;
  -- consulta de contacto de un nro sifcos en el sistema viejo y 
  -- antes se verifica si ese nro sifcos no posee datos en el sistema nuevo
IF V_Entidad = 0 THEN
 v_SELECT :='SELECT  
             tr.id_tramite nro_tramite,
             tr.nro_recsep nro_sifcos,
             nvl2('''','' - '',vtc.nro_mail) EMAIL,
             vttelprinc.cod_area||'' - ''||vttelprinc.nro_mail TEL_PRINCIPAL,
             vttelcel.cod_area||'' - ''||vttelcel.nro_mail CELULAR,
             vttelpart.cod_area||'' - ''||vttelpart.nro_mail TEL_PARTICULAR,
             '''' PAG_WEB,
             '''' FACEBOOK
             '; 
  v_FROM:=' FROM  sifcos.t_tramites tr
            join sifcos.t_entidades e on e.id_entidad=tr.id_entidad
/*obtengo el email del comercio*/
            LEFT JOIN t_comunes.vt_comunicaciones vtc ON e.id_entidad = vtc.id_entidad
                                                      AND vtc.id_tipo_comunicacion = ''11''
                                                      AND vtc.tabla_origen = ''SIFCOS.T_ENTIDADES''
/*obtengo el nro de telefono principal comercio*/
            LEFT JOIN t_comunes.vt_comunicaciones vttelprinc ON e.id_entidad = vttelprinc.id_entidad
                                                      AND vttelprinc.id_tipo_comunicacion = ''01''
                                                      AND vttelprinc.tabla_origen = ''SIFCOS.T_ENTIDADES''
/*obtengo el nro de celular*/
            LEFT JOIN t_comunes.vt_comunicaciones vttelcel ON e.id_entidad = vttelcel.id_entidad
                                                      AND vttelcel.id_tipo_comunicacion = ''07''
                                                      AND vttelcel.tabla_origen = ''SIFCOS.T_ENTIDADES''
/*obtengo el nro de telefono particular*/
            LEFT JOIN t_comunes.vt_comunicaciones vttelpart ON e.id_entidad = vttelpart.id_entidad
                                                      AND vttelpart.id_tipo_comunicacion = ''08''
                                                      AND vttelpart.tabla_origen = ''SIFCOS.T_ENTIDADES'' ';
  v_WHERE:=' WHERE tr.id_tipo_tramite=1 and tr.nro_recsep='||P_NRO_SIFCOS;
ELSE
  v_SELECT :='SELECT tr.nro_tramite_sifcos nro_tramite,
              e.nro_sifcos nro_sifcos,
              (SELECT email.nro_mail
              FROM t_comunes.t_comunicaciones email
              WHERE email.id_entidad=to_char(tr.nro_tramite_sifcos)
              and email.id_tipo_comunicacion=''11'') EMAIL,
              (SELECT  TELPPAL.cod_area ||'' - ''||TELPPAL.nro_mail
              FROM t_comunes.t_comunicaciones TELPPAL
              WHERE TELPPAL.id_tipo_comunicacion=''01'' 
              and TELPPAL.id_entidad=to_char(tr.nro_tramite_sifcos)) TEL_PRINCIPAL,
              (SELECT  CELULAR.cod_area ||'' - ''||CELULAR.nro_mail
              FROM t_comunes.t_comunicaciones CELULAR
              WHERE CELULAR.id_tipo_comunicacion=''07'' 
              and CELULAR.id_entidad=to_char(tr.nro_tramite_sifcos)) CELULAR,
              (SELECT PAG_WEB.nro_mail
              FROM t_comunes.t_comunicaciones PAG_WEB
              WHERE PAG_WEB.id_entidad=to_char(tr.nro_tramite_sifcos)
              and PAG_WEB.id_tipo_comunicacion=''12'') PAG_WEB,
              (SELECT FACEBOOK.nro_mail
              FROM t_comunes.t_comunicaciones FACEBOOK
              WHERE FACEBOOK.id_entidad=to_char(tr.nro_tramite_sifcos)
              and FACEBOOK.id_tipo_comunicacion=''17'') FACEBOOK
             '; 
v_FROM:=' FROM  sifcos.t_sif_tramites_sifcos tr
          JOIN sifcos.t_sif_entidades e on e.id_entidad=tr.id_entidad ';
v_WHERE:=' WHERE e.nro_sifcos='||P_NRO_SIFCOS;  
END IF;                    
       
v_SQL:=v_SELECT || v_FROM ||v_WHERE;
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_Consulta_Contacto;

--- edicion: (IB) --- fecha: 03/2018
--- Aplica filtros para obtener los comercios con el objetivo de dibujar en un mapa
PROCEDURE pr_get_mapa (
  P_ID_ORGANISMO IN NUMBER,
  P_ID_DEPARTAMENTO IN NUMBER,
  P_ID_LOCALIDAD IN NUMBER,
  P_ID_PRODUCTO IN NUMBER,
  P_ID_RUBRO IN NUMBER,
  pResultado OUT sys_refcursor
   )
   IS
   v_SQL  varchar2(5000);
   v_SELECT varchar2(2000);
   v_FROM varchar2(1000);
   v_WHERE varchar2(1000);
   -- Obtengo los datos del comercio 
   -- las columnas son: RAZON_SOCIAL,LATITUD,LONGITUD,DOMICILIO,
   --                   PRODUCTO,ID_PRODUCTO_ID_DEPARTAMENTO,ID_LOCALIDAD
   
   BEGIN
         v_SELECT :='SELECT pe.RAZON_SOCIAL, 
       e.CUIT,
       e.id_entidad,
       e.nro_sifcos, 
       e.LATITUD_UBI LATITUD, 
       e.LONGITUD_UBI LONGITUD,
       vt.N_DEPARTAMENTO ||'' - '' || vt.N_LOCALIDAD || '' - '' || vt.N_BARRIO || '' - '' || vt.N_CALLE || '' ''|| vt.altura domicilio,
       o.ID_ORGANISMO  ';

         v_FROM:=' FROM SIFCOS.T_SIF_ENTIDADES e
         --Persona juridica, razon social
         LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS pe ON e.CUIT = pe.CUIT AND e.ID_SEDE=pe.ID_SEDE
         --Tramite alta o reempa
         JOIN SIFCOS.T_SIF_TRAMITES_SIFCOS t ON e.ID_ENTIDAD = t.ID_ENTIDAD AND t.ID_TIPO_TRAMITE IN (1,4)
         --Domicilio del local
         LEFT JOIN DOM_MANAGER.VT_DOMICILIOS vt ON VT.ID_VIN = T.ID_VIN_DOM_LOCAL
         --Boca TRAMITE
         JOIN SIFCOS.T_SIF_ORGANISMOS o ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA ';

               v_WHERE:='WHERE NRO_SIFCOS > 0
      --Descartamos comercios sin latitud valida
      AND LATITUD_UBI IS NOT NULL 
      --Descartamos comercios sin longitud valida
      AND LONGITUD_UBI IS NOT NULL
      AND t.ID_VIN_DOM_LOCAL IS NOT NULL
      AND NOT EXISTS (
          SELECT NRO_TRAMITE_SIFCOS
          FROM T_SIF_TRAMITES_SIFCOS B
          WHERE B.ID_ENTIDAD =  e.ID_ENTIDAD
          AND (B.ID_TIPO_TRAMITE=2 OR (B.ID_TIPO_TRAMITE=4 AND B.NRO_TRAMITE_SIFCOS > T.NRO_TRAMITE_SIFCOS))
      )
      ';

      IF (P_ID_LOCALIDAD <> 0) THEN
          v_WHERE:= v_WHERE ||' --Localidad
      AND ID_LOCALIDAD= ' || P_ID_LOCALIDAD || '
      ';
        END IF;   

      IF (P_ID_DEPARTAMENTO <> 0) THEN
          v_WHERE:= v_WHERE ||' --Departamento
      AND ID_DEPARTAMENTO= ' || P_ID_DEPARTAMENTO || '
      ';
        END IF; 

      IF(P_ID_PRODUCTO IS NOT NULL) THEN
          v_FROM:= v_FROM || '--Producto
      JOIN T_SIF_PRODUCTOS_TRAMITE pt
      ON pt.ID_TRAMITE_SIFCOS = t.NRO_TRAMITE_SIFCOS
      ';

          v_WHERE:= v_WHERE ||' --Producto
      AND pt.ID_PRODUCTO = ' || P_ID_PRODUCTO;
        END IF;   


      IF(P_ID_RUBRO IS NOT NULL) THEN
          v_FROM:= v_FROM || '--Rubro
      LEFT JOIN T_SIF_RUBROS_ACTIVIDAD rap
      ON rap.ID_ACTIVIDAD_CLANAE = t.ID_ACTIVIDAD_PPAL
      LEFT JOIN T_SIF_RUBROS_ACTIVIDAD ras
      ON ras.ID_ACTIVIDAD_CLANAE = t.ID_ACTIVIDAD_SRIA
      ';

          v_WHERE:= v_WHERE ||' --Rubro
      AND (rap.ID_RUBRO = ' || P_ID_RUBRO || ' OR ras.ID_RUBRO=' || P_ID_RUBRO || ') ' ;
        END IF;

      IF (P_ID_ORGANISMO!=1)
      THEN
           v_FROM:= v_FROM || ' JOIN T_SIF_ORGANISMOS_COBERTURA oc
      ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
      AND oc.ID_ORGANISMO=' ||  P_ID_ORGANISMO || '
      ';
      END IF;
                        
      v_SQL:=v_SELECT || v_FROM ||v_WHERE ;
      --dbms_output.put_line(v_SQL);
      OPEN pResultado FOR
      v_SQL;

END pr_get_mapa;

--- edicion: (IB) --- fecha: 03/2018
--- calcula la cantidad de reempadronmiientos pendientes por organismo logueado
PROCEDURE pr_get_tramites_reempapen_tot (
  P_FECHA_DESDE IN date,
  ID_ORGANISMO_LOGUEADO IN NUMBER,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000);
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
    v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = ID_ORGANISMO_LOGUEADO
AND PO.ID_LOCALIDAD IS NOT NULL;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

 v_SELECT :='SELECT COUNT(*)  Cantidad ';

 v_FROM:=' FROM (
SELECT DISTINCT E.ID_ENTIDAD
FROM T_SIF_TRAMITES_SIFCOS T
JOIN T_SIF_HIST_ESTADO HE
ON HE.ID_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS AND ID_ESTADO_TRAMITE = 6
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD 
';

IF (ID_ORGANISMO_LOGUEADO!=1)
THEN
     v_FROM:= v_FROM || 'JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  ID_ORGANISMO_LOGUEADO || '
';
END IF;

v_FROM:= v_FROM || 'WHERE T.ID_TIPO_TRAMITE IN (4,1)
AND T.FEC_VENCIMIENTO < SYSDATE AND  E.NRO_SIFCOS <>0 
AND NOT EXISTS (
    SELECT NRO_TRAMITE_SIFCOS
    FROM T_SIF_TRAMITES_SIFCOS B
    JOIN T_SIF_ENTIDADES EB
    ON B.ID_ENTIDAD = EB.ID_ENTIDAD 
    WHERE EB.NRO_SIFCOS =  E.NRO_SIFCOS
    AND (B.ID_TIPO_TRAMITE=2 OR (B.ID_TIPO_TRAMITE=4 AND B.NRO_TRAMITE_SIFCOS > T.NRO_TRAMITE_SIFCOS))
)
';                

IF (ID_ORGANISMO_LOGUEADO!=1)
THEN
--Buscamos aquellos que tengan domicilio en la localidad
v_FROM:= v_FROM || ' UNION
SELECT DISTINCT E.ID_ENTIDAD
FROM T_SIF_TRAMITES_SIFCOS T
JOIN T_SIF_HIST_ESTADO HE
ON HE.ID_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS AND ID_ESTADO_TRAMITE = 6
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
 WHERE T.ID_TIPO_TRAMITE IN (4,1)
AND NOT EXISTS (
    SELECT NRO_TRAMITE_SIFCOS
    FROM T_SIF_TRAMITES_SIFCOS B
    JOIN T_SIF_ENTIDADES EB
    ON B.ID_ENTIDAD = EB.ID_ENTIDAD 
    WHERE EB.NRO_SIFCOS =  E.NRO_SIFCOS
    AND (B.ID_TIPO_TRAMITE=2 OR (B.ID_TIPO_TRAMITE=4 AND B.NRO_TRAMITE_SIFCOS > T.NRO_TRAMITE_SIFCOS))
)
AND T.FEC_VENCIMIENTO < SYSDATE AND  E.NRO_SIFCOS <>0 AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || '
) ENTIDADESV
';
ELSE
    v_FROM:= v_FROM || '
) ENTIDADESV
';
END IF;
        
v_WHERE:=' ';

V_GROUP_BY:=' ';           

  
v_SQL:=v_SELECT || v_FROM ||v_WHERE || V_GROUP_BY ;
--dbms_output.put_line(v_SQL);
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_reempapen_tot;

--- edicion: (IB) --- fecha: 03/2018
--- Devuelve el listado de reempadronmiientos pendientes por organismo logueado
PROCEDURE pr_get_tramites_reempapen (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
    P_INICIO in number ,
    P_FINAL in number ,
    P_ORDER in varchar2,
  P_RESULTADO OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000); 
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
   V_ORDER varchar2(100);
    v_UNION varchar2(3000);
     v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN

    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = P_ID_ORGANISMO
AND PO.ID_LOCALIDAD IS NOT NULL;

-- v_SELECT :='SELECT DISTINCT E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_VENCIMIENTO, T.NRO_TRAMITE_SIFCOS, T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA  , tt.N_TIPO_TRAMITE ';
 v_SELECT :='SELECT CUIT, NRO_SIFCOS, RAZON_SOCIAL, FEC_VENCIMIENTO, NRO_TRAMITE_SIFCOS, ID_ORGANISMO_ALTA, BOCA  , N_TIPO_TRAMITE ';


 v_FROM:=' FROM (
SELECT DISTINCT E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_VENCIMIENTO, T.NRO_TRAMITE_SIFCOS, T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA  , tt.N_TIPO_TRAMITE 
 FROM T_SIF_TRAMITES_SIFCOS T
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
JOIN T_SIF_HIST_ESTADO HE
ON HE.ID_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS AND ID_ESTADO_TRAMITE = 6
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND e.ID_SEDE=pe.ID_SEDE
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
 ';                

IF (P_ID_ORGANISMO!=1)
THEN
     v_FROM:= v_FROM || 'JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  P_ID_ORGANISMO || '
';
END IF;
        
v_FROM:= v_FROM || ' WHERE T.ID_TIPO_TRAMITE IN (4,1)
AND T.FEC_VENCIMIENTO < SYSDATE AND E.NRO_SIFCOS <>0   
AND NOT EXISTS (
    SELECT NRO_TRAMITE_SIFCOS
    FROM T_SIF_TRAMITES_SIFCOS B
    JOIN T_SIF_ENTIDADES EB
    ON B.ID_ENTIDAD = EB.ID_ENTIDAD 
    WHERE EB.NRO_SIFCOS =  E.NRO_SIFCOS
    AND (B.ID_TIPO_TRAMITE=2 OR (B.ID_TIPO_TRAMITE=4 AND B.NRO_TRAMITE_SIFCOS > T.NRO_TRAMITE_SIFCOS))
)
';


v_UNION:=  ' UNION
SELECT DISTINCT E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_VENCIMIENTO, T.NRO_TRAMITE_SIFCOS, T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA  , tt.N_TIPO_TRAMITE 
 FROM T_SIF_TRAMITES_SIFCOS T
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
JOIN T_SIF_HIST_ESTADO HE
ON HE.ID_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS AND ID_ESTADO_TRAMITE = 6
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND e.ID_SEDE=pe.ID_SEDE
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN 
WHERE T.ID_TIPO_TRAMITE IN (4,1)
AND NOT EXISTS (
    SELECT NRO_TRAMITE_SIFCOS
    FROM T_SIF_TRAMITES_SIFCOS B
    JOIN T_SIF_ENTIDADES EB
    ON B.ID_ENTIDAD = EB.ID_ENTIDAD 
    WHERE EB.NRO_SIFCOS =  E.NRO_SIFCOS
    AND (B.ID_TIPO_TRAMITE=2 OR (B.ID_TIPO_TRAMITE=4 AND B.NRO_TRAMITE_SIFCOS > T.NRO_TRAMITE_SIFCOS))
)
AND T.FEC_VENCIMIENTO < SYSDATE AND E.NRO_SIFCOS <>0 AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || ')
';


--V_GROUP_BY:=' ORDER BY T.FEC_VENCIMIENTO ';           


--Secci?n ordenamiento y paginaci?n
         V_ORDER := ' ORDER BY ';
         if P_ORDER is null then
            V_ORDER := V_ORDER || ' FEC_VENCIMIENTO ' ;
         end if;

         if P_ORDER is not null then
            V_ORDER := V_ORDER || P_ORDER ;
         end if;
  
         V_SELECT := V_SELECT || ' , row_number() over(' || V_ORDER  || ') rn ';

v_SQL:='SELECT * FROM (' || v_SELECT || v_FROM || v_UNION ||  ') where rn between ' ||  P_INICIO ||  ' and ' || P_FINAL;

----dbms_output.put_line(v_SQL);
    OPEN P_RESULTADO FOR
      v_SQL;
 

END pr_get_tramites_reempapen;


--- edicion: (IB) --- fecha: 03/2018
--- calcula la cantidad de altas hist?ricas por organismo logueado
PROCEDURE pr_get_tramites_altashist_tot (
  P_FECHA_DESDE IN date,
  ID_ORGANISMO_LOGUEADO IN NUMBER,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000);
   v_SELECT varchar2(2000);

   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
    v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = ID_ORGANISMO_LOGUEADO
AND PO.ID_LOCALIDAD IS NOT NULL;


 v_SELECT :='SELECT COUNT(*)  Cantidad ';

 v_FROM:=' FROM (
SELECT DISTINCT E.ID_ENTIDAD
FROM T_SIF_TRAMITES_SIFCOS T
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND E.ID_SEDE=pe.ID_SEDE
';

IF (ID_ORGANISMO_LOGUEADO!=1)
THEN
     v_FROM:= v_FROM || 'JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  ID_ORGANISMO_LOGUEADO || '
';
END IF;


v_FROM:= v_FROM || 'WHERE T.ID_TIPO_TRAMITE  =1 AND  E.NRO_SIFCOS <>0 ';

IF (ID_ORGANISMO_LOGUEADO!=1)
THEN
--Buscamos aquellos que tengan domicilio en la localidad
v_FROM:= v_FROM || ' UNION
SELECT DISTINCT E.ID_ENTIDAD
FROM T_SIF_TRAMITES_SIFCOS T
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
WHERE T.ID_TIPO_TRAMITE  =1 AND  E.NRO_SIFCOS <>0 AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || '
) ENTIDADESV
';                
ELSE
    v_FROM:= v_FROM || '
) ENTIDADESV
';
END IF;
        
v_WHERE:=' ';

V_GROUP_BY:=' ';           

  
v_SQL:=v_SELECT || v_FROM ||v_WHERE || V_GROUP_BY ;
----dbms_output.put_line(v_SQL);
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_altashist_tot;

--- edicion: (IB) --- fecha: 03/2018
--- devuelve el listado de altas hist?ricas por organismo logueado
PROCEDURE pr_get_tramites_altashist (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
    P_INICIO in number ,
    P_FINAL in number ,
    P_ORDER in varchar2 ,
  P_RESULTADO OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000); 
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
   V_ORDER varchar2(100);
   v_UNION varchar2(3000);
v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN

    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = P_ID_ORGANISMO
AND PO.ID_LOCALIDAD IS NOT NULL;


-- v_SELECT :='SELECT E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_ALTA,  T.NRO_TRAMITE_SIFCOS, T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA, tt.N_TIPO_TRAMITE  ';
v_SELECT :='SELECT CUIT, NRO_SIFCOS, RAZON_SOCIAL, FEC_ALTA,  NRO_TRAMITE_SIFCOS, ID_ORGANISMO_ALTA, RAZON_SOCIAL BOCA, N_TIPO_TRAMITE ';

 v_FROM:=' FROM (
SELECT E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_ALTA,  T.NRO_TRAMITE_SIFCOS, T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA, tt.N_TIPO_TRAMITE      
FROM T_SIF_TRAMITES_SIFCOS T
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND e.ID_SEDE=pe.ID_SEDE
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
 ';                

IF (P_ID_ORGANISMO!=1)
THEN
     v_FROM:= v_FROM || 'JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  P_ID_ORGANISMO || '
';
END IF;

v_FROM:= v_FROM || ' WHERE T.ID_TIPO_TRAMITE  =1 AND  E.NRO_SIFCOS <>0 
'; 
        
--v_WHERE:='WHERE T.ID_TIPO_TRAMITE  =1 AND  E.NRO_SIFCOS <>0 ';

--V_GROUP_BY:=' ORDER BY T.FEC_VENCIMIENTO ';           

--Buscamos aquellos que tengan domicilio en la localidad
v_UNION:=  ' UNION
SELECT E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_ALTA,  T.NRO_TRAMITE_SIFCOS, T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA, tt.N_TIPO_TRAMITE 
FROM T_SIF_TRAMITES_SIFCOS T
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND e.ID_SEDE=pe.ID_SEDE
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
WHERE T.ID_TIPO_TRAMITE  =1 AND  E.NRO_SIFCOS <>0 AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || ')
';


--Secci?n ordenamiento y paginaci?n
         V_ORDER := ' ORDER BY ';
         if P_ORDER is null then
            V_ORDER := V_ORDER || ' FEC_ALTA ' ;
         end if;

         if P_ORDER is not null then
            V_ORDER := V_ORDER || P_ORDER ;
         end if;
  
         V_SELECT := V_SELECT || ' , row_number() over(' || V_ORDER  || ') rn ';

v_SQL:='SELECT * FROM (' || v_SELECT || v_FROM || v_UNION ||  ') where rn between ' ||  P_INICIO ||  ' and ' || P_FINAL;
----dbms_output.put_line(v_SQL);
    OPEN P_RESULTADO FOR
      v_SQL;
 

END pr_get_tramites_altashist;

--- edicion: (IB) --- fecha: 03/2018
--- calcula la cantidad de altas hist?ricas por organismo logueado
PROCEDURE pr_get_tramites_bajashist_tot (
  P_FECHA_DESDE IN date,
  ID_ORGANISMO_LOGUEADO IN NUMBER,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000);
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
     v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = ID_ORGANISMO_LOGUEADO
AND PO.ID_LOCALIDAD IS NOT NULL;


 v_SELECT :='SELECT COUNT(*)  Cantidad ';

 v_FROM:=' FROM (
SELECT DISTINCT E.ID_ENTIDAD
FROM T_SIF_TRAMITES_SIFCOS T
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND e.ID_SEDE=pe.ID_SEDE
';

IF (ID_ORGANISMO_LOGUEADO!=1)
THEN
     v_FROM:= v_FROM || 'JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  ID_ORGANISMO_LOGUEADO || '
';
END IF;

v_FROM:= v_FROM || 'WHERE T.ID_TIPO_TRAMITE  =2 AND  E.NRO_SIFCOS <>0 
';

IF (ID_ORGANISMO_LOGUEADO!=1)
THEN
--Buscamos aquellos que tengan domicilio en la localidad
v_FROM:= v_FROM || ' UNION
SELECT DISTINCT E.ID_ENTIDAD
FROM T_SIF_TRAMITES_SIFCOS T
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
WHERE T.ID_TIPO_TRAMITE  =2 AND  E.NRO_SIFCOS <>0 AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || '
) ENTIDADESV
';
ELSE
    v_FROM:= v_FROM || '
) ENTIDADESV
';
END IF;        
v_WHERE:=' ';

V_GROUP_BY:=' ';           

  
v_SQL:=v_SELECT || v_FROM ||v_WHERE || V_GROUP_BY ;
----dbms_output.put_line(v_SQL);
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_bajashist_tot;
--- edicion: (IB) --- fecha: 03/2018
--- devuelve el listado de bajas hist?ricas por organismo logueado
PROCEDURE pr_get_tramites_bajashist (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
    P_INICIO in number ,
    P_FINAL in number ,
    P_ORDER in varchar2 ,
  P_RESULTADO OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000); 
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
   V_ORDER varchar2(100);
    v_UNION varchar2(3000);
     v_ID_LOCALIDAD NUMBER;
                                                                                      
BEGIN

    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = P_ID_ORGANISMO
AND PO.ID_LOCALIDAD IS NOT NULL;

-- v_SELECT :='SELECT E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_ALTA,  T.NRO_TRAMITE_SIFCOS , T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA  , tt.N_TIPO_TRAMITE ';
 v_SELECT :='SELECT CUIT, NRO_SIFCOS, RAZON_SOCIAL, FEC_ALTA,  NRO_TRAMITE_SIFCOS , ID_ORGANISMO_ALTA, RAZON_SOCIAL BOCA  , N_TIPO_TRAMITE ';
 
v_FROM:=' FROM (
SELECT E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_ALTA,  T.NRO_TRAMITE_SIFCOS , T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA  , tt.N_TIPO_TRAMITE 
FROM T_SIF_TRAMITES_SIFCOS T
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND e.ID_SEDE=pe.ID_SEDE
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
 ';                

IF (P_ID_ORGANISMO!=1)
THEN
     v_FROM:= v_FROM || 'JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  P_ID_ORGANISMO || '
';
END IF;
        
v_FROM:= v_FROM || 'WHERE T.ID_TIPO_TRAMITE  =2 AND  E.NRO_SIFCOS <>0 ';

--V_GROUP_BY:=' ORDER BY T.FEC_VENCIMIENTO ';           
--Buscamos aquellos que tengan domicilio en la localidad
v_UNION:=  ' UNION
SELECT E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_ALTA,  T.NRO_TRAMITE_SIFCOS , T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA  , tt.N_TIPO_TRAMITE 
FROM T_SIF_TRAMITES_SIFCOS T
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND e.ID_SEDE=pe.ID_SEDE
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
   --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
WHERE T.ID_TIPO_TRAMITE  =2 AND  E.NRO_SIFCOS <>0  AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || ')
';

--Secci?n ordenamiento y paginaci?n
         V_ORDER := ' ORDER BY ';
         if P_ORDER is null then
            V_ORDER := V_ORDER || ' FEC_ALTA ' ;
         end if;

         if P_ORDER is not null then
            V_ORDER := V_ORDER || P_ORDER ;
         end if;
  
         V_SELECT := V_SELECT || ' , row_number() over(' || V_ORDER  || ') rn ';

v_SQL:='SELECT * FROM (' || v_SELECT || v_FROM || v_UNION ||  ') where rn between ' ||  P_INICIO ||  ' and ' || P_FINAL;
----dbms_output.put_line(v_SQL);
    OPEN P_RESULTADO FOR
      v_SQL;
 

END pr_get_tramites_bajashist;
  
--- edicion: (IB) --- fecha: 03/2018
--- calcula la cantidad de comercios activos por organismo logueado
PROCEDURE pr_get_tramites_com_act_tot (
  P_FECHA_DESDE IN date,
  ID_ORGANISMO_LOGUEADO IN NUMBER,
  pResultado OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000);
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
 v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN
    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;
--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = ID_ORGANISMO_LOGUEADO
AND PO.ID_LOCALIDAD IS NOT NULL;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

 v_SELECT :='SELECT COUNT(*)  Cantidad ';

 v_FROM:=' FROM (
SELECT DISTINCT E.ID_ENTIDAD
FROM T_SIF_TRAMITES_SIFCOS t
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND e.ID_SEDE=pe.ID_SEDE
';

IF (ID_ORGANISMO_LOGUEADO!=1)
THEN
     v_FROM:= v_FROM || 'JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  ID_ORGANISMO_LOGUEADO || '
';
END IF;

v_FROM:= v_FROM || 'WHERE T.ID_TIPO_TRAMITE IN (1,4) AND  E.NRO_SIFCOS <>0
AND T.FEC_VENCIMIENTO > SYSDATE 
AND NOT EXISTS (
    SELECT NRO_TRAMITE_SIFCOS
    FROM T_SIF_TRAMITES_SIFCOS B
    JOIN T_SIF_ENTIDADES EB
    ON B.ID_ENTIDAD = EB.ID_ENTIDAD 
    WHERE EB.NRO_SIFCOS =  E.NRO_SIFCOS
    AND B.ID_TIPO_TRAMITE=2 
)
';                

IF (ID_ORGANISMO_LOGUEADO!=1)
THEN
--Buscamos aquellos que tengan domicilio en la localidad
v_FROM:= v_FROM || ' UNION
SELECT DISTINCT E.ID_ENTIDAD
FROM T_SIF_TRAMITES_SIFCOS t
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
WHERE T.ID_TIPO_TRAMITE IN (1,4) AND  E.NRO_SIFCOS <>0
AND T.FEC_VENCIMIENTO > SYSDATE AND  d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || '
AND NOT EXISTS (
    SELECT NRO_TRAMITE_SIFCOS
    FROM T_SIF_TRAMITES_SIFCOS B
    JOIN T_SIF_ENTIDADES EB
    ON B.ID_ENTIDAD = EB.ID_ENTIDAD 
    WHERE EB.NRO_SIFCOS =  E.NRO_SIFCOS
    AND B.ID_TIPO_TRAMITE=2 
)
) ENTIDADESV
'; 
ELSE
    v_FROM:= v_FROM || '
) ENTIDADESV
';
END IF;        
v_WHERE:=' ';

V_GROUP_BY:=' ';           

  
v_SQL:=v_SELECT || v_FROM ||v_WHERE || V_GROUP_BY ;
--dbms_output.put_line(v_SQL);
    OPEN pResultado FOR
      v_SQL;
 

END pr_get_tramites_com_act_tot;

--- edicion: (IB) --- fecha: 03/2018
--- devuelve el listado de comercios activos por organismo logueado
PROCEDURE pr_get_tramites_com_act (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
    P_INICIO in number ,
    P_FINAL in number ,
    P_ORDER in varchar2 ,
  P_RESULTADO OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000); 
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
   V_ORDER varchar2(100);
    v_UNION varchar2(3000);
     v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN

    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = P_ID_ORGANISMO
AND PO.ID_LOCALIDAD IS NOT NULL;


 --v_SELECT :='SELECT DISTINCT  E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_VENCIMIENTO,  T.NRO_TRAMITE_SIFCOS , T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA  , tt.N_TIPO_TRAMITE ';
 v_SELECT :='SELECT   CUIT, NRO_SIFCOS, RAZON_SOCIAL, FEC_VENCIMIENTO,  NRO_TRAMITE_SIFCOS , ID_ORGANISMO_ALTA, BOCA  , N_TIPO_TRAMITE ';

 v_FROM:=' FROM (
SELECT DISTINCT  E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_VENCIMIENTO,  T.NRO_TRAMITE_SIFCOS , T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA  , tt.N_TIPO_TRAMITE 
FROM T_SIF_TRAMITES_SIFCOS t
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND E.ID_SEDE=pe.ID_SEDE
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
 ';                

IF (P_ID_ORGANISMO!=1)
THEN
     v_FROM:= v_FROM || 'JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  P_ID_ORGANISMO || '
';
END IF;
        
v_FROM:= v_FROM || ' WHERE T.ID_TIPO_TRAMITE IN (1,4) AND  E.NRO_SIFCOS <>0
AND T.FEC_VENCIMIENTO > SYSDATE
AND NOT EXISTS (
    SELECT NRO_TRAMITE_SIFCOS
    FROM T_SIF_TRAMITES_SIFCOS B
    JOIN T_SIF_ENTIDADES EB
    ON B.ID_ENTIDAD = EB.ID_ENTIDAD 
    WHERE EB.NRO_SIFCOS =  E.NRO_SIFCOS
    AND B.ID_TIPO_TRAMITE=2 
)
';


--V_GROUP_BY:=' ORDER BY T.FEC_VENCIMIENTO ';           
v_UNION:=  ' UNION
SELECT DISTINCT  E.CUIT, E.NRO_SIFCOS, PE.RAZON_SOCIAL, T.FEC_VENCIMIENTO,  T.NRO_TRAMITE_SIFCOS , T.ID_ORGANISMO_ALTA, po.RAZON_SOCIAL BOCA  , tt.N_TIPO_TRAMITE 
FROM T_SIF_TRAMITES_SIFCOS t
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND e.ID_SEDE=pe.ID_SEDE
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
 WHERE T.ID_TIPO_TRAMITE IN (1,4) AND  E.NRO_SIFCOS <>0
AND NOT EXISTS (
    SELECT NRO_TRAMITE_SIFCOS
    FROM T_SIF_TRAMITES_SIFCOS B
    JOIN T_SIF_ENTIDADES EB
    ON B.ID_ENTIDAD = EB.ID_ENTIDAD 
    WHERE EB.NRO_SIFCOS =  E.NRO_SIFCOS
    AND B.ID_TIPO_TRAMITE=2 
)
AND T.FEC_VENCIMIENTO > SYSDATE AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || ')
';

--Secci?n ordenamiento y paginaci?n
         V_ORDER := ' ORDER BY ';
         if P_ORDER is null then
            V_ORDER := V_ORDER || ' FEC_VENCIMIENTO ' ;
         end if;

         if P_ORDER is not null then
            V_ORDER := V_ORDER || P_ORDER ;
         end if;
  
         V_SELECT := V_SELECT || ' , row_number() over(' || V_ORDER  || ') rn ';

v_SQL:='SELECT * FROM (' || v_SELECT || v_FROM || v_UNION ||  ') where rn between ' ||  P_INICIO ||  ' and ' || P_FINAL;
--dbms_output.put_line(v_SQL);
----dbms_output.put_line(v_SQL);
    OPEN P_RESULTADO FOR
      v_SQL;
 

END pr_get_tramites_com_act;    
--- edicion: (IB) --- fecha: 04/2018
--- Devuelve el listado de reempadronmiientos pendientes por organismo logueado
--- Sin paginaci?n con el objetivo de exportar
PROCEDURE pr_exp_tramites_reempapen (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
  P_RESULTADO OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000); 
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_FROM_COBERTURA varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
   V_ORDER varchar2(100);
    v_UNION varchar2(3000);
v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN

    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = P_ID_ORGANISMO
AND PO.ID_LOCALIDAD IS NOT NULL;

 v_SELECT :='SELECT DISTINCT     E.CUIT, 
    E.NRO_SIFCOS, 
    PE.RAZON_SOCIAL, 
    T.FEC_VENCIMIENTO,
    T.NRO_TRAMITE_SIFCOS,
    T.FEC_INI_TRAMITE,
    T.FEC_ALTA,
    D.N_CALLE, d.ALTURA, D.PISO, d.DEPTO, d.TORRE,'''' as MZNA, '''' as  LOTE, d.N_LOCALIDAD, '''' as cpa,
    po.CUIT CUIT_BOCA, po.RAZON_SOCIAL BOCA , tt.N_TIPO_TRAMITE,
    TELP.NRO_MAIL AS Telefono_principal, TELP.COD_AREA,
    CORREO.NRO_MAIL AS Correo , apri.N_ACTIVIDAD AS ACTIVIDAD_PRI, asec.N_ACTIVIDAD AS ACTIVIDAD_SEC 
';

 v_FROM:=' FROM T_SIF_TRAMITES_SIFCOS T
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
JOIN T_SIF_HIST_ESTADO HE
ON HE.ID_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS AND ID_ESTADO_TRAMITE = 6
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND E.ID_SEDE=pe.ID_SEDE
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
--Tel?fono principal
LEFT JOIN T_COMUNES.VT_COMUNICACIONES TELP
ON TELP.ID_ENTIDAD = TO_CHAR(T.NRO_TRAMITE_SIFCOS)
 AND TELP.ID_TIPO_COMUNICACION=1
--Correo
LEFT JOIN T_COMUNES.VT_COMUNICACIONES CORREO
ON CORREO.ID_ENTIDAD = TO_CHAR(T.NRO_TRAMITE_SIFCOS)
 AND CORREO.ID_TIPO_COMUNICACION=11
--ACTIVIDADES
LEFT JOIN  t_comunes.VT_ACTIVIDADES apri
 ON apri.id_actividad=t.id_actividad_ppal
LEFT JOIN  t_comunes.VT_ACTIVIDADES asec
 ON asec.id_actividad=t.id_actividad_ppal

 ';                

v_FROM_COBERTURA:=  ' ';
IF (P_ID_ORGANISMO!=1)
THEN
     v_FROM_COBERTURA:=  ' JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  P_ID_ORGANISMO || '
';
END IF;
        
v_WHERE:=' WHERE T.ID_TIPO_TRAMITE IN (4,1)
AND NOT EXISTS (
    SELECT NRO_TRAMITE_SIFCOS
    FROM T_SIF_TRAMITES_SIFCOS B
    JOIN T_SIF_ENTIDADES EB
    ON B.ID_ENTIDAD = EB.ID_ENTIDAD 
    WHERE EB.NRO_SIFCOS =  E.NRO_SIFCOS
    AND (B.ID_TIPO_TRAMITE=2 OR (B.ID_TIPO_TRAMITE=4 AND B.NRO_TRAMITE_SIFCOS > T.NRO_TRAMITE_SIFCOS))
)
AND T.FEC_VENCIMIENTO < SYSDATE AND  E.NRO_SIFCOS <>0  ';

--Buscamos aquellos que tengan domicilio en la localidad
v_UNION:= ' UNION   
' || v_SELECT || v_FROM ||  v_WHERE || '
 AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || '
';                

v_SQL:= v_SELECT || v_FROM || v_WHERE;
IF (P_ID_ORGANISMO!=1)
THEN
    v_SQL:= v_SELECT || v_FROM || v_FROM_COBERTURA || v_WHERE || v_UNION;
END IF;
     
--dbms_output.put_line(v_SQL);
    OPEN P_RESULTADO FOR
      v_SQL;
 

END pr_exp_tramites_reempapen;

--- edicion: (IB) --- fecha: 03/2018
--- devuelve el listado de altas hist?ricas por organismo logueado
--- Sin paginaci?n con el objetivo de exportar
PROCEDURE pr_exp_tramites_altashist (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
  P_RESULTADO OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000); 
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_FROM_COBERTURA varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
   V_ORDER varchar2(100);
    v_UNION varchar2(3000);
v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN

    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = P_ID_ORGANISMO
AND PO.ID_LOCALIDAD IS NOT NULL;


 v_SELECT :='SELECT E.CUIT, 
    E.NRO_SIFCOS, 
    PE.RAZON_SOCIAL, 
    T.FEC_VENCIMIENTO,
    T.NRO_TRAMITE_SIFCOS,
    T.FEC_INI_TRAMITE,
    T.FEC_ALTA,
    D.N_CALLE, d.ALTURA, D.PISO, d.DEPTO, d.TORRE, d.MZNA, d.LOTE, d.N_LOCALIDAD, d.cpa,
    po.CUIT CUIT_BOCA, po.RAZON_SOCIAL BOCA, tt.N_TIPO_TRAMITE,
    TELP.NRO_MAIL AS Telefono_principal, TELP.COD_AREA,
    CORREO.NRO_MAIL AS Correo , apri.N_ACTIVIDAD AS ACTIVIDAD_PRI, asec.N_ACTIVIDAD AS ACTIVIDAD_SEC  ';

 v_FROM:='FROM T_SIF_TRAMITES_SIFCOS T
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND E.ID_SEDE=pe.ID_SEDE
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
--Tel?fono principal
LEFT JOIN T_COMUNES.VT_COMUNICACIONES TELP
ON TELP.ID_ENTIDAD = TO_CHAR(T.NRO_TRAMITE_SIFCOS)
 AND TELP.ID_TIPO_COMUNICACION=1
--Correo
LEFT JOIN T_COMUNES.VT_COMUNICACIONES CORREO
ON CORREO.ID_ENTIDAD = TO_CHAR(T.NRO_TRAMITE_SIFCOS)
 AND CORREO.ID_TIPO_COMUNICACION=11
--ACTIVIDADES
LEFT JOIN  t_comunes.VT_ACTIVIDADES apri
 ON apri.id_actividad=t.id_actividad_ppal
LEFT JOIN  t_comunes.VT_ACTIVIDADES asec
 ON asec.id_actividad=t.id_actividad_ppal

 ';                

v_FROM_COBERTURA:=  ' ';
IF (P_ID_ORGANISMO!=1)
THEN
     v_FROM_COBERTURA:=  ' JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  P_ID_ORGANISMO || '
';
END IF;
        
v_WHERE:= 'WHERE T.ID_TIPO_TRAMITE  =1 AND  E.NRO_SIFCOS <>0 ';

--Buscamos aquellos que tengan domicilio en la localidad
v_UNION:= ' UNION   
' || v_SELECT || v_FROM ||  v_WHERE || '
 AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || '
';                

v_SQL:= v_SELECT || v_FROM || v_WHERE;
IF (P_ID_ORGANISMO!=1)
THEN
    v_SQL:= v_SELECT || v_FROM || v_FROM_COBERTURA || v_WHERE || v_UNION;
END IF;
     
--dbms_output.put_line(v_SQL);
    OPEN P_RESULTADO FOR
      v_SQL;
 

END pr_exp_tramites_altashist;

--- edicion: (IB) --- fecha: 05/2018
--- devuelve el listado de bajas hist?ricas por organismo logueado
--- Sin paginaci?n con el objetivo de exportar
PROCEDURE pr_exp_tramites_bajashist (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
  P_RESULTADO OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000); 
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_FROM_COBERTURA varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
   V_ORDER varchar2(100);
    v_UNION varchar2(3000);
v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN

    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = P_ID_ORGANISMO
AND PO.ID_LOCALIDAD IS NOT NULL;

 v_SELECT :='SELECT E.CUIT, 
    E.NRO_SIFCOS, 
    PE.RAZON_SOCIAL, 
    T.FEC_VENCIMIENTO,
    T.NRO_TRAMITE_SIFCOS,
    T.FEC_INI_TRAMITE,
    T.FEC_ALTA,
    D.N_CALLE, d.ALTURA, D.PISO, d.DEPTO, d.TORRE, d.MZNA, d.LOTE, d.N_LOCALIDAD, d.cpa,
    po.CUIT CUIT_BOCA, po.RAZON_SOCIAL BOCA, tt.N_TIPO_TRAMITE,    
    TELP.NRO_MAIL AS Telefono_principal, TELP.COD_AREA,
    CORREO.NRO_MAIL AS Correo , apri.N_ACTIVIDAD AS ACTIVIDAD_PRI, asec.N_ACTIVIDAD AS ACTIVIDAD_SEC  ';

 v_FROM:='FROM T_SIF_TRAMITES_SIFCOS T
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND e.ID_SEDE=pe.ID_SEDE
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
    --Tel?fono principal
    LEFT JOIN T_COMUNES.VT_COMUNICACIONES TELP
    ON TELP.ID_ENTIDAD = TO_CHAR(T.NRO_TRAMITE_SIFCOS)
     AND TELP.ID_TIPO_COMUNICACION=1
    --Correo
    LEFT JOIN T_COMUNES.VT_COMUNICACIONES CORREO
    ON CORREO.ID_ENTIDAD = TO_CHAR(T.NRO_TRAMITE_SIFCOS)
     AND CORREO.ID_TIPO_COMUNICACION=11
--ACTIVIDADES
LEFT JOIN  t_comunes.VT_ACTIVIDADES apri
 ON apri.id_actividad=t.id_actividad_ppal
LEFT JOIN  t_comunes.VT_ACTIVIDADES asec
 ON asec.id_actividad=t.id_actividad_ppal

 ';                

v_FROM_COBERTURA:=  ' ';
IF (P_ID_ORGANISMO!=1)
THEN
     v_FROM_COBERTURA:=  ' JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  P_ID_ORGANISMO || '
';
END IF;
        
v_WHERE:='WHERE T.ID_TIPO_TRAMITE  =2 ';

--Buscamos aquellos que tengan domicilio en la localidad
v_UNION:= ' UNION   
' || v_SELECT || v_FROM ||  v_WHERE || '
 AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || '
';                

v_SQL:= v_SELECT || v_FROM || v_WHERE;
IF (P_ID_ORGANISMO!=1)
THEN
    v_SQL:= v_SELECT || v_FROM || v_FROM_COBERTURA || v_WHERE || v_UNION;
END IF;
   --dbms_output.put_line(v_SQL);  
    OPEN P_RESULTADO FOR
      v_SQL;
 

END pr_exp_tramites_bajashist;

--- edicion: (IB) --- fecha: 05/2018
--- devuelve el listado de comercios activos por organismo logueado
--- Sin paginaci?n con el objetivo de exportar
PROCEDURE pr_exp_tramites_com_act (
  P_FECHA_DESDE IN date,
  P_ID_ORGANISMO IN NUMBER,
  P_RESULTADO OUT sys_refcursor
   )
   IS
   V_SENTENCIA  varchar2(100);
   V_SENTENCIA2 varchar2(100);
   v_SQL  varchar2(7000); 
   v_SELECT varchar2(2000);
   
   v_FROM varchar2(2000);
   v_FROM_COBERTURA varchar2(2000);
   v_WHERE varchar2(2000);
   V_GROUP_BY varchar2(1000);
   V_ORDER varchar2(100);
    v_UNION varchar2(3000);
v_ID_LOCALIDAD NUMBER;
                                                                                          
BEGIN

    V_SENTENCIA := '  ALTER SESSION SET NLS_DATE_FORMAT=' || '''' ||
                   ' DD/MM/YYYY' || '''';
    EXECUTE IMMEDIATE V_SENTENCIA;

    V_SENTENCIA2 := 'ALTER SESSION SET NLS_LANGUAGE=SPANISH';
    EXECUTE IMMEDIATE V_SENTENCIA2;

--Obtenemos la localidad del organismo logueado
SELECT DISTINCT PO.ID_LOCALIDAD INTO v_ID_LOCALIDAD
FROM T_SIF_ORGANISMOS o
   LEFT JOIN t_comunes.vt_domsolo_persjur po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE = po.ID_SEDE
WHERE o.ID_ORGANISMO = P_ID_ORGANISMO
AND PO.ID_LOCALIDAD IS NOT NULL;

 v_SELECT :='SELECT DISTINCT     E.CUIT, 
    E.NRO_SIFCOS, 
    PE.RAZON_SOCIAL, 
    T.FEC_VENCIMIENTO,
    T.NRO_TRAMITE_SIFCOS,
    T.FEC_INI_TRAMITE,
    T.FEC_ALTA,
    D.N_CALLE, d.ALTURA, D.PISO, d.DEPTO, d.TORRE, d.MZNA, d.LOTE, d.N_LOCALIDAD, d.cpa,
    po.CUIT CUIT_BOCA, po.RAZON_SOCIAL BOCA , tt.N_TIPO_TRAMITE,
    TELP.NRO_MAIL AS Telefono_principal, TELP.COD_AREA,
    CORREO.NRO_MAIL AS Correo, apri.N_ACTIVIDAD AS ACTIVIDAD_PRI, asec.N_ACTIVIDAD AS ACTIVIDAD_SEC ';

 v_FROM:='FROM T_SIF_TRAMITES_SIFCOS t
    JOIN T_SIF_TIPOS_TRAMITE tt
    ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
LEFT JOIN T_SIF_ENTIDADES E
ON E.ID_ENTIDAD = T.ID_ENTIDAD
LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS     pe
ON e.CUIT = pe.CUIT
AND E.ID_SEDE=pe.ID_SEDE
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
    --Boca TRAMITE
    LEFT JOIN T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = t.ID_ORGANISMO_ALTA
    LEFT JOIN T_COMUNES.VT_PERS_JURIDICAS po
    ON o.CUIT = po.CUIT
    AND o.ID_SEDE= po.ID_SEDE
    --Tel?fono principal
    LEFT JOIN T_COMUNES.VT_COMUNICACIONES TELP
    ON TELP.ID_ENTIDAD = TO_CHAR(T.NRO_TRAMITE_SIFCOS)
     AND TELP.ID_TIPO_COMUNICACION=1
    --Correo
    LEFT JOIN T_COMUNES.VT_COMUNICACIONES CORREO
    ON CORREO.ID_ENTIDAD = TO_CHAR(T.NRO_TRAMITE_SIFCOS)
     AND CORREO.ID_TIPO_COMUNICACION=11
--ACTIVIDADES
LEFT JOIN  t_comunes.VT_ACTIVIDADES apri
 ON apri.id_actividad=t.id_actividad_ppal
LEFT JOIN  t_comunes.VT_ACTIVIDADES asec
 ON asec.id_actividad=t.id_actividad_ppal

 ';                

v_FROM_COBERTURA:=  ' ';
IF (P_ID_ORGANISMO!=1)
THEN
     v_FROM_COBERTURA:=  ' JOIN T_SIF_ORGANISMOS_COBERTURA oc
ON oc.ID_ORGANISMO_CUBIERTO = T.ID_ORGANISMO_ALTA
AND oc.ID_ORGANISMO=' ||  P_ID_ORGANISMO || '
';
END IF;
        
v_WHERE:='WHERE T.ID_TIPO_TRAMITE IN (1,4) AND  E.NRO_SIFCOS <>0 
AND T.FEC_VENCIMIENTO > SYSDATE
AND NOT EXISTS (
    SELECT NRO_TRAMITE_SIFCOS
    FROM T_SIF_TRAMITES_SIFCOS B
    JOIN T_SIF_ENTIDADES EB
    ON B.ID_ENTIDAD = EB.ID_ENTIDAD 
    WHERE EB.NRO_SIFCOS =  E.NRO_SIFCOS
    AND B.ID_TIPO_TRAMITE=2 
)
';

--Buscamos aquellos que tengan domicilio en la localidad
v_UNION:= ' UNION   
' || v_SELECT || v_FROM ||  v_WHERE || '
 AND d.ID_LOCALIDAD=' ||  v_ID_LOCALIDAD || '
';                

v_SQL:= v_SELECT || v_FROM || v_WHERE;
IF (P_ID_ORGANISMO!=1)
THEN
    v_SQL:= v_SELECT || v_FROM || v_FROM_COBERTURA || v_WHERE || v_UNION;
END IF;
     
--dbms_output.put_line(v_SQL);
    OPEN P_RESULTADO FOR
      v_SQL;
 

END pr_exp_tramites_com_act;

END PCK_SIFCOS_REPORTES;
/
