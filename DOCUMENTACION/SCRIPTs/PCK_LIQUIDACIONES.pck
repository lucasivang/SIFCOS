CREATE OR REPLACE PACKAGE PCK_LIQUIDACIONES
AS
PROCEDURE "PR_SIF_LIQUIDACIONES_DELETE"
(
		pIdLiquidacion IN NUMBER 
		
 ); 
PROCEDURE "PR_SIF_LIQUIDACIONES_GET" 
    (
		 pIdLiquidacion IN NUMBER default null , 
	 pNroSifcosDesde IN NUMBER default null , 
	 pNroSifcosHasta IN NUMBER default null , 
	 pFecDesde IN DATE default null , 
	 pFecDesdeHasta IN DATE default null , 
	 pFecHasta IN DATE default null , 
	 pFecHastaHasta IN DATE default null , 
	 pIdTipoTramite IN NUMBER default null , 
	 pIdUsuario IN VARCHAR2 default null , 
	 pFecAlta IN DATE default null , 
	 pFecAltaHasta IN DATE default null , 
	 pNroExpediente IN VARCHAR2 default null , 
	 pNroResolucion IN VARCHAR2 default null , 
	 pFechaResolucion IN DATE default null , 
	 pFechaResolucionHasta IN DATE default null ,
	pCursor out types.cursorType
    );
PROCEDURE "PR_SIF_LIQUIDACIONES_INSERT"
(
	pIdLiquidacion OUT NUMBER, 
	pNroSifcosDesde IN NUMBER, 
	pNroSifcosHasta IN NUMBER, 
	pFecDesde IN DATE, 
	pFecHasta IN DATE, 
	pIdTipoTramite IN NUMBER, 
	pIdUsuario IN VARCHAR2, 
	pFecAlta IN DATE, 
	pNroExpediente IN VARCHAR2, 
	pNroResolucion IN VARCHAR2, 
	pFechaResolucion IN DATE
); 
PROCEDURE "PR_SIF_LIQUIDACIONES_UPDATE"
(
	pIdLiquidacion IN NUMBER, 
	pNroSifcosDesde IN NUMBER, 
	pNroSifcosHasta IN NUMBER, 
	pFecDesde IN DATE, 
	pFecHasta IN DATE, 
	pIdTipoTramite IN NUMBER, 
	pIdUsuario IN VARCHAR2, 
	pNroExpediente IN VARCHAR2, 
	pNroResolucion IN VARCHAR2, 
	pFechaResolucion IN DATE
); 

PROCEDURE "PR_SIF_LIQ_ORGANISMOS_DELETE"
(
		pIdLiqOrganismo IN NUMBER 
		
 );
PROCEDURE "PR_SIF_LIQ_ORGANISMOS_GET" 
    (
		 pIdLiqOrganismo IN NUMBER default null , 
	 pIdOrganismo IN NUMBER default null , 
	 pIdLiquidacion IN NUMBER default null , 
	 pTotalLiquidado IN NUMBER default null , 
	 pCantidad IN NUMBER default null ,
    pIdOrganismoSuperior IN NUMBER default null ,
	pCursor out types.cursorType
    );
PROCEDURE "PR_SIF_LIQ_ORGANISMOSUP_GET" 
    (
	 pIdLiquidacion IN NUMBER default null , 
	pCursor out types.cursorType
    );
PROCEDURE "PR_SIF_LIQ_ORGANISMOS_INSERT"
(
	pIdLiqOrganismo OUT NUMBER, 
	pIdOrganismo IN NUMBER, 
	pIdLiquidacion IN NUMBER, 
	pTotalLiquidado IN NUMBER, 
	pCantidad IN NUMBER
);
PROCEDURE "PR_SIF_LIQ_ORGANISMOS_UPDATE"
(
	pIdLiqOrganismo IN NUMBER, 
	pIdOrganismo IN NUMBER, 
	pIdLiquidacion IN NUMBER, 
	pTotalLiquidado IN NUMBER, 
	pCantidad IN NUMBER
);

PROCEDURE "PR_SIF_LIQ_TRAMITES_DELETE"
(
		pIdLiqTramite IN NUMBER 
		
);
PROCEDURE "PR_SIF_LIQ_TRAMITES_GET" 
    (
		 pIdLiqTramite IN NUMBER default null , 
	 pIdLiqOrganismo IN NUMBER default null , 
	 pNroTramiteSifcos IN NUMBER default null , 
	 pIdOrganismo IN NUMBER default null , 
     pIdOrganismoSup IN NUMBER default null ,
     pIdLiquidacion IN NUMBER default null ,
	 pMontoLiquidado IN NUMBER default null ,
	pCursor out types.cursorType
    );
PROCEDURE "PR_SIF_LIQ_TRAMITES_INSERT"
(
	pIdLiqTramite OUT NUMBER, 
	pIdLiqOrganismo IN NUMBER, 
	pNroTramiteSifcos IN NUMBER, 
	pIdOrganismo IN NUMBER, 
	pMontoLiquidado IN NUMBER
);
PROCEDURE "PR_SIF_LIQ_TRAMITES_UPDATE"
(
	pIdLiqTramite IN NUMBER, 
	pIdLiqOrganismo IN NUMBER, 
	pNroTramiteSifcos IN NUMBER, 
	pIdOrganismo IN NUMBER, 
	pMontoLiquidado IN NUMBER
);
PROCEDURE "PR_SIF_LIQ_ALTAS_GET_ULTIMA"
(
    pMaxNroSifcos OUT NUMBER
);
PROCEDURE "PR_SIF_LIQ_ALTAS"
(
	pIdLiquidacion OUT NUMBER, 
	pNroSifcosDesde IN NUMBER, 
	pNroSifcosHasta IN NUMBER, 
	pMensaje OUT VARCHAR2,
    pIdUsuario IN VARCHAR2 default null 
);
PROCEDURE "PR_SIF_LIQ_REEMPA"
(
	pIdLiquidacion OUT NUMBER, 
	pFecHasta IN DATE,
	pMensaje OUT VARCHAR2,
    pIdUsuario IN VARCHAR2 default null 
);
PROCEDURE "PR_SIF_LIQ_DEEPDEL"
(
	pIdLiquidacion IN NUMBER, 
	pMensaje OUT VARCHAR2
);
PROCEDURE "PR_SIF_PREV_LIQ_ALTAS"
(
	pNroSifcosDesde IN NUMBER, 
	pNroSifcosHasta IN NUMBER, 
	pCursor out types.cursorType 
);
PROCEDURE "PR_SIF_PREV_LIQ_REEMPA"
(
	pFecHasta IN DATE,
	pCursor out types.cursorType
    );
END PCK_LIQUIDACIONES;
/
CREATE OR REPLACE PACKAGE BODY PCK_LIQUIDACIONES AS

