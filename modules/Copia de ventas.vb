Imports System.Data.SqlClient


Module ventas
    Const WidthError = 1



#Region " INFORMES VENTA CLIENTE ITEM VALOR DETALLE"


    Public Function vta_x_cliente_item_valor_detalle(ByVal cod_cliente As String, _
                            ByVal cod_producto As String, ByVal mes_periodo As Integer, _
                            ByVal ano_periodo As Integer, ByVal cod_filial As String, _
                            ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_venta_fisica_cliente_item_valor_detalle"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_cliente", SqlDbType.VarChar, 9).Value = cod_cliente
            spCall.Parameters("@cod_cliente").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_producto", SqlDbType.VarChar, 12).Value = cod_producto
            spCall.Parameters("@cod_producto").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "vta_x_cliente_item", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se encontraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 10, "vta_x_cliente_item_valor_detalle", Err.Description)
            End If


            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " INFORMES VENTA CLIENTE ITEM "


    Public Function vta_x_cliente_item(ByVal cod_cliente As String, _
                            ByVal val_tipo As String, ByRef nom_cliente As String, ByVal cod_promotora As String, _
                            ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String, _
                            ByVal tipo As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_venta_fisica_cliente_item_paso"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_cliente", SqlDbType.VarChar, 9).Value = cod_cliente
            spCall.Parameters("@cod_cliente").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_promotora", SqlDbType.VarChar, 3).Value = cod_promotora
            spCall.Parameters("@cod_promotora").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@tipo", SqlDbType.VarChar, 3).Value = tipo
            spCall.Parameters("@tipo").Direction = ParameterDirection.Input


            spCall.Parameters.Add("@val_tipo", SqlDbType.VarChar, 3).Value = "CAN"
            spCall.Parameters("@val_tipo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "vta_x_cliente_item", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se encontraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 10, "vta_x_cliente_item", Err.Description)
            End If

            nom_cliente = resultDT.Rows(0).Item("nom_cliente")

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
    Public Function vta_x_cliente_item_detalle(ByVal cod_cliente As String, ByVal cod_producto As String, ByRef des_producto As String, _
                            ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_venta_fisica_cliente_item_detalle"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_cliente", SqlDbType.VarChar, 9).Value = cod_cliente
            spCall.Parameters("@cod_cliente").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_producto", SqlDbType.VarChar, 6).Value = cod_producto
            spCall.Parameters("@cod_producto").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@des_producto", SqlDbType.VarChar, 60)
            spCall.Parameters("@des_producto").Direction = ParameterDirection.Output
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            spCall.CommandTimeout = 60
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()
            spCall.ResetCommandTimeout()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "vta_x_cliente_item", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)
            If Not (spCall.Parameters("@des_producto").Value Is DBNull.Value) Then
                des_producto = spCall.Parameters("@des_producto").Value
            End If


            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region " INFORMES VENTA ITEM CLIENTE"
    Public Function vta_x_item_cliente(ByVal cod_producto As String, ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_vta_fisica_item_cliente"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", ano_periodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", mes_periodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", cod_filial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", cod_sucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_producto", cod_producto).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, spName, Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function

#End Region

