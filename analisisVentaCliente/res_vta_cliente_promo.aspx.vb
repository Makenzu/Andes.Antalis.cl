Imports Exportador

Public Class res_vta_cliente_promo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList

    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents bConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents lEjecutivoComercial As System.Web.UI.WebControls.Label
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents ddlCelula As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMes2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lNombreCelula As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado2 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ibExportar2 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents tab1 As System.Web.UI.WebControls.Panel
    Protected WithEvents tab2 As System.Web.UI.WebControls.Panel
    Protected WithEvents Literal2 As System.Web.UI.WebControls.Literal
    Protected WithEvents ddlEjecCom As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bConsultar2 As System.Web.UI.WebControls.Button
    Protected WithEvents cbl2 As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents cbl As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents lSucursales As System.Web.UI.WebControls.Label
    Protected WithEvents lSucursales2 As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Imagebutton1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents dgResultado3 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents tab3 As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlSalesAdv As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMes3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbl3 As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents bConsultar3 As System.Web.UI.WebControls.Button
    Protected WithEvents lSalesAdvisor As System.Web.UI.WebControls.Label
    Protected WithEvents lSucursales3 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    ' Para calculo de totales
    Dim totales(8) As Double
    Dim totRows As Integer


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
            'Utiles.cargaAñosCalendario(ddlAno2, 2001, Now.Year)
            Utiles.cargaAñosCalendario(ddlAno3, 2001, Now.Year)

            '----------------------------------------------------------
            'Cargamos meses calendario
            '----------------------------------------------------------
            Utiles.cargaMesesCalendario(ddlMes, Now.Month)
            'Utiles.cargaMesesCalendario(ddlMes2, Now.Month)
            Utiles.cargaMesesCalendario(ddlMes3, Now.Month)

            '----------------------------------------------------------
            'Cargamos Sales Advisors (cod_grupo=CELULA ; cod_tipo_agente=EJEVE)
            '----------------------------------------------------------
            Dim arrSalesAdv() As String
            If Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = Constantes.CTE_ANDES_VAL_GRUPO_AGENTE_CELULA And _
               Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = Constantes.CTE_ANDES_VAL_TIPO_AGENTE_SALES_ADVISOR And _
               Session(Constantes.CTE_ANDES_SUPER_AGENTE) <> "X" Then
                ReDim arrSalesAdv(0)
                arrSalesAdv(0) = Session(Constantes.CTE_ANDES_CODIGO_AGENTE)
            Else
                arrSalesAdv = Nothing
            End If
            Utiles.cargaSalesAdvisors(ddlSalesAdv, tUserInfo.codigoFilial, arrSalesAdv)

            '----------------------------------------------------------
            'Cargamos ejecutivos comerciales (cod_grupo=CELULA ; cod_tipo_agente=EJECO)
            '----------------------------------------------------------
            Dim arrEjecutivos() As String
            If Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = Constantes.CTE_ANDES_VAL_GRUPO_AGENTE_CELULA And _
               Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = Constantes.CTE_ANDES_VAL_TIPO_AGENTE_EJECUTIVO_COMERCIAL And _
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

                'If Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = "CELULA" Or _
                '    Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) = "EJEC" Then

                '    tab2.Visible = True
                '    Literal2.Text &= "<LI id=""op2""><A href=""#tab2"">Célula</A></LI>"

                '    Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
                '                    "$(""#tab2"").show();"

                'Else
                '    tab2.Visible = False
                'End If


                'Si el agente es ejecutivo comercial, entonces concedemos vista a datos cartera ejecutivo
                If (Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = Constantes.CTE_ANDES_VAL_GRUPO_AGENTE_CELULA And _
                    Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = Constantes.CTE_ANDES_VAL_TIPO_AGENTE_EJECUTIVO_COMERCIAL) Or _
                    Session(Constantes.CTE_ANDES_SUPER_AGENTE) = "X" Then
                    'Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) = "EJEC" Then

                    tab1.Visible = True
                    Literal2.Text &= "<LI id=""op1""><A href=""#tab1"">Ejecutivo comercial</A></LI>"

                    Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                                    "$(""#tab1"").show();"

                Else
                    tab1.Visible = False
                End If

                'Si el agente es sales advisor, entonces concedemos vista a datos cartera sales advisor
                If (Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = Constantes.CTE_ANDES_VAL_GRUPO_AGENTE_CELULA And _
                    Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = Constantes.CTE_ANDES_VAL_TIPO_AGENTE_SALES_ADVISOR) Or _
                    Session(Constantes.CTE_ANDES_SUPER_AGENTE) = "X" Then
                    'Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) = "EJEC" Then

                    tab3.Visible = True
                    Literal2.Text &= "<LI id=""op3""><A href=""#tab3"">Sales Advisor</A></LI>"

                    Literal1.Text = "$(""#op3"").addClass(""active"").show();" & vbCrLf & _
                                    "$(""#tab3"").show();"
                Else
                    tab3.Visible = False
                End If
            Else
                'En el caso de filiales solo dejamos habilitada consulta por ejecutiva comercial
                'Si el agente es ejecutivo comercial, entonces concedemos vista a datos cartera ejecutivo
                tab1.Visible = True
                Literal2.Text &= "<LI id=""op1""><A href=""#tab1"">Ejecutivo comercial</A></LI>"

                Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                                "$(""#tab1"").show();"
            End If
        End If
    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim i As Int16

        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then


        End If

        ' DG  ITEM CODE
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            totRows = totRows + 1
            For i = 1 To 7
                If e.Item.Cells(i + 1).Text() <> "" And e.Item.Cells(i + 1).Text() <> "&nbsp;" Then
                    totales(i) += CDbl(e.Item.Cells(i + 1).Text())
                End If
            Next
            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If

            ' *** CODIGO PARA HIGHLIGHT  -END- ******
        End If

        ' DG  FOOTER CODE
        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then

            e.Item.Cells(0).Text() = "Totales : "
            For i = 1 To 7
                If i = 6 Or i = 7 Then
                    e.Item.Cells(i + 1).Text() = String.Format(cl, "{0:N0}", totales(i) / totRows)
                Else
                    e.Item.Cells(i + 1).Text() = String.Format(cl, "{0:N0}", totales(i))
                End If
            Next
        End If

    End Sub

    Private Sub dgResultado3_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim i As Int16

        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then


        End If

        ' DG  ITEM CODE
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            totRows = totRows + 1
            For i = 1 To 7
                If e.Item.Cells(i + 1).Text() <> "" And e.Item.Cells(i + 1).Text() <> "&nbsp;" Then
                    totales(i) += CDbl(e.Item.Cells(i + 1).Text())
                End If
            Next
            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If

            ' *** CODIGO PARA HIGHLIGHT  -END- ******
        End If

        ' DG  FOOTER CODE
        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then

            e.Item.Cells(0).Text() = "Totales : "
            For i = 1 To 7
                If i = 6 Or i = 7 Then
                    e.Item.Cells(i + 1).Text() = String.Format(cl, "{0:N0}", totales(i) / totRows)
                Else
                    e.Item.Cells(i + 1).Text() = String.Format(cl, "{0:N0}", totales(i))
                End If
            Next
        End If

    End Sub

