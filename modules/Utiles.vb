Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Module Utiles

    Public Function obtieneStringDeConexion() As String
        Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader
        obtieneStringDeConexion = CType(configurationAppSettings.GetValue("dbConn.ConnectionString", GetType(System.String)), String)
    End Function

    Public Structure stSegmentos
        Dim cod_cliente As String
        Dim cod_segmento As String
        Dim porcentajes As String

    End Structure

    Public Structure stMaquinas
        Dim anoPeriodo As String
        Dim mesPeriodo As String
        Dim codFilial As String
        Dim codCliente As String
        Dim codMaquina As String
        Dim modeloMaquina As String
        Dim codMarca As String
        Dim tamanoPlancha As String
        Dim nCuerpos As String
        Dim tBarniz As String
        Dim TipoPlancha As String
        Dim consumoMensual As String
        Dim tBarra As String
    End Structure

    Public Structure stMantilla
        Dim codFilial As String
        Dim codSucursal As String
        Dim codCliente As String
        Dim codMantilla As String
        Dim consumo As String
    End Structure

    Public Structure stTinta
        Dim anoPeriodo As String
        Dim mesPeriodo As String
        Dim codFilial As String
        Dim codSucursal As String
        Dim codCliente As String
        Dim codTinta As String
        Dim consumo As String
    End Structure

    Public Structure stMarcaPapel
        Dim anoPeriodo As String
        Dim mesPeriodo As String
        Dim codFilial As String
        Dim codSucursal As String
        Dim codCliente As String
        Dim codMarcaPapel As String
        Dim consumo As String
    End Structure

    Public Structure stventas
        Dim anoPeriodo As String
        Dim mesPeriodo As String
        Dim codFilial As String
        Dim codSucursal As String
        Dim codCliente As String
        Dim vta_mensual As String
        Dim tipoAnoAnt As String
        Dim vta_ano_anterior As String
        Dim tprepre As String
        Dim tipo_pre As String
    End Structure


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

    Public Function buscaListaPrecios(ByVal xYear As Integer, ByVal xMes As Integer, ByVal codfilial As String, ByVal codSucursal As String, ByVal xCodFamilia As String, ByVal xCodSubFamilia As String) As DataSet

        Const spName = "ido_lista_precios_AGS"

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
            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = codfilial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = codSucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_familia", SqlDbType.Char, 4).Value = xCodFamilia
            spCall.Parameters("@cod_familia").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_subfamilia", SqlDbType.Char, 300).Value = xCodSubFamilia
            spCall.Parameters("@cod_subfamilia").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@nom_tabla", SqlDbType.Char, 300).Value = "productos"
            spCall.Parameters("@nom_tabla").Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall

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

    Public Function buscaListaPreciosTotal(ByVal ano_periodo As Integer, ByVal mes_periodo As Integer, ByVal cod_filial As String, ByVal cod_sucursal As String) As DataSet
        Const spName = "ido_lista_precios_total_AGS"

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

            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

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

    Public Function buscaFamilias(ByVal codigoFilial As String) As DataTable

        ' si cod_familia no es null trae el nombre de esa familia
        Const spName = "ido_sel_familias"

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
            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall

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

    Enum eModo
        extendido
        estandar
    End Enum

    Enum eModoLista
        Normal = 0
        Dummy = 1
        Todos = 2
    End Enum

    Public Function buscaSubFamilias(ByVal modo As eModo, ByVal codigoFilial As String, ByVal codigoFamilia As String) As DataTable
        ' si cod_familia no es null trae el nombre de esa familia
        Const spName = "ido_sel_subfamilias"

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
            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_familia", codigoFamilia).Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall

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
    Public Function ObtienePromotoras(ByVal codigoFilial As String)
        Const spName = "ido_sel_promotoras"

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

            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

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

    Public Function obtieneEjecutivosComerciales(ByVal codigoFilial As String)
        Const spName As String = "aws_sel_ejec_com"

        Dim dbConn As SqlConnection = New SqlConnection
        Dim daSql As SqlDataAdapter = New SqlDataAdapter
        Dim resultDT As DataTable = New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

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

    Public Function obtieneSalesAdvisors(ByVal codigoFilial As String)
        Const spName As String = "aws_sel_sales_advisor"

        Dim dbConn As SqlConnection = New SqlConnection
        Dim daSql As SqlDataAdapter = New SqlDataAdapter
        Dim resultDT As DataTable = New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

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

            daSql.Fill(resultDT)
            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value + " (get_cliente_name)"
                Err.Raise(vbObjectError + 528, "ido_get_areas", Err.Description + " (" + spName + ")")
            End If



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

    Public Function buscaTodosProveedores(ByVal codigoFilial As String) As DataTable

        ' si cod_familia no es null trae el nombre de esa familia
        Const spName = "ido_sel_proveedores"

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
            spCall.Parameters.Add("@cod_filial", codigoFilial).Value = codigoFilial

            daSql.SelectCommand = spCall

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