PROCEDURE "PR_SIF_LIQUIDACIONES_DELETE"
(
		pIdLiquidacion IN NUMBER 
		
 ) IS

BEGIN

    DELETE FROM SIFCOS.T_SIF_LIQUIDACIONES WHERE ID_LIQUIDACION= pIdLiquidacion;
end PR_SIF_LIQUIDACIONES_DELETE;

PROCEDURE PR_SIF_LIQUIDACIONES_GET 
    (
		 pIdLiquidacion IN NUMBER default null , 
	 pNroSifcosDesde IN NUMBER default null , 
	 pNroSifcosHasta IN NUMBER default null , 
	 pFecDesde IN DATE default null , 
	 pFecDesdeHasta IN DATE default null , 
	 pFecHasta IN DATE default null , 
	 pFecHastaHasta IN DATE default null , 
	 pIdTipoTramite IN NUMBER default null , 
	 pIdUsuario IN VARCHAR2 default null , 
	 pFecAlta IN DATE default null , 
	 pFecAltaHasta IN DATE default null , 
	 pNroExpediente IN VARCHAR2 default null , 
	 pNroResolucion IN VARCHAR2 default null , 
	 pFechaResolucion IN DATE default null , 
	 pFechaResolucionHasta IN DATE default null ,
	pCursor out types.cursorType
    ) is
begin

         open pCursor FOR
SELECT 
	ID_LIQUIDACION, 
	NRO_SIFCOS_DESDE, 
	NRO_SIFCOS_HASTA, 
	FEC_DESDE, 
	FEC_HASTA, 
	SIFCOS.T_SIF_TIPOS_TRAMITE.ID_TIPO_TRAMITE, 
	ID_USUARIO, 
	FEC_ALTA, 
	NRO_EXPEDIENTE, 
	NRO_RESOLUCION, 
	FECHA_RESOLUCION,
    SIFCOS.T_SIF_TIPOS_TRAMITE.N_TIPO_TRAMITE
FROM   SIFCOS.T_SIF_LIQUIDACIONES
JOIN SIFCOS.T_SIF_TIPOS_TRAMITE
ON SIFCOS.T_SIF_TIPOS_TRAMITE.ID_TIPO_TRAMITE = SIFCOS.T_SIF_LIQUIDACIONES.ID_TIPO_TRAMITE
WHERE  
	(pIdLiquidacion IS NULL OR ID_LIQUIDACION = pIdLiquidacion) AND 
	(pNroSifcosDesde IS NULL OR NRO_SIFCOS_DESDE = pNroSifcosDesde) AND 
	(pNroSifcosHasta IS NULL OR NRO_SIFCOS_HASTA = pNroSifcosHasta) AND 
	(pFecDesde IS NULL OR FN_COMPARA_FECHAS_MENOR(pFecDesde,FEC_DESDE) = 1 ) AND
	(pFecDesdeHasta IS NULL OR FN_COMPARA_FECHAS_MAYOR(pFecDesdeHasta ,FEC_DESDE) = 1 ) AND 
	(pFecHasta IS NULL OR FN_COMPARA_FECHAS_MENOR(pFecHasta,FEC_HASTA) = 1 ) AND
	(pFecHastaHasta IS NULL OR FN_COMPARA_FECHAS_MAYOR(pFecHastaHasta ,FEC_HASTA) = 1 ) AND 
	(pIdTipoTramite IS NULL OR SIFCOS.T_SIF_TIPOS_TRAMITE.ID_TIPO_TRAMITE = pIdTipoTramite) AND 
	(pIdUsuario IS NULL OR UPPER(ID_USUARIO) LIKE '%' || upper(pIdUsuario) ||'%' ) AND 
	(pFecAlta IS NULL OR FN_COMPARA_FECHAS_MENOR(pFecAlta,FEC_ALTA) = 1 ) AND
	(pFecAltaHasta IS NULL OR FN_COMPARA_FECHAS_MAYOR(pFecAltaHasta ,FEC_ALTA) = 1 ) AND 
	(pNroExpediente IS NULL OR UPPER(NRO_EXPEDIENTE) LIKE '%' || upper(pNroExpediente) ||'%' ) AND 
	(pNroResolucion IS NULL OR UPPER(NRO_RESOLUCION) LIKE '%' || upper(pNroResolucion) ||'%' ) AND 
	(pFechaResolucion IS NULL OR FN_COMPARA_FECHAS_MENOR(pFechaResolucion,FECHA_RESOLUCION) = 1 ) AND
	(pFechaResolucionHasta IS NULL OR FN_COMPARA_FECHAS_MAYOR(pFechaResolucionHasta ,FECHA_RESOLUCION) = 1 );

end PR_SIF_LIQUIDACIONES_GET;

PROCEDURE PR_SIF_LIQUIDACIONES_INSERT
(
	pIdLiquidacion OUT NUMBER, 
	pNroSifcosDesde IN NUMBER, 
	pNroSifcosHasta IN NUMBER, 
	pFecDesde IN DATE, 
	pFecHasta IN DATE, 
	pIdTipoTramite IN NUMBER, 
	pIdUsuario IN VARCHAR2, 
	pFecAlta IN DATE, 
	pNroExpediente IN VARCHAR2, 
	pNroResolucion IN VARCHAR2, 
	pFechaResolucion IN DATE
) IS
    pNextId NUMBER;
BEGIN
   
SELECT SIFCOS.SEQ_SIF_LIQUIDACIONES.NEXTVAL INTO pNextId FROM DUAL;

INSERT INTO SIFCOS.T_SIF_LIQUIDACIONES
(
	ID_LIQUIDACION, 
	NRO_SIFCOS_DESDE, 
	NRO_SIFCOS_HASTA, 
	FEC_DESDE, 
	FEC_HASTA, 
	ID_TIPO_TRAMITE, 
	ID_USUARIO, 
	FEC_ALTA, 
	NRO_EXPEDIENTE, 
	NRO_RESOLUCION, 
	FECHA_RESOLUCION

)   
VALUES 
(
				 pNextId, 
				 pNroSifcosDesde, 
				 pNroSifcosHasta, 
				 pFecDesde, 
				 pFecHasta, 
				 pIdTipoTramite, 
				 pIdUsuario, 
				 pFecAlta, 
				 pNroExpediente, 
				 pNroResolucion, 
				 pFechaResolucion
);    
pIdLiquidacion:=pNextId;
end PR_SIF_LIQUIDACIONES_INSERT;

PROCEDURE PR_SIF_LIQUIDACIONES_UPDATE
(
	pIdLiquidacion IN NUMBER, 
	pNroSifcosDesde IN NUMBER, 
	pNroSifcosHasta IN NUMBER, 
	pFecDesde IN DATE, 
	pFecHasta IN DATE, 
	pIdTipoTramite IN NUMBER, 
	pIdUsuario IN VARCHAR2, 
	pNroExpediente IN VARCHAR2, 
	pNroResolucion IN VARCHAR2, 
	pFechaResolucion IN DATE
) IS

