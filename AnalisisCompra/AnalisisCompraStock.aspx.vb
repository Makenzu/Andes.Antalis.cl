Imports System.Data
Imports System.Data.SqlClient
Imports Exportador

Public Class AnalisisCompraStock
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents ddlArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents ddlEncargadoSubfamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Imagebutton1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlSubfamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tbMaterial As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Const codigoFilial As String = "CHI"
    Const codigoSociedad As String = "GMSC"
    Dim wsAndes As cl.gms.andes.ws.materiales.materialesSrv = New cl.gms.andes.ws.materiales.materialesSrv

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If


        If Page.IsPostBack = False Then

            Dim usuarioSesion As String = Session(Constantes.CTE_ANDES_USERNAME)
            bConsultar.Attributes.Add("onClick", "$('#contenidoDetalleOC').html('');$('#contenidoDetallePROF').html('');$('#contenidoDetalleFACT').html('')")


            'Poblamos listado de PM's
            cargaEncargadosSubfamilia(usuarioSesion)

            'Validamos si se están pasando parámetros a la URL.
            'Esto se produce cuando estamos ordenando por columnas

            If Request.QueryString.Count > 0 Then

                Dim encargado As String = Request.QueryString("encargado")
                Dim area As String = Request.QueryString("area")
                Dim familia As String = Request.QueryString("familia")
                Dim subFamilia As String = Request.QueryString("subfamilia")
                Dim campo As String = Request.QueryString("campo")
                Dim orden As String = Request.QueryString("orden")
                Dim material As String = Request.QueryString("material")

                'Cargamos Areas
                cargaAreas(codigoSociedad, area, encargado)

                'Cargamos familias
                cargaFamilias(codigoSociedad, area, familia, encargado)

                'cargamos subfamilias
                cargaSubfamilias(codigoSociedad, area, familia, subFamilia, encargado)


                If Not IsNothing(ddlEncargadoSubfamilia.Items.FindByValue(encargado)) Then

                    If Not IsNothing(ddlEncargadoSubfamilia.Items.FindByValue(encargado)) Then
                        ddlEncargadoSubfamilia.ClearSelection()
                        ddlEncargadoSubfamilia.Items.FindByValue(encargado).Selected = True
                    End If
                End If

                If material <> "" Then

                    Dim dsMaterial As DataSet = wsAndes.obtieneMaterial("CHI", material)
                    If dsMaterial.Tables.Count = 1 Then
                        If dsMaterial.Tables(0).Rows.Count = 1 Then
                            With dsMaterial.Tables(0).Rows(0)

                                tbMaterial.Text = String.Format("{0} :: {1}", material, Trim(.Item("des_producto")))

                                encargado = Trim(.Item("encargado"))
                                area = Trim(.Item("cod_area"))
                                familia = Trim(.Item("cod_familia"))
                                subFamilia = Trim(.Item("cod_subfamilia"))

                                cargaEncargadosSubfamilia(encargado)
                                cargaAreas(codigoSociedad, area, encargado)
                                cargaFamilias(codigoSociedad, area, familia, encargado)
                                cargaSubfamilias(codigoSociedad, area, familia, subFamilia, encargado)
                            End With
                        End If
                    End If
                Else
                    'Cargamos jerarquía según selección anterior
                    cargaAreas(codigoSociedad, area, encargado)
                    cargaFamilias(codigoSociedad, area, familia, encargado)
                    cargaSubfamilias(codigoSociedad, area, familia, subFamilia, encargado)

                End If



                dibujaTablaHtml("GMSC", encargado, area, familia, subFamilia, campo, orden, material)

                Imagebutton1.Visible = True
            Else
                'Cargamos areas
                cargaAreas(codigoSociedad, "", ddlEncargadoSubfamilia.SelectedValue)

                'Cargamos familias
                cargaFamilias(codigoSociedad, ddlArea.SelectedValue, "", ddlEncargadoSubfamilia.SelectedValue)

                'cargamos subfamilias
                cargaSubfamilias(codigoSociedad, ddlArea.SelectedValue, ddlFamilia.SelectedValue, "", ddlEncargadoSubfamilia.SelectedValue)

            End If

        End If
        
        
        

    End Sub

    Private Function obtieneAnalisisCompra(ByVal codigoSociedad As String, _
                                            ByVal encargado As String, _
                                            ByVal codigoArea As String, _
                                            ByVal codigoFamilia As String, _
                                            ByVal codigoSubfamilia As String, _
                                            ByVal campo As String, _
                                            ByVal orden As String, _
                                            ByVal material As String) As DataTable

        Const Spname = "ido_sel_analisis_stock_compras"
        Dim dbConn As SqlConnection = New SqlConnection
        Dim spCall As SqlCommand = New SqlCommand(Spname, dbConn)
        Dim daSql As SqlDataAdapter = New SqlDataAdapter

        Try
            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()
            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@encargado", encargado).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_area", codigoArea).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_familia", codigoFamilia).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_subfamilia", codigoSubfamilia).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@campo", campo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@orden", orden).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_material", material).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

            Dim resultDT As DataTable = New DataTable
            daSql.Fill(resultDT)
            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
        End Try

    End Function

    Private Sub bConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConsultar.Click
        Dim codigoArea As String = ddlArea.SelectedValue
        Dim codigoFamilia As String = ddlFamilia.SelectedValue
        Dim codigoSubfamilia As String = ddlSubfamilia.SelectedValue
        Dim codigoEncargado As String = ddlEncargadoSubfamilia.SelectedValue
        Dim material As String = "*"

        If tbMaterial.Text.Trim <> "" Then
            Dim arrStr() As String = Split(tbMaterial.Text, "::")
            material = arrStr(0).Trim


            Dim dsMaterial As DataSet = wsAndes.obtieneMaterial("CHI", material)
            If dsMaterial.Tables.Count = 1 Then
                If dsMaterial.Tables(0).Rows.Count = 1 Then
                    With dsMaterial.Tables(0).Rows(0)

                        tbMaterial.Text = String.Format("{0} :: {1}", material, Trim(.Item("des_producto")))

                        codigoEncargado = Trim(.Item("encargado"))
                        codigoArea = Trim(.Item("cod_area"))
                        codigoFamilia = Trim(.Item("cod_familia"))
                        codigoSubfamilia = Trim(.Item("cod_subfamilia"))

                        cargaEncargadosSubfamilia(codigoEncargado)
                        cargaAreas(codigoSociedad, codigoArea, codigoEncargado)
                        cargaFamilias(codigoSociedad, codigoArea, codigoFamilia, codigoEncargado)
                        cargaSubfamilias(codigoSociedad, codigoArea, codigoFamilia, codigoSubfamilia, codigoEncargado)
                    End With
                End If
            End If
        End If

        dibujaTablaHtml(codigoSociedad, codigoEncargado, codigoArea, codigoFamilia, codigoSubfamilia, "cod_producto", "DESC", material)
        Imagebutton1.Visible = True
    End Sub

    Private Sub ddlFamilia_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFamilia.SelectedIndexChanged
        tbMaterial.Text = ""
        cargaSubfamilias(codigoSociedad, ddlArea.SelectedValue, ddlFamilia.SelectedValue, "", ddlEncargadoSubfamilia.SelectedValue)

    End Sub

    Private Sub ddlArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
        ddlFamilia.Items.Clear()
        ddlSubfamilia.Items.Clear()
        tbMaterial.Text = ""
        cargaFamilias(codigoSociedad, ddlArea.SelectedValue, "", ddlEncargadoSubfamilia.SelectedValue)
        cargaSubfamilias(codigoSociedad, ddlArea.SelectedValue, "", "", ddlEncargadoSubfamilia.SelectedValue)

    End Sub

    Private Sub cargaAreas(ByVal codigoSociedad As String, _
                            ByVal valorInicialArea As String, _
                            ByVal encargado As String)

        'Cargamos areas
        With ddlArea
            .DataSource = wsAndes.obtieneAreasProdManager(codigoSociedad, encargado)
            .DataValueField = "cod_area"
            .DataTextField = "cod_area"
            .DataBind()
            .Items.Add(New ListItem("- Todas - ", "*"))
        End With

        ddlArea.ClearSelection()

        If Not IsNothing(ddlArea.Items.FindByValue(valorInicialArea)) Then
            ddlArea.Items.FindByValue(valorInicialArea).Selected = True
        Else
            ddlArea.Items.FindByValue("*").Selected = True
        End If
    End Sub

    Private Sub cargaFamilias(ByVal codigoSociedad As String, _
                                ByVal codigoArea As String, _
                                ByVal valorInicialFamilia As String, _
                                ByVal encargado As String)
        'Cargamos familias
        With ddlFamilia
            .DataSource = wsAndes.obtieneFamiliasProdManager(codigoSociedad, codigoArea, encargado)
            .DataValueField = "cod_familia"
            .DataTextField = "cod_familia"
            .DataBind()
            ddlFamilia.Items.Add(New ListItem("- Todos - ", "*"))
        End With

        ddlFamilia.ClearSelection()

        If Not IsNothing(ddlFamilia.Items.FindByValue(valorInicialFamilia)) Then
            ddlFamilia.Items.FindByValue(valorInicialFamilia).Selected = True
        Else
            ddlFamilia.Items.FindByValue("*").Selected = True
        End If
    End Sub

    Private Sub cargaSubfamilias(ByVal codigoSucursal As String, _
                                ByVal codigoArea As String, _
                                ByVal codigoFamilia As String, _
                                ByVal valorInicialSubfamilia As String, _
                                ByVal encargado As String)
        'Cargamos subfamilias
        With ddlSubfamilia
            .DataSource = wsAndes.obtieneSubFamiliasProdManager(codigoSociedad, codigoArea, codigoFamilia, encargado)
            .DataValueField = "cod_subfamilia"
            .DataTextField = "cod_subfamilia"
            .DataBind()
            ddlSubfamilia.Items.Add(New ListItem("- Todos - ", "*"))
        End With

        ddlSubfamilia.ClearSelection()

        If Not IsNothing(ddlSubfamilia.Items.FindByValue(valorInicialSubfamilia)) Then
            ddlSubfamilia.Items.FindByValue(valorInicialSubfamilia).Selected = True
        Else
            ddlSubfamilia.Items.FindByValue("*").Selected = True
        End If

    End Sub
    Private Sub Imagebutton1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebutton1.Click

        Dim codigoArea As String = ddlArea.SelectedValue
        Dim codigoFamilia As String = ddlFamilia.SelectedValue
        Dim codigoSubFamilia As String = ddlSubfamilia.SelectedValue
        Dim encargado As String = ddlEncargadoSubfamilia.SelectedValue
        Dim material As String = "*"

        If tbMaterial.Text <> "" Then
            Dim arrStr() As String = Split(tbMaterial.Text, "::")
            material = arrStr(0).Trim
        End If

        Dim dtResult As DataTable = obtieneAnalisisCompra(codigoSociedad, _
                                                            encargado, _
                                                            codigoArea, _
                                                            codigoFamilia, _
                                                            codigoSubFamilia, _
                                                            "cod_producto", _
                                                            "DESC", _
                                                            material)

        If dtResult.Rows.Count > 0 Then
            Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))
            ' Configuracion de impresion
            Exportar.PageScale = 86
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = ""
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            Exportar.TableToExcel(preparaTablaParaExportar(dtResult))
            Exportar.SaveToClient(Response)


            Exportar = Nothing

        End If
    End Sub
    Private Function preparaTablaParaExportar(ByVal dtDatos As DataTable) As Table
        Dim tDatos As Table = New Table

        tDatos.CellSpacing = 3

        Dim tFila As TableRow = New TableRow

        For i As Integer = 0 To 24
            Dim tCelda As TableCell = New TableCell
            tFila.Cells.Add(tCelda)
        Next


        'Creamos la fila para contener campos de cabecera
        tDatos.Rows.Add(tFila)


        tDatos.Rows(0).Cells(0).Text = "Código"
        tDatos.Rows(0).Cells(0).RowSpan = 2

        tDatos.Rows(0).Cells(1).Text = "Descripción"
        tDatos.Rows(0).Cells(1).RowSpan = 2

        tDatos.Rows(0).Cells(2).Text = "UMB"
        tDatos.Rows(0).Cells(2).RowSpan = 2
        tDatos.Rows(0).Cells(3).Text = "DII"
        tDatos.Rows(0).Cells(3).RowSpan = 2

        tDatos.Rows(0).Cells(4).Text = "Stock"
        tDatos.Rows(0).Cells(4).RowSpan = 2

        tDatos.Rows(0).Cells(5).Text = "Total"
        tDatos.Rows(0).Cells(5).RowSpan = 2

        tDatos.Rows(0).Cells(6).Text = "OC"
        tDatos.Rows(0).Cells(6).RowSpan = 2

        tDatos.Rows(0).Cells(7).Text = "Prof"
        tDatos.Rows(0).Cells(7).RowSpan = 2

        tDatos.Rows(0).Cells(8).Text = "Fact"
        tDatos.Rows(0).Cells(8).RowSpan = 2

        tDatos.Rows(0).Cells(9).Text = "Prom 3M"
        tDatos.Rows(0).Cells(9).RowSpan = 2

        tDatos.Rows(0).Cells(10).Text = "Prom 6M"
        tDatos.Rows(0).Cells(10).RowSpan = 2

        tDatos.Rows(0).Cells(11).Text = "Prom 12M"
        tDatos.Rows(0).Cells(11).RowSpan = 2

        tDatos.Rows(0).Cells(12).Text = "Concepto"
        tDatos.Rows(0).Cells(12).RowSpan = 2

        tDatos.Rows(0).Cells(13).Text = Now().AddMonths(-11).ToString("MMM-yy")
        tDatos.Rows(0).Cells(13).RowSpan = 2

        tDatos.Rows(0).Cells(14).Text = Now().AddMonths(-10).ToString("MMM-yy")
        tDatos.Rows(0).Cells(14).RowSpan = 2

        tDatos.Rows(0).Cells(15).Text = Now().AddMonths(-9).ToString("MMM-yy")
        tDatos.Rows(0).Cells(15).RowSpan = 2

        tDatos.Rows(0).Cells(16).Text = Now().AddMonths(-8).ToString("MMM-yy")
        tDatos.Rows(0).Cells(16).RowSpan = 2

        tDatos.Rows(0).Cells(17).Text = Now().AddMonths(-7).ToString("MMM-yy")
        tDatos.Rows(0).Cells(17).RowSpan = 2

        tDatos.Rows(0).Cells(18).Text = Now().AddMonths(-6).ToString("MMM-yy")
        tDatos.Rows(0).Cells(18).RowSpan = 2

        tDatos.Rows(0).Cells(19).Text = Now().AddMonths(-5).ToString("MMM-yy")
        tDatos.Rows(0).Cells(19).RowSpan = 2


        tDatos.Rows(0).Cells(20).Text = Now().AddMonths(-4).ToString("MMM-yy")
        tDatos.Rows(0).Cells(20).RowSpan = 2


        tDatos.Rows(0).Cells(21).Text = Now().AddMonths(-3).ToString("MMM-yy")
        tDatos.Rows(0).Cells(21).RowSpan = 2


        tDatos.Rows(0).Cells(22).Text = Now().AddMonths(-2).ToString("MMM-yy")
        tDatos.Rows(0).Cells(22).RowSpan = 2

        tDatos.Rows(0).Cells(23).Text = Now().AddMonths(-1).ToString("MMM-yy")
        tDatos.Rows(0).Cells(23).RowSpan = 2

        tDatos.Rows(0).Cells(24).Text = Now().ToString("MMM-yy")
        tDatos.Rows(0).Cells(24).RowSpan = 2

        Dim primeraOcurrenciaCodigo As Boolean = False
        Dim ultimoCodigo As String = ""

        For Each x As DataRow In dtDatos.Rows

            tDatos.Rows.Add(New TableRow)

            For i As Integer = 0 To 24
                tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
            Next

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = Trim(x.Item("cod_producto"))
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).CssClass = "acs-t1"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = Trim(x.Item("des_producto"))
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).CssClass = "acs-t2"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Wrap = False
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = x.Item("cod_umb")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).CssClass = "acs-t3"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = CType(x.Item("dii"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).CssClass = "acs-t3"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = CType(x.Item("stock_contable"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).CssClass = "acs-t3"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = CType(x.Item("tot_pedidos"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).CssClass = "acs-t3"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Text = CType(x.Item("oc_pedidos"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).CssClass = "acs-t3"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Text = CType(x.Item("prof_pedidos"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).CssClass = "acs-t3"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Text = CType(x.Item("trans_pedidos"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).CssClass = "acs-t3"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).Text = CType(x.Item("prom_vta_3m"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).CssClass = "acs-t3"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).Text = CType(x.Item("prom_vta_6m"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).CssClass = "acs-t3"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).Text = CType(x.Item("prom_vta_12m"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).RowSpan = 2
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).CssClass = "acs-t3"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(12).Text = x.Item("concepto")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(12).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(13).Text = CType(x.Item("mes_11"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(13).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(14).Text = CType(x.Item("mes_10"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(14).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(15).Text = CType(x.Item("mes_09"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(15).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(16).Text = CType(x.Item("mes_08"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(16).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(17).Text = CType(x.Item("mes_07"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(17).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(18).Text = CType(x.Item("mes_06"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(18).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(19).Text = CType(x.Item("mes_05"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(19).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(20).Text = CType(x.Item("mes_04"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(20).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(21).Text = CType(x.Item("mes_03"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(21).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(22).Text = CType(x.Item("mes_02"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(22).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(23).Text = CType(x.Item("mes_01"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(23).CssClass = "acs-t6"
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(24).Text = CType(x.Item("mes_00"), Double).ToString("#,#0")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(24).CssClass = "acs-t6"

        Next

        Return tDatos

    End Function

    Private Function dibujaTablaHtml(ByVal codigoSociedad As String, _
                                ByVal encargado As String, _
                                ByVal codigoArea As String, _
                                ByVal codigoFamilia As String, _
                                ByVal codigoSubfamilia As String, _
                                ByVal campo As String, _
                                ByVal orden As String, _
                                ByVal material As String) As Table

        Dim tDatos As Table = New Table

        tDatos.BorderWidth = New Unit(0, UnitType.Pixel)
        tDatos.CellSpacing = 1
        tDatos.CssClass = "acs-reporte"

        Dim tFila As TableRow = New TableRow
        tFila.CssClass = "acs-cabecera"

        For i As Integer = 0 To 19
            Dim tCelda As TableCell = New TableCell
            tFila.Cells.Add(tCelda)
        Next

        Dim hlCampoCabecera As HyperLink
        Dim ordenURL As String

        If orden = "ASC" Then
            ordenURL = "DESC"
        Else
            ordenURL = "ASC"
        End If


        'Creamos la fila para contener campos de cabecera
        tDatos.Rows.Add(tFila)


        'Creamos campos de cabecera
        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = "CODIGO"
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "cod_producto", ordenURL, material)
        tDatos.Rows(0).Cells(0).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(0).RowSpan = 2
        tDatos.Rows(0).Cells(0).CssClass = "acs-cabecera-codigo"


        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = "DESCRIPCION"
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "des_producto", ordenURL, material)
        tDatos.Rows(0).Cells(1).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(1).RowSpan = 2
        tDatos.Rows(0).Cells(1).CssClass = "acs-cabecera-descripcion"

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = "UMB"
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "cod_umb", ordenURL, material)
        tDatos.Rows(0).Cells(2).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(2).RowSpan = 2
        tDatos.Rows(0).Cells(2).CssClass = "acs-cabecera-umb"


        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = "DII"
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "dii", ordenURL, material)
        tDatos.Rows(0).Cells(3).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(3).RowSpan = 2
        tDatos.Rows(0).Cells(3).CssClass = "acs-cabecera-dii"

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = "STOCK"
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "stock_contable", ordenURL, material)
        tDatos.Rows(0).Cells(4).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(4).RowSpan = 2
        tDatos.Rows(0).Cells(4).CssClass = "acs-cabecera-stock"


        tDatos.Rows(0).Cells(5).Text = "PEDIDOS"
        tDatos.Rows(0).Cells(5).ColumnSpan = 7
        tDatos.Rows(0).Cells(5).CssClass = "acs-cabecera-pedidos"
        tDatos.Rows(0).Cells(6).Text = "PROMEDIOS DE VTA"
        tDatos.Rows(0).Cells(6).ColumnSpan = 3
        tDatos.Rows(0).Cells(6).CssClass = "acs-cabecera-promedios"
        tDatos.Rows(0).Cells(7).Text = "CONCEPTO"
        tDatos.Rows(0).Cells(7).RowSpan = 2
        tDatos.Rows(0).Cells(7).CssClass = "acs-cabecera-concepto"

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-11).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(8).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(8).Wrap = False
        tDatos.Rows(0).Cells(8).RowSpan = 2

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-10).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(9).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(9).Wrap = False
        tDatos.Rows(0).Cells(9).RowSpan = 2

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-9).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(10).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(10).Wrap = False
        tDatos.Rows(0).Cells(10).RowSpan = 2

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-8).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(11).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(11).Wrap = False
        tDatos.Rows(0).Cells(11).RowSpan = 2

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-7).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(12).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(12).Wrap = False
        tDatos.Rows(0).Cells(12).RowSpan = 2


        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-6).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(13).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(13).Wrap = False
        tDatos.Rows(0).Cells(13).RowSpan = 2

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-5).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(14).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(14).Wrap = False
        tDatos.Rows(0).Cells(14).RowSpan = 2

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-4).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(15).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(15).Wrap = False
        tDatos.Rows(0).Cells(15).RowSpan = 2

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-3).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(16).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(16).Wrap = False
        tDatos.Rows(0).Cells(16).RowSpan = 2

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-2).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(17).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(17).Wrap = False
        tDatos.Rows(0).Cells(17).RowSpan = 2

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().AddMonths(-1).ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(18).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(18).Wrap = False
        tDatos.Rows(0).Cells(18).RowSpan = 2

        hlCampoCabecera = New HyperLink
        hlCampoCabecera.Text = Now().ToString("MMM-yy").ToUpper
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        hlCampoCabecera.NavigateUrl = ""
        tDatos.Rows(0).Cells(19).Controls.Add(hlCampoCabecera)
        tDatos.Rows(0).Cells(19).Wrap = False
        tDatos.Rows(0).Cells(19).RowSpan = 2


        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(1).CssClass = "acs-subcabecera"
        hlCampoCabecera.CssClass = "hl-acs-cabecera"
        tDatos.Rows(1).Cells.Add(New TableCell)
        tDatos.Rows(1).Cells.Add(New TableCell)
        tDatos.Rows(1).Cells.Add(New TableCell)
        tDatos.Rows(1).Cells.Add(New TableCell)
        tDatos.Rows(1).Cells.Add(New TableCell)
        tDatos.Rows(1).Cells.Add(New TableCell)
        tDatos.Rows(1).Cells.Add(New TableCell)
        tDatos.Rows(1).Cells.Add(New TableCell)
        tDatos.Rows(1).Cells.Add(New TableCell)
        tDatos.Rows(1).Cells.Add(New TableCell)

        Dim hlCampoSubcabecera As HyperLink

        hlCampoSubcabecera = New HyperLink
        hlCampoSubcabecera.Text = "Total"
        hlCampoSubcabecera.CssClass = "hl-acs-cabecera"
        hlCampoSubcabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "tot_pedidos", ordenURL, material)
        tDatos.Rows(1).Cells(0).Controls.Add(hlCampoSubcabecera)
        tDatos.Rows(1).Cells(0).CssClass = "acs-cabecera-total"

        hlCampoSubcabecera = New HyperLink
        hlCampoSubcabecera.Text = "OC"
        hlCampoSubcabecera.CssClass = "hl-acs-cabecera"
        hlCampoSubcabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "oc_pedidos", ordenURL, material)
        tDatos.Rows(1).Cells(1).Controls.Add(hlCampoSubcabecera)
        tDatos.Rows(1).Cells(1).CssClass = "acs-cabecera-oc"

        hlCampoSubcabecera = New HyperLink
        hlCampoSubcabecera.Text = "Prof"
        hlCampoSubcabecera.CssClass = "hl-acs-cabecera"
        hlCampoSubcabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "prof_pedidos", ordenURL, material)
        tDatos.Rows(1).Cells(2).Controls.Add(hlCampoSubcabecera)
        tDatos.Rows(1).Cells(2).CssClass = "acs-cabecera-prof"

        'Agregamos encabezado para ordenes en factura
        hlCampoSubcabecera = New HyperLink
        hlCampoSubcabecera.Text = "Fact"
        hlCampoSubcabecera.CssClass = "hl-acs-cabecera"
        hlCampoSubcabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "trans_pedidos", ordenURL, material)
        tDatos.Rows(1).Cells(3).Controls.Add(hlCampoSubcabecera)
        tDatos.Rows(1).Cells(3).CssClass = "acs-cabecera-fact"


        'Agregamos encabezado para ordenes en proceso de recepción
        hlCampoSubcabecera = New HyperLink
        hlCampoSubcabecera.Text = "En proceso<br />recepción"
        hlCampoSubcabecera.CssClass = "hl-acs-cabecera"
        hlCampoSubcabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "en_proc_recep", ordenURL, material)
        tDatos.Rows(1).Cells(4).Controls.Add(hlCampoSubcabecera)
        tDatos.Rows(1).Cells(4).CssClass = "acs-cabecera-en-proc-recep"

        'Agregamos encabezado para cant prox recepción
        hlCampoSubcabecera = New HyperLink
        hlCampoSubcabecera.Text = "Cant Prox.<br />recepción"
        hlCampoSubcabecera.CssClass = "hl-acs-cabecera"
        hlCampoSubcabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "cant_prox_recep", ordenURL, material)
        tDatos.Rows(1).Cells(5).Controls.Add(hlCampoSubcabecera)
        tDatos.Rows(1).Cells(5).CssClass = "acs-cabecera-cant-prox-recep"


        'Agregamos encabezado para fec prox recepción
        hlCampoSubcabecera = New HyperLink
        hlCampoSubcabecera.Text = "Fec. Prox.<br />recepción"
        hlCampoSubcabecera.CssClass = "hl-acs-cabecera"
        hlCampoSubcabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "cant_prox_recep", ordenURL, material)
        tDatos.Rows(1).Cells(6).Controls.Add(hlCampoSubcabecera)
        tDatos.Rows(1).Cells(6).CssClass = "acs-cabecera-fec_prox_recep"









        hlCampoSubcabecera = New HyperLink
        hlCampoSubcabecera.Text = "3M"
        hlCampoSubcabecera.CssClass = "hl-acs-cabecera"
        hlCampoSubcabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "prom_vta_3m", ordenURL, material)
        tDatos.Rows(1).Cells(7).Controls.Add(hlCampoSubcabecera)
        tDatos.Rows(1).Cells(7).CssClass = "acs-cabecera-p3m"

        hlCampoSubcabecera = New HyperLink
        hlCampoSubcabecera.Text = "6M"
        hlCampoSubcabecera.CssClass = "hl-acs-cabecera"
        hlCampoSubcabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "prom_vta_6m", ordenURL, material)
        tDatos.Rows(1).Cells(8).Controls.Add(hlCampoSubcabecera)
        tDatos.Rows(1).Cells(8).CssClass = "acs-cabecera-p6m"

        hlCampoSubcabecera = New HyperLink
        hlCampoSubcabecera.Text = "12M"
        hlCampoSubcabecera.CssClass = "hl-acs-cabecera"
        hlCampoSubcabecera.NavigateUrl = String.Format("/AnalisisCompra/AnalisisCompraStock.aspx?encargado={0}&area={1}&familia={2}&subfamilia={3}&campo={4}&orden={5}&material={6}", _
                                        encargado, codigoArea, codigoFamilia, codigoSubfamilia, "prom_vta_12m", ordenURL, material)
        tDatos.Rows(1).Cells(9).Controls.Add(hlCampoSubcabecera)
        tDatos.Rows(1).Cells(9).CssClass = "acs-cabecera-p12m"

        '--------------------------------------------------
        'Consultamos los datos a desplegar en pantalla
        '--------------------------------------------------
        Dim tb As DataTable = obtieneAnalisisCompra(codigoSociedad, _
                                                    encargado, _
                                                    codigoArea, _
                                                    codigoFamilia, _
                                                    codigoSubfamilia, _
                                                    campo, _
                                                    orden, _
                                                    material)

        Dim primeraOcurrenciaCodigo As Boolean = False
        Dim ultimoCodigo As String = ""


        For Each x As DataRow In tb.Rows

            If ultimoCodigo <> x.Item("cod_producto") Then
                primeraOcurrenciaCodigo = True
                ultimoCodigo = x.Item("cod_producto")
            Else
                primeraOcurrenciaCodigo = False
            End If

            tDatos.Rows.Add(New TableRow)

            For i As Integer = 0 To 27
                tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
            Next


            If primeraOcurrenciaCodigo = True Then
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = Trim(x.Item("cod_producto"))
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).CssClass = "acs-t1"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = Trim(x.Item("des_producto"))
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).CssClass = "acs-t2"
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Wrap = False
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).RowSpan = 2

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = x.Item("cod_umb")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).CssClass = "acs-t3"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = CType(x.Item("dii"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).CssClass = "acs-t3"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = CType(x.Item("stock_contable"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).CssClass = "acs-t3"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = CType(x.Item("tot_pedidos"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).CssClass = "acs-t3"

                hlCampoSubcabecera = New HyperLink
                hlCampoSubcabecera.CssClass = "acs-popup1"
                hlCampoSubcabecera.Text = CType(x.Item("oc_pedidos"), Double).ToString("#,#0")
                hlCampoSubcabecera.NavigateUrl = String.Format("#OC|{0}", Trim(x.Item("cod_producto")))
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Controls.Add(hlCampoSubcabecera)
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).CssClass = "acs-t3"

                hlCampoSubcabecera = New HyperLink
                hlCampoSubcabecera.CssClass = "acs-popup1"
                hlCampoSubcabecera.Text = CType(x.Item("prof_pedidos"), Double).ToString("#,#0")
                hlCampoSubcabecera.NavigateUrl = String.Format("#PROF|{0}", Trim(x.Item("cod_producto")))
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Controls.Add(hlCampoSubcabecera)
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).CssClass = "acs-t3"


                hlCampoSubcabecera = New HyperLink
                hlCampoSubcabecera.CssClass = "acs-popup1"
                hlCampoSubcabecera.Text = CType(x.Item("trans_pedidos"), Double).ToString("#,#0")
                hlCampoSubcabecera.NavigateUrl = String.Format("#FACT|{0}", Trim(x.Item("cod_producto")))
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Controls.Add(hlCampoSubcabecera)
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).CssClass = "acs-t3"


                If Not x.IsNull("en_proc_recepcion") Then
                    tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).Text = CType(x.Item("en_proc_recepcion"), Double).ToString("#,#0")
                Else
                    tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).Text = ""
                End If
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).CssClass = "acs-t3"

                If Not x.IsNull("cant_prox_recepcion") Then
                    tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).Text = CType(x.Item("cant_prox_recepcion"), Double).ToString("#,#0")
                Else
                    tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).Text = ""
                End If

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).CssClass = "acs-t3"

                If Not x.IsNull("fec_prox_recepcion") Then
                    tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).Text = CType(x.Item("fec_prox_recepcion"), DateTime).ToString("dd/MM/yyyy")
                Else
                    tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).Text = ""
                End If

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).CssClass = "acs-t3"



                tDatos.Rows(tDatos.Rows.Count - 1).Cells(12).Text = CType(x.Item("prom_vta_3m"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(12).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(12).CssClass = "acs-t3"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(13).Text = CType(x.Item("prom_vta_6m"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(13).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(13).CssClass = "acs-t3"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(14).Text = CType(x.Item("prom_vta_12m"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(14).RowSpan = 2
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(14).CssClass = "acs-t3"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(15).Text = x.Item("concepto")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(15).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(16).Text = CType(x.Item("mes_11"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(16).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(17).Text = CType(x.Item("mes_10"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(17).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(18).Text = CType(x.Item("mes_09"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(18).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(19).Text = CType(x.Item("mes_08"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(19).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(20).Text = CType(x.Item("mes_07"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(20).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(21).Text = CType(x.Item("mes_06"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(21).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(22).Text = CType(x.Item("mes_05"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(22).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(23).Text = CType(x.Item("mes_04"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(23).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(24).Text = CType(x.Item("mes_03"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(24).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(25).Text = CType(x.Item("mes_02"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(25).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(26).Text = CType(x.Item("mes_01"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(26).CssClass = "acs-t6"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(27).Text = CType(x.Item("mes_00"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(27).CssClass = "acs-t6"

            Else

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = x.Item("concepto")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).CssClass = "acs-t4"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = CType(x.Item("mes_00"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = CType(x.Item("mes_01"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = CType(x.Item("mes_02"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = CType(x.Item("mes_03"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = CType(x.Item("mes_04"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Text = CType(x.Item("mes_05"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Text = CType(x.Item("mes_06"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Text = CType(x.Item("mes_07"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).Text = CType(x.Item("mes_08"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).Text = CType(x.Item("mes_09"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).Text = CType(x.Item("mes_10"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).CssClass = "acs-t5"

                tDatos.Rows(tDatos.Rows.Count - 1).Cells(12).Text = CType(x.Item("mes_11"), Double).ToString("#,#0")
                tDatos.Rows(tDatos.Rows.Count - 1).Cells(12).CssClass = "acs-t5"
            End If
        Next

        Panel1.Controls.Clear()
        Panel1.Controls.Add(tDatos)
    End Function

    Private Sub ddlEncargadoSubfamilia_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlEncargadoSubfamilia.SelectedIndexChanged
        cargaAreas(codigoSociedad, "", ddlEncargadoSubfamilia.SelectedValue)
        tbMaterial.Text = ""
        cargaFamilias(codigoSociedad, ddlArea.SelectedValue, "", ddlEncargadoSubfamilia.SelectedValue)
        cargaSubfamilias(codigoSociedad, ddlArea.SelectedValue, ddlFamilia.SelectedValue, "", ddlEncargadoSubfamilia.SelectedValue)
    End Sub

    Private Sub cargaEncargadosSubfamilia(ByVal valorInicial As String)
        With ddlEncargadoSubfamilia
            .DataSource = wsAndes.obtieneEncargadosSubfamilias(codigoFilial)
            .DataValueField = "encargado"
            .DataTextField = "encargado"
            .DataBind()
            .Items.Add(New ListItem("- seleccione -", "*"))
        End With

        'Si el usuario de sesión está dentro de la lista, entonces seleccionamos
        'valor dentro de la lista
        If Not IsNothing(ddlEncargadoSubfamilia.Items.FindByValue(valorInicial)) Then
            ddlEncargadoSubfamilia.Items.FindByValue(valorInicial).Selected = True
        Else
            ddlEncargadoSubfamilia.Items.FindByValue("*").Selected = True
        End If
    End Sub

    Private Sub ddlSubfamilia_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlSubfamilia.SelectedIndexChanged
        tbMaterial.Text = ""
    End Sub
End Class
