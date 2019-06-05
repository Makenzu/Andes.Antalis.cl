Public Class res_pedido_couche
    Inherits System.Web.UI.Page

    Dim sTipoReporte As String

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents plParams As System.Web.UI.WebControls.Panel
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents ddlTipoReporte As System.Web.UI.WebControls.DropDownList

    Dim tUserInfo As usuario.t_Usuario
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btnNuevo As System.Web.UI.WebControls.Button
    Protected WithEvents pnlNuevo As System.Web.UI.WebControls.Panel
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents txtCodProducto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtValGramos As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtValAncho As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtValLargo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCodColor As System.Web.UI.WebControls.DropDownList

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

 
    End Sub

    Private Sub btSend_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSend.Click
        RefrescarGrilla(String.Empty)
    End Sub

    Private Sub dgResultado_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResultado.EditCommand
        lbErrors.Text = "Editando..."
        dgResultado.EditItemIndex = e.Item.ItemIndex
        RefrescarGrilla(String.Empty)
    End Sub

    Private Sub dgResultado_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResultado.CancelCommand
        lbErrors.Text = ""
        dgResultado.EditItemIndex = -1
        RefrescarGrilla(String.Empty)
    End Sub

    Private Sub dgResultado_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResultado.UpdateCommand
        Dim GrpProducto As New grp_productos

        With GrpProducto
            .cod_producto = CType(e.Item.FindControl("txtCodProductoEdit"), TextBox).Text
            .val_gramos = CType(e.Item.FindControl("txtValGramosEdit"), TextBox).Text
            .val_ancho = CType(e.Item.FindControl("txtValAnchoEdit"), TextBox).Text
            .val_largo = CType(e.Item.FindControl("txtValLargoEdit"), TextBox).Text
            .cod_color = CType(e.Item.FindControl("txtCodColorEdit"), TextBox).Text
            .cod_corte = Nothing
        End With

        Try
            materiales.upd_grp_productos(GrpProducto)
        Catch ex As Exception
            Err.Description &= "Error al actualizar en GRP_PRODUCTO."
            Err.Raise(vbObjectError + 512 + 10, "no_results", Err.Description)
        End Try

        dgResultado.EditItemIndex = -1
        RefrescarGrilla(String.Empty)
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        pnlNuevo.Visible = True
        txtCodProducto.Text = ""
        txtValGramos.Text = ""
        txtValLargo.Text = ""
        txtValAncho.Text = ""
        ddlCodColor.SelectedIndex = -1
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim GrpProducto As New grp_productos

        With GrpProducto
            .cod_producto = txtCodProducto.Text
            .cod_reporte = ddlTipoReporte.SelectedValue
            .val_gramos = txtValGramos.Text
            .val_largo = txtValLargo.Text
            .val_ancho = txtValAncho.Text
            .cod_color = ddlCodColor.SelectedValue
            .cod_corte = Nothing
        End With

        Try
            materiales.ins_grp_productos(GrpProducto)
        Catch ex As Exception
            Err.Description &= "Error al insertar en GRP_PRODUCTO."
            Err.Raise(vbObjectError + 512 + 10, "no_results", Err.Description)
        End Try

        pnlNuevo.Visible = False
        RefrescarGrilla(String.Empty)
    End Sub

    Private Sub RefrescarGrilla(ByVal order As String)
        sTipoReporte = ddlTipoReporte.SelectedItem.Value
        Dim dtResultado As New DataTable

        dtResultado = materiales.get_grp_productos(sTipoReporte, "BOL")

        If dtResultado.Rows.Count <= 0 Then
            Err.Description = "No se encontraron datos para esta consulta."
            Err.Raise(vbObjectError + 512 + 10, "no_results", Err.Description)
        End If
        Dim dvResultado As DataView = New DataView(dtResultado)
        dvResultado.Sort = order

        dgResultado.DataSource = dvResultado
        dgResultado.DataBind()
    End Sub

    Private Sub dgResultado_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgResultado.SortCommand
        RefrescarGrilla(e.SortExpression)
    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            If e.Item.Cells(8).Text <> e.Item.Cells(9).Text Then
                e.Item.Style.Add("background-color", "IndianRed")
            End If
        End If
    End Sub
End Class