#Region " INFORMES VENTA X SUBFAMILIA"
    Public Function vta_x_subfamilia(ByVal val_tipo As String, ByVal cod_prodmanager As Object, ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_vta_x_subfamilia"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@val_tipo", SqlDbType.VarChar, 3).Value = val_tipo
            spCall.Parameters("@val_tipo").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_prodmanager", SqlDbType.Int).Value = cod_prodmanager
            spCall.Parameters("@cod_prodmanager").Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Raise(vbObjectError + 512 + WidthError, "ido_Ventas_x_SubFamilia", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)
            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se encontraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 10, "ido_Ventas_x_SubFamilia", Err.Description)
            End If

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " INFORMES VENTA SUBFAMILIA X PRODUCTO "
    Public Function vta_x_subfamilia_item(ByVal cod_subfamilia As String, ByVal val_tipo As String, _
                            ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_vta_x_subfamilia_item"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_subfamilia", SqlDbType.Char, 4).Value = cod_subfamilia
            spCall.Parameters("@cod_subfamilia").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@val_tipo", SqlDbType.VarChar, 3).Value = val_tipo
            spCall.Parameters("@val_tipo").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "vta_x_cliente_item", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " RESUMEN VENTA PROMOTORA-CLIENTE "
    Public Function res_vta_cliente_promo(ByVal cod_promotora As String, ByRef nom_promotora As String, _
                            ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_res_venta_x_cliente"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_promotora", SqlDbType.VarChar, 3).Value = cod_promotora
            spCall.Parameters("@cod_promotora").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@nom_promotora", SqlDbType.VarChar, 30)
            spCall.Parameters("@nom_promotora").Direction = ParameterDirection.Output
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_res_venta_x_cliente", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            nom_promotora = spCall.Parameters("@nom_promotora").Value
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

    Public Function res_sinvta_cliente_promo(ByVal cod_promotora As String, ByRef nom_promotora As String, _
                                                       ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                                                       ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_res_venta_x_cliente"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_promotora", SqlDbType.VarChar, 3).Value = cod_promotora
            spCall.Parameters("@cod_promotora").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_venta", SqlDbType.VarChar, 6).Value = "SINVTA"
            spCall.Parameters("@cod_venta").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@nom_promotora", SqlDbType.VarChar, 30)
            spCall.Parameters("@nom_promotora").Direction = ParameterDirection.Output
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_res_venta_x_cliente", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            nom_promotora = spCall.Parameters("@nom_promotora").Value
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

#Region " MATRIZ ANALISIS VENTA CLIENTE (UNIDADES FISICAS)"

    Public Function matriz_vta_fisica_cliente_prom(ByVal ano_periodo As Int16, ByVal mes_periodo As Int16, _
                                                                        ByVal cod_filial As String, ByVal cod_sucursal As String, _
                                                                        ByVal cod_promotora As String, ByVal cod_sector As String) As DataSet



        Const spName = "ido_matriz_vta_fisica_cliente_promo"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDS As New DataSet

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer


            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_promotora", SqlDbType.VarChar, 4).Value = cod_promotora
            spCall.Parameters("@cod_promotora").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sector", SqlDbType.VarChar, 30).Value = cod_sector
            spCall.Parameters("@cod_sector").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_res_venta_x_cliente", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDS)

            Return resultDS

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
            resultDS.Dispose()
        End Try

    End Function

    Public Function matriz_vta_fisica_cliente_calc(ByVal cod_promotora As String, ByVal cod_grupo As String, ByVal cod_cliente As String) As String

        Const spName = "ido_matriz_vta_fisica_cliente_calc"
        Dim dbConn As New SqlConnection
        Dim daSql As New SqlDataAdapter
        Dim spCall As New SqlCommand(spName, dbConn)

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer
            Dim valor

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_promotora", SqlDbType.VarChar, 3).Value = cod_promotora
            spCall.Parameters("@cod_promotora").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_grupo", SqlDbType.VarChar, 20).Value = cod_grupo
            spCall.Parameters("@cod_grupo").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", SqlDbType.Char, 9).Value = cod_cliente
            spCall.Parameters("@cod_cliente").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@valor", SqlDbType.VarChar, 15).Value = DBNull.Value
            spCall.Parameters("@valor").Direction = ParameterDirection.Output
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_matriz_vta_fisica_cliente_calc", Err.Description + " (sp)")
            End If

            valor = spCall.Parameters("@valor").Value
            Return valor

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall.Dispose()
            daSql.Dispose()
        End Try

    End Function
#End Region

#Region " DETALLE DOCUMENTO DESPACHO "

    Public Function vta_detalle_documento(ByVal ano_periodo As Integer, ByVal mes_periodo As Integer, ByVal cod_filial As String, ByVal cod_sucursal As String, ByVal num_documento As Integer, ByVal cod_documento As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_detalle_documento_x_sucursal"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue


            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int, 3).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int, 3).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@num_documento", SqlDbType.Int).Value = num_documento
            spCall.Parameters("@num_documento").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_documento", SqlDbType.Char, 3).Value = cod_documento
            spCall.Parameters("@cod_documento").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "vta_detalle_documento", Err.Description + " (sp)")
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

