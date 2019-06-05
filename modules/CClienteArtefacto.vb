Imports System.Data.SqlClient
Imports System.Data

Public Class CClienteArtefacto
    Public Shared Function ingresaRegistroCliente(ByVal codigoSociedad As String, _
                                                    ByVal codigoCliente As String, _
                                                    ByVal idArtefacto As Integer, _
                                                    ByVal usrResponsable As String) As Integer
        Const spName = "cma_ing_artefacto_cliente"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codigoCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@id_artefacto", idArtefacto).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@id_registro_cliente", SqlDbType.Int).Direction = ParameterDirection.Output
            spCall.Parameters.Add("@usr_responsable", usrResponsable).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()

            Return CType(spCall.Parameters("@id_registro_cliente").Value, Integer)

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function

    Public Shared Function actualizaRegistroCliente(ByVal idRegistroCliente As Integer, _
                                                        ByVal idArtefacto As Integer, _
                                                        ByVal usrResponsable As String) As Boolean
        Const spName = "cma_act_artefacto_cliente"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id_registro_cliente", idRegistroCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@id_artefacto", idArtefacto).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@usr_responsable", usrResponsable).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()

            Return CType(spCall.Parameters("@return").Value, Integer) = 1

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function


    Public Shared Function ingresaValorPropiedadRegistroCliente(ByVal idRegistroCliente As Integer, _
                                                ByVal propiedad As String, _
                                                ByVal valor As String, _
                                                ByVal usrResponsable As String) As Boolean

        Const spName = "cma_ing_artefacto_cliente_valor_propiedad"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id_registro_cliente", idRegistroCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@propiedad", propiedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@valor", valor).Direction = ParameterDirection.Input
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

    Public Shared Function actualizaValorPropiedadRegistroCliente(ByVal idRegistroCliente As Integer, _
                                                ByVal propiedad As String, _
                                                ByVal valor As String, _
                                                ByVal usrResponsable As String) As Boolean

        Const spName = "cma_act_artefacto_cliente_valor_propiedad"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id_registro_cliente", idRegistroCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@propiedad", propiedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@valor", valor).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@usr_responsable", valor).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()

            Return (spCall.Parameters("@return").Value = 1)

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function

    Public Shared Function obtieneValorRegistroCliente(ByVal idRegistroCliente As Integer, ByVal propiedad As String) As DataTable
        Const spName = "cma_sel_artefacto_cliente_valor_propiedad"
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
            spCall.Parameters.Add("@propiedad", propiedad).Direction = ParameterDirection.Input

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


    Public Shared Function obtieneRegistroCliente(ByVal idRegistroCliente As Integer) As DataTable
        Const spName = "cma_sel_artefacto_cliente"
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

    Public Shared Function eliminaValoresPropiedadRegistroCliente(ByVal idRegistroCliente As Integer, _
                                                ByVal propiedad As String, _
                                                ByVal usrResponsable As String) As Boolean

        Const spName = "cma_eli_artefacto_cliente_valor_propiedad"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id_registro_cliente", idRegistroCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@propiedad", propiedad).Direction = ParameterDirection.Input
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

    Public Shared Function obtieneValoresRegistro(ByVal idRegistroCliente As Integer) As DataTable
        Const spName = "cma_sel_artefacto_cliente_valores_propiedades"
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
