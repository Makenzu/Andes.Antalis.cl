Public Class Template
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPContent As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents ddlFilial As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lbUsuario As System.Web.UI.WebControls.Label
    Protected WithEvents lbfilialActual As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Page.IsPostBack = False Then
            If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) = "SI" Then

                Dim tUserInfo As t_Usuario = CType(Session(Constantes.CTE_ANDES_INFO_USUARIO), t_Usuario)
                lbUsuario.Text = tUserInfo.nombre
                With ddlFilial
                    .DataSource = obtieneFiliales(tUserInfo.codigo)
                    .DataTextField = "des_filial"
                    .DataValueField = "cod_filial"
                    .DataBind()
                End With

                ddlFilial.Items.FindByValue(tUserInfo.codigoFilial).Selected = True
                ddlFilial.Enabled = Not (tUserInfo.superUsuario = "N")
            End If
        End If
    End Sub

    Public Function LoadMenu() As String
        Dim menu As String
        Dim tUserInfo As usuario.t_Usuario
        Dim ancho As Int16 = 100

        menu = vbCrLf
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Select Case tUserInfo.perfil
            Case "EJEC"    ' MENU DE EJECUTIVO
                ancho = 120
                menu += vbCrLf & " oCMenu.makeMenu('top0','','&nbsp;Detalle Vtas','','','80','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub01','top0','Venta Cliente - Item','/detalleVenta/vta_x_cliente_item.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub02','top0','Venta Item - Cliente','/detalleVenta/vta_x_item_cliente.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub03','top0','Pedidos x Cliente-Item','/detalleVenta/pedidos_x_cliente_item.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub04','top0','Resumen Cliente','/detalleVenta/resumen_cliente.aspx','','" & ancho & "','18'); "

                ancho = 250
                menu += vbCrLf & " oCMenu.makeMenu('top1','','&nbsp;Análisis Vtas por Clientes','','','132','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub10','top1','Resumen Facturacion por Cliente','/analisisVentaCliente/res_vta_cliente_promo.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub11','top1','Resumen Sin Venta - Cliente','/analisisVentaCliente/res_sinvta_cliente_promo.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub12','top1','Matriz Análisis Venta Cliente','/analisisVentaCliente/vta_matriz_cliente_uni_fisicas.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub13','top1','Ranking Margen Venta Cliente','/analisisVentaCliente/vta_mar_promo_cliente_acu.aspx','','" & ancho & "','18') ;"

                If tUserInfo.codigoFilial = "CHI" Then
                    'menu += vbCrLf & " oCMenu.makeMenu('sub14','top1','Resumen e-Catalogo Cartera Ejec. Comercial','/analisisVentaCliente/res_ecatalogo_promo.aspx','','" & ancho & "','18') ;"
                    'menu += vbCrLf & " oCMenu.makeMenu('sub15','top1','Resumen e-Catalogo Cartera Virtual','/analisisVentaCliente/res_ecatalogo_cvirtual.aspx','','" & ancho & "','18') ;"

                    menu += vbCrLf & " oCMenu.makeMenu('sub15','top1','Resumen ventas e-Catalogo','/analisisVentaCliente/res_ventas_ecatalogo.aspx','','" & ancho & "','18') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('sub16','top1','Análisis de precios x subfamilia','/analisisVentaCliente/analisis_precios_cartera_promotora.aspx','','" & ancho & "','18') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('sub17','top1','Análisis cartera cliente-area-clasificación','/analisisVentaCliente/analisis_cartera_promotora_x_cliente_area_clasificacion.aspx','','" & ancho & "','18') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('sub18','top1','Ventas Acc USD por Clasif. Antalis','/analisisVentaCliente/analisis_cartera_promotora_x_cliente_clasificacion_antalis.aspx','','" & ancho & "','18') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('sub19','top1','Comp. precios nueva política de precios','/analisisVentaCliente/comparativo_precios_cartera_promotora_np.aspx','','" & ancho & "','18') ;"
                End If

                ancho = 198
                menu += vbCrLf & " oCMenu.makeMenu('top2','','&nbsp;Análisis Vtas por Producto','','','145','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub20','top2','Análisis Venta Items','/analisisVentaProducto/vta_x_item.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub21','top2','Análisis Venta Item 12 Meses','/analisisVentaProducto/vta_fisica_item_12mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub22','top2','Análisis Venta Item 06 Meses','/analisisVentaProducto/vta_fisica_item_6mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub23','top2','Análisis Venta Volumen 12 Meses','/analisisVentaProducto/vta_vol_item_12mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub24','top2','Análisis Venta Volumen 06 Meses','/analisisVentaProducto/vta_vol_item_6mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub25','top2','Análisis Venta Consolidado Couché','/analisisVentaProducto/vta_consolidado_couche.aspx','','" & ancho & "','18') ;"
                'menu += vbCrLf & " oCMenu.makeMenu('sub26','top2','Resumen Venta por Marca','/analisisVentaProducto/res_vta_x_marca.aspx','','" & ancho & "','18') ;"
                'menu += vbCrLf & " oCMenu.makeMenu('sub27','top2','Resumen Compra por Marca','/analisisVentaProducto/res_compra_x_marca.aspx','','" & ancho & "','18') ;"

                ancho = 220
                menu += vbCrLf & " oCMenu.makeMenu('top3','','&nbsp;Ctrl de Gestión','','','89','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub30','top3','Resumen de Venta por Vendedora','/controlGestion/res_vta_x_vendedora.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub31','top3','Resumen de Venta por Ejec. Comercial','/controlGestion/res_vta_x_promotora.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub32','top3','Resumen de Facturación ABCD','/controlGestion/vta_res_facturacion_mensual.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub33','top3','Detalle Control Facturación ABCD','/controlGestion/vta_detalle_control_factura.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub34','top3','Descuentos Manuales Modificados','/controlGestion/res_descto_manual.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub35','top3','Análisis Cartera Ejec. Comercial','/controlGestion/analisisCarteraClientePromotora.aspx','','" & ancho & "','18'); "

                Select Case tUserInfo.codigoFilial
                    Case "BOL"
                        menu += vbCrLf & " oCMenu.makeMenu('sub36','top3','Reposición Stock Sta Cruz','/controlGestion/reposicionStock.aspx','','" & ancho & "','18'); "

                        If (tUserInfo.codigo = 501) Or (tUserInfo.codigo = 610) Or (tUserInfo.codigo = 952) Then
                            menu += vbCrLf & " oCMenu.makeMenu('sub37','top3','Mantención Pedido Couche','/controlGestion/man_pedidos.aspx','','" & ancho & "','18'); "
                        End If
                    Case "CHI"
                        menu += vbCrLf & " oCMenu.makeMenu('sub36','top3','Inventario Neto Realizable','/controlGestion/inv_neto_realizable.aspx','','" & ancho & "','18'); "
                        menu += vbCrLf & " oCMenu.makeMenu('sub37','top3','Análisis Frecuencia Compra','/controlGestion/analisisFrecuenciaCompra.aspx','','" & ancho & "','18'); "
                    Case "PER"
                        menu += vbCrLf & " oCMenu.makeMenu('sub36','top3','Inventario Neto Realizable','/controlGestion/inv_neto_realizable.aspx','','" & ancho & "','18'); "
                End Select

                ancho = 115
                menu += vbCrLf & " oCMenu.makeMenu('top4','','&nbsp;Históricos','','','60','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub40','top4','Control de Despacho','/historico/vta_control_despacho.aspx','','" & ancho & "','18'); "
                'menu += vbCrLf & " oCMenu.makeMenu('sub41','top4','Lista de Precios','/historico/listado_productos.aspx','','" & ancho & "','18'); "

                ancho = 175
                menu += vbCrLf & " oCMenu.makeMenu('top5','','&nbsp;Misceláneos','','','71','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub50','top5','Lista de prec. (todas las subfam)','/miscelaneos/listado_productos_total.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub51','top5','Lista de prec. por Subfam.','/miscelaneos/listado_productos.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub52','top5','Clientes por Ejec. Comercial','/miscelaneos/clientes_x_promotora.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub53','top5','Ficha Ingreso Clientes','/catastroClientes/encuesta.aspx','','" & ancho & "','18'); "

                If tUserInfo.codigoFilial = "CHI" Then
                    ancho = 90
                    menu += vbCrLf & " oCMenu.makeMenu('CATRCLI','','&nbsp;Catastro Clientes','','','90','18','','') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('CATRCLI01','CATRCLI','Equipos','/catastroEquipos/default.aspx','','" & ancho & "','18'); "
                End If

            Case "PRMA"     ' MENU DE PRODUCT MANAGER 
                ancho = 120
                menu += vbCrLf & " oCMenu.makeMenu('top0','','&nbsp;Detalle Vtas','','','80','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub01','top0','Venta Cliente - Item','/detalleVenta/vta_x_cliente_item.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub02','top0','Venta Item - Cliente','/detalleVenta/vta_x_item_cliente.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub03','top0','Pedidos por Cliente-Item','/detalleVenta/pedidos_x_cliente_item.aspx','','" & ancho & "','18'); "

                ancho = 200
                menu += vbCrLf & " oCMenu.makeMenu('top2','','&nbsp;Análisis Vtas por Producto','','','135','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub20','top2','Análisis Venta Items','/analisisVentaProducto/vta_x_item.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub21','top2','Análisis Venta Física Item 12 Meses','/analisisVentaProducto/vta_fisica_item_12mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub22','top2','Análisis Venta Física Item 06 Meses','/analisisVentaProducto/vta_fisica_item_6mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub23','top2','Análisis Venta Volumen Item 12 Meses','/analisisVentaProducto/vta_vol_item_12mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub24','top2','Análisis Venta Volumen Item 06 Meses','/analisisVentaProducto/vta_vol_item_6mes.aspx','','" & ancho & "','18') ;"
                'menu += vbCrLf & " oCMenu.makeMenu('sub25','top2','Resumen Venta Por Marca','/analisisVentaProducto/res_vta_x_marca.aspx','','" & ancho & "','18') ;"
                'menu += vbCrLf & " oCMenu.makeMenu('sub26','top2','Resumen Compra por Marca','/analisisVentaProducto/res_compra_x_marca.aspx','','" & ancho & "','18') ;"

                ancho = 115
                menu += vbCrLf & " oCMenu.makeMenu('top4','','&nbsp;Históricos','','','60','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub40','top4','Control de Despacho','/historico/vta_control_despacho.aspx','','" & ancho & "','18'); "

                ancho = 175
                menu += vbCrLf & " oCMenu.makeMenu('top5','','&nbsp;Misceláneos','','','71','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub50','top5','Lista de prec. (todas las subfam)','/miscelaneos/listado_productos_total.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub51','top5','Lista de prec. por Subfam.','/miscelaneos/listado_productos.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub52','top5','Clientes por Ejec. Comercial','/miscelaneos/clientes_x_promotora.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub53','top5','Ficha Ingreso Clientes','/catastroClientes/encuesta.aspx','','" & ancho & "','18'); "

                If tUserInfo.codigoFilial = "CHI" Then
                    ancho = 90
                    menu += vbCrLf & " oCMenu.makeMenu('CATRCLI','','&nbsp;Catastro Clientes','','','90','18','','') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('CATRCLI01','CATRCLI','Equipos','/catastroEquipos/default.aspx','','" & ancho & "','18'); "
                End If
            Case "PROM"  ' MENU DE PROMOTORA
                ancho = 180
                menu += vbCrLf & " oCMenu.makeMenu('top0','','&nbsp;Detalle Vtas','','','80','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub01','top0','Venta Cliente - Item','/detalleVenta/vta_x_cliente_item.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub02','top0','Venta Item - Cliente','/detalleVenta/vta_x_item_cliente.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub03','top0','Pedidos por Cliente-Item','/detalleVenta/pedidos_x_cliente_item.aspx','','" & ancho & "','18'); "
                If tUserInfo.codigoFilial = "CHI" Then
                    menu += vbCrLf & " oCMenu.makeMenu('sub04','top0','Resumen Cliente','/detalleVenta/resumen_cliente.aspx','','" & ancho & "','18'); "
                End If

                ancho = 250
                menu += vbCrLf & " oCMenu.makeMenu('top2','','&nbsp;Análisis Vtas por Clientes','','','132','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub20','top2','Resumen Facturacion por Cliente','/analisisVentaCliente/res_vta_cliente_promo.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub21','top2','Resumen Sin Venta - Cliente','/analisisVentaCliente/res_sinvta_cliente_promo.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub22','top2','Matriz Análisis Venta Cliente','/analisisVentaCliente/vta_matriz_cliente_uni_fisicas.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub23','top2','Ranking Margen Venta Cliente','/analisisVentaCliente/vta_mar_promo_cliente_acu.aspx','','" & ancho & "','18') ;"

                If tUserInfo.codigoFilial = "CHI" Then
                    menu += vbCrLf & " oCMenu.makeMenu('sub24','top2','Resumen ventas e-Catalogo','/analisisVentaCliente/res_ventas_ecatalogo.aspx','','" & ancho & "','18') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('sub25','top2','Análisis de precios x subfamilia','/analisisVentaCliente/analisis_precios_cartera_promotora.aspx','','" & ancho & "','18') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('sub26','top2','Análisis cartera cliente-area-clasificación','/analisisVentaCliente/analisis_cartera_promotora_x_cliente_area_clasificacion.aspx','','" & ancho & "','18') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('sub27','top2','Comp. precios nueva política de precios','/analisisVentaCliente/comparativo_precios_cartera_promotora_np.aspx','','" & ancho & "','18') ;"
                End If


                ancho = 198
                menu += vbCrLf & " oCMenu.makeMenu('top3','','&nbsp;Análisis Vtas por Producto','','','135','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub30','top3','Análisis Venta Física Item 12 Meses','/analisisVentaProducto/vta_fisica_item_12mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub31','top3','Análisis Venta Física Item 06 Meses','/analisisVentaProducto/vta_fisica_item_6mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub32','top3','Análisis Venta Volumen Item 12 Meses','/analisisVentaProducto/vta_vol_item_12mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub33','top3','Análisis Venta Volumen Item 06 Meses','/analisisVentaProducto/vta_vol_item_6mes.aspx','','" & ancho & "','18') ;"

                ancho = 175
                menu += vbCrLf & " oCMenu.makeMenu('top4','','&nbsp;Ctrl de Gestión','','','89','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub40','top4','Resumen de Venta por Ejec. Comercial','/controlGestion/res_vta_x_promotora.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub41','top4','Descuentos Manuales Modificados','/controlGestion/res_descto_manual.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub42','top4','Análisis Cartera Ejec. Comercial','/controlGestion/analisisCarteraClientePromotora.aspx','','" & ancho & "','18'); "

                ancho = 115
                menu += vbCrLf & " oCMenu.makeMenu('top5','','&nbsp;Históricos','','','60','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub50','top5','Control de Despacho','/historico/vta_control_despacho.aspx','','" & ancho & "','18'); "

                ancho = 175
                menu += vbCrLf & " oCMenu.makeMenu('top6','','&nbsp;Misceláneos','','','71','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub60','top6','Lista de prec. (todas las subfam)','/miscelaneos/listado_productos_total.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub61','top6','Lista de prec. por Subfam.','/miscelaneos/listado_productos.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub62','top6','Clientes por Ejec. Comercial','/miscelaneos/clientes_x_promotora.aspx','','" & ancho & "','18'); "

                If tUserInfo.codigoFilial = "CHI" Then
                    ancho = 90
                    menu += vbCrLf & " oCMenu.makeMenu('CATRCLI','','&nbsp;Catastro Clientes','','','90','18','','') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('CATRCLI01','CATRCLI','Equipos','/catastroEquipos/default.aspx','','" & ancho & "','18'); "
                End If
            Case "VEND"   ' MENU DE VENDEDORA
                ancho = 180
                menu += vbCrLf & " oCMenu.makeMenu('top0','','&nbsp;Detalle Vtas','','','80','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub71','top0','Venta Cliente - Item','/detalleVenta/vta_x_cliente_item.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub72','top0','Venta Item - Cliente','/detalleVenta/vta_x_item_cliente.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub73','top0','Pedidos x Cliente-Item','/detalleVenta/pedidos_x_cliente_item.aspx','','" & ancho & "','18'); "


                If tUserInfo.codigoFilial = "CHI" Then
                    menu += vbCrLf & " oCMenu.makeMenu('sub74','top0','Resumen Cliente','/detalleVenta/resumen_cliente.aspx','','" & ancho & "','18'); "
                End If

                ancho = 175
                menu += vbCrLf & " oCMenu.makeMenu('top3','','&nbsp;Ctrl de Gestión','','','89','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub30','top3','Resumen de Venta por Vendedora','/controlGestion/res_vta_x_vendedora.aspx','','" & ancho & "','18'); "


                ancho = 115
                menu += vbCrLf & " oCMenu.makeMenu('top1','','&nbsp;Históricos','','','60','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub80','top1','Control de Despacho','/historico/vta_control_despacho.aspx','','" & ancho & "','18'); "

                ancho = 230
                menu += vbCrLf & " oCMenu.makeMenu('top2','','&nbsp;Misceláneos','','','71','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub90','top2','Lista de prec. (todas las subfam)','/miscelaneos/listado_productos_total.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub91','top2','Lista de prec. x Subfam.','/miscelaneos/listado_productos.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub92','top2','Clientes x Ejec. Comercial','/miscelaneos/clientes_x_promotora.aspx','','" & ancho & "','18'); "

                If tUserInfo.codigoFilial = "CHI" Then
                    ancho = 90
                    menu += vbCrLf & " oCMenu.makeMenu('CATRCLI','','&nbsp;Catastro Clientes','','','90','18','','') ;"
                    menu += vbCrLf & " oCMenu.makeMenu('CATRCLI01','CATRCLI','Equipos','/catastroEquipos/default.aspx','','" & ancho & "','18'); "
                End If

            Case "STPE" 'PERU
                ancho = 120
                menu += vbCrLf & " oCMenu.makeMenu('top0','','&nbsp;Detalle Ventas','','','90','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub01','top0','Venta Cliente - Item','/detalleVenta/vta_x_cliente_item.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub02','top0','Venta Item - Cliente','/detalleVenta/vta_x_item_cliente.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub03','top0','Pedidos por Cliente-Item','/detalleVenta/pedidos_x_cliente_item.aspx','','" & ancho & "','18'); "

                ancho = 165
                menu += vbCrLf & " oCMenu.makeMenu('top1','','&nbsp;Análisis Ventas por Clientes','','','150','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub10','top1','Resumen Facturacion por Cliente','/analisisVentaCliente/res_vta_cliente_promo.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub11','top1','Resumen Sin Venta - Cliente','/analisisVentaCliente/res_sinvta_cliente_promo.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub12','top1','Matriz Análisis Venta Cliente','/analisisVentaCliente/vta_matriz_cliente_uni_fisicas.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub13','top1','Ranking Margen Venta Cliente','/analisisVentaCliente/vta_mar_promo_cliente_acu.aspx','','" & ancho & "','18') ;"

                menu += vbCrLf & " oCMenu.makeMenu('sub24','Ztop2','Resumen ventas e-Catalogo','/analisisVentaCliente/res_ventas_ecatalogo.aspx','','" & ancho & "','18') ;"

                ancho = 200
                menu += vbCrLf & " oCMenu.makeMenu('top2','','&nbsp;Análisis Ventas por Producto','','','160','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub20','top2','Análisis Venta Items','/analisisVentaProducto/vta_x_item.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub21','top2','Análisis Venta Física Item 12 Meses','/analisisVentaProducto/vta_fisica_item_12mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub22','top2','Análisis Venta Física Item 06 Meses','/analisisVentaProducto/vta_fisica_item_6mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub23','top2','Análisis Venta Volumen Item 12 Meses','/analisisVentaProducto/vta_vol_item_12mes.aspx','','" & ancho & "','18') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub24','top2','Análisis Venta Volumen Item 06 Meses','/analisisVentaProducto/vta_vol_item_6mes.aspx','','" & ancho & "','18') ;"
                'menu += vbCrLf & " oCMenu.makeMenu('sub25','top2','Resumen Venta Por Marca','/analisisVentaProducto/res_vta_x_marca.aspx','','" & ancho & "','18') ;"
                'menu += vbCrLf & " oCMenu.makeMenu('sub26','top2','Resumen Compra por Marca','/analisisVentaProducto/res_compra_x_marca.aspx','','" & ancho & "','18') ;"

                ancho = 115
                menu += vbCrLf & " oCMenu.makeMenu('top4','','&nbsp;Históricos','','','75','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub40','top4','Control de Despacho','/historico/vta_control_despacho.aspx','','" & ancho & "','18'); "

                ancho = 175
                menu += vbCrLf & " oCMenu.makeMenu('top5','','&nbsp;Misceláneos','','','105','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub50','top5','Lista de prec. (todas las subfam)','/miscelaneos/listado_productos_total.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub51','top5','Lista de prec. por Subfam.','/miscelaneos/listado_productos.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub52','top5','Clientes por Ejec. Comercial','/miscelaneos/clientes_x_promotora.aspx','','" & ancho & "','18'); "
                menu += vbCrLf & " oCMenu.makeMenu('sub53','top5','Ficha Ingreso Clientes','/catastroClientes/encuesta.aspx','','" & ancho & "','18'); "

                menu += vbCrLf & " oCMenu.makeMenu('top6','','&nbsp;Control de Gestión','','','105','18','','') ;"
                menu += vbCrLf & " oCMenu.makeMenu('sub60','top6','Análisis Cartera Ejec. Comercial','/controlGestion/analisisCarteraClientePromotora.aspx','','" & ancho & "','18'); "
        End Select

        ' EXCEPCIONES 
        If tUserInfo.codigo = 102 Then   'Olga Cecilia de la Maza
            ancho = 160
            menu += vbCrLf & " oCMenu.makeMenu('top100','','&nbsp;Análisis Vtas por Producto','','','135','18','','') ;"
            menu += vbCrLf & " oCMenu.makeMenu('sub101','top100','Análisis Venta Items','/analisisVentaProducto/vta_x_item.aspx','','" & ancho & "','18') ;"

            ancho = 175
            menu += vbCrLf & " oCMenu.makeMenu('top110','','&nbsp;Ctrl de Gestión','','','89','18','','') ;"
            menu += vbCrLf & " oCMenu.makeMenu('sub111','top110','Resumen de Venta x Vendedora','/controlGestion/res_vta_x_vendedora.aspx','','" & ancho & "','18'); "
            menu += vbCrLf & " oCMenu.makeMenu('sub112','top110','Descuentos Manuales Modificados','/controlGestion/res_descto_manual.aspx','','" & ancho & "','18'); "

            menu += vbCrLf & " oCMenu.makeMenu('CATRCLI','','&nbsp;Catastro Clientes','','','90','18','','') ;"
            menu += vbCrLf & " oCMenu.makeMenu('CATRCLI01','CATRCLI','Equipos','/catastroEquipos/default.aspx','','" & ancho & "','18'); "

        End If


        If tUserInfo.codigo = 105 Then  'Antonia Diaz
            ancho = 200
            menu += vbCrLf & " oCMenu.makeMenu('top120','','&nbsp;Análisis Vtas por Producto','','','135','18','','') ;"
            menu += vbCrLf & " oCMenu.makeMenu('sub123','top120','Análisis Venta Items','/AnalisisVentaProducto/vta_x_item.aspx','','" & ancho & "','18') ;"
            menu += vbCrLf & " oCMenu.makeMenu('sub125','top120','Análisis Venta Item 06 Meses','/analisisVentaProducto/vta_fisica_item_6mes.aspx','','" & ancho & "','18') ;"
            menu += vbCrLf & " oCMenu.makeMenu('sub124','top120','Análisis Venta Item 12 Meses','/analisisVentaProducto/vta_fisica_item_12mes.aspx','','" & ancho & "','18') ;"
            menu += vbCrLf & " oCMenu.makeMenu('sub121','top120','Análisis Venta Vol. 06 Meses','/AnalisisVentaProducto/vta_vol_item_6mes.aspx','','" & ancho & "','18') ;"
            menu += vbCrLf & " oCMenu.makeMenu('sub122','top120','Análisis Venta Vol. 12 Meses','/AnalisisVentaProducto/vta_vol_item_12mes.aspx','','" & ancho & "','18') ;"

            menu += vbCrLf & " oCMenu.makeMenu('CATRCLI','','&nbsp;Catastro Clientes','','','90','18','','') ;"
            menu += vbCrLf & " oCMenu.makeMenu('CATRCLI01','CATRCLI','Equipos','/catastroEquipos/default.aspx','','" & ancho & "','18'); "

        End If

        If tUserInfo.codigo = 820 Then
            menu += vbCrLf & " oCMenu.makeMenu('top100','','&nbsp;Ctrl de Gestión','','','89','18','','') ;"
            menu += vbCrLf & " oCMenu.makeMenu('sub1000','top100','Análisis Cartera Ejec. Comercial','/controlGestion/analisisCarteraClientePromotora.aspx','','" & ancho & "','18'); "
        End If

        Dim pu As CPermisoUsuario = Session(Constantes.CTE_OBJ_PERMISOS_USUARIO)
        If pu.validaPermiso(Constantes.MODULO_POTENCIAL_CLIENTE, CPermisoUsuario.eTipoAccion.lectura) Then
            menu += vbCrLf & " oCMenu.makeMenu('top900','','Potencial','/potencial/default.aspx','','70','18','','') ;"
            menu += vbCrLf & " oCMenu.makeMenu('Atop901','top900','Grilla potencial - capacidad','/potencial/grilla.aspx','','140','18','','') ;"
            menu += vbCrLf & " oCMenu.makeMenu('Atop900','top900','Ficha clientes','/potencial/default.aspx','','140','18','','') ;"
        End If

        If pu.validaPermiso(Constantes.MODULO_SEGUIMIENTO_METAS, CPermisoUsuario.eTipoAccion.lectura) Then
            menu += vbCrLf & " oCMenu.makeMenu('top1000','','Metas','','','70','18','','') ;"
            menu += vbCrLf & " oCMenu.makeMenu('top10001','top1000','Seguimiento','/seguimientoMetas/','','140','18','','') ;"
        End If

        If pu.validaPermiso(Constantes.OPMENU_ANALISIS_VENTAS_X_CLIENTE, CPermisoUsuario.eTipoAccion.lectura) Then
            menu += vbCrLf & " oCMenu.makeMenu('MENU_AVXC','','Análisis Vtas por Cliente','','','132','18','','') ;"

            If pu.validaPermiso(Constantes.MODULO_RESUMEN_FACTURACION_X_CLIENTE, CPermisoUsuario.eTipoAccion.lectura) Then
                menu += vbCrLf & " oCMenu.makeMenu('RESFACXCLI','MENU_AVXC','Resumen Facturacion por Cliente','/analisisVentaCliente/res_vta_cliente_promo.aspx','','200','18'); "
            End If

            If pu.validaPermiso(Constantes.MODULO_RESUMEN_ECATALOGO_CARTERA_VIRTUAL, CPermisoUsuario.eTipoAccion.lectura) Then
                menu += vbCrLf & " oCMenu.makeMenu('RESECACARVIR','MENU_AVXC','Resumen e-Catalogo Cartera Virtual','/analisisVentaCliente/res_ecatalogo_cvirtual.aspx','','200','18') ;"
            End If

            If pu.validaPermiso(Constantes.MODULO_RESUMEN_VENTAS_ECATALOGO, CPermisoUsuario.eTipoAccion.lectura) Then
                menu += vbCrLf & " oCMenu.makeMenu('RESVTASECAT','MENU_AVXC','Resumen ventas e-Catalogo','/analisisVentaCliente/res_ventas_ecatalogo.aspx','','200','18') ;"
            End If

            If pu.validaPermiso(Constantes.MODULO_MATRIZ_ANALISIS_VTA_CLI, CPermisoUsuario.eTipoAccion.lectura) Then
                menu += vbCrLf & " oCMenu.makeMenu('MTRANVTACLI ','MENU_AVXC','Matriz Análisis Venta Cliente','/analisisVentaCliente/vta_matriz_cliente_uni_fisicas.aspx','','200','18'); "
            End If

            If pu.validaPermiso(Constantes.MODULO_RANKING_MG_VTA_CLIENTE, CPermisoUsuario.eTipoAccion.lectura) Then
                menu += vbCrLf & " oCMenu.makeMenu('RKMGVTACLI','MENU_AVXC','Ranking Margen Venta Cliente','/analisisVentaCliente/vta_mar_promo_cliente_acu.aspx','','200','18'); "
            End If

        End If

        If tUserInfo.codigoFilial = "CHI" Then
            menu += vbCrLf & " oCMenu.makeMenu('AC10','','Stock & Compras','','','100','18','','')"
            menu += vbCrLf & " oCMenu.makeMenu('AC101','AC10','Análisis stock & compras','/AnalisisCompra/AnalisisCompraStock.aspx ','','140','18','','')"
        End If


        Return menu
    End Function

    Private Sub ddlFilial_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFilial.SelectedIndexChanged
        Dim tUserInfo As usuario.t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        tUserInfo.codigoFilial = Request("ddlFilial")

        If tUserInfo.codigoFilial = "CHI" Then
            tUserInfo.codigoSucursal = "001"

        ElseIf tUserInfo.codigoFilial = "PER" Then
            tUserInfo.codigoSucursal = "002"

        ElseIf tUserInfo.codigoFilial = "BOL" Then
            tUserInfo.codigoSucursal = "003"
        ElseIf tUserInfo.codigoFilial = "PCL" Then
            tUserInfo.codigoSucursal = "P01"
        End If

        Session(Constantes.CTE_ANDES_FILIAL_ACTIVA) = Request("ddlFilial")

        Session(Constantes.CTE_ANDES_INFO_USUARIO) = tUserInfo
        Response.Redirect(Request.ServerVariables("HTTP_REFERER"))
    End Sub
End Class
