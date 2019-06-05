Imports System.Data.SqlClient
Imports System.Data

Public Class CTipoArtefacto

    Public Shared Function obtieneOcurrencias() As DataTable
        Const spName = "cma_sel_artefactos_tipos"
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

            daSql.SelectCommand = spCall

            resultDT = New DataTable
            daSql.Fill(resultDT)
            Return resultDT


        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try
    End Function

    Public Shared Function obtienePropiedades(ByVal tipoArtefacto As String) As DataTable
        Const spName = "cma_sel_artefacto_propiedades"
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
            spCall.Parameters.Add("@tipo_artefacto", tipoArtefacto).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

            resultDT = New DataTable
            daSql.Fill(resultDT)
            Return resultDT


        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try
    End Function

End Class
