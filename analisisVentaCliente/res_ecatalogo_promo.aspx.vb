Imports Exportador

Public Class res_ecatalogo_promo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents lbCodPromo As System.Web.UI.WebControls.Label
    Protected WithEvents lbNomPromo As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage

    Dim tUserInfo As usuario.t_Usuario
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btSend As System.Web.UI.HtmlControls.HtmlInputImage
    Protected WithEvents ddlPromotora As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkEcat As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblCartera As System.Web.UI.WebControls.Label
    Protected WithEvents lblEcatalogo As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim fecha_ini As Date
    Dim fecha_fin As Date
    ' Para calculo de totales
    Dim totales(9) As Double
    Dim totRows As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        Dim nom_promotora, cod_promotora As String

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        If Not Page.IsPostBack Then

            ' Prepara los datos de los controles...

            cargaPromotoras(tUserInfo.codigoFilial, tUserInfo.codigoSucursal)

            Dim cl As New System.Globalization.CultureInfo("es-CL")

            fecha_ini = Now().AddDays((Day(Now) * -1) + 1)
            fecha_fin = Now()

            txtIni.Text = String.Format(cl, "{0:d}", fecha_ini)
            txtFin.Text = String.Format(cl, "{0:d}", fecha_fin)

            If tUserInfo.perfil.Trim = P_PROMOTORA Then
                ' USER ES UNA PROMOTORA
                cod_promotora = tUserInfo.codigoPromo
                nom_promotora = tUserInfo.nombre

                ddlPromotora.SelectedValue = cod_promotora
                ddlPromotora.Enabled = False

                lbNota.Visible = True
                lbNota.Text = "* Solo Clientes Cartera"
            End If
        Else

            ' Ejecuta el form...

            lbErrors.Text = ""

            cod_promotora = ddlPromotora.SelectedValue
            If cod_promotora = "" Then
                Err.Description = "Faltan parametros para poder ejecutar la consulta."
                Err.Raise(vbObjectError + 512 + 10, "res_eCatalogo_promo", Err.Description)
            End If

            If cod_promotora = "*" Then
                nom_promotora = "TODAS LAS PROMOTORAS"
            Else
                nom_promotora = Utiles.get_promotora_name(tUserInfo.codigoFilial, tUserInfo.codigoSucursal, cod_promotora)
            End If
            If nom_promotora = "" Then
                Err.Description = "No se encontró ejec. comercial con codigo: " & cod_promotora
                Err.Raise(vbObjectError + 512 + 10, "mar_vta_promo_cliente_acu", Err.Description)
            Else

                Try

                    ' Asigna fechas
                    fecha_ini = txtIni.Text
                    fecha_fin = txtFin.Text

                    tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
                    Dim cod_filial As String = tUserInfo.codigoFilial
                    Dim cod_sucursal As String = tUserInfo.codigoSucursal

                    ' Muestra o no el codigo y nombre de la promotora
                    dgResultado.Columns(0).Visible = (cod_promotora = "*")
                    dgResultado.Columns(1).Visible = (cod_promotora = "*")

                    Dim dtResult As DataTable

                    dtResult = ventas.resumenEcatalogoCarteraPromotora(cod_promotora, nom_promotora, _
                                                fecha_ini, fecha_fin, cod_filial, cod_sucursal)

                    Dim dvResult As DataView = New DataView(dtResult)

                    If chkEcat.Checked Then
                        dvResult.RowFilter = "Es_Ecat=1"
                    End If

                    Session("DGResultado") = dtResult

                    dgResultado.DataSource = dvResult

                    dgResultado.DataBind()

                    lbCodPromo.Text = cod_promotora
                    lbNomPromo.Text = nom_promotora

                    ibExportar.Visible = True

                Catch ex As Exception
                    lbErrors.Text = Err.Description
                    Err.Clear()
                    ' Throw ex
                Finally
                End Try

                'lbFecha.Text = "Entre " & fecha_ini.ToString & " y " & fecha_fin.ToString
            End If
        End If

    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, _
                                            ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound
        Dim i As Int16

        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then

        End If

        ' DG  ITEM CODE
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            If (Not chkEcat.Checked) Or _
                    (chkEcat.Checked And e.Item.Cells(17).Text() = "1") Then

                totRows = totRows + 1

                ' Calcula totales...
                ' Total Vta Dolar eCat
                totales(0) += CDbl(e.Item.Cells(5).Text())
                ' Total Vta Dolar GMS
                totales(1) += CDbl(e.Item.Cells(6).Text())
                ' Total mg dolar ecat
                totales(2) += CDbl(e.Item.Cells(8).Text())
                ' Total mg dolar gms
                totales(3) += CDbl(e.Item.Cells(9).Text())
                ' Total # ped eCat
                totales(4) += CDbl(e.Item.Cells(11).Text())
                ' Total # ped gms
                totales(5) += CDbl(e.Item.Cells(12).Text())
                ' Total saldo epesos
                totales(6) += CDbl(e.Item.Cells(14).Text())
                ' Total # epesos utilizados
                totales(7) += CDbl(e.Item.Cells(15).Text())
                ' Cantidad de Registros que pertenecen a eCatalogo...
                If e.Item.Cells(17).Text = "1" Then
                    totales(8) += 1
                End If

            End If

            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If

            ' *** CODIGO PARA HIGHLIGHT  -END- ******
        End If

        ' DG  FOOTER CODE
        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            ''Muestra totales
            e.Item.Cells(4).Text() = "Totales : "
            e.Item.Cells(5).Text() = String.Format(cl, "{0:N2}", totales(0))
            e.Item.Cells(6).Text() = String.Format(cl, "{0:N2}", totales(1))
            ' Promedio valores anteriores...
            e.Item.Cells(7).Text() = String.Format(cl, "{0:##0.00%}", totales(0) / totales(1))

            e.Item.Cells(8).Text() = String.Format(cl, "{0:N2}", totales(2))
            e.Item.Cells(9).Text() = String.Format(cl, "{0:N2}", totales(3))
            ' Promedio valores anteriores...
            e.Item.Cells(10).Text() = String.Format(cl, "{0:##0.00%}", totales(2) / totales(3))

            e.Item.Cells(11).Text() = String.Format(cl, "{0:N0}", totales(4))
            e.Item.Cells(12).Text() = String.Format(cl, "{0:N0}", totales(5))
            ' Promedio valores anteriores...
            e.Item.Cells(13).Text() = String.Format(cl, "{0:##0.00%}", totales(4) / totales(5))

            e.Item.Cells(14).Text() = String.Format(cl, "{0:N0}", totales(6))
            e.Item.Cells(15).Text() = String.Format(cl, "{0:N0}", totales(7))

            lblCartera.Text = String.Format(cl, "{0:N0}", totRows)
            lblEcatalogo.Text = String.Format(cl, "{0:N0}", totales(8))

        End If

    End Sub

