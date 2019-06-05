Public Class res_descto_manual
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents tbResultado As System.Web.UI.WebControls.Table
    Protected WithEvents rfvSubFam As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton

    Dim tUserInfo As usuario.t_Usuario
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


        Page.Server.ScriptTimeout = 90
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)



        If Not Page.IsPostBack Then
            txFecha.Text = Format(Now.AddDays(-1), "dd/MM/yyyy")

            If Not Request("sort") Is Nothing Then
                'Aplicamos ordenamiento por columna
                Dim dtMarVta As DataTable
                Dim sortCommand As String

                If (Session("TMP_DG_PRECIO_MANUAL_SORTCOMMAND") Is Nothing) Then
                    Session("TMP_DG_PRECIO_MANUAL_SORTCOMMAND") = "DESC"
                ElseIf (Session("TMP_DG_PRECIO_MANUAL_SORTCOMMAND") = "DESC") Then
                    Session("TMP_DG_PRECIO_MANUAL_SORTCOMMAND") = "ASC"
                Else
                    Session("TMP_DG_PRECIO_MANUAL_SORTCOMMAND") = "DESC"
                End If


                sortCommand = Request("sort") & " " & Session("TMP_DG_PRECIO_MANUAL_SORTCOMMAND")

                dtMarVta = Session("TMP_DG_PRECIO_MANUAL")
                Report_Create(dtMarVta, sortCommand)
                ibExportar.Visible = True

            End If

        Else

            Dim cod_filial As String = tUserInfo.codigoFilial
            Dim cod_sucursal As String = tUserInfo.codigoSucursal

            Dim dtMarVta As New DataTable

            Try

                Dim fec_periodo = txFecha.Text.Trim

                lbErrors.Text = ""

                ' CLEAR DATATABLE
                tbResultado.DataBind()

                ' LOAD DATAGRID
                dtMarVta = ventas.res_descto_manual(fec_periodo, cod_filial, cod_sucursal)
                Session("TMP_DG_PRECIO_MANUAL") = dtMarVta

                If dtMarVta.Rows.Count <= 0 Then
                    Err.Description = "No se encontraron datos para esta consulta."
                    Err.Raise(vbObjectError + 512 + 10, "no_results", Err.Description)
                Else
                    Report_Create(dtMarVta, Nothing)
                    ibExportar.Visible = True
                End If

            Catch ex As Exception
                ' SHOW ERROR
                lbErrors.Text = Err.Description
                lbErrors.Visible = True
                Err.Clear()
            Finally
                dtMarVta = Nothing
            End Try

        End If



    End Sub



    Private Sub Report_Create(ByVal dtResult As DataTable, ByVal sortCommand As String)

        Dim tcItem As TableCell
        Dim trRow As TableRow

        Dim dvDatos As DataView = New DataView(dtResult)

        If Not sortCommand Is Nothing Then
            dvDatos.Sort = sortCommand
        End If

        Dim i As Integer
        Dim totRows As Integer = dtResult.Rows.Count

        Me.Header_Create(dtResult)

        For i = 0 To totRows - 1

            trRow = New TableRow


            tcItem = New TableCell
            tcItem.Text = dvDatos.Item(i).Item("cod_promotora")
            tcItem.HorizontalAlign = HorizontalAlign.Center
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            tcItem.Text = dvDatos.Item(i).Item("cod_vendedora")
            tcItem.HorizontalAlign = HorizontalAlign.Center
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            tcItem.Text = dvDatos.Item(i).Item("cod_cliente")
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            tcItem.Text = dvDatos.Item(i).Item("nom_cliente")
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            If dtResult.Rows(i).Item("num_documento") = 0 Then
                tcItem.Text = ""
            Else
                Dim hp As New HyperLink
                hp.Text = dvDatos.Item(i).Item("cod_documento") & " &nbsp; " & dvDatos.Item(i).Item("num_documento")
                hp.NavigateUrl = "/historico/vta_detalle_documento.aspx?ap=" & Trim(dvDatos.Item(i).Item("ano_periodo")) & "&mp=" & Trim(dvDatos.Item(i).Item("mes_periodo")) & "&cf=" & Trim(dvDatos.Item(i).Item("cod_filial")) & "&cs=" & Trim(dvDatos.Item(i).Item("cod_sucursal")) & "&nd=" & Trim(dvDatos.Item(i).Item("num_documento")) & "&cd=" & Trim(dvDatos.Item(i).Item("cod_documento"))
                tcItem.Controls.Add(hp)
            End If
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            tcItem.Text = dvDatos.Item(i).Item("cod_material")
            tcItem.HorizontalAlign = HorizontalAlign.Center
            trRow.Cells.Add(tcItem)


            tcItem = New TableCell
            tcItem.HorizontalAlign = HorizontalAlign.Right
            tcItem.Text = Format(dvDatos.Item(i).Item("pre_aut"), "#,##0")
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            tcItem.HorizontalAlign = HorizontalAlign.Right
            tcItem.Text = Format(dvDatos.Item(i).Item("pre_man"), "#,##0")
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            tcItem.HorizontalAlign = HorizontalAlign.Right
            tcItem.Text = Format(dvDatos.Item(i).Item("var_pre"), "#,##0.0")
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            tcItem.HorizontalAlign = HorizontalAlign.Right
            tcItem.Text = Format(dvDatos.Item(i).Item("mg_aut"), "#,##0.0")
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            tcItem.HorizontalAlign = HorizontalAlign.Right
            tcItem.Text = Format(dvDatos.Item(i).Item("mg_man"), "#,##0.0")
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            tcItem.Text = dvDatos.Item(i).Item("cond_pp")
            tcItem.HorizontalAlign = HorizontalAlign.Center
            trRow.Cells.Add(tcItem)

            tcItem = New TableCell
            tcItem.Text = dvDatos.Item(i).Item("cond_pp_cliente")
            tcItem.HorizontalAlign = HorizontalAlign.Center
            trRow.Cells.Add(tcItem)

            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            trRow.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")

            If (i Mod 2) = 0 Then
                trRow.CssClass = "tbl-DataGridItemAlternating"
                trRow.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            Else
                trRow.CssClass = "tbl-DataGridItem"
                trRow.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If
            ' *** CODIGO PARA HIGHLIGHT  -END- ******

            tbResultado.Rows.Add(trRow)

        Next


    End Sub


    Private Sub Header_Create(ByVal dtResult As DataTable)

        Dim tcValores As TableCell
        Dim trColumnas As TableRow
        Dim myHyperLink As HyperLink


        trColumnas = New TableRow


        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "Promo."
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=cod_promotora"
        tcValores.Controls.Add(myHyperLink)
        tcValores.RowSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "Vend."
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=cod_vendedora"
        tcValores.Controls.Add(myHyperLink)
        tcValores.RowSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "Código"
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=cod_cliente"
        tcValores.Controls.Add(myHyperLink)
        tcValores.RowSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "Cliente"
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=nom_cliente"
        tcValores.Controls.Add(myHyperLink)
        tcValores.RowSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "Factura"
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=num_documento"
        tcValores.Controls.Add(myHyperLink)
        tcValores.RowSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "Material"
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=cod_material"
        tcValores.Controls.Add(myHyperLink)
        tcValores.RowSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Precio($)"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        tcValores.ColumnSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "Var. <br>Precio(%)"
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=var_pre"
        tcValores.Controls.Add(myHyperLink)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        tcValores.RowSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        tcValores.Text = "Margen(%)"
        tcValores.HorizontalAlign = HorizontalAlign.Center
        tcValores.ColumnSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "Pl. Pgo<br> Docto."
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=cond_pp"
        tcValores.Controls.Add(myHyperLink)
        tcValores.RowSpan = 2
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "Pl. Pgo<br>Cliente."
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=cond_pp_cliente"
        tcValores.Controls.Add(myHyperLink)
        tcValores.RowSpan = 2
        trColumnas.Cells.Add(tcValores)

        trColumnas.CssClass = "tbl-DataGridHeader"
        tbResultado.Rows.Add(trColumnas)

        trColumnas = New TableRow

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "AUT"
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=pre_aut"
        tcValores.Controls.Add(myHyperLink)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "MAN"
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=pre_man"
        tcValores.Controls.Add(myHyperLink)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "AUT"
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=mg_aut"
        tcValores.Controls.Add(myHyperLink)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)

        tcValores = New TableCell
        myHyperLink = New HyperLink
        myHyperLink.Text = "MAN"
        myHyperLink.NavigateUrl = "res_descto_manual.aspx?sort=mg_man"
        tcValores.Controls.Add(myHyperLink)
        tcValores.HorizontalAlign = HorizontalAlign.Right
        trColumnas.Cells.Add(tcValores)



        trColumnas.CssClass = "tbl-DataGridHeader"
        tbResultado.Rows.Add(trColumnas)

    End Sub




    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click

        Dim sTableHeader As String = "Fecha: " & Me.lbFecha.Text.Trim

        ' Nueva instancia del Informe
        Dim xlsResultado As Table = tbResultado

        ' Agregar encabezado del informe
        Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

        Dim Exportar As New Exportador.Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

        ' Configuracion de impresion
        Exportar.PageScale = 80
        Exportar.PageLayout = "Landscape"

        ' Encabezado y Pie de Pagina
        Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DControl De Descuentos Manuales Modificados - " & lbFecha.Text.Trim)
        Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

        ' Exportar
        Exportar.TableToExcel(xlsResultado)
        Exportar.SaveToClient(Response)

    End Sub
End Class
