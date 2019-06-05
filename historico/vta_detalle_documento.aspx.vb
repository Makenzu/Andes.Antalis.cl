Public Class vta_detalle_documento
    Inherits System.Web.UI.Page

    Dim tUserInfo As usuario.t_Usuario
    Protected WithEvents ddlMoneda As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lbNum_Docto_Sap As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lbdiasPago As System.Web.UI.WebControls.Label

    Dim totales(12) As Double
    Dim suma, TotalNeto, TotalMargen, TotalVolumen, TotalCosto As Double
    Dim cl As New System.Globalization.CultureInfo("es-CL")
    Dim fmtpmg, fmtNeto, fmtMG, fmtCto, fmtTotalItem, fmtTotalDocto As String
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents dgDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbNum_Reparto As System.Web.UI.WebControls.Label
    Protected WithEvents lbFec_Reparto As System.Web.UI.WebControls.Label
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents lbFecSalida As System.Web.UI.WebControls.Label
    Protected WithEvents lbHoraSalida As System.Web.UI.WebControls.Label
    Protected WithEvents lbFecLlegada As System.Web.UI.WebControls.Label
    Protected WithEvents lbHoraLlegada As System.Web.UI.WebControls.Label
    Protected WithEvents lbDirDes As System.Web.UI.WebControls.Label
    Protected WithEvents lbVehiculo As System.Web.UI.WebControls.Label
    Protected WithEvents lbTrans As System.Web.UI.WebControls.Label
    Protected WithEvents lbBoleto As System.Web.UI.WebControls.Label
    Protected WithEvents lbValCambio As System.Web.UI.WebControls.Label
    Protected WithEvents lbNum_Doc As System.Web.UI.WebControls.Label
    Protected WithEvents lbFec_Doc As System.Web.UI.WebControls.Label
    Protected WithEvents lbFecVenc As System.Web.UI.WebControls.Label
    Protected WithEvents lbCodCliente As System.Web.UI.WebControls.Label
    Protected WithEvents lbNom_Cliente As System.Web.UI.WebControls.Label
    Protected WithEvents lbCPago As System.Web.UI.WebControls.Label
    Protected WithEvents lbNom_Vend As System.Web.UI.WebControls.Label
    Protected WithEvents lbMonto_Peso As System.Web.UI.WebControls.Label
    Protected WithEvents lbtotal As System.Web.UI.WebControls.Label

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

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        Dim num_doc As Integer
        Dim cod_doc As String
        Dim dtResult As DataTable

        lbErrors.Visible = False

        Try
            If Trim(Request("nd")) = "" Then
                Err.Description = "Faltan parametros para poder ejecutar la consulta."
                Err.Raise(vbObjectError + 512 + 10, "vta_detalle_documento", Err.Description)
            Else
                num_doc = Trim(Request("nd"))
            End If

            ' POR DEFECTO BUSCA FACTURAS
            If Trim(Request("cd")) = "" Then
                cod_doc = "FAC"
            Else
                cod_doc = Trim(Request("cd"))
            End If

            Dim codigoFilial As String = Request("cf")
            Dim codigoSucursal As String = Request("cs")
            Dim anoPeriodo As Integer = Request("ap")
            Dim mesPeriodo As Integer = Request("mp")
            Dim moneda As String = Request("mo")

            If IsNothing(codigoFilial) And IsNothing(codigoSucursal) Then
                Dim tuserinfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)
                codigoFilial = tuserinfo.codigoFilial
                codigoSucursal = tuserinfo.codigoSucursal
            End If

            dtResult = ventas.vta_detalle_documento(anoPeriodo, mesPeriodo, codigoFilial, codigoSucursal, num_doc, cod_doc)

            If dtResult.Rows.Count <= 0 Then

                Err.Description = "No se encontraron documentos."
                Err.Raise(vbObjectError + 512 + 10, "vta_detalle_documento", Err.Description)
            Else

                If Page.IsPostBack = False Then
                    If Request("mo") <> "" Then
                        moneda = Request("mo").Trim
                    End If
                Else
                    moneda = Request("ddlMoneda")
                End If

                If moneda = "PES" Then
                    fmtNeto = "#,##0.00"
                    fmtMG = "#,##0.00"
                    fmtCto = "#,##0.00"
                    fmtpmg = "#,##0.00 %"
                    fmtTotalItem = "#,##0"
                    fmtTotalDocto = "#,##0"

                Else
                    fmtNeto = "#,##0.0000"
                    fmtMG = "#,##0.0000"
                    fmtCto = "#,##0.0000"
                    fmtpmg = "#,##0.00 %"
                    fmtTotalItem = "#,##0.00"
                    fmtTotalDocto = "#,##0.00"

                End If

                If dtResult.Rows(0).Item("Monto_Ori") Is DBNull.Value Then
                    Err.Description = "No se encontraron todos los datos para esta consulta (Monto_Ori)."
                    Err.Raise(vbObjectError + 512 + 10, "vta_detalle_documento", Err.Description)
                ElseIf moneda = "PES" Then
                    lbMonto_Peso.Text = Format(dtResult.Rows(0).Item("Monto_Ori"), fmtTotalDocto)
                Else
                    lbMonto_Peso.Text = Format(dtResult.Rows(0).Item("val_neto_gusd"), fmtTotalDocto)
                End If

                dgDetalle.DataSource = dtResult
                dgDetalle.DataBind()

                If moneda = "DOL" Then
                    ddlMoneda.SelectedIndex = 0
                    dgDetalle.Columns(7).Visible = False
                Else
                    ddlMoneda.SelectedIndex = 1
                    dgDetalle.Columns(7).Visible = True
                End If

                lbNum_Doc.Text = dtResult.Rows(0).Item("Cod_Documento") & " " & dtResult.Rows(0).Item("Num_Documento")
                lbFec_Doc.Text = Replace(dtResult.Rows(0).Item("Fec_Documento"), "-", "/")

                If dtResult.Rows(0).Item("Num_Reparto") Is DBNull.Value Then
                    lbNum_Reparto.Text = "-"
                Else
                    lbNum_Reparto.Text = dtResult.Rows(0).Item("Num_Reparto")
                End If

                If dtResult.Rows(0).Item("Fec_Reparto") Is DBNull.Value Then
                    lbFec_Reparto.Text = "-"
                Else
                    lbFec_Reparto.Text = Replace(dtResult.Rows(0).Item("Fec_Reparto"), "-", "/")
                End If

                lbNom_Vend.Text = dtResult.Rows(0).Item("Nom_Vendedora") & " (" & dtResult.Rows(0).Item("Cod_Vendedora") & ")"
                lbNom_Cliente.Text = dtResult.Rows(0).Item("Nom_Cliente")
                lbCodCliente.Text = dtResult.Rows(0).Item("Cod_Cliente")
                lbCPago.Text = dtResult.Rows(0).Item("Cod_Plazo_Pago") & dtResult.Rows(0).Item("Cod_Forma_Pago")
                lbdiasPago.Text = dtResult.Rows(0).Item("ind_dias_pago") & "Dias"

                Dim codMoneda As String
                codMoneda = Request("ddlMoneda")

                If Not (dtResult.Rows(0).Item("val_cambio") Is DBNull.Value) Then
                    lbValCambio.Text = String.Format(cl, "{0:N2}", dtResult.Rows(0).Item("val_cambio"))
                End If

                If dtResult.Rows(0).Item("Fec_Vencimiento") Is DBNull.Value Then
                    lbFecVenc.Text = "-"
                Else
                    lbFecVenc.Text = Replace(dtResult.Rows(0).Item("Fec_Vencimiento"), "-", "/")
                End If

                If Not dtResult.Rows(0).Item("Direccion_Despacho") Is DBNull.Value Then
                    lbDirDes.Text = dtResult.Rows(0).Item("Direccion_Despacho")
                End If

                If Not dtResult.Rows(0).Item("Cod_Vehiculo") Is DBNull.Value Then
                    lbVehiculo.Text = dtResult.Rows(0).Item("Cod_Vehiculo")
                End If

                If Not dtResult.Rows(0).Item("fecha_sal") Is DBNull.Value Then
                    lbFecSalida.Text = Replace(dtResult.Rows(0).Item("fecha_sal"), "-", "/")
                End If

                If Not dtResult.Rows(0).Item("hora_sal") Is DBNull.Value Then
                    lbHoraSalida.Text = dtResult.Rows(0).Item("hora_sal")
                End If

                If Not dtResult.Rows(0).Item("fec_lle") Is DBNull.Value Then
                    lbFecLlegada.Text = Replace(dtResult.Rows(0).Item("fec_lle"), "-", "/")
                End If

                If Not dtResult.Rows(0).Item("hor_lle") Is DBNull.Value Then
                    lbHoraLlegada.Text = dtResult.Rows(0).Item("hor_lle")
                End If

                If Not dtResult.Rows(0).Item("transporte") Is DBNull.Value Then
                    lbTrans.Text = dtResult.Rows(0).Item("transporte")
                End If

                If Not dtResult.Rows(0).Item("num_boleto") Is DBNull.Value Then
                    lbBoleto.Text = dtResult.Rows(0).Item("num_boleto")
                End If

                'Información de margenes y costo de gestion sólo es visible para
                'perfiles EJECUTIVO
                Dim tUserInfo As usuario.t_Usuario
                tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

                dgDetalle.Columns(5).Visible = (tUserInfo.perfil = "EJEC")
                dgDetalle.Columns(7).Visible = (tUserInfo.perfil = "EJEC")

                If codMoneda = "PES" Then
                    dgDetalle.Columns(8).Visible = (tUserInfo.perfil = "EJEC")
                Else
                    dgDetalle.Columns(8).Visible = False
                End If

                dgDetalle.Columns(9).Visible = (tUserInfo.perfil = "EJEC")

                'Agregamos numero interno SAP para referencias
                If (Not dtResult.Rows(0).Item("num_docto_sap") Is DBNull.Value) Then
                    lbNum_Docto_Sap.Text = "N° FACTURA SAP: " & Trim(dtResult.Rows(0).Item("num_docto_sap"))
                Else
                    lbNum_Docto_Sap.Text = ""
                End If

            End If

        Catch ex As Exception
            lbErrors.Text = "ERROR: " & Err.Description
            lbErrors.Visible = True
            Err.Clear()
            ' Throw ex
        Finally
        End Try
    End Sub
    Private Property SortExpression() As String
        Get
            Dim o As Object = viewstate("SortExpression")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return o.ToString
            End If
        End Get
        Set(ByVal Value As String)
            viewstate("SortExpression") = Value
        End Set
    End Property

    Private Property SortAscending() As Boolean
        Get
            Dim o As Object = viewstate("SortAscending")
            If o Is Nothing Then
                Return True
            Else
                Return Convert.ToBoolean(o)
            End If
        End Get
        Set(ByVal Value As Boolean)
            viewstate("SortAscending") = Value
        End Set
    End Property

    Private Sub dgDetalle_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDetalle.ItemDataBound
        Dim i As Int16
        Dim precio, neto, costo, cantidad As Double



        Dim codigoMoneda As String
        If Page.IsPostBack = False Then
            codigoMoneda = Request("mo")
        Else
            codigoMoneda = Request("ddlMoneda")
        End If

        If e.Item.ItemType = ListItemType.Header Then
            For i = 0 To 11
                totales(i) = 0
            Next

            If codigoMoneda = "DOL" Then
                e.Item.Cells(7).Text = "Costo Gestion"
            Else
                e.Item.Cells(7).Text = "Costo"
            End If
        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If
            ' *** CODIGO PARA HIGHLIGHT  -END- ******
            e.Item.Attributes.Add("title", e.Item.Cells(1).Text().Trim)
            For i = 4 To 15
                totales(i - 4) = totales(i - 4) + Double.Parse(e.Item.Cells(i).Text())
            Next

            cantidad = Double.Parse(e.Item.Cells(2).Text)

            If codigoMoneda = "DOL" Then
                precio = Double.Parse(e.Item.Cells(10).Text)
                costo = Double.Parse(e.Item.Cells(11).Text)
            Else
                precio = Double.Parse(e.Item.Cells(14).Text)
                costo = Double.Parse(e.Item.Cells(15).Text)
            End If



            e.Item.Cells(4).Text = Format(precio, fmtNeto)
            TotalNeto = TotalNeto + (e.Item.Cells(2).Text * e.Item.Cells(4).Text)
            e.Item.Cells(5).Text = Format(precio - costo, fmtMG)
            TotalMargen = TotalMargen + (e.Item.Cells(2).Text * e.Item.Cells(5).Text)
            e.Item.Cells(7).Text = Format(costo, fmtCto)
            TotalVolumen = TotalVolumen + (e.Item.Cells(2).Text * e.Item.Cells(6).Text)
            e.Item.Cells(9).Text = Format((precio - costo) / precio, fmtpmg)
            TotalCosto = TotalCosto + (e.Item.Cells(2).Text * e.Item.Cells(7).Text)
            e.Item.Cells(16).Text = Format(precio * cantidad, fmtTotalItem)
            suma = suma + e.Item.Cells(16).Text

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            If codigoMoneda = "DOL" Then
                neto = totales(6)
                costo = totales(7)
            Else
                neto = totales(10)
                costo = totales(11)
            End If

            e.Item.Cells(0).Text = "Totales : "
            e.Item.Cells(0).Attributes.Add("align", "left")

            e.Item.Cells(4).Text = Format(TotalNeto, fmtNeto)
            e.Item.Cells(5).Text = Format(TotalMargen, fmtMG)
            e.Item.Cells(6).Text = Format(TotalVolumen, "#,##0.0000")
            e.Item.Cells(7).Text = Format(TotalCosto, fmtCto)
            e.Item.Cells(9).Text = Format((TotalNeto - TotalCosto) / TotalNeto, fmtpmg)

            'e.Item.Cells(5).Text = Format(neto - costo, fmtMG)
            'e.Item.Cells(6).Text = Format(totales(2), "#,##0.0000")
            'e.Item.Cells(7).Text = Format(costo, fmtCto)
            'e.Item.Cells(9).Text = Format((neto - costo) / neto, fmtpmg)

            e.Item.Cells(16).Text = Format(suma, fmtTotalItem)

        End If

        'If codigoMoneda = "DOL" Then
        '    If e.Item.ItemType = ListItemType.Footer Then
        '        For i = 4 To 15
        '            e.Item.Cells(i).Text() = Format(totales(i - 4), "#,##0.00")
        '        Next
        '        netoDolar = Double.Parse(e.Item.Cells(10).Text)
        '        costoDolar = Double.Parse(e.Item.Cells(11).Text)
        '        e.Item.Cells(4).Text = Format(netoDolar, "#,##0.0000")
        '        e.Item.Cells(5).Text = Format(netoDolar - costoDolar, "#,##0.0000")
        '        e.Item.Cells(9).Text = Format((netoDolar - costoDolar) / netoDolar * 100, "#,##0.0000")
        '        e.Item.Cells(7).Text = Format(costoDolar, "#,##0.0000")
        '        e.Item.Cells(16).Text = suma
        '    End If
        'Else
        '    If e.Item.ItemType = ListItemType.Footer Then
        '        e.Item.Cells(0).Text = "Totales : "
        '        e.Item.Cells(0).Attributes.Add("align", "left")
        '        For i = 4 To 15
        '            e.Item.Cells(i).Text() = Format(totales(i - 4), "#,##0.00")
        '        Next
        '        e.Item.Cells(16).Text = suma
        '    End If
        'End If

    End Sub

End Class
