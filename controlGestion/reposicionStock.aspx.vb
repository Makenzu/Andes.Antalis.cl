Public Class reposicionStock
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton
    Protected WithEvents dgReposicion As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAnno As System.Web.UI.WebControls.DropDownList

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

        If Not Page.IsPostBack Then

            Dim annoActual As Integer
            annoActual = Year(Now)

            ' Año listbox
            Dim i As Integer
            For i = 0 To 1
                Dim newListItem As New ListItem
                newListItem.Text = DateAdd(DateInterval.Year, -i, Date.Today.Date).ToString("yyyy")
                newListItem.Value = DateAdd(DateInterval.Year, -i, Date.Today.Date).ToString("yyyy")
                ddlAnno.Items.Add(newListItem)
                If newListItem.Value = annoActual Then
                    ddlAnno.SelectedIndex = i
                End If

            Next

            Dim mesActual As Integer
            mesActual = Month(Now)

            ' Months ComboBox
            For i = 0 To 12
                Dim newListItem As New ListItem
                newListItem.Text = DateAdd(DateInterval.Month, -i, Date.Today.Date).ToString("MMM")
                newListItem.Value = DateAdd(DateInterval.Month, -i, Date.Today.Date).ToString("MM")
                ddlMes.Items.Add(newListItem)
                If newListItem.Value = mesActual Then
                    ddlMes.SelectedIndex = i
                End If

            Next
        End If

    End Sub

    Private Sub btSend_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSend.Click

        Dim ano_periodo As Integer
        Dim mes_periodo As Integer
        Dim resultTbl As DataTable

        ano_periodo = Request("ddlAnno")
        mes_periodo = Request("ddlMes")

        resultTbl = ReposicionStockBolivia(ano_periodo, mes_periodo)
        lbErrors.Visible = False
        Try

            If resultTbl.Rows.Count <= 0 Then

                dgReposicion.DataSource = Nothing
                dgReposicion.Visible = False
                Err.Description = "No se encontraron datos para esta consulta."
                Err.Raise(vbObjectError + 512 + 10, "ReposicionStockBolivia", Err.Description)

            Else
                dgReposicion.Visible = True
                dgReposicion.Visible = True
                dgReposicion.DataSource = resultTbl

            End If
            dgReposicion.DataBind()

        Catch ex As Exception
            lbErrors.Text = Err.Description
            lbErrors.Visible = True
            Err.Clear()

        Finally
            resultTbl.Dispose()
        End Try

    End Sub
End Class
