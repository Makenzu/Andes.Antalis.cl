Imports System.Data.SqlClient
Imports System.Data

Public Class actIndicadoresCliente
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
        Dim codigoCliente As String = Request("codCliente")
        Dim area As String = Request("area")
        Dim valor As Double = CType(Request("valor"), Double)
        Dim seccion As String = Request("seccion")
        Dim codigoSociedad As String = Request("codSociedad")
        Dim usuarioSesion As t_Usuario = Session(Constantes.CTE_OBJ_USER_INFO)

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "pot_act_indicadores_vta_cliente"

        Dim daSql As SqlDataAdapter = New SqlDataAdapter
        Dim dtResult As DataTable = New DataTable
        Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codigoCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@area", area).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@valor", valor).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@seccion", seccion).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@usr_responsable", usuarioSesion.codigo).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            If spCall.Parameters("@return").Value() = 1 Then
                Response.Write("resultado_ok")
            Else
                Response.Write("resultado_nok")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try

    End Sub

End Class
