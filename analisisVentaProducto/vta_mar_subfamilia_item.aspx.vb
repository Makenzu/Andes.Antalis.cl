Public Class vta_mar_subfamilia_item
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents plDatos As System.Web.UI.WebControls.Panel
    Protected WithEvents plParams As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents rbSubfam As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txCodProd As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbCodProd As System.Web.UI.WebControls.RadioButton

    Dim tUserInfo As usuario.t_Usuario
    Dim totales(9) As Integer


    Protected WithEvents lbData As System.Web.UI.WebControls.Label
    Protected WithEvents lbSearch As System.Web.UI.WebControls.Label
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents txSubfam As System.Web.UI.WebControls.TextBox
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label

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

        Page.Server.ScriptTimeout = 90

        If IsNothing(Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO)) = True Then
            Response.Redirect("logout.aspx")
            Response.End()
        Else
            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        End If


        Dim ano_periodo As Int16
        Dim mes_periodo As Int16

        If Not Page.IsPostBack Then

            'Llenar DorpDownList con Fechas
            ddlAno.Items.Add(Year(Date.Now))
            ddlAno.Items.Add(Year(Date.Now.AddYears(-1)))

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

            ano_periodo = Year(Date.Now)
            mes_periodo = Month(Date.Now)

        Else
            mes_periodo = CInt(ddlMes.SelectedItem.Value)
            ano_periodo = CInt(ddlAno.SelectedItem.Value)

            Dim dtMarVta As New DataTable
            Dim cod_filial As String = tUserInfo.codigoFilial
            Dim cod_sucursal As String = tUserInfo.codigoSucursal

            plDatos.Visible = True
            lbErrors.Text = ""
            lbNota.Text = "- SE EXCLUYEN VENTAS INTEREMPRESAS<br>- VALORES INDICAN UNIDADES DE VENTA"

            If rbCodProd.Checked = True Then

                ' BUSCAR POR PRODUCTO
                Try

                    Dim cod_producto = txCodProd.Text.Trim

                    ' CLEAR DATAGRID
                    dgResultado.DataSource = Nothing
                    dgResultado.DataBind()

                    ' LOAD DATAGRID
                    dtMarVta = ventas.mar_vta_subfamilia_item(cod_producto, "", mes_periodo, ano_periodo, cod_filial, cod_sucursal)
                    dgResultado.DataSource = dtMarVta
                    dgResultado.DataBind()

                    lbSearch.Text = "Producto "
                    lbData.Text = Utiles.get_producto_name(cod_producto)

                Catch ex As Exception
                    ' SHOW ERROR
                    lbErrors.Text = "ERROR: " & Err.Description
                    lbErrors.Visible = True
                    Err.Clear()
                    ' HIDE DATA
                    plDatos.Visible = False
                Finally
                    dtMarVta.Dispose()
                End Try

            ElseIf rbSubfam.Checked = True Then
                ' BUSCAR POR SUBFAMILIA
                Try

                    Dim cod_subfamilia = txSubfam.Text.Trim

                    ' CLEAR DATAGRID
                    dgResultado.DataSource = Nothing
                    dgResultado.DataBind()
                    ' LOAD DATAGRID
                    dtMarVta = ventas.mar_vta_subfamilia_item("", cod_subfamilia, mes_periodo, ano_periodo, cod_filial, cod_sucursal)
                    dgResultado.DataSource = dtMarVta
                    dgResultado.DataBind()

                    lbSearch.Text = "Subfamilia "
                    lbData.Text = Utiles.get_subfamilia_name(cod_subfamilia)

                Catch ex As Exception
                    ' SHOW ERROR
                    lbErrors.Text = "ERROR: " & Err.Description
                    lbErrors.Visible = True
                    Err.Clear()
                    ' HIDE DATA
                    plDatos.Visible = False
                Finally
                    dtMarVta.Dispose()
                End Try

            End If


        End If

        rbCodProd.Attributes.Add("onClick", "javascript:document.forms[0].txSubfam.value='';")
        rbSubfam.Attributes.Add("onClick", "javascript:document.forms[0].txCodProd.value='';")

        lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo


    End Sub

    Private Sub dgResultado_SortCommand(ByVal source As Object, _
ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) _
Handles dgResultado.SortCommand


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

            dgResultado.DataSource = dv
            dgResultado.DataBind()

        Catch ex As Exception
            lbErrors.Text = "ERRORES EN PAGINA: " & ex.Message
            Err.Clear()
        End Try


    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, _
ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound

        Dim i As Int16


        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then
            Dim imgUp1 As New System.Web.UI.WebControls.Image
            imgUp1.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp2 As New System.Web.UI.WebControls.Image
            imgUp2.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp3 As New System.Web.UI.WebControls.Image
            imgUp3.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp4 As New System.Web.UI.WebControls.Image
            imgUp4.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp5 As New System.Web.UI.WebControls.Image
            imgUp5.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp6 As New System.Web.UI.WebControls.Image
            imgUp6.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp7 As New System.Web.UI.WebControls.Image
            imgUp7.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp8 As New System.Web.UI.WebControls.Image
            imgUp8.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp9 As New System.Web.UI.WebControls.Image
            imgUp9.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp10 As New System.Web.UI.WebControls.Image
            imgUp10.ImageUrl = "images/sort_2arrows.gif"
            e.Item.Cells(0).Controls.Add(imgUp1)
            e.Item.Cells(1).Controls.Add(imgUp2)
            e.Item.Cells(2).Controls.Add(imgUp3)
            e.Item.Cells(3).Controls.Add(imgUp4)
            e.Item.Cells(4).Controls.Add(imgUp5)
            e.Item.Cells(5).Controls.Add(imgUp6)
            e.Item.Cells(6).Controls.Add(imgUp7)
            e.Item.Cells(7).Controls.Add(imgUp8)
            e.Item.Cells(8).Controls.Add(imgUp9)
            e.Item.Cells(9).Controls.Add(imgUp10)

            'RESET TOTALES
            For i = 0 To totales.Length - 1
                totales(i) = 0
            Next

        End If


        ' DG  ITEM CODE
        Dim cl As New System.Globalization.CultureInfo("es-CL")

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

            ' CALCULAR TOTALES
            For i = 2 To 9
                totales(i) += e.Item.Cells(i).Text
            Next

            If e.Item.Cells(2).Text().Trim() <> "" And e.Item.Cells(2).Text() <> "&nbsp;" Then
                e.Item.Cells(2).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(2).Text()))
            End If

            If e.Item.Cells(3).Text().Trim() <> "" And e.Item.Cells(3).Text() <> "&nbsp;" Then
                e.Item.Cells(3).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(3).Text()))
            End If
            If e.Item.Cells(4).Text().Trim() <> "" And e.Item.Cells(4).Text() <> "&nbsp;" Then
                e.Item.Cells(4).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(4).Text()))
            End If
            If e.Item.Cells(5).Text().Trim() <> "" And e.Item.Cells(5).Text() <> "&nbsp;" Then
                e.Item.Cells(5).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(5).Text()))
            End If
            If e.Item.Cells(6).Text().Trim() <> "" And e.Item.Cells(6).Text() <> "&nbsp;" Then
                e.Item.Cells(6).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(6).Text()))
            End If
            If e.Item.Cells(7).Text().Trim() <> "" And e.Item.Cells(7).Text() <> "&nbsp;" Then
                e.Item.Cells(7).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(7).Text()))
            End If
            If e.Item.Cells(8).Text().Trim() <> "" And e.Item.Cells(8).Text() <> "&nbsp;" Then
                e.Item.Cells(8).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(8).Text()))
            End If
            If e.Item.Cells(9).Text().Trim() <> "" And e.Item.Cells(9).Text() <> "&nbsp;" Then
                e.Item.Cells(9).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(9).Text()))
            End If
            If e.Item.Cells(10).Text().Trim() <> "" And e.Item.Cells(10).Text() <> "&nbsp;" Then
                e.Item.Cells(10).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(10).Text()))
            End If
        End If

        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(0).Text = "Totales : "

            ' MOSTRAR TOTALES
            For i = 2 To 9
                e.Item.Cells(i).Text = String.Format(cl, "{0:N0}", totales(i))
            Next
        End If

    End Sub

    'The Page-level properties that write to ViewState
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


End Class
