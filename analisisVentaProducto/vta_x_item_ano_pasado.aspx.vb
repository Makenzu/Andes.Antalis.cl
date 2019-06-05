Public Class vta_x_item_pasado
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents lbTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents lbProducto As System.Web.UI.WebControls.Label
    Protected WithEvents lbDescripcion As System.Web.UI.WebControls.Label
    Protected WithEvents tbResultado As System.Web.UI.WebControls.Table

    Dim totales(4) As Integer

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

        If Request("an") = "" Or Request("cp") = "" Then
            lbErrors.Text = "Faltan datos para ejecutar esta consulta."
            lbErrors.Visible = True
            Response.End()
        End If

        Dim ano_periodo As Integer = CInt(Request("an")) - 1
        Dim cod_producto As String = Request("cp")

        Dim tUserInfo As usuario.t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        Dim cod_filial As String = tUserInfo.codigoFilial
        Dim cod_sucursal As String = tUserInfo.codigoSucursal

        Dim dtMarVta As New DataTable

        lbFecha.Text = ano_periodo

        Try

            lbErrors.Text = ""

            ' CLEAR DATATABLE
            tbResultado.DataBind()

            ' LOAD DATAGRID
            dtMarVta = ventas.info_vta_item_ano(cod_producto, ano_periodo, cod_filial, cod_sucursal)
            If dtMarVta.Rows.Count <= 0 Then
                Err.Description = "No se encontraron datos para esta consulta."
                Err.Raise(vbObjectError + 512 + 10, "no_results", Err.Description)
            End If
            Report_Create(dtMarVta)


        Catch ex As Exception
            ' SHOW ERROR
            lbErrors.Text = "ERROR: " & Err.Description
            lbErrors.Visible = True
            Err.Clear()
        Finally
            dtMarVta.Dispose()
        End Try

    End Sub


    Private Sub Report_Create(ByVal dtResult As DataTable)

        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim tcValores As New TableCell
        Dim trDatos As New TableRow



        lbDescripcion.Text = dtResult.Rows(0).Item("des_producto")
        lbProducto.Text = dtResult.Rows(0).Item("cod_producto")

        tbResultado.GridLines = GridLines.Both

        Header_Create()


        ' CREATE ITEMS -- START--
        Dim i, x As Int16
        x = 1
        i = 0
        While i <= dtResult.Rows.Count - 1

            If dtResult.Rows(i).Item("mes_periodo") = x Then

                trDatos = New TableRow

                tcValores = New TableCell
                tcValores.Text = MonthName(x, True)
                trDatos.Cells.Add(tcValores)

                tcValores = New TableCell
                tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(i).Item("val_cantidad_venta"))
                tcValores.HorizontalAlign = HorizontalAlign.Right
                trDatos.Cells.Add(tcValores)
                totales(0) += dtResult.Rows(i).Item("val_cantidad_venta")

                tcValores = New TableCell
                tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(i).Item("val_venta_dolar"))
                tcValores.HorizontalAlign = HorizontalAlign.Right
                trDatos.Cells.Add(tcValores)
                totales(1) += dtResult.Rows(i).Item("val_venta_dolar")

                tcValores = New TableCell
                tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(i).Item("val_cantidad_compra"))
                tcValores.HorizontalAlign = HorizontalAlign.Right
                trDatos.Cells.Add(tcValores)
                totales(2) += dtResult.Rows(i).Item("val_cantidad_compra")

                tcValores = New TableCell
                tcValores.Text = String.Format(cl, "{0:N0}", dtResult.Rows(i).Item("val_compra_dolar"))
                tcValores.HorizontalAlign = HorizontalAlign.Right
                trDatos.Cells.Add(tcValores)
                totales(3) += dtResult.Rows(i).Item("val_compra_dolar")

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

                x += 1 ' month count
                i += 1 '  record count
            Else

                While x < dtResult.Rows(i).Item("mes_periodo")

                    trDatos = New TableRow

                    tcValores = New TableCell
                    tcValores.Text = MonthName(x, True)
                    trDatos.Cells.Add(tcValores)

                    tcValores = New TableCell
                    tcValores.Text = 0
                    tcValores.HorizontalAlign = HorizontalAlign.Right
                    trDatos.Cells.Add(tcValores)
                    tcValores = New TableCell
                    tcValores.Text = 0
                    tcValores.HorizontalAlign = HorizontalAlign.Right
                    trDatos.Cells.Add(tcValores)
                    tcValores = New TableCell
                    tcValores.Text = 0
                    tcValores.HorizontalAlign = HorizontalAlign.Right
                    trDatos.Cells.Add(tcValores)
                    tcValores = New TableCell
                    tcValores.Text = 0
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

                    x += 1 ' month count
                End While

            End If

        End While

        Footer_Create()

    End Sub

    Private Sub Header_Create()

        Dim trColumnas As New TableRow
        Dim tcValores As New TableCell

        ' CREATE HEADER -- START--
        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = "MESES"
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Q. Vta."
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "US$ Vta."
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Q. Comp."
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "US$ Comp."
        trColumnas.Cells.Add(tcValores)

        trColumnas.CssClass = "tbl-DataGridHeader"
        tbResultado.Rows.Add(trColumnas)

    End Sub



    Private Sub Footer_Create()

        Dim trColumnas As New TableRow
        Dim tcValores As New TableCell
        Dim cl As New System.Globalization.CultureInfo("es-CL")

        ' CREATE HEADER -- START--
        trColumnas = New TableRow

        tcValores = New TableCell
        tcValores.Text = "Total: "
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totales(0))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totales(1))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totales(2))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = String.Format(cl, "{0:N0}", totales(3))
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        trColumnas.CssClass = "tbl-DataGridFooter"
        tbResultado.Rows.Add(trColumnas)

    End Sub

End Class
