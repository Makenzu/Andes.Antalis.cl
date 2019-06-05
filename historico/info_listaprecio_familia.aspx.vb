Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class info_listaprecio_familia
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents lbData As System.Web.UI.WebControls.Label
    Protected WithEvents plDatos As System.Web.UI.WebControls.Panel
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents tbResultado As System.Web.UI.WebControls.Table
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents txFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents cblSubFamilias As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents ddlFamilias As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rfvSubFamilia As System.Web.UI.WebControls.Label

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

        lbErrors.Visible = False
        If txFecha.Text = "" Then
            Dim en As New System.Globalization.CultureInfo("en-US")
            txFecha.Text = Date.Now.ToString("dd / MM / yyyy", en)
        End If


        If Page.IsPostBack = False Then

            Dim cod_familia, cod_area As SqlString
            Dim tbFamilias As DataTable

            Try

                tbFamilias = Utiles.get_familias(cod_area, cod_familia)
                ddlFamilias.DataSource = tbFamilias
                ddlFamilias.DataTextField = "des_familia"
                ddlFamilias.DataValueField = "cod_familia"
                ddlFamilias.DataBind()

                ddlFamilias.Items.Insert(0, "--Elija una Familia --")

            Catch ex As Exception
                lbErrors.Text = Err.Description
                Err.Clear()
            Finally

            End Try

        Else

            If validate_chkboxlist(cblSubFamilias) = False Then
                rfvSubFamilia.Visible = True

            Else

                Try

                Catch ex As Exception

                End Try

            End If


        End If
        
    End Sub

    Private Sub ddlFamilias_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFamilias.SelectedIndexChanged

        Dim cod_subfamilia As SqlString
        Dim cod_familia As New SqlString(ddlFamilias.SelectedValue.ToString)
        Dim tbSubfamilias As DataTable

        Try

            tbSubfamilias = Utiles.get_subfamilias(cod_familia, cod_subfamilia)
            cblSubFamilias.DataSource = tbSubfamilias
            cblSubFamilias.DataTextField = "des_subfamilia"
            cblSubFamilias.DataValueField = "cod_subfamilia"
            cblSubFamilias.Style.Add("WHITE-SPACE", "nowrap")
            cblSubFamilias.DataBind()

        Catch ex As Exception
            lbErrors.Text = Err.Description
            Err.Clear()
        Finally

        End Try

    End Sub

    Function validate_chkboxlist(ByVal chkList As CheckBoxList) As Boolean
        Dim rtn As Boolean = False
        Dim i As Int16
        For i = 0 To (chkList.Items.Count - 1)
            If (chkList.Items(i).Selected) Then
                rtn = True
                Exit Function
            End If
        Next
        Return rtn
    End Function
End Class