BEGIN
   
UPDATE T_SIF_LIQUIDACIONES
SET 
	ID_LIQUIDACION= pIdLiquidacion, 
	NRO_SIFCOS_DESDE= pNroSifcosDesde, 
	NRO_SIFCOS_HASTA= pNroSifcosHasta, 
	FEC_DESDE= pFecDesde, 
	FEC_HASTA= pFecHasta, 
	ID_TIPO_TRAMITE= pIdTipoTramite, 
	ID_USUARIO= pIdUsuario, 
	NRO_EXPEDIENTE= pNroExpediente, 
	NRO_RESOLUCION= pNroResolucion, 
	FECHA_RESOLUCION= pFechaResolucion
WHERE  
	ID_LIQUIDACION= pIdLiquidacion;
end PR_SIF_LIQUIDACIONES_UPDATE;


PROCEDURE "PR_SIF_LIQ_ORGANISMOS_DELETE"
(
		pIdLiqOrganismo IN NUMBER 
		
 ) IS

BEGIN

    DELETE FROM SIFCOS.T_SIF_LIQ_ORGANISMOS WHERE ID_LIQ_ORGANISMO= pIdLiqOrganismo;
end;

PROCEDURE "PR_SIF_LIQ_ORGANISMOS_GET" 
    (
		 pIdLiqOrganismo IN NUMBER default null , 
	 pIdOrganismo IN NUMBER default null , 
	 pIdLiquidacion IN NUMBER default null , 
	 pTotalLiquidado IN NUMBER default null , 
	 pCantidad IN NUMBER default null ,
    pIdOrganismoSuperior IN NUMBER default null ,
	pCursor out types.cursorType
    ) is
begin

         open pCursor FOR
SELECT 
	lo.ID_LIQ_ORGANISMO, 
	lo.ID_ORGANISMO, 
	lo.ID_LIQUIDACION, 
	lo.TOTAL_LIQUIDADO, 
	lo.CANTIDAD,                                                                                                                     
    po.RAZON_SOCIAL,
o.ID_ORGANISMO_SUPERIOR
FROM   SIFCOS.T_SIF_LIQ_ORGANISMOS lo
    JOIN sifcos.T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = lo.ID_ORGANISMO
    LEFT JOIN T_COMUNES.T_PERS_JURIDICA po
    ON o.CUIT = po.CUIT
WHERE  
	(pIdLiqOrganismo IS NULL OR lo.ID_LIQ_ORGANISMO = pIdLiqOrganismo) AND 
	(pIdOrganismo IS NULL OR lo.ID_ORGANISMO = pIdOrganismo) AND 
	(pIdLiquidacion IS NULL OR lo.ID_LIQUIDACION = pIdLiquidacion) AND 
	(pTotalLiquidado IS NULL OR lo.TOTAL_LIQUIDADO = pTotalLiquidado) AND 
    (pIdOrganismoSuperior IS NULL OR o.ID_ORGANISMO_SUPERIOR = pIdOrganismoSuperior) AND
	(pCantidad IS NULL OR lo.CANTIDAD = pCantidad);

end PR_SIF_LIQ_ORGANISMOS_GET;

PROCEDURE "PR_SIF_LIQ_ORGANISMOSUP_GET" 
    (
	 pIdLiquidacion IN NUMBER default null , 
	pCursor out types.cursorType
    ) is
begin

         open pCursor FOR
SELECT 
licor.ID_ORGANISMO_SUPERIOR AS ID_ORGANISMO,
licor.TOTAL_LIQUIDADO,
licor.CANTIDAD,
posup.RAZON_SOCIAL,
licor.ID_LIQUIDACION
FROM (
    SELECT 
        lo.ID_LIQUIDACION, 
    o.ID_ORGANISMO_SUPERIOR,
        SUM(lo.TOTAL_LIQUIDADO) AS TOTAL_LIQUIDADO , 
        SUM(lo.CANTIDAD) as CANTIDAD
    FROM   SIFCOS.T_SIF_LIQ_ORGANISMOS lo
        JOIN sifcos.T_SIF_ORGANISMOS o
        ON o.ID_ORGANISMO = lo.ID_ORGANISMO
        LEFT JOIN T_COMUNES.T_PERS_JURIDICA po
        ON o.CUIT = po.CUIT
GROUP BY  (       lo.ID_LIQUIDACION, o.ID_ORGANISMO_SUPERIOR)
) licor
 JOIN sifcos.T_SIF_ORGANISMOS o
ON O.ID_ORGANISMO = licor.ID_ORGANISMO_SUPERIOR
    LEFT JOIN T_COMUNES.T_PERS_JURIDICA posup
    ON o.CUIT = posup.CUIT   
WHERE  
	(pIdLiquidacion IS NULL OR licor.ID_LIQUIDACION = pIdLiquidacion); 

END "PR_SIF_LIQ_ORGANISMOSUP_GET";

PROCEDURE "PR_SIF_LIQ_ORGANISMOS_INSERT"
(
	pIdLiqOrganismo OUT NUMBER, 
	pIdOrganismo IN NUMBER, 
	pIdLiquidacion IN NUMBER, 
	pTotalLiquidado IN NUMBER, 
	pCantidad IN NUMBER
) IS
    pNextId NUMBER;
BEGIN
   
SELECT SIFCOS.SEQ_SIF_LIQ_ORGANISMOS.NEXTVAL INTO pNextId FROM DUAL;

INSERT INTO SIFCOS.T_SIF_LIQ_ORGANISMOS
(
	ID_LIQ_ORGANISMO, 
	ID_ORGANISMO, 
	ID_LIQUIDACION, 
	TOTAL_LIQUIDADO, 
	CANTIDAD

)   
VALUES 
(
				 pNextId, 
				 pIdOrganismo, 
				 pIdLiquidacion, 
				 pTotalLiquidado, 
				 pCantidad
);    
pIdLiqOrganismo:=pNextId;
end;


PROCEDURE "PR_SIF_LIQ_ORGANISMOS_UPDATE"
(
	pIdLiqOrganismo IN NUMBER, 
	pIdOrganismo IN NUMBER, 
	pIdLiquidacion IN NUMBER, 
	pTotalLiquidado IN NUMBER, 
	pCantidad IN NUMBER
) IS

BEGIN
   
UPDATE "SIFCOS".T_SIF_LIQ_ORGANISMOS
SET 
	ID_LIQ_ORGANISMO= pIdLiqOrganismo, 
	ID_ORGANISMO= pIdOrganismo, 
	ID_LIQUIDACION= pIdLiquidacion, 
	TOTAL_LIQUIDADO= pTotalLiquidado, 
	CANTIDAD= pCantidad
WHERE  
	ID_LIQ_ORGANISMO= pIdLiqOrganismo;
end;