#Region "   obtieneFamilias   "

    Public Function obtieneFamilias(ByVal cod_area As SqlString, ByVal cod_familia As SqlString) As DataTable

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
            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_area", cod_area).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_familia", cod_familia).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output

            daSql.SelectCommand = spCall
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value + " (get_familias)"
                Err.Raise(vbObjectError + 528, "ido_get_familias", Err.Description + " (" + spName + ")")
            End If



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


#Region " buscaEncargadoSubfamilia() "
    Public Function buscaEncargadoSubfamilia(ByVal codigoFilial As String, ByVal codigoSubfamilia As String) As String

        ' si cod_familia no es null trae el nombre de esa familia
        Const spName = "ido_sel_encargado_subfamilia"

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
            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_subfamilia", codigoSubfamilia).Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall

            daSql.Fill(resultDT)

            Return Trim(resultDT.Rows(0).Item("nombre"))

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
    Public Function obtieneSubfamilias(ByVal cod_familia As SqlString, ByVal cod_subfamilia As SqlString) As DataTable

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
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value + " (get_subfamilias)"
                Err.Raise(vbObjectError + 528, "get_subfamilias", Err.Description + " (" + spName + ")")
            End If



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
    Public Function get_matriz_sector(ByVal codigoSociedad As String) As DataTable

        Dim dbConn As New SqlConnection(Utiles.obtieneStringDeConexion())
        dbConn.Open()

        Dim spName = "select cod_sector from matriz_sector where cod_sociedad='" & codigoSociedad & "'"
        Dim daSql As New SqlDataAdapter
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim resultDT As New DataTable

        Try
            spCall.CommandType = CommandType.Text
            daSql.SelectCommand = spCall

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
    Public Function buscaClientes(ByVal codigoFilial As String, _
                                                ByVal codigoCliente As String, _
                                               ByVal razonSocial As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "ido_sel_clientes"
        Dim dbConn As SqlConnection = New SqlConnection
        Dim daSql As SqlDataAdapter = New SqlDataAdapter
        Dim resultDT As DataTable = New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", IIf(codigoCliente = "", DBNull.Value, codigoCliente)).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@nom_cliente", IIf(razonSocial = "", DBNull.Value, razonSocial)).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
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
        Const spName = "ido_sel_sucursales"

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
            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

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

    Public Function obtieneSucursales(ByVal codigoFilial As String, ByVal TipoLista As eModoLista) As DataTable
        Dim resultDT As New DataTable

        resultDT = obtieneSucursales(codigoFilial)
        Select Case TipoLista
            Case eModoLista.Todos
                Dim Myrow As DataRow = resultDT.NewRow
                Myrow.Item(0) = "-100"
                Myrow.Item(1) = "* Todos *"
                resultDT.Rows.InsertAt(Myrow, 0)
        End Select

        Return resultDT
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

#Region " buscaClientesPromotora() "

    Public Function buscaClientesPromotora(ByVal codigoFilial As String, ByVal codigoPromotora As String) As DataSet

        Const spName = "ido_sel_clientes_cartera"

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
            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_promotora", codigoPromotora).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

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

#Region " MAPEO PARAMETROS ANDES - SAP"
    Public Function seteaParametrosConsulta(ByVal sucursal As String, ByRef sociedad As String, ByRef centro As String) As String
        Select Case sucursal
            Case "001"
                sociedad = "GMSC"
                centro = ""
            Case "002"
                sociedad = "APER"
                centro = "2100"
            Case "022"
                sociedad = "APER"
                centro = "2200"
            Case "003"
                sociedad = "ABOL"
                centro = "3100"
            Case "004"
                sociedad = "ABOL"
                centro = "3200"
            Case "005"
                sociedad = "DGS0"
                centro = ""
            Case "P01"
                sociedad = "PCL1"
                centro = ""
            Case "P02"
                sociedad = "PCL1"
                centro = "5400"
            Case "P03"
                sociedad = "PCL1"
                centro = "5500"

        End Select

    End Function


