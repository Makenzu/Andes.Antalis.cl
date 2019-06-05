Public Class vta_x_familia
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
    Dim TotDolares(6) As Integer ' Totales en dolares
    Dim TotPesos(6) As Integer ' Totales en Pesos

    Dim ano_periodo As Int16
    Dim mes_periodo As Int16


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

        Else
            mes_periodo = CInt(ddlMes.SelectedItem.Value)
            ano_periodo = CInt(ddlAno.SelectedItem.Value)

            Dim cod_filial As String = tUserInfo.codigoFilial
            Dim cod_sucursal As String = tUserInfo.codigoSucursal

            Dim dtResultado As New DataTable

            Try


                plDatos.Visible = True
                lbErrors.Text = ""

                ' CLEAR DATATABLE
                tbResultado.DataBind()

                ' LOAD DATA
                dtResultado = ventas.vta_x_familia(ano_periodo, mes_periodo, cod_filial, cod_sucursal)
                If dtResultado.Rows.Count <= 0 Then
                    Err.Description = "No se encontraron datos para esta consulta."
                    Err.Raise(vbObjectError + 512 + 10, "no_results", Err.Description)
                End If

                ' CREATE REPORT
                Report_Create(dtResultado)

                ibExportar.Visible = True

            Catch ex As Exception
                ' SHOW ERROR
                lbErrors.Text = Err.Description
                lbErrors.Visible = True
                Err.Clear()
                ' HIDE DATA
                plDatos.Visible = False
            Finally
                dtResultado.Dispose()
            End Try


        End If

        lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo

    End Sub


    Private Sub Report_Create(ByVal dtResult As DataTable)

        Dim tcValores As New TableCell
        Dim trColumnas As New TableRow
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim trDatos As New TableRow
        Dim i, x, y As Integer
        Dim prevSubfam As String = "" ' subfamilia anterior
        Dim totRows As Integer = dtResult.Rows.Count


        ' CREAR ENCABEZADO
        Me.Header_Show()


        For x = 0 To totRows - 1


            'tcValores.Style("background-color") = "#f8f8ff"

            ' CREAR ITEMS -- STARTS--
            trDatos = New TableRow

            tcValores = New TableCell
            tcValores.Text = dtResult.Rows(x).Item("cod_familia")
            tcValores.RowSpan = 2
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = Trim(dtResult.Rows(x).Item("des_familia"))
            tcValores.RowSpan = 2
            trDatos.Cells.Add(tcValores)

            ' VALOR STOCK DIA 1
            TotPesos(0) += dtResult.Rows(x).Item("val_stock_d1_clp")
            TotDolares(0) += dtResult.Rows(x).Item("val_stock_d1_gusd")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_stock_d1_clp"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)


            ' VENTAS MES
            TotPesos(1) += dtResult.Rows(x).Item("val_venta_mes_clp")
            TotDolares(1) += dtResult.Rows(x).Item("val_venta_mes_gusd")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_venta_mes_clp"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)


            ' COSTO MES
            TotPesos(2) += dtResult.Rows(x).Item("cost_mes_clp")
            TotDolares(2) += dtResult.Rows(x).Item("cost_mes_gusd")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("cost_mes_clp"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)


            ' VENTAS AÑO
            TotPesos(3) += dtResult.Rows(x).Item("val_venta_ano_clp")
            TotDolares(3) += dtResult.Rows(x).Item("val_venta_ano_gusd")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_venta_ano_clp"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            ' COSTO VT. AÑO
            TotPesos(4) += dtResult.Rows(x).Item("val_costo_vt_ano_clp")
            TotDolares(4) += dtResult.Rows(x).Item("val_costo_vt_ano_gusd")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_costo_vt_ano_clp"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            ' MARGEN. AÑO
            TotPesos(5) += dtResult.Rows(x).Item("val_margen_ano_clp")
            TotDolares(5) += dtResult.Rows(x).Item("val_margen_ano_gusd")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_margen_ano_clp"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            ' STOCK PROMEDIO
            TotPesos(6) += dtResult.Rows(x).Item("val_stock_prom_clp")
            TotDolares(6) += dtResult.Rows(x).Item("val_stock_prom_gusd")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_stock_prom_clp"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)


            ' RENT %
            TotPesos(6) += dtResult.Rows(x).Item("p_rent_clp")
            TotDolares(6) += dtResult.Rows(x).Item("p_rent_gusd")
            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N2}", dtResult.Rows(x).Item("p_rent_clp"))
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

            tbResultado.Rows.Add(trDatos)


            trDatos = New TableRow

            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_stock_d1_gusd"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_venta_mes_gusd"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("cost_mes_gusd"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_venta_ano_gusd"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_costo_vt_ano_gusd"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_margen_ano_gusd"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(x).Item("val_stock_prom_gusd"))
            tcValores.HorizontalAlign = HorizontalAlign.Right
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = String.Format(cl, "{0:N2}", dtResult.Rows(x).Item("p_rent_gusd"))
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

            tbResultado.Rows.Add(trDatos)

            ' CREAR ITEMS -- ENDS--

        Next

        ' CREAR FOOTER
        '            TotalFinal_Show()

        ' CLEANUP
        trDatos.Dispose()
        trColumnas.Dispose()
        tcValores.Dispose()
        cl = Nothing

    End Sub

    ' DESPLIEGA LOS ENCABEZADOS
    Private Sub Header_Show()

        Dim tcValores As New TableCell
        Dim trColumnas As New TableRow

        ' CREATE HEADER --STARTS--
        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = "-- F A M I L I A --"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        tcValores.ColumnSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Valor Stock<br>Día 1"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Ventas<br>Mes"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Costo<br>Mes"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Ventas<br>Año"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Costo Vt.<br>Año"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Margen<br>Mes"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Stock<br>Promedio"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Rent.<br>%"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        trColumnas.Cells.Add(tcValores)

        trColumnas.CssClass = "tbl-DataGridHeader"
        tbResultado.Rows.Add(trColumnas)

        ' CREATE HEADER --ENDS--
    End Sub
End Class