#Region " ORDENAMIENTO DE COLUMNAS DATAGRID "
    Private Sub dgResultado_SortCommand(ByVal source As Object, _
                                        ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) _
                                        Handles dgResultado.SortCommand

        Try
            Dim ColumnToSort As String
            Dim SortExprs() As String
            Dim i As Integer

            ' Limpiamos totales, en caso contrario se duplican
            For i = 0 To 9
                totales(i) = 0
            Next

            SortExprs = Split(e.SortExpression, " ")
            ColumnToSort = SortExprs(0)

            If e.SortExpression.ToLower = Me.SortExpression.ToLower Then
                ' SortAscending = Not SortAscending
                Me.SortExpression = ColumnToSort & " DESC"
            Else
                'SortAscending = True
                Me.SortExpression = ColumnToSort & " ASC"
            End If


            Dim dtResult As DataTable = Session("DGResultado")

            Dim dv As DataView = New DataView(dtResult)
            dv.Sort = Me.SortExpression

            If chkEcat.Checked Then
                dv.RowFilter = "Es_Ecat=1"
            End If

            dgResultado.DataSource = dv
            dgResultado.DataBind()

            ibExportar.Visible = True

        Catch ex As Exception
            lbErrors.Text = "ERRORES EN PAGINA: " & ex.Message
            Err.Clear()
        End Try

    End Sub


    'The Page-level properties that write to ViewState
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

#End Region

#Region " EXPORTACION A EXCELL "
    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click

        If dgResultado.Items.Count > 0 Then

            ' Nueva instancia del Informe
            Dim xlsResultado As Table = CType(dgResultado.Controls(0), System.Web.UI.WebControls.Table)

            Dim sTableHeader As String
            sTableHeader = "Ejec. comercial: " & Me.lbCodPromo.Text.Trim & " - " & Me.lbNomPromo.Text.Trim

            ' Agregar encabezado del informe
            Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

            Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

            ' Configuracion de impresion
            Exportar.ExcelXml.PreserveWhitespace = False
            Exportar.PageScale = 80
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DResumen Venta Cliente - " & lbFecha.Text.Trim)
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Exportar.TableToExcel(xlsResultado)
            Exportar.SaveToClient(Response)
        End If
    End Sub

#End Region

#Region " INICIALIZACION DE CONTROLES FORMULARIO "
    Private Sub cargaPromotoras(ByVal codigoFilial As String, ByVal codigoSucursal As String)
        Dim xDatos As DataTable = Utiles.ObtienePromotoras(codigoFilial)
        With ddlPromotora
            .Visible = True
            .DataSource = xDatos
            .DataTextField = "COL2"
            .DataValueField = "COL1"
            .DataBind()
            .Items.Add(New ListItem("TODAS", "*"))
        End With
    End Sub

#End Region

End Class