#Region " LISTADO DOCUMENTOS DESPACHO X FECHA "

    Public Function vta_despacho_doc_cli_fecha(ByVal cod_filial As String, _
                                               ByVal cod_sucursal As String, _
                                               ByVal cod_documento As String, _
                                               ByVal cod_cliente As String, _
                                               ByVal fecha_inicio As Date, _
                                               ByVal fecha_termino As Date) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_encabezado_documento_x_sucursal"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_documento", SqlDbType.Char, 3).Value = cod_documento
            spCall.Parameters("@cod_documento").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_cliente", SqlDbType.VarChar, 12).Value = cod_cliente
            spCall.Parameters("@cod_cliente").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@fec_inicio", SqlDbType.DateTime).Value = fecha_inicio
            spCall.Parameters("@fec_inicio").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@fec_termino", SqlDbType.DateTime).Value = fecha_termino
            spCall.Parameters("@fec_termino").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_encabezado_documento", Err.Description + " (sp)")
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

#Region " LISTADO DOCUMENTOS DESPACHO X PRODUCTO "

    Public Function vta_despacho_doc_cli_prod(ByVal cod_filial As String, _
                                                ByVal cod_sucursal As String, _
                                                ByVal cod_documento As String, _
                                                ByVal cod_cliente As String, _
                                                ByVal cod_producto As String) As DataTable


        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_facturas_cliente_producto"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_documento", SqlDbType.Char, 3).Value = cod_documento
            spCall.Parameters("@cod_documento").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", SqlDbType.VarChar, 10).Value = cod_cliente
            spCall.Parameters("@cod_cliente").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_producto", SqlDbType.VarChar, 12).Value = cod_producto
            spCall.Parameters("@cod_producto").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_encabezado_documento", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " DOCUMENTOS POR NUMERO DE DOCUMENTO "


    Public Function vta_despacho_doc_cli_docto(ByVal cod_filial As String, _
                                                ByVal cod_sucursal As String, _
                                                ByVal num_documento As String) As DataTable


        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_documentos_x_numero"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@num_documento", SqlDbType.Int, 4).Value = num_documento
            spCall.Parameters("@num_documento").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "vta_despacho_doc_cli_docto", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region


#Region " RANKING MARGEN VENTA CLIENTE ACUMULADO "
    Public Function mar_vta_cliente_acu(ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_vta_margen_prom_cli_acu"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", ano_periodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", mes_periodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", cod_filial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", cod_sucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_promotora", DBNull.Value).Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_vta_margen_prom_cli_acu", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " RANKING MARGEN VENTA PROMOTORA-CLIENTE ACUMULADO "
    Public Function mar_vta_promo_cliente_acu(ByVal cod_promotora As String, _
                            ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_vta_margen_prom_cli_acu"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_promotora", SqlDbType.VarChar, 3).Value = cod_promotora
            spCall.Parameters("@cod_promotora").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_vta_margen_prom_cli_acu", Err.Description + " (sp)")
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

