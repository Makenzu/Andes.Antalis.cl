if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ido_lista_precios]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ido_lista_precios]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE ido_lista_precios
	@ano_periodo	 INTEGER,
	@mes_periodo	 INTEGER,
	@cod_filial CHAR(3) = 'CHI',
	@cod_sucursal CHAR(3) = '001',
	@cod_familia	 CHAR(3),
	@cod_subfamilia CHAR(300),
	@nom_tabla CHAR(300)
AS
	DECLARE @Qry as CHAR(3500)
	SET NOCOUNT ON

	IF @cod_subfamilia=''
		BEGIN
		SELECT FAM.cod_familia + ' - ' + FAM.des_familia, 
			SFAM.cod_subfamilia + ' - ' + SFAM.des_subfamilia, 
			PRO.cod_producto, 
			PRO.des_producto, 
			PRO.cod_umb, 
			PRO.val_precio_lista, 
			INV.val_stock_actual, 
			INV.val_cant_pend, 
			INV.val_stock_transito,
			isnull(ido_usuarios.nom_usuario,'')
		FROM PRODUCTOS PRO
		INNER JOIN INVENTARIOS INV
			ON INV.ano_periodo = PRO.ano_periodo 
			AND INV.mes_periodo = PRO.mes_periodo
			AND INV.cod_filial = PRO.cod_filial 
			AND INV.cod_sucursal = PRO.cod_sucursal
			AND INV.cod_producto = PRO.cod_producto
		INNER JOIN FAMILIAS FAM 
			ON  FAM.cod_familia = PRO.cod_familia
		INNER JOIN SUB_FAMILIA SFAM
			ON SFAM.cod_familia = PRO.cod_familia
			AND SFAM.cod_subfamilia = PRO.cod_subfamilia 
		left join ido_subfamilia_prodmanager 
			on SFAM.cod_subfamilia = ido_subfamilia_prodmanager.cod_subfamilia
		left join ido_usuarios 
			on ido_subfamilia_prodmanager.cod_usuario=ido_usuarios.cod_usuario
		WHERE PRO.ano_periodo=@ano_periodo
		AND PRO.mes_periodo=@mes_periodo
		AND PRO.cod_filial=@cod_filial
		AND PRO.cod_sucursal=@cod_sucursal
		AND PRO.cod_familia=@cod_familia
		ORDER BY PRO.cod_familia, 
			PRO.cod_subfamilia, 
			PRO.cod_producto
	END
	ELSE
		BEGIN

		set @cod_subfamilia = REPLACE(@cod_subfamilia,'|','''')

		SET @Qry='SELECT familias.cod_familia +'' - '' +familias.des_familia, sub_familia.cod_subfamilia + '' - '' + sub_familia.des_subfamilia, 
			productos.cod_producto, productos.des_producto, productos.cod_umb, productos.val_precio_lista, 
			inventarios.val_stock_actual, inventarios.val_cant_pend, inventarios.val_stock_transito,
			isnull(ido_usuarios.nom_usuario,'''')
		FROM productos
			left join inventarios on productos.ano_periodo=inventarios.ano_periodo and productos.mes_periodo=inventarios.mes_periodo
				and productos.cod_filial=inventarios.cod_filial and productos.cod_sucursal=inventarios.cod_sucursal
				and productos.cod_producto=inventarios.cod_producto
			left join familias on  productos.cod_familia=familias.cod_familia
			left join sub_familia on productos.cod_subfamilia=sub_familia.cod_subfamilia and familias.cod_familia=sub_familia.cod_familia
			left join ido_subfamilia_prodmanager on sub_familia.cod_subfamilia=ido_subfamilia_prodmanager.cod_subfamilia
			left join ido_usuarios on ido_subfamilia_prodmanager.cod_usuario=ido_usuarios.cod_usuario
		WHERE productos.ano_periodo=' + ltrim(rtrim(str(@ano_periodo))) + '
			and productos.mes_periodo=' + ltrim(rtrim(str(@mes_periodo))) + '
			and productos.cod_filial=''' + @cod_filial + ''' 
			and productos.cod_sucursal=''' + @cod_sucursal + '''
			and productos.cod_familia=''' + @cod_familia + '''
			and productos.cod_subfamilia in(' + ltrim(rtrim(@cod_subfamilia)) + ')
		ORDER BY productos.cod_familia, productos.cod_subfamilia, productos.cod_producto'
		set @Qry=ltrim(rtrim(@Qry))
		EXEC (@Qry)
		END

	SELECT fec_ult_act as FECHA
	FROM control_carga_tbase
	WHERE ano_periodo=@ano_periodo
		and mes_periodo=@mes_periodo
		and cod_filial=@cod_filial
		and cod_sucursal=@cod_sucursal
		and nom_tabla=@nom_tabla
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