#Region " ORDENAMIENTO DE COLUMNAS DATAGRID "



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

#End Region

#Region " EXPORTACION A EXCELL "
    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click
        Dim usuarioSesion As t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim dtResult As DataTable = Session("TMP_DATOS_X_EJECOM")

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

            Exportar.TableToExcel(preparaTabla("RESUMEN FACTURACION POR CLIENTE", _
                                                "Ejec. Comercial: " & ddlEjecCom.SelectedValue, _
                                                "Sucursales: " & lSucursales.Text, _
                                                dtResult, _
                                                fechaConsulta))
            Exportar.SaveToClient(Response)

            Exportar = Nothing
        End If
    End Sub


    Private Function preparaTabla(ByVal titulo1 As String, _
                                    ByVal titulo2 As String, _
                                    ByVal titulo3 As String, _
                                    ByVal dt As DataTable, _
                                    ByVal qryDate As DateTime) As Table

        Dim totales(17) As Double
        For i As Integer = 0 To 17
            totales(i) = 0
        Next

        Dim tDatos As Table = New Table

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 9
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = titulo1

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 9
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = titulo2

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 9
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = titulo3

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 9
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = ""

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 9
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = ""


        'Encabezados
        tDatos.Rows.Add(New TableRow)
        For i As Integer = 1 To 9
            tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        Next

        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = "CODCLI"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = "RAZON SOCIAL"

        tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = "VTA " & Format(qryDate, "MMM/yy") 'Venta mes actual
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = "VTA " & Format(qryDate, "yyyy") 'Venta año actual
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = "VTA " & Format(qryDate.AddMonths(-1), "MMM/yy") 'Venta 1 mes atrás
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = "VTA " & Format(qryDate.AddMonths(-2), "MMM/yy") 'Venta 1 mes atrás 'Venta 2 meses atrás
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Text = "VTA " & Format(qryDate.AddMonths(-3), "MMM/yy") 'Venta 1 mes atrás 'Venta 3 meses atrás
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Text = "PROM " & Format(qryDate, "yyyy") 'Venta año actual 'Promedio año actual
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Text = "PROM " & Format(qryDate.AddYears(-1), "yyyy")  'Venta año actual 'Promedio año anterior



        For Each dr As DataRow In dt.Rows
            tDatos.Rows.Add(New TableRow)

            For i As Integer = 1 To 9
                tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
            Next

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = Trim(dr("cod_cliente"))
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = Trim(dr("nom_cliente"))


            tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = CType(dr("val_mes_act"), Double).ToString("##0.0")
            totales(0) += dr("val_mes_act")

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = CType(dr("val_ano_act"), Double).ToString("##0.0")
            totales(1) += dr("val_ano_act")

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = CType(dr("val_mes_uno"), Double).ToString("##0.0")
            totales(2) += dr("val_mes_uno")

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = CType(dr("val_mes_dos"), Double).ToString("##0.0")
            totales(3) += dr("val_mes_dos")

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Text = CType(dr("val_mes_tres"), Double).ToString("##0.0")
            totales(4) += dr("val_mes_tres")

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Text = CType(dr("prom_ano_act"), Double).ToString("##0.0")
            totales(5) += dr("prom_ano_act")

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Text = CType(dr("prm_ano_ant"), Double).ToString("##0.0")
            totales(6) += dr("prm_ano_ant")

        Next

        tDatos.Rows.Add(New TableRow)

        For i As Integer = 1 To 9
            tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        Next

        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = ""
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = "TOTALES:"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = totales(0).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = totales(1).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = totales(2).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = totales(3).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Text = totales(4).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Text = totales(5).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Text = totales(6).ToString("#,##0.0")

        Return tDatos

    End Function