#End Region

#Region " OBTIENE FECHA ACTUALIZACION DE TABLA"
    Public Function obtieneFechaActualizacion(ByVal ano_periodo As Integer, _
                                                ByVal mes_periodo As Integer, _
                                                ByVal codigo_sucursal As String, _
                                                ByVal nombre_tabla As String) As DateTime
        Const spName = "get_fecha_actualizacion_tabla"

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
            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int, 4).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int, 4).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = codigo_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@nombre_tabla", SqlDbType.Char, 60).Value = nombre_tabla
            spCall.Parameters("@nombre_tabla").Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

            daSql.Fill(resultDT)

            Return resultDT.Rows(0).Item("fecha_actualizacion")


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
#End Region

#Region "OBTIENE FAMILIAS"

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

            daSql.Fill(resultDT)
            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value + " (get_familias)"
                Err.Raise(vbObjectError + 528, "ido_get_familias", Err.Description + " (" + spName + ")")
            End If



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

            daSql.Fill(resultDT)
            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value + " (get_subfamilias)"
                Err.Raise(vbObjectError + 528, "get_subfamilias", Err.Description + " (" + spName + ")")
            End If



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

#Region "OBTIENE MARCAS DE PRODUCTOS"
    Public Function ObtieneMarcas(ByVal ano_periodo As Integer, ByVal mes_periodo As Integer, ByVal codigoFilial As String)
        Const spName = "ido_sel_marcas"

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

            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
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

#Region "  CLIENTES X CODIGO   "


    Public Function clientesCodigo(ByVal codigoFilial As String, _
                                                ByVal codigoSucursal As String, _
                                               ByVal codigoCliente As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "ido_datos_clientes_x_codigo"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codigoSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codigoCliente).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
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


    'VENTAS
#Region "VENTAS CATASTRO CLIENTES"
#Region " INGRESA VENTAS"
    Public Sub ingresaventas(ByRef infoVentas As stventas)




        Const spName = "fic_ingreso_ventas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter


        Dim spCall As New SqlCommand(spName, dbConn)
        spCall.CommandType = CommandType.StoredProcedure

        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        With infoVentas
            spCall.Parameters.Add("@result_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", infoVentas.anoPeriodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", infoVentas.mesPeriodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@codFilial", infoVentas.codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@codSucursal", infoVentas.codSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", infoVentas.codCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@vta_mensual", infoVentas.vta_mensual).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@vta_ano_anterior", infoVentas.vta_ano_anterior).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@tiene_preprensa", infoVentas.tprepre).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@tipo_maq_preprensa", infoVentas.tipo_pre).Direction = ParameterDirection.Input


        End With

        spCall.ExecuteNonQuery()

        dbConn.Close()
        spCall = Nothing
        dbConn = Nothing
    End Sub
#End Region

