Imports Exportador

Public Class vta_detalle_control_factura
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents rfvPromo As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents ibtDesde As System.Web.UI.WebControls.ImageButton

    ' Para calculo de totales
    Dim totA As Integer = 0
    Dim totB As Integer = 0
    Dim totC As Integer = 0
    Dim totD As Integer = 0
    Dim totItems As Integer = 0
    Dim totProm As Double = 0
    Dim totLineas As Integer = 0
    Protected WithEvents plParams As System.Web.UI.WebControls.Panel
    Protected WithEvents txFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlMoneda As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents hdCodSucursal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdCodFilial As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlCentro As System.Web.UI.WebControls.DropDownList


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
    Dim anoPeriodo, mesPeriodo As Integer


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        Dim moneda As String
        Dim fecha As String = ""
        Dim codigoFilial, codigoSucursal As String
        Dim en As New System.Globalization.CultureInfo("en-US")


        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        lbErrors.Visible = False

        'Llenamos las sucursales
        With ddlCentro
            .DataSource = obtieneSucursales(tUserInfo.codigoFilial)
            .DataTextField = "des_sucursal"
            .DataValueField = "cod_sucursal"
            .DataBind()

            If (.Items.Count > 1) Then
                .Items.Add(New ListItem("TODOS LOS CENTROS PARA " & tUserInfo.codigoFilial, "*"))
            End If
        End With



        If Page.IsPostBack Then
            moneda = ddlMoneda.SelectedValue
            fecha = txFecha.Text.Trim
            anoPeriodo = Year(Date.Parse(fecha))
            mesPeriodo = Month(Date.Parse(fecha))
            codigoFilial = hdCodFilial.Value
            If codigoFilial.Length = 0 Then
                codigoFilial = tUserInfo.codigoFilial
            End If
            codigoSucursal = hdCodSucursal.Value
            If codigoSucursal.Length = 0 Then
                codigoSucursal = Request("ddlCentro")

                ddlCentro.Items.FindByValue(codigoSucursal).Selected = True

            End If


        Else



            If Request("mo") <> "" Then
                moneda = Request("mo").Trim
            End If

            If moneda = "DOL" Then
                ddlMoneda.SelectedIndex = 0
            ElseIf moneda = "PES" Then
                ddlMoneda.SelectedIndex = 1
            End If

            If Request("fec") <> "" Then
                fecha = Date.Parse(Request("fec").Trim).ToString("dd / MM / yyyy", en)
                anoPeriodo = Year(fecha)
                mesPeriodo = Month(fecha)
            End If

            codigoFilial = Request.QueryString("cf")
            codigoSucursal = Request.QueryString("cs")
            hdCodFilial.Value = codigoFilial
            hdCodSucursal.Value = codigoSucursal

            If IsNothing(codigoSucursal) Then
                codigoSucursal = tUserInfo.codigoSucursal
            End If

            If codigoSucursal.Length > 0 Then
                ddlCentro.Items.FindByValue(codigoSucursal).Selected = True
            End If

        End If

        If moneda <> "" And fecha <> "" Then

            Dim dtResult As New DataTable
            Try
                dgResultado.DataSource = Nothing
                dgResultado.DataBind()

                totA = 0
                totB = 0
                totC = 0
                totD = 0
                totItems = 0
                totProm = 0
                totLineas = 0

                dtResult = ventas.vta_det_control_fac(codigoFilial, codigoSucursal, moneda, fecha)
                dgResultado.DataSource = dtResult
                dgResultado.DataBind()

                lbNota.Text = "<b>Fecha: " & fecha & "</b><br>- Incluye ventas intercompañía."
                lbNota.Text &= "<br>- Valores expresados en " & moneda
                lbNota.Visible = True
                ibExportar.Visible = True

            Catch ex As Exception
                lbErrors.Text = Err.Description
                lbErrors.Visible = True
                Err.Clear()
            Finally
                dtResult.Dispose()
            End Try

        End If


        If fecha = "" Then
            txFecha.Text = Date.Now.ToString("dd / MM / yyyy", en)
        Else
            txFecha.Text = Date.Parse(fecha).ToString("dd / MM / yyyy", en)
        End If

    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, _
ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound


        Dim cl As New System.Globalization.CultureInfo("es-CL")

        If e.Item.ItemType = ListItemType.Header Then

            Dim imgUp1 As New System.Web.UI.WebControls.Image
            imgUp1.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp2 As New System.Web.UI.WebControls.Image
            imgUp2.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp3 As New System.Web.UI.WebControls.Image
            imgUp3.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp4 As New System.Web.UI.WebControls.Image
            imgUp4.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp5 As New System.Web.UI.WebControls.Image
            imgUp5.ImageUrl = "/images/sort_2arrows.gif"


            e.Item.Cells(4).Controls.Add(imgUp1)
            e.Item.Cells(5).Controls.Add(imgUp2)
            e.Item.Cells(6).Controls.Add(imgUp3)
            e.Item.Cells(7).Controls.Add(imgUp4)
            e.Item.Cells(10).Controls.Add(imgUp5)

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


            Dim calc As Double
            If CDbl(e.Item.Cells(5).Text) > 0 Then
                calc = (e.Item.Cells(7).Text / e.Item.Cells(5).Text) * 100
            Else
                calc = 0
            End If

            totA += e.Item.Cells(4).Text
            totB += e.Item.Cells(5).Text
            totC += e.Item.Cells(6).Text
            totD += e.Item.Cells(7).Text
            totProm += calc
            totItems += e.Item.Cells(9).Text
            totLineas += 1

            Dim codigoFilial As String = tUserInfo.codigoFilial
            Dim codigoSucursal As String = e.Item.Cells(1).Text

            If e.Item.Cells(3).Text().Trim() <> "" And e.Item.Cells(3).Text() <> "&nbsp;" Then
                e.Item.Cells(3).Text() = "<a href=""/historico/vta_detalle_documento.aspx?ap=" & anoPeriodo.ToString & "&mp=" & mesPeriodo.ToString & "&cf=" & codigoFilial & "&cs=" & codigoSucursal & "&nd=" & _
                                                        e.Item.Cells(3).Text().Trim & "&cd=" & e.Item.Cells(2).Text().Trim & "&mo=" & ddlMoneda.SelectedValue & """ title=""Ver Detalle"">" & e.Item.Cells(3).Text() & "</a>"
            End If

            Dim i As Int16

            For i = 4 To 7
                If ddlMoneda.SelectedValue = "DOL" Then
                    e.Item.Cells(i).Text = String.Format(cl, "{0:N2}", CDbl(e.Item.Cells(i).Text)) ' A$, B$, C$, D$
                Else
                    e.Item.Cells(i).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(i).Text)) ' A$, B$, C$, D$
                End If
            Next

            e.Item.Cells(8).Text = String.Format(cl, "{0:N2}", CDbl(calc)) ' D$/B$*100
            e.Item.Cells(9).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(9).Text)) ' Items

        End If

        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(2).Text = "Totales : "
            e.Item.Cells(4).Text = String.Format(cl, "{0:N0}", CInt(totA))
            e.Item.Cells(5).Text = String.Format(cl, "{0:N0}", CInt(totB))
            e.Item.Cells(6).Text = String.Format(cl, "{0:N0}", CInt(totC))
            e.Item.Cells(7).Text = String.Format(cl, "{0:N0}", CInt(totD))
            e.Item.Cells(8).Text = String.Format(cl, "{0:N2}", CDbl((totD / totB) * 100))
            e.Item.Cells(9).Text = String.Format(cl, "{0:N0}", CInt(totItems))
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

            totA = 0
            totB = 0
            totC = 0
            totD = 0
            totItems = 0
            totProm = 0
            totLineas = 0

            dgResultado.DataSource = dv
            dgResultado.DataBind()

        Catch ex As Exception
            lbErrors.Text = "ERROR: " & ex.Message
            Err.Clear()
        End Try


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
            sTableHeader = "Detalle control Facturación ABCD (" & txFecha.Text & ")"

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


    Private Sub ddlMoneda_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMoneda.SelectedIndexChanged

    End Sub
End Class
