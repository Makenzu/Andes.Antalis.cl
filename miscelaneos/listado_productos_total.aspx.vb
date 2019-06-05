
Imports Exportador
Public Class listado_productos_total
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents ibExportar2 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ddlMeses As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlanos As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btenvia As System.Web.UI.HtmlControls.HtmlInputImage
    Protected WithEvents mitabla As System.Web.UI.WebControls.Table
    Protected WithEvents ddlSucursal As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim msgConsultaSap As String
    Dim ano_periodo As String
    Dim mes_periodo As String
    Dim tUserInfo As usuario.t_Usuario


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        Dim Datos As DataTable
        Dim Año As Integer
        Dim Mes As Integer
        Dim i As Integer

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        'AGS-20080620: Agregar listbox con sucursales
        If Not IsPostBack Then
            ddlSucursal.DataSource = Utiles.obtieneSucursales(tUserInfo.codigoFilial)
            ddlSucursal.DataTextField = "des_sucursal"
            ddlSucursal.DataValueField = "cod_sucursal"
            ddlSucursal.DataBind()
        End If

        Año = Year(Now)
        Mes = Month(Now)
        lbErrors.Text = ""
        If ddlanos.SelectedValue = "" Then
            ddlanos.Items.Add(Año)
            ddlanos.Items(0).Value = Año
            ddlanos.Items.Add(Año - 1)
            ddlanos.Items(1).Value = Año - 1
            ddlanos.Items.Add(Año - 2)
            ddlanos.Items(2).Value = Año - 2
            ddlMeses.SelectedValue = Mes
            Label4.Text = ""
        End If
    End Sub

    Private Sub btenvia_ServerClick(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btenvia.ServerClick
        Dim Resultados As DataTable
        Dim encabezado As TableRow
        Dim DSResultados As DataSet
        Dim cod_sucursal As String
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        'tUserInfo.codigoFilial = Request("ddlFilial")

        'AGS-20080620: Agregar listbox con sucursales
        tUserInfo.codigoSucursal = ddlSucursal.SelectedValue

        'If tUserInfo.codigoFilial = "CHI" Then
        '    tUserInfo.codigoSucursal = "001"

        'ElseIf tUserInfo.codigoFilial = "PER" Then
        '    tUserInfo.codigoSucursal = "002"

        'ElseIf tUserInfo.codigoFilial = "BOL" Then
        '    tUserInfo.codigoSucursal = "003"
        'End If

        ano_periodo = ddlanos.SelectedValue
        mes_periodo = ddlMeses.SelectedValue

        If ano_periodo = 2005 And mes_periodo < 8 Then
            lbErrors.Text = "No se puede obtener información para el período seleccionado."
            Exit Sub
        End If

        DSResultados = Utiles.buscaListaPreciosTotal(ano_periodo, mes_periodo, tUserInfo.codigoFilial, tUserInfo.codigoSucursal)
        Resultados = DSResultados.Tables(0)

        If Resultados.Rows.Count = 0 Then
            lbErrors.Text = "No se encontraron items."
            Exit Sub
        End If

        '------------------------------------------------------------------------------------------------------
        'Intentamos recoger indicadores de stock y pedidos pendientes desde SAP
        'Si la consulta se realiza en mes abierto, entonces obtenemos valores de pedidos pendientes y de stock
        'desde SAP.
        '------------------------------------------------------------------------------------------------------

        'If mes_periodo = Now.Month Then
        '    Dim tblStockMaterial As DataTable = New DataTable

        '    Try
        '        Dim sociedad, centro As String

        '        'Segun el código de sucursal, obtenemos equivalencia de sociedad y centro de distribución.
        '        seteaParametrosConsulta(tUserInfo.codigoSucursal, sociedad, centro)

        '        'consultamos a SAP stock y pedidos pendientes expresados en unidad de medida base
        '        tblStockMaterial = materiales.obtieneStockMateriales(sociedad, centro, Resultados)

        '        'modificamos los valores de la ultima carga de datos Andes/Iddeo, con info actualizada de 
        '        'stock y pedidos pendientes.
        '        Dim i As Integer
        '        For i = 0 To Resultados.Rows.Count - 1
        '            If Trim(Resultados.Rows(i).Item("cod_producto")) = tblStockMaterial.Rows(i).Item("material") Then
        '                Resultados.Rows(i).Item("val_stock_actual") = tblStockMaterial.Rows(i).Item("stock")
        '                Resultados.Rows(i).Item("val_cant_pend") = tblStockMaterial.Rows(i).Item("pendiente")                        ' Resultados.Rows(i).Item("ped_pend") = tblStockMaterial.Rows(i).Item("pendiente")
        '            End If
        '        Next
        '        msgConsultaSap = ":: Fuente stock / pendientes: <img align=""absmiddle"" src=""/images/sap.jpg"" border=""0"" alt=""Datos obtenidos en línea desde SAP"">"
        '    Catch ex As Exception
        '        msgConsultaSap = ":: Fuente stock / pendientes: <b>Registro ANDES</b>."
        '    End Try
        'Else
        '    msgConsultaSap = ":: Fuente stock / pendientes: <b>Registro ANDES</b>."
        'End If
        '------------------------------------------------------------------------------------------------------

        msgConsultaSap = ":: Fuente stock / pendientes: <b>Registro ANDES</b>."

        Crea_tabla(Resultados)

        ibExportar2.Visible = True
    End Sub

    Private Function Crea_tabla(ByVal dtResult As DataTable)
        Dim myTableCell As TableCell
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim myTableRow As TableRow
        Dim i As Integer

        i = 0
        myTableRow = New TableRow
        myTableCell = New TableCell
        myTableCell.Text = "LISTA DE PRECIOS"
        myTableCell.HorizontalAlign = HorizontalAlign.Center
        myTableCell.ColumnSpan = 8
        myTableCell.CssClass = "tbl-DataGridHeader"
        myTableRow.Cells.Add(myTableCell)
        mitabla.Rows.Add(myTableRow)

        myTableRow = New TableRow
        myTableCell = New TableCell

        '-----------------------------------------------------------------------------------------------------
        'Agregamos cabecera para datos de consulta de datos.
        Dim msgFechaConsulta As String = ":: Consulta: " + Format(Now, "dddd dd/MMM/yyyy HH:mm:ss")
        Dim fechaActualizacion As String
        Try
            fechaActualizacion = ":: Datos actualizados al: " & Format(obtieneFechaActualizacion(ano_periodo, mes_periodo, Session("ID_SUCURSAL"), "PRODUCTOS"), "dddd dd/MMM/yyyy HH:mm:ss")
        Catch ex As Exception
            fechaActualizacion = ":: Datos actualizados al: (no determinado))"
        End Try
        '-----------------------------------------------------------------------------------------------------

        myTableCell.Text = msgFechaConsulta + "<br>" + _
                                fechaActualizacion + "<br><br>" _
                                + msgConsultaSap
        myTableCell.HorizontalAlign = HorizontalAlign.Left
        myTableCell.ColumnSpan = 8
        myTableCell.CssClass = "info-informes"
        myTableRow.Cells.Add(myTableCell)
        mitabla.Rows.Add(myTableRow)

        myTableRow = New TableRow

        myTableCell = New TableCell
        myTableCell.Text = "FAMILIA"
        myTableCell.HorizontalAlign = HorizontalAlign.Center
        myTableCell.RowSpan = 2
        myTableCell.CssClass = "tbl-DataGridHeader"
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        myTableCell.Text = "SUBFAM"
        myTableCell.HorizontalAlign = HorizontalAlign.Center
        myTableCell.RowSpan = 2
        myTableCell.CssClass = "tbl-DataGridHeader"
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        myTableCell.Text = "CODIGO"
        myTableCell.HorizontalAlign = HorizontalAlign.Center
        myTableCell.RowSpan = 2
        myTableCell.CssClass = "tbl-DataGridHeader"
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        myTableCell.Text = "DESCRIPCION MATERIAL"
        myTableCell.HorizontalAlign = HorizontalAlign.Left
        myTableCell.RowSpan = 2
        myTableCell.CssClass = "tbl-DataGridHeader"
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        myTableCell.Text = "UMB"
        myTableCell.HorizontalAlign = HorizontalAlign.Center
        myTableCell.RowSpan = 2
        myTableCell.CssClass = "tbl-DataGridHeader"
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        myTableCell.Text = "PRECIO LISTA USD"
        myTableCell.HorizontalAlign = HorizontalAlign.Right
        myTableCell.RowSpan = 2
        myTableCell.CssClass = "tbl-DataGridHeader"
        myTableRow.Cells.Add(myTableCell)

        If tUserInfo.codigoFilial <> "CHI" Then
            myTableCell = New TableCell
            myTableCell.Text = "PRECIO LISTA MON SOC"
            myTableCell.HorizontalAlign = HorizontalAlign.Right
            myTableCell.RowSpan = 2
            myTableCell.CssClass = "tbl-DataGridHeader"
            myTableRow.Cells.Add(myTableCell)
        End If

        myTableCell = New TableCell
        myTableCell.Text = "STOCK"
        myTableCell.HorizontalAlign = HorizontalAlign.Center
        myTableCell.RowSpan = 1
        myTableCell.ColumnSpan = 2
        myTableCell.CssClass = "tbl-DataGridHeader"
        myTableRow.Cells.Add(myTableCell)

        mitabla.Rows.Add(myTableRow)

        myTableRow = New TableRow

        myTableCell = New TableCell
        myTableCell.Text = "Actual"
        myTableCell.HorizontalAlign = HorizontalAlign.Right
        myTableCell.CssClass = "tbl-DataGridHeader"
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        myTableCell.Text = "Pendiente"
        myTableCell.HorizontalAlign = HorizontalAlign.Right
        myTableCell.CssClass = "tbl-DataGridHeader"
        myTableRow.Cells.Add(myTableCell)

        mitabla.Rows.Add(myTableRow)

        While i <= dtResult.Rows.Count - 1
            myTableRow = New TableRow

            myTableRow.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If (i Mod 2 = 0) Then
                myTableRow.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF';")
                myTableRow.CssClass = "tbl-DataGridItemAlternating"
            Else
                myTableRow.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue';")
                myTableRow.CssClass = "tbl-DataGridItem"
            End If

            myTableCell = New TableCell
            myTableCell.Text = Trim(dtResult.Rows(i).Item("cod_familia"))
            myTableCell.HorizontalAlign = HorizontalAlign.Center
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            myTableCell.Text = Trim(dtResult.Rows(i).Item("cod_subfamilia"))
            myTableCell.HorizontalAlign = HorizontalAlign.Center
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            myTableCell.Text = Trim(dtResult.Rows(i).Item("cod_producto"))
            myTableCell.HorizontalAlign = HorizontalAlign.Center
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            myTableCell.Text = Trim(dtResult.Rows(i).Item("des_producto"))
            myTableCell.HorizontalAlign = HorizontalAlign.Left
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            myTableCell.Text = dtResult.Rows(i).Item("cod_umb")
            myTableCell.HorizontalAlign = HorizontalAlign.Center
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            myTableCell.Text = Format(dtResult.Rows(i).Item("val_precio_lista"), "#,##0.00")
            myTableCell.HorizontalAlign = HorizontalAlign.Right
            myTableRow.Cells.Add(myTableCell)

            If tUserInfo.codigoFilial <> "CHI" Then
                myTableCell = New TableCell
                myTableCell.Text = Format(dtResult.Rows(i).Item("val_precio_lista_msoc"), "#,##0.00")
                myTableCell.HorizontalAlign = HorizontalAlign.Right
                myTableRow.Cells.Add(myTableCell)
            End If

            myTableCell = New TableCell
            myTableCell.Text = Format(dtResult.Rows(i).Item("val_stock_actual"), "#,##0")
            myTableCell.HorizontalAlign = HorizontalAlign.Right
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            myTableCell.Text = Format(dtResult.Rows(i).Item("val_cant_pend"), "#,##0")
            myTableCell.HorizontalAlign = HorizontalAlign.Right
            myTableRow.Cells.Add(myTableCell)

            i += 1
            mitabla.Rows.Add(myTableRow)
        End While

        Session("myTable") = mitabla
    End Function

    Private Sub ibExportar2_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar2.Click
        Dim sTableHeader As String = "Listado de Precios  -  Período " & ddlMeses.SelectedItem.Text & "/" & ddlanos.SelectedItem.Text

        ' Nueva instancia del Informe
        Dim xlsResultado As Table = Session("myTable")

        ' Agregar encabezado del informe
        Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

        Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

        ' Configuracion de impresion
        Exportar.PageScale = 80
        Exportar.PageLayout = "Landscape"

        ' Encabezado y Pie de Pagina
        Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;Listado de Precios - " & ddlMeses.SelectedItem.Text & "/" & ddlanos.SelectedItem.Text)
        Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

        ' Exportar
        Exportar.TableToExcel(xlsResultado)
        Exportar.SaveToClient(Response)
    End Sub
End Class