#Region "   OBTIENE VENTAS "

    Public Function obtieneVentas(ByVal anoPeriodo As String, _
                            ByVal codFilial As String, ByVal codSucursal As String, _
                            ByVal codCliente As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_ventas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@ano_periodo", anoPeriodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codCliente).Direction = ParameterDirection.Input


            daSql.SelectCommand = spCall
            daSql.Fill(resultDT)


            'If resultDT.Rows.Count <= 0 Then
            '    Err.Description = ""
            '    Err.Raise(vbObjectError + 512 + 104, "Maquina", Err.Description + " (sp)")
            'End If

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







#End Region

    'SEGMENTOS

#Region "  SEGMENTOS CLIENTES  "


    Public Function SegmentoCliente() As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_segmento"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure

            daSql.SelectCommand = spCall
            daSql.Fill(resultDT)


            If resultDT.Rows.Count < 0 Then
                Err.Description = "Ha excedido el maximo de rubros permitidos."
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

#Region "  SEGMENTOS X CLIENTES  "

    Public Function SegmentoXCliente(ByVal codigoCliente As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_segmento_x_clientes"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@cod_cliente", codigoCliente).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.Fill(resultDT)


            ''If resultDT.Rows.Count <= 0 Then
            'Err.Description = "No se econtraron resultados para esta consulta."
            'Err.Raise(vbObjectError + 512 + 104, "SegmentoXCliente", Err.Description + " (sp)")
            'End If

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

#Region " ELIMINA SEGMENTO Y PORCENTAJE DE VENTA "
    Public Sub eliminaSegmento(ByVal Segmento As String, ByRef Cliente As String)

        'Define Objects and Variables
        Const spName = "fic_elimina_tipo_segmento"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter

        Dim spCall As New SqlCommand

        Try

            ' Set and Open Connection
            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            ' Set sp Parameters
            With spCall
                .CommandText = spName
                .Connection = dbConn
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                .Parameters.Add("@cod_segmento", Segmento).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_cliente", Cliente).Direction = ParameterDirection.Input
            End With

            'Execute sp
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            If (spCall.Parameters("@resultValue").Value <= 0) Then
                Throw New Exception("No se pudo completar operación.")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            ' Dispose
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
        End Try

    End Sub
#End Region

#Region " INGRESA SEGMENTOS"
    Public Sub ingresaSegmento(ByRef infoSegmento As stSegmentos)




        Const spName = "fic_ingreso_segmentos"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter


        Dim spCall As New SqlCommand(spName, dbConn)
        spCall.CommandType = CommandType.StoredProcedure

        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        With infoSegmento
            spCall.Parameters.Add("@result_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_cliente", infoSegmento.cod_cliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_segmento", infoSegmento.cod_segmento).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@porcentaje", infoSegmento.porcentajes).Direction = ParameterDirection.Input

        End With

        spCall.ExecuteNonQuery()

        dbConn.Close()
        spCall = Nothing
        dbConn = Nothing
    End Sub
#End Region

    'MAQUINAS

#Region "   OBTIENE MAQUINA "

    Public Function Maquina(ByVal codFilial As String, _
                                    ByVal codSucursal As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_maquinas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure


            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codSucursal).Direction = ParameterDirection.Input


            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            daSql.Fill(resultDT)


            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se econtraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 104, "Maquina", Err.Description + " (sp)")
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

#Region "   OBTIENE MODELO MAQUINA "

    Public Function ModeloMaquina(ByVal cod_maquina As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_modelo_maquina"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure


            spCall.Parameters.Add("@cod_maquina", cod_maquina).Direction = ParameterDirection.Input



            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            daSql.Fill(resultDT)


            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se econtraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 104, "Maquina", Err.Description + " (sp)")
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

#Region "  MAQUINA X CLIENTES  "

    Public Function MaquinaXCliente(ByVal codFilial As String, _
                                    ByVal codigoCliente As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_maquina_cliente"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure


            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codigoCliente).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            daSql.Fill(resultDT)


            'If resultDT.Rows.Count <= 0 Then
            '    Err.Description = "No se econtraron resultados para esta consulta."
            '    Err.Raise(vbObjectError + 512 + 104, "MaquinaXCliente", Err.Description + " (sp)")
            'End If

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

#Region " ELIMINA MAQUINA CLIENTE "
    Public Sub eliminaMaquina(ByVal codFilial As String, _
                                ByVal codCliente As String, ByVal codMaquina As String)

        'Define Objects and Variables
        Const spName = "fic_elimina_maquina_x_cliente"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter

        Dim spCall As New SqlCommand

        Try

            ' Set and Open Connection
            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            ' Set sp Parameters
            With spCall
                .CommandText = spName
                .Connection = dbConn
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                .Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_cliente", codCliente).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_maquina", codMaquina).Direction = ParameterDirection.Input
            End With

            'Execute sp
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            If (spCall.Parameters("@resultValue").Value <= 0) Then
                Throw New Exception("No se pudo completar operación.")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            ' Dispose
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
        End Try

    End Sub
#End Region

#Region " INGRESA MAQUINAS"
    Public Sub ingresaMaquina(ByRef infoMaquina As stMaquinas)




        Const spName = "fic_ingreso_maquinas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter


        Dim spCall As New SqlCommand(spName, dbConn)
        spCall.CommandType = CommandType.StoredProcedure

        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        With infoMaquina
            spCall.Parameters.Add("@result_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_filial", infoMaquina.codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", infoMaquina.codCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_maquina", infoMaquina.codMaquina).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@modelo_maquina", infoMaquina.modeloMaquina).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@tipo_plancha", infoMaquina.TipoPlancha).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@tam_plancha", infoMaquina.tamanoPlancha).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@nCuerpos", infoMaquina.nCuerpos).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@tBarniz", infoMaquina.tBarniz).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@barra", infoMaquina.tBarra).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@consumo", infoMaquina.consumoMensual).Direction = ParameterDirection.Input


        End With

        spCall.ExecuteNonQuery()

        dbConn.Close()
        spCall = Nothing
        dbConn = Nothing
    End Sub
