Imports System.Web.Security

Public Class login
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents tbUser As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbPass As System.Web.UI.WebControls.TextBox
    Protected WithEvents ibEntrar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lError As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage

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


        If Not Page.IsPostBack Then

            Session.Clear()
            Session.Abandon()

            If tbUser.Text.Trim = "" Then
                tbUser.Text = Response.Cookies("USERNAME").Value
            End If

            If Request("action") = "login" Then
                lError.Text = "Su sesión ha caducado. Favor reingrese sus credenciales. Si el problema persiste contacte a Mesa de Ayuda."
            End If

        End If


    End Sub

    Private Sub ibEntrar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibEntrar.Click

        Try
            Dim usuario As String = tbUser.Text.Trim
            Dim password As String = tbPass.Text.Trim

            If usuario <> "" And password <> "" Then
                Dim tUserInfo As t_Usuario

                tUserInfo = loginUsuario(usuario, password)

                Response.Cookies("USERNAME").Value = usuario
                Response.Cookies("USERNAME").Expires.AddYears(1)

                'Obtenemos info de tipo(s) de usuario(s) y célula
                Dim dtDatos As DataTable = obtieneEstructuraComercialUsuario(usuario)
                If dtDatos.Rows.Count > 0 Then
                    Session(Constantes.CTE_ANDES_CODIGO_AGENTE) = Trim(dtDatos.Rows(0).Item("cod_agente"))
                    Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = Trim(dtDatos.Rows(0).Item("cod_tipo_agente"))
                    Session(Constantes.CTE_ANDES_CODIGO_CELULA) = Trim(dtDatos.Rows(0).Item("cod_celula"))
                    Session(Constantes.CTE_ANDES_SUPER_AGENTE) = Trim(dtDatos.Rows(0).Item("super_agente")).ToUpper()
                    Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = Trim(dtDatos.Rows(0).Item("cod_grupo")).ToUpper()
                Else
                    Session(Constantes.CTE_ANDES_CODIGO_AGENTE) = ""
                    Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = ""
                    Session(Constantes.CTE_ANDES_CODIGO_CELULA) = ""
                    Session(Constantes.CTE_ANDES_SUPER_AGENTE) = ""
                    Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = ""
                End If

                'Login OK
                Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) = "SI"
                Session(Constantes.CTE_ANDES_INFO_USUARIO) = tUserInfo
                Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) = tUserInfo.perfil
                Session(Constantes.CTE_ANDES_USERNAME) = usuario
                Session(Constantes.CTE_ANDES_FILIAL_ACTIVA) = tUserInfo.codigoFilial

                Dim pu As CPermisoUsuario = New CPermisoUsuario("GMSC", usuario)
                Session(Constantes.CTE_OBJ_PERMISOS_USUARIO) = pu

                FormsAuthentication.RedirectFromLoginPage(usuario, False)
                Response.Redirect("/default.aspx")
                Return

            End If


        Catch ex As Exception
            lError.Visible = True
            lError.Text = ex.Message
        End Try

    End Sub

    
End Class