#Region " VENTA ITEM 12 MESES T1"
    Public Function vta_item_12meses_t1(ByVal ano_periodo As Integer, _
                                      ByVal mes_periodo As Integer, _
                                        ByVal cod_filial As String, _
                                        ByVal cod_sucursal As String, _
                                        ByVal cod_familia As String, _
                                        ByVal cod_subfamilia As String, _
                                        ByVal cod_producto As String) As DataTable

        Const spName = "ido_vta_item_12meses_t1"
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
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            If (cod_familia = "") Then
                spCall.Parameters.Add("@cod_familia", SqlDbType.Char, 3).Value = DBNull.Value
            Else
                spCall.Parameters.Add("@cod_familia", SqlDbType.Char, 3).Value = cod_familia
            End If
            spCall.Parameters("@cod_familia").Direction = ParameterDirection.Input

            If (cod_subfamilia = "") Then
                spCall.Parameters.Add("@cod_subfamilia", SqlDbType.Char, 4).Value = DBNull.Value
            Else
                spCall.Parameters.Add("@cod_subfamilia", SqlDbType.Char, 4).Value = cod_subfamilia
            End If
            spCall.Parameters("@cod_subfamilia").Direction = ParameterDirection.Input

            If (cod_producto = "") Then
                spCall.Parameters.Add("@cod_producto", SqlDbType.Char, 12).Value = DBNull.Value
            Else
                spCall.Parameters.Add("@cod_producto", SqlDbType.Char, 12).Value = cod_producto
            End If
            spCall.Parameters("@cod_producto").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_vta_item_12meses", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            ' nom_promotora = spCall.Parameters("@nom_promotora").Value
            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " VENTA ITEM 12 MESES T2"
    Public Function vta_item_12meses_t2(ByVal ano_periodo As Integer, _
                                      ByVal mes_periodo As Integer, _
                                        ByVal cod_filial As String, _
                                        ByVal cod_sucursal As String, _
                                        ByVal cod_familia As String, _
                                        ByVal cod_subfamilia As String, _
                                        ByVal cod_producto As String, _
                                        ByVal cod_proveedor As String, _
                                        ByVal incluye_mes_actual As Integer) As DataTable

        Const spName = "ido_vta_item_12meses_t2_filiales"
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
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            If (cod_familia = "") Then
                spCall.Parameters.Add("@cod_familia", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_familia", cod_familia).Direction = ParameterDirection.Input
            End If

            If (cod_subfamilia = "") Then
                spCall.Parameters.Add("@cod_subfamilia", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_subfamilia", cod_subfamilia).Direction = ParameterDirection.Input
            End If

            If (cod_producto = "") Then
                spCall.Parameters.Add("@cod_producto", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_producto", cod_producto).Direction = ParameterDirection.Input
            End If

            If (cod_proveedor = "") Then
                spCall.Parameters.Add("@cod_proveedor", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_proveedor", cod_proveedor).Direction = ParameterDirection.Input
            End If

            spCall.Parameters.Add("@incluye_mes_actual", incluye_mes_actual).Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_vta_item_12meses", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region "  INFO VENTA  ITEM 12 MESES  "
    Public Function info_vta_item_mes(ByVal cod_producto As String, ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_info_venta_item_mes"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            'spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            'spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", ano_periodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", mes_periodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", cod_filial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", cod_sucursal).Direction = ParameterDirection.Input
            If Trim(cod_producto) = "" Then
                spCall.Parameters.Add("@cod_producto", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_producto", cod_producto).Direction = ParameterDirection.Input
            End If

            'spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            'spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            'resultSQL = spCall.Parameters("@resultValue").Value
            'If resultSQL <> 0 Then
            '    Err.Description = spCall.Parameters("@errorMsg").Value
            '    Err.Raise(vbObjectError + 512 + WidthError, "ido_info_venta_item_mes", Err.Description + " (sp)")
            'End If

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

#Region "  INFO VENTA  ITEM 12 MESES  (AÑO ANTERIOR)"
    Public Function info_vta_item_ano(ByVal cod_producto As String, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_info_venta_item_ano"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            'spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            'spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", ano_periodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", cod_filial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", cod_sucursal).Direction = ParameterDirection.Input
            If Trim(cod_producto) = "" Then
                spCall.Parameters.Add("@cod_producto", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_producto", cod_producto).Direction = ParameterDirection.Input
            End If

            'spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            'spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            'resultSQL = spCall.Parameters("@resultValue").Value
            'If resultSQL <> 0 Then
            '    Err.Description = spCall.Parameters("@errorMsg").Value
            '    Err.Raise(vbObjectError + 512 + WidthError, "ido_info_venta_item_mes", Err.Description + " (sp)")
            'End If

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

#Region " VENTA VOLUMEN 12 MESES "
    Public Function vta_volumen_12meses(ByVal ano_periodo As Integer, _
                                        ByVal mes_periodo As Integer, _
                                        ByVal cod_filial As String, _
                                        ByVal cod_sucursal As String, _
                                        ByVal cod_familia As String, _
                                        ByVal cod_subfamilia As String, _
                                        ByVal cod_producto As String, _
                                        ByVal cod_proveedor As String, _
                                        ByVal incluye_mes_actual As Integer) As DataTable

        Const spName = "ido_vta_volumen_12meses_filiales"
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
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", ano_periodo).Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", mes_periodo).Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", cod_filial).Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", cod_sucursal).Direction = ParameterDirection.Input

            If (cod_familia = "") Then
                spCall.Parameters.Add("@cod_familia", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_familia", cod_familia).Direction = ParameterDirection.Input
            End If

            If (cod_subfamilia = "") Then
                spCall.Parameters.Add("@cod_subfamilia", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_subfamilia", cod_subfamilia).Direction = ParameterDirection.Input
            End If

            If (cod_producto = "") Then
                spCall.Parameters.Add("@cod_producto", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_producto", cod_producto).Direction = ParameterDirection.Input
            End If

            If (cod_proveedor = "") Then
                spCall.Parameters.Add("@cod_proveedor", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_proveedor", cod_proveedor).Direction = ParameterDirection.Input
            End If

            spCall.Parameters.Add("@incluye_mes_actual", incluye_mes_actual).Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_vta_volumen_12meses", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            ' nom_promotora = spCall.Parameters("@nom_promotora").Value
            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " VENTA ITEM 6 MESES "

    Public Function vta_item_6meses(ByVal ano_periodo As Integer, _
                                      ByVal mes_periodo As Integer, _
                                        ByVal cod_filial As String, _
                                        ByVal cod_sucursal As String, _
                                        ByVal cod_familia As String, _
                                        ByVal cod_subfamilia As String, _
                                        ByVal cod_producto As String, _
                                        ByVal cod_proveedor As String, _
                                        ByVal incluye_mes_actual As Integer) As DataTable

        Const spName = "ido_vta_item_6meses_filiales"
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
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            If (cod_familia = "") Then
                spCall.Parameters.Add("@cod_familia", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_familia", cod_familia).Direction = ParameterDirection.Input
            End If

            If (cod_subfamilia = "") Then
                spCall.Parameters.Add("@cod_subfamilia", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_subfamilia", cod_subfamilia).Direction = ParameterDirection.Input
            End If

            If (cod_producto = "") Then
                spCall.Parameters.Add("@cod_producto", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_producto", cod_producto).Direction = ParameterDirection.Input
            End If

            If (cod_proveedor = "") Then
                spCall.Parameters.Add("@cod_proveedor", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_proveedor", cod_proveedor).Direction = ParameterDirection.Input
            End If

            spCall.Parameters.Add("@incluye_mes_actual", incluye_mes_actual).Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_vta_item_12meses", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " VENTA VOLUMEN 6 MESES "

    Public Function vta_volumen_6meses(ByVal ano_periodo As Integer, _
                                      ByVal mes_periodo As Integer, _
                                        ByVal cod_filial As String, _
                                        ByVal cod_sucursal As String, _
                                        ByVal cod_familia As String, _
                                        ByVal cod_subfamilia As String, _
                                        ByVal cod_producto As String, _
                                        ByVal cod_proveedor As String, _
                                         ByVal incluye_mes_actual As Integer) As DataTable

        Const spName = "ido_vta_volumen_6meses_filiales"
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
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 4).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 4).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            If (cod_familia = "") Then
                spCall.Parameters.Add("@cod_familia", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_familia", cod_familia).Direction = ParameterDirection.Input
            End If

            If (cod_subfamilia = "") Then
                spCall.Parameters.Add("@cod_subfamilia", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_subfamilia", cod_subfamilia).Direction = ParameterDirection.Input
            End If

            If (cod_producto = "") Then
                spCall.Parameters.Add("@cod_producto", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_producto", cod_producto).Direction = ParameterDirection.Input
            End If

            If (cod_proveedor = "") Then
                spCall.Parameters.Add("@cod_proveedor", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_proveedor", cod_proveedor).Direction = ParameterDirection.Input
            End If

            spCall.Parameters.Add("@incluye_mes_actual", incluye_mes_actual).Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_vta_item_12meses", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " DETALLE CONTROL FACTURAS  "

    Public Function vta_det_control_fac(ByVal cod_filial As String, ByVal cod_sucursal As String, ByVal tipo_moneda As String, ByVal fecha As Date) As DataTable

        Const spName = "ido_det_control_fac_filiales"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandTimeout = 90
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha
            spCall.Parameters("@fecha").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@tipo_moneda", SqlDbType.Char, 3).Value = tipo_moneda
            spCall.Parameters("@tipo_moneda").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output

            spCall.ResetCommandTimeout()
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_det_control_fac", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)
            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " RESUMEN FACTURACION DIAS MES  "

    Public Function res_facturacion_dias_mes(ByVal cod_filial As String, ByVal cod_sucursal As String, ByVal fecha As Date, ByVal tipo_moneda As String) As DataTable

        Const spName = "ido_res_facturacion_dias_mes_filiales"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandTimeout = 90
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha
            spCall.Parameters("@fecha").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@tipo_moneda", SqlDbType.Char, 3).Value = tipo_moneda
            spCall.Parameters("@tipo_moneda").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output

            spCall.ResetCommandTimeout()
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_res_facturacion_dias_mes", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)
            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " RESUMEN FACTURACION MESES ANO  "

    Public Function res_facturacion_meses_ano(ByVal cod_filial As String, ByVal cod_sucursal As String, ByVal fecha As Date) As DataTable

        Const spName = "ido_res_facturacion_meses_ano2"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable


        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandTimeout = 90
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@xMes", SqlDbType.Int).Value = Month(fecha)
            spCall.Parameters("@xMes").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@xYear", SqlDbType.Int).Value = Year(fecha)
            spCall.Parameters("@xYear").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input
            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            spCall.ResetCommandTimeout()
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_res_facturacion_meses_ano", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)
            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " RESUMEN VENTA X INTERLOCUTOR   "

    Public Function res_vta_x_interlocutor(ByVal cod_sucursal As String, ByVal cod_filial As String, _
                                                               ByVal fecha_inicio As Date, ByVal fecha_termino As Date, ByVal tipo_inter As String) As DataTable


        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_res_venta_x_interlocutor_2"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 3).Value = cod_filial
            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters.Add("@fec_inicio", SqlDbType.DateTime).Value = fecha_inicio
            spCall.Parameters.Add("@fec_termino", SqlDbType.DateTime).Value = fecha_termino
            spCall.Parameters.Add("@tipo", tipo_inter)

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_res_venta_x_interlocutor", Err.Description + " (sp)")
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

#Region " RANKING MARGEN VENTA SUBFAMILIA ITEM  "
    Public Function mar_vta_subfamilia_item(ByVal cod_producto As String, ByVal cod_subfamilia As String, _
                            ByVal mes_periodo As Integer, ByVal ano_periodo As Integer, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_vta_margen_subfamilia_item"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)


        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", ano_periodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", mes_periodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", cod_filial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", cod_sucursal).Direction = ParameterDirection.Input
            If Trim(cod_subfamilia) = "" Then
                spCall.Parameters.Add("@cod_subfamilia", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_subfamilia", cod_subfamilia).Direction = ParameterDirection.Input
            End If
            If Trim(cod_producto) = "" Then
                spCall.Parameters.Add("@cod_producto", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@cod_producto", cod_producto).Direction = ParameterDirection.Input
            End If

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_vta_margen_subfam_item", Err.Description + " (sp)")
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

#Region "  CONTROL DE DESCUENTOS MANUALES MODIFICADOS  "
    Public Function res_descto_manual(ByVal fec_periodo As Date, _
                            ByVal cod_filial As String, ByVal cod_sucursal As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "id_res_descto_manual"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_filial", cod_filial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", cod_sucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@fec_periodo", fec_periodo).Direction = ParameterDirection.Input

            'spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            'spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = "Ocurrió un error al tratar de ejecutar el procedimiento almacenado " + spName 'spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_info_venta_item_mes", Err.Description)
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

#Region " ANALISIS VENTA CONSOLIDADO COUCHE  "
    Public Function vta_consolidado_couche(ByVal ano_periodo As Integer, _
                                      ByVal mes_periodo As Integer, _
                                        ByVal cod_filial As String, _
                                        ByVal cod_sucursal As String) As DataTable

        Const spName = "ido_vta_consolidado_couche"
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
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "ido_vta_item_12meses", Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " ANALISIS VENTA POR FAMILIA  "
    Public Function vta_x_familia(ByVal ano_periodo As Integer, _
                                      ByVal mes_periodo As Integer, _
                                        ByVal cod_filial As String, _
                                        ByVal cod_sucursal As String) As DataTable

        Const spName = "ido_vta_x_familia"
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
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@mes_periodo", SqlDbType.Int).Value = mes_periodo
            spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.VarChar, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = "Error al ejecutar " & spName
                Err.Raise(vbObjectError + 512 + WidthError, spName, Err.Description + " (sp)")
            End If

            daSql.Fill(resultDT)

            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
            resultDT.Dispose()
        End Try

    End Function
#End Region

#Region " ANALISIS CARTERA PROMOTORA "
    Public Function obtieneAnalisisCarteraPromotora(ByVal ano_periodo As Integer, _
                                                    ByVal cod_filial As String, _
                                                    ByVal cod_sucursal As String, _
                                                    ByVal cod_promotora As String, _
                                                    ByVal cod_concepto As String, _
                                                    ByVal cod_familia As String, _
                                                    ByVal cod_subfamilia As String) As DataTable

        Const spName = "ido_get_analisis_promotora"
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
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@ano_periodo", SqlDbType.Int).Value = ano_periodo
            spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_filial", SqlDbType.Char).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_promotora", SqlDbType.Char, 3).Value = cod_promotora
            spCall.Parameters("@cod_promotora").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_concepto", SqlDbType.Char, 6).Value = cod_concepto
            spCall.Parameters("@cod_concepto").Direction = ParameterDirection.Input

            If cod_familia.Length > 0 Then
                spCall.Parameters.Add("@cod_familia", SqlDbType.Char, 3).Value = cod_familia
            Else
                spCall.Parameters.Add("@cod_familia", SqlDbType.Char, 3).Value = DBNull.Value
            End If
            spCall.Parameters("@cod_familia").Direction = ParameterDirection.Input

            If cod_subfamilia.Length > 0 Then
                spCall.Parameters.Add("@cod_subfamilia", SqlDbType.Char, 4).Value = cod_subfamilia
            Else
                spCall.Parameters.Add("@cod_subfamilia", SqlDbType.Char, 4).Value = DBNull.Value
            End If
            spCall.Parameters("@cod_subfamilia").Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "obtieneAnalisisCarteraPromotora", Err.Description + " (sp)")
            End If

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
#End Region

#Region " OBTIENE DE DETALLE PARA ULTIMOS 10 PEDIDOS "

    Public Function pedidoClienteItem(ByVal cod_filial As String, ByVal cod_sucursal As String, _
                                       ByVal cod_cliente As String, ByVal cod_producto As String) As DataTable

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_pedidos_x_cliente_item"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_filial", SqlDbType.Char, 3).Value = cod_filial
            spCall.Parameters("@cod_filial").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_sucursal", SqlDbType.Char, 3).Value = cod_sucursal
            spCall.Parameters("@cod_sucursal").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_cliente", SqlDbType.Char, 10).Value = cod_cliente
            spCall.Parameters("@cod_cliente").Direction = ParameterDirection.Input

            spCall.Parameters.Add("@cod_producto", SqlDbType.Char, 12).Value = cod_producto
            spCall.Parameters("@cod_producto").Direction = ParameterDirection.Input



            spCall.Parameters.Add("@errorMsg", SqlDbType.VarChar, 255)
            spCall.Parameters("@errorMsg").Direction = ParameterDirection.Output
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "pedidoClienteItem", Err.Description + " (sp)")
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

#Region " REPOSICION STOCK BOLIVIA  "

    Public Function ReposicionStockBolivia(ByVal ano_periodo As Integer, ByVal mes_periodo As Integer) As DataTable
        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()

        Const spName = "ido_bolivia_productos_stock"
        Dim spCall As New SqlCommand(spName, dbConn)

        spCall.CommandType = CommandType.StoredProcedure

        'resultado de la llamada al sp
        spCall.Parameters.Add("@resultValue", SqlDbType.Int)
        spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

        'año periodo
        spCall.Parameters.Add("@ano_periodo", ano_periodo)
        spCall.Parameters("@ano_periodo").Direction = ParameterDirection.Input

        'mes periodo para bolivia
        spCall.Parameters.Add("@mes_periodo", mes_periodo)
        spCall.Parameters("@mes_periodo").Direction = ParameterDirection.Input

        'mensaje de error
        spCall.Parameters.Add("@msgError", SqlDbType.Char, 80)
        spCall.Parameters("@msgError").Direction = ParameterDirection.Output

        dbConn.Open()

        Dim myDataAdapter As New SqlDataAdapter
        myDataAdapter.SelectCommand = spCall
        myDataAdapter.SelectCommand.ExecuteNonQuery()

        Dim iResult As Integer
        Dim msgError As String
        Dim myDataTable As New DataTable

        iResult = spCall.Parameters("@resultValue").Value



        If (iResult = 0) Then

            myDataAdapter.Fill(myDataTable)
            ReposicionStockBolivia = myDataTable



        Else
            msgError = spCall.Parameters("@msgError").Value
        End If

        dbConn.Close()
        dbConn = Nothing
        spCall = Nothing
        myDataTable = Nothing
        If (iResult <> 0) Then
            Err.Raise(vbObjectError + 512 + WidthError, "ReposicionStockBolivia()", msgError)
        End If

    End Function
#End Region


End Module



