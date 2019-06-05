Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Module Utiles

    Public Function obtieneStringDeConexion() As String
        Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader
        obtieneStringDeConexion = CType(configurationAppSettings.GetValue("dbConn.ConnectionString", GetType(System.String)), String)
    End Function


#Region "BUSCA ULTIMA ACTUALIZACIÓN"

    Public Function buscaFechaActualizacion(ByVal xYear As Integer, ByVal xMes As Integer, ByVal xNomTabla As String) As DataTable

        Const spName = "ido_busca_fecha_act"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int, 4).Value = xYear
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int, 4).Value = xMes
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = "CHI"
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = "001"
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@nom_tabla", SqlDbType.Char, 4).Value = xNomTabla
            spCall.Parameters("@nom_tabla").Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region "BUSCA LISTA DE PRECIOS"

    Public Function buscaListaPrecios(ByVal xYear As Integer, ByVal xMes As Integer, ByVal xCodFamilia As String, ByVal xCodSubFamilia As String) As DataSet

        Const spName = "ido_lista_precios"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataSet
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int, 4).Value = xYear
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int, 4).Value = xMes
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = "CHI"
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = "001"
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_familia", SqlDbType.Char, 4).Value = xCodFamilia
            spCall.Parameters("@cod_familia").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_subfamilia", SqlDbType.Char, 300).Value = xCodSubFamilia
            spCall.Parameters("@cod_subfamilia").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@nom_tabla", SqlDbType.Char, 300).Value = "productos"
            spCall.Parameters("@nom_tabla").Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region "BUSCA LISTA DE PRECIOS TOTAL"

    Public Function buscaListaPreciosTotal(ByVal ano_periodo As Integer, ByVal mes_periodo As Integer, ByVal cod_sucursal As String) As DataSet

        Const spName = "ido_lista_precios_total"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataSet
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int, 4).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int, 4).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input



            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region "BUSCA TODAS LAS FAMILIAS"

    Public Function buscaFamilias() As DataTable

        ' si cod_familia no es null trae el nombre de esa familia
        Const spName = "ido_select_familias_ecat"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 4).Value = "CHI"
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 4).Value = "001"
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region "BUSCA LAS SUB - FAMILIAS"

    Public Function buscaSubFamilias(ByVal xTipo As Integer, ByVal xCodFamilia As String) As DataTable

        ' si cod_familia no es null trae el nombre de esa familia
        Const spName = "ido_select_subfamilias"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@tipo", SqlDbType.Int, 4).Value = xTipo
            spCall.Parameters("@tipo").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_familia", SqlDbType.Char, 4).Value = xCodFamilia
            spCall.Parameters("@cod_familia").Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region " OBTIENE NOMBRE DE SUBFAMILIA "
    Public Function get_subfamilia_name(ByVal cod_subfamilia As String) As String

        Dim dbConn As New SqlConnection(Utiles.obtieneStringDeConexion())
        dbConn.Open()

        Dim spName = "select des_subfamilia from Sub_Familia where cod_subfamilia= '" & Trim(cod_subfamilia) & "'"
        Dim daSql As New SqlDataAdapter
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.Text
            daSql.SelectCommand = spCall

            Return daSql.SelectCommand.ExecuteScalar()

        Catch ex As Exception
            Err.Description = Err.Description + " (get_subfamilia_name)"
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
        End Try

    End Function
#End Region

#Region " OBTIENE NOMBRE DE PRODUCTO "
    Public Function get_producto_name(ByVal cod_producto As String) As String

        Dim dbConn As New SqlConnection(Utiles.obtieneStringDeConexion())
        dbConn.Open()

        Dim spName = "select des_producto from Productos where cod_producto= '" & Trim(cod_producto) & "'" & _
                                " AND mes_periodo=" & Date.Now.Month & " AND ano_periodo=" & Date.Now.Year & " AND cod_sucursal='001' "
        Dim daSql As New SqlDataAdapter
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.Text
            daSql.SelectCommand = spCall

            Return daSql.SelectCommand.ExecuteScalar()

        Catch ex As Exception
            Err.Description = Err.Description + " (get_producto_name)"
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
        End Try

    End Function
#End Region

