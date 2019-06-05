Public Class WebForm3
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents txICodProd As System.Web.UI.WebControls.TextBox

    Dim dtResult As New DataTable
    Protected WithEvents txICliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents rfvCliente As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvFechaIni As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvFechaHasta As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents cvFechas As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents plDirecta As System.Web.UI.WebControls.Panel
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents lbError As System.Web.UI.WebControls.Label
    Protected WithEvents ibDirectaxPromotora As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibDirectaxAgente As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibAgentexDirecta As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibAgentexPromotora As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibPromotoraxDirecta As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibPromotoraxAgente As System.Web.UI.WebControls.ImageButton
    Protected WithEvents plxAgente As System.Web.UI.WebControls.Panel
    Protected WithEvents plxPromotora As System.Web.UI.WebControls.Panel
    Protected WithEvents rbCodCli As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbRut As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbRazonSocial As System.Web.UI.WebControls.RadioButton
    Protected WithEvents btDirecta As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txRut As System.Web.UI.WebControls.TextBox
    Protected WithEvents txRazonSocial As System.Web.UI.WebControls.TextBox
    Protected WithEvents lbResult As System.Web.UI.WebControls.Label
    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dlAgente As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dlPromotora As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btxAgente As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btxPromotora As System.Web.UI.WebControls.ImageButton
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label


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
        If IsNothing(Session("USUARIO_AUTENTICADO")) = True Then
            Response.Redirect("logout.aspx")
            Response.End()
        End If
        lbError.Visible = False
        lbResult.Text = ""
    End Sub

    'Activación del panel que corresponde

    Private Sub ibDirectaxPromotora_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibDirectaxPromotora.Click
        Dim Resultados As New DataTable

        Resultados = buscar.buscarTodosPromotora()
        dlPromotora.DataSource = Resultados
        dlPromotora.DataTextField = "COL2"
        dlPromotora.DataValueField = "COL1"
        dlPromotora.DataBind()
        plxPromotora.Visible = True
        plDirecta.Visible = False
        plxAgente.Visible = False
        dgResultado.Visible = False
    End Sub

    Private Sub ibDirectaxAgente_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibDirectaxAgente.Click
        Dim Resultados As New DataTable

        Resultados = buscar.buscarTodosAgente()
        dlAgente.DataSource = Resultados
        dlAgente.DataTextField = "COL2"
        dlAgente.DataValueField = "COL1"
        dlAgente.DataBind()
        plxPromotora.Visible = False
        plDirecta.Visible = False
        plxAgente.Visible = True
        dgResultado.Visible = False
    End Sub

    Private Sub ibAgentexDirecta_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibAgentexDirecta.Click
        plxPromotora.Visible = False
        plDirecta.Visible = True
        plxAgente.Visible = False
        dgResultado.Visible = False
    End Sub

    Private Sub ibAgentexPromotora_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibAgentexPromotora.Click
        Dim Resultados As New DataTable

        Resultados = buscar.buscarTodosPromotora()
        dlPromotora.DataSource = Resultados
        dlPromotora.DataTextField = "COL2"
        dlPromotora.DataValueField = "COL1"
        dlPromotora.DataBind()
        plxPromotora.Visible = True
        plDirecta.Visible = False
        plxAgente.Visible = False
        dgResultado.Visible = False
    End Sub

    Private Sub ibPromotoraxDirecta_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibPromotoraxDirecta.Click
        plxPromotora.Visible = False
        plDirecta.Visible = True
        plxAgente.Visible = False
        dgResultado.Visible = False
    End Sub

    Private Sub ibPromotoraxAgente_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibPromotoraxAgente.Click
        Dim Resultados As New DataTable

        Resultados = buscar.buscarTodosAgente()
        dlAgente.DataSource = Resultados
        dlAgente.DataTextField = "COL2"
        dlAgente.DataValueField = "COL1"
        dlAgente.DataBind()
        plxPromotora.Visible = False
        plDirecta.Visible = False
        plxAgente.Visible = True
        dgResultado.Visible = False
    End Sub

    Private Sub btDirecta_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btDirecta.Click
        Dim dtEntidadesDirecta As New DataTable
        Dim sCodCli As String = String.Empty
        Dim sRutCli As String = String.Empty
        Dim sNomCli As String = String.Empty
        Dim xDato As String
        Dim xDato1 As String

        If rbCodCli.Checked = True Then
            sCodCli = Me.txCodigo.Text
        ElseIf Me.rbRazonSocial.Checked = True Then
            sNomCli = Me.txRazonSocial.Text.Trim
        ElseIf Me.rbRut.Checked = True Then
            sRutCli = Me.txRut.Text.Trim
        End If

        Try
            dtEntidadesDirecta = buscar.buscarEntidad(sCodCli, sRutCli, sNomCli)
            dgResultado.DataSource = dtEntidadesDirecta
            dgResultado.DataBind()
            dgResultado.Visible = True
            If dtEntidadesDirecta.Rows.Count <= 0 Then
                lbResult.Text = "No se encontraron registros."
            Else
                lbResult.Text = "Se encontraron " & dtEntidadesDirecta.Rows.Count & " registros."
                If dtEntidadesDirecta.Rows.Count = 1 Then
                    If rbCodCli.Checked = True Or Me.rbRut.Checked = True Then
                        Dim currRows() As DataRow = dtEntidadesDirecta.Select(Nothing, Nothing, DataViewRowState.CurrentRows)
                        xDato = currRows(0).Item(1).ToString.Trim()
                        xDato1 = currRows(0).Item(0).ToString.Trim()
                        Response.Redirect("edit_entidad.aspx?id_ent=" & xDato1 & "&tip_ent=" & xDato, True)
                    End If
                End If
            End If

        Catch ex As Exception
            lbError.Text = Err.Description
            lbError.Visible = True
        Finally
            dtEntidadesDirecta.Dispose()
        End Try
    End Sub

    Private Sub btxAgente_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btxAgente.Click
        Dim dtEntidadesAgente As New DataTable
        Dim xDato As String
        Dim xDato1 As String

        Try
            dtEntidadesAgente = buscar.buscarEntidadesxAgente(dlAgente.SelectedValue)
            dgResultado.DataSource = dtEntidadesAgente
            dgResultado.DataBind()
            dgResultado.Visible = True
            If dtEntidadesAgente.Rows.Count <= 0 Then
                lbResult.Text = "No se encontraron registros."
            Else
                lbResult.Text = "Se encontraron " & dtEntidadesAgente.Rows.Count & " registros."
                If dtEntidadesAgente.Rows.Count = 1 Then
                    Dim currRows() As DataRow = dtEntidadesAgente.Select(Nothing, Nothing, DataViewRowState.CurrentRows)
                    xDato = currRows(0).Item(1).ToString.Trim()
                    xDato1 = currRows(0).Item(0).ToString.Trim()
                    Response.Redirect("edit_entidad.aspx?id_ent=" & xDato1 & "&tip_ent=" & xDato, True)
                End If
            End If

        Catch ex As Exception
            lbError.Text = Err.Description
            lbError.Visible = True
        Finally
            dtEntidadesAgente.Dispose()
        End Try
    End Sub

    Private Sub btxPromotora_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btxPromotora.Click
        Dim dtEntidadesPromotora As New DataTable
        Dim xDato As String
        Dim xDato1 As String

        Try
            dtEntidadesPromotora = buscar.buscarEntidadesxPromotora(dlPromotora.SelectedValue)
            dgResultado.DataSource = dtEntidadesPromotora
            dgResultado.DataBind()
            dgResultado.Visible = True
            If dtEntidadesPromotora.Rows.Count <= 0 Then
                lbResult.Text = "No se encontraron registros."
            Else
                lbResult.Text = "Se encontraron " & dtEntidadesPromotora.Rows.Count & " registros."
                If dtEntidadesPromotora.Rows.Count = 1 Then
                    Dim currRows() As DataRow = dtEntidadesPromotora.Select(Nothing, Nothing, DataViewRowState.CurrentRows)
                    xDato = currRows(0).Item(1).ToString.Trim()
                    xDato1 = currRows(0).Item(0).ToString.Trim()
                    Response.Redirect("edit_entidad.aspx?id_ent=" & xDato1 & "&tip_ent=" & xDato, True)
                End If
            End If

        Catch ex As Exception
            lbError.Text = Err.Description
            lbError.Visible = True
        Finally
            dtEntidadesPromotora.Dispose()
        End Try

    End Sub
End Class