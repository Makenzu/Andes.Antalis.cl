CREATE   PROCEDURE ido_venta_fisica_cliente_item_paso
	@ano_periodo		INTEGER,
	@mes_periodo		INTEGER,
	@cod_filial		CHAR(03),
	@cod_sucursal		CHAR(03),
	@cod_cliente		CHAR(10),
	@cod_promotora		CHAR(3) = null,
	@tipo			CHAR(3),
	@errorMsg		VARCHAR(255) = null OUTPUT  
AS
	SET NOCOUNT ON
 
	SELECT CLI.nom_cliente,
		CLI.cod_promotora,
		V12M.cod_filial,
		V12M.cod_producto,
		ISNULL(PRO.cod_subfamilia, '000') AS 'cod_subfamilia',
		SF.des_subfamilia,
		ISNULL(PRO.des_producto, '*** PRODUCTO DESCONTINUADO') AS 'des_producto',

		CASE WHEN @tipo = 'UMV' THEN PRO.cod_umv WHEN @tipo = 'UMB' THEN PRO.cod_umb WHEN @tipo = 'VOL' THEN PRO.cod_un_vol END AS 'cod_um',
		
		SUM(V12M.mes_actual) AS 'mes_actual',
		SUM(V12M.mes_uno) AS 'mes_uno',
		SUM(V12M.mes_dos) AS 'mes_dos',
		SUM(V12M.mes_tres) AS 'mes_tres',
		SUM(V12M.mes_cuatro) AS 'mes_cuatro',
		SUM(V12M.mes_cinco) AS 'mes_cinco',
		SUM(V12M.mes_seis) AS 'mes_seis',
		SUM(V12M.mes_siete) AS 'mes_siete',
		SUM(V12M.mes_ocho) AS 'mes_ocho',
		SUM(V12M.mes_nueve) AS 'mes_nueve',
		SUM(V12M.mes_diez) AS 'mes_diez',
		SUM(V12M.mes_once) AS 'mes_once',
		SUM(V12M.mes_doce) AS 'mes_doce',
		SUM(ISNULL(m.val_cantidad,0)) as 'can_mes_act',
		SUM(ISNULL(m.val_venta_pesos,0)) as 'val_mes_act'
		
	FROM VENTAS_CLIENTE_ITEM_12MESES V12M 
	
	INNER JOIN CLIENTES CLI
		ON CLI.ano_periodo = V12M.ano_periodo
		AND CLI.mes_periodo = V12M.mes_periodo
		AND CLI.cod_sucursal = V12M.cod_sucursal
		AND CLI.cod_cliente = V12M.cod_cliente

	LEFT OUTER JOIN PRODUCTOS PRO
		ON PRO.ano_periodo = V12M.ano_periodo
		AND PRO.mes_periodo = V12M.mes_periodo
		AND PRO.cod_filial = V12M.cod_filial
		AND PRO.cod_sucursal = V12M.cod_sucursal
		AND PRO.cod_producto =  V12M.cod_producto

	LEFT OUTER JOIN SUB_FAMILIA SF
		ON SF.cod_subfamilia = PRO.cod_subfamilia
	
	LEFT OUTER JOIN VENTAS_MES_X_ITEM_CLIENTE M with (index = inx_001)-- agregar mes _actual
		ON M.ano_periodo = V12M.ano_periodo
		AND M.mes_periodo = V12M.mes_periodo
		AND M.cod_filial = V12M.cod_filial
		AND M.cod_sucursal= V12M.cod_sucursal
		AND M.cod_producto = V12M.cod_producto
		AND M.cod_cliente = V12M.cod_cliente
	
	WHERE V12M.ano_periodo = @ano_periodo
	AND V12M.mes_periodo = @mes_periodo
	AND V12M.cod_filial = @cod_filial
	AND V12M.cod_sucursal = @cod_sucursal
	AND V12M.cod_cliente = @cod_cliente
	AND V12M.val_tipo = @tipo

	GROUP BY CLI.nom_cliente,
		CLI.cod_promotora,
		V12M.cod_filial,
		V12M.cod_producto,
		PRO.cod_subfamilia,
		SF.des_subfamilia,
		PRO.des_producto,
		PRO.cod_umb,
		PRO.cod_un_vol,
		PRO.cod_umv,
		PRO.cod_umb
	
	ORDER BY V12M.cod_producto


	IF @@RowCount <=  0 
	BEGIN
		SELECT @errorMsg = 'No se encontraron registros para esta consulta.'
		RETURN 1100
	END

	RETURN 0
GO
