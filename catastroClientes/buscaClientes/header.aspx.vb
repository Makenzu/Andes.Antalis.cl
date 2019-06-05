Public Class header
    Inherits System.Web.UI.Page
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Protected WithEvents rblTipoBusqueda As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents tbPatron As System.Web.UI.WebControls.TextBox
    Protected WithEvents chAlcanceBusqueda As System.Web.UI.WebControls.CheckBox

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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Dim codigoAgente As String

        If chAlcanceBusqueda.Checked Then
            codigoAgente = ""
        Else
            codigoAgente = Request.QueryString("cod_agente")
        End If

        If rblTipoBusqueda.SelectedIndex = 0 Then
            Response.Write("<SCRIPT LANGUAGE='JavaScript'>" + vbCrLf)
            Response.Write("<!--" + vbCrLf)
            Response.Write("parent.frames[1].document.location='detail.aspx?myAction=search&source=" + Request.QueryString("source") + "&cod_cli=" + tbPatron.Text.Trim + "&razon_soc=&cod_sucursal=" & Request.QueryString("cod_sucursal") & "&cod_agente=" & codigoAgente & "';" + vbCrLf)
            Response.Write("//-->" + vbCrLf)
            Response.Write("</SCRIPT>" + vbCrLf)
        Else
            Response.Write("<SCRIPT LANGUAGE='JavaScript'>" + vbCrLf)
            Response.Write("<!--" + vbCrLf)
            Response.Write("parent.frames[1].document.location='detail.aspx?myAction=search&source=" & Request.QueryString("source") & "&cod_cli=&razon_soc=" & tbPatron.Text.Trim & "&cod_sucursal=" & Request.QueryString("cod_sucursal") & "&cod_agente=" & codigoAgente & "'" + vbCrLf)
            Response.Write("//-->" + vbCrLf)
            Response.Write("</SCRIPT>" + vbCrLf)
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
