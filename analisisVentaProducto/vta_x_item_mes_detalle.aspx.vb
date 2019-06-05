Public Class vta_x_item_detalle
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents lbProducto As System.Web.UI.WebControls.Label
    Protected WithEvents lbDescripcion As System.Web.UI.WebControls.Label
    Protected WithEvents tbResultado As System.Web.UI.WebControls.Table
    Protected WithEvents lbStock As System.Web.UI.WebControls.Label
    Protected WithEvents lbPend As System.Web.UI.WebControls.Label

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

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        If Request("ms") = "" Or Request("an") = "" Or Request("cp") = "" Then
            lbErrors.Text = "Faltan datos para ejecutar esta consulta."
            lbErrors.Visible = True
            Response.End()
        End If

        Dim mes_periodo As Integer = CInt(Request("ms"))
        Dim ano_periodo As Integer = CInt(Request("an"))
        Dim cod_producto As String = Request("cp")

        Dim tUserInfo As usuario.t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        Dim cod_filial As String = tUserInfo.codigoFilial
        Dim cod_sucursal As String = tUserInfo.codigoSucursal

        Dim dtMarVta As New DataTable

        lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo

        Try

            lbErrors.Text = ""

            ' CLEAR DATATABLE
            tbResultado.DataBind()

            ' LOAD DATAGRID
            dtMarVta = ventas.info_vta_item_mes(cod_producto, mes_periodo, ano_periodo, cod_filial, cod_sucursal)
            If dtMarVta.Rows.Count <= 0 Then
                Err.Description = "No se encontraron datos para esta consulta."
                Err.Raise(vbObjectError + 512 + 10, "no_results", Err.Description)
            End If
            Report_Create(dtMarVta, mes_periodo, ano_periodo)


        Catch ex As Exception
            ' SHOW ERROR
            lbErrors.Text = "ERROR: " & Err.Description
            lbErrors.Visible = True
            Err.Clear()
        Finally
            dtMarVta.Dispose()
        End Try

    End Sub

    Private Sub Report_Create(ByVal dtResult As DataTable, ByVal mes_periodo As Integer, ByVal ano_periodo As Integer)

        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim tcValores As New TableCell
        Dim trColumnas As New TableRow
        Dim trDatos As New TableRow

        lbDescripcion.Text = dtResult.Rows(0).Item("des_producto")
        lbProducto.Text = dtResult.Rows(0).Item("cod_producto")

        ' CREATE HEADER -- START--
        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = ""
        tcValores.ColumnSpan = 1
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "-- MES --"
        tcValores.Style.Add("font-size", "11px")
        tcValores.Style.Add("font-weight", "bold")
        tcValores.ColumnSpan = 3
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "-- AÑO --"
        tcValores.Style.Add("font-size", "11px")
        tcValores.Style.Add("font-weight", "bold")
        tcValores.ColumnSpan = 3
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        tbResultado.Rows.Add(trColumnas)

        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = ""
        tcValores.Width = New Unit(90, UnitType.Pixel)
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Física"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = " US$ "
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "&nbsp; $ &nbsp;"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Física"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = " US $"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "&nbsp; $ &nbsp;"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        trColumnas.CssClass = "tbl-DataGridHeader"
        tbResultado.Rows.Add(trColumnas)

        ' CREATE HEADER -- END--



        ' CREATE ITEMS -- START --

        '    -- VENTAS
        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = "Ventas"
        tcValores.CssClass = "tbl-HorizHeader"
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_venta_fisica_mes"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_venta_dolar_mes"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_venta_pesos_mes"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_venta_fisica_ano"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_venta_dolar_ano"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_venta_pesos_ano"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tbResultado.Rows.Add(trColumnas)


        '    -- COMPRAS
        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = "Compras"
        tcValores.CssClass = "tbl-HorizHeader"
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_compra_fisica_mes"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_compra_dolar_mes"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = " "
        tcValores.Style.Add("background-color", "#eeeeee")
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_compra_fisica_ano"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_compra_dolar_ano"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = " "
        tcValores.Style.Add("background-color", "#eeeeee")
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tbResultado.Rows.Add(trColumnas)


        '    -- DEVOLUCIONES
        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = "Devol"
        tcValores.CssClass = "tbl-HorizHeader"
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_devolucion_fisica_mes"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = " "
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.Style.Add("background-color", "#eeeeee")
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_devolucion_pesos_mes"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_devolucion_fisica_ano"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = " "
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.Style.Add("background-color", "#eeeeee")
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_devolucion_pesos_ano"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tbResultado.Rows.Add(trColumnas)



        '    -- COSTOS
        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = "Costos"
        tcValores.CssClass = "tbl-HorizHeader"
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = " "
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.Style.Add("background-color", "#eeeeee")
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_costo_dolar_mes"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_costo_pesos_mes"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = " "
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.Style.Add("background-color", "#eeeeee")
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_costo_dolar_ano"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("val_costo_pesos_ano"))
        tcValores.CssClass = "tbl-HorizItem"
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tbResultado.Rows.Add(trColumnas)

        ' CREATE ITEMS -- END --

        lbStock.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("stock_actual"))
        lbPend.Text = String.Format(cl, "{0:N0}", dtResult.Rows(0).Item("pedidos_pendientes"))

    End Sub
End Class