PROCEDURE "PR_SIF_LIQ_TRAMITES_DELETE"
(
		pIdLiqTramite IN NUMBER 
		
 ) IS

BEGIN

    DELETE FROM SIFCOS.T_SIF_LIQ_TRAMITES WHERE ID_LIQ_TRAMITE= pIdLiqTramite;
end;

PROCEDURE "PR_SIF_LIQ_TRAMITES_GET" 
    (
		 pIdLiqTramite IN NUMBER default null , 
	 pIdLiqOrganismo IN NUMBER default null , 
	 pNroTramiteSifcos IN NUMBER default null , 
	 pIdOrganismo IN NUMBER default null ,
     pIdOrganismoSup IN NUMBER default null ,
     pIdLiquidacion IN NUMBER default null ,
	 pMontoLiquidado IN NUMBER default null ,
	pCursor out types.cursorType
    ) is
begin

         open pCursor FOR
SELECT lt.ID_LIQ_TRAMITE, lt.ID_LIQ_ORGANISMO, lt.NRO_TRAMITE_SIFCOS, lt.ID_ORGANISMO, lt.MONTO_LIQUIDADO, --T_SIF_LIQ_TRAMITES
    e.CUIT, e.NRO_SIFCOS, e.local, e.stand, --T_SIF_ENTIDADES
    pe.RAZON_SOCIAL, --T_PERS_JURIDICA
    t.FEC_INI_TRAMITE, t.FEC_ALTA, t.FEC_VENCIMIENTO, --T_SIF_TRAMITES_SIFCOS
    d.N_CALLE, d.ALTURA, D.PISO, d.DEPTO, d.TORRE, d.MZNA, d.LOTE, d.N_LOCALIDAD, d.cpa, --VT_DOMICILIOS_TODO_MZNA_LOTE
    --Info del organismo superior
    pos.CUIT CUIT_BOCA_SUP, pos.RAZON_SOCIAL BOCA_SUP,
    --Info de la boca
    po.CUIT CUIT_BOCA, po.RAZON_SOCIAL BOCA
FROM SIFCOS.T_SIF_LIQ_TRAMITES lt
JOIN SIFCOS.T_SIF_LIQ_ORGANISMOS lo
ON lt.ID_LIQ_ORGANISMO = lo.ID_LIQ_ORGANISMO
JOIN sifcos.T_SIF_TRAMITES_SIFCOS t
ON t.NRO_TRAMITE_SIFCOS = lt.NRO_TRAMITE_SIFCOS
JOIN sifcos.T_SIF_ENTIDADES e
ON e.ID_ENTIDAD = t.ID_ENTIDAD
    LEFT JOIN T_COMUNES.T_PERS_JURIDICA pe
    ON e.CUIT = pe.CUIT
    --Domicilio
    LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_TODO_MZNA_LOTE d
    ON t.ID_VIN_DOM_LOCAL = d.ID_VIN
    --Boca TRAMITE
    JOIN sifcos.T_SIF_ORGANISMOS o
    ON o.ID_ORGANISMO = lt.ID_ORGANISMO
    LEFT JOIN T_COMUNES.T_PERS_JURIDICA po
    ON o.CUIT = po.CUIT
    --Buscamos el organismo supoerior
    LEFT JOIN sifcos.T_SIF_ORGANISMOS os
    ON os.ID_ORGANISMO = o.ID_ORGANISMO_SUPERIOR
    LEFT JOIN T_COMUNES.T_PERS_JURIDICA pos
    ON os.CUIT = pos.CUIT
WHERE  
	(pIdLiqTramite IS NULL OR lt.ID_LIQ_TRAMITE = pIdLiqTramite) AND 
	(pIdLiqOrganismo IS NULL OR lt.ID_LIQ_ORGANISMO = pIdLiqOrganismo) AND 
	(pNroTramiteSifcos IS NULL OR lt.NRO_TRAMITE_SIFCOS = pNroTramiteSifcos) AND 
	(pIdOrganismo IS NULL OR lt.ID_ORGANISMO = pIdOrganismo) AND
	(pIdOrganismoSup IS NULL OR os.ID_ORGANISMO = pIdOrganismoSup) AND
    (pIdLiquidacion IS NULL OR lo.ID_LIQUIDACION = pIdLiquidacion) AND
	(pMontoLiquidado IS NULL OR lt.MONTO_LIQUIDADO = pMontoLiquidado);

end PR_SIF_LIQ_TRAMITES_GET;

PROCEDURE "PR_SIF_LIQ_TRAMITES_INSERT"
(
	pIdLiqTramite OUT NUMBER, 
	pIdLiqOrganismo IN NUMBER, 
	pNroTramiteSifcos IN NUMBER, 
	pIdOrganismo IN NUMBER, 
	pMontoLiquidado IN NUMBER
) IS
    pNextId NUMBER;
BEGIN
   
SELECT SIFCOS.SEQ_SIF_LIQ_TRAMITES.NEXTVAL INTO pNextId FROM DUAL;

INSERT INTO SIFCOS.T_SIF_LIQ_TRAMITES
(
	ID_LIQ_TRAMITE, 
	ID_LIQ_ORGANISMO, 
	NRO_TRAMITE_SIFCOS, 
	ID_ORGANISMO, 
	MONTO_LIQUIDADO

)   
VALUES 
(
				 pNextId, 
				 pIdLiqOrganismo, 
				 pNroTramiteSifcos, 
				 pIdOrganismo, 
				 pMontoLiquidado
);    
pIdLiqTramite:=pNextId;
end;


PROCEDURE "PR_SIF_LIQ_TRAMITES_UPDATE"
(
	pIdLiqTramite IN NUMBER, 
	pIdLiqOrganismo IN NUMBER, 
	pNroTramiteSifcos IN NUMBER, 
	pIdOrganismo IN NUMBER, 
	pMontoLiquidado IN NUMBER
) IS

BEGIN
   
UPDATE SIFCOS.T_SIF_LIQ_TRAMITES
SET 
	ID_LIQ_TRAMITE= pIdLiqTramite, 
	ID_LIQ_ORGANISMO= pIdLiqOrganismo, 
	NRO_TRAMITE_SIFCOS= pNroTramiteSifcos, 
	ID_ORGANISMO= pIdOrganismo, 
	MONTO_LIQUIDADO= pMontoLiquidado
WHERE  
	ID_LIQ_TRAMITE= pIdLiqTramite;
end;

PROCEDURE "PR_SIF_LIQ_ALTAS_GET_ULTIMA"
(
    pMaxNroSifcos OUT NUMBER
) IS
BEGIN
    SELECT NVL(MAX(NRO_SIFCOS_HASTA),0)+1 MAXNROSIFCOS INTO pMaxNroSifcos
    FROM T_SIF_LIQUIDACIONES
    WHERE ID_TIPO_TRAMITE=1;
