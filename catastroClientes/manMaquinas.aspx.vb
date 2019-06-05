Imports System.Data
Imports System.Data.SqlClient

Public Class manMaquinas
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents bIngresaNuevaMaquina As System.Web.UI.WebControls.Button
    Protected WithEvents pDatosCatastro As System.Web.UI.WebControls.Panel
    Protected WithEvents dgMaquinas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lMensaje As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim msgError As String

    Const Cnt_JavaScriptWinOpen As String = "window.open('editaMaquina.aspx?accion={0}&idMaquina={1}', 'maq', 'menubar=0,resizable=0,width=407,height=208'); return false;"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Dim tUserInfo As t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        Panel1.Visible = (tUserInfo.codigoPromo = "16")

        If Not Page.IsPostBack Then
            bIngresaNuevaMaquina.Attributes.Add("onClick", String.Format(Cnt_JavaScriptWinOpen, "ingresa", ""))
            pDatosCatastro.Visible = True
            despliegaMaquinas()

            If Session("MENSAJE_USUARIO") <> "" Then
                Label1.Text = Session("MENSAJE_USUARIO")
                Label1.CssClass = "mensajeAccion"
                Session("MENSAJE_USUARIO") = ""
            Else
                Label1.Text = ""
                Label1.CssClass = ""
            End If

        End If
    End Sub


    Private Sub despliegaMaquinas()

        Dim cuentaMaquinas As Integer
        Dim dtMaquinas As DataTable = Me.obtieneMaquinas()

        With dgMaquinas
            .DataSource = dtMaquinas
            .DataBind()
        End With
        cuentaMaquinas = dtMaquinas.Rows.Count

        lMensaje.Text = String.Format("Total {0} máquinas encontradas.", cuentaMaquinas)

    End Sub


    Private Sub dgMaquinas_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgMaquinas.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            'Agregamos javascript para abrir ventana emergente 
            Dim myLinkButton As LinkButton = e.Item.Cells(4).Controls(0)
            Dim idMaquina As Integer = e.Item.Cells(0).Text
            myLinkButton.Attributes.Add("onClick", String.Format(Cnt_JavaScriptWinOpen, "edita", idMaquina))

            'Agregamos javascript para confirmar operación de borrado
            myLinkButton = e.Item.Cells(5).Controls(0)

            myLinkButton.Attributes.Add("onClick", "return confirm('¿Está seguro que desea eliminar este elemento?');")

        End If
    End Sub


    Private Sub dgMaquinas_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMaquinas.DeleteCommand
        Dim idRegistro As Integer = e.Item.Cells(0).Text
        If Me.eliminaMaquina(idRegistro) Then
            Session("MENSAJE_USUARIO") = "Se ha eliminado la máquina."
            Response.Redirect("manMaquinas.aspx")
        Else
            Response.Write("No se pudo completar la operación")
        End If
    End Sub


    Public Function obtieneMaquinas() As DataTable
        Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

        Dim spCall As SqlCommand = New SqlCommand("cma_sel_maquinas", dbConn)

        Try
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            Dim daSql As SqlDataAdapter = New SqlDataAdapter
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            If (spCall.Parameters("@resultValue").Value >= 0) Then
                Dim resultDT As DataTable = New DataTable
                daSql.Fill(resultDT)
                Return resultDT
            End If

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function

    Public Function eliminaMaquina(ByVal idMaquina As Integer) As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

        Dim spCall As SqlCommand = New SqlCommand("cma_eli_maquina", dbConn)

        Try
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id", idMaquina).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()

            Return (spCall.Parameters("@resultValue").Value >= 0)
        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function
End Class
