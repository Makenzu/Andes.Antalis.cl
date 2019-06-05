Public Class header1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents tbCodProv As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbNomProv As System.Web.UI.WebControls.TextBox

    Dim tUserInfo As usuario.t_Usuario

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        If IsNothing(Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO)) = True Then
            Response.Redirect("logout.aspx")
            Response.End()
        Else

            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Write("<SCRIPT LANGUAGE='JavaScript'>" + vbCrLf)
        Response.Write("<!--" + vbCrLf)
        Response.Write("parent.frames[1].document.location='detail.aspx?myAction=search&source=" + Request.QueryString("source") + "&cod_prov=" + tbCodProv.Text.Trim + "&nom_prov=';" + vbCrLf)
        Response.Write("//-->" + vbCrLf)
        Response.Write("</SCRIPT>" + vbCrLf)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Write("<SCRIPT LANGUAGE='JavaScript'>" + vbCrLf)
        Response.Write("<!--" + vbCrLf)
        Response.Write("parent.frames[1].document.location='detail.aspx?myAction=search&cod_prov=&nom_prov=" + tbNomProv.Text.Trim + "';" + vbCrLf)
        Response.Write("//-->" + vbCrLf)
        Response.Write("</SCRIPT>" + vbCrLf)
    End Sub
End Class