END;
--Generación
PROCEDURE "PR_SIF_LIQ_ALTAS"
(
	pIdLiquidacion OUT NUMBER, 
	pNroSifcosDesde IN NUMBER, 
	pNroSifcosHasta IN NUMBER, 
	pMensaje OUT VARCHAR2,
    pIdUsuario IN VARCHAR2 default null 
)
IS
BEGIN

    SELECT SIFCOS.SEQ_SIF_LIQUIDACIONES.NEXTVAL INTO pIdLiquidacion FROM DUAL;

    INSERT INTO SIFCOS.T_SIF_LIQUIDACIONES
(
	ID_LIQUIDACION, 
	NRO_SIFCOS_DESDE, 
	NRO_SIFCOS_HASTA, 
	FEC_DESDE, 
	FEC_HASTA, 
	ID_TIPO_TRAMITE, 
	ID_USUARIO, 
	FEC_ALTA, 
	NRO_EXPEDIENTE, 
	NRO_RESOLUCION, 
	FECHA_RESOLUCION

)   
VALUES 
(
				 pIdLiquidacion, 
				 pNroSifcosDesde, 
				 pNroSifcosHasta, 
				 NULL,  
				 null, 
				 1, 
				 pIdUsuario, 
				 SYSDATE, 
				 NULL, 
				 NULL, 
				 NULL
);


INSERT INTO SIFCOS.T_SIF_LIQ_ORGANISMOS
(
	ID_LIQ_ORGANISMO, 
	ID_ORGANISMO, 
	ID_LIQUIDACION, 
	TOTAL_LIQUIDADO, 
	CANTIDAD
)
SELECT 
				 SIFCOS.SEQ_SIF_LIQ_ORGANISMOS.NEXTVAL, 
				 o2l.ID_ORGANISMO, 
				 pIdLiquidacion, 
				 o2l.TOTAL_LIQUIDADO, 
				 o2l.CANTIDAD
FROM (
        SELECT ID_ORGANISMO, COUNT(*) AS CANTIDAD, SUM(IMPORTE) AS TOTAL_LIQUIDADO
    FROM (
/*
                SELECT nro_tramite_sifcos, 400 AS IMPORTE, nvl(id_organismo_alta,1) AS ID_ORGANISMO 
        FROM t_sif_tramites_sifcos t
        join SIFCOS.T_SIF_ORGANISMOS
        on SIFCOS.T_SIF_ORGANISMOS.ID_ORGANISMO = nvl(id_organismo_alta,1)
        WHERE NRO_TRAMITE_SIFCOS > 273227
*/
        --QUERY ALTA

            SELECT 
            NRO_TRAMITE_SIFCOS,
            FEC_ALTA,
            CASE WHEN id_organismo_alta IS NULL THEN id_organismo ELSE id_organismo_alta END id_organismo,
            IMPORTE,
            nroliquidacionoriginal 
            FROM (
                SELECT DISTINCT
                t.NRO_TRAMITE_SIFCOS,
                t.FEC_ALTA,
                --Info del organismo alta
                t.id_organismo_alta,
                --Info de la Boca verificacion
                u.id_organismo,
                --tasas
                nvl(vt.importe_total,0) AS IMPORTE, 
                vt.nroliquidacionoriginal      
                FROM  sifcos.T_SIF_ENTIDADES e
                JOIN sifcos.T_SIF_TRAMITES_SIFCOS t
                ON e.ID_ENTIDAD = t.ID_ENTIDAD
                JOIN sifcos.T_SIF_TIPOS_TRAMITE tt
                ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
                --Estado del trmite. Filtro por el estado liquidable que es el estado 6
                JOIN (
                    SELECT DISTINCT ID_TRAMITE_SIFCOS, ID_ESTADO_TRAMITE
                    FROM sifcos.T_SIF_HIST_ESTADO 
                ) h
                ON h.ID_TRAMITE_SIFCOS = t.NRO_TRAMITE_SIFCOS
                AND h.ID_ESTADO_TRAMITE=6
                --Buscamos el ultimo estado verificado
                LEFT JOIN (
                    SELECT H.ID_TRAMITE_SIFCOS, MIN(FEC_DESDE_ESTADO) FEC_DESDE_ESTADO
                    FROM sifcos.T_SIF_HIST_ESTADO H
                    WHERE H.ID_ESTADO_TRAMITE IN (4,2)
                --    AND H.ID_TRAMITE_SIFCOS = 252415 
                    GROUP BY H.ID_TRAMITE_SIFCOS
                ) hv1
                ON hv1.ID_TRAMITE_SIFCOS = t.NRO_TRAMITE_SIFCOS
                LEFT JOIN sifcos.T_SIF_HIST_ESTADO hv
                ON HV.ID_TRAMITE_SIFCOS = HV1.ID_TRAMITE_SIFCOS
                AND HV.FEC_DESDE_ESTADO = HV1.FEC_DESDE_ESTADO
                --BOCA VERIFICACION
                LEFT JOIN sifcos.T_SIF_USUARIOS_ORGANISMO u
                ON hv.cuil_usr_cidi = u.cuil_usr_cidi
                    -- tasas
                    JOIN sifcos.t_sif_tasas ts
                    ON ts.nro_tramite_sifcos = t.nro_tramite_sifcos
                    join (  select obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL  
                                FROM TRS.VT_TRANSACCIONES_VERTICALES 
                                union 
                                select  obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL 
                                FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) vt  
                    on vt.obligacion = ts.nro_transaccion
                    and vt.pagado = 'S'
                WHERE NOT EXISTS (
                    --Que no esté liquidado
                    SELECT * 
                    FROM T_SIF_LIQ_TRAMITES 
                    WHERE T_SIF_LIQ_TRAMITES.NRO_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS 
                )
                AND e.NRO_SIFCOS >= pNroSifcosDesde
                AND e.NRO_SIFCOS <= pNroSifcosHasta
                AND t.ID_TIPO_TRAMITE=1
            ) altas

        --FIN QUERY ALTA
                
        ) t2l
    GROUP BY ID_ORGANISMO) o2l;

INSERT INTO T_SIF_LIQ_TRAMITES
(
ID_LIQ_TRAMITE,
ID_LIQ_ORGANISMO,
NRO_TRAMITE_SIFCOS,
ID_ORGANISMO,
MONTO_LIQUIDADO
)
SELECT SEQ_SIF_LIQ_TRAMITES.NEXTVAL,
    LIQ_OR.ID_LIQ_ORGANISMO,
    NRO_TRAMITE_SIFCOS,
    t2l.ID_ORGANISMO,
    t2l.IMPORTE
