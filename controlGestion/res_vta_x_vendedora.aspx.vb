Public Class res_vta_x_vendedora
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents txFecIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txFecTer As System.Web.UI.WebControls.TextBox
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton
    Protected WithEvents plParams As System.Web.UI.WebControls.Panel
    Protected WithEvents rfvFecTer As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvFecIni As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents cvFechas As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage

    Dim tUserInfo As usuario.t_Usuario

    Dim totales(6) As Double
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton

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

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)


        lbErrors.Text = ""
        btSend.Attributes.Add("onClick", "javascript:noDblClick(document.all.disbtn,this);")

        Dim cl As New System.Globalization.CultureInfo("en-US")

        If txFecIni.Text = "" Then
            txFecIni.Text = Date.Now.AddDays(-1).ToString("dd / MM / yyyy", cl)
        End If

        If txFecTer.Text = "" Then
            txFecTer.Text = Date.Now.ToString("dd / MM / yyyy", cl)
        End If

        Dim tipo As String = "VTA"

        If Page.IsPostBack Then

            Dim cod_cliente As String
            Dim cod_doc As String
            Dim fec_ini, fec_ter As Date
            Dim dtResult As New DataTable


            If Date.Parse(txFecIni.Text) > Date.Parse(txFecTer.Text) Then
                cvFechas.Visible = True
            Else

                cvFechas.Visible = False
                Try

                    fec_ini = txFecIni.Text
                    fec_ter = txFecTer.Text

                    lbFecha.Text = " del " & fec_ini & " al " & fec_ter

                    dgResultado.DataSource = Nothing
                    dgResultado.DataBind()

                    dtResult = ventas.res_vta_x_interlocutor(tUserInfo.codigoSucursal, tUserInfo.codigoFilial, fec_ini, fec_ter, tipo)
                    dgResultado.DataSource = dtResult
                    dgResultado.DataBind()

                    'If dtResult.Rows.Count > 0 Then
                    '    lbNota.Text = "Ultima Actualización: <b>" & dtResult.Rows(1).Item("ULT_ACTUALIZACION") & "</b>"
                    '    lbNota.Visible = True
                    'Else
                    '    lbNota.Visible = False
                    'End If

                    ibExportar.Visible = True
                Catch ex As Exception
                    lbErrors.Text = "ERROR: " & Err.Description
                    lbErrors.Visible = True
                    Err.Clear()
                    ' Throw ex
                Finally
                    dtResult.Dispose()

                End Try

            End If

        End If

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
            lbErrors.Text = "ERROR: " & ex.Message
            Err.Clear()
        End Try

    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, _
ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound


        Dim i As Int16

        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then

            Dim images(7) As System.Web.UI.WebControls.Image

            For i = 1 To 7
                images(i) = New System.Web.UI.WebControls.Image
                images(i).ImageUrl = "/images/sort_2arrows.gif"
                e.Item.Cells(i).Controls.Add(images(i))
                e.Item.Cells(i).Attributes.Add("title", "Ordenar")
            Next

            For i = 1 To 6
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
            e.Item.Attributes.Add("title", e.Item.Cells(1).Text().Trim)

            For i = 1 To 6
                totales(i) += e.Item.Cells(i + 1).Text()
                e.Item.Cells(i + 1).Text() = String.Format(cl, "{0:N0}", e.Item.Cells(i + 1).Text())
            Next



        End If

        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(0).Text = "Totales : "
            e.Item.Cells(0).Attributes.Add("align", "left")

            For i = 1 To 6
                e.Item.Cells(2).Attributes.Add("align", "right")
                e.Item.Cells(i + 1).Text() = String.Format(cl, "{0:N0}", totales(i))
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

    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click
        If dgResultado.Items.Count > 0 Then

            ' Nueva instancia del Informe
            Dim xlsResultado As Table = CType(dgResultado.Controls(0), System.Web.UI.WebControls.Table)

            Dim sTableHeader As String
            sTableHeader = "Fecha: " & lbFecha.Text.Trim

            ' Agregar encabezado del informe
            Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

            Dim Exportar As New Exportador.Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

            ' Configuracion de impresion
            Exportar.PageScale = 85
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DResumen Ventas por Vendedora - " & lbFecha.Text.Trim)
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Exportar.TableToExcel(xlsResultado)
            Exportar.SaveToClient(Response)

        End If
    End Sub
End Class
