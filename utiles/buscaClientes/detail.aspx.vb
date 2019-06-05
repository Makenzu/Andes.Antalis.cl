Public Class detail
    Inherits System.Web.UI.Page
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lbMsg As System.Web.UI.WebControls.Label
    Protected WithEvents dgClientes As System.Web.UI.WebControls.DataGrid

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If (Request.QueryString("myAction") = "search") Then

            Dim tUserInfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)
            Dim cod_cliente As String = Request.QueryString("cod_cli")
            Dim razonSocial As String = Request.QueryString("razon_soc")
            'codigoAgente = Request.QueryString("cod_agente")
            Try
                Dim dtClientes As New DataTable
                dtClientes = Utiles.buscaClientes(tUserInfo.codigoFilial, cod_cliente, razonSocial)
                dgClientes.DataSource = dtClientes
                dgClientes.DataBind()

            Catch ex As Exception
                lbMsg.Text = Err.Description
            Finally
                dgClientes.Dispose()
                Label1.Visible = False
            End Try

        Else
            Label1.Visible = True

        End If
    End Sub

End Class
