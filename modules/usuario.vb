Imports System.Data
Imports System.Data.SqlClient


Module usuario

    Const WidthError = 1
    Const WidthHelp = 101



    Public Structure t_Usuario
        Dim nombre As String
        Dim email As String
        Dim codigo As Integer
        Dim perfil As String
        Dim codigoPromo As String
        Dim codigoFilial As String
        Dim codigoSucursal As String
        Dim superUsuario As String
    End Structure



    Public Function loginUsuario(ByVal username As String, ByVal password As String) As t_Usuario

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()

        Const spName = "ido_autentica_usuario_x_sucursal"
        Dim spCall As New SqlCommand(spName, dbConn)

        spCall.CommandType = CommandType.StoredProcedure

        'resultado de la llamada al sp
        spCall.Parameters.Add("@resultValue", SqlDbType.Int)
        spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

        spCall.Parameters.Add("@username", username).Direction = ParameterDirection.Input
        spCall.Parameters.Add("@password", password).Direction = ParameterDirection.Input
        spCall.Parameters.Add("@cod_usuario", SqlDbType.Int).Direction = ParameterDirection.Output
        spCall.Parameters.Add("@nom_usuario", SqlDbType.Char, 30).Direction = ParameterDirection.Output
        spCall.Parameters.Add("@email_usuario", SqlDbType.Char, 30).Direction = ParameterDirection.Output
        spCall.Parameters.Add("@cod_perfil", SqlDbType.Char, 4).Direction = ParameterDirection.Output
        spCall.Parameters.Add("@cod_promotora", SqlDbType.Char, 3).Direction = ParameterDirection.Output
        spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Direction = ParameterDirection.Output
        spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Direction = ParameterDirection.Output
        spCall.Parameters.Add("@super_usuario", SqlDbType.Char, 1).Direction = ParameterDirection.Output
        spCall.Parameters.Add("@msgError", SqlDbType.Char, 80).Direction = ParameterDirection.Output


        dbConn.Open()

        Dim myDataAdapter As New SqlDataAdapter
        myDataAdapter.SelectCommand = spCall
        myDataAdapter.SelectCommand.ExecuteNonQuery()


        Dim iResult As Integer
        Dim msgError As String

        iResult = spCall.Parameters("@resultValue").Value

        If (iResult = 0) Then
            With spCall
                loginUsuario.nombre = Trim(.Parameters("@nom_usuario").Value)
                loginUsuario.email = Trim(.Parameters("@email_usuario").Value)
                loginUsuario.codigo = .Parameters("@cod_usuario").Value
                loginUsuario.perfil = Trim(.Parameters("@cod_perfil").Value)
                loginUsuario.codigoPromo = Trim(.Parameters("@cod_promotora").Value)
                loginUsuario.codigoSucursal = Trim(.Parameters("@cod_sucursal").Value)
                loginUsuario.codigoFilial = Trim(.Parameters("@cod_filial").Value)
                loginUsuario.superUsuario = Trim(.Parameters("@super_usuario").Value)
            End With
        Else
            msgError = spCall.Parameters("@msgError").Value
        End If

        dbConn.Close()
        dbConn = Nothing
        spCall = Nothing


        If (iResult <> 0) Then
            Err.Raise(vbObjectError + 512 + WidthError, "loginUsuario()", msgError)
        End If

    End Function


    Public Function obtieneEstructuraComercialUsuario(ByVal username As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()


        Const spName = "aws_sel_estr_com_usuario"
        Dim spCall As New SqlCommand(spName, dbConn)

        spCall.CommandType = CommandType.StoredProcedure

        'resultado de la llamada al sp
        spCall.Parameters.Add("@resultValue", SqlDbType.Int)
        spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

        spCall.Parameters.Add("@username", username).Direction = ParameterDirection.Input

        Try
            dbConn.Open()

            Dim myDataAdapter As New SqlDataAdapter
            myDataAdapter.SelectCommand = spCall
            myDataAdapter.SelectCommand.ExecuteNonQuery()

            Dim dtResult As DataTable = New DataTable
            myDataAdapter.Fill(dtResult)

            Return dtResult

        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try

    End Function

End Module