#End Region

#Region " INICIALIZACION DE CONTROLES FORMULARIO "


    Private Sub generaEncabezadoDataGrid(ByVal dg As DataGrid, ByVal anoPeriodo As Integer, ByVal mesPeriodo As Integer)
        Dim qryDate As Date = New Date(anoPeriodo, mesPeriodo, 1)
        'Sólo redefinimos títulos de columnas dependientes a la fecha de consulta
        With dg
            .Columns(2).HeaderText = "VTA " & Format(qryDate, "MMM/yy") 'Venta mes actual
            .Columns(3).HeaderText = "VTA " & Format(qryDate, "yyyy") 'Venta año actual
            .Columns(4).HeaderText = "VTA " & Format(qryDate.AddMonths(-1), "MMM/yy") 'Venta 1 mes atrás
            .Columns(5).HeaderText = "VTA " & Format(qryDate.AddMonths(-2), "MMM/yy") 'Venta 1 mes atrás 'Venta 2 meses atrás
            .Columns(6).HeaderText = "VTA " & Format(qryDate.AddMonths(-3), "MMM/yy") 'Venta 1 mes atrás 'Venta 3 meses atrás
            .Columns(7).HeaderText = "PROM " & Format(qryDate, "yyyy") 'Venta año actual 'Promedio año actual
            .Columns(8).HeaderText = "PROM " & Format(qryDate.AddYears(-1), "yyyy")  'Venta año actual 'Promedio año anterior
        End With
    End Sub

