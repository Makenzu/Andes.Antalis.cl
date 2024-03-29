Imports System.Data.SqlClient

Public Class acMateriales
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

        Dim q As String = Request("q")

        If (Not IsNothing(q)) And (q.Length > 0) Then
            Const spName = "ido_sel_materiales_x_patron"
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
                spCall.Parameters.Add("@cod_sociedad", "GMSC").Direction = ParameterDirection.Input
                spCall.Parameters.Add("@query", q).Direction = ParameterDirection.Input
                spCall.Parameters.Add("@busqueda_exacta", False).Direction = ParameterDirection.Input

                daSql.SelectCommand = spCall

                daSql.Fill(resultDT)

                For Each dr As DataRow In resultDT.Rows
                    Response.Write(String.Format("{0} :: {1}{2}", Trim(dr("cod_producto")), Trim(dr("des_producto")), vbCrLf))
                Next

            Catch ex As Exception
                Throw ex
            Finally
                dbConn.Close()
                dbConn = Nothing
                spCall = Nothing
                daSql = Nothing
                resultDT = Nothing
            End Try
        End If

    End Sub

End Class
