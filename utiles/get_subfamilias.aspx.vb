Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class get_subfamilias
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlAreas As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFamilias As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSubfamilia As System.Web.UI.WebControls.DropDownList

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


        If Not (Page.IsPostBack) Then

            Dim cod_area As SqlString
            lbErrors.Text = ""

            Dim tbAreas As DataTable

            Try
                tbAreas = Utiles.get_areas(cod_area)
                ddlAreas.DataSource = tbAreas
                ddlAreas.DataTextField = "des_area"
                ddlAreas.DataValueField = "cod_area"
                ddlAreas.DataBind()

                ddlAreas.Items.Insert(0, "--Elija una Area --")
            Catch ex As Exception
                lbErrors.Text = Err.Description
                Err.Clear()

            Finally


            End Try
            ddlFamilias.Items.Insert(0, "--Elija una Familia --")
        End If

        ddlSubfamilia.Items.Insert(0, "--Elija una Subfamilia --")

    End Sub

    Private Sub ddlAreas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlAreas.SelectedIndexChanged

        Dim cod_familia As SqlString
        Dim cod_area As New SqlString(ddlAreas.SelectedValue.ToString)
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

    End Sub

    Private Sub ddlFamilias_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFamilias.SelectedIndexChanged

        Dim cod_subfamilia As SqlString
        Dim cod_familia As New SqlString(ddlFamilias.SelectedValue.ToString)
        Dim tbSubfamilias As DataTable

        Try

            tbSubfamilias = Utiles.get_subfamilias(cod_familia, cod_subfamilia)
            ddlSubfamilia.DataSource = tbSubfamilias
            ddlSubfamilia.DataTextField = "des_subfamilia"
            ddlSubfamilia.DataValueField = "cod_subfamilia"
            ddlSubfamilia.DataBind()

            ddlSubfamilia.Items.Insert(0, "--Elija una Subfamilia --")

        Catch ex As Exception
            lbErrors.Text = Err.Description
            Err.Clear()
        Finally

        End Try

    End Sub
End Class