#End Region

    Private Sub bConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConsultar.Click
        Dim codEjecCom As String = ddlEjecCom.SelectedValue
        Dim anoPeriodo As Integer = ddlAno.SelectedValue
        Dim mesPeriodo As Integer = ddlMes.SelectedValue
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

        Dim nomEjecCom As String = ""
        Try

            For i As Integer = 0 To totales.Length - 1
                totales(i) = 0
            Next

            With dgResultado
                Dim dtResult As DataTable = ventas.resumenVentaClientesCarteraEjecutivo(codEjecCom, _
                                                                                        nomEjecCom, _
                                                                                        mesPeriodo, _
                                                                                        anoPeriodo, _
                                                                                        codigoFilial, _
                                                                                        codigoSucursales)
                .DataSource = dtResult
                Session("TMP_DATOS_X_EJECOM") = dtResult

                generaEncabezadoDataGrid(dgResultado, anoPeriodo, mesPeriodo)
                .DataBind()
            End With


            lEjecutivoComercial.Text = String.Format("{0} {1}", codEjecCom, nomEjecCom)
            ibExportar.Visible = True

            Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                "$(""#tab1"").show();"

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub bConsultar3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConsultar3.Click
        Dim codSalesAdv As String = ddlSalesAdv.SelectedValue
        Dim anoPeriodo As Integer = ddlAno.SelectedValue
        Dim mesPeriodo As Integer = ddlMes.SelectedValue
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

        Dim nomSalesAdv As String = ""
        Try
            For i As Integer = 0 To totales.Length - 1
                totales(i) = 0
            Next

            With dgResultado3
                Dim dtResult As DataTable = ventas.resumenVentaClientesCarteraSalesAdvisor(codSalesAdv, _
                                                                                           nomSalesAdv, _
                                                                                           mesPeriodo, _
                                                                                           anoPeriodo, _
                                                                                           codigoFilial, _
                                                                                           codigoSucursales)
                .DataSource = dtResult
                'Session("TMP_DATOS_X_EJECOM") = dtResult

                generaEncabezadoDataGrid(dgResultado3, anoPeriodo, mesPeriodo)
                .DataBind()
            End With


            lSalesAdvisor.Text = String.Format("{0} {1}", codSalesAdv, nomSalesAdv)
            ibExportar.Visible = True

            Literal1.Text = "$(""#op3"").addClass(""active"").show();" & vbCrLf & _
                "$(""#tab3"").show();"

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub dgResultado2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado2.ItemDataBound
        Dim i As Int16

        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then

        End If

        ' DG  ITEM CODE
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            totRows = totRows + 1
            For i = 1 To 7
                If e.Item.Cells(i + 1).Text() <> "" And e.Item.Cells(i + 1).Text() <> "&nbsp;" Then
                    totales(i) += CDbl(e.Item.Cells(i + 1).Text())
                End If
            Next
            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If

            ' *** CODIGO PARA HIGHLIGHT  -END- ******
        End If

        ' DG  FOOTER CODE
        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then

            e.Item.Cells(0).Text() = "Totales : "
            For i = 1 To 7
                If i = 6 Or i = 7 Then
                    e.Item.Cells(i + 1).Text() = String.Format(cl, "{0:N0}", totales(i) / totRows)
                Else
                    e.Item.Cells(i + 1).Text() = String.Format(cl, "{0:N0}", totales(i))
                End If
            Next
        End If
    End Sub

    Private Sub ibExportar2_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar2.Click
        Dim usuarioSesion As t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim dtResult As DataTable = Session("TMP_DATOS_X_CELULA")

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

            Exportar.TableToExcel(preparaTabla("RESUMEN FACTURACION POR CLIENTE", _
                                                "Celula: " & ddlCelula.SelectedValue, _
                                                "Sucursales: " & lSucursales.Text, _
                                                dtResult, _
                                                fechaConsulta))
            Exportar.SaveToClient(Response)

            Exportar = Nothing
        End If
    End Sub


    Private Sub dgResultado_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)
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


            Dim dv As DataView = New DataView(CType(Session("TMP_DATOS_X_EJECOM"), DataTable))
            dv.Sort = Me.SortExpression
            dgResultado.DataSource = dv
            dgResultado.DataBind()

            ibExportar.Visible = True

        Catch ex As Exception

        End Try

        Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                "$(""#tab1"").show();"
    End Sub

    Private Sub dgResultado2_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgResultado2.SortCommand
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


            Dim dv As DataView = New DataView(CType(Session("TMP_DATOS_X_CELULA"), DataTable))
            dv.Sort = Me.SortExpression2
            dgResultado2.DataSource = dv
            dgResultado2.DataBind()

            ibExportar2.Visible = True

        Catch ex As Exception

        End Try

        Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
                "$(""#tab2"").show();"
    End Sub

    Private Sub bConsultar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConsultar2.Click
        Dim codigoCelula As String = ddlCelula.SelectedValue
        Dim anoPeriodo As Integer = ddlAno2.SelectedValue
        Dim mesPeriodo As Integer = ddlMes2.SelectedValue
        Dim tUserInfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim codigoFilial As String = tUserInfo.codigoFilial
        Dim codigoSucursales As String = "|"

        lSucursales2.Text = ""

        For Each cb As ListItem In cbl.Items
            If cb.Selected Then
                codigoSucursales &= cb.Value & "|"
                lSucursales2.Text &= cb.Value & " "
            End If
        Next
        lSucursales2.Text = "Sucursales: " & lSucursales2.Text.Trim.Replace(" ", " - ")
        lSucursales2.Visible = True

        Dim nombreCelula As String = ""

        Try

            For i As Integer = 0 To totales.Length - 1
                totales(i) = 0
            Next

            With dgResultado2
                Dim dtResult As DataTable = ventas.resumenVentaClientesCelula(codigoCelula, _
                                                                nombreCelula, _
                                                                mesPeriodo, _
                                                                anoPeriodo, _
                                                                codigoFilial, _
                                                                codigoSucursales)

                Session("TMP_DATOS_X_CELULA") = dtResult
                generaEncabezadoDataGrid(dgResultado2, anoPeriodo, mesPeriodo)

                .DataSource = dtResult
                .DataBind()
            End With


            lNombreCelula.Text = String.Format("CÉLULA {0}", codigoCelula)
            ibExportar2.Visible = True

            Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
                            "$(""#tab2"").show();"

        Catch ex As Exception

        Finally

        End Try
    End Sub
End Class
