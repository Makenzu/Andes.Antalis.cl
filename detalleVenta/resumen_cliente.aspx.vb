Public Class vta_x_cliente
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Dim tUserInfo As usuario.t_Usuario
    Protected WithEvents txCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvCliente As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents tbProductos As System.Web.UI.WebControls.TextBox
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lbNomPro As System.Web.UI.WebControls.Label
    Protected WithEvents grdOtello As System.Web.UI.WebControls.DataGrid
    Protected WithEvents grdNoComprados As System.Web.UI.WebControls.DataGrid
    Protected WithEvents grdDatosGrales As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents grdContactos As System.Web.UI.WebControls.DataGrid
    Protected WithEvents grdMensajes As System.Web.UI.WebControls.DataGrid
    Protected WithEvents grdCampañas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents grdObjetivos As System.Web.UI.WebControls.DataGrid
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sAreaFamilia As String
    Dim iItem As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '13-09-2010 - AGS: Se eliminan datos de Otello, de acuerdo a lo solicitado por área ventas

        'lbErrors.Text = "Ini: " & Now() & "<p>"
        lbErrors.Text = ""

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)


        btSend.Attributes.Add("onClick", "javascript:Validar();return false;")
        btSend.Attributes.Add("alt", "Consultar datos")
        'btItem.Attributes.Add("alt", "Filtrar por material")
        'btDesc.Attributes.Add("alt", "Filtrar por descripción")

        'Page.Server.ScriptTimeout = 120

        'tblFiltro.Visible = False
        If Not Page.IsPostBack Then
            Me.grdDatosGrales.DataSource = Nothing
            Me.grdNoComprados.DataSource = Nothing
            'Me.grdComprados.DataSource = Nothing
            'Me.grdOtello.DataSource = Nothing
            'Me.grdVentasArea.DataSource = Nothing

            Me.grdDatosGrales.Visible = False
            Me.grdNoComprados.Visible = False
            'Me.grdComprados.Visible = False
            'Me.grdOtello.Visible = False
            'Me.grdVentasArea.Visible = False
            Me.grdCampañas.Visible = False
        Else
            Dim cod_cliente As String
            Dim cod_filial As String
            Dim cod_sucursal As String
            Dim resultDS As New DataSet

            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

            cod_filial = "CHI"  '= tUserInfo.codigoFilial
            cod_sucursal = "001"  '= tUserInfo.codigoSucursal

            Try
                cod_cliente = txCliente.Text

                If cod_cliente = "" Then
                    Err.Description = "Faltan parametros para poder ejecutar la consulta."
                    Err.Raise(vbObjectError + 512 + 10, "vta_x_cliente", Err.Description)
                End If

                'lbErrors.Text &= "Ini BD: " & Now() & "<p>"
                resultDS = DatosGralesParaVendedoras(cod_filial, cod_sucursal, cod_cliente)
                'lbErrors.Text &= "Fin BD: " & Now() & "<p>"
                If resultDS.Tables.Count <= 0 Then
                    Me.grdDatosGrales.DataSource = Nothing
                    Me.grdNoComprados.DataSource = Nothing
                    'Me.grdComprados.DataSource = Nothing
                    'Me.grdOtello.DataSource = Nothing
                    'Me.grdVentasArea.DataSource = Nothing
                    Me.grdContactos.DataSource = Nothing
                    'Me.grdVtaMgMin.DataSource = Nothing
                    'Me.grdVtaMgSeg.DataSource = Nothing
                    Me.grdMensajes.DataSource = Nothing
                    Me.grdObjetivos.DataSource = Nothing
                    'Me.grdVtaFallida.DataSource = Nothing
                    Me.grdCampañas.DataSource = Nothing

                    Me.grdDatosGrales.Visible = False
                    Me.grdNoComprados.Visible = False
                    'Me.grdComprados.Visible = False
                    'Me.grdOtello.Visible = False
                    'Me.grdVentasArea.Visible = False
                    Me.grdContactos.Visible = False
                    'Me.grdVtaMgMin.Visible = False
                    'Me.grdVtaMgSeg.Visible = False
                    Me.grdMensajes.Visible = False
                    Me.grdObjetivos.Visible = False
                    'Me.grdVtaFallida.Visible = False
                    Me.grdCampañas.Visible = False

                    Err.Description = "No se encontraron datos para esta consulta."
                    Err.Raise(vbObjectError + 512 + 10, "pedidoClienteItem", Err.Description)
                Else
                    Me.grdDatosGrales.Visible = True
                    Me.grdDatosGrales.DataSource = resultDS.Tables("DatosCliente")

                    'Me.grdComprados.Visible = True
                    'Me.grdComprados.DataSource = resultDS.Tables("ProdsComprados")

                    Me.grdNoComprados.Visible = True
                    Me.grdNoComprados.DataSource = resultDS.Tables("ProdsNOComprados")

                    'Me.grdOtello.Visible = True
                    'Me.grdOtello.DataSource = resultDS.Tables(3)

                    'Me.grdVentasArea.Visible = True
                    'Me.grdVentasArea.DataSource = resultDS.Tables("VtasAnual")

                    Me.grdContactos.Visible = True
                    Me.grdContactos.DataSource = resultDS.Tables("Contactos")

                    'Me.grdVtaMgMin.Visible = True
                    'Me.grdVtaMgMin.DataSource = resultDS.Tables(5)

                    'Me.grdVtaMgSeg.Visible = True
                    'Me.grdVtaMgSeg.DataSource = resultDS.Tables(6)

                    Me.grdMensajes.Visible = True
                    Me.grdMensajes.DataSource = resultDS.Tables("Mensajes")

                    Me.grdObjetivos.Visible = True
                    Me.grdObjetivos.DataSource = resultDS.Tables("Objetivos")

                    'Me.grdVtaFallida.Visible = True
                    'Me.grdVtaFallida.DataSource = resultDS.Tables("VtaFallida")

                    Me.grdCampañas.Visible = True
                    Me.grdCampañas.DataSource = resultDS.Tables("Campañas")
                End If

                Me.grdDatosGrales.DataBind()
                Me.grdNoComprados.DataBind()
                'Me.grdComprados.DataBind()
                'Me.grdOtello.DataBind()
                'Me.grdVentasArea.DataBind()
                Me.grdContactos.DataBind()
                'Me.grdVtaMgMin.DataBind()
                'Me.grdVtaMgSeg.DataBind()
                Me.grdMensajes.DataBind()
                Me.grdObjetivos.DataBind()
                'Me.grdVtaFallida.DataBind()
                Me.grdCampañas.DataBind()
            Catch ex As Exception
                lbErrors.Text = Err.Description

                lbErrors.Visible = True
                Err.Clear()
            Finally
                'lbErrors.Text &= "Fin: " & Now()

                resultDS.Dispose()
            End Try
        End If
    End Sub

    Private Sub grdNoComprados_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdNoComprados.DataBinding
        iItem = 0
        sAreaFamilia = ""
    End Sub

    Private Sub grdNoComprados_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdNoComprados.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                If sAreaFamilia <> e.Item.Cells(7).Text & e.Item.Cells(9).Text Then
                    sAreaFamilia = e.Item.Cells(7).Text & e.Item.Cells(9).Text
                    iItem += 1
                    Dim dgItem As DataGridItem
                    Dim dgCell As TableCell
                    dgItem = New DataGridItem(0, 0, ListItemType.Header)
                    dgCell = New TableCell
                    dgCell.ColumnSpan = 7
                    dgItem.Cells.Add(dgCell)
                    If e.Item.Cells(7).Text = "" Then
                        dgCell.Text = "Productos recurrentes"
                    Else
                        dgCell.Text = e.Item.Cells(7).Text & " - " & e.Item.Cells(9).Text
                    End If
                    grdNoComprados.Controls(0).Controls.AddAt(e.Item.ItemIndex + iItem, dgItem)
                End If

                'If e.Item.Cells(10).Text = "0" Then
                '    Dim MyImage As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgEstrella"), System.Web.UI.WebControls.Image)
                '    MyImage.Visible = True
                'End If
        End Select
    End Sub

    'Private Sub grdVentasArea_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
    '    If e.Item.Cells(0).Text = "VENTAS" Then
    '        e.Item.Cells(1).Text = CDec(e.Item.Cells(1).Text).ToString("#,##0")
    '        e.Item.Cells(2).Text = CDec(e.Item.Cells(2).Text).ToString("#,##0")
    '        e.Item.Cells(3).Text = CDec(e.Item.Cells(3).Text).ToString("#,##0")
    '        e.Item.Cells(4).Text = CDec(e.Item.Cells(4).Text).ToString("#,##0")
    '        e.Item.Cells(5).Text = CDec(e.Item.Cells(5).Text).ToString("#,##0")
    '    End If
    'End Sub

    'Private Sub grdVtaFallida_DataBinding(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    iItem = 0
    '    sAreaFamilia = ""
    'End Sub

    'Private Sub grdVtaFallida_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
    '    Select Case e.Item.ItemType
    '        Case ListItemType.Item, ListItemType.AlternatingItem
    '            If sAreaFamilia <> e.Item.Cells(8).Text & e.Item.Cells(10).Text Then
    '                sAreaFamilia = e.Item.Cells(8).Text & e.Item.Cells(10).Text
    '                iItem += 1
    '                Dim dgItem As DataGridItem
    '                Dim dgCell As TableCell
    '                dgItem = New DataGridItem(0, 0, ListItemType.Header)
    '                dgCell = New TableCell
    '                dgCell.ColumnSpan = 8
    '                dgItem.Cells.Add(dgCell)
    '                dgCell.Text = e.Item.Cells(8).Text & " - " & e.Item.Cells(10).Text
    '                Me.grdVtaFallida.Controls(0).Controls.AddAt(e.Item.ItemIndex + iItem, dgItem)
    '            End If
    '    End Select
    'End Sub

    'Private Sub grdVtaMgSeg_DataBinding(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    iItem = 0
    '    sAreaFamilia = ""
    'End Sub

    'Private Sub grdVtaMgMin_DataBinding(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    iItem = 0
    '    sAreaFamilia = ""
    'End Sub

    'Private Sub grdVtaMgMin_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
    '    Select Case e.Item.ItemType
    '        Case ListItemType.Item, ListItemType.AlternatingItem
    '            If sAreaFamilia <> e.Item.Cells(8).Text & e.Item.Cells(10).Text Then
    '                sAreaFamilia = e.Item.Cells(8).Text & e.Item.Cells(10).Text
    '                iItem += 1
    '                Dim dgItem As DataGridItem
    '                Dim dgCell As TableCell
    '                dgItem = New DataGridItem(0, 0, ListItemType.Header)
    '                dgCell = New TableCell
    '                dgCell.ColumnSpan = 8
    '                dgItem.Cells.Add(dgCell)
    '                dgCell.Text = e.Item.Cells(8).Text & " - " & e.Item.Cells(10).Text
    '                Me.grdVtaMgMin.Controls(0).Controls.AddAt(e.Item.ItemIndex + iItem, dgItem)
    '            End If
    '            e.Item.Cells(3).Text = CDec(e.Item.Cells(3).Text).ToString("#,##0.0") & "%"
    '            e.Item.Cells(4).Text = CDec(e.Item.Cells(4).Text).ToString("#,##0.0") & "%"
    '    End Select
    'End Sub

    'Private Sub grdVtaMgSeg_DataBinding(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    iItem = 0
    '    sAreaFamilia = ""
    'End Sub

    'Private Sub grdVtaMgSeg_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
    '    Select Case e.Item.ItemType
    '        Case ListItemType.Item, ListItemType.AlternatingItem
    '            If sAreaFamilia <> e.Item.Cells(8).Text & e.Item.Cells(10).Text Then
    '                sAreaFamilia = e.Item.Cells(8).Text & e.Item.Cells(10).Text
    '                iItem += 1
    '                Dim dgItem As DataGridItem
    '                Dim dgCell As TableCell
    '                dgItem = New DataGridItem(0, 0, ListItemType.Header)
    '                dgCell = New TableCell
    '                dgCell.ColumnSpan = 8
    '                dgItem.Cells.Add(dgCell)
    '                dgCell.Text = e.Item.Cells(8).Text & " - " & e.Item.Cells(10).Text
    '                Me.grdVtaMgSeg.Controls(0).Controls.AddAt(e.Item.ItemIndex + iItem, dgItem)
    '            End If
    '            e.Item.Cells(3).Text = CDec(e.Item.Cells(3).Text).ToString("#,##0.0") & "%"
    '            e.Item.Cells(4).Text = CDec(e.Item.Cells(4).Text).ToString("#,##0.0") & "%"
    '    End Select
    'End Sub

    'Private Sub grdComprados_DataBinding(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    iItem = 0
    '    sAreaFamilia = ""
    'End Sub

    'Private Sub grdComprados_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
    '    Select Case e.Item.ItemType
    '        Case ListItemType.Item, ListItemType.AlternatingItem
    '            If sAreaFamilia <> e.Item.Cells(6).Text & e.Item.Cells(8).Text Then
    '                sAreaFamilia = e.Item.Cells(6).Text & e.Item.Cells(8).Text
    '                iItem += 1
    '                Dim dgItem As DataGridItem
    '                Dim dgCell As TableCell
    '                dgItem = New DataGridItem(0, 0, ListItemType.Header)
    '                dgCell = New TableCell
    '                dgCell.ColumnSpan = 6
    '                dgItem.Cells.Add(dgCell)
    '                dgCell.Text = e.Item.Cells(6).Text & " - " & Chr(10) & Chr(13) & e.Item.Cells(8).Text
    '                grdComprados.Controls(0).Controls.AddAt(e.Item.ItemIndex + iItem, dgItem)
    '            End If
    '            'e.Item.Cells(3).Text = CDec(e.Item.Cells(3).Text).ToString("#,##0")
    '            'e.Item.Cells(4).Text = CDec(e.Item.Cells(4).Text).ToString("#,##0")
    '            'e.Item.Cells(5).Text = CDec(e.Item.Cells(5).Text).ToString("#,##0")
    '    End Select
    'End Sub

End Class
