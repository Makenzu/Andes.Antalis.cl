Imports Exportador

Public Class inv_neto_realizable
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents grdComprados As System.Web.UI.WebControls.DataGrid
    Protected WithEvents grdNoComprados As System.Web.UI.WebControls.DataGrid
    Protected WithEvents TotalGralInvMin As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents TotalGralInvAvg As System.Web.UI.HtmlControls.HtmlTableCell
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
    Dim tUserInfo As t_Usuario
    Dim ano_periodo As Int16
    Dim mes_periodo As Int16
    Dim sArea As String
    Dim iItem, iTotalItems As Integer
    Dim dValTotalInvMin, dValTotalInvAvg, dValAreaInvMin, dValAreaInvAvg, dTotalGralInvMin, dTotalGralInvAvg As Decimal

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        Page.Server.ScriptTimeout = 90
 
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)


        If Not Page.IsPostBack Then
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
        Else
            ano_periodo = CInt(ddlAno.SelectedItem.Value)
            mes_periodo = CInt(ddlMes.SelectedItem.Value)

            Dim cod_filial As String = tUserInfo.codigoFilial
            Dim cod_sucursal As String = tUserInfo.codigoSucursal
            Dim cod_sociedad As String
            Select Case cod_filial
                Case "CHI"
                    cod_sociedad = "GMSC"
                Case "BOL"
                    cod_sociedad = "ABOL"
                Case "PER"
                    cod_sociedad = "APER"
            End Select

            Try
                Dim MyDataSet As DataSet = ventas.inv_neto_realizable(ano_periodo, mes_periodo, cod_sociedad)

                iTotalItems = MyDataSet.Tables(0).Rows.Count
                dTotalGralInvMin = 0
                dTotalGralInvAvg = 0
                grdComprados.DataSource = MyDataSet.Tables(0)
                grdComprados.DataBind()

                grdNoComprados.DataSource = MyDataSet.Tables(1)
                grdNoComprados.DataBind()

                Me.TotalGralInvMin.InnerText = dTotalGralInvMin.ToString("#,##0.00")
                Me.TotalGralInvAvg.InnerText = dTotalGralInvAvg.ToString("#,##0.00")
            Catch ex As Exception
                lbErrors.Text = "ERRORES EN PAGINA: " & Err.Description
                Err.Clear()
                ' Throw ex
            Finally
                ibExportar.Visible = True
            End Try
        End If
    End Sub

    Private Sub grdComprados_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdComprados.DataBinding
        iItem = 0
        sArea = ""
        dValTotalInvMin = 0
        dValTotalInvAvg = 0
        dValAreaInvMin = 0
        dValAreaInvAvg = 0
    End Sub

    Private Sub grdComprados_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdComprados.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                If iItem = 0 Then
                    sArea = e.Item.Cells(0).Text
                    iItem = 1
                End If

                If sArea <> e.Item.Cells(0).Text Then
                    Dim dgItem As DataGridItem
                    Dim dgCell As TableCell

                    dgItem = New DataGridItem(0, 0, ListItemType.Footer)
                    dgCell = New TableCell
                    dgCell.ColumnSpan = 9
                    dgCell.Text = "Total Area " & sArea
                    dgItem.Cells.Add(dgCell)

                    dgCell = New TableCell
                    dgCell.HorizontalAlign = HorizontalAlign.Right
                    dgCell.Text = dValAreaInvMin.ToString("#,##0.00")
                    dgItem.Cells.Add(dgCell)

                    dgCell = New TableCell
                    dgCell.HorizontalAlign = HorizontalAlign.Right
                    dgCell.Text = dValAreaInvAvg.ToString("#,##0.00")
                    dgItem.Cells.Add(dgCell)

                    grdComprados.Controls(0).Controls.AddAt(e.Item.ItemIndex + iItem, dgItem)

                    iItem += 1
                    dValAreaInvMin = 0
                    dValAreaInvAvg = 0
                    sArea = e.Item.Cells(0).Text
                End If

                iTotalItems -= 1
                dValAreaInvMin += CDec(e.Item.Cells(9).Text)
                dValAreaInvAvg += CDec(e.Item.Cells(10).Text)
                dValTotalInvMin += CDec(e.Item.Cells(9).Text)
                dValTotalInvAvg += CDec(e.Item.Cells(10).Text)
                dTotalGralInvMin += CDec(e.Item.Cells(9).Text)
                dTotalGralInvAvg += CDec(e.Item.Cells(10).Text)

                'Control para el subtotal del último grupo
                If iTotalItems = 0 Then
                    Dim dgItem As DataGridItem
                    Dim dgCell As TableCell

                    dgItem = New DataGridItem(0, 0, ListItemType.Footer)
                    dgCell = New TableCell
                    dgCell.ColumnSpan = 9
                    dgCell.Text = "Total Area " & sArea
                    dgItem.Cells.Add(dgCell)

                    dgCell = New TableCell
                    dgCell.HorizontalAlign = HorizontalAlign.Right
                    dgCell.Text = dValAreaInvMin.ToString("#,##0.00")
                    dgItem.Cells.Add(dgCell)

                    dgCell = New TableCell
                    dgCell.HorizontalAlign = HorizontalAlign.Right
                    dgCell.Text = dValAreaInvAvg.ToString("#,##0.00")
                    dgItem.Cells.Add(dgCell)

                    grdComprados.Controls(0).Controls.AddAt(-1, dgItem)
                End If
            Case ListItemType.Footer
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells(0).ColumnSpan = 9
                e.Item.Cells(0).Text = "Total General Productos Comprados"
                e.Item.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Item.Cells(1).Text = dValTotalInvMin.ToString("#,##0.00")
                e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Item.Cells(2).Text = dValTotalInvAvg.ToString("#,##0.00")
        End Select
    End Sub

    Private Sub grdNoComprados_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdNoComprados.DataBinding
        iItem = 0
        sArea = ""
        dValTotalInvMin = 0
        dValTotalInvAvg = 0
        dValAreaInvMin = 0
        dValAreaInvAvg = 0
    End Sub

    Private Sub grdNoComprados_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdNoComprados.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                If iItem = 0 Then
                    sArea = e.Item.Cells(0).Text
                    iItem = 1
                End If

                If sArea <> e.Item.Cells(0).Text Then
                    Dim dgItem As DataGridItem
                    Dim dgCell As TableCell

                    dgItem = New DataGridItem(0, 0, ListItemType.Footer)
                    dgCell = New TableCell
                    dgCell.ColumnSpan = 9
                    dgCell.Text = "Total Area " & sArea
                    dgItem.Cells.Add(dgCell)

                    dgCell = New TableCell
                    dgCell.HorizontalAlign = HorizontalAlign.Right
                    dgCell.Text = dValAreaInvMin.ToString("#,##0.00")
                    dgItem.Cells.Add(dgCell)

                    dgCell = New TableCell
                    dgCell.HorizontalAlign = HorizontalAlign.Right
                    dgCell.Text = dValAreaInvAvg.ToString("#,##0.00")
                    dgItem.Cells.Add(dgCell)

                    grdNoComprados.Controls(0).Controls.AddAt(e.Item.ItemIndex + iItem, dgItem)

                    iItem += 1
                    dValAreaInvMin = 0
                    dValAreaInvAvg = 0
                    sArea = e.Item.Cells(0).Text
                End If

                iTotalItems -= 1
                dValAreaInvMin += CDec(e.Item.Cells(9).Text)
                dValAreaInvAvg += CDec(e.Item.Cells(10).Text)
                dValTotalInvMin += CDec(e.Item.Cells(9).Text)
                dValTotalInvAvg += CDec(e.Item.Cells(10).Text)
                dTotalGralInvMin += CDec(e.Item.Cells(9).Text)
                dTotalGralInvAvg += CDec(e.Item.Cells(10).Text)

                'Control para el subtotal del último grupo
                If iTotalItems = 0 Then
                    Dim dgItem As DataGridItem
                    Dim dgCell As TableCell

                    dgItem = New DataGridItem(0, 0, ListItemType.Footer)
                    dgCell = New TableCell
                    dgCell.ColumnSpan = 9
                    dgCell.Text = "Total Area " & sArea
                    dgItem.Cells.Add(dgCell)

                    dgCell = New TableCell
                    dgCell.HorizontalAlign = HorizontalAlign.Right
                    dgCell.Text = dValAreaInvMin.ToString("#,##0.00")
                    dgItem.Cells.Add(dgCell)

                    dgCell = New TableCell
                    dgCell.HorizontalAlign = HorizontalAlign.Right
                    dgCell.Text = dValAreaInvAvg.ToString("#,##0.00")
                    dgItem.Cells.Add(dgCell)

                    grdNoComprados.Controls(0).Controls.AddAt(-1, dgItem)
                End If
            Case ListItemType.Footer
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells.RemoveAt(1)
                e.Item.Cells(0).ColumnSpan = 9
                e.Item.Cells(0).Text = "Total General Productos NO Comprados"
                e.Item.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Item.Cells(1).Text = dValTotalInvMin.ToString("#,##0.00")
                e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Item.Cells(2).Text = dValTotalInvAvg.ToString("#,##0.00")
        End Select
    End Sub

    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click
        If Me.grdComprados.Items.Count > 0 Then
            ' Nueva instancia del Informe
            Dim xlsComprados As Table = CType(Me.grdComprados.Controls(0), System.Web.UI.WebControls.Table)
            Dim xlsNOComprados As Table = CType(Me.grdNoComprados.Controls(0), System.Web.UI.WebControls.Table)
            Dim TempRows(xlsNOComprados.Rows.Count - 1) As TableRow

            xlsNOComprados.Rows.CopyTo(TempRows, 0)
            xlsComprados.Rows.AddRange(TempRows)

            ' Agregar encabezado del informe
            Utiles.AddReportHeader(xlsComprados, String.Empty, String.Empty, "Inventario Neto Realizable")

            Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

            ' Configuracion de impresion
            Exportar.PageScale = 85
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DInventario Neto Realizable - " & lbFecha.Text.Trim)
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Exportar.TableToExcel(xlsComprados)
            Exportar.SaveToClient(Response)
        End If
    End Sub
End Class