#Region "PROMOTORAS "
    Public Function ObtienePromotoras(ByVal codigoFilial As String, ByVal codigoSucursal As String)
        Const spName = "ido_select_promotoras_x_sucursal"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@codigoFilial", SqlDbType.Char, 3).Value = codigoFilial
            spCall.Parameters("@codigoFilial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@codigoSucursal", SqlDbType.Char, 3).Value = codigoSucursal
            spCall.Parameters("@codigoSucursal").Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try
    End Function

    Public Function get_promotora_name(ByVal cod_filial As String, ByVal cod_sucursal As String, ByVal cod_promotora As String) As String

        Dim dbConn As New SqlConnection(Utiles.obtieneStringDeConexion())
        dbConn.Open()

        Dim spName = "SELECT nom_promotora " & _
                     "FROM PROMOTORAS " & _
                     "WHERE cod_filial='" & cod_filial & "' " & _
                     "AND cod_sucursal='" & cod_sucursal & "' " & _
                     "AND cod_promotora='" & Trim(cod_promotora) & "'"

        Dim daSql As New SqlDataAdapter
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.Text
            daSql.SelectCommand = spCall

            Return daSql.SelectCommand.ExecuteScalar()

        Catch ex As Exception
            Err.Description = Err.Description + " (get_promotora_name)"
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
        End Try

    End Function
#End Region

#Region " OBTIENE NOMBRE DE CLIENTE "
    Public Function get_cliente_name(ByVal cod_cliente As String) As String

        Dim dbConn As New SqlConnection(Utiles.obtieneStringDeConexion())
        dbConn.Open()

        Dim spName = "select rtrim(nom_cliente) from clientes where cod_cliente= '" & Trim(cod_cliente) & "'" & _
                                " AND cod_sucursal='001' and mes_periodo=" & Month(Now) & " AND ano_periodo=" & Year(Now)
        Dim daSql As New SqlDataAdapter
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.Text
            daSql.SelectCommand = spCall

            Return daSql.SelectCommand.ExecuteScalar()

        Catch ex As Exception
            Err.Description = Err.Description + " (get_cliente_name)"
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
        End Try

    End Function
#End Region

#Region "   OBTIENE AREAS   "

    Public Function get_areas(ByVal cod_area As SqlString) As DataTable

        Const spName = "ido_get_areas"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_area", SqlDbType.Char, 3).Value = cod_area
            spCall.Parameters("@cod_area").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value + " (get_cliente_name)"
                Err.Raise(vbObjectError + 528, "ido_get_areas", Err.Description + " (" + spName + ")")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region "OBTIENE TODOS LOS PROVEEDORES"

    Public Function buscaTodosProveedores(ByVal xMes As Integer, ByVal xYear As Integer) As DataTable

        ' si cod_familia no es null trae el nombre de esa familia
        Const spName = "ido_select_proveedores"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@anoPeriodo", SqlDbType.Int, 3).Value = xYear
            spCall.Parameters("@anoPeriodo").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mesPeriodo", SqlDbType.Int, 3).Value = xMes
            spCall.Parameters("@mesPeriodo").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = "001"
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = "CHI"
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region "   OBTIENE FAMILIAS   "

    Public Function get_familias(ByVal cod_area As SqlString, ByVal cod_familia As SqlString) As DataTable

        ' si cod_familia no es null trae el nombre de esa familia
        Const spName = "ido_get_familias"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_area", SqlDbType.Char, 3).Value = cod_area
            spCall.Parameters("@cod_area").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_familia", SqlDbType.Char, 3).Value = cod_familia
            spCall.Parameters("@cod_familia").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value + " (get_familias)"
                Err.Raise(vbObjectError + 528, "ido_get_familias", Err.Description + " (" + spName + ")")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region "   OBTIENE SUBFAMILIAS   "

    Public Function buscaEncargadoSubfamilia(ByVal cod_subfamilia As String) As DataTable

        ' si cod_familia no es null trae el nombre de esa familia
        Const spName = "ido_select_encargado_subfamilia"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@cod_subfamilia", SqlDbType.Char, 10).Value = cod_subfamilia
            spCall.Parameters("@cod_subfamilia").Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

    Public Function get_subfamilias(ByVal cod_familia As SqlString, ByVal cod_subfamilia As SqlString) As DataTable

        ' si cod_subfamilia no es null trae el nombre de esa subfamilia
        Const spName = "ido_get_subfamilias"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_familia", SqlDbType.Char, 3).Value = cod_familia
            spCall.Parameters("@cod_familia").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_subfamilia", SqlDbType.Char, 4).Value = cod_subfamilia
            spCall.Parameters("@cod_subfamilia").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value + " (get_subfamilias)"
                Err.Raise(vbObjectError + 528, "get_subfamilias", Err.Description + " (" + spName + ")")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region " OBTIENE SECTORES PARA MATRIZ "
    Public Function get_matriz_sector() As DataTable

        Dim dbConn As New SqlConnection(Utiles.obtieneStringDeConexion())
        dbConn.Open()

        Dim spName = "select cod_sector from matriz_sector"
        Dim daSql As New SqlDataAdapter
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim resultDT As New DataTable

        Try
            spCall.CommandType = CommandType.Text
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Err.Description = Err.Description + " (get_matriz_sector)"
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region "   BUSQUEDA DE CLIENTES    "


    Public Function buscaClientes(ByVal codigoSucursal As String, _
                                                ByVal codigoCliente As String, _
                                               ByVal razonSocial As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "ido_busqueda_clientes"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure
            If (Month(Today) = 1) Then
                spCall.Parameters.Add("@anoPeriodo", Year(Today) - 1)
                spCall.Parameters.Add("@mesPeriodo", 12)
            Else
                spCall.Parameters.Add("@anoPeriodo", Year(Today))
                spCall.Parameters.Add("@mesPeriodo", Month(Today) - 1)
            End If

            spCall.Parameters.Add("@codigoSucursal", codigoSucursal)

            If (codigoCliente <> "") Then
                spCall.Parameters.Add("@codigoCliente", codigoCliente)
            Else
                spCall.Parameters.Add("@codigoCliente", DBNull.Value)
            End If

            If (razonSocial <> "") Then
                spCall.Parameters.Add("@razonSocial", razonSocial)
            Else
                spCall.Parameters.Add("@razonSocial", DBNull.Value)
            End If

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            daSql.Fill(resultDT)


            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se econtraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 104, "ido_busqueda_proveedores", Err.Description + " (sp)")
            End If

            Return resultDT

            spCall.Dispose()

        Catch ex As Exception
            Throw ex
        Finally
            resultDT.Dispose()
            daSql.Dispose()
            dbConn.Close()
            dbConn.Dispose()
        End Try

    End Function

