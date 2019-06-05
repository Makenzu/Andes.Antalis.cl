CREATE   PROCEDURE ido_venta_fisica_cliente_item_valor_detalle
	@ano_periodo		INTEGER,
	@mes_periodo		INTEGER,
	@cod_filial		CHAR(03),
	@cod_sucursal		CHAR(03),
	@cod_cliente		CHAR(10),
	@cod_producto 		CHAR(12) ,
	@tipo 			CHAR(3),
	@errorMsg		VARCHAR(255) = null OUTPUT  
AS
	SET NOCOUNT ON
	SET @errorMsg = ''

	DECLARE @errores int, @numFilas int 

	IF NOT EXISTS
		(
		SELECT	TOP 1 cod_cliente 
		FROM	clientes
		WHERE	ano_periodo = @ano_periodo
		AND	mes_periodo = @mes_periodo
		AND	cod_cliente = @cod_cliente
		)
	BEGIN 
		SELECT	@errorMsg = 'No se encontró cliente '+ @cod_cliente + 
				   ' para el periodo '+ CAST(@mes_periodo as varchar(2)) +' / ' + CAST(@ano_periodo as varchar(4))
		RETURN 1055
	END

	IF @tipo ='umb'
	BEGIN	
		SELECT	VF.cod_documento,
			VF.fec_documento,
			VF.num_documento,
			VF.num_docto_sap,
			SUM(VF.val_cantidad * D.val_multiplo_venta)  as 'cantidad',
			ISNULL(PRO.cod_umb, '') AS 'cod_um'
		FROM	V_FACTURACION VF
		INNER JOIN DOCUMENTOS D
		ON	D.cod_documento=VF.cod_documento
		INNER JOIN PRODUCTOS PRO
		ON	PRO.ano_periodo		= VF.ano_periodo
		AND	PRO.mes_periodo		= VF.mes_periodo
		AND	PRO.cod_filial		= VF.cod_filial
		AND	PRO.cod_sucursal	= VF.cod_sucursal
		AND	PRO.cod_producto	= VF.cod_producto
		WHERE	VF.ano_periodo		= @ano_periodo
		AND	VF.mes_periodo		= @mes_periodo
		AND	VF.cod_filial		= @cod_filial
		AND	VF.cod_sucursal		= @cod_sucursal
		AND	VF.cod_cliente		= @cod_cliente
		AND	VF.cod_producto		= @cod_producto
		GROUP BY VF.cod_documento,
			 VF.fec_documento,
			 VF.num_documento,
			 VF.num_docto_sap,
			 PRO.cod_umb
	END 
	ELSE
	BEGIN
		SELECT	VF.cod_documento,
			VF.fec_documento,
			VF.num_documento,
			VF.num_docto_sap,
			SUM(VF.val_cantidad* D.val_multiplo_venta * VF.val_volumen)  as 'cantidad',
			ISNULL(PRO.cod_un_vol, '') AS 'cod_um'
		FROM	V_FACTURACION VF
		INNER JOIN DOCUMENTOS D
		ON	D.cod_documento=VF.cod_documento
		INNER JOIN PRODUCTOS PRO
		ON	PRO.ano_periodo		= VF.ano_periodo
		AND	PRO.mes_periodo		= VF.mes_periodo
		AND	PRO.cod_filial		= VF.cod_filial
		AND	PRO.cod_sucursal	= VF.cod_sucursal
		AND	PRO.cod_producto	= VF.cod_producto
		WHERE	VF.ano_periodo		= @ano_periodo
		AND	VF.mes_periodo		= @mes_periodo
		AND	VF.cod_filial		= @cod_filial
		AND	VF.cod_sucursal		= @cod_sucursal
		AND	VF.cod_cliente		= @cod_cliente
		AND	VF.cod_producto		= @cod_producto	
		GROUP BY VF.cod_documento,
			 VF.fec_documento,
			 VF.num_documento,
			 VF.num_docto_sap,
			 PRO.cod_un_vol
	END

	IF @@RowCount <=  0 
	BEGIN
		SELECT	@errorMsg = 'No se encontraron registros para esta consulta.'
		RETURN 1100
	END

	RETURN 0
GO