#End Region




    'MANTILLAS

#Region "   OBTIENE MANTILLAS X CLIENTE "

    Public Function mantillasXcliente(ByVal codFilial As String, _
                                    ByVal codSucursal As String, _
                                    ByVal codCliente As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_mantilla_x_cliente"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codCliente).Direction = ParameterDirection.Input


            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            daSql.Fill(resultDT)


            'If resultDT.Rows.Count <= 0 Then
            '    Err.Description = "No se econtraron resultados para esta consulta."
            '    Err.Raise(vbObjectError + 512 + 104, "mantillasXcliente", Err.Description + " (sp)")
            'End If

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

#Region "   OBTIENE MANTILLAS "

    Public Function mantillas(ByVal codFilial As String, _
                                    ByVal codSucursal As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_mantillas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codSucursal).Direction = ParameterDirection.Input


            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            daSql.Fill(resultDT)


            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se econtraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 104, "tintas", Err.Description + " (sp)")
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

#Region " ELIMINA MANTILLA "
    Public Sub eliminaMantilla(ByVal codFilial As String, ByVal codCliente As String, ByVal desMantilla As String)

        'Define Objects and Variables
        Const spName = "fic_elimina_mantilla_x_cliente"
        Dim dbConn As New SqlConnection

        Dim spCall As New SqlCommand

        Try

            ' Set and Open Connection
            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            ' Set sp Parameters
            With spCall
                .CommandText = spName
                .Connection = dbConn
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                .Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_cliente", codCliente).Direction = ParameterDirection.Input
                .Parameters.Add("@des_mantilla", desMantilla).Direction = ParameterDirection.Input

            End With

            'Execute sp
            spCall.ExecuteNonQuery()

            If (spCall.Parameters("@resultValue").Value <= 0) Then
                Throw New Exception("No se pudo completar operación.")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            ' Dispose
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing

        End Try

    End Sub
#End Region

#Region " INGRESA MANTILLAS"
    Public Sub ingresaMantilla(ByRef infoMantilla As stMantilla)




        Const spName = "fic_ingreso_mantillas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter


        Dim spCall As New SqlCommand(spName, dbConn)
        spCall.CommandType = CommandType.StoredProcedure

        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        With infoMantilla
            spCall.Parameters.Add("@result_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@codFilial", infoMantilla.codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@codSucursal", infoMantilla.codSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", infoMantilla.codCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@des_mantilla", infoMantilla.codMantilla).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@consumo", infoMantilla.consumo).Direction = ParameterDirection.Input


        End With

        spCall.ExecuteNonQuery()

        dbConn.Close()
        spCall = Nothing
        dbConn = Nothing
    End Sub
#End Region


    'TINTAS

#Region "   OBTIENE TINTAS "

    Public Function tintas(ByVal codFilial As String, _
                                    ByVal codSucursal As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_tintas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure


            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codSucursal).Direction = ParameterDirection.Input


            daSql.SelectCommand = spCall
            daSql.Fill(resultDT)


            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se econtraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 104, "tintas", Err.Description + " (sp)")
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

#Region "   OBTIENE TINTAS X CLIENTE "

    Public Function tintasXcliente(ByVal codFilial As String, _
                                    ByVal codSucursal As String, _
                                    ByVal codCliente As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_tinta_x_cliente"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure


            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codCliente).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.Fill(resultDT)


            'If resultDT.Rows.Count <= 0 Then
            '    Err.Description = "No se econtraron resultados para esta consulta."
            '    Err.Raise(vbObjectError + 512 + 104, "tintasXcliente", Err.Description + " (sp)")
            'End If

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

