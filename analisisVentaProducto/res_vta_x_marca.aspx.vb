Imports Exportador

Public Class res_vta_x_marca
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton
    Protected WithEvents plParams As System.Web.UI.WebControls.Panel
    Protected WithEvents rfvFecIni As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents dsfsd As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents vFechas As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents tbxFecIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbxFecTer As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlmarca As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim tUserInfo As usuario.t_Usuario
    Dim totales(5) As Integer


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If


        Dim cl As New System.Globalization.CultureInfo("en-US")

        If IsNothing(Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO)) = True Then
            Response.Redirect("logout.aspx")
            Response.End()
        Else
            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        End If


        If Not Page.IsPostBack Then
            despliegaMarcas()

            If tbxFecIni.Text = "" Then
                tbxFecIni.Text = Format((New Date(Now.Year, Now.Month, 1)), "dd/MM/yyyy")

            End If

            If tbxFecTer.Text = "" Then
                tbxFecTer.Text = Format(Date.Now, "dd/MM/yyyy")
            End If

            If Date.Parse(tbxFecIni.Text) > Date.Parse(tbxFecTer.Text) Then
                vFechas.Visible = True
            Else
                vFechas.Visible = False
            End If

            lbErrors.Text = ""

        End If

    End Sub

    Private Sub despliegaMarcas()

        Dim tUserInfo As usuario.t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        With ddlmarca
            .DataSource = Utiles.ObtieneMarcas(Date.Now.Year, Date.Now.Month, tUserInfo.codigoFilial)
            ddlmarca.DataTextField = "cod_marca"
            ddlmarca.DataValueField = "des_marca"
            .DataBind()
        End With

        ddlmarca.Items.Add(New ListItem("- Todas -", "T"))
        ddlmarca.SelectedValue = "T"

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

    Private Sub btSend_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSend.Click

        Dim fec_ini, fec_ter As Date
        Dim dtResult As New DataTable
        ibExportar.Visible = True

        Try

            fec_ini = tbxFecIni.Text
            fec_ter = tbxFecTer.Text
            Dim marca As String
            marca = Request("ddlmarca")


            lbFecha.Text = " del " & fec_ini & " al " & fec_ter

            dgResultado.DataSource = Nothing
            dgResultado.DataBind()

            dtResult = ventas.res_vta_x_marca(tUserInfo.codigoFilial, tUserInfo.codigoSucursal, fec_ini, fec_ter, marca.Trim)
            dgResultado.DataSource = dtResult
            Session("dgResultado") = dtResult
            dgResultado.DataBind()


        Catch ex As Exception
            lbErrors.Text = "ERROR: " & Err.Description
            lbErrors.Visible = True
            Err.Clear()
        Finally
            dtResult.Dispose()

        End Try

    End Sub

    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click

        If dgResultado.Items.Count > 0 Then

            ' Nueva instancia del Informe
            Dim xlsResultado As Table = CType(dgResultado.Controls(0), System.Web.UI.WebControls.Table)

            Dim sTableHeader As String
            'sTableHeader = "Cliente: " & txCliente.Text & " - " & Me.lbNomCli.Text.Trim

            ' Agregar encabezado del informe
            Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

            Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

            ' Configuracion de impresion
            Exportar.PageScale = 85
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DDetalle Control Facturación - " & lbFecha.Text.Trim)
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Exportar.TableToExcel(xlsResultado)
            Exportar.SaveToClient(Response)

        End If

    End Sub


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
            Dim datos As DataTable = Session("dgResultado")
            Dim dv As DataView = New DataView(datos)
            dv.Sort = Me.SortExpression

            dgResultado.DataSource = dv
            dgResultado.DataBind()

        Catch ex As Exception
            lbErrors.Text = "ERROR: " & ex.Message
            Err.Clear()
        End Try
    End Sub

    Private Sub dgResultado_ItemDataBound1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound
        'Dim i As Int16

        ''  DG HEADER  CODE
        'If e.Item.ItemType = ListItemType.Header Then

        '    Dim images(10) As System.Web.UI.WebControls.Image

        '    For i = 0 To 10
        '        images(i) = New System.Web.UI.WebControls.Image
        '        images(i).ImageUrl = "/images/sort_2arrows.gif"
        '        e.Item.Cells(i).Controls.Add(images(i))
        '        e.Item.Cells(i).Attributes.Add("title", "Ordenar")
        '    Next

        '    e.Item.Cells(10).Text = "%Mg"

        '    For i = 0 To 2
        '        totales(i) = 0
        '    Next
        'End If



        '' DG  ITEM CODE
        'Dim cl As New System.Globalization.CultureInfo("es-CL")

        'If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

        '    ' *** CODIGO PARA HIGHLIGHT  -START- ******
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
        '    If e.Item.ItemType = ListItemType.AlternatingItem Then
        '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
        '    End If

        '    If e.Item.ItemType = ListItemType.Item Then
        '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
        '    End If

        '    ' *** CODIGO PARA HIGHLIGHT  -END- ******
        '    e.Item.Attributes.Add("title", e.Item.Cells(1).Text().Trim)
        '    For i = 5 To 9
        '        totales(i - 5) = totales(i - 5) + e.Item.Cells(i).Text()
        '    Next

        '    Dim precio, costo As Double

        '    precio = Double.Parse(e.Item.Cells(8).Text)
        '    costo = Double.Parse(e.Item.Cells(9).Text)

        '    e.Item.Cells(10).Text = Format((precio - costo) / precio * 100, "#,##0.00")

        'End If




        'If e.Item.ItemType = ListItemType.Footer Then


        '    e.Item.Cells(0).Text = "Totales : "
        '    e.Item.Cells(0).Attributes.Add("align", "left")

        '    For i = 5 To 9
        '        e.Item.Cells(i).Text() = Format(totales(i - 5), "#,##0.00")
        '    Next


        '    Dim precio, costo As Double

        '    precio = Double.Parse(e.Item.Cells(8).Text)
        '    costo = Double.Parse(e.Item.Cells(9).Text)

        '    e.Item.Cells(10).Text = Format((precio - costo) / precio * 100, "#,##0.00")


        'End If

    End Sub
End Class
