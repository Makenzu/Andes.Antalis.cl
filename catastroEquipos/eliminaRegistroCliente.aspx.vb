Imports System.Data.SqlClient
Imports System.Data

Public Class eliminaRegistroCliente
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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

        If IsNumeric(Request("idRegistroCliente")) Then

            Dim idRegistroCliente As String = Request("idRegistroCliente")
            Dim codSociedad As String = Request("codSociedad")
            Dim usrResponsable As String = Request("usrResponsable")

            Const spName = "cma_eli_artefacto_cliente"
            Dim dbConn As New SqlConnection
            Dim spCall As New SqlCommand(spName, dbConn)
            Dim daSql As New SqlDataAdapter
            Dim resultDT As New DataTable

            Try
                dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
                dbConn.Open()

                Dim msgError As String = ""
                Dim resultSQL As Integer

                spCall.CommandType = CommandType.StoredProcedure
                spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                spCall.Parameters.Add("@id_registro_cliente", idRegistroCliente).Direction = ParameterDirection.Input
                spCall.Parameters.Add("@usr_responsable", usrResponsable).Direction = ParameterDirection.Input

                daSql.SelectCommand = spCall
                daSql.SelectCommand.ExecuteNonQuery()

                resultSQL = spCall.Parameters("@return").Value

            Catch ex As Exception
                Throw ex
            Finally
                dbConn.Close()
                dbConn = Nothing
                spCall = Nothing
                daSql = Nothing
                resultDT = Nothing
            End Try
        End If 'Put user code to initialize the page here
    End Sub

End Class
