Imports Exportador

Public Class res_sinvta_cliente_promo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents lbNomPromo As System.Web.UI.WebControls.Label
    Protected WithEvents lbCodPromo As System.Web.UI.WebControls.Label
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label


    ' para calculo de totales
    Dim totales(7) As Double
    Dim totRows As Double

    Dim tUserInfo As usuario.t_Usuario
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ddlPromotora As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim ano_periodo, mes_periodo As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        Page.Server.ScriptTimeout = 90
        Dim nom_promotora
        Dim cod_promotora = ""

        lbErrors.Text = ""

        If Not Page.IsPostBack Then
            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

            cargaPromotoras(tUserInfo.codigoFilial, tUserInfo.codigoSucursal)
            cargaAnos()
            cargaMeses()


            If tUserInfo.perfil.Trim = P_PROMOTORA Then
                ' USER ES UNA PROMOTORA
                cod_promotora = tUserInfo.codigoPromo
                nom_promotora = tUserInfo.nombre

                ddlPromotora.SelectedValue = cod_promotora
                ddlPromotora.Enabled = False

                lbNota.Visible = True
                lbNota.Text = "* Solo Clientes Cartera"
            End If
        Else

            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

            If tUserInfo.perfil.Trim = P_PROMOTORA Then
                ' USER ES UNA PROMOTORA
                cod_promotora = tUserInfo.codigoPromo
                nom_promotora = tUserInfo.nombre

                lbNota.Visible = True
                lbNota.Text = "* Solo Clientes Cartera"
            ElseIf tUserInfo.perfil.Trim = P_EJECUTIVO Or tUserInfo.perfil.Trim = P_PRODMANAGER Then
                cod_promotora = ddlPromotora.SelectedValue
                nom_promotora = Utiles.get_promotora_name(tUserInfo.codigoFilial, tUserInfo.codigoSucursal, cod_promotora)
            End If

            mes_periodo = CInt(ddlMes.SelectedItem.Value)
            ano_periodo = CInt(ddlAno.SelectedItem.Value)

            Try
                Dim cod_filial As String = tUserInfo.codigoFilial
                Dim cod_sucursal As String = tUserInfo.codigoSucursal

                If cod_promotora = "" Then
                    Err.Description = "Faltan parametros para poder ejecutar la consulta."
                    Err.Raise(vbObjectError + 512 + 10, "res_vta_cliente_promo", Err.Description)
                End If

                If nom_promotora = "" Then
                    Err.Description = "No se encontró ejec. comercial con codigo: " & cod_promotora
                    Err.Raise(vbObjectError + 512 + 10, "mar_vta_promo_cliente_acu", Err.Description)
                End If

                inicializaTotales()
                generaEncabezadoDataGrid()
                dgResultado.DataSource = ventas.res_sinvta_cliente_promo(cod_promotora, nom_promotora, mes_periodo, ano_periodo, cod_filial, cod_sucursal)
                dgResultado.DataBind()

                lbCodPromo.Text = cod_promotora
                lbNomPromo.Text = nom_promotora

                ibExportar.Visible = True

            Catch ex As Exception
                lbErrors.Text = "ERROR: " & Err.Description
                Err.Clear()
                ' Throw ex
            Finally
            End Try

            lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo
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

            ibExportar.Visible = True
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

            If e.Item.Cells(2).Text().Trim() <> "" And e.Item.Cells(2).Text() <> "&nbsp;" Then
                e.Item.Cells(2).Text() = Format(Date.Parse(e.Item.Cells(2).Text()), "dd/MM/yy").Replace("-", "/")
            End If

            For i = 1 To 6
                If e.Item.Cells(i + 2).Text() <> "" And e.Item.Cells(i + 2).Text() <> "&nbsp;" Then
                    totales(i) += CInt(e.Item.Cells(i + 2).Text())
                    e.Item.Cells(i + 2).Text() = String.Format(cl, "{0:N0}", CDbl(e.Item.Cells(i + 2).Text()))
                End If
            Next

            totRows = totRows + 1

        End If

        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(0).Text() = "Totales : "
            For i = 1 To 6
                If i = 6 Then
                    e.Item.Cells(i + 2).Text() = String.Format(cl, "{0:N0}", CDbl(totales(i) / totRows))
                Else
                    e.Item.Cells(i + 2).Text() = String.Format(cl, "{0:N0}", totales(i))
                End If

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
            sTableHeader = "Promotora: " & Me.lbCodPromo.Text.Trim & " - " & Me.lbNomPromo.Text.Trim

            ' Agregar encabezado del informe
            Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

            Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

            ' Configuracion de impresion
            Exportar.ExcelXml.PreserveWhitespace = False
            Exportar.PageScale = 80
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DResumen Sin Venta Cliente - " & lbFecha.Text.Trim)
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Exportar.TableToExcel(xlsResultado)
            Exportar.SaveToClient(Response)
        End If
    End Sub


#Region " INICIALIZACION DE CONTROLES FORMULARIO "
    Private Sub cargaPromotoras(ByVal codigoFilial As String, ByVal codigoSucursal As String)
        Dim xDatos As DataTable = Utiles.ObtienePromotoras(codigoFilial)
        With ddlPromotora
            .Visible = True
            .DataSource = xDatos
            .DataTextField = "COL2"
            .DataValueField = "COL1"
            .DataBind()
        End With
    End Sub

    Private Sub cargaAnos()
        ddlAno.Items.Add(Year(Date.Now))
        ddlAno.Items.Add(Year(Date.Now.AddYears(-1)))
        ddlAno.Items.Add(Year(Date.Now.AddYears(-2)))
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

    Private Sub generaEncabezadoDataGrid()
        Dim qryDate As Date = New Date(ano_periodo, mes_periodo, 1)
        'Sólo redefinimos títulos de columnas dependientes a la fecha de consulta
        dgResultado.Columns(3).HeaderText = "VTA " & Format(qryDate, "yyyy") 'Venta año
        dgResultado.Columns(4).HeaderText = "VTA " & Format(qryDate.AddMonths(-1), "MMM/yy") 'Venta 1 mes atrás
        dgResultado.Columns(5).HeaderText = "VTA " & Format(qryDate.AddMonths(-2), "MMM/yy") 'Venta 2 meses atrás
        dgResultado.Columns(6).HeaderText = "VTA " & Format(qryDate.AddMonths(-3), "MMM/yy") 'Venta 3 meses atrás
        dgResultado.Columns(7).HeaderText = "VTA " & Format(qryDate.AddMonths(-4), "MMM/yy") 'Venta 4 meses atrás
        dgResultado.Columns(8).HeaderText = "PROM " & Format(qryDate.AddYears(-1), "yyyy")  'Promedio año anterior
    End Sub

#End Region

    Private Sub inicializaTotales()
        Dim i As Integer
        For i = 0 To totales.Length - 1
            totales(i) = 0
        Next
        totRows = 0
    End Sub
End Class