#Region " ELIMINA TINTA "
    Public Sub eliminaTinta(ByVal codFilial As String, _
                                ByVal codCliente As String, ByVal codTinta As String)

        'Define Objects and Variables
        Const spName = "fic_elimina_tintas_x_cliente"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter

        Dim spCall As New SqlCommand

        Try

            ' Set and Open Connection
            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            ' Set sp Parameters
            With spCall
                .CommandText = spName
                .Connection = dbConn
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                .Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_cliente", codCliente).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_tinta", codTinta).Direction = ParameterDirection.Input

            End With

            'Execute sp
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            If (spCall.Parameters("@resultValue").Value <= 0) Then
                Throw New Exception("No se pudo completar operación.")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            ' Dispose
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
        End Try

    End Sub
#End Region

#Region " INGRESA TINTAS"
    Public Sub ingresaTinta(ByRef infoTinta As stTinta)




        Const spName = "fic_ingreso_tintas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter


        Dim spCall As New SqlCommand(spName, dbConn)
        spCall.CommandType = CommandType.StoredProcedure

        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        With infoTinta
            spCall.Parameters.Add("@result_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@codFilial", infoTinta.codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@codSucursal", infoTinta.codSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", infoTinta.codCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_tinta", infoTinta.codTinta).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@consumo", infoTinta.consumo).Direction = ParameterDirection.Input


        End With

        spCall.ExecuteNonQuery()

        dbConn.Close()
        spCall = Nothing
        dbConn = Nothing
    End Sub
#End Region


    'MARCAS

#Region "   OBTIENE MARCAS "

    Public Function Marcas(ByVal codFilial As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_marcas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure


            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input



            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            daSql.Fill(resultDT)


            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se econtraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 104, "Maquina", Err.Description + " (sp)")
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

#Region "   OBTIENE MARCAS X CLIENTE "

    Public Function marcaXcliente(ByVal anoPeriodo As String, _
                                    ByVal mesPeriodo As String, _
                                    ByVal codFilial As String, _
                                    ByVal codSucursal As String, _
                                    ByVal codCliente As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_mantilla_x_cliente"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@ano_periodo", anoPeriodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", mesPeriodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codCliente).Direction = ParameterDirection.Input


            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            daSql.Fill(resultDT)


            'If resultDT.Rows.Count <= 0 Then
            '    Err.Description = "No se econtraron resultados para esta consulta."
            '    Err.Raise(vbObjectError + 512 + 104, "tintasXcliente", Err.Description + " (sp)")
            'End If

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

#Region " ELIMINA MARCA "
    Public Sub eliminaMarca(ByVal anoPeriodo As String, ByRef mesPeriodo As String, _
                                ByVal codFilial As String, ByVal codSucursal As String, _
                                ByVal codCliente As String, ByVal codMarca As String)

        'Define Objects and Variables
        Const spName = "fic_elimina_marca_x_cliente"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter

        Dim spCall As New SqlCommand

        Try

            ' Set and Open Connection
            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            ' Set sp Parameters
            With spCall
                .CommandText = spName
                .Connection = dbConn
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                .Parameters.Add("@ano_periodo", anoPeriodo).Direction = ParameterDirection.Input
                .Parameters.Add("@mes_periodo", mesPeriodo).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_sucursal", codSucursal).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_cliente", codCliente).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_mantilla", codMarca).Direction = ParameterDirection.Input

            End With

            'Execute sp
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            If (spCall.Parameters("@resultValue").Value <= 0) Then
                Throw New Exception("No se pudo completar operación.")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            ' Dispose
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
        End Try

    End Sub
#End Region

#Region " INGRESA MARCA"
    Public Sub ingresaMarca(ByRef infoMarca As stMarcaPapel)




        Const spName = "fic_ingreso_marcas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter


        Dim spCall As New SqlCommand(spName, dbConn)
        spCall.CommandType = CommandType.StoredProcedure

        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        With infoMarca
            spCall.Parameters.Add("@result_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", infoMarca.anoPeriodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", infoMarca.mesPeriodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", infoMarca.codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", infoMarca.codSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", infoMarca.codCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_tinta", infoMarca.codMarcaPapel).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@consumo", infoMarca.consumo).Direction = ParameterDirection.Input


        End With

        spCall.ExecuteNonQuery()

        dbConn.Close()
        spCall = Nothing
        dbConn = Nothing
    End Sub
#End Region


    'PAPEL

#Region "   OBTIENE PAPEL "

    Public Function papel(ByVal codFilial As String, _
                                    ByVal codSucursal As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_papel"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure


            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codSucursal).Direction = ParameterDirection.Input


            daSql.SelectCommand = spCall
            daSql.Fill(resultDT)


            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se econtraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 104, "tintas", Err.Description + " (sp)")
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

