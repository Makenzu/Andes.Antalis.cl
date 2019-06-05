Imports Exportador

Public Class vta_x_item_cliente
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid

    ' Para calculo de totales
    Dim totales(13) As Double
    Dim mes_periodo As Int16
    Dim ano_periodo As Int32

    Protected WithEvents lbCodProd As System.Web.UI.WebControls.Label
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents txCodProd As System.Web.UI.WebControls.TextBox
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label

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

        lbErrors.Text = ""

        Dim tUserInfo As t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim cod_filial As String = tUserInfo.codigoFilial
        Dim cod_sucursal As String = tUserInfo.codigoSucursal

        If Not Page.IsPostBack Then

            cargaAnos()
            cargaMeses()

            ano_periodo = Year(Date.Now)
            mes_periodo = Month(Date.Now)

        Else
            mes_periodo = ddlMes.SelectedItem.Value
            ano_periodo = ddlAno.SelectedItem.Value

            Try

                Dim cod_Producto As String = Trim(Request("cp"))

                If cod_Producto = "" Then
                    cod_Producto = txCodProd.Text.Trim
                End If

                If cod_Producto = "" Then
                    Err.Description = "Faltan parametros para poder ejecutar la consulta."
                    Err.Raise(vbObjectError + 512 + 10, "vta_x_subfamilia_item", Err.Description)
                End If

                inicializaTotales()
                generaTitulosDataGrid()

                dgResultado.DataSource = ventas.vta_x_item_cliente(cod_Producto, mes_periodo, ano_periodo, cod_filial, cod_sucursal)
                dgResultado.DataBind()

                lbCodProd.Text = Utiles.get_producto_name(cod_Producto) & "  (" & cod_Producto & ")"

                'plDatos.Visible = True
                ibExportar.Visible = True

            Catch ex As Exception
                lbErrors.Text = Err.Description
                Err.Clear()
                ' Throw ex
            Finally


            End Try

        End If

        lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo


    End Sub

    ' no se ocupa pero crea un bound column en runtime
    Private Function CreateBoundColumn(ByVal c As DataColumn)
        Dim column As New BoundColumn
        column.DataField = c.ColumnName
        column.HeaderText = c.ColumnName.Replace("_", " ")
        'column.DataFormatString = setFormating(c)
        Return column

    End Function

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, _
ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound


        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim j As Integer

        If e.Item.ItemType = ListItemType.Header Then

        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If

            ' *** CODIGO PARA HIGHLIGHT  -END- ******

            Dim i As Int16
            For i = 0 To 12
                totales(i) += e.Item.Cells(i + 2).Text
            Next


            ' e.Item.Cells(1).Attributes.Add("title", "Ver otros items comprados por este Cliente.")


        End If


        If e.Item.ItemType = ListItemType.Footer Then

            e.Item.Cells(0).Text = "Totales : "
            Dim i As Int16
            For i = 0 To 12
                e.Item.Cells(i + 2).Text = String.Format(cl, "{0:N0}", totales(i))
                e.Item.Cells(i + 2).Wrap = False
            Next

        End If

    End Sub

    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click

        If dgResultado.Items.Count > 0 Then

            ' Nueva instancia del Informe
            Dim xlsResultado As Table = CType(dgResultado.Controls(0), System.Web.UI.WebControls.Table)

            Dim sTableHeader As String
            sTableHeader = "Producto: " & Me.lbCodProd.Text.Trim

            ' Agregar encabezado del informe
            Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

            Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

            ' Configuracion de impresion
            Exportar.PageScale = 85
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DVentas por Item - Cliente - " & lbFecha.Text.Trim)
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Exportar.TableToExcel(xlsResultado)
            Exportar.SaveToClient(Response)

        End If

    End Sub

    Private Sub generaTitulosDataGrid()
        ' ENCABEZADO DE UTIMOS 12 MESES
        Dim i, j As Integer

        Dim dateToShow As Date = New Date(ano_periodo, mes_periodo, 1)

        j = 2
        For i = 0 To 12
            dgResultado.Columns(j).HeaderText = Format(dateToShow.AddMonths(-i), ("MMM/yy"))
            j += 1
        Next
    End Sub

    Private Sub inicializaTotales()
        Dim i As Integer
        For i = 0 To 12
            totales(i) = 0
        Next
    End Sub

    Private Property SortExpression() As String
        Get
            Dim o As Object = viewstate("SortExpression")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return o.ToString
            End If
        End Get
        Set(ByVal Value As String)
            viewstate("SortExpression") = Value
        End Set
    End Property

    Private Property SortAscending() As Boolean
        Get
            Dim o As Object = viewstate("SortAscending")
            If o Is Nothing Then
                Return True
            Else
                Return Convert.ToBoolean(o)
            End If
        End Get
        Set(ByVal Value As Boolean)
            viewstate("SortAscending") = Value
        End Set
    End Property

    Private Sub dgResultado_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgResultado.SortCommand
        Try
            Dim ColumnToSort As String
            Dim SortExprs() As String

            SortExprs = Split(e.SortExpression, " ")
            ColumnToSort = SortExprs(0)

            If e.SortExpression.ToLower = Me.SortExpression.ToLower Then
                ' SortAscending = Not SortAscending
                Me.SortExpression = ColumnToSort & " ASC"
            Else
                'SortAscending = True
                Me.SortExpression = ColumnToSort & " DESC"
            End If

            Dim dv As DataView = New DataView(dgResultado.DataSource)
            dv.Sort = Me.SortExpression
            inicializaTotales()
            dgResultado.DataSource = dv
            dgResultado.DataBind()

        Catch ex As Exception
            lbErrors.Text = ex.Message
            Err.Clear()
        End Try
    End Sub

    Private Sub cargaAnos()
        For i As Integer = Year(Date.Now) To (Year(Date.Now) - 5) Step -1
            ddlAno.Items.Add(i)
        Next
        'For i As Integer = 2005 To Now.Year
        '    ddlAno.Items.Add(i)
        'Next
    End Sub

    Private Sub cargaMeses()
        Dim i As Integer
        Dim x As Integer = 12
        For i = 0 To 11

            Dim newListItem As New ListItem
            If (Month(Date.Now) - i) >= 1 Then
                newListItem.Text = MonthName(Month(Date.Now) - i)
                newListItem.Value = Month(Date.Now) - i
            Else
                newListItem.Text = MonthName(Month(Date.Now) + x)
                newListItem.Value = Month(Date.Now) + x
            End If
            ddlMes.Items.Add(newListItem)

            If Month(Date.Now) - i = Month(Date.Now) Then ddlMes.Items(i).Selected = True
            x -= 1
        Next
    End Sub
End Class
