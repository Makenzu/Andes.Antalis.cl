Imports Exportador

Public Class vta_x_item
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents plDatos As System.Web.UI.WebControls.Panel
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage

    Dim tUserInfo As usuario.t_Usuario

    ' sub totales por subfamilia
    Dim subTotMeses(13) As Integer
    Dim subTotMesAct(2) As Integer
    Dim subTotAnoAcc(2) As Integer
    Dim totItemsSubfam As Integer ' total de items en una subfamilia

    ' total (solo usado cuando se busca por familia )
    Dim totMeses(13) As Integer
    Dim totMesAct(2) As Integer
    Dim totAnoAcc(2) As Integer

    Protected WithEvents tbResultado As System.Web.UI.WebControls.Table
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents plParams As System.Web.UI.WebControls.Panel
    Protected WithEvents rbCodFam As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txFamilia As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rbSubfam As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txSubfam As System.Web.UI.WebControls.TextBox
    Protected WithEvents txCodProd As System.Web.UI.WebControls.TextBox
    Protected WithEvents lbData As System.Web.UI.WebControls.Label
    Protected WithEvents rbCodProd As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Table3 As System.Web.UI.HtmlControls.HtmlTable
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


        Dim ano_periodo As Int16
        Dim mes_periodo As Int16

        txFamilia.Attributes.Add("onClick", "javascript:wasClicked(this);")
        txSubfam.Attributes.Add("onClick", "javascript:wasClicked(this);")
        txCodProd.Attributes.Add("onClick", "javascript:wasClicked(this);")

        rbCodFam.Attributes.Add("onClick", "javascript:wasClicked(this);")
        rbSubfam.Attributes.Add("onClick", "javascript:wasClicked(this);")
        rbCodProd.Attributes.Add("onClick", "javascript:wasClicked(this);")

        Table3.Visible = Page.IsPostBack
        Dim tUserInfo As usuario.t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

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
            Dim cod_sucursal As String = Request("ddlCentro")

            Dim dtMarVta As New DataTable

            Try


                Dim cod_producto = txCodProd.Text.Trim
                Dim cod_subfamilia = txSubfam.Text.Trim
                Dim cod_familia = txFamilia.Text.Trim

                plDatos.Visible = True
                lbErrors.Text = ""

                ' CLEAR DATATABLE
                tbResultado.DataBind()

                ' LOAD DATAGRID
                dtMarVta = ventas.vta_item_12meses_t1(ano_periodo, mes_periodo, cod_filial, cod_sucursal, cod_familia, cod_subfamilia, cod_producto)
                If dtMarVta.Rows.Count <= 0 Then
                    Err.Description = "No se encontraron datos para esta consulta."
                    Err.Raise(vbObjectError + 512 + 10, "no_results", Err.Description)
                End If
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
        Dim nomFamilia As String = dtResult.Rows(0).Item("des_familia")

        lbData.Text = dtResult.Rows(0).Item("cod_familia") & " - " & nomFamilia.ToUpper

        For x = 0 To totRows - 1

            If dtResult.Rows(x).Item("cod_subfamilia") <> prevSubfam Then

                If x > 0 Then
                    Footer_Show(Trim(prevSubfam))
                    totItemsSubfam = 0
                End If


                ' CREATE HEADER --STARTS--
                trColumnas = New TableRow

                tcValores = New TableCell
                tcValores.Text = Trim(dtResult.Rows(x).Item("cod_subfamilia")) & " - " & Trim(dtResult.Rows(x).Item("des_subfamilia")).ToUpper
                tcValores.ColumnSpan = 3
                trColumnas.Cells.Add(tcValores)

                tcValores = New TableCell
                tcValores.Text = MonthName(mes_periodo, True).ToUpper & " , " & CStr(ano_periodo)
                tcValores.ColumnSpan = 2
                tcValores.HorizontalAlign = HorizontalAlign.Center
                trColumnas.Cells.Add(tcValores)

                tcValores = New TableCell
                tcValores.Text = "ACC. " & ano_periodo
                tcValores.ColumnSpan = 2
                tcValores.HorizontalAlign = HorizontalAlign.Center
                trColumnas.Cells.Add(tcValores)

                tcValores = New TableCell
                tcValores.Text = "-- U&nbsp;L&nbsp;T&nbsp;I&nbsp;M&nbsp;O&nbsp;S&nbsp;&nbsp;&nbsp;1&nbsp;2&nbsp;&nbsp;&nbsp;M&nbsp;E&nbsp;S&nbsp;E&nbsp;S&nbsp;&nbsp;--"
                tcValores.ColumnSpan = 12
                tcValores.HorizontalAlign = HorizontalAlign.Center
                trColumnas.Cells.Add(tcValores)

                trColumnas.CssClass = "tbl-DataGridFooter"
                trColumnas.Style("background-color") = "#b0c4de"
                tbResultado.Rows.Add(trColumnas)

                ' CREATE SUBHEADER
                trColumnas = New TableRow

                tcValores = New TableCell
                tcValores.Text = "CODIGO"
                trColumnas.Cells.Add(tcValores)

                tcValores = New TableCell
                tcValores.Text = "DESCRIPCION"
                trColumnas.Cells.Add(tcValores)

                tcValores = New TableCell
                tcValores.Text = "UN.<br>MED."
                tcValores.HorizontalAlign = HorizontalAlign.Center
                trColumnas.Cells.Add(tcValores)

                tcValores = New TableCell
                tcValores.Text = "Q VTA."
                tcValores.HorizontalAlign = HorizontalAlign.Right
                trColumnas.Cells.Add(tcValores)

                tcValores = New TableCell
                tcValores.Text = "US$ VTA."
                tcValores.HorizontalAlign = HorizontalAlign.Right
                trColumnas.Cells.Add(tcValores)

                tcValores = New TableCell
                tcValores.Text = "Q VTA."
                tcValores.HorizontalAlign = HorizontalAlign.Right
                trColumnas.Cells.Add(tcValores)

                tcValores = New TableCell
                tcValores.Text = "US$ VTA."
                tcValores.HorizontalAlign = HorizontalAlign.Right
                trColumnas.Cells.Add(tcValores)

                ' ENCABEZADO DE ULTIMOS 12 MESES
                Dim dateToShow As DateTime
                dateToShow = New Date(ano_periodo, mes_periodo, 1)
                For i = 1 To 12
                    tcValores = New TableCell
                    dateToShow = dateToShow.AddMonths(-1)
                    tcValores.Text = Format(dateToShow, "MMM/yy").ToUpper
                    tcValores.HorizontalAlign = HorizontalAlign.Right
                    tcValores.Wrap = False
                    trColumnas.Cells.Add(tcValores)
                Next



                trColumnas.CssClass = "tbl-DataGridHeader"
                tbResultado.Rows.Add(trColumnas)

                ' CREATE HEADER --ENDS--

                prevSubfam = dtResult.Rows(x).Item("cod_subfamilia")
                totItemsSubfam = 0

            End If

            'tcValores.Style("background-color") = "#f8f8ff"

            ' CREAR ITEMS -- STARTS--
            trDatos = New TableRow

            tcValores = New TableCell
            'Dim hp As New HyperLink
            'hp.Text = dtResult.Rows(x).Item("cod_producto")
            'hp.NavigateUrl = "javascript:showDatos('" & Trim(dtResult.Rows(x).Item("cod_producto")) & "'," & mes_periodo & "," & ano_periodo & ");"
            'tcValores.Controls.Add(hp)
            tcValores.Text = "<a href=""javascript:showDatos('" & Trim(dtResult.Rows(x).Item("cod_producto")) & "'," & mes_periodo & _
                                        "," & ano_periodo & ");"">" & dtResult.Rows(x).Item("cod_producto") & "</a>"
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = dtResult.Rows(x).Item("des_producto")
            tcValores.Wrap = False
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = dtResult.Rows(x).Item("cod_umb")
            trDatos.Cells.Add(tcValores)

            subTotMesAct(1) += dtResult.Rows(x).Item("val_cantidad")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMesAct(2) += dtResult.Rows(x).Item("val_venta_dolar")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N2}", dtResult.Rows(x).Item("val_venta_dolar"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotAnoAcc(1) += dtResult.Rows(x).Item("val_cantidad_acc")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_acc"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotAnoAcc(2) += dtResult.Rows(x).Item("val_venta_dolar_acc")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N2}", dtResult.Rows(x).Item("val_venta_dolar_acc"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(1) += dtResult.Rows(x).Item("val_cantidad_m1")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m1"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(2) += dtResult.Rows(x).Item("val_cantidad_m2")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m2"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(3) += dtResult.Rows(x).Item("val_cantidad_m3")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m3"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(4) += dtResult.Rows(x).Item("val_cantidad_m4")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m4"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(5) += dtResult.Rows(x).Item("val_cantidad_m5")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m5"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(6) += dtResult.Rows(x).Item("val_cantidad_m6")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m6"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(7) += dtResult.Rows(x).Item("val_cantidad_m7")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m7"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(8) += dtResult.Rows(x).Item("val_cantidad_m8")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m8"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(9) += dtResult.Rows(x).Item("val_cantidad_m9")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m9"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(10) += dtResult.Rows(x).Item("val_cantidad_m10")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m10"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(11) += dtResult.Rows(x).Item("val_cantidad_m11")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m11"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            subTotMeses(12) += dtResult.Rows(x).Item("val_cantidad_m12")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_cantidad_m12"))
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
                Footer_Show(Trim(dtResult.Rows(x).Item("cod_subfamilia")))
                totItemsSubfam = 0

                If rbCodFam.Checked = True Then
                    TotalFamilia_Show(mes_periodo, ano_periodo)
                End If
            End If

        Next


    End Sub

    Private Sub Footer_Show(ByVal cod_subfamilia As String)

        Dim i As Int16
        Dim tcValores As New TableCell
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim trDatos As New TableRow

        totItemsSubfam -= 1

        ' CREATE FOOTER
        trDatos = New TableRow

        tcValores = New TableCell
        tcValores.Text = "<a href=""javascript:doHide('" & cod_subfamilia & "', " & totItemsSubfam & " )"" border='0'> <img src=""/images/up_down_arrow_grey.gif"" border='0' ></a>"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trDatos.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "TOTAL SUBFAMILIA : "
        trDatos.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = ""
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", subTotMesAct(1))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", subTotMesAct(2))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)


        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", subTotAnoAcc(1))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", subTotAnoAcc(2))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)


        ' FOOTER DE ULTIMOS 12 MESES
        For i = 1 To 12
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", subTotMeses(i))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)
            totMeses(i) += subTotMeses(i)
            subTotMeses(i) = 0
        Next

        trDatos.CssClass = "tbl-DataGridFooter"

        ' *** CODIGO PARA HIGHLIGHT  -START- ******
        trDatos.Attributes.Add("onmouseover", "this.style.backgroundColor='#90ee90';")
        trDatos.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF';")
        ' *** CODIGO PARA HIGHLIGHT  -END- ******

        tbResultado.Rows.Add(trDatos)

        ' LINEA DIVISORIA
        trDatos = New TableRow
        tcValores = New TableCell
        tcValores.ColumnSpan = 19
        tcValores.Height = New Unit(2, UnitType.Pixel)
        tcValores.Style("background-color") = "#000000"
        trDatos.Cells.Add(tcValores)
        tbResultado.Rows.Add(trDatos)

        ' SAVE TOTALES
        totMesAct(1) += subTotMesAct(1)
        totMesAct(2) += subTotMesAct(2)

        totAnoAcc(1) += subTotAnoAcc(1)
        totAnoAcc(2) += subTotAnoAcc(2)

        ' RESET SUB TOTALES
        subTotMesAct(1) = 0
        subTotMesAct(2) = 0

        subTotAnoAcc(1) = 0
        subTotAnoAcc(2) = 0

    End Sub

    Private Sub TotalFamilia_Show(ByVal mes_periodo As Integer, ByVal ano_periodo As Integer)

        Dim i, y As Int16
        Dim tcValores As New TableCell
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim trDatos As New TableRow

        trDatos = New TableRow

        tcValores = New TableCell
        tcValores.ColumnSpan = 18
        tcValores.Height = New Unit(10, UnitType.Pixel)

        trDatos.CssClass = "tbl-DataGridHeader"
        tbResultado.Rows.Add(trDatos)


        ' TOTALES
        trDatos = New TableRow

        tcValores = New TableCell
        tcValores.Text = " TOTALES FAMILIA :  "
        tcValores.ColumnSpan = 3
        trDatos.Cells.Add(tcValores)



        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totMesAct(1))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totMesAct(2))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)


        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totAnoAcc(1))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totAnoAcc(2))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trDatos.Cells.Add(tcValores)

        ' TOTALES DE ULTIMOS 12 MESES
        For i = 1 To 12
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", totMeses(i))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)
            totMeses(i) = 0
        Next

        trDatos.CssClass = "tbl-DataGridFooter"
        trDatos.Style.Add("background-color", "#ffd700")
        tbResultado.Rows.Add(trDatos)

    End Sub


    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click


        ' Nueva instancia del Informe
        Dim xlsResultado As Table = tbResultado

        If Me.rbCodFam.Checked = True Then
            Dim sTableHeader As String = "Familia: " & Me.lbData.Text.Trim

            ' Agregar encabezado del informe
            Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)
        End If

        Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

        ' Configuracion de impresion
        Exportar.PageScale = 80
        Exportar.PageLayout = "Landscape"

        ' Encabezado y Pie de Pagina
        Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DMatriz An&aacute;lisis Venta Items - " & lbFecha.Text.Trim)
        Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

        ' Exportar
        Exportar.TableToExcel(xlsResultado)
        Exportar.SaveToClient(Response)





    End Sub




End Class