FROM (
/*
                       SELECT  nro_tramite_sifcos, nvl(id_organismo_alta,1) AS ID_ORGANISMO, 400 AS IMPORTE 
        FROM t_sif_tramites_sifcos t
        join SIFCOS.T_SIF_ORGANISMOS
        on SIFCOS.T_SIF_ORGANISMOS.ID_ORGANISMO = nvl(id_organismo_alta,1)
        WHERE NRO_TRAMITE_SIFCOS > 273227
*/
        --QUERY ALTA

            SELECT 
            NRO_TRAMITE_SIFCOS,
            FEC_ALTA,
            CASE WHEN id_organismo_alta IS NULL THEN id_organismo ELSE id_organismo_alta END id_organismo,
            IMPORTE,
            nroliquidacionoriginal 
            FROM (
                SELECT DISTINCT
                t.NRO_TRAMITE_SIFCOS,
                t.FEC_ALTA,
                --Info del organismo alta
                t.id_organismo_alta,
                --Info de la Boca verificacion
                u.id_organismo,
                --tasas
                nvl(vt.importe_total,0) AS IMPORTE, 
                vt.nroliquidacionoriginal      
                FROM  sifcos.T_SIF_ENTIDADES e
                JOIN sifcos.T_SIF_TRAMITES_SIFCOS t
                ON e.ID_ENTIDAD = t.ID_ENTIDAD
                JOIN sifcos.T_SIF_TIPOS_TRAMITE tt
                ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
                --Estado del trmite. Filtro por el estado liquidable que es el estado 6
                JOIN (
                    SELECT DISTINCT ID_TRAMITE_SIFCOS, ID_ESTADO_TRAMITE
                    FROM sifcos.T_SIF_HIST_ESTADO 
                ) h
                ON h.ID_TRAMITE_SIFCOS = t.NRO_TRAMITE_SIFCOS
                AND h.ID_ESTADO_TRAMITE=6
                --Buscamos el ultimo estado verificado
                LEFT JOIN (
                    SELECT H.ID_TRAMITE_SIFCOS, MIN(FEC_DESDE_ESTADO) FEC_DESDE_ESTADO
                    FROM sifcos.T_SIF_HIST_ESTADO H
                    WHERE H.ID_ESTADO_TRAMITE IN (4,2)
                --    AND H.ID_TRAMITE_SIFCOS = 252415 
                    GROUP BY H.ID_TRAMITE_SIFCOS
                ) hv1
                ON hv1.ID_TRAMITE_SIFCOS = t.NRO_TRAMITE_SIFCOS
                LEFT JOIN sifcos.T_SIF_HIST_ESTADO hv
                ON HV.ID_TRAMITE_SIFCOS = HV1.ID_TRAMITE_SIFCOS
                AND HV.FEC_DESDE_ESTADO = HV1.FEC_DESDE_ESTADO
                --BOCA VERIFICACION
                LEFT JOIN sifcos.T_SIF_USUARIOS_ORGANISMO u
                ON hv.cuil_usr_cidi = u.cuil_usr_cidi
                    -- tasas
                    JOIN sifcos.t_sif_tasas ts
                    ON ts.nro_tramite_sifcos = t.nro_tramite_sifcos
                    join (  select obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL  
                                FROM TRS.VT_TRANSACCIONES_VERTICALES 
                                union 
                                select  obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL 
                                FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) vt  
                    on vt.obligacion = ts.nro_transaccion
                    and vt.pagado = 'S'
                WHERE NOT EXISTS (
                    --Que no esté liquidado
                    SELECT * 
                    FROM T_SIF_LIQ_TRAMITES 
                    WHERE T_SIF_LIQ_TRAMITES.NRO_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS 
                )
                AND e.NRO_SIFCOS >= pNroSifcosDesde
                AND e.NRO_SIFCOS <= pNroSifcosHasta
                AND t.ID_TIPO_TRAMITE=1
            ) altas
        
        --FIN QUERY ALTA

        ) t2l
JOIN (
    SELECT DISTINCT ID_ORGANISMO, ID_LIQ_ORGANISMO
    FROM T_SIF_LIQ_ORGANISMOS
    WHERE ID_LIQUIDACION= pIdLiquidacion
) LIQ_OR
ON t2l.ID_ORGANISMO = LIQ_OR.ID_ORGANISMO;


    pMensaje:='OK';
    COMMIT;
EXCEPTION
  WHEN OTHERS THEN 
    pMensaje:=SQLERRM;
    pIdLiquidacion:=0;
  ROLLBACK;
END;
PROCEDURE "PR_SIF_LIQ_REEMPA"
(
	pIdLiquidacion OUT NUMBER, 
	pFecHasta IN DATE,
	pMensaje OUT VARCHAR2,
    pIdUsuario IN VARCHAR2 default null 
)
IS
v_FecDesde DATE;
BEGIN

    SELECT SIFCOS.SEQ_SIF_LIQUIDACIONES.NEXTVAL INTO pIdLiquidacion FROM DUAL;

    INSERT INTO SIFCOS.T_SIF_LIQUIDACIONES
(
	ID_LIQUIDACION, 
	NRO_SIFCOS_DESDE, 
	NRO_SIFCOS_HASTA, 
	FEC_DESDE, 
	FEC_HASTA, 
	ID_TIPO_TRAMITE, 
	ID_USUARIO, 
	FEC_ALTA, 
	NRO_EXPEDIENTE, 
	NRO_RESOLUCION, 
	FECHA_RESOLUCION

)   
VALUES 
(
				 pIdLiquidacion, 
				 NULL, 
				 NULL, 
				 NULL, --Completar después 
				 pFecHasta, 
				 4, 
				 pIdUsuario, 
				 SYSDATE, 
				 NULL, 
				 NULL, 
				 NULL
);


INSERT INTO SIFCOS.T_SIF_LIQ_ORGANISMOS
(
	ID_LIQ_ORGANISMO, 
	ID_ORGANISMO, 
	ID_LIQUIDACION, 
	TOTAL_LIQUIDADO, 
	CANTIDAD
)
SELECT 
				 SIFCOS.SEQ_SIF_LIQ_ORGANISMOS.NEXTVAL, 
				 o2l.ID_ORGANISMO, 
				 pIdLiquidacion, 
				 o2l.TOTAL_LIQUIDADO, 
				 o2l.CANTIDAD