#Region "   OBTIENE PAPEL X CLIENTE "

    Public Function papelXcliente(ByVal codFilial As String, _
                                    ByVal codSucursal As String, _
                                    ByVal codCliente As String) As DataTable

        'recibimos un criterio de búsqueda que puede ser código de cliente
        'o subcadena de la razón social
        'Llamamos a psp
        Const spName = "fic_obtiene_papel_cliente"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim spCall As New SqlCommand(spName, dbConn)

            spCall.CommandType = CommandType.StoredProcedure


            spCall.Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codCliente).Direction = ParameterDirection.Input


            daSql.SelectCommand = spCall
            daSql.Fill(resultDT)


            'If resultDT.Rows.Count <= 0 Then
            '    Err.Description = "No se econtraron resultados para esta consulta."
            '    Err.Raise(vbObjectError + 512 + 104, "tintasXcliente", Err.Description + " (sp)")
            'End If

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

#Region " ELIMINA PAPEL "
    Public Sub eliminaPapel(ByVal codFilial As String, _
                                ByVal codCliente As String, ByVal codPapel As String)

        'Define Objects and Variables
        Const spName = "fic_elimina_papel_x_cliente"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter

        Dim spCall As New SqlCommand

        Try

            ' Set and Open Connection
            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            ' Set sp Parameters
            With spCall
                .CommandText = spName
                .Connection = dbConn
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                .Parameters.Add("@cod_filial", codFilial).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_cliente", codCliente).Direction = ParameterDirection.Input
                .Parameters.Add("@cod_tinta", codPapel).Direction = ParameterDirection.Input

            End With

            'Execute sp
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            If (spCall.Parameters("@resultValue").Value <= 0) Then
                Throw New Exception("No se pudo completar operación.")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            ' Dispose
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
        End Try

    End Sub
#End Region

#Region " INGRESA PAPEL"
    Public Sub ingresaPapel(ByRef infoTinta As stTinta)




        Const spName = "fic_ingreso_tintas"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter


        Dim spCall As New SqlCommand(spName, dbConn)
        spCall.CommandType = CommandType.StoredProcedure

        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        With infoTinta
            spCall.Parameters.Add("@result_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@codFilial", infoTinta.codFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@codSucursal", infoTinta.codSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", infoTinta.codCliente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_tinta", infoTinta.codTinta).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@consumo", infoTinta.consumo).Direction = ParameterDirection.Input


        End With

        spCall.ExecuteNonQuery()

        dbConn.Close()
        spCall = Nothing
        dbConn = Nothing
    End Sub
