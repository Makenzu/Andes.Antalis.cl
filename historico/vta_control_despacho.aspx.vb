Public Class vta_control_despacho
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents plRango As System.Web.UI.WebControls.Panel
    Protected WithEvents plFactura As System.Web.UI.WebControls.Panel
    Protected WithEvents txFactura As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvFactura As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ibFacturaOff As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibCliRanOff As System.Web.UI.WebControls.ImageButton
    Protected WithEvents cvFechas As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents rfvCliente As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txHasta As System.Web.UI.WebControls.TextBox
    Protected WithEvents txDesde As System.Web.UI.WebControls.TextBox
    Protected WithEvents plItem As System.Web.UI.WebControls.Panel
    Protected WithEvents ibProdOff As System.Web.UI.WebControls.ImageButton
    Protected WithEvents rfvICliente As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ibCliFecOff As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txICodProd As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvIProd As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ibIFacturaOff As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibProdOff1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents rfvFechaHasta As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvFechaIni As System.Web.UI.WebControls.RequiredFieldValidator

    Dim dtResult As New DataTable
    Protected WithEvents txCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents txICliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btAceptarF As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btAceptarI As System.Web.UI.WebControls.ImageButton


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

        lbErrors.Visible = False

        Dim en As New System.Globalization.CultureInfo("en-US")
        If txDesde.Text = "" Then
            txDesde.Text = Date.Now.AddDays(-30).ToString("dd / MM / yyyy", en)
        End If

        If txHasta.Text = "" Then
            txHasta.Text = Date.Now().ToString("dd / MM / yyyy", en)
        End If

    End Sub




    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, _
ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound



        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then
            If plItem.Visible = True Then
                e.Item.Cells(4).Text = "Monto Uni. ($)"
            End If

            'Dim i As Integer
            'Dim imgUp As New System.Web.UI.WebControls.Image
            'imgUp.ImageUrl = "images/sort_2arrows.gif"
            'Dim imgUp2 As New System.Web.UI.WebControls.Image
            'imgUp2.ImageUrl = "images/sort_2arrows.gif"

            'e.Item.Cells(0).Controls.Add(imgUp)
            'e.Item.Cells(2).Controls.Add(imgUp2)

            e.Item.Cells(0).Text = "Fec. Fac."
            e.Item.Cells(2).Text = "N°. Factura"
        End If



        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            ' DG  ITEM CODE
            Dim cl As New System.Globalization.CultureInfo("es-CL")

            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If
            ' *** CODIGO PARA HIGHLIGHT  -END- ******



            If e.Item.Cells(2).Text().Trim() <> "" And e.Item.Cells(2).Text() <> "&nbsp;" Then
                e.Item.Cells(2).Text() = "<a href=""vta_detalle_documento.aspx?ap=" & e.Item.Cells(5).Text & "&mo=" & "PES" & "&mp=" & e.Item.Cells(6).Text & _
                                            "&cf=" & e.Item.Cells(7).Text & "&cs=" & e.Item.Cells(8).Text & _
                                            "&nd=" & e.Item.Cells(2).Text() & "&cd=" & e.Item.Cells(1).Text & """ title=""Ver Detalle"">" & e.Item.Cells(2).Text() & "</a>"
            End If

            'If e.Item.Cells(3).Text().Trim() <> "" And e.Item.Cells(3).Text() <> "&nbsp;" Then
            '    e.Item.Cells(3).Text() = Format(Date.Parse(e.Item.Cells(3).Text()), "dd/MM/yy").Replace("-", "/")
            'End If

            If e.Item.Cells(4).Text().Trim() <> "" And e.Item.Cells(4).Text() <> "&nbsp;" Then
                e.Item.Cells(4).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(4).Text()))
            End If

        End If

        'Use the footer to display the summary row.
        'If e.Item.ItemType = ListItemType.Footer Then
        'End If

    End Sub



    Private Sub ibCliRanOn_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        plFactura.Visible = True
        plRango.Visible = False
        plItem.Visible = False
    End Sub

    Private Sub ibFacturaOff_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibFacturaOff.Click
        plRango.Visible = False
        plFactura.Visible = True
        plItem.Visible = False
    End Sub

    Private Sub ibCliRanOff_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibCliRanOff.Click
        plRango.Visible = True
        plFactura.Visible = False
        plItem.Visible = False
    End Sub



    Private Sub ibCliFecOff_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibCliFecOff.Click
        plRango.Visible = True
        plFactura.Visible = False
        plItem.Visible = False
    End Sub

    Private Sub ibIFacturaOff_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibIFacturaOff.Click
        plRango.Visible = False
        plFactura.Visible = True
        plItem.Visible = False
    End Sub

    Private Sub ibProdOff_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibProdOff.Click
        plRango.Visible = False
        plFactura.Visible = False
        plItem.Visible = True
    End Sub


    Private Sub ibProdOff1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibProdOff1.Click
        plRango.Visible = False
        plFactura.Visible = False
        plItem.Visible = True
    End Sub





    Private Sub btSend_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSend.Click
        Dim cod_cliente As String
        Dim cod_doc As String
        Dim fec_ini, fec_ter As Date
        'Dim dtResult As DataTable


        If Date.Parse(txDesde.Text) > Date.Parse(txHasta.Text) Then
            cvFechas.Visible = True
        Else

            cvFechas.Visible = False
            Try

                cod_cliente = txCliente.Text()
                cod_doc = "FAC"
                fec_ini = txDesde.Text
                fec_ter = txHasta.Text

                Dim tuserinfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

                dtResult = ventas.vta_despacho_doc_cli_fecha(tuserinfo.codigoFilial, tuserinfo.codigoSucursal, cod_doc, cod_cliente, fec_ini, fec_ter)
                dgResultado.DataSource = dtResult
                dgResultado.DataBind()

                'lbNum_Doc.Text = dtResult.Rows(0).Item("Num_Documento")
                'lbFec_Doc.Text = dtResult.Rows(0).Item("Fec_Documento")
                'lbNum_Reparto.Text = dtResult.Rows(0).Item("Num_Reparto")
                'lbFec_Reparto.Text = dtResult.Rows(0).Item("Fec_Reparto")

                'lbNom_Vend.Text = dtResult.Rows(0).Item("Nom_Vendedora") & " (" & dtResult.Rows(0).Item("Cod_Vendedora") & ")"
                'lbNom_Cliente.Text = dtResult.Rows(0).Item("Nom_Cliente")
                'lbCPago.Text = dtResult.Rows(0).Item("Cod_Forma_Pago") & dtResult.Rows(0).Item("Cod_Plazo_Pago")
                'lbMonto_Peso.Text = dtResult.Rows(0).Item("Monto_Peso")
                'lbTipoCambio.Text = dtResult.Rows(0).Item("tipo_cambio")
                'lbFecVenc.Text = dtResult.Rows(0).Item("Fec_Vencimiento")


            Catch ex As Exception
                lbErrors.Text = "ERRORES EN PAGINA: " & Err.Description
                lbErrors.Visible = True
                Err.Clear()
                ' Throw ex
            Finally
                dtResult.Dispose()

            End Try

        End If

    End Sub

    Private Sub btAceptarF_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btAceptarF.Click
        Dim num_docto As String
        num_docto = txFactura.Text.Trim

        Try

            Dim tuserinfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

            dtResult = ventas.vta_despacho_doc_cli_docto(tuserinfo.codigoFilial, tuserinfo.codigoSucursal, num_docto)
            dgResultado.DataSource = dtResult
            dgResultado.DataBind()

        Catch ex As Exception
            lbErrors.Text = "ERROR: " & Err.Description
            lbErrors.Visible = True
            Err.Clear()
            ' Throw ex
        Finally
            dtResult.Dispose()

        End Try

    End Sub

    Private Sub btAceptarI_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btAceptarI.Click


        Dim cod_doc As String
        Dim cod_cliente As String
        Dim cod_producto As String

        Try

            cod_cliente = txICliente.Text.Trim
            cod_doc = "FAC"
            cod_producto = txICodProd.Text.Trim

            Dim tuserinfo As usuario.t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

            dtResult = ventas.vta_despacho_doc_cli_prod(tuserinfo.codigoFilial, tuserinfo.codigoSucursal, cod_doc, cod_cliente, cod_producto)
            dgResultado.DataSource = dtResult
            dgResultado.DataBind()

        Catch ex As Exception
            lbErrors.Text = "ERROR: " & Err.Description
            lbErrors.Visible = True
            Err.Clear()
            ' Throw ex
        Finally
            dtResult.Dispose()

        End Try

    End Sub
End Class