FROM (
        SELECT ID_ORGANISMO, COUNT(*) AS CANTIDAD, SUM(IMPORTE) AS TOTAL_LIQUIDADO
    FROM (
/*
                SELECT nro_tramite_sifcos, 400 AS IMPORTE, nvl(id_organismo_alta,1) AS ID_ORGANISMO 
        FROM t_sif_tramites_sifcos t
        join SIFCOS.T_SIF_ORGANISMOS
        on SIFCOS.T_SIF_ORGANISMOS.ID_ORGANISMO = nvl(id_organismo_alta,1)
        WHERE NRO_TRAMITE_SIFCOS > 273227
*/
        --QUERY REEMPA

        SELECT  distinct
            t.NRO_TRAMITE_SIFCOS,
            t.FEC_ALTA,
            t.ID_ORGANISMO_ALTA AS ID_ORGANISMO,
            --tasas
            nvl(vt.importe_total,0) AS IMPORTE, 
            vt.nroliquidacionoriginal     
        FROM sifcos.T_SIF_TRAMITES_SIFCOS t
            JOIN sifcos.T_SIF_HIST_ESTADO h
            ON H.ID_TRAMITE_SIFCOS = t.NRO_TRAMITE_SIFCOS
            AND h.ID_ESTADO_TRAMITE=6
            -- tasas
            JOIN sifcos.t_sif_tasas ts
            ON ts.nro_tramite_sifcos = t.nro_tramite_sifcos
            join (  select obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL  
                        FROM TRS.VT_TRANSACCIONES_VERTICALES 
                        union 
                        select  obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL 
                        FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) vt  
            on vt.obligacion = ts.nro_transaccion
            and vt.pagado = 'S'
        WHERE NOT EXISTS (
            --Que no esté liquidado
            SELECT * 
            FROM T_SIF_LIQ_TRAMITES 
            WHERE T_SIF_LIQ_TRAMITES.NRO_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS 
        )
        AND T.FEC_ALTA < pFecHasta
        AND t.ID_TIPO_TRAMITE=4
        AND t.id_organismo_alta is not null
        and t.id_organismo_alta <> 0

        --FIN QUERY REEMPA
                
        ) t2l
    GROUP BY ID_ORGANISMO) o2l;

INSERT INTO T_SIF_LIQ_TRAMITES
(
ID_LIQ_TRAMITE,
ID_LIQ_ORGANISMO,
NRO_TRAMITE_SIFCOS,
ID_ORGANISMO,
MONTO_LIQUIDADO
)
SELECT SEQ_SIF_LIQ_TRAMITES.NEXTVAL,
    LIQ_OR.ID_LIQ_ORGANISMO,
    NRO_TRAMITE_SIFCOS,
    t2l.ID_ORGANISMO,
    t2l.IMPORTE
FROM (
/*
                       SELECT  nro_tramite_sifcos, nvl(id_organismo_alta,1) AS ID_ORGANISMO, 400 AS IMPORTE 
        FROM t_sif_tramites_sifcos t
        join SIFCOS.T_SIF_ORGANISMOS
        on SIFCOS.T_SIF_ORGANISMOS.ID_ORGANISMO = nvl(id_organismo_alta,1)
        WHERE NRO_TRAMITE_SIFCOS > 273227
*/
        --QUERY REEMPA

        SELECT  distinct
            t.NRO_TRAMITE_SIFCOS,
            t.FEC_ALTA,
            t.ID_ORGANISMO_ALTA AS ID_ORGANISMO,
            --tasas
            nvl(vt.importe_total,0) AS IMPORTE, 
            vt.nroliquidacionoriginal     
        FROM sifcos.T_SIF_TRAMITES_SIFCOS t
            JOIN sifcos.T_SIF_HIST_ESTADO h
            ON H.ID_TRAMITE_SIFCOS = t.NRO_TRAMITE_SIFCOS
            AND h.ID_ESTADO_TRAMITE=6
            -- tasas
            JOIN sifcos.t_sif_tasas ts
            ON ts.nro_tramite_sifcos = t.nro_tramite_sifcos
            join (  select obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL  
                        FROM TRS.VT_TRANSACCIONES_VERTICALES 
                        union 
                        select  obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL 
                        FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) vt  
            on vt.obligacion = ts.nro_transaccion
            and vt.pagado = 'S'
        WHERE NOT EXISTS (
            --Que no esté liquidado
            SELECT * 
            FROM T_SIF_LIQ_TRAMITES 
            WHERE T_SIF_LIQ_TRAMITES.NRO_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS 
        )
        AND T.FEC_ALTA < pFecHasta
        AND t.ID_TIPO_TRAMITE=4
        AND t.id_organismo_alta is not null
        and t.id_organismo_alta <> 0

        --FIN QUERY REEMPA

        ) t2l
JOIN (
    SELECT DISTINCT ID_ORGANISMO, ID_LIQ_ORGANISMO
    FROM T_SIF_LIQ_ORGANISMOS
    WHERE ID_LIQUIDACION= pIdLiquidacion
) LIQ_OR
ON t2l.ID_ORGANISMO = LIQ_OR.ID_ORGANISMO;

    SELECT MIN(ts.FEC_ALTA) INTO v_FecDesde
    FROM T_SIF_LIQ_TRAMITES t
    JOIN T_SIF_LIQ_ORGANISMOS o
    ON t.ID_LIQ_ORGANISMO = o.ID_LIQ_ORGANISMO
    JOIN T_SIF_TRAMITES_SIFCOS ts
    ON t.NRO_TRAMITE_SIFCOS = ts.NRO_TRAMITE_SIFCOS
    WHERE o.ID_LIQUIDACION=pIdLiquidacion;

    UPDATE T_SIF_LIQUIDACIONES SET FEC_DESDE=v_FecDesde WHERE ID_LIQUIDACION=pIdLiquidacion;
    pMensaje:='OK';
    COMMIT;
EXCEPTION
  WHEN OTHERS THEN 
    pMensaje:=SQLERRM;
    pIdLiquidacion:=0;
  ROLLBACK;
END;
PROCEDURE "PR_SIF_LIQ_DEEPDEL"
(
	pIdLiquidacion IN NUMBER, 
	pMensaje OUT VARCHAR2
)
IS
BEGIN

delete from
(
SELECT pf.* From T_SIF_LIQ_TRAMITES pf 
WHERE pf.rowid in 
  (
    Select T_SIF_LIQ_TRAMITES.rowid 
    from T_SIF_LIQ_TRAMITES 
    inner join t_sif_liq_organismos 
    on T_SIF_LIQ_TRAMITES.ID_LIQ_ORGANISMO = t_sif_liq_organismos.ID_LIQ_ORGANISMO
    And t_sif_liq_organismos.ID_LIQUIDACION=pIdLiquidacion
  )
); 

DELETE FROM t_sif_liq_organismos WHERE ID_LIQUIDACION=pIdLiquidacion;

DELETE FROM T_SIF_LIQUIDACIONES where ID_LIQUIDACION=pIdLiquidacion;


    pMensaje:='OK';
    COMMIT;
EXCEPTION
  WHEN OTHERS THEN
    pMensaje:=SQLERRM;
  ROLLBACK;
END;
PROCEDURE "PR_SIF_PREV_LIQ_ALTAS"
(
	pNroSifcosDesde IN NUMBER, 
	pNroSifcosHasta IN NUMBER, 
	pCursor out types.cursorType
    ) is
