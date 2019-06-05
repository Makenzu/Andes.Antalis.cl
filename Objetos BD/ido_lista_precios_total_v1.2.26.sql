if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ido_lista_precios_total]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ido_lista_precios_total]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE ido_lista_precios_total
	@ano_periodo	INTEGER,
	@mes_periodo	INTEGER,
	@cod_sucursal	CHAR(3)
	
AS
	SET NOCOUNT ON

	SELECT	PRO.cod_familia,
		PRO.cod_subfamilia,
		PRO.cod_producto,
		PRO.des_producto,
		PRO.cod_umb,
		PRO.val_precio_lista,
		ISNULL(I.val_stock_Actual,0) 'val_stock_Actual', 
		ISNULL(I.val_cant_pend,0) 'val_cant_pend',
		ISNULL(I.val_stock_transito,0) 'val_stock_transito'
	FROM	productos PRO
	LEFT OUTER JOIN inventarios i
	ON	i.ano_periodo		= pro.ano_periodo
	AND	i.mes_periodo		= pro.mes_periodo
	AND	i.cod_filial		= pro.cod_filial
	AND	i.cod_sucursal		= pro.cod_sucursal
	AND	i.cod_producto		= pro.cod_producto
	WHERE	PRO.ano_periodo		= @ano_periodo
	AND	PRO.mes_periodo		= @mes_periodo
	AND	PRO.cod_sucursal	= @cod_sucursal
	AND	PRO.cod_familia		<> ''
	ORDER BY PRO.cod_filial,
		 PRO.cod_sucursal,
		 PRO.cod_producto
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

