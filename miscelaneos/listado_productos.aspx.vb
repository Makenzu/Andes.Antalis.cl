Imports Exportador

Public Class res_listado_productos
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label

    ' Para calculo de totales
    Dim totales(8) As Double
    Dim totRows As Integer


    Dim tUserInfo As usuario.t_Usuario
    Protected WithEvents tblResultados As System.Web.UI.WebControls.Table
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents DropDownList4 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents cmbFamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSubFamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cblSubFamilia As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btSend As System.Web.UI.HtmlControls.HtmlInputImage
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSucursal As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim xCodigoFamilia As String
    Dim msgConsultaSap As String

    Dim codFilial As String
    Dim codSucursal As String

    Private Sub btSend_ServerClick(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSend.ServerClick
        Dim xCodFamilia As String
        Dim xCodSubFamilia As String
        Dim xYear As String
        Dim xMes As String
        Dim i As Integer
        Dim j As Integer
        Dim Resultados As DataTable
        Dim xAux As DataTable
        Dim DSResultados As DataSet
        Dim xSubFamiliaActual As String
        Dim trDatos As TableRow
        Dim tcValores As TableCell
        Dim totalSubfamilia As Double
        Dim acumSubfamilia As Double
        Dim txtSubFamilia As String
        Dim sw As Integer

        Dim tUserInfo As usuario.t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        'tUserInfo.codigoFilial = Request("ddlFilial")

        'AGS-20080620: Agregar listbox con sucursales
        tUserInfo.codigoSucursal = ddlSucursal.SelectedValue

        'If tUserInfo.codigoFilial = "CHI" Then
        '    tUserInfo.codigoSucursal = "001"

        'ElseIf tUserInfo.codigoFilial = "PER" Then
        '    tUserInfo.codigoSucursal = "002"

        'ElseIf tUserInfo.codigoFilial = "BOL" Then
        '    tUserInfo.codigoSucursal = "003"

        'End If

        xCodFamilia = cmbFamilia.SelectedValue
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
        xYear = ddlYear.SelectedValue
        xMes = ddlMes.SelectedValue
        If xYear = 2005 And xMes < 8 Then
            lbErrors.Text = "No se puede obtener información para el período seleccionado."
            Exit Sub
        End If
        DSResultados = Utiles.buscaListaPrecios(xYear, xMes, tUserInfo.codigoFilial, tUserInfo.codigoSucursal, xCodFamilia, xCodSubFamilia)
        Resultados = DSResultados.Tables(0)

        '------------------------------------------------------------------------------------------------------
        'Intentamos recoger indicadores de stock y pedidos pendientes desde SAP
        'Si la consulta se realiza en mes abierto, entonces obtenemos valores de pedidos pendientes y de stock
        'desde SAP.
        '------------------------------------------------------------------------------------------------------

        'If xMes = Now.Month Then
        '    Dim tblStockMaterial As DataTable = New DataTable

        '    Try
        '        Dim sociedad, centro As String
        '        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        '        'Segun el código de sucursal, obtenemos equivalencia de sociedad y centro de distribución.
        '        seteaParametrosConsulta(tUserInfo.codigoSucursal, sociedad, centro)

        '        'consultamos a SAP stock y pedidos pendientes expresados en unidad de medida base
        '        tblStockMaterial = materiales.obtieneStockMateriales(sociedad, centro, Resultados)

        '        'modificamos los valores de la ultima carga de datos Andes/Iddeo, con info actualizada de 
        '        'stock y pedidos pendientes.

        '        For i = 0 To Resultados.Rows.Count - 1
        '            If Trim(Resultados.Rows(i).Item("cod_producto")) = tblStockMaterial.Rows(i).Item("material") Then
        '                Resultados.Rows(i).Item("val_stock_actual") = tblStockMaterial.Rows(i).Item("stock")
        '                Resultados.Rows(i).Item("val_cant_pend") = tblStockMaterial.Rows(i).Item("pendiente")                        ' Resultados.Rows(i).Item("ped_pend") = tblStockMaterial.Rows(i).Item("pendiente")
        '            End If
        '        Next
        '        msgConsultaSap = ":: Fuente stock / pendientes: <img align=""absmiddle"" src=""/images/sap.jpg"" border=""0"" alt=""Datos obtenidos en línea desde SAP"">"
        '    Catch ex As Exception
        '        msgConsultaSap = ":: Fuente stock / pendientes: <b>Registro ANDES</b>."
        '    End Try
        'Else
        '    msgConsultaSap = ":: Fuente stock / pendientes: <b>Registro ANDES</b>."
        'End If
        '------------------------------------------------------------------------------------------------------

        msgConsultaSap = ":: Fuente stock / pendientes: <b>Registro ANDES</b>."

        If Resultados.Rows.Count = 0 Then
            lbErrors.Text = "No se encontraron items para la familia."
            Exit Sub
        End If

        Dim myTableRow As TableRow
        Dim myTableCell As TableCell
        myTableRow = New TableRow
        myTableCell = New TableCell

        '-----------------------------------------------------------------------------------------------------
        'Agregamos cabecera para datos de consulta de datos.
        Dim msgFechaConsulta As String = ":: Consulta: " + Format(Now, "dddd dd/MMM/yyyy HH:mm:ss")
        Dim fechaActualizacion As String

        Try
            fechaActualizacion = ":: Datos actualizados al: " & Format(obtieneFechaActualizacion(xYear, xMes, tUserInfo.codigoSucursal, "PRODUCTOS"), "dddd dd/MMM/yyyy HH:mm:ss")
        Catch ex As Exception
            fechaActualizacion = ":: Datos actualizados al: (no determinado)"
        End Try
        '-----------------------------------------------------------------------------------------------------

        myTableCell.Text = msgFechaConsulta + "<br>" + fechaActualizacion + "<br><br>" + msgConsultaSap
        myTableCell.HorizontalAlign = HorizontalAlign.Left
        myTableCell.ColumnSpan = 8
        myTableCell.CssClass = "info-informes"
        myTableRow.Cells.Add(myTableCell)
        tblResultados.Rows.Add(myTableRow)

        xSubFamiliaActual = ""
        xAux = DSResultados.Tables(1)
        If xAux.Rows.Count > 0 Then
            'Label4.Text = "Precios al " & Format(CDate(xAux.Rows(0).Item(0)), "dd-MM-yyyy")
        Else
            Label4.Text = "No hay información de la última actualización."
        End If

        For i = 0 To Resultados.Rows.Count - 1
            If xSubFamiliaActual <> CStr(Resultados.Rows(i).Item("cod_subfamilia")) Then
                If i > 0 Then
                    'Coloco los totales
                    trDatos = Crea_Linea(2, "", "ITEMS SUBFAMILIA: " & totalSubfamilia, "", "", "", "", "", "")
                    tblResultados.Rows.Add(trDatos)
                ElseIf i = 0 Then
                    'Coloco el título
                    trDatos = Crea_Linea(2, "", "", "Precios al " & Format(CDate(xAux.Rows(0).Item(0)), "dd-MM-yyyy"), "", "", "", "", "")
                    trDatos.CssClass = "tbl-FechaHeader"
                    tblResultados.Rows.Add(trDatos)
                    trDatos = Crea_Linea(1, UCase(Resultados.Rows(i).Item("cod_familia")), "", "", "", "", "", "", "")
                    tblResultados.Rows.Add(trDatos)
                End If
                'Coloco encabezado
                trDatos = Crea_Linea(5, " ", " ", " ", " ", " ", " ", " ", " ")
                tblResultados.Rows.Add(trDatos)

                trDatos = Crea_Linea(2, "", UCase(Resultados.Rows(i).Item("cod_subfamilia")), IIf(UCase(Resultados.Rows(i).Item("nom_usuario")) = "", "SIN ENCARGADO", "ENCARGADO : " & UCase(Resultados.Rows(i).Item("nom_usuario"))), "", "", "", "", "")
                tblResultados.Rows.Add(trDatos)

                If tUserInfo.codigoFilial = "CHI" Then
                    trDatos = Crea_Linea(3, "", "CÓDIGO", "DESCRIPCIÓN", "UMB", "PRECIO LISTA USD", "STOCK", "PENDIENTE", "TRÁNSITO")
                Else
                    trDatos = Crea_Linea(3, "", "CÓDIGO", "DESCRIPCIÓN", "UMB", "PRECIO LISTA USD", "STOCK", "PENDIENTE", "TRÁNSITO", "PRECIO LISTA MON SOC")
                End If

                tblResultados.Rows.Add(trDatos)
                totalSubfamilia = 0
                xSubFamiliaActual = Resultados.Rows(i).Item("cod_subfamilia")
            End If
            totalSubfamilia = totalSubfamilia + 1
            acumSubfamilia = acumSubfamilia + 1

            'AGS-20080621: Si son sucs de BOL(003, 004) ==> Mostrar precio lista en moneda local, para todos los otros en USD
            If tUserInfo.codigoFilial = "CHI" Then
                trDatos = Crea_Linea(4, "", Resultados.Rows(i).Item("cod_producto"), Resultados.Rows(i).Item("des_producto"), Resultados.Rows(i).Item("cod_umb"), Resultados.Rows(i).Item("val_precio_lista"), Resultados.Rows(i).Item("val_stock_actual"), Resultados.Rows(i).Item("val_cant_pend"), Resultados.Rows(i).Item("val_stock_transito"), Nothing)
            Else
                trDatos = Crea_Linea(4, "", Resultados.Rows(i).Item("cod_producto"), Resultados.Rows(i).Item("des_producto"), Resultados.Rows(i).Item("cod_umb"), Resultados.Rows(i).Item("val_precio_lista"), Resultados.Rows(i).Item("val_stock_actual"), Resultados.Rows(i).Item("val_cant_pend"), Resultados.Rows(i).Item("val_stock_transito"), Resultados.Rows(i).Item("val_precio_lista_msoc"))
            End If

            trDatos.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If (i Mod 2 = 0) Then
                trDatos.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF';")
                trDatos.CssClass = "tbl-DataGridItemAlternating"
            Else
                trDatos.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue';")
                trDatos.CssClass = "tbl-DataGridItem"
            End If

            tblResultados.Rows.Add(trDatos)
        Next

        'Coloco los totales de la ultima subfamilia
        trDatos = Crea_Linea(2, "", "ITEMS SUBFAMILIA: " & totalSubfamilia, "", "", "", "", "", "")
        tblResultados.Rows.Add(trDatos)
        trDatos = Crea_Linea(5, " ", " ", " ", " ", " ", " ", " ", " ")
        tblResultados.Rows.Add(trDatos)
        'Coloco los totales generales
        If ddlSubFamilia.SelectedValue = "-100" Then
            trDatos = Crea_Linea(1, "ITEMS FAMILIA: " & acumSubfamilia, "", "", "", "", "", "", "")
        Else
            trDatos = Crea_Linea(1, "ITEMS SELECCIÓN: " & acumSubfamilia, "", "", "", "", "", "", "")
        End If
        tblResultados.Rows.Add(trDatos)
        If ddlSubFamilia.SelectedValue = "-200" Then
            For i = 0 To ddlSubFamilia.Items.Count - 1
                ddlSubFamilia.Items(i).Selected = False
            Next
            ddlSubFamilia.Items(0).Selected = True
            ddlSubFamilia.Visible = True
            cblSubFamilia.Visible = False
            Label2.Text = ""
        End If
        ibExportar.Visible = True

        Session("myTable") = tblResultados
    End Sub

    Private Function Crea_Linea(ByVal Tipo As Integer, ByVal A1 As String, ByVal A2 As String, ByVal A3 As String, ByVal A4 As String, ByVal A5 As String, ByVal A6 As String, ByVal A7 As String, ByVal A8 As String, Optional ByVal A5_2 As String = Nothing) As TableRow
        Dim trDatos As TableRow
        Dim tcValores As TableCell

        trDatos = New TableRow

        If Tipo = 1 Or Tipo = 5 Then 'Titulo de Familia o Espacio en Blanco
            tcValores = New TableCell
            tcValores.Text = A1
            If Not A5_2 Is Nothing Then
                tcValores.ColumnSpan = 8
            Else
                tcValores.ColumnSpan = 9
            End If
            If Tipo = 5 Then
                tcValores.Height = Unit.Pixel(20)
                trDatos.Cells.Add(tcValores)
                trDatos.CssClass = "tbl-DataGridItemAlternating"
            Else
                trDatos.Cells.Add(tcValores)
                trDatos.CssClass = "tbl-DataGridHeaderAlternating"
            End If
        ElseIf Tipo = 2 Then 'Título de Subfamilia
            tcValores = New TableCell
            tcValores.Text = A1
            tcValores.CssClass = "tbl-DataGridHeader"
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = A2
            tcValores.ColumnSpan = 3
            tcValores.CssClass = "tbl-DataGridHeader"
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = A3
            If Not A5_2 Is Nothing Then
                tcValores.ColumnSpan = 4
            Else
                tcValores.ColumnSpan = 5
            End If
            tcValores.HorizontalAlign = HorizontalAlign.Right
            tcValores.CssClass = "tbl-EncargadoHeader"
            trDatos.Cells.Add(tcValores)
        ElseIf Tipo = 3 Or Tipo = 4 Then
            tcValores = New TableCell
            tcValores.Text = A1
            tcValores.Width = Unit.Pixel(30)
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = A2
            If Tipo = 3 Then
                tcValores.HorizontalAlign = HorizontalAlign.Center
            End If
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = A3
            If Tipo = 3 Then
                tcValores.HorizontalAlign = HorizontalAlign.Center
            End If
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = A4
            If Tipo = 3 Then
                tcValores.HorizontalAlign = HorizontalAlign.Center
            End If
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = A5
            If Tipo = 4 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
                tcValores.Text = Format(Double.Parse(A5), ",###,###,##0.00")
            Else
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)

            If Not A5_2 Is Nothing Then
                tcValores = New TableCell
                tcValores.Text = A5_2
                If Tipo = 4 Then
                    tcValores.HorizontalAlign = HorizontalAlign.Right
                    tcValores.Text = Format(Double.Parse(A5_2), ",###,###,##0.00")
                Else
                    tcValores.HorizontalAlign = HorizontalAlign.Right
                End If
                trDatos.Cells.Add(tcValores)
            End If

            tcValores = New TableCell
            tcValores.Text = A6
            If Tipo = 4 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
                tcValores.Text = Format(Double.Parse(A6), ",###,###,##0")
            Else
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = A7
            If Tipo = 4 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
                tcValores.Text = Format(Double.Parse(A7), ",###,###,##0")
            Else
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)

            tcValores = New TableCell
            tcValores.Text = A8
            If Tipo = 4 Then
                tcValores.HorizontalAlign = HorizontalAlign.Right
                tcValores.Text = Format(Double.Parse(A8), ",###,###,##0")
            Else
                tcValores.HorizontalAlign = HorizontalAlign.Right
            End If
            trDatos.Cells.Add(tcValores)
            If Tipo = 3 Then
                trDatos.CssClass = "tbl-DataGridHeader"
            Else
                trDatos.CssClass = "tbl-DataGridItem"
            End If
        End If
        Return trDatos
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim xDatos As DataTable
        Dim xAño As Integer
        Dim xMes As Integer
        Dim i As Integer

        'AGS-20080620: Agregar listbox con sucursales
        If Not IsPostBack Then
            ddlSucursal.DataSource = Utiles.obtieneSucursales(tUserInfo.codigoFilial)
            ddlSucursal.DataTextField = "des_sucursal"
            ddlSucursal.DataValueField = "cod_sucursal"
            ddlSucursal.DataBind()
        End If

        xAño = Year(Now)
        xMes = Month(Now)
        lbErrors.Text = ""
        If ddlYear.SelectedValue = "" Then
            ddlYear.Items.Add(xAño)
            ddlYear.Items(0).Value = xAño
            ddlYear.Items.Add(xAño - 1)
            ddlYear.Items(1).Value = xAño - 1
            ddlYear.Items.Add(xAño - 2)
            ddlYear.Items(2).Value = xAño - 2
            ddlMes.SelectedValue = xMes
            Label4.Text = ""
        End If
        If cmbFamilia.SelectedValue = "" Then
            cblSubFamilia.Visible = False
            xDatos = Utiles.buscaFamilias(tUserInfo.codigoFilial)
            cmbFamilia.DataSource = xDatos
            cmbFamilia.DataTextField = "COL1"
            cmbFamilia.DataValueField = "cod_familia"
            cmbFamilia.DataBind()
            Label1.Text = cmbFamilia.SelectedValue
            xCodigoFamilia = cmbFamilia.SelectedValue
            xDatos = Utiles.buscaSubFamilias(eModo.estandar, tUserInfo.codigoFilial, xCodigoFamilia)
            ddlSubFamilia.DataSource = xDatos
            ddlSubFamilia.DataTextField = "COL1"
            ddlSubFamilia.DataValueField = "cod_subfamilia"
            ddlSubFamilia.DataBind()
            ddlSubFamilia.Visible = True
            Label2.Text = ""
            Label4.Text = ""
        Else
            If ddlSubFamilia.SelectedValue <> Label3.Text Then
                ibExportar.Visible = False
                Label4.Text = ""
            End If
            If Label1.Text <> cmbFamilia.SelectedValue Then
                cblSubFamilia.Visible = False
                Label1.Text = cmbFamilia.SelectedValue
                xCodigoFamilia = cmbFamilia.SelectedValue
                xDatos = Utiles.buscaSubFamilias(eModo.estandar, tUserInfo.codigoFilial, xCodigoFamilia)
                ddlSubFamilia.DataSource = xDatos
                ddlSubFamilia.DataTextField = "COL1"
                ddlSubFamilia.DataValueField = "cod_subfamilia"
                ddlSubFamilia.DataBind()
                ddlSubFamilia.Visible = True
                Label2.Text = ""
                ibExportar.Visible = False
            Else
                If ddlSubFamilia.SelectedValue = "-200" And Label2.Text = "" Then
                    Label2.Text = "1"
                    ddlSubFamilia.Visible = False
                    xCodigoFamilia = cmbFamilia.SelectedValue
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
    End Sub

    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click
        Dim sTableHeader As String = "Listado de Precios  -  Período " & ddlMes.SelectedItem.Text & "/" & ddlYear.SelectedItem.Text

        ' Nueva instancia del Informe
        Dim xlsResultado As Table = Session("myTable")

        ' Agregar encabezado del informe
        Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

        Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

        ' Configuracion de impresion
        Exportar.PageScale = 80
        Exportar.PageLayout = "Landscape"

        ' Encabezado y Pie de Pagina
        Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;Listado de Precios - " & ddlMes.SelectedItem.Text & "/" & ddlYear.SelectedItem.Text)
        Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

        ' Exportar
        Exportar.TableToExcel(xlsResultado)
        Exportar.SaveToClient(Response)
    End Sub
End Class
