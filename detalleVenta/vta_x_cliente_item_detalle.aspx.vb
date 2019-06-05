Public Class vta_x_cliente_item_detalle
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private TotValVta As Integer = 0
    Private TotCanVta As Integer = 0
    Private TotValVtaAct As Integer = 0
    Private TotCanVtaAct As Integer = 0
    Private flag As Boolean = False


    Protected WithEvents lbTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lbProducto As System.Web.UI.WebControls.Label
    Protected WithEvents lbValAnoAct As System.Web.UI.WebControls.Label
    Protected WithEvents lbCanAnoAct As System.Web.UI.WebControls.Label
    Protected WithEvents lbCanMesAct As System.Web.UI.WebControls.Label
    Protected WithEvents lbValMesAct As System.Web.UI.WebControls.Label
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents lbDescripcion As System.Web.UI.WebControls.Label


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

        Dim tUserInfo As usuario.t_Usuario

        Dim des_producto As String = ""
        Dim mes_periodo As Int16
        Dim ano_periodo As Int16


        Dim cl As New System.Globalization.CultureInfo("es-CL")
        lbErrors.Text = ""

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        lbTitle.Text = "Detalle Anual Cliente-Item"
        Try
            If Trim(Request("cc")) = "" Or Trim(Request("cp")) = "" Or Trim(Request("ms")) = "" Or Trim(Request("an")) = "" Then
                Err.Description = "Faltan parametros para poder ejecutar la consulta."
                Err.Raise(vbObjectError + 512 + 10, "vta_x_cliente_item_detalle", Err.Description)
            End If

            mes_periodo = CStr(Request("ms"))
            ano_periodo = CStr(Request("an"))
            Dim cod_filial As String = tUserInfo.codigoFilial
            Dim cod_sucursal As String = tUserInfo.codigoSucursal




            dgDetalle.DataSource = ventas.vta_x_cliente_item_detalle(Request("cc"), Request("cp"), des_producto, mes_periodo, ano_periodo, cod_filial, cod_sucursal)
            dgDetalle.DataBind()

            lbDescripcion.Text = des_producto
            lbProducto.Text = Request("cp").Trim
            lbCanMesAct.Text = String.Format(cl, "{0:N0}", dgDetalle.Items(0).Cells(1).Text())
            lbValMesAct.Text = String.Format(cl, "{0:N0}", dgDetalle.Items(0).Cells(2).Text())
            lbCanAnoAct.Text = String.Format(cl, "{0:N0}", TotCanVtaAct)
            lbValAnoAct.Text = String.Format(cl, "{0:N0}", TotValVtaAct)
            dgDetalle.Items(0).Visible() = False
            'dgDetalle.Items(0).BackColor = Color.AntiqueWhite




        Catch ex As Exception
            lbErrors.Text = "ERRORES EN PAGINA: " & Err.Description
            lbErrors.Visible = True
            Err.Clear()
            ' Throw ex
        Finally
        End Try

        lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo

    End Sub

    Private Sub dgDetalle_ItemDataBound(ByVal sender As Object, _
ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDetalle.ItemDataBound


        Dim cl As New System.Globalization.CultureInfo("es-CL")

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then


            If CInt(e.Item.Cells(0).Text) <= Month(Date.Now()) And flag = False Then
                TotCanVtaAct += CInt(e.Item.Cells(1).Text)
                TotValVtaAct += CInt(e.Item.Cells(2).Text)
                If CInt(e.Item.Cells(0).Text) = 1 Then flag = True
            End If

            e.Item.Cells(0).Text() = MonthName(e.Item.Cells(0).Text())
            If e.Item.Cells(2).Text() <> "" And e.Item.Cells(2).Text() <> "&nbsp;" Then
                e.Item.Cells(2).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(2).Text()))
            End If

            If e.Item.Cells(1).Text().Trim() <> "" And e.Item.Cells(1).Text() <> "&nbsp;" Then
                e.Item.Cells(1).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(1).Text()))
            End If
        End If

        'Use the footer to display the summary row.
        'If e.Item.ItemType = ListItemType.Footer Then
        '    e.Item.Cells(0).Text = "Totales : "
        '    e.Item.Cells(0).Attributes.Add("align", "left")
        '    e.Item.Cells(1).Attributes.Add("align", "right")
        '    e.Item.Cells(1).Text = String.Format(cl, "{0:N0}", TotCanVta)
        '    e.Item.Cells(2).Attributes.Add("align", "right")
        '    e.Item.Cells(2).Text = String.Format(cl, "{0:N0}", TotValVta)
        '    lbCanAnoAct.Text = TotCanVtaAct
        '    lbValAnoAct.Text = TotValVtaAct
        'End If

    End Sub
End Class
