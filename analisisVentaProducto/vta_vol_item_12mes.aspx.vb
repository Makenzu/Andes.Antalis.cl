Imports Exportador

Public Class vta_vol_item_12mes
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage


    ' Para calculo de totales
    Dim totales(13) As Integer

    Dim ano_periodo As Int16
    Dim mes_periodo As Int16

    Dim tUserInfo As usuario.t_Usuario

    Protected WithEvents plProveedor As System.Web.UI.WebControls.Panel
    Protected WithEvents ibFamiliaxProveedor As System.Web.UI.WebControls.ImageButton
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlFamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSubFamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cblSubFamilia As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMesFamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAnoFamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ibSendFamilia As System.Web.UI.WebControls.ImageButton
    Protected WithEvents plFamilia As System.Web.UI.WebControls.Panel
    Protected WithEvents ibProveedorxFamilia As System.Web.UI.WebControls.ImageButton
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlProveedor As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMesProveedor As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAnoProveedor As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btSendProveedor As System.Web.UI.WebControls.ImageButton
    Protected WithEvents tblResultados As System.Web.UI.WebControls.Table
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibVolver As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCentro As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCentro2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbIncMesActual As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbIncMesActual2 As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lbNombreCentro As System.Web.UI.WebControls.Label
    Protected WithEvents cbExcluyeVentasIqq As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbExcluyeVentasIqq2 As System.Web.UI.WebControls.CheckBox

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


        Dim xDatos As DataTable
        Dim xCodigoFamilia As String

        Page.Server.ScriptTimeout = 90

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        lbErrors.Text = ""
        ibVolver.Visible = False

        lbNombreCentro.Visible = Page.IsPostBack

        If Not Page.IsPostBack Then
            ddlAnoFamilia.Items.Add(Year(Date.Now))
            ddlAnoFamilia.Items.Add(Year(Date.Now.AddYears(-1)))
            ddlAnoFamilia.Items.Add(Year(Date.Now.AddYears(-2)))
            ddlAnoProveedor.Items.Add(Year(Date.Now))
            ddlAnoProveedor.Items.Add(Year(Date.Now.AddYears(-1)))
            ddlAnoProveedor.Items.Add(Year(Date.Now.AddYears(-2)))
            Dim i As Integer
            Dim x As Integer = 12
            For i = 0 To 11

                Dim newListItem As New ListItem
                If (Month(Date.Now) - i) >= 1 Then
                    newListItem.Text = MonthName(Month(Date.Now) - i)
                    newListItem.Value = Month(Date.Now) - i
                Else
                    newListItem.Text = MonthName(Month(Date.Now) + x)
                    newListItem.Value = Month(Date.Now) + x
                End If
                ddlMesFamilia.Items.Add(newListItem)
                ddlMesProveedor.Items.Add(newListItem)

                If Month(Date.Now) - i = Month(Date.Now) Then
                    ddlMesFamilia.Items(i).Selected = True
                    ddlMesProveedor.Items(i).Selected = True
                End If
                x -= 1
            Next
            ano_periodo = Year(Date.Now)
            mes_periodo = Month(Date.Now)
            xDatos = Utiles.buscaFamilias(tUserInfo.codigoFilial)
            ddlFamilia.DataSource = xDatos
            ddlFamilia.DataTextField = "COL1"
            ddlFamilia.DataValueField = "cod_familia"
            ddlFamilia.DataBind()
            ddlFamilia.Items(0).Selected = True

            xCodigoFamilia = ddlFamilia.SelectedValue
            xDatos = Utiles.buscaSubFamilias(Utiles.eModo.estandar, tUserInfo.codigoFilial, xCodigoFamilia)
            ddlSubFamilia.DataSource = xDatos
            ddlSubFamilia.DataTextField = "COL1"
            ddlSubFamilia.DataValueField = "cod_subfamilia"
            ddlSubFamilia.DataBind()
            ddlSubFamilia.Visible = True
            cblSubFamilia.Visible = False
            ddlSubFamilia.Items(0).Selected = True
            xDatos = Utiles.buscaTodosProveedores(tUserInfo.codigoFilial)
            ddlProveedor.DataSource = xDatos
            ddlProveedor.DataTextField = "COL1"
            ddlProveedor.DataValueField = "cod_proveedor"
            ddlProveedor.DataBind()
            Session("ID_FAMILIA") = ddlFamilia.Items(0).Value
            Session("ID_SUBFAMILIA") = ddlSubFamilia.Items(0).Value

            'Llenamos centros disponibles.
            With ddlCentro
                .DataSource = obtieneSucursales(tUserInfo.codigoFilial)
                .DataTextField = "des_sucursal"
                .DataValueField = "cod_sucursal"
                .DataBind()
                If (.Items.Count > 1) Then
                    .Items.Add(New ListItem("TODOS LOS CENTROS PARA " & tUserInfo.codigoFilial, "*"))
                End If
            End With

            With ddlCentro2
                .DataSource = obtieneSucursales(tUserInfo.codigoFilial)
                .DataTextField = "des_sucursal"
                .DataValueField = "cod_sucursal"
                .DataBind()
                If (.Items.Count > 1) Then
                    .Items.Add(New ListItem("TODOS LOS CENTROS PARA " & tUserInfo.codigoFilial, "*"))
                End If
            End With

            cbExcluyeVentasIqq.Visible = (tUserInfo.codigoFilial = "BOL")
            cbExcluyeVentasIqq.Visible = (tUserInfo.codigoFilial = "BOL")

        Else
            If ddlFamilia.SelectedValue <> Session("ID_FAMILIA") Then
                'Recargar las subfamilias
                cblSubFamilia.Visible = False
                xCodigoFamilia = ddlFamilia.SelectedValue
                xDatos = Utiles.buscaSubFamilias(eModo.estandar, tUserInfo.codigoFilial, xCodigoFamilia)
                ddlSubFamilia.DataSource = xDatos
                ddlSubFamilia.DataTextField = "COL1"
                ddlSubFamilia.DataValueField = "cod_subfamilia"
                ddlSubFamilia.DataBind()
                ddlSubFamilia.Visible = True
                Session("ID_FAMILIA") = ddlFamilia.SelectedValue
                Session("ID_SUBFAMILIA") = ddlSubFamilia.Items(0).Value
            Else
                If ddlSubFamilia.SelectedValue <> Session("ID_SUBFAMILIA") Then
                    Session("ID_SUBFAMILIA") = ddlSubFamilia.SelectedValue
                    If ddlSubFamilia.SelectedValue = "-200" Then
                        ddlSubFamilia.Visible = False
                        xCodigoFamilia = ddlFamilia.SelectedValue
                        xDatos = Utiles.buscaSubFamilias(eModo.estandar, tUserInfo.codigoFilial, xCodigoFamilia)
                        cblSubFamilia.DataSource = xDatos
                        cblSubFamilia.DataTextField = "COL1"
                        cblSubFamilia.DataValueField = "cod_subfamilia"
                        cblSubFamilia.DataBind()
                        cblSubFamilia.Items(0).Selected = True
                        cblSubFamilia.Visible = True
                    End If
                End If
            End If
            If plFamilia.Visible = True Then
                mes_periodo = CInt(ddlMesFamilia.SelectedItem.Value)
                ano_periodo = CInt(ddlAnoFamilia.SelectedItem.Value)
                Session("ID_SUCURSAL") = ddlCentro.SelectedValue
                Session("NOMBRE_SUCURSAL") = ddlCentro.SelectedItem.Text
            Else
                mes_periodo = CInt(ddlMesProveedor.SelectedItem.Value)
                ano_periodo = CInt(ddlAnoProveedor.SelectedItem.Value)
                Session("ID_SUCURSAL") = ddlCentro2.SelectedValue
                Session("NOMBRE_SUCURSAL") = ddlCentro2.SelectedItem.Text

            End If
        End If
        lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo
    End Sub

    Private Sub GeneraTabla()
        Dim xCodFamilia As String
        Dim xCodSubFamilia As String
        Dim xYear As String
        Dim xMes As String
        Dim i As Integer
        Dim j As Integer
        Dim Resultados As DataTable
        Dim xAux As DataTable
        Dim xEncargado As DataTable
        Dim Encargado As String
        Dim DSResultados As DataSet
        Dim xSubFamiliaActual As String
        Dim trDatos As TableRow
        Dim tcValores As TableCell
        Dim totalSubfamilia As Double
        Dim acumSubfamilia As Double
        Dim txtSubFamilia As String
        Dim sw As Integer
        Dim xTexto As String
        Dim Acum1, Acum2, Acum3, Acum4, Acum5, Acum6, Acum7, Acum8, Acum9, Acum10, Acum11, Acum12, Acum13, AcumStock, AcumPendiente, AcumCOnsignacion As Double
        Dim Total1, Total2, Total3, Total4, Total5, Total6, Total7, Total8, Total9, Total10, Total11, Total12, Total13, TotalStock, TotalPendiente, TotalConsignacion As Double
        Dim Calculo As Double
        Dim incluyeMesActual, excluyeVentasIqq As Integer






        'Vemos cual es la opcion del usuario, y llenamos la subfamilia
        xCodFamilia = ddlFamilia.SelectedValue
        If ddlSubFamilia.SelectedValue = "-100" Then
            xCodSubFamilia = ""
        ElseIf ddlSubFamilia.SelectedValue = "-200" Then
            sw = 0
            For j = 0 To cblSubFamilia.Items.Count - 1
                If cblSubFamilia.Items(j).Selected = True Then
                    sw = sw + 1
                    If sw = 1 Then
                        txtSubFamilia = txtSubFamilia & "|" & cblSubFamilia.Items(j).Value & "|"
                    Else
                        txtSubFamilia = txtSubFamilia & ", |" & cblSubFamilia.Items(j).Value & "|"
                    End If
                End If
            Next
            xCodSubFamilia = txtSubFamilia
        Else
            xCodSubFamilia = "|" & ddlSubFamilia.SelectedValue & "|"
        End If
        'Vemos de donde sacamos los datos
        If plFamilia.Visible = True Then
            xYear = ddlAnoFamilia.SelectedValue
            xMes = ddlMesFamilia.SelectedValue
            If cbIncMesActual.Checked Then
                incluyeMesActual = 1
            Else
                incluyeMesActual = 0
            End If


            If cbExcluyeVentasIqq.Checked Then
                excluyeVentasIqq = 1
            Else
                excluyeVentasIqq = 0
            End If

            'Año y mes
            If xYear = 2005 And xMes < 8 Then
                lbErrors.Text = "No se puede obtener información para el período seleccionado."
                Exit Sub
            End If
            If xCodSubFamilia = "" Then
                Resultados = ventas.vta_volumen_12meses(xYear, xMes, tUserInfo.codigoFilial, Session("ID_SUCURSAL"), xCodFamilia, "", "", "", incluyeMesActual, excluyeVentasIqq)
            Else
                Resultados = ventas.vta_volumen_12meses(xYear, xMes, tUserInfo.codigoFilial, Session("ID_SUCURSAL"), "", xCodSubFamilia, "", "", incluyeMesActual, excluyeVentasIqq)
            End If
        Else
            If cbIncMesActual2.Checked Then
                incluyeMesActual = 1
            Else
                incluyeMesActual = 0
            End If

            If cbExcluyeVentasIqq.Checked Then
                excluyeVentasIqq = 1
            Else
                excluyeVentasIqq = 0
            End If

            xYear = ddlAnoProveedor.SelectedValue
            xMes = ddlMesProveedor.SelectedValue
            'Año y mes
            If xYear = 2005 And xMes < 8 Then
                lbErrors.Text = "No se puede obtener información para el período seleccionado."
                Exit Sub
            End If
            Resultados = ventas.vta_volumen_12meses(xYear, xMes, tUserInfo.codigoFilial, Session("ID_SUCURSAL"), "", "", "", ddlProveedor.SelectedValue, incluyeMesActual, excluyeVentasIqq)
        End If






        '------------------------------------------------------------------------------------------------------
        'Intentamos recoger indicadores de stock y pedidos pendientes desde SAP
        'Si la consulta se realiza en mes abierto, entonces obtenemos valores de pedidos pendientes y de stock
        'desde SAP.
        '------------------------------------------------------------------------------------------------------
        Dim msgConsultaSap As String

        If mes_periodo = Now.Month Then
            Dim tblStockMaterial As DataTable = New DataTable
            Dim tblStockMaterial2 As DataTable = New DataTable

            Try
                Dim sociedad, centro As String

                'Segun el código de sucursal, obtenemos equivalencia de sociedad y centro de distribución.
                tblStockMaterial2.Clear()
                If Session("ID_SUCURSAL") = "*" Then
                    Dim tUserInfo As t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

                    If (tUserInfo.codigoFilial = "BOL") Then
                        tblStockMaterial = materiales.obtieneStockMateriales("ABOL", "", Resultados)

                        If cbExcluyeVentasIqq.Checked = False And cbExcluyeVentasIqq2.Checked = False Then
                            tblStockMaterial2 = materiales.obtieneStockMateriales("DGS0", "", Resultados)
                        End If

                    ElseIf (tUserInfo.codigoFilial = "PER") Then
                        tblStockMaterial = materiales.obtieneStockMateriales("APER", "", Resultados)
                    ElseIf (tUserInfo.codigoFilial = "CHI") Then
                        tblStockMaterial = materiales.obtieneStockMateriales("GMSC", "", Resultados)
                    End If

                Else
                    seteaParametrosConsulta(Session("ID_SUCURSAL"), sociedad, centro)
                    'consultamos a SAP stock y pedidos pendientes expresados en unidad de medida base
                    tblStockMaterial = materiales.obtieneStockMateriales(sociedad, centro, Resultados)
                End If




                'modificamos los valores de la ultima carga de datos Andes/Iddeo, con info actualizada de 
                'stock y pedidos pendientes.
                For i = 0 To Resultados.Rows.Count - 1
                    If Trim(Resultados.Rows(i).Item("cod_producto")) = tblStockMaterial.Rows(i).Item("material") Then
                        Resultados.Rows(i).Item("val_stock_contable") = tblStockMaterial.Rows(i).Item("stock_contable") * Resultados.Rows(i).Item("val_volumen")
                        Resultados.Rows(i).Item("val_stock_pendiente") = tblStockMaterial.Rows(i).Item("stock_pendiente") * Resultados.Rows(i).Item("val_volumen")
                        Resultados.Rows(i).Item("val_stock_consignacion") = tblStockMaterial.Rows(i).Item("stock_consignacion") * Resultados.Rows(i).Item("val_volumen")

                        If tblStockMaterial2.Rows.Count > 0 Then

                            For j = 0 To tblStockMaterial2.Rows.Count - 1
                                If (Trim(Resultados.Rows(i).Item("cod_producto")) = Trim(tblStockMaterial2.Rows(j).Item("material"))) Then
                                    Resultados.Rows(i).Item("val_stock_contable") += tblStockMaterial2.Rows(j).Item("stock_contable") * Resultados.Rows(i).Item("val_volumen")
                                    Resultados.Rows(i).Item("val_stock_pendiente") += tblStockMaterial2.Rows(j).Item("stock_pendiente") * Resultados.Rows(i).Item("val_volumen")
                                    Resultados.Rows(i).Item("val_stock_consignacion") += tblStockMaterial2.Rows(j).Item("stock_consignacion") * Resultados.Rows(i).Item("val_volumen")
                                    Exit For
                                End If

                            Next

                        End If
                    End If
                Next
                msgConsultaSap = ":: Fuente stock / pendientes: <img align=""absmiddle"" src=""/images/sap.jpg"" border=""0"" alt=""Datos obtenidos en línea desde SAP"">"
            Catch ex As Exception
                msgConsultaSap = ":: Fuente stock / pendientes: <b>Registro ANDES</b>."
            End Try
        Else
            msgConsultaSap = ":: Fuente stock / pendientes: <b>Registro ANDES</b>."
        End If
        '------------------------------------------------------------------------------------------------------

        If Resultados.Rows.Count = 0 Then
            lbErrors.Text = "No se encontraron datos para la selección."
            Exit Sub
        End If


        '-----------------------------------------------------------------------------------------------------
        'Agregamos cabecera para datos de consulta de datos.
        Dim msgFechaConsulta As String = ":: Consulta: " + Format(Now, "dddd dd/MMM/yyyy HH:mm:ss")
        Dim fechaActualizacion As String

        Try
            fechaActualizacion = ":: Datos actualizados al: " & Format(obtieneFechaActualizacion(ano_periodo, mes_periodo, Session("ID_SUCURSAL"), "VENTAS_ITEM_12MESES"), "dddd dd/MMM/yyyy HH:mm:ss")

        Catch ex As Exception
            fechaActualizacion = ":: Datos actualizados al: (no determinado))"
        End Try

        trDatos = Crea_Linea(100, "", msgFechaConsulta, msgConsultaSap, fechaActualizacion, " ", " ", " ", " ", " ", " ")
        tblResultados.Rows.Add(trDatos)
        '-----------------------------------------------------------------------------------------------------

        'Recorrido de los datos
        xSubFamiliaActual = ""
        xAux = Resultados
        For i = 0 To Resultados.Rows.Count - 1
            If xSubFamiliaActual <> CStr(Resultados.Rows(i).Item("cod_subfamilia")) Then
                If i > 0 Then
                    'Coloco los totales
                    xTexto = CStr(Acum1) & "|" & CStr(Acum2) & "|" & CStr(Acum3) & "|" & CStr(Acum4) & "|" & CStr(Acum5) & "|" & CStr(Acum6) & "|" & CStr(Acum7) & "|" & CStr(Acum8) & "|" & CStr(Acum9) & "|" & CStr(Acum10) & "|" & CStr(Acum11) & "|" & CStr(Acum12) & "|" & CStr(Acum13)


                    If (incluyeMesActual) Then
                        Calculo = (Acum1 + Acum2 + Acum3 + Acum4 + Acum5 + Acum6 + Acum7 + Acum8 + Acum9 + Acum10 + Acum11 + Acum12 + Acum13) / 13
                    Else
                        Calculo = (Acum1 + Acum2 + Acum3 + Acum4 + Acum5 + Acum6 + Acum7 + Acum8 + Acum9 + Acum10 + Acum11 + Acum12) / 12
                    End If


                    trDatos = Crea_Linea(6, "", "", "TOTAL SUBFAMILIA: ", "", xTexto, Calculo, AcumStock, AcumPendiente, AcumCOnsignacion, IIf(Calculo = 0, 0, (AcumStock + AcumPendiente - AcumCOnsignacion) / Calculo))
                    tblResultados.Rows.Add(trDatos)
                    Acum1 = 0
                    Acum2 = 0
                    Acum3 = 0
                    Acum4 = 0
                    Acum5 = 0
                    Acum6 = 0
                    Acum7 = 0
                    Acum8 = 0
                    Acum9 = 0
                    Acum10 = 0
                    Acum11 = 0
                    Acum12 = 0
                    Acum13 = 0
                    AcumStock = 0
                    AcumPendiente = 0
                    AcumCOnsignacion = 0
                ElseIf i = 0 Then
                    'Coloco el título
                    'trDatos = Crea_Linea(1, "", "", "", "", "", "", "", "")
                    'trDatos.CssClass = "tbl-FechaHeader"
                    'tblResultados.Rows.Add(trDatos)
                    If plFamilia.Visible = True Then
                        trDatos = Crea_Linea(1, "", Resultados.Rows(i).Item("cod_familia") & " - " & UCase(Resultados.Rows(i).Item("des_familia")), "", "", "", "", "", "", "", "")
                    Else
                        trDatos = Crea_Linea(1, "", ddlProveedor.SelectedItem.Text, "", "", "", "", "", "", "", "")
                    End If
                    tblResultados.Rows.Add(trDatos)
                End If
                'Coloco encabezado
                trDatos = Crea_Linea(5, "", " ", " ", " ", " ", " ", " ", " ", " ", " ")
                tblResultados.Rows.Add(trDatos)

                Try
                    Encargado = Utiles.buscaEncargadoSubfamilia(tUserInfo.codigoFilial, UCase(Resultados.Rows(i).Item("cod_subfamilia")))
                Catch ex As Exception
                    Encargado = ""
                End Try


                trDatos = Crea_Linea(2, "", "", UCase(Resultados.Rows(i).Item("cod_subfamilia") & "-" & UCase(Resultados.Rows(i).Item("des_subfamilia"))), IIf(Encargado = "", "SIN ENCARGADO", "ENCARGADO : " & UCase(Encargado)), "", "", "", "", "", "")
                tblResultados.Rows.Add(trDatos)
                xTexto = Utiles.ObtieneListaMeses(xMes, xYear, 13)
                trDatos = Crea_Linea(3, "", "CODIGO", "MATERIAL", "UN.", xTexto, "PROM.", "STOCK", "PEND", "CONSIGN", "MESES")
                tblResultados.Rows.Add(trDatos)
                totalSubfamilia = 0
                xSubFamiliaActual = Resultados.Rows(i).Item("cod_subfamilia")
            End If
            'Coloco la linea de datos
            totalSubfamilia = totalSubfamilia + 1
            acumSubfamilia = acumSubfamilia + 1
            xTexto = Resultados.Rows(i).Item("mes_doce") & "|" & Resultados.Rows(i).Item("mes_once") & "|" & Resultados.Rows(i).Item("mes_diez") & "|" & Resultados.Rows(i).Item("mes_nueve") & "|" & Resultados.Rows(i).Item("mes_ocho") & "|" & Resultados.Rows(i).Item("mes_siete") & "|" & Resultados.Rows(i).Item("mes_seis") & "|" & Resultados.Rows(i).Item("mes_cinco") & "|" & Resultados.Rows(i).Item("mes_cuatro") & "|" & Resultados.Rows(i).Item("mes_tres") & "|" & Resultados.Rows(i).Item("mes_dos") & "|" & Resultados.Rows(i).Item("mes_uno") & "|" & Resultados.Rows(i).Item("mes_actual")
            Acum1 = Acum1 + Double.Parse(Resultados.Rows(i).Item("mes_doce"))
            Acum2 = Acum2 + Double.Parse(Resultados.Rows(i).Item("mes_once"))
            Acum3 = Acum3 + Double.Parse(Resultados.Rows(i).Item("mes_diez"))
            Acum4 = Acum4 + Double.Parse(Resultados.Rows(i).Item("mes_nueve"))
            Acum5 = Acum5 + Double.Parse(Resultados.Rows(i).Item("mes_ocho"))
            Acum6 = Acum6 + Double.Parse(Resultados.Rows(i).Item("mes_siete"))
            Acum7 = Acum7 + Double.Parse(Resultados.Rows(i).Item("mes_seis"))
            Acum8 = Acum8 + Double.Parse(Resultados.Rows(i).Item("mes_cinco"))
            Acum9 = Acum9 + Double.Parse(Resultados.Rows(i).Item("mes_cuatro"))
            Acum10 = Acum10 + Double.Parse(Resultados.Rows(i).Item("mes_tres"))
            Acum11 = Acum11 + Double.Parse(Resultados.Rows(i).Item("mes_dos"))
            Acum12 = Acum12 + Double.Parse(Resultados.Rows(i).Item("mes_uno"))
            Acum13 = Acum13 + Double.Parse(Resultados.Rows(i).Item("mes_actual"))

            AcumStock = AcumStock + Double.Parse(Resultados.Rows(i).Item("val_stock_contable"))
            AcumPendiente = AcumPendiente + Double.Parse(Resultados.Rows(i).Item("val_stock_pendiente"))
            AcumCOnsignacion = AcumCOnsignacion + Double.Parse(Resultados.Rows(i).Item("val_stock_consignacion"))

            Total1 = Total1 + Double.Parse(Resultados.Rows(i).Item("mes_doce"))
            Total2 = Total2 + Double.Parse(Resultados.Rows(i).Item("mes_once"))
            Total3 = Total3 + Double.Parse(Resultados.Rows(i).Item("mes_diez"))
            Total4 = Total4 + Double.Parse(Resultados.Rows(i).Item("mes_nueve"))
            Total5 = Total5 + Double.Parse(Resultados.Rows(i).Item("mes_ocho"))
            Total6 = Total6 + Double.Parse(Resultados.Rows(i).Item("mes_siete"))
            Total7 = Total7 + Double.Parse(Resultados.Rows(i).Item("mes_seis"))
            Total8 = Total8 + Double.Parse(Resultados.Rows(i).Item("mes_cinco"))
            Total9 = Total9 + Double.Parse(Resultados.Rows(i).Item("mes_cuatro"))
            Total10 = Total10 + Double.Parse(Resultados.Rows(i).Item("mes_tres"))
            Total11 = Total11 + Double.Parse(Resultados.Rows(i).Item("mes_dos"))
            Total12 = Total12 + Double.Parse(Resultados.Rows(i).Item("mes_uno"))
            Total13 = Total13 + Double.Parse(Resultados.Rows(i).Item("mes_actual"))

            TotalStock = TotalStock + Double.Parse(Resultados.Rows(i).Item("val_stock_contable"))
            TotalPendiente = TotalPendiente + Double.Parse(Resultados.Rows(i).Item("val_stock_pendiente"))
            TotalConsignacion = TotalConsignacion + Double.Parse(Resultados.Rows(i).Item("val_stock_consignacion"))

            Dim stockContableActual As Double = Double.Parse(Resultados.Rows(i).Item("val_stock_contable"))
            Dim stockPendienteActual As Double = Double.Parse(Resultados.Rows(i).Item("val_stock_pendiente"))
            Dim stockConsignacionActual As Double = Double.Parse(Resultados.Rows(i).Item("val_stock_consignacion"))
            Dim promedioVentaActual As Double

            If (incluyeMesActual) Then
                promedioVentaActual = (Double.Parse(Resultados.Rows(i).Item("mes_doce")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_once")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_diez")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_nueve")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_ocho")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_siete")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_seis")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_cinco")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_cuatro")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_tres")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_dos")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_uno")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_actual"))) / 13.0
            Else
                promedioVentaActual = (Double.Parse(Resultados.Rows(i).Item("mes_doce")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_once")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_diez")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_nueve")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_ocho")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_siete")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_seis")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_cinco")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_cuatro")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_tres")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_dos")) + _
                                        Double.Parse(Resultados.Rows(i).Item("mes_uno"))) / 12.0
            End If

            Dim mesesStockActual As Double

            If promedioVentaActual <> 0 Then
                mesesStockActual = (stockContableActual + stockPendienteActual - stockConsignacionActual) / promedioVentaActual
            Else
                mesesStockActual = 0
            End If

            trDatos = Crea_Linea(4, "", "'" & Resultados.Rows(i).Item("cod_producto"), _
                                                Resultados.Rows(i).Item("des_producto"), _
                                                Resultados.Rows(i).Item("cod_un_vol"), _
                                                xTexto, _
                                                promedioVentaActual, _
                                                stockContableActual, _
                                                stockPendienteActual, _
                                                stockConsignacionActual, _
                                                mesesStockActual)

            tblResultados.Rows.Add(trDatos)
        Next
        'Coloco los totales de la ultima subfamilia
        xTexto = CStr(Acum1) & "|" & CStr(Acum2) & "|" & CStr(Acum3) & "|" & CStr(Acum4) & "|" & CStr(Acum5) & "|" & CStr(Acum6) & "|" & CStr(Acum7) & "|" & CStr(Acum8) & "|" & CStr(Acum9) & "|" & CStr(Acum10) & "|" & CStr(Acum11) & "|" & CStr(Acum12) & "|" & CStr(Acum13)

        If (incluyeMesActual) Then
            Calculo = (Acum1 + Acum2 + Acum3 + Acum4 + Acum5 + Acum6 + Acum7 + Acum8 + Acum9 + Acum10 + Acum11 + Acum12 + Acum13) / 13
        Else
            Calculo = (Acum1 + Acum2 + Acum3 + Acum4 + Acum5 + Acum6 + Acum7 + Acum8 + Acum9 + Acum10 + Acum11 + Acum12) / 12
        End If

        trDatos = Crea_Linea(6, "", "", "TOTAL SUBFAMILIA: ", "", xTexto, Calculo, AcumStock, AcumPendiente, AcumCOnsignacion, IIf(Calculo = 0, 0, (AcumStock + AcumPendiente - AcumCOnsignacion) / Calculo))
        tblResultados.Rows.Add(trDatos)
        trDatos = Crea_Linea(5, "", " ", " ", " ", " ", " ", " ", " ", " ", " ")
        tblResultados.Rows.Add(trDatos)
        'Coloco los totales generales
        xTexto = CStr(Total1) & "|" & CStr(Total2) & "|" & CStr(Total3) & "|" & CStr(Total4) & "|" & CStr(Total5) & "|" & CStr(Total6) & "|" & CStr(Total7) & "|" & CStr(Total8) & "|" & CStr(Total9) & "|" & CStr(Total10) & "|" & CStr(Total11) & "|" & CStr(Total12) & "|" & CStr(Total13)
        If plFamilia.Visible = True Then
            If ddlSubFamilia.SelectedValue = "-100" Then

                If (incluyeMesActual) Then
                    Calculo = (Total1 + Total2 + Total3 + Total4 + Total5 + Total6 + Total7 + Total8 + Total9 + Total10 + Total11 + Total12 + Total13) / 13
                Else
                    Calculo = (Total1 + Total2 + Total3 + Total4 + Total5 + Total6 + Total7 + Total8 + Total9 + Total10 + Total11 + Total12) / 12
                End If

                trDatos = Crea_Linea(6, "", "", "TOTAL FAMILIA: ", "", xTexto, Calculo, TotalStock, TotalPendiente, TotalConsignacion, IIf(Calculo = 0, 0, (TotalStock + TotalPendiente - TotalConsignacion) / Calculo))
            Else
                If (incluyeMesActual) Then
                    Calculo = (Total1 + Total2 + Total3 + Total4 + Total5 + Total6 + Total7 + Total8 + Total9 + Total10 + Total11 + Total12 + Total13) / 13
                Else
                    Calculo = (Total1 + Total2 + Total3 + Total4 + Total5 + Total6 + Total7 + Total8 + Total9 + Total10 + Total11 + Total12) / 12
                End If

                trDatos = Crea_Linea(6, "", "", "TOTAL SELECCION: ", "", xTexto, Calculo, TotalStock, TotalPendiente, TotalConsignacion, IIf(Calculo = 0, 0, (TotalStock + TotalPendiente - TotalConsignacion) / Calculo))
            End If
        Else
            If (incluyeMesActual) Then
                Calculo = (Total1 + Total2 + Total3 + Total4 + Total5 + Total6 + Total7 + Total8 + Total9 + Total10 + Total11 + Total12 + Total13) / 13
            Else
                Calculo = (Total1 + Total2 + Total3 + Total4 + Total5 + Total6 + Total7 + Total8 + Total9 + Total10 + Total11 + Total12) / 12
            End If

            trDatos = Crea_Linea(6, "", "", "TOTAL PROVEEDOR: ", "", xTexto, Calculo, TotalStock, TotalPendiente, TotalConsignacion, IIf(Calculo = 0, 0, (TotalStock + TotalPendiente - TotalConsignacion) / Calculo))
        End If
        tblResultados.Rows.Add(trDatos)
        'Desmarco toda la selección del usuario
        If ddlSubFamilia.SelectedValue = "-200" Then
            For i = 0 To ddlSubFamilia.Items.Count - 1
                ddlSubFamilia.Items(i).Selected = False
            Next
            ddlSubFamilia.Items(0).Selected = True
            ddlSubFamilia.Visible = True
            cblSubFamilia.Visible = False
        End If

        ibExportar.Visible = True
        ibVolver.Visible = True
        If plFamilia.Visible = True Then
            Session("LAST_VISIBLE") = "FAMILIA"
        Else
            Session("LAST_VISIBLE") = "PROVEEDOR"
        End If
        plFamilia.Visible = False
        plProveedor.Visible = False
        lbNombreCentro.Visible = True
        lbNombreCentro.Text = "CENTRO: " & Session("NOMBRE_SUCURSAL")

        Session("myTable") = tblResultados
    End Sub

    Private Function Crea_Linea(ByVal Tipo As Integer, _
                                ByVal Sangria As String, _
                                ByVal codigoProducto As String, _
                                ByVal Material As String, _
                                ByVal UnidadMedida As String, _
                                ByVal Meses As String, _
                                ByVal Promedio As String, _
                                ByVal stockContable As String, _
                                ByVal stockPendiente As String, _
                                ByVal stockConsignacion As String, _
                                ByVal NMeses As String) As TableRow
        Dim trDatos As TableRow
        Dim tcValores As TableCell
        Dim i As Integer
        Dim xTexto() As String
        Dim TotalColumnas As Integer

        TotalColumnas = 22
        trDatos = New TableRow

        If Tipo = 1 Or Tipo = 5 Then 'Titulo de Familia o Espacio en Blanco
            tcValores = New TableCell
            tcValores.Text = Sangria
            tcValores.Wrap = False
            tcValores.ColumnSpan = TotalColumnas
            If Tipo = 5 Then
                tcValores.Height = Unit.Pixel(20)
                trDatos.Cells.Add(tcValores)
                trDatos.CssClass = "tbl-DataGridItemAlternating"
            Else
                trDatos.Cells.Add(tcValores)
                trDatos.CssClass = "tbl-DataGridHeaderAlternating"
            End If
        ElseIf Tipo = 100 Then 'Titulo de Familia o Espacio en Blanco
            tcValores = New TableCell
            tcValores.Text = Sangria + "<br>" + UnidadMedida + "<br><br>" + Material
            tcValores.Wrap = False
            tcValores.ColumnSpan = TotalColumnas
            tcValores.CssClass = "info-informes"
            trDatos.Cells.Add(tcValores)
        ElseIf Tipo = 2 Then 'Título de Subfamilia
            tcValores = New TableCell
            tcValores.Wrap = False
            tcValores.Text = Sangria
            tcValores.CssClass = "tbl-DataGridHeader"
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = Material
            tcValores.Wrap = False
            tcValores.ColumnSpan = TotalColumnas / 2
            tcValores.CssClass = "tbl-DataGridHeader"
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = UnidadMedida
            tcValores.Wrap = False
            tcValores.ColumnSpan = TotalColumnas - (1 + TotalColumnas / 2)
            tcValores.HorizontalAlign = HorizontalAlign.Right
            tcValores.CssClass = "tbl-EncargadoHeader"
            trDatos.Cells.Add(tcValores)

        ElseIf Tipo = 3 Or Tipo = 4 Or Tipo = 6 Then
            'Corresponde a un espacio en blanco
            tcValores = New TableCell
            tcValores.Text = Sangria
            tcValores.Wrap = False
            tcValores.Width = Unit.Pixel(30)
            trDatos.Cells.Add(tcValores)

            'Código de material
            tcValores = New TableCell
            tcValores.Wrap = False
            tcValores.Text = codigoProducto
            If Tipo = 3 Then
                tcValores.HorizontalAlign = HorizontalAlign.Center
                tcValores.Wrap = False
            ElseIf Tipo = 6 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)

            'Corresponde al Material
            tcValores = New TableCell
            tcValores.Wrap = False
            tcValores.Text = Material
            If Tipo = 3 Then
                tcValores.HorizontalAlign = HorizontalAlign.Center
                tcValores.Wrap = False
            ElseIf Tipo = 6 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)

            'Corresponde a la Unidad de Medida
            tcValores = New TableCell
            tcValores.Width = Unit.Pixel(20)
            tcValores.Wrap = False
            tcValores.Text = UnidadMedida
            If Tipo = 3 Then
                tcValores.HorizontalAlign = HorizontalAlign.Center
                tcValores.Wrap = False
            End If
            trDatos.Cells.Add(tcValores)
            'Corresponde a los meses
            xTexto = Split(Meses, "|", -1, CompareMethod.Text)
            For i = 0 To xTexto.Length - 1
                tcValores = New TableCell
                tcValores.Width = Unit.Pixel(40)
                tcValores.Text = xTexto(i)
                tcValores.Wrap = False
                If Tipo = 4 Or Tipo = 6 Then
                    tcValores.HorizontalAlign = HorizontalAlign.Right
                    tcValores.Text = Format(Double.Parse(xTexto(i)), "#,##0")
                Else
                    tcValores.HorizontalAlign = HorizontalAlign.Right
                    tcValores.Wrap = False
                End If
                trDatos.Cells.Add(tcValores)
            Next

            tcValores = New TableCell
            tcValores.Width = Unit.Pixel(40)
            tcValores.Wrap = False
            tcValores.Text = Promedio
            If Tipo = 4 Or Tipo = 6 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
                If Promedio <> "" Then
                    tcValores.Text = Format(Double.Parse(Promedio), "#,##0.0")
                End If
            Else
                tcValores.Wrap = False
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Width = Unit.Pixel(40)
            tcValores.Wrap = False
            tcValores.Text = stockContable
            If Tipo = 4 Or Tipo = 6 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
                If stockContable <> "" Then
                    tcValores.Text = Format(Double.Parse(stockContable), "#,##0")
                End If
            Else
                tcValores.Wrap = False
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Width = Unit.Pixel(40)
            tcValores.Wrap = False
            tcValores.Text = stockPendiente
            If Tipo = 4 Or Tipo = 6 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
                If stockPendiente <> "" Then
                    tcValores.Text = Format(Double.Parse(stockPendiente), "#,##0")
                End If
            Else
                tcValores.Wrap = False
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Width = Unit.Pixel(40)
            tcValores.Wrap = False
            tcValores.Text = stockConsignacion
            If Tipo = 4 Or Tipo = 6 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
                If stockConsignacion <> "" Then
                    tcValores.Text = Format(Double.Parse(stockConsignacion), "#,##0")
                End If
            Else
                tcValores.Wrap = False
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Width = Unit.Pixel(40)
            tcValores.Wrap = False
            tcValores.Text = NMeses
            If Tipo = 4 Or Tipo = 6 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
                If NMeses <> "" Then
                    tcValores.Text = Format(Double.Parse(NMeses), "#,##0.0")
                End If
            Else
                tcValores.Wrap = False
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)
            If Tipo = 3 Then
                trDatos.CssClass = "tbl-DataGridHeader"
            ElseIf Tipo = 6 Then
                trDatos.CssClass = "tbl-DataGridHeader"
            Else
                trDatos.CssClass = "tbl-DataGridItem"
            End If
        End If
        Return trDatos
    End Function

    Private Sub ibFamiliaxProveedor_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibFamiliaxProveedor.Click
        plFamilia.Visible = False
        plProveedor.Visible = True
    End Sub

    Private Sub ibProveedorxFamilia_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibProveedorxFamilia.Click
        plFamilia.Visible = True
        plProveedor.Visible = False
    End Sub

    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click
        Dim xMes As String
        Dim xAño As String

        If plFamilia.Visible = True Then
            xMes = ddlMesFamilia.SelectedItem.Text
            xAño = ddlAnoFamilia.SelectedItem.Text
        Else
            xMes = ddlMesProveedor.SelectedItem.Text
            xAño = ddlAnoProveedor.SelectedItem.Text
        End If
        Dim sTableHeader As String = "Análisis Venta Volumen 12 Meses  -  Período " & xMes & "/" & xAño

        ' Nueva instancia del Informe
        Dim xlsResultado As Table = Session("myTable")

        ' Agregar encabezado del informe
        Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

        Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

        ' Configuracion de impresion
        Exportar.PageScale = 80
        Exportar.PageLayout = "Landscape"

        ' Encabezado y Pie de Pagina
        Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;Venta Volumen 12 Meses - " & xMes & "/" & xAño)
        Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

        ' Exportar
        Exportar.TableToExcel(xlsResultado)
        Exportar.SaveToClient(Response)
    End Sub

    Private Sub ibSendFamilia_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibSendFamilia.Click
        GeneraTabla()
    End Sub

    Private Sub btSendProveedor_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSendProveedor.Click
        GeneraTabla()
    End Sub

    Private Sub ibVolver_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibVolver.Click
        If Session("LAST_VISIBLE") = "FAMILIA" Then
            plFamilia.Visible = True
            lbNombreCentro.Visible = False
        Else
            plProveedor.Visible = True
            lbNombreCentro.Visible = False
        End If
    End Sub
End Class