#End Region

#Region "   RECUPERA CONTRASEÑA   "

    Public Function get_password(ByVal vUsuario As String) As Boolean

        ' si cod_subfamilia no es null trae el nombre de esa subfamilia
        Const spName = "ido_get_password"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim resultSQL As Integer
        Dim msgError As String = ""
        Dim vPassword As String = ""
        Dim vEmail As String = ""

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@usuario", vUsuario).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@password", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output
            spCall.Parameters.Add("@email", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteScalar()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 528, "recupera_clave", Err.Description)
            End If

            vPassword = spCall.Parameters("@password").Value
            vEmail = spCall.Parameters("@email").Value

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
        End Try


        Dim htmlMail
        Dim myMail As New Mail.MailMessage
        Dim mySMTP As Mail.SmtpMail

        Try
            htmlMail = "<b><font face='Verdana' size=3>ANDES: Recupera Contraseña </font></b><br><br>"
            htmlMail = htmlMail & "&nbsp;&nbsp;&nbsp;&nbsp;<font face='Verdana' size=2>Su contraseña para ANDES es: " & vPassword & "</font>"
            myMail.From = "<webmaster@gms.cl>Sistema Otello"
            myMail.To = vEmail
            'mail.To = "rolivares@gms.cl"
            myMail.Subject = "ANDES: Recupera Contraseña"
            myMail.Body = htmlMail
            myMail.BodyFormat = Mail.MailFormat.Html
            mySMTP.SmtpServer = "localhost"
            mySMTP.Send(myMail)
        Catch ex As Exception
            Throw ex
        Finally
            myMail = Nothing
            mySMTP = Nothing
        End Try


        Return True

    End Function

#End Region

#Region "   BUSQUEDA DE PROVEEDORES    "


    Public Function buscaProveedor(ByVal cod_sucursal As String, _
                                                ByVal cod_filial As String, _
                                               ByVal cod_proveedor As String, _
                                               ByVal nom_proveedor As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código o nombre del proveedor
        ' se acepta el wildcard "*"

        Const spName = "ido_busqueda_proveedores"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@anoPeriodo", Year(Today))
            spCall.Parameters.Add("@mesPeriodo", Month(Today))
            spCall.Parameters.Add("@cod_sucursal", cod_sucursal)
            spCall.Parameters.Add("@cod_filial", cod_filial)

            If (cod_proveedor <> "") Then
                spCall.Parameters.Add("@cod_proveedor", cod_proveedor)
            Else
                spCall.Parameters.Add("@cod_proveedor", DBNull.Value)
            End If

            If (nom_proveedor <> "") Then
                spCall.Parameters.Add("@nom_proveedor", nom_proveedor)
            Else
                spCall.Parameters.Add("@nom_proveedor", DBNull.Value)
            End If


            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            daSql.Fill(resultDT)


            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se econtraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 104, "ido_busqueda_proveedores", Err.Description + " (sp)")
            End If

            Return resultDT

            spCall.Dispose()

        Catch ex As Exception
            Throw ex
        Finally
            resultDT.Dispose()
            daSql.Dispose()
            dbConn.Close()
            dbConn.Dispose()
        End Try


    End Function

#End Region

