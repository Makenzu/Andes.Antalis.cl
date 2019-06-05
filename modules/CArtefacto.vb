Imports System.Data.SqlClient
Imports System.Data

Public Class CArtefacto

    Public Shared Function ingresa(ByVal tipoArtefacto As String, ByVal dmcArtefacto As String, ByVal usrResponsable As String) As Integer
        Const spName = "cma_ing_artefacto"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id_artefacto", SqlDbType.Int).Direction = ParameterDirection.Output
            spCall.Parameters.Add("@dmc_artefacto", dmcArtefacto).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@tipo_artefacto", tipoArtefacto).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@usr_responsable", usrResponsable).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sist_imp", usrResponsable).Direction = ParameterDirection.Input
            spCall.ExecuteNonQuery()

            Return spCall.Parameters("@id_artefacto").Value

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function

    Public Shared Function actualiza(ByVal idArtefacto As Integer, ByVal tipoArtefacto As String, ByVal dmcArtefacto As String, ByVal usrResponsable As String) As Boolean
        Const spName = "cma_act_artefacto"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id_artefacto", idArtefacto).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@dmc_artefacto", dmcArtefacto).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@tipo_artefacto", tipoArtefacto).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@usr_responsable", usrResponsable).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function





    Public Shared Function obtieneOcurrenciasPorTipo(ByVal tipoArtefacto As String, ByVal patron As String) As DataTable
        Const spName = "cma_sel_artefactos_x_tipo"
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
            spCall.Parameters.Add("@patron", patron).Direction = ParameterDirection.Input

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

    Public Shared Function obtieneId(ByVal tipoArtefacto As String, ByVal dmcArtefacto As String) As Integer
        Const spName = "cma_sel_artefacto_x_dmc"
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
            spCall.Parameters.Add("@dmc_artefacto", dmcArtefacto).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@id_artefacto", SqlDbType.Int).Direction = ParameterDirection.Output

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            Return spCall.Parameters("@id_artefacto").Value

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

    Public Shared Function existeValorPropiedad(ByVal propiedad As String, ByVal valor As String) As Boolean
        Const spName = "cma_existe_valor_propiedad"
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
            spCall.Parameters.Add("@propiedad", propiedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@valor", valor).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@existe", SqlDbType.Int).Direction = ParameterDirection.Output

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            Return spCall.Parameters("@existe").Value = 1

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

    Public Shared Function ingresaValorPropiedad(ByVal propiedad As String, _
                                                    ByVal valor As String, _
                                                    ByVal usrResponsable As String) As Boolean
        Const spName = "cma_ing_valor_propiedad"
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
            spCall.Parameters.Add("@propiedad", propiedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@valor", valor).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@usr_responsable", usrResponsable).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            Return spCall.Parameters("@return").Value = 1

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
