Public Class vta_res_facturacion_mensual
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMoneda As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList

    ' Para calculo de totales
    Dim totA As Double = 0
    Dim totAprm As Double = 0
    Dim totB As Double = 0
    Dim totBprm As Double = 0
    Dim totC As Double = 0
    Dim totCprm As Double = 0
    Dim totD As Double = 0
    Dim totDprm As Double = 0
    Dim totDB As Double = 0
    Dim totDBprm As Double = 0
    Dim totBA As Double = 0
    Dim totBAprm As Double = 0
    Dim totNoFac As Integer = 0
    Dim totRec As Integer = 0

    Dim totAMes As Double = 0
    Dim totAprmMes As Double = 0
    Dim totBMes As Double = 0
    Dim totBprmMes As Double = 0
    Dim totCMes As Double = 0
    Dim totCprmMes As Double = 0
    Dim totDMes As Double = 0
    Dim totDprmMes As Double = 0
    Dim totDBMes As Double = 0
    Dim totDBprmMes As Double = 0
    Dim totBAMes As Double = 0
    Dim totBAprmMes As Double = 0
    Dim totNoFacMes As Integer = 0
    Dim totRecMes As Integer = 0

    Protected WithEvents dgResultado2 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbTitulo2 As System.Web.UI.WebControls.Label
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton
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

        If (Not Page.IsPostBack) Then
            lbErrors.Visible = False
            btSend.Attributes.Add("onClick", "doublecheck();")

            Dim idx As String
            idx = Request("ddlMes")

            ' Months ComboBox 
            Dim i As Integer
            For i = 0 To 12
                Dim newListItem As New ListItem
                newListItem.Text = DateAdd(DateInterval.Month, -i, Date.Today.Date).ToString("MMM / yyy")
                newListItem.Value = DateAdd(DateInterval.Month, -i, Date.Today.Date)
                ddlMes.Items.Add(newListItem)
                If CStr(newListItem.Value) = idx Then
                    ddlMes.SelectedIndex = i
                End If
            Next

            'Poblamos combobox con sucursales para la filial en curso.
            Dim tUserInfo As usuario.t_Usuario
            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

            With ddlCentro
                .DataSource = obtieneSucursales(tUserInfo.codigoFilial)
                .DataTextField = "des_sucursal"
                .DataValueField = "cod_sucursal"
                .DataBind()

                If (.Items.Count > 1) Then
                    .Items.Add(New ListItem("TODOS LOS CENTROS PARA " & tUserInfo.codigoFilial, "*"))
                End If
            End With
        End If
    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound
        Dim cl As New System.Globalization.CultureInfo("es-CL")

        If e.Item.ItemType = ListItemType.Header Then
            ' CAMBIAR HEADERS SI MONEDA ES DOLAR
            If ddlMoneda.SelectedValue = "DOL" Then
                e.Item.Cells(1).Text = "AUS$"
                e.Item.Cells(2).Text = "BUS$"
                e.Item.Cells(3).Text = "CUS$"
                e.Item.Cells(4).Text = "DUS$"
            End If
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

            ' CALCULAR TOTALES
            totA += CDbl(e.Item.Cells(1).Text)
            totB += CDbl(e.Item.Cells(2).Text)
            totC += CDbl(e.Item.Cells(3).Text)
            totD += CDbl(e.Item.Cells(4).Text)
            totDB += CDbl(e.Item.Cells(5).Text)
            totBA += CDbl(e.Item.Cells(6).Text)
            totNoFac += CInt(e.Item.Cells(7).Text)
            totRec = totRec + 1

            ' FORMAT Y DESPLIEGUE DE DATOS
            Dim fecha As String
            Dim codigoFilial, codigoSucursal As String
            Dim tUserInfo As usuario.t_Usuario
            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
            codigoFilial = tUserInfo.codigoFilial
            codigoSucursal = ddlCentro.SelectedValue

            fecha = e.Item.Cells(0).Text().Trim & "/" & Right("0" & Month(Date.Parse(ddlMes.SelectedValue)), 2) & "/" & Year(Date.Parse(ddlMes.SelectedValue))
            e.Item.Cells(0).Text() = "<a href=""vta_detalle_control_factura.aspx?cf=" & codigoFilial & "&cs=" & codigoSucursal & "&fec=" & fecha & "&mo=" & ddlMoneda.SelectedValue & """ title=""Ver Detalle de Facturación"">" & e.Item.Cells(0).Text() & "</a>"

            Dim i As Int16
            For i = 1 To 4
                If e.Item.Cells(i).Text <> "&nbsp;" Then
                    e.Item.Cells(i).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(i).Text))
                Else
                    e.Item.Cells(i).Text = 0
                End If
            Next

            e.Item.Cells(5).Text = String.Format(cl, "{0:N2}", CDbl(e.Item.Cells(5).Text))
            e.Item.Cells(6).Text = String.Format(cl, "{0:N2}", CDbl(e.Item.Cells(6).Text))
            e.Item.Cells(7).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(7).Text))

            ' TOOLTIPS CON SUBTOTALES
            e.Item.Cells(1).Attributes.Add("title", "Subtotal: " & String.Format(cl, "{0:N0}", totA))
            e.Item.Cells(2).Attributes.Add("title", "Subtotal: " & String.Format(cl, "{0:N0}", totB))
            e.Item.Cells(3).Attributes.Add("title", "Subtotal: " & String.Format(cl, "{0:N0}", totC))
            e.Item.Cells(4).Attributes.Add("title", "Subtotal: " & String.Format(cl, "{0:N0}", totD))
            'e.Item.Cells(5).Attributes.Add("title", "Subtotal: " & String.Format(cl, "{0:N0}", totDB))
            'e.Item.Cells(6).Attributes.Add("title", "Subtotal: " & String.Format(cl, "{0:N0}", totBA))

        End If

        If e.Item.ItemType = ListItemType.Footer Then

            e.Item.Cells(0).Text = wrapValue("<b>Total:</b>", "<b>Promedio:</b>")

            ' PROMEDIOS
            totAprm = totA / totRec
            totBprm = totB / totRec
            totCprm = totC / totRec
            totDprm = totD / totRec

            ' SHOW TOTALES Y PROMEDIOS
            e.Item.Cells(1).Text = wrapValue("<b>" & String.Format(cl, "{0:N0}", totA), "<b>" & String.Format(cl, "{0:N0}", totAprm))
            e.Item.Cells(2).Text = wrapValue("<b>" & String.Format(cl, "{0:N0}", totB), "<b>" & String.Format(cl, "{0:N0}", totBprm))
            e.Item.Cells(3).Text = wrapValue("<b>" & String.Format(cl, "{0:N0}", totC), "<b>" & String.Format(cl, "{0:N0}", totCprm))
            e.Item.Cells(4).Text = wrapValue("<b>" & String.Format(cl, "{0:N0}", totD), "<b>" & String.Format(cl, "{0:N0}", totDprm))
            e.Item.Cells(5).Text = String.Format(cl, "{0:N2}", (totDprm / totBprm) * 100)
            e.Item.Cells(6).Text = String.Format(cl, "{0:N2}", (totBprm / totAprm) * 100)
            e.Item.Cells(7).Text = String.Format(cl, "{0:N0}", totNoFac)
        End If
    End Sub

    Private Sub dgResultado2_ItemDataBound(ByVal sender As Object, _
ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado2.ItemDataBound
        Dim cl As New System.Globalization.CultureInfo("es-CL")

        'If e.Item.ItemType = ListItemType.Header Then
        'End If

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

            ' FORMAT Y DESPLIEGUE DE DATOS
            Dim i As Int16
            For i = 1 To 4
                If e.Item.Cells(i).Text <> "&nbsp;" Then
                    e.Item.Cells(i).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(i).Text))
                Else
                    e.Item.Cells(i).Text = 0
                End If
            Next

            e.Item.Cells(5).Text = String.Format(cl, "{0:N2}", CDbl(CDbl(e.Item.Cells(4).Text) / CDbl(e.Item.Cells(2).Text)) * 100)
            e.Item.Cells(6).Text = String.Format(cl, "{0:N2}", CDbl(CDbl(e.Item.Cells(2).Text) / CDbl(e.Item.Cells(1).Text)) * 100)
            e.Item.Cells(7).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(7).Text))

            ' TOTALES
            totAMes += CDbl(e.Item.Cells(1).Text)
            totBMes += CDbl(e.Item.Cells(2).Text)
            totCMes += CDbl(e.Item.Cells(3).Text)
            totDMes += CDbl(e.Item.Cells(4).Text)
            totDBMes += CDbl(e.Item.Cells(5).Text)
            totBAMes += CDbl(e.Item.Cells(6).Text)
            totNoFacMes += CInt(e.Item.Cells(7).Text)
            totRecMes = totRecMes + 1

            ' TOOLTIPS CON SUBTOTALES
            e.Item.Cells(1).Attributes.Add("title", "Subtotal: " & String.Format(cl, "{0:N0}", totAMes))
            e.Item.Cells(2).Attributes.Add("title", "Subtotal: " & String.Format(cl, "{0:N0}", totBMes))
            e.Item.Cells(3).Attributes.Add("title", "Subtotal: " & String.Format(cl, "{0:N0}", totCMes))
            e.Item.Cells(4).Attributes.Add("title", "Subtotal: " & String.Format(cl, "{0:N0}", totDMes))
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            ' SHOW FOOTER
            e.Item.Cells(0).Text = "<b>Total:</b>"
            e.Item.Cells(1).Text = String.Format(cl, "{0:N0}", totAMes)
            e.Item.Cells(2).Text = String.Format(cl, "{0:N0}", totBMes)
            e.Item.Cells(3).Text = String.Format(cl, "{0:N0}", totCMes)
            e.Item.Cells(4).Text = String.Format(cl, "{0:N0}", totDMes)
            e.Item.Cells(5).Text = String.Format(cl, "{0:N2}", (totDMes / totBMes) * 100)
            e.Item.Cells(6).Text = String.Format(cl, "{0:N2}", (totBMes / totAMes) * 100)
            e.Item.Cells(7).Text = String.Format(cl, "{0:N0}", totNoFacMes)
        End If
    End Sub

    Private Function wrapValue(ByVal topVal As String, ByVal botVal As String) As String
        wrapValue = "<TABLE cellSpacing=""0"" cellPadding=""0"" width=""90%"" border=""0""><TR><TD align=""right""><div style=""font-size:7pt;"">" & topVal & "</div></TD></TR><TR>" & _
              "<TD bgColor=""#20b2aa"" height=""1""><IMG src=""images/transparent.gif"" width=""20"" height=""1""></TD>" & _
                 "</TR><TR><TD align=""right""><div style=""font-size:7pt;"">" & botVal & "</div></TD></TR></TABLE>"
    End Function

    Private Sub btSend_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSend.Click
        Dim dtResult As New DataTable
        Dim dtResult2 As New DataTable

        lbFecha.Text = Date.Parse(ddlMes.SelectedValue).ToString("MMMM - yy")
        If ddlMoneda.SelectedValue = "DOL" Then
            lbNota.Text = "Valores en DOLARES."
            lbNota.Visible = True
        Else
            lbNota.Text = "Valores en PESOS."
            lbNota.Visible = True
        End If

        lbTitulo2.Visible = True

        Dim tUserInfo As usuario.t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Try
            dtResult = ventas.res_facturacion_dias_mes(tUserInfo.codigoFilial, ddlCentro.SelectedValue, ddlMes.SelectedValue, ddlMoneda.SelectedValue)
            dgResultado.DataSource = dtResult
            dgResultado.DataBind()

            dtResult2 = ventas.res_facturacion_meses_ano(tUserInfo.codigoFilial, ddlCentro.SelectedValue, ddlMes.SelectedValue)
            dgResultado2.DataSource = dtResult2
            dgResultado2.DataBind()
        Catch ex As Exception
            lbErrors.Text = Err.Description
            lbErrors.Visible = True
            Err.Clear()
        Finally
            dtResult.Dispose()
            dtResult2.Dispose()
        End Try
    End Sub
End Class



