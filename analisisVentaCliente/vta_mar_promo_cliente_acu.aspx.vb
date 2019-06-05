Imports Exportador

Public Class vta_mar_promo_cliente_acu
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage

    Dim totVtaAno As Double = 0
    Dim totVtaMesUS As Double = 0
    Dim totCosMesUS As Double = 0
    Dim totMarMesUS As Double = 0
    Dim totvtaAnoUS As Double = 0
    Dim totCosAnoUS As Double = 0
    Dim totMarAnoUS As Double = 0

    Dim tUserInfo As usuario.t_Usuario

    Protected WithEvents Literal2 As System.Web.UI.WebControls.Literal
    Protected WithEvents Imagebutton1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ddlEjecCom As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbl As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents bConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents lEjecutivoComercial As System.Web.UI.WebControls.Label
    Protected WithEvents lSucursales As System.Web.UI.WebControls.Label
    Protected WithEvents tab1 As System.Web.UI.WebControls.Panel
    Protected WithEvents ibExportar2 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ddlCelula As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMes2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bConsultar2 As System.Web.UI.WebControls.Button
    Protected WithEvents cbl2 As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents lNombreCelula As System.Web.UI.WebControls.Label
    Protected WithEvents lSucursales2 As System.Web.UI.WebControls.Label
    Protected WithEvents tab2 As System.Web.UI.WebControls.Panel
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgResultado2 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents tab3 As System.Web.UI.WebControls.Panel
    Protected WithEvents ibExportar3 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ddlTopN As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lTopClientes As System.Web.UI.WebControls.Label
    Protected WithEvents lSucursales3 As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado3 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlAno3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMes3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbl3 As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents bConsultar3 As System.Web.UI.WebControls.Button
    Protected WithEvents rbVentaAcumulado As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbMgAcumulado As System.Web.UI.WebControls.RadioButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim xPeticion As Boolean

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        Dim tUserInfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)


        If Not Page.IsPostBack Then

            Literal2.Text = ""

            '----------------------------------------------------------
            'Cargamos años calendario
            '----------------------------------------------------------
            Utiles.cargaAñosCalendario(ddlAno, 2001, Now.Year)
            Utiles.cargaAñosCalendario(ddlAno2, 2001, Now.Year)
            Utiles.cargaAñosCalendario(ddlAno3, 2001, Now.Year)

            '----------------------------------------------------------
            'Cargamos meses calendario
            '----------------------------------------------------------
            Utiles.cargaMesesCalendario(ddlMes, Now.Month)
            Utiles.cargaMesesCalendario(ddlMes2, Now.Month)
            Utiles.cargaMesesCalendario(ddlMes3, Now.Month)

            With ddlTopN
                .Items.Add(New ListItem("10", "10"))
                .Items.Add(New ListItem("50", "50"))
                .Items.Add(New ListItem("100", "100"))
            End With

            '----------------------------------------------------------
            'Cargamos ejecutivos comerciales
            '----------------------------------------------------------
            Dim arrEjecutivos() As String
            If Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = Constantes.CTE_ANDES_VAL_TIPO_AGENTE_EJECUTIVO_COMERCIAL And _
               Session(Constantes.CTE_ANDES_SUPER_AGENTE) <> "X" Then
                ReDim arrEjecutivos(0)
                arrEjecutivos(0) = Session(Constantes.CTE_ANDES_CODIGO_AGENTE)
            Else
                arrEjecutivos = Nothing
            End If
            Utiles.cargaEjecutivosComerciales(ddlEjecCom, tUserInfo.codigoFilial, arrEjecutivos)

            '----------------------------------------------------------
            'Cargamos celulas
            '----------------------------------------------------------
            Dim arrCelulas() As String
            If Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = Constantes.CTE_ANDES_VAL_GRUPO_AGENTE_CELULA And _
               Session(Constantes.CTE_ANDES_SUPER_AGENTE) <> "X" Then
                ReDim arrEjecutivos(0)
                arrEjecutivos(0) = Session(Constantes.CTE_ANDES_CODIGO_CELULA)
            Else
                arrEjecutivos = Nothing
            End If
            Utiles.cargaCelulas(ddlCelula, tUserInfo.codigoFilial, arrEjecutivos)

            '----------------------------------------------------------
            'Cargamos sucursales
            '----------------------------------------------------------
            Utiles.cargaSucursales(cbl, tUserInfo.codigoFilial)
            Utiles.cargaSucursales(cbl2, tUserInfo.codigoFilial)
            Utiles.cargaSucursales(cbl3, tUserInfo.codigoFilial)

            'Si el agente es integrante de alguna célula, entonces concedemos vista a datos células
            Literal2.Text = ""

            'Lo siguiente aplica solamente a Chile
            If tUserInfo.codigoFilial = "CHI" Then

                If Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = "CELULA" Or _
                    Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) = "EJEC" Then

                    tab2.Visible = True

                    Literal2.Text &= "<LI id=""op2""><A href=""#tab2"">Célula</A></LI>"
                    Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
                                    "$(""#tab2"").show();"

                Else
                    tab2.Visible = False
                End If


                'Si el agente es ejecutivo comercial, entonces concedemos vista a datos cartera ejecutivo
                If Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = "EJECO" Or _
                        Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) = "EJEC" Then

                    tab1.Visible = True
                    Literal2.Text &= "<LI id=""op1""><A href=""#tab1"">Ejecutivo comercial</A></LI>" & vbCrLf
                    Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                                    "$(""#tab1"").show();"

                Else
                    tab1.Visible = False
                End If
            Else

                'En el caso de filiales solo dejamos habilitada consulta por ejecutiva comercial
                'Si el agente es ejecutivo comercial, entonces concedemos vista a datos cartera ejecutivo
                tab1.Visible = True
                Literal2.Text &= "<LI id=""op1""><A href=""#tab1"">Ejecutivo comercial</A></LI>"
                If Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) = "EJEC" Then
                    Literal2.Text &= "<LI id=""op3""><A href=""#tab3"">Top N</A></LI>"
                End If
                Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                                "$(""#tab1"").show();"
            End If
        End If





        'If Not Page.IsPostBack Then
        '    xPeticion = False

        '    cargaPromotoras(tUserInfo.codigoFilial)
        '    cargaAnos()
        '    cargaMeses()

        '    If tUserInfo.perfil.Trim = P_PROMOTORA Then
        '        ' USER ES UNA PROMOTORA
        '        cod_promotora = tUserInfo.codigoPromo
        '        nom_promotora = tUserInfo.nombre

        '        ddlPromotora.SelectedValue = cod_promotora
        '        ddlPromotora.Enabled = False

        '        lbNota.Visible = True
        '        lbNota.Text = "* Solo Clientes Cartera"
        '    End If



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



    Private Property SortExpression2() As String
        Get
            Dim o As Object = viewstate("SortExpression2")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return o.ToString
            End If
        End Get
        Set(ByVal Value As String)
            viewstate("SortExpression2") = Value
        End Set
    End Property

    Private Property SortAscending2() As Boolean
        Get
            Dim o As Object = viewstate("SortAscending2")
            If o Is Nothing Then
                Return True
            Else
                Return Convert.ToBoolean(o)
            End If
        End Get
        Set(ByVal Value As Boolean)
            viewstate("SortAscending2") = Value
        End Set
    End Property



    Private Property SortExpression3() As String
        Get
            Dim o As Object = viewstate("SortExpression3")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return o.ToString
            End If
        End Get
        Set(ByVal Value As String)
            viewstate("SortExpression3") = Value
        End Set
    End Property

    Private Property SortAscending3() As Boolean
        Get
            Dim o As Object = viewstate("SortAscending3")
            If o Is Nothing Then
                Return True
            Else
                Return Convert.ToBoolean(o)
            End If
        End Get
        Set(ByVal Value As Boolean)
            viewstate("SortAscending3") = Value
        End Set
    End Property




    Private Sub btSend_ServerClick(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        xPeticion = True
    End Sub


    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'If dgResultado.Items.Count > 0 Then
        '    ' Nueva instancia del Informe
        '    Dim xlsResultado As Table = CType(dgResultado.Controls(0), System.Web.UI.WebControls.Table)

        '    Dim sTableHeader As String
        '    sTableHeader = "Ejec. comercial: " & Me.lbCodPromo.Text.Trim & " - " & Me.lbNomPromo.Text.Trim

        '    ' Agregar encabezado del informe
        '    Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

        '    Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

        '    ' Configuracion de impresion
        '    Exportar.PageScale = 90
        '    Exportar.PageLayout = "Landscape"

        '    ' Encabezado y Pie de Pagina
        '    Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DRanking Margen Venta Cliente - " & lbFecha.Text.Trim)
        '    Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

        '    ' Exportar
        '    Exportar.TableToExcel(xlsResultado)
        '    Exportar.SaveToClient(Response)
        'End If
    End Sub

    Private Sub dgResultado_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgResultado.SelectedIndexChanged

    End Sub

    Private Sub bConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConsultar.Click

        Dim anoPeriodo As Integer = ddlAno.SelectedValue
        Dim mesPeriodo As Integer = ddlMes.SelectedValue
        Dim codEjecCom As String = ddlEjecCom.SelectedValue
        Dim tUserInfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim codigoFilial As String = tUserInfo.codigoFilial
        Dim codigoSucursales As String = "|"

        lSucursales.Text = ""

        For Each cb As ListItem In cbl.Items
            If cb.Selected Then
                codigoSucursales &= cb.Value & "|"
                lSucursales.Text &= cb.Value & " "
            End If
        Next
        lSucursales.Text = "Sucursales: " & lSucursales.Text.Trim.Replace(" ", " - ")
        lSucursales.Visible = True

        With dgResultado
            Dim dtResultado As DataTable = ventas.rankingMargenVentaClienteCarteraEjecCom(anoPeriodo, mesPeriodo, codigoFilial, codigoSucursales, codEjecCom)
            Session("dgResultado_x_ejecom") = dtResultado
            .DataSource = dtResultado
            .DataBind()
            Imagebutton1.Visible = True
        End With

        Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                "$(""#tab1"").show();"
    End Sub

    Private Sub bConsultar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConsultar2.Click
        Dim anoPeriodo As Integer = ddlAno2.SelectedValue
        Dim mesPeriodo As Integer = ddlMes2.SelectedValue
        Dim codigocelula As String = ddlCelula.SelectedValue
        Dim tUserInfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim codigoFilial As String = tUserInfo.codigoFilial
        Dim codigoSucursales As String = "|"

        lSucursales.Text = ""

        For Each cb As ListItem In cbl2.Items
            If cb.Selected Then
                codigoSucursales &= cb.Value & "|"
                lSucursales.Text &= cb.Value & " "
            End If
        Next

        lSucursales2.Text = "Sucursales: " & lSucursales.Text.Trim.Replace(" ", " - ")
        lSucursales2.Visible = True

        With dgResultado2
            Dim dtResultado As DataTable = ventas.rankingMargenVentaClienteCelula(anoPeriodo, mesPeriodo, codigoFilial, codigoSucursales, codigocelula)
            Session("dgResultado_x_celula") = dtResultado
            .DataSource = dtResultado
            .DataBind()

            ibExportar2.Visible = True
        End With

        Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab2"").show();"
    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound
        Dim xResultado As Double

        '  DG HEADER  CODE
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
            Dim imgUp6 As New System.Web.UI.WebControls.Image
            imgUp6.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp7 As New System.Web.UI.WebControls.Image
            imgUp7.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp8 As New System.Web.UI.WebControls.Image
            imgUp8.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp9 As New System.Web.UI.WebControls.Image
            imgUp9.ImageUrl = "/images/sort_2arrows.gif"
            e.Item.Cells(0).Controls.Add(imgUp1)
            e.Item.Cells(1).Controls.Add(imgUp2)
            e.Item.Cells(2).Controls.Add(imgUp3)
            e.Item.Cells(3).Controls.Add(imgUp4)
            e.Item.Cells(4).Controls.Add(imgUp5)
            e.Item.Cells(5).Controls.Add(imgUp6)
            e.Item.Cells(6).Controls.Add(imgUp7)
            e.Item.Cells(7).Controls.Add(imgUp8)
            e.Item.Cells(8).Controls.Add(imgUp9)
            e.Item.Cells(0).Attributes("title") = "Ordenar"
            e.Item.Cells(1).Attributes("title") = "Ordenar"
            e.Item.Cells(2).Attributes("title") = "Ordenar"
            e.Item.Cells(3).Attributes("title") = "Ordenar"
            e.Item.Cells(4).Attributes("title") = "Ordenar"
            e.Item.Cells(5).Attributes("title") = "Ordenar " & vbCrLf & "Genera Reporte: Cliente- Mensual"
            e.Item.Cells(6).Attributes("title") = "Ordenar"
            e.Item.Cells(7).Attributes("title") = "Ordenar"
            e.Item.Cells(8).Attributes("title") = "Ordenar " & vbCrLf & "Genera Reporte: Cliente- Acumulado"

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

            ' *** CODIGO PARA FORMATEO  -START- ******

            If e.Item.Cells(2).Text() <> "" And e.Item.Cells(2).Text() <> "&nbsp;" Then
                totVtaAno += e.Item.Cells(2).Text()
                e.Item.Cells(2).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(2).Text()))
            End If

            If e.Item.Cells(3).Text().Trim() <> "" And e.Item.Cells(3).Text() <> "&nbsp;" Then
                totVtaMesUS += e.Item.Cells(3).Text()
                e.Item.Cells(3).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(3).Text()))
            End If

            If e.Item.Cells(4).Text().Trim() <> "" And e.Item.Cells(4).Text() <> "&nbsp;" Then
                totCosMesUS += e.Item.Cells(4).Text()
                e.Item.Cells(4).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(4).Text()))
            End If

            If e.Item.Cells(5).Text().Trim() <> "" And e.Item.Cells(5).Text() <> "&nbsp;" Then
                totMarMesUS += e.Item.Cells(5).Text()
                e.Item.Cells(5).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(5).Text()))
            End If

            If e.Item.Cells(6).Text().Trim() <> "" And e.Item.Cells(6).Text() <> "&nbsp;" Then
                totvtaAnoUS += e.Item.Cells(6).Text()
                e.Item.Cells(6).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(6).Text()))
            End If

            If e.Item.Cells(7).Text().Trim() <> "" And e.Item.Cells(7).Text() <> "&nbsp;" Then
                totCosAnoUS += e.Item.Cells(7).Text()
                e.Item.Cells(7).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(7).Text()))
            End If

            If e.Item.Cells(8).Text().Trim() <> "" And e.Item.Cells(8).Text() <> "&nbsp;" Then
                totMarAnoUS += e.Item.Cells(8).Text()
                e.Item.Cells(8).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(8).Text()))
            End If


            ' *** CODIGO PARA FORMATEO  -END- ******

        End If

        ' DG  FOOTER CODE
        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(0).Text() = "Totales : "

            e.Item.Cells(2).Text() = String.Format(cl, "{0:N0}", totVtaAno)
            e.Item.Cells(3).Text() = String.Format(cl, "{0:N0}", totVtaMesUS)
            e.Item.Cells(4).Text() = String.Format(cl, "{0:N0}", totCosMesUS)
            e.Item.Cells(5).Text() = String.Format(cl, "{0:N0}", totMarMesUS)
            e.Item.Cells(6).Text() = String.Format(cl, "{0:N0}", totvtaAnoUS)
            e.Item.Cells(7).Text() = String.Format(cl, "{0:N0}", totCosAnoUS)
            e.Item.Cells(8).Text() = String.Format(cl, "{0:N0}", totMarAnoUS)
            xResultado = IIf(totvtaAnoUS = 0, 0, (totMarAnoUS / totvtaAnoUS) * 100)
            e.Item.Cells(9).Text() = String.Format(cl, "{0:N2}", CDbl(xResultado))

        End If
    End Sub

    Private Sub dgResultado_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgResultado.SortCommand

        totVtaAno = 0
        totVtaMesUS = 0
        totCosMesUS = 0
        totMarMesUS = 0
        totvtaAnoUS = 0
        totCosAnoUS = 0
        totMarAnoUS = 0

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

            Dim dtResultado As DataTable = CType(Session("dgResultado_x_ejecom"), DataTable)

            Dim dv As DataView = New DataView(dtResultado)
            dv.Sort = Me.SortExpression

            dgResultado.DataSource = dv
            dgResultado.DataBind()

        Catch ex As Exception

        End Try

        Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
         "$(""#tab1"").show();"
    End Sub

    Private Sub dgResultado_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgResultado.Unload

    End Sub

    Private Sub dgResultado2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado2.ItemDataBound
        Dim xResultado As Double

        '  DG HEADER  CODE
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
            Dim imgUp6 As New System.Web.UI.WebControls.Image
            imgUp6.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp7 As New System.Web.UI.WebControls.Image
            imgUp7.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp8 As New System.Web.UI.WebControls.Image
            imgUp8.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp9 As New System.Web.UI.WebControls.Image
            imgUp9.ImageUrl = "/images/sort_2arrows.gif"
            e.Item.Cells(0).Controls.Add(imgUp1)
            e.Item.Cells(1).Controls.Add(imgUp2)
            e.Item.Cells(2).Controls.Add(imgUp3)
            e.Item.Cells(3).Controls.Add(imgUp4)
            e.Item.Cells(4).Controls.Add(imgUp5)
            e.Item.Cells(5).Controls.Add(imgUp6)
            e.Item.Cells(6).Controls.Add(imgUp7)
            e.Item.Cells(7).Controls.Add(imgUp8)
            e.Item.Cells(8).Controls.Add(imgUp9)
            e.Item.Cells(0).Attributes("title") = "Ordenar"
            e.Item.Cells(1).Attributes("title") = "Ordenar"
            e.Item.Cells(2).Attributes("title") = "Ordenar"
            e.Item.Cells(3).Attributes("title") = "Ordenar"
            e.Item.Cells(4).Attributes("title") = "Ordenar"
            e.Item.Cells(5).Attributes("title") = "Ordenar " & vbCrLf & "Genera Reporte: Cliente- Mensual"
            e.Item.Cells(6).Attributes("title") = "Ordenar"
            e.Item.Cells(7).Attributes("title") = "Ordenar"
            e.Item.Cells(8).Attributes("title") = "Ordenar " & vbCrLf & "Genera Reporte: Cliente- Acumulado"

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

            ' *** CODIGO PARA FORMATEO  -START- ******

            If e.Item.Cells(2).Text() <> "" And e.Item.Cells(2).Text() <> "&nbsp;" Then
                totVtaAno += e.Item.Cells(2).Text()
                e.Item.Cells(2).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(2).Text()))
            End If

            If e.Item.Cells(3).Text().Trim() <> "" And e.Item.Cells(3).Text() <> "&nbsp;" Then
                totVtaMesUS += e.Item.Cells(3).Text()
                e.Item.Cells(3).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(3).Text()))
            End If

            If e.Item.Cells(4).Text().Trim() <> "" And e.Item.Cells(4).Text() <> "&nbsp;" Then
                totCosMesUS += e.Item.Cells(4).Text()
                e.Item.Cells(4).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(4).Text()))
            End If

            If e.Item.Cells(5).Text().Trim() <> "" And e.Item.Cells(5).Text() <> "&nbsp;" Then
                totMarMesUS += e.Item.Cells(5).Text()
                e.Item.Cells(5).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(5).Text()))
            End If

            If e.Item.Cells(6).Text().Trim() <> "" And e.Item.Cells(6).Text() <> "&nbsp;" Then
                totvtaAnoUS += e.Item.Cells(6).Text()
                e.Item.Cells(6).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(6).Text()))
            End If

            If e.Item.Cells(7).Text().Trim() <> "" And e.Item.Cells(7).Text() <> "&nbsp;" Then
                totCosAnoUS += e.Item.Cells(7).Text()
                e.Item.Cells(7).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(7).Text()))
            End If

            If e.Item.Cells(8).Text().Trim() <> "" And e.Item.Cells(8).Text() <> "&nbsp;" Then
                totMarAnoUS += e.Item.Cells(8).Text()
                e.Item.Cells(8).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(8).Text()))
            End If


            ' *** CODIGO PARA FORMATEO  -END- ******

        End If

        ' DG  FOOTER CODE
        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(0).Text() = "Totales : "

            e.Item.Cells(2).Text() = String.Format(cl, "{0:N0}", totVtaAno)
            e.Item.Cells(3).Text() = String.Format(cl, "{0:N0}", totVtaMesUS)
            e.Item.Cells(4).Text() = String.Format(cl, "{0:N0}", totCosMesUS)
            e.Item.Cells(5).Text() = String.Format(cl, "{0:N0}", totMarMesUS)
            e.Item.Cells(6).Text() = String.Format(cl, "{0:N0}", totvtaAnoUS)
            e.Item.Cells(7).Text() = String.Format(cl, "{0:N0}", totCosAnoUS)
            e.Item.Cells(8).Text() = String.Format(cl, "{0:N0}", totMarAnoUS)
            xResultado = IIf(totvtaAnoUS = 0, 0, (totMarAnoUS / totvtaAnoUS) * 100)
            e.Item.Cells(9).Text() = String.Format(cl, "{0:N2}", CDbl(xResultado))

        End If
    End Sub


    Private Sub dgResultado2_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgResultado2.SortCommand

        totVtaAno = 0
        totVtaMesUS = 0
        totCosMesUS = 0
        totMarMesUS = 0
        totvtaAnoUS = 0
        totCosAnoUS = 0
        totMarAnoUS = 0

        Try
            Dim ColumnToSort As String
            Dim SortExprs() As String

            SortExprs = Split(e.SortExpression, " ")
            ColumnToSort = SortExprs(0)

            If e.SortExpression.ToLower = Me.SortExpression2.ToLower Then
                ' SortAscending = Not SortAscending
                Me.SortExpression2 = ColumnToSort & " ASC"
            Else
                'SortAscending = True
                Me.SortExpression2 = ColumnToSort & " DESC"
            End If

            Dim dtResultado As DataTable = CType(Session("dgResultado_x_celula"), DataTable)

            Dim dv As DataView = New DataView(dtResultado)
            dv.Sort = Me.SortExpression2

            dgResultado2.DataSource = dv
            dgResultado2.DataBind()

        Catch ex As Exception

        End Try

        Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
         "$(""#tab2"").show();"
    End Sub

    Private Sub bConsultar3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConsultar3.Click
        Dim anoPeriodo As Integer = ddlAno3.SelectedValue
        Dim mesPeriodo As Integer = ddlMes3.SelectedValue
        Dim topN As String = ddlTopN.SelectedValue
        Dim tUserInfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim codigoFilial As String = tUserInfo.codigoFilial
        Dim codigoSucursales As String = "|"

        lSucursales.Text = ""

        For Each cb As ListItem In cbl3.Items
            If cb.Selected Then
                codigoSucursales &= cb.Value & "|"
                lSucursales.Text &= cb.Value & " "
            End If
        Next

        lSucursales3.Text = "Sucursales: " & lSucursales.Text.Trim.Replace(" ", " - ")
        lSucursales3.Visible = True

        Dim concepto As String = ""
        If rbMgAcumulado.Checked Then
            concepto = "MG"
        ElseIf rbVentaAcumulado.Checked Then
            concepto = "VTA"
        End If

        With dgResultado3
            Dim dtResultado As DataTable = ventas.rankingMargenVentaClienteTopN(anoPeriodo, mesPeriodo, codigoFilial, codigoSucursales, topN, concepto)
            Session("dgResultado_top_clientes") = dtResultado
            .DataSource = dtResultado
            .DataBind()
            ibExportar3.Visible = True
        End With

        Literal1.Text = "$(""#op3"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab3"").show();"
    End Sub

    Private Sub dgResultado3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado3.ItemDataBound
        Dim xResultado As Double

        '  DG HEADER  CODE
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
            Dim imgUp6 As New System.Web.UI.WebControls.Image
            imgUp6.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp7 As New System.Web.UI.WebControls.Image
            imgUp7.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp8 As New System.Web.UI.WebControls.Image
            imgUp8.ImageUrl = "/images/sort_2arrows.gif"
            Dim imgUp9 As New System.Web.UI.WebControls.Image
            imgUp9.ImageUrl = "/images/sort_2arrows.gif"
            e.Item.Cells(0).Controls.Add(imgUp1)
            e.Item.Cells(1).Controls.Add(imgUp2)
            e.Item.Cells(2).Controls.Add(imgUp3)
            e.Item.Cells(3).Controls.Add(imgUp4)
            e.Item.Cells(4).Controls.Add(imgUp5)
            e.Item.Cells(5).Controls.Add(imgUp6)
            e.Item.Cells(6).Controls.Add(imgUp7)
            e.Item.Cells(7).Controls.Add(imgUp8)
            e.Item.Cells(8).Controls.Add(imgUp9)
            e.Item.Cells(0).Attributes("title") = "Ordenar"
            e.Item.Cells(1).Attributes("title") = "Ordenar"
            e.Item.Cells(2).Attributes("title") = "Ordenar"
            e.Item.Cells(3).Attributes("title") = "Ordenar"
            e.Item.Cells(4).Attributes("title") = "Ordenar"
            e.Item.Cells(5).Attributes("title") = "Ordenar " & vbCrLf & "Genera Reporte: Cliente- Mensual"
            e.Item.Cells(6).Attributes("title") = "Ordenar"
            e.Item.Cells(7).Attributes("title") = "Ordenar"
            e.Item.Cells(8).Attributes("title") = "Ordenar " & vbCrLf & "Genera Reporte: Cliente- Acumulado"

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

            ' *** CODIGO PARA FORMATEO  -START- ******

            If e.Item.Cells(2).Text() <> "" And e.Item.Cells(2).Text() <> "&nbsp;" Then
                totVtaAno += e.Item.Cells(2).Text()
                e.Item.Cells(2).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(2).Text()))
            End If

            If e.Item.Cells(3).Text().Trim() <> "" And e.Item.Cells(3).Text() <> "&nbsp;" Then
                totVtaMesUS += e.Item.Cells(3).Text()
                e.Item.Cells(3).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(3).Text()))
            End If

            If e.Item.Cells(4).Text().Trim() <> "" And e.Item.Cells(4).Text() <> "&nbsp;" Then
                totCosMesUS += e.Item.Cells(4).Text()
                e.Item.Cells(4).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(4).Text()))
            End If

            If e.Item.Cells(5).Text().Trim() <> "" And e.Item.Cells(5).Text() <> "&nbsp;" Then
                totMarMesUS += e.Item.Cells(5).Text()
                e.Item.Cells(5).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(5).Text()))
            End If

            If e.Item.Cells(6).Text().Trim() <> "" And e.Item.Cells(6).Text() <> "&nbsp;" Then
                totvtaAnoUS += e.Item.Cells(6).Text()
                e.Item.Cells(6).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(6).Text()))
            End If

            If e.Item.Cells(7).Text().Trim() <> "" And e.Item.Cells(7).Text() <> "&nbsp;" Then
                totCosAnoUS += e.Item.Cells(7).Text()
                e.Item.Cells(7).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(7).Text()))
            End If

            If e.Item.Cells(8).Text().Trim() <> "" And e.Item.Cells(8).Text() <> "&nbsp;" Then
                totMarAnoUS += e.Item.Cells(8).Text()
                e.Item.Cells(8).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(8).Text()))
            End If


            ' *** CODIGO PARA FORMATEO  -END- ******

        End If

        ' DG  FOOTER CODE
        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(0).Text() = "Totales : "

            e.Item.Cells(2).Text() = String.Format(cl, "{0:N0}", totVtaAno)
            e.Item.Cells(3).Text() = String.Format(cl, "{0:N0}", totVtaMesUS)
            e.Item.Cells(4).Text() = String.Format(cl, "{0:N0}", totCosMesUS)
            e.Item.Cells(5).Text() = String.Format(cl, "{0:N0}", totMarMesUS)
            e.Item.Cells(6).Text() = String.Format(cl, "{0:N0}", totvtaAnoUS)
            e.Item.Cells(7).Text() = String.Format(cl, "{0:N0}", totCosAnoUS)
            e.Item.Cells(8).Text() = String.Format(cl, "{0:N0}", totMarAnoUS)
            xResultado = IIf(totvtaAnoUS = 0, 0, (totMarAnoUS / totvtaAnoUS) * 100)
            e.Item.Cells(9).Text() = String.Format(cl, "{0:N2}", CDbl(xResultado))

        End If
    End Sub

    Private Sub dgResultado3_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgResultado3.SortCommand
        totVtaAno = 0
        totVtaMesUS = 0
        totCosMesUS = 0
        totMarMesUS = 0
        totvtaAnoUS = 0
        totCosAnoUS = 0
        totMarAnoUS = 0

        Try
            Dim ColumnToSort As String
            Dim SortExprs() As String

            SortExprs = Split(e.SortExpression, " ")
            ColumnToSort = SortExprs(0)

            If e.SortExpression.ToLower = Me.SortExpression3.ToLower Then
                ' SortAscending = Not SortAscending
                Me.SortExpression3 = ColumnToSort & " ASC"
            Else
                'SortAscending = True
                Me.SortExpression3 = ColumnToSort & " DESC"
            End If

            Dim dtResultado As DataTable = CType(Session("dgResultado_top_clientes"), DataTable)

            Dim dv As DataView = New DataView(dtResultado)
            dv.Sort = Me.SortExpression3

            dgResultado3.DataSource = dv
            dgResultado3.DataBind()

        Catch ex As Exception

        End Try

        Literal1.Text = "$(""#op3"").addClass(""active"").show();" & vbCrLf & _
         "$(""#tab3"").show();"
    End Sub

    Private Sub Imagebutton1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebutton1.Click
        Dim usuarioSesion As t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim dtResult As DataTable = Session("dgResultado_x_ejecom")

        If dtResult.Rows.Count > 0 Then
            Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))
            ' Configuracion de impresion
            Exportar.PageScale = 85
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = ""
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Dim fechaConsulta As DateTime = New DateTime(ddlAno.SelectedValue, ddlMes.SelectedValue, 1)

            Exportar.TableToExcel(preparaTabla("RANKING MARGEN VENTA CLIENTE", _
                                                "Ejec. Comercial: " & ddlEjecCom.SelectedItem.Text, _
                                                "Sucursales: " & lSucursales.Text, _
                                                fechaConsulta, _
                                                dtResult))
            Exportar.SaveToClient(Response)

            Exportar = Nothing
        End If
    End Sub

    Private Sub ibExportar2_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar2.Click
        Dim usuarioSesion As t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim dtResult As DataTable = Session("dgResultado_x_celula")

        If dtResult.Rows.Count > 0 Then
            Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))
            ' Configuracion de impresion
            Exportar.PageScale = 85
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = ""
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Dim fechaConsulta As DateTime = New DateTime(ddlAno.SelectedValue, ddlMes.SelectedValue, 1)

            Exportar.TableToExcel(preparaTabla("RANKING MARGEN VENTA CLIENTE", _
                                                "Célula: " & ddlCelula.SelectedItem.Text, _
                                                "Sucursales: " & lSucursales.Text, _
                                                fechaConsulta, _
                                                dtResult))
            Exportar.SaveToClient(Response)

            Exportar = Nothing
        End If
    End Sub

    Private Sub ibExportar3_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar3.Click
        Dim usuarioSesion As t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim dtResult As DataTable = Session("dgResultado_top_clientes")

        If dtResult.Rows.Count > 0 Then
            Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))
            ' Configuracion de impresion
            Exportar.PageScale = 85
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = ""
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Dim fechaConsulta As DateTime = New DateTime(ddlAno.SelectedValue, ddlMes.SelectedValue, 1)

            Exportar.TableToExcel(preparaTabla("RANKING MARGEN VENTA CLIENTE", _
                                                "Top clientes: " & ddlTopN.SelectedItem.Text, _
                                                "Sucursales: " & lSucursales.Text, _
                                                fechaConsulta, _
                                                dtResult))
            Exportar.SaveToClient(Response)

            Exportar = Nothing
        End If
    End Sub
    Private Function preparaTabla(ByVal titulo As String, _
                            ByVal unidadComercial As String, _
                            ByVal sucursales As String, _
                            ByVal Periodo As DateTime, _
                            ByVal dt As DataTable) As Table

        Dim tDatos As Table = New Table

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 10
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = titulo

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 10
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = "Fecha:" & Now.ToString("dd/MMMM/yyyy HH:mm")

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 10
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = String.Format("Período: {0}", Periodo.ToString("MMMM - yyyy"))

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 10
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = "Sucursales: " & sucursales

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 10
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = unidadComercial


        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 10
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = ""

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 10
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = ""


        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)

        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = "Cod.Cliente"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = "Razón social"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = "Venta Año"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = "Vta.Mes (US$)"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = "Costo Mes (US$)"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = "Margen Mes (US$)"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Text = "Vta Año (US$)"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Text = "Costo Año (US$)"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Text = "Margen Año (US$)"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).Text = "Prom Año"

        Dim totales(6) As Double
        totales(0) = 0
        totales(1) = 0
        totales(3) = 0
        totales(4) = 0
        totales(5) = 0
        totales(6) = 0

        For Each dr As DataRow In dt.Rows
            tDatos.Rows.Add(New TableRow)

            For i As Integer = 1 To 10
                tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
            Next

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = Trim(dr("cod_cliente"))
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = Trim(dr("nom_cliente"))
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = CType(dr("vta_ano"), Double).ToString("##0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = CType(dr("vta_mes_dol"), Double).ToString("##0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = CType(dr("cos_mes_dol"), Double).ToString("##0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = CType(dr("mar_mes_dol"), Double).ToString("##0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Text = CType(dr("vta_ano_dol"), Double).ToString("##0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Text = CType(dr("cos_ano_dol"), Double).ToString("##0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Text = CType(dr("mar_ano_dol"), Double).ToString("##0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).Text = CType(dr("prm_ano_dol"), Double).ToString("##0.00")

            totales(0) += dr("vta_ano")
            totales(1) += dr("vta_mes_dol")
            totales(2) += dr("cos_mes_dol")
            totales(3) += dr("mar_mes_dol")
            totales(4) += dr("vta_ano_dol")
            totales(5) += dr("cos_ano_dol")
            totales(6) += dr("mar_ano_dol")

        Next

        tDatos.Rows.Add(New TableRow)
        For i As Integer = 1 To 10
            tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        Next
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = ""
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = "TOTALES:"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = totales(0).ToString("#,##0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = totales(1).ToString("#,##0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = totales(2).ToString("#,##0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = totales(3).ToString("#,##0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Text = totales(4).ToString("#,##0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Text = totales(5).ToString("#,##0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Text = totales(6).ToString("#,##0")


        'If totales(0) <> 0 Then
        'tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).Text = (100.0 * (totales(1) / totales(0))).ToString("#,##0.0")
        'End If

        Return tDatos

    End Function

End Class