begin

         open pCursor FOR
        --QUERY ALTA

            SELECT
            NRO_SIFCOS, 
            NRO_TRAMITE_SIFCOS,
            FEC_ALTA,
            id_organismo_alta,
            IMPORTE,
            nroliquidacionoriginal 
            FROM (
                SELECT DISTINCT
                e.NRO_SIFCOS,
                t.NRO_TRAMITE_SIFCOS,
                t.FEC_ALTA,
                --Info del organismo alta
                t.id_organismo_alta,
                --Info de la Boca verificacion
                u.id_organismo,
                --tasas
                nvl(vt.importe_total,0) AS IMPORTE, 
                vt.nroliquidacionoriginal      
                FROM  sifcos.T_SIF_ENTIDADES e
                JOIN sifcos.T_SIF_TRAMITES_SIFCOS t
                ON e.ID_ENTIDAD = t.ID_ENTIDAD
                JOIN sifcos.T_SIF_TIPOS_TRAMITE tt
                ON tt.ID_TIPO_TRAMITE = t.ID_TIPO_TRAMITE
                --Estado del trmite. Filtro por el estado liquidable que es el estado 6
                JOIN (
                    SELECT DISTINCT ID_TRAMITE_SIFCOS, ID_ESTADO_TRAMITE
                    FROM sifcos.T_SIF_HIST_ESTADO 
                ) h
                ON h.ID_TRAMITE_SIFCOS = t.NRO_TRAMITE_SIFCOS
                AND h.ID_ESTADO_TRAMITE=6
                --Buscamos el ultimo estado verificado
                LEFT JOIN (
                    SELECT H.ID_TRAMITE_SIFCOS, MIN(FEC_DESDE_ESTADO) FEC_DESDE_ESTADO
                    FROM sifcos.T_SIF_HIST_ESTADO H
                    WHERE H.ID_ESTADO_TRAMITE IN (4,2)
                --    AND H.ID_TRAMITE_SIFCOS = 252415 
                    GROUP BY H.ID_TRAMITE_SIFCOS
                ) hv1
                ON hv1.ID_TRAMITE_SIFCOS = t.NRO_TRAMITE_SIFCOS
                LEFT JOIN sifcos.T_SIF_HIST_ESTADO hv
                ON HV.ID_TRAMITE_SIFCOS = HV1.ID_TRAMITE_SIFCOS
                AND HV.FEC_DESDE_ESTADO = HV1.FEC_DESDE_ESTADO
                --BOCA VERIFICACION
                LEFT JOIN sifcos.T_SIF_USUARIOS_ORGANISMO u
                ON hv.cuil_usr_cidi = u.cuil_usr_cidi
                    -- tasas
                    JOIN sifcos.t_sif_tasas ts
                    ON ts.nro_tramite_sifcos = t.nro_tramite_sifcos
                    join (  select obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL  
                                FROM TRS.VT_TRANSACCIONES_VERTICALES 
                                union 
                                select  obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL 
                                FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) vt  
                    on vt.obligacion = ts.nro_transaccion
                    and vt.pagado = 'S'
                WHERE NOT EXISTS (
                    --Que no esté liquidado
                    SELECT * 
                    FROM T_SIF_LIQ_TRAMITES 
                    WHERE T_SIF_LIQ_TRAMITES.NRO_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS 
                )
                AND e.NRO_SIFCOS >= pNroSifcosDesde
                AND e.NRO_SIFCOS <= pNroSifcosHasta
                AND t.ID_TIPO_TRAMITE=1
            ) altas;
        --FIN QUERY ALTA

/*        
        SELECT distinct e.NRO_SIFCOS, nro_tramite_sifcos,  FEC_ALTA,id_organismo_alta, 0 AS importe, 0 AS nroliquidacionoriginal
        FROM  sifcos.T_SIF_ENTIDADES e
                JOIN sifcos.T_SIF_TRAMITES_SIFCOS t
                ON e.ID_ENTIDAD = t.ID_ENTIDAD 
        join SIFCOS.T_SIF_ORGANISMOS
        on SIFCOS.T_SIF_ORGANISMOS.ID_ORGANISMO = nvl(id_organismo_alta,1)
        WHERE NRO_TRAMITE_SIFCOS > 273227;
  */      
END;
PROCEDURE "PR_SIF_PREV_LIQ_REEMPA"
(
	pFecHasta IN DATE,
	pCursor out types.cursorType
    ) is
begin

         open pCursor FOR
        SELECT  distinct
            e.NRO_SIFCOS,
            t.NRO_TRAMITE_SIFCOS,
            t.FEC_ALTA,
            t.ID_ORGANISMO_ALTA AS ID_ORGANISMO,
            --tasas
            nvl(vt.importe_total,0) AS IMPORTE, 
            vt.nroliquidacionoriginal     
        FROM sifcos.T_SIF_ENTIDADES e
                JOIN sifcos.T_SIF_TRAMITES_SIFCOS t
                ON e.ID_ENTIDAD = t.ID_ENTIDAD 
            JOIN sifcos.T_SIF_HIST_ESTADO h
            ON H.ID_TRAMITE_SIFCOS = t.NRO_TRAMITE_SIFCOS
            AND h.ID_ESTADO_TRAMITE=6
            -- tasas
            JOIN sifcos.t_sif_tasas ts
            ON ts.nro_tramite_sifcos = t.nro_tramite_sifcos
            join (  select obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL  
                        FROM TRS.VT_TRANSACCIONES_VERTICALES 
                        union 
                        select  obligacion, nroliquidacionoriginal, pagado, IMPORTE_TOTAL 
                        FROM TRS.VT_TRANSACCIONES_VERTICALES_MI) vt  
            on vt.obligacion = ts.nro_transaccion
            and vt.pagado = 'S'
        WHERE NOT EXISTS (
            --Que no esté liquidado
            SELECT * 
            FROM T_SIF_LIQ_TRAMITES 
            WHERE T_SIF_LIQ_TRAMITES.NRO_TRAMITE_SIFCOS = T.NRO_TRAMITE_SIFCOS 
        )
        AND T.FEC_ALTA < pFecHasta
        AND t.ID_TIPO_TRAMITE=4
        AND t.id_organismo_alta is not null
        and t.id_organismo_alta <> 0;
        --FIN QUERY REEMPA
        /*
        SELECT e.NRO_SIFCOS nro_tramite_sifcos,  FEC_ALTA,id_organismo_alta, 0 AS importe, 0 AS nroliquidacionoriginal
        FROM  sifcos.T_SIF_ENTIDADES e
                JOIN sifcos.T_SIF_TRAMITES_SIFCOS t
                ON e.ID_ENTIDAD = t.ID_ENTIDAD 
        join SIFCOS.T_SIF_ORGANISMOS
        on SIFCOS.T_SIF_ORGANISMOS.ID_ORGANISMO = nvl(id_organismo_alta,1)
        WHERE NRO_TRAMITE_SIFCOS > 273227
        */
END;
END PCK_LIQUIDACIONES;
/
