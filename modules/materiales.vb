Imports System.Xml
Imports System.Data.SqlClient


Module materiales
    Public Structure grp_productos
        Dim cod_producto As String
        Dim cod_reporte As String
        Dim cod_grupo As String
        Dim des_grupo As String
        Dim val_gramos As Double
        Dim val_ancho As Double
        Dim val_largo As Double
        Dim cod_color As String
        Dim cod_corte As String
    End Structure

    Const WidthError = 1

    'A partir del código de sociedad, centro y material, consultamos cantidad de stock contable
    'y pedidos pendientes expresados en unidad base.
    Public Function obtieneStockMateriales(ByVal sociedad As String, ByVal centro As String, ByVal materialTbl As DataTable) As DataTable
        Dim SAPXMLPacket As New XmlDocument
        Dim XMLQuery As XmlElement
        Dim XMLHeader As XmlElement
        Dim XMLDetail As XmlElement

        SAPXMLPacket.PreserveWhitespace = False
        SAPXMLPacket.AppendChild(SAPXMLPacket.CreateProcessingInstruction("xml", "version=" & Chr(34) & "1.0" & Chr(34)))

        XMLQuery = SAPXMLPacket.CreateElement("AndesStockMaterial")
        ' SAPXMLPacket.AppendChild(XMLCotizacion)

        ' Attach Cabecera
        XMLHeader = SAPXMLPacket.CreateElement("cabecera")

        Dim XMLItem As XmlElement
        ' Sociedad
        XMLItem = SAPXMLPacket.CreateElement("sociedad")
        XMLItem.InnerText = sociedad
        XMLHeader.AppendChild(XMLItem)

        ' Centro de distribución
        XMLItem = SAPXMLPacket.CreateElement("centro")
        XMLItem.InnerText = centro
        XMLHeader.AppendChild(XMLItem)

        XMLQuery.AppendChild(XMLHeader)



        ' Attach Detalle
        XMLDetail = SAPXMLPacket.CreateElement("detalle")

        Dim myDataRow As DataRow
        For Each myDataRow In materialTbl.Rows
            ' Material
            XMLItem = SAPXMLPacket.CreateElement("material")
            XMLItem.InnerText = Trim(myDataRow.Item("cod_producto"))
            XMLDetail.AppendChild(XMLItem)
        Next
        XMLQuery.AppendChild(XMLDetail)


        SAPXMLPacket.AppendChild(XMLQuery)

        SAPXMLPacket.Save("C:\query.xml")

        Dim xmlResponse As New XmlDocument

        Dim wsEcatSrv As New cl.gms.ecatalogo.ws.eCatSrv

        ' TRAER DATOS DESDE SAP
        xmlResponse.LoadXml(wsEcatSrv.obtieneStockMaterial2(SAPXMLPacket).OuterXml)

        xmlResponse.Save("C:\response.xml")

        'Traspasamos datos XML a DataTable

        'Creamos estructura DataTable para contener resultados
        Dim dtResult As DataTable = New DataTable
        Dim dtColumn As DataColumn

        dtColumn = New DataColumn
        dtColumn.DataType = System.Type.GetType("System.String")
        dtColumn.ColumnName = "material"
        dtResult.Columns.Add(dtColumn)

        dtColumn = New DataColumn
        dtColumn.DataType = System.Type.GetType("System.Double")
        dtColumn.ColumnName = "stock_pendiente"
        dtResult.Columns.Add(dtColumn)

        dtColumn = New DataColumn
        dtColumn.DataType = System.Type.GetType("System.Decimal")
        dtColumn.ColumnName = "stock_contable"
        dtResult.Columns.Add(dtColumn)

        dtColumn = New DataColumn
        dtColumn.DataType = System.Type.GetType("System.Decimal")
        dtColumn.ColumnName = "stock_consignacion"
        dtResult.Columns.Add(dtColumn)

        dtColumn = New DataColumn
        dtColumn.DataType = System.Type.GetType("System.String")
        dtColumn.ColumnName = "cod_umb"
        dtResult.Columns.Add(dtColumn)


        'Poblamos tabla de resultado
        Dim material, codigoUMB As String
        Dim pedidoPendiente As Double
        Dim stockContable As Double
        Dim stockConsignacion As Double



        Dim xmlEl As XmlElement
        Dim xmlNL As XmlNodeList
        Dim dtRow As DataRow


        xmlEl = xmlResponse.DocumentElement
        xmlEl = xmlEl.FirstChild

        While (Not IsNothing(xmlEl))

            'buscamos tag <detalle>
            If (xmlEl.Name = "detalle") Then

                'buscamos <item>
                XMLItem = xmlEl.FirstChild

                While (Not IsNothing(XMLItem))

                    xmlNL = XMLItem.GetElementsByTagName("material")
                    material = xmlNL.Item(0).InnerText

                    xmlNL = XMLItem.GetElementsByTagName("stock_pendiente")
                    pedidoPendiente = xmlNL.Item(0).InnerText

                    xmlNL = XMLItem.GetElementsByTagName("cod_umb")
                    codigoUMB = xmlNL.Item(0).InnerText

                    xmlNL = XMLItem.GetElementsByTagName("stock_contable")
                    stockContable = xmlNL.Item(0).InnerText

                    xmlNL = XMLItem.GetElementsByTagName("stock_consignacion")
                    stockConsignacion = xmlNL.Item(0).InnerText

                    XMLItem = XMLItem.NextSibling


                    'Creamos y poblamos nueva fila para la tabla de resultado
                    dtRow = dtResult.NewRow
                    dtRow.Item("material") = material
                    dtRow.Item("stock_pendiente") = pedidoPendiente
                    dtRow.Item("stock_contable") = stockContable
                    dtRow.Item("stock_consignacion") = stockConsignacion
                    dtRow.Item("cod_umb") = codigoUMB

                    'Agregamos tabla de resultado
                    dtResult.Rows.Add(dtRow)
                End While
            End If

            xmlEl = xmlEl.NextSibling
        End While

        If xmlResponse.GetElementsByTagName("error").Count > 0 Then
            Err.Raise(1000, Nothing, xmlResponse.OuterXml)
        End If

        Return dtResult
    End Function

    Public Function get_grp_productos(ByVal cod_reporte As String, ByVal cod_filial As String) As DataTable
        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "ido_get_grp_productos"
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable
        Dim msgError As String = ""
        Dim resultSQL As Integer
        Dim spCall As New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int)
            spCall.Parameters("@resultValue").Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_reporte", SqlDbType.VarChar, 10).Value = cod_reporte
            spCall.Parameters.Add("@ano_periodo", DBNull.Value).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", DBNull.Value).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_filial", SqlDbType.VarChar, 3).Value = cod_filial
            spCall.Parameters.Add("@cod_familia", SqlDbType.VarChar, 3).Value = "105"
            spCall.Parameters.Add("@ind_orden", SqlDbType.VarChar, 3).Value = "G"

            daSql.SelectCommand = spCall
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@resultValue").Value
            If resultSQL <> 0 Then
                Err.Description = spCall.Parameters("@errorMsg").Value
                Err.Raise(vbObjectError + 512 + WidthError, "get_grp_productos", Err.Description + " (sp)")
            End If



            If resultDT.Rows.Count <= 0 Then
                Err.Description = "No se encontraron resultados para esta consulta."
                Err.Raise(vbObjectError + 512 + 10, "ido_get_grp_productos", Err.Description)
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

    Public Sub upd_grp_productos(ByVal GrpProductos As grp_productos)
        Const spName = "ido_upd_grp_productos"
        Dim dbConn As New SqlConnection

        Dim spCall As New SqlCommand(spName, dbConn)
        spCall.CommandType = CommandType.StoredProcedure

        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Try
            spCall.Parameters.Add("@result_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_producto", SqlDbType.VarChar, 6).Value = GrpProductos.cod_producto
            spCall.Parameters.Add("@val_gramos", SqlDbType.Float).Value = GrpProductos.val_gramos
            spCall.Parameters.Add("@val_ancho", SqlDbType.Float).Value = GrpProductos.val_ancho
            spCall.Parameters.Add("@val_largo", SqlDbType.Float).Value = GrpProductos.val_largo
            spCall.Parameters.Add("@cod_color", SqlDbType.VarChar, 1).Value = GrpProductos.cod_color
            spCall.Parameters.Add("@cod_corte", IIf(GrpProductos.cod_corte Is Nothing, DBNull.Value, GrpProductos.cod_corte)).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
        End Try
    End Sub

    Public Sub ins_grp_productos(ByVal GrpProductos As grp_productos)
        Const spName = "ido_ins_grp_productos"
        Dim dbConn As New SqlConnection

        Dim spCall As New SqlCommand(spName, dbConn)
        spCall.CommandType = CommandType.StoredProcedure

        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Try
            With GrpProductos
                spCall.Parameters.Add("@result_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

                spCall.Parameters.Add("@cod_producto", SqlDbType.VarChar, 6).Value = GrpProductos.cod_producto
                spCall.Parameters.Add("@cod_reporte", SqlDbType.VarChar, 10).Value = GrpProductos.cod_reporte
                spCall.Parameters.Add("@val_gramos", SqlDbType.Float).Value = GrpProductos.val_gramos
                spCall.Parameters.Add("@val_ancho", SqlDbType.Float).Value = GrpProductos.val_ancho
                spCall.Parameters.Add("@val_largo", SqlDbType.Float).Value = GrpProductos.val_largo
                spCall.Parameters.Add("@cod_color", SqlDbType.VarChar, 1).Value = GrpProductos.cod_color
                spCall.Parameters.Add("@cod_corte", IIf(GrpProductos.cod_corte Is Nothing, DBNull.Value, GrpProductos.cod_corte)).Direction = ParameterDirection.Input
            End With

            spCall.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
        End Try
    End Sub
End Module
