Public Class vta_x_cliente_item_val_detalle
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents lb_Fecha As System.Web.UI.WebControls.Label
    Protected WithEvents lbErrores As System.Web.UI.WebControls.Label
    Protected WithEvents dgdetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lb_producto As System.Web.UI.WebControls.Label
    Protected WithEvents lb_nom_cliente As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim cod_cliente As String
    Dim tUserInfo As usuario.t_Usuario
    Dim totales(7) As Double
    Dim totRows As Double
    Dim cod_producto As String
    Dim mes_periodo As String
    Dim ano_periodo As String
    Dim cod_filial As String
    Dim cod_sucursal As String
    Dim tipo_consulta As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)


        If Not Page.IsPostBack Then
            ibExportar.Visible = True

            cod_cliente = Request.QueryString("cc")
            cod_producto = Request.QueryString("cp")
            mes_periodo = Request.QueryString("mp")
            ano_periodo = Request.QueryString("ap")
            cod_filial = Request.QueryString("cf")
            cod_sucursal = Request.QueryString("cs")
            lb_producto.Text = Request.QueryString("dp")
            lb_nom_cliente.Text = Request.QueryString("nc")
            tipo_consulta = Request.QueryString("tc")

            Dim tabla As DataTable
            tabla = vta_x_cliente_item_valor_detalle(cod_cliente, _
                                                    cod_producto, _
                                                    mes_periodo, _
                                                    ano_periodo, _
                                                    cod_filial, _
                                                    cod_sucursal, _
                                                    tipo_consulta)
            dgdetalle.DataSource = tabla
            dgdetalle.DataBind()
        End If
    End Sub

    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click
        If dgdetalle.Items.Count > 0 Then

            ' Nueva instancia del Informe
            Dim xlsResultado As Table = CType(dgdetalle.Controls(0), System.Web.UI.WebControls.Table)

            Dim sTableHeader As String
            sTableHeader = "Cliente: " & lb_nom_cliente.Text & " - " & Me.lb_producto.Text.Trim

            ' Agregar encabezado del informe
            Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

            Dim Exportar As New Exportador.Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

            ' Configuracion de impresion
            Exportar.PageScale = 85
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DVentas por Cliente Item - ")
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Exportar.TableToExcel(xlsResultado)
            Exportar.SaveToClient(Response)
        End If
    End Sub

    Private Sub dgdetalle_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgdetalle.ItemDataBound
        Dim i As Int16

        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then
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

            If e.Item.Cells(5).Text() <> "" And e.Item.Cells(5).Text() <> "&nbsp;" Then
                totales(1) += CInt(e.Item.Cells(5).Text())
                e.Item.Cells(5).Text() = String.Format(cl, "{0:N0}", CDbl(e.Item.Cells(5).Text()))
            End If

            If e.Item.Cells(2).Text().Trim() <> "" And e.Item.Cells(2).Text() <> "&nbsp;" Then
                e.Item.Cells(2).Text() = "<a href=""/historico/vta_detalle_documento.aspx?ap=" & ano_periodo.ToString & "&mp=" & mes_periodo.ToString & "&cf=" & cod_filial & "&cs=" & e.Item.Cells(0).Text().Trim & "&nd=" & _
                                                       e.Item.Cells(2).Text().Trim & "&cd=" & e.Item.Cells(1).Text().Trim & """ title=""Ver Detalle"">" & e.Item.Cells(2).Text() & "</a>"
            End If

            totRows = totRows + 1
        End If

        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(4).Text() = "Total Items : "

            If i = 6 Then
                e.Item.Cells(5).Text() = String.Format(cl, "{0:N0}", CDbl(totales(1) / totRows))
            Else
                e.Item.Cells(5).Text() = String.Format(cl, "{0:N0}", totales(1))
            End If
        End If
    End Sub
End Class
