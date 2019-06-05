Public Class vta_consolidado_couche
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents plDatos As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents tbResultado As System.Web.UI.WebControls.Table
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage

    Dim tUserInfo As usuario.t_Usuario

    ' sub totales por subfamilia
    Dim subTotMeses(7) As Integer ' Totales mensual por subfamilia (incluye mes actual)
    Dim totItemsSubfam As Integer ' Total items en una subfamilia
    Dim subTotStock As Integer    ' Subtotal del stock
    Dim totFinStock As Integer    ' Total final del stock
    Dim subTotPendientes As Integer    ' Subtotal de pedidos pendientes
    Dim totFinPendientes As Integer    ' Total final de pedidos pendientes
    Dim subTotSugeridos As Integer    ' Subtotal de pedidos sugeridos
    Dim totFinSugeridos As Integer    ' Total final de pedidos sugeridos
    Dim totFinMeses(7) As Integer ' Total final

    Dim ano_periodo As Int16
    Dim mes_periodo As Int16
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

            With ddlCentro
                .DataSource = obtieneSucursales(tUserInfo.codigoFilial)
                .DataTextField = "des_sucursal"
                .DataValueField = "cod_sucursal"
                .DataBind()
            End With

        Else
            mes_periodo = CInt(ddlMes.SelectedItem.Value)
            ano_periodo = CInt(ddlAno.SelectedItem.Value)

            Dim cod_filial As String = tUserInfo.codigoFilial
            Dim cod_sucursal As String = ddlCentro.SelectedValue

            Dim dtMarVta As New DataTable

            Try


                plDatos.Visible = True
                lbErrors.Text = ""

                ' CLEAR DATATABLE
                tbResultado.DataBind()

                ' LOAD DATA
                dtMarVta = ventas.vta_consolidado_couche(ano_periodo, mes_periodo, cod_filial, cod_sucursal)
                If dtMarVta.Rows.Count <= 0 Then
                    Err.Description = "No se encontraron datos para esta consulta."
                    Err.Raise(vbObjectError + 512 + 10, "no_results", Err.Description)
                End If

                ' CREATE REPORT
                Report_Create(dtMarVta, mes_periodo, ano_periodo)

                ibExportar.Visible = True

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

        lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo

    End Sub

    Private Sub Report_Create(ByVal dtResult As DataTable, ByVal mes_periodo As Integer, ByVal ano_periodo As Integer)

        Dim tcValores As New TableCell
        Dim trColumnas As New TableRow
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim trDatos As New TableRow
        Dim i, x, y As Integer
        Dim prevSubfam As String = "" ' subfamilia anterior
        Dim totRows As Integer = dtResult.Rows.Count

        For x = 0 To totRows - 1

            If dtResult.Rows(x).Item("cod_subfamilia") <> prevSubfam Then

                If x > 0 Then
                    Subtotal_Show(Trim(prevSubfam))
                    totItemsSubfam = 0
                End If

                Me.Header_Show(Trim(dtResult.Rows(x).Item("cod_subfamilia")), Trim(dtResult.Rows(x).Item("des_subfamilia")), Trim(dtResult.Rows(x).Item("des_familia")))

                prevSubfam = dtResult.Rows(x).Item("cod_subfamilia")
                totItemsSubfam = 0
            End If

            'tcValores.Style("background-color") = "#f8f8ff"

            ' CREAR ITEMS -- STARTS--
            trDatos = New TableRow

            tcValores = New TableCell
            tcValores.Text = dtResult.Rows(x).Item("cod_producto")
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = Trim(dtResult.Rows(x).Item("des_producto"))
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = Trim(dtResult.Rows(x).Item("cod_un_vol"))
            tcValores.HorizontalAlign = HorizontalAlign.Center
            trDatos.Cells.Add(tcValores)

            subTotMeses(6) += dtResult.Rows(x).Item("mes_seis")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("mes_seis"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(5) += dtResult.Rows(x).Item("mes_cinco")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("mes_cinco"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(4) += dtResult.Rows(x).Item("mes_cuatro")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("mes_cuatro"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(3) += dtResult.Rows(x).Item("mes_tres")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("mes_tres"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(2) += dtResult.Rows(x).Item("mes_dos")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("mes_dos"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(1) += dtResult.Rows(x).Item("mes_uno")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("mes_uno"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            ' Mes Actual
            subTotMeses(0) += dtResult.Rows(x).Item("mes_actual")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("mes_actual"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("promedio"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotStock += dtResult.Rows(x).Item("stock")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("stock"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotPendientes += dtResult.Rows(x).Item("ped_pend")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("ped_pend"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotSugeridos += dtResult.Rows(x).Item("ped_sugerido")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("ped_sugerido"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N1}", dtResult.Rows(x).Item("num_mes_stock"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            trDatos.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")

            If (x Mod 2) = 0 Then
                trDatos.CssClass = "tbl-DataGridItemAlternating"
                trDatos.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            Else
                trDatos.CssClass = "tbl-DataGridItem"
                trDatos.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If
            ' *** CODIGO PARA HIGHLIGHT  -END- ******

            ' *** CODIGO PARA ESCONDER FILAS POR SUBFAMILIA -START- ***
            trDatos.ID = Trim(dtResult.Rows(x).Item("cod_subfamilia")) & totItemsSubfam
            trDatos.Attributes.Add("name", Trim(dtResult.Rows(x).Item("cod_subfamilia")) & totItemsSubfam)
            totItemsSubfam += 1
            ' *** CODIGO PARA ESCONDER FILAS POR SUBFAMILIA -END- ***

            tbResultado.Rows.Add(trDatos)

            ' CREAR ITEMS -- ENDS--

            If x = totRows - 1 Then
                Subtotal_Show(Trim(dtResult.Rows(x).Item("cod_subfamilia")))
                totItemsSubfam = 0

                TotalFinal_Show()

            End If

        Next

        ' CLEANUP
        trDatos.Dispose()
        trColumnas.Dispose()
        tcValores.Dispose()
        cl = Nothing

    End Sub

    ' DESPLIEGA LOS ENCABEZADOS
    Private Sub Header_Show(ByVal cod_subfamilia As String, ByVal des_subfamilia As String, ByVal des_familia As String)

        Dim i, x, y As Int16
        Dim tcValores As New TableCell
        Dim trColumnas As New TableRow

        Dim cl As New System.Globalization.CultureInfo("es-CL")

        ' CREATE HEADER --STARTS--
        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = (cod_subfamilia & " - " & des_familia & " " & des_subfamilia).ToUpper
        tcValores.ColumnSpan = 3
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "-- ULTIMOS 6 MESES --"
        tcValores.ColumnSpan = 6
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "-- VENTAS --"
        tcValores.ColumnSpan = 2
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)


        tcValores = New TableCell
        tcValores.Text = "STOCK"
        tcValores.RowSpan = 2
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "PED.<br> PDTES."
        tcValores.RowSpan = 2
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "PEDIDO<br>SUGER."
        tcValores.RowSpan = 2
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "N° MESES<br>STOCK"
        tcValores.RowSpan = 2
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        trColumnas.CssClass = "tbl-DataGridHeader"
        '        trColumnas.Style("background-color") = "#b0c4de"
        tbResultado.Rows.Add(trColumnas)

        ' CREATE SUBHEADER
        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = "MATERIAL"
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "DESCRIPCION"
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "UN. MED."
        trColumnas.Cells.Add(tcValores)

        ' ENCABEZADO DE ULTIMOS 6 MESES
        Dim dateToShow As Date = New Date(ano_periodo, mes_periodo, 1)
        For i = 6 To 0 Step -1
            tcValores = New TableCell
            tcValores.Text = Format(dateToShow.AddMonths(-i), "MMM/yy")
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trColumnas.Cells.Add(tcValores)
        Next

        'tcValores = New TableCell
        'tcValores.Text = "MES"
        'tcValores.HorizontalAlign = HorizontalAlign.Center
        'trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "PROMEDIO"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)


        trColumnas.CssClass = "tbl-DataGridHeader"
        tbResultado.Rows.Add(trColumnas)

        ' CREATE HEADER --ENDS--
    End Sub

    ' DESPLIEGA EL SUBTOTAL MENSUAL POR SUBFAMILIA
    Private Sub Subtotal_Show(ByVal cod_subfamilia As String)

        Dim i As Int16
        Dim tcValores As New TableCell
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim trDatos As New TableRow
        Dim prom As Integer
        Dim Promedio As Double
        Dim xStock As Double

        totItemsSubfam -= 1

        ' CREATE FOOTER
        trDatos = New TableRow

        tcValores = New TableCell
        tcValores.Text = "<a href=""javascript:doHide('" & cod_subfamilia & "', " & totItemsSubfam & " )"" border='0'> <img src=""images/up_down_arrow_grey.gif"" border='0' ></a>"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trDatos.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "TOTAL SUBFAMILIA : "
        tcValores.ColumnSpan = 2
        trDatos.Cells.Add(tcValores)


        ' FOOTER DE ULTIMOS 12 MESES
        For i = 6 To 0 Step -1
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", subTotMeses(i))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)
            totFinMeses(i) += subTotMeses(i)
            prom += subTotMeses(i)
            subTotMeses(i) = 0
        Next

        ' Subtot Mes Actual
        'tcValores = New TableCell
        'tcValores.Text = String.Format(cl, "{0:N0}", subTotMeses(0))
        'tcValores.HorizontalAlign = HorizontalAlign.Right
        'trDatos.Cells.Add(tcValores)
        'totFinMeses(0) += subTotMeses(0)
        'subTotMeses(0) = 0

        ' Subtot Promedio
        Promedio = prom / 7
        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", Promedio)
        trDatos.Cells.Add(tcValores)

        ' Subtot Stock
        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", subTotStock)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)
        totFinStock += subTotStock

        ' Subtot Ped Pend
        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", subTotPendientes)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)
        totFinPendientes += subTotPendientes

        ' Subtot Ped Sugeridos
        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", subTotSugeridos)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)
        totFinSugeridos += subTotSugeridos
        subTotSugeridos = 0

        ' Subtot N° Mes Stock
        xStock = IIf(Promedio = 0, 0, (subTotPendientes + subTotStock) / Promedio)
        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", xStock)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)

        subTotStock = 0
        subTotPendientes = 0

        trDatos.CssClass = "tbl-DataGridFooter"

        ' *** CODIGO PARA HIGHLIGHT  -START- ******
        trDatos.Attributes.Add("onmouseover", "this.style.backgroundColor='#90ee90';")
        trDatos.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF';")
        ' *** CODIGO PARA HIGHLIGHT  -END- ******

        tbResultado.Rows.Add(trDatos)

        ' LINEA DIVISORIA
        trDatos = New TableRow
        tcValores = New TableCell
        tcValores.ColumnSpan = 16
        tcValores.Height = New Unit(2, UnitType.Pixel)
        tcValores.Style("background-color") = "#000000"
        trDatos.Cells.Add(tcValores)


        tbResultado.Rows.Add(trDatos)



    End Sub

    ' DESPLIEGA EL TOTAL FINAL
    Private Sub TotalFinal_Show()

        Dim i, y As Int16
        Dim tcValores As New TableCell
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim trDatos As New TableRow
        Dim prom As Integer
        Dim Promedio As Double
        Dim xStock As Double

        ' CREATE FOOTER
        trDatos = New TableRow

        tcValores = New TableCell
        tcValores.Text = ""
        trDatos.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "TOTAL CONSOLIDADO COUCHE: "
        tcValores.HorizontalAlign = HorizontalAlign.Right
        tcValores.ColumnSpan = 2
        trDatos.Cells.Add(tcValores)


        ' FOOTER DE ULTIMOS 12 MESES
        For i = 6 To 0 Step -1
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", totFinMeses(i))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)
            prom += totFinMeses(i)
            totFinMeses(i) = 0
        Next

        ' Total Mes Actual
        'tcValores = New TableCell
        'tcValores.Text = String.Format(cl, "{0:N0}", totFinMeses(0))
        'tcValores.HorizontalAlign = HorizontalAlign.Right
        'trDatos.Cells.Add(tcValores)
        'totFinMeses(0) = 0

        ' Total Promedio
        Promedio = prom / 7
        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", Promedio)
        trDatos.Cells.Add(tcValores)

        ' Total Stock
        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totFinStock)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)

        ' Total Ped Pend
        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totFinPendientes)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)

        ' Total Ped Sugeridos
        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totFinSugeridos)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)
        totFinSugeridos = Nothing

        ' Total N° Mes Stock
        xStock = IIf(Promedio = 0, 0, (totFinPendientes + totFinStock) / Promedio)
        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", (totFinStock + totFinPendientes) / Promedio)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)

        totFinPendientes = Nothing
        totFinStock = Nothing

        trDatos.CssClass = "tbl-DataGridFooter"
        trDatos.Style.Add("background-color", "#ffd700")
        tbResultado.Rows.Add(trDatos)

    End Sub

    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click

        'Dim sTableHeader As String = "Promotora: " & Me.lbCodPromo.Text.Trim & " - " & Me.lbNomPromo.Text.Trim

        ' Nueva instancia del Informe
        Dim xlsResultado As Table = tbResultado

        ' Agregar encabezado del informe
        'Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

        Dim Exportar As New Exportador.Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

        ' Configuracion de impresion
        Exportar.PageScale = 80
        Exportar.PageLayout = "Landscape"

        ' Encabezado y Pie de Pagina
        Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DMatriz An&aacute;lisis Venta por Cliente - " & lbFecha.Text.Trim)
        Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

        ' Exportar
        Exportar.TableToExcel(xlsResultado)
        Exportar.SaveToClient(Response)


    End Sub

End Class
