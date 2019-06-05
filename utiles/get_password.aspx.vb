Public Class get_password
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents lbTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents txUsuario As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvCliente As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ibtAceptar As System.Web.UI.WebControls.ImageButton

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

        Dim en As New System.Globalization.CultureInfo("en-US")
        lbFecha.Text = Date.Now.ToString("dd / MM / yyyy")

    End Sub

    Private Sub ibtAceptar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtAceptar.Click

        Dim vUsername = Trim(txUsuario.Text)

        Try

            Utiles.get_password(vUsername)

        Catch ex As Exception
            lbErrors.Text = "ERROR: " & Err.Description
            lbErrors.Visible = True
            Err.Clear()
        End Try




    End Sub
End Class