#End Region



    Public Sub cargaMesesCalendario(ByVal ddl As DropDownList, ByVal mesActivo As Integer)
        ddl.ClearSelection()
        ddl.Items.Clear()
        For mes As Integer = 1 To 12
            ddl.Items.Add(New ListItem(MonthName(mes), mes.ToString("00")))
        Next

        If Not IsNothing(ddl.Items.FindByValue(mesActivo.ToString("00"))) Then
            ddl.Items.FindByValue(mesActivo.ToString("00")).Selected = True
        End If
    End Sub

    Public Sub cargaAñosCalendario(ByVal ddl As DropDownList, ByVal añoBase As Integer, ByVal añoActivo As Integer)
        ddl.ClearSelection()
        ddl.Items.Clear()
        For año As Integer = Now.Year To añoBase Step -1
            ddl.Items.Add(New ListItem(año, año))
        Next

        If Not IsNothing(ddl.Items.FindByValue(añoActivo)) Then
            ddl.Items.FindByValue(añoActivo).Selected = True
        End If
    End Sub

    Public Sub cargaEjecutivosComerciales(ByVal ddl As DropDownList, ByVal codigoFilial As String, ByVal arrDominioValores() As String)
        ddl.ClearSelection()
        ddl.Items.Clear()

        With ddl
            .DataSource = Utiles.obtieneEjecutivosComerciales(codigoFilial)
            .DataTextField = "nom_ejec_com_2"
            .DataValueField = "cod_ejec_com"
            .DataBind()
        End With

        'A veces es necesario acotar el dominio de valores de ejecutivos comerciales
        If Not IsNothing(arrDominioValores) Then
            Dim eliminar As Boolean
            Dim i As Integer = 0

            'Iteramos sobre elementos del dropdownlist buscando coincidencia con valor de dominio
            While i <= ddl.Items.Count - 1

                eliminar = True

                For j As Integer = 0 To arrDominioValores.Length - 1
                    If ddl.Items(i).Value = arrDominioValores(j) Then
                        eliminar = False
                        Exit For
                    End If
                Next

                If eliminar Then
                    ddl.Items.RemoveAt(i)
                Else
                    i += 1
                End If
            End While
        End If
    End Sub

    Public Sub cargaSalesAdvisors(ByVal ddl As DropDownList, ByVal codigoFilial As String, ByVal arrDominioValores() As String)
        ddl.ClearSelection()
        ddl.Items.Clear()

        With ddl
            .DataSource = Utiles.obtieneSalesAdvisors(codigoFilial)
            .DataTextField = "nom_sales_adv"
            .DataValueField = "cod_sales_adv"
            .DataBind()
        End With

        'A veces es necesario acotar el dominio de valores de sales advisors
        If Not IsNothing(arrDominioValores) Then
            Dim eliminar As Boolean
            Dim i As Integer = 0

            'Iteramos sobre elementos del dropdownlist buscando coincidencia con valor de dominio
            While i <= ddl.Items.Count - 1

                eliminar = True

                For j As Integer = 0 To arrDominioValores.Length - 1
                    If ddl.Items(i).Value = arrDominioValores(j) Then
                        eliminar = False
                        Exit For
                    End If
                Next

                If eliminar Then
                    ddl.Items.RemoveAt(i)
                Else
                    i += 1
                End If
            End While
        End If
    End Sub

    Public Sub cargaCelulas(ByVal ddl As DropDownList, ByVal codigoSociedad As String, ByVal arrDominioValores() As String)
        ddl.ClearSelection()
        ddl.Items.Clear()
        With ddl
            .DataSource = CCelula.obtieneOcurrencias()
            .DataTextField = "celula"
            .DataValueField = "celula"
            .DataBind()
        End With

        'A veces es necesario acotar el dominio de valores de ejecutivos comerciales
        If Not IsNothing(arrDominioValores) Then
            Dim eliminar As Boolean
            Dim i As Integer = 0


            'Iteramos sobre elementos del dropdownlist buscando coincidencia con valor de dominio
            While i <= ddl.Items.Count - 1

                eliminar = True

                For j As Integer = 0 To arrDominioValores.Length - 1
                    If ddl.Items(i).Value = arrDominioValores(j) Then
                        eliminar = False
                        Exit For
                    End If
                Next

                If eliminar Then
                    ddl.Items.RemoveAt(i)
                Else
                    i += 1
                End If
            End While
        End If
    End Sub

    Public Sub cargaSociedades(ByVal ddl As DropDownList, ByVal codigoFilial As String)
        ddl.Items.Clear()
        Select Case codigoFilial
            Case "CHI"
                ddl.Items.Add(New ListItem("GMSC", "GMSC"))
            Case "BOL"
                ddl.Items.Add(New ListItem("ABOL", "ABOL"))
                ddl.Items.Add(New ListItem("DGS0", "DGS0"))
                ddl.Items.Add(New ListItem("ABOL y DGS0", "ABDG"))
            Case "PER"
                ddl.Items.Add(New ListItem("APER", "APER"))
        End Select
    End Sub

    Public Sub cargaSucursales(ByVal cbl As CheckBoxList, ByVal codigoFilial As String)
        cbl.Items.Clear()

        Select Case codigoFilial
            Case "CHI"
                cbl.Items.Add(New ListItem("001 - Santiago", "001"))
            Case "BOL"
                cbl.Items.Add(New ListItem("003 - La Paz", "003"))
                cbl.Items.Add(New ListItem("004 - Santa Cruz", "004"))
                cbl.Items.Add(New ListItem("005 - Iquique", "005"))
                cbl.Items.Add(New ListItem("007 - Vtas IQQ-BOL", "007"))
            Case "PER"
                cbl.Items.Add(New ListItem("002 - Casa Matriz", "002"))
                cbl.Items.Add(New ListItem("022 - Centro Lima", "022"))
        End Select

        For Each cb As ListItem In cbl.Items
            cb.Selected = True
        Next


    End Sub


End Module