#Region " Add Report Header for Excel Export "
    Public Function AddReportHeader(ByRef Report As Table, ByVal sReportName As String, ByVal sReportDate As String, _
                                            ByVal sTableHeader As String)

        Dim Filler As TableCell
        Dim tRow As TableRow
        Dim colCount As Int16 = 0

        For Each tRow In Report.Rows
            If tRow.Cells.Count > colCount Then colCount = tRow.Cells.Count
        Next

        colCount = colCount - 2
        Filler = New TableCell

        If sTableHeader <> String.Empty Then
            tRow = New TableRow
            Filler = New TableCell
            Filler.ColumnSpan = colCount
            tRow.Cells.Add(Filler)
            Report.Rows.AddAt(0, tRow)

            tRow = New TableRow
            Dim TableHeader As New TableCell
            TableHeader.Text = sTableHeader
            TableHeader.ColumnSpan = colCount
            TableHeader.Font.Bold = True
            tRow.Cells.Add(TableHeader)
            Report.Rows.AddAt(0, tRow)
            TableHeader.Dispose()

        End If



        If sReportDate <> String.Empty Then
            tRow = New TableRow
            Dim ReportDate As New TableCell
            ReportDate.Text = sReportDate
            ReportDate.ColumnSpan = colCount
            tRow.Cells.Add(ReportDate)
            Report.Rows.AddAt(0, tRow)
            ReportDate.Dispose()
        End If


        If sReportName <> String.Empty Then
            tRow = New TableRow
            Dim ReportName As New TableCell
            ReportName.Text = sReportName
            ReportName.ColumnSpan = colCount
            tRow.Cells.Add(ReportName)
            Report.Rows.AddAt(0, tRow)
            ReportName.Dispose()

            tRow = New TableRow
            Filler = New TableCell
            Filler.ColumnSpan = colCount
            tRow.Cells.Add(Filler)
            Report.Rows.AddAt(0, tRow)
        End If

        'tRow = New TableRow
        'Dim CompanyHeader As New TableCell
        'CompanyHeader.Text = COMPANYNAME
        'CompanyHeader.ColumnSpan = colCount
        'CompanyHeader.Font.Bold = True
        'tRow.Cells.Add(CompanyHeader)
        'Report.Rows.AddAt(0, tRow)

        ' CompanyHeader.Dispose()


        Filler.Dispose()


    End Function
#End Region

#Region "TRATAMIENTO TABLA DE ANALISIS VENTA"
    Public Function ObtieneListaMeses(ByVal Mes As Integer, ByVal Año As Integer, ByVal Cantidad As Integer) As String
        Dim xMes As Integer
        Dim xAño As Integer
        Dim xTexto() As String
        Dim Texto As String
        Dim k As Integer
        Dim xMeses() As String

        Texto = "Ene,Feb,Mar,Abr,May,Jun,Jul,Ago,Sep,Oct,Nov,Dic"
        xMeses = Split(Texto, ",", -1, CompareMethod.Text)
        Texto = ""
        xMes = Mes
        xAño = Año
        Texto = ""
        For k = 0 To Cantidad - 1
            Texto = Texto & ", " & (xMeses(xMes - 1)) & "-" & (CStr(xAño)).Chars(2) & (CStr(xAño)).Chars(3)
            xMes = xMes - 1
            If xMes = 0 Then
                xAño = xAño - 1
                xMes = 12
            End If
        Next
        xTexto = Split(Texto, ",", -1, CompareMethod.Text)
        Texto = ""
        For k = Cantidad To 1 Step -1
            If k = Cantidad Then
                Texto = Texto & "" & xTexto(k)
            Else
                Texto = Texto & "|" & xTexto(k)
            End If
        Next
        Return Texto
    End Function

#End Region

#Region " SUCURSALES "
    Public Function obtieneSucursales(ByVal codigoFilial As String) As DataTable
        Const spName = "ido_get_sucursales"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@codigoFilial", SqlDbType.Char, 3).Value = codigoFilial
            spCall.Parameters("@codigoFilial").Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try
    End Function
#End Region

#Region " OBTIENE FILIALES "
    Public Function obtieneFiliales(ByVal cod_usuario As String) As DataTable
        Const spName = "ido_get_filiales"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@cod_usuario", SqlDbType.Char, 3).Value = cod_usuario
            spCall.Parameters("@cod_usuario").Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()



            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try
    End Function
#End Region

#Region "CLIENTES POR PROMOTORA"

    Public Function clientes_x_promotora(ByVal cod_filial As String, _
                                    ByVal cod_sucursal As String, ByVal cod_promotora As String) As DataSet

        Const spName = "ido_cliente_x_promotora"

        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataSet
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_promotora", SqlDbType.Char, 3).Value = cod_promotora
            spCall.Parameters("@cod_promotora").Direction = ParameterDirection.Input



            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region


End Module
