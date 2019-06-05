Public Class detail1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents dgProveedores As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbMsg As System.Web.UI.WebControls.Label

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

        If (Request.QueryString("myAction") = "search") Then

            Dim dtProveedores As New DataTable
            Try

                Dim tUserInfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)
                Dim cod_proveedor As String = Request.QueryString("cod_prov")
                Dim nom_proveedor As String = Request.QueryString("nom_prov")
                Dim cod_sucursal As String = tUserInfo.codigoSucursal
                Dim cod_filial As String = tUserInfo.codigoFilial
                'Dim anoPeriodo As Integer
                'Dim mesPeriodo As Integer

                dtProveedores = Utiles.buscaProveedor(cod_sucursal, cod_filial, cod_proveedor, nom_proveedor)
                dgProveedores.DataSource = dtProveedores
                dgProveedores.DataBind()

            Catch ex As Exception
                lbMsg.Text = Err.Description
            Finally
                dtProveedores.Dispose()
                Label1.Visible = False
            End Try


        Else
            Label1.Visible = True

        End If
    End Sub

End Class
