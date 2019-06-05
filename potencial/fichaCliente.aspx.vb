Imports System.Data.SqlClient
Imports System.Data

Public Class potencial_fichacliente
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents pResultadoCliente As System.Web.UI.WebControls.Panel
    Protected WithEvents lRazonSocialCliente As System.Web.UI.WebControls.Label
    Protected WithEvents lDireccionComercial As System.Web.UI.WebControls.Label
    Protected WithEvents lEjecutivaComercial As System.Web.UI.WebControls.Label
    Protected WithEvents lVendedoraVirtual As System.Web.UI.WebControls.Label
    Protected WithEvents lCobrador As System.Web.UI.WebControls.Label
    Protected WithEvents lCelula As System.Web.UI.WebControls.Label
    Protected WithEvents hlBuscarotroCliente As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lRutCliente As System.Web.UI.WebControls.Label
    Protected WithEvents hfCodCliente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents dgSerie As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lVentaClp3meses As System.Web.UI.WebControls.Label
    Protected WithEvents lVentaClp6meses As System.Web.UI.WebControls.Label
    Protected WithEvents lVentaClp12meses As System.Web.UI.WebControls.Label
    Protected WithEvents lVentaGusd3meses As System.Web.UI.WebControls.Label
    Protected WithEvents lVentaGusd6meses As System.Web.UI.WebControls.Label
    Protected WithEvents lVentaGusd12meses As System.Web.UI.WebControls.Label
    Protected WithEvents lVentaMgd3meses As System.Web.UI.WebControls.Label
    Protected WithEvents lVentaMgd6meses As System.Web.UI.WebControls.Label
    Protected WithEvents lVentaMgd12meses As System.Web.UI.WebControls.Label
    Protected WithEvents lMaterialesDistintos3meses As System.Web.UI.WebControls.Label
    Protected WithEvents lMaterialesDistintos6meses As System.Web.UI.WebControls.Label
    Protected WithEvents lMaterialesDistintos12meses As System.Web.UI.WebControls.Label
    Protected WithEvents lPedidoVta3meses As System.Web.UI.WebControls.Label
    Protected WithEvents lPedidoVta6meses As System.Web.UI.WebControls.Label
    Protected WithEvents lPedidoVta12meses As System.Web.UI.WebControls.Label
    Protected WithEvents lCodigoCliente As System.Web.UI.WebControls.Label
    Protected WithEvents tbPotCliGNR As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbPotCliPAP As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbPotCliCVI As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbPotCliPAK As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbPotCliEII As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbPotCliST As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbCapInstGNR As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbCapInstPAP As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbCapInstCVI As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbCapInstPAK As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbCapInstEII As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbCapInstST As System.Web.UI.WebControls.TextBox
    Protected WithEvents lMensajeAccion2 As System.Web.UI.WebControls.Label
    Protected WithEvents pMensajeAccion2 As System.Web.UI.WebControls.Panel
    Protected WithEvents pImgPotCli As System.Web.UI.WebControls.Panel
    Protected WithEvents pImgCapInst As System.Web.UI.WebControls.Panel
    Protected WithEvents tbDotacionPersonal As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlClasificacionAntalis As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Enum eTipoMensaje
        exito
        fallo
        ninguno
    End Enum

   

    Private Sub muestraMensajeAccion2(ByVal mensaje As String, ByVal tipoMensaje As eTipoMensaje)
        lMensajeAccion2.Text = mensaje
        pMensajeAccion2.Visible = True
        Select Case tipoMensaje
            Case eTipoMensaje.exito
                pMensajeAccion2.CssClass = "mensaje-exito"
            Case eTipoMensaje.fallo
                pMensajeAccion2.CssClass = "mensaje-fallo"
            Case eTipoMensaje.ninguno
                pMensajeAccion2.CssClass = ""
        End Select

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        'Chequeamos que el usuario ejecutando la acción tenga acceso a este módulo
        Dim pu As CPermisoUsuario = Session(Constantes.CTE_OBJ_PERMISOS_USUARIO)
        If Not pu.validaPermiso(Constantes.MODULO_POTENCIAL_CLIENTE, CPermisoUsuario.eTipoAccion.lectura) Then
            Response.Redirect("/")
            Return
        End If

        If Page.IsPostBack = False Then

            With ddlClasificacionAntalis
                .DataSource = obtieneClasificacionClientesAntalis()
                .DataTextField = "denominacion2"
                .DataValueField = "cod_clasificacion"
                .DataBind()
                .Items.Add(New ListItem("", ""))
                .Items.FindByValue("").Selected = True
            End With


            tbDotacionPersonal.Attributes.Add("onchange", "actualizaIndicadoresMiscCliente(this)")
            ddlClasificacionAntalis.Attributes.Add("onchange", "actualizaIndicadoresMiscCliente(this)")

            pResultadoCliente.Visible = True
            Dim codigoCliente As String = Request("cc")
            If codigoCliente.Length > 0 Then
                'Tratamos de buscar datos para el cliente
                If buscaClienteyDespliega("GMSC", codigoCliente) Then
                    hfCodCliente.Value = codigoCliente
                    cargaSerieDatos("GMSC", codigoCliente)
                    cargaPromediosVentas("GMSC", codigoCliente)
                    pResultadoCliente.Visible = True
                End If
            End If

        End If
    End Sub

    Private Sub cargaPromediosVentas(ByVal codigoSociedad As String, ByVal codigoCliente As String)

        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "pot_sel_indicadores_cliente"

        Dim daSql As SqlDataAdapter = New SqlDataAdapter
        Dim dtResult As DataTable = New DataTable
        Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codigoCliente).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.Fill(dtResult)

            If spCall.Parameters("@return").Value() >= 1 Then


                For Each dr As DataRow In dtResult.Rows
                    If Trim(dr("ventana")) = "12meses" Then
                        lVentaClp12meses.Text = (CType(dr("val_venta_pesos"), Double) / 12.0).ToString("#,##0")
                        lVentaGusd12meses.Text = (CType(dr("val_venta_dolar"), Double) / 12.0).ToString("#,##0")
                        lVentaMgd12meses.Text = (CType(dr("val_mg_dolar"), Double) / 12.0).ToString("#,##0")
                        lPedidoVta12meses.Text = (CType(dr("cant_facturas"), Double) / 12.0).ToString("#,##0")
                        lMaterialesDistintos12meses.Text = (CType(dr("cant_prod_distintos"), Double) / 12.0).ToString("#,##0")
                    ElseIf Trim(dr("ventana")) = "06meses" Then
                        lVentaClp6meses.Text = (CType(dr("val_venta_pesos"), Double) / 6.0).ToString("#,##0")
                        lVentaGusd6meses.Text = (CType(dr("val_venta_dolar"), Double) / 6.0).ToString("#,##0")
                        lVentaMgd6meses.Text = (CType(dr("val_mg_dolar"), Double) / 6.0).ToString("#,##0")
                        lPedidoVta6meses.Text = (CType(dr("cant_facturas"), Double) / 6.0).ToString("#,##0")
                        lMaterialesDistintos6meses.Text = (CType(dr("cant_prod_distintos"), Double) / 6.0).ToString("#,##0")
                    ElseIf Trim(dr("ventana")) = "03meses" Then
                        lVentaClp3meses.Text = (CType(dr("val_venta_pesos"), Double) / 3.0).ToString("#,##0")
                        lVentaGusd3meses.Text = (CType(dr("val_venta_dolar"), Double) / 3.0).ToString("#,##0")
                        lVentaMgd3meses.Text = (CType(dr("val_mg_dolar"), Double) / 3.0).ToString("#,##0")
                        lPedidoVta3meses.Text = (CType(dr("cant_facturas"), Double) / 3.0).ToString("#,##0")
                        lMaterialesDistintos3meses.Text = (CType(dr("cant_prod_distintos"), Double) / 3.0).ToString("#,##0")
                    End If
                Next

            End If
        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Sub


    Private Function buscaClienteyDespliega(ByVal codigoSociedad As String, ByVal codigoCliente As String) As Boolean
        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "pot_sel_cliente"

        Dim daSql As SqlDataAdapter = New SqlDataAdapter
        Dim dtResult As DataTable = New DataTable
        Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codigoCliente).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

            daSql.Fill(dtResult)


            If dtResult.Rows.Count = 1 Then

                With dtResult.Rows(0)
                    lCodigoCliente.Text = Trim(.Item("cod_cliente"))
                    hfCodCliente.Value = Trim(.Item("cod_cliente"))
                    lRazonSocialCliente.Text = Trim(.Item("nom_cliente"))
                    lRutCliente.Text = Trim(.Item("rut_cliente"))
                    lDireccionComercial.Text = String.Format("{0}<br />{1} - {2}", _
                                                            Trim(.Item("direccion")), _
                                                            Trim(.Item("des_comuna")), _
                                                            Trim(.Item("des_ciudad")))

                    lEjecutivaComercial.Text = String.Format("{0} :: {1}", Trim(.Item("cod_ejecom")), Trim(.Item("nom_ejecom")))
                    lVendedoraVirtual.Text = String.Format("{0} :: {1}", Trim(.Item("cod_venvir")), Trim(.Item("nom_venvir")))
                    lCobrador.Text = String.Format("{0} :: {1}", Trim(.Item("cod_cobrador")), Trim(.Item("nom_cobrador")))
                    lCelula.Text = Trim(.Item("celula"))

                    If Not IsNothing(ddlClasificacionAntalis.Items.FindByValue(Trim(.Item("cod_clasificacion_antalis")))) Then
                        ddlClasificacionAntalis.ClearSelection()
                        ddlClasificacionAntalis.Items.FindByValue(Trim(.Item("cod_clasificacion_antalis"))).Selected = True
                    End If

                    If (.Item("dot_personal") Is DBNull.Value) Then
                        tbDotacionPersonal.Text = ""
                    Else
                        If (CType(.Item("dot_personal"), Integer) >= 0) Then
                            tbDotacionPersonal.Text = .Item("dot_personal")
                        Else
                            tbDotacionPersonal.Text = ""
                        End If
                    End If

                    'Cargamos indicadores de potencial cliente
                    If Not .Item("potcli_gnr") Is DBNull.Value Then
                        tbPotCliGNR.Text = CType(.Item("potcli_gnr"), Double).ToString("#,##0.00")
                    End If

                    If Not .Item("potcli_pap") Is DBNull.Value Then
                        tbPotCliPAP.Text = CType(.Item("potcli_pap"), Double).ToString("#,##0.00")
                    End If

                    If Not .Item("potcli_cvi") Is DBNull.Value Then
                        tbPotCliCVI.Text = CType(.Item("potcli_cvi"), Double).ToString("#,##0.00")
                    End If

                    If Not .Item("potcli_pak") Is DBNull.Value Then
                        tbPotCliPAK.Text = CType(.Item("potcli_pak"), Double).ToString("#,##0.00")
                    End If

                    If Not .Item("potcli_eii") Is DBNull.Value Then
                        tbPotCliEII.Text = CType(.Item("potcli_eii"), Double).ToString("#,##0.00")
                    End If

                    If Not .Item("potcli_st") Is DBNull.Value Then
                        tbPotCliST.Text = CType(.Item("potcli_st"), Double).ToString("#,##0.00")
                    End If


                    'Cargamos indicadores de capacidad instalada
                    If Not .Item("capinst_gnr") Is DBNull.Value Then
                        tbCapInstGNR.Text = CType(.Item("capinst_gnr"), Double).ToString("#,##0.00")
                    End If

                    If Not .Item("capinst_pap") Is DBNull.Value Then
                        tbCapInstPAP.Text = CType(.Item("capinst_pap"), Double).ToString("#,##0.00")
                    End If

                    If Not .Item("capinst_cvi") Is DBNull.Value Then
                        tbCapInstCVI.Text = CType(.Item("capinst_cvi"), Double).ToString("#,##0.00")
                    End If

                    If Not .Item("capinst_pak") Is DBNull.Value Then
                        tbCapInstPAK.Text = CType(.Item("capinst_pak"), Double).ToString("#,##0.00")
                    End If

                    If Not .Item("capinst_eii") Is DBNull.Value Then
                        tbCapInstEII.Text = CType(.Item("capinst_eii"), Double).ToString("#,##0.00")
                    End If

                    If Not .Item("capinst_st") Is DBNull.Value Then
                        tbCapInstST.Text = CType(.Item("capinst_st"), Double).ToString("#,##0.00")
                    End If

                    pImgPotCli.ID = "img_potcli_" & codigoCliente
                    pImgCapInst.ID = "img_capinst_" & codigoCliente

                    tbPotCliGNR.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'potcli', '{0}', 'GNR', this)", codigoCliente))
                    tbPotCliPAP.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'potcli', '{0}', 'PAP', this)", codigoCliente))
                    tbPotCliCVI.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'potcli', '{0}', 'CVI', this)", codigoCliente))
                    tbPotCliPAK.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'potcli', '{0}', 'PAK', this)", codigoCliente))
                    tbPotCliEII.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'potcli', '{0}', 'EII', this)", codigoCliente))
                    tbPotCliST.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'potcli', '{0}', 'ST', this)", codigoCliente))

                    tbCapInstGNR.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'capinst', '{0}', 'GNR', this)", codigoCliente))
                    tbCapInstPAP.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'capinst', '{0}', 'PAP', this)", codigoCliente))
                    tbCapInstCVI.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'capinst', '{0}', 'CVI', this)", codigoCliente))
                    tbCapInstPAK.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'capinst', '{0}', 'PAK', this)", codigoCliente))
                    tbCapInstEII.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'capinst', '{0}', 'EII', this)", codigoCliente))
                    tbCapInstST.Attributes.Add("onchange", String.Format("actualizaIndicadoresVentaCliente('GMSC', 'capinst', '{0}', 'ST', this)", codigoCliente))


                    pResultadoCliente.Visible = True

                    Return True
                End With

            End If

            Return False

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function

    Private Sub cargaSerieDatos(ByVal codigoSociedad As String, ByVal codigoCliente As String)
        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "pot_sel_serie_venta_12meses"

        Dim daSql As SqlDataAdapter = New SqlDataAdapter
        Dim dtResult As DataTable = New DataTable
        Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cliente", codigoCliente).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.Fill(dtResult)


            If spCall.Parameters("@return").Value() >= 1 Then
                Dim anoPeriodo As Integer = dtResult.Rows(0).Item("ano_periodo")
                Dim mesPeriodo As Integer = dtResult.Rows(0).Item("mes_periodo")
                Dim fechaConsulta As DateTime = New DateTime(anoPeriodo, mesPeriodo, 1).AddMonths(-1)
                For i As Integer = 12 To 1 Step -1
                    dgSerie.Columns(0 + i).HeaderText = fechaConsulta.ToString("MMM-yy")
                    fechaConsulta = fechaConsulta.AddMonths(-1)
                Next
                With dgSerie
                    .DataSource = dtResult
                    .DataBind()
                End With

            End If


        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Sub


    Private Function existeClasificacion(ByVal codigoclasificacion As String) As Boolean
        Dim dbConn As New SqlConnection
        dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
        dbConn.Open()

        Const spName = "pot_sel_clasificacion_antalis"

        Dim dtResult As DataTable = New DataTable
        Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)

        Try
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_clacli_antalis", codigoclasificacion).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()

            Return (spCall.Parameters("@return").Value() = 1)

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function

    Private Function obtieneClasificacionClientesAntalis() As DataTable
        Const spName = "pot_sel_clasificacion_antalis_cliente_x_patron"
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
            spCall.Parameters.Add("@query", "%").Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
 
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

    Private Sub ddlClasificacionAntalis_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlClasificacionAntalis.SelectedIndexChanged

    End Sub
End Class
