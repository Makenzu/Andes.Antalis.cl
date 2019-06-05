Imports Exportador

Public Class clientes_x_promotora
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents ddlPromotoras As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tblDetalleAnalisis As System.Web.UI.WebControls.Table
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents mytable As System.Web.UI.WebControls.Table
    Protected WithEvents ibExportar2 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label

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


        Dim cod_promotora As String

        If Page.IsPostBack = False Then
            despliegaPromotoras()
        Else


            Dim DSResultados As DataSet
            Dim Resultados As DataTable
            Dim tUserInfo As usuario.t_Usuario
            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

            If Not IsNothing(Request("ddlFilial")) Then
                tUserInfo.codigoFilial = Request("ddlFilial")
            End If


            If tUserInfo.codigoFilial = "CHI" Then
                tUserInfo.codigoSucursal = "001"

            ElseIf tUserInfo.codigoFilial = "PER" Then
                tUserInfo.codigoSucursal = "002"

            ElseIf tUserInfo.codigoFilial = "BOL" Then
                tUserInfo.codigoSucursal = "003"
            End If


            cod_promotora = Request("ddlPromotoras")


            If cod_promotora = "" Then
                lbErrors.Visible = True
                lbErrors.Text = "Debe seleccionar ejec. comercial."
                Exit Sub
            End If

            DSResultados = Utiles.buscaClientesPromotora(tUserInfo.codigoFilial, cod_promotora)
            Resultados = DSResultados.Tables(0)

            If Resultados.Rows.Count = 0 Then
                lbErrors.Text = "No se encontraron items."
                Exit Sub
            End If


            Crea_tabla(Resultados)

            ibExportar2.Visible = True


        End If


    End Sub

#Region " DESPLIEGA PROMOTORAS "
    Private Sub despliegaPromotoras()

        Dim tUserInfo As usuario.t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        With ddlPromotoras
            .DataSource = Utiles.ObtienePromotoras(tUserInfo.codigoFilial)
            .DataTextField = "col2"
            .DataValueField = "col1"
            .DataBind()
        End With

        ddlPromotoras.Items.Add(New ListItem("- Seleccione -", ""))
        ddlPromotoras.SelectedValue = ""

    End Sub
#End Region

#Region "CREA TABLA"
    Private Function Crea_tabla(ByVal dtResult As DataTable)


        Dim mycell As TableCell
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim fila As TableRow
        Dim file As TableRow


        Dim i As Int16
        i = 0

        file = New TableRow


        mycell = New TableCell
        mycell.Text = "CLIENTE"
        mycell.HorizontalAlign = HorizontalAlign.Left
        file.CssClass = "tbl-DataGridHeader"
        file.Cells.Add(mycell)

        mycell = New TableCell
        mycell.Text = "RAZON SOCIAL"
        mycell.HorizontalAlign = HorizontalAlign.Left
        file.CssClass = "tbl-DataGridHeader"
        file.Cells.Add(mycell)

        mycell = New TableCell
        mycell.Text = "RUT"
        mycell.HorizontalAlign = HorizontalAlign.Left
        file.CssClass = "tbl-DataGridHeader"
        file.Cells.Add(mycell)

        mycell = New TableCell
        mycell.Text = "DIRECCION"
        mycell.HorizontalAlign = HorizontalAlign.Left
        file.CssClass = "tbl-DataGridHeader"
        file.Cells.Add(mycell)

        mycell = New TableCell
        mycell.Text = "COMUNA"
        mycell.HorizontalAlign = HorizontalAlign.Left
        file.CssClass = "tbl-DataGridHeader"
        file.Cells.Add(mycell)

        mycell = New TableCell
        mycell.Text = "CIUDAD"
        mycell.HorizontalAlign = HorizontalAlign.Left
        file.CssClass = "tbl-DataGridHeader"
        file.Cells.Add(mycell)

        mycell = New TableCell
        mycell.Text = "TELEFONO"
        mycell.HorizontalAlign = HorizontalAlign.Left
        file.CssClass = "tbl-DataGridHeader"
        file.Cells.Add(mycell)

        mycell = New TableCell
        mycell.Text = "FAX"
        mycell.HorizontalAlign = HorizontalAlign.Left
        file.CssClass = "tbl-DataGridHeader"
        file.Cells.Add(mycell)

        mycell = New TableCell
        mycell.Text = "MOVIL"
        mycell.HorizontalAlign = HorizontalAlign.Left
        file.CssClass = "tbl-DataGridHeader"
        file.Cells.Add(mycell)

        mytable.Rows.Add(file)

        While i <= dtResult.Rows.Count - 1

            fila = New TableRow


            mycell = New TableCell
            mycell.Text = dtResult.Rows(i).Item("cod_cliente")
            mycell.HorizontalAlign = HorizontalAlign.Left
            fila.CssClass = "tbl-DataGridItem"
            fila.Cells.Add(mycell)

            mycell = New TableCell
            mycell.Text = dtResult.Rows(i).Item("nom_cliente")
            mycell.HorizontalAlign = HorizontalAlign.Left
            fila.CssClass = "tbl-DataGridItem"
            fila.Cells.Add(mycell)

            mycell = New TableCell
            mycell.Text = dtResult.Rows(i).Item("rut_cliente")
            mycell.HorizontalAlign = HorizontalAlign.Left
            fila.CssClass = "tbl-DataGridItem"
            fila.Cells.Add(mycell)

            mycell = New TableCell
            mycell.Text = dtResult.Rows(i).Item("direccion")
            mycell.HorizontalAlign = HorizontalAlign.Left
            fila.CssClass = "tbl-DataGridItem"
            fila.Cells.Add(mycell)

            mycell = New TableCell
            mycell.Text = dtResult.Rows(i).Item("des_comuna")
            mycell.HorizontalAlign = HorizontalAlign.Left
            fila.CssClass = "tbl-DataGridItem"
            fila.Cells.Add(mycell)

            mycell = New TableCell
            mycell.Text = dtResult.Rows(i).Item("des_ciudad")
            mycell.HorizontalAlign = HorizontalAlign.Left
            fila.CssClass = "tbl-DataGridItem"
            fila.Cells.Add(mycell)

            mycell = New TableCell
            mycell.Text = dtResult.Rows(i).Item("telefono")
            mycell.HorizontalAlign = HorizontalAlign.Left
            fila.CssClass = "tbl-DataGridItem"
            fila.Cells.Add(mycell)

            mycell = New TableCell
            mycell.Text = dtResult.Rows(i).Item("fax")
            mycell.HorizontalAlign = HorizontalAlign.Left
            fila.CssClass = "tbl-DataGridItem"
            fila.Cells.Add(mycell)

            mycell = New TableCell
            mycell.Text = dtResult.Rows(i).Item("movil")
            mycell.HorizontalAlign = HorizontalAlign.Left
            fila.CssClass = "tbl-DataGridItem"
            fila.Cells.Add(mycell)

            i = i + 1

            mytable.Rows.Add(fila)

            Session("myTable") = mytable

        End While

    End Function
#End Region

#Region "BOTON EXPORTAR A EXCEL"


#End Region

    Private Sub ibExportar2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar2.Click
        Dim sTableHeader As String = "Clientes por Ejec. Comercial "
        ' Nueva instancia del Informe
        Dim xlsResultado As Table = Session("myTable")

        ' Agregar encabezado del informe
        Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

        Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

        ' Configuracion de impresion
        Exportar.PageScale = 80
        Exportar.PageLayout = "Landscape"

        ' Encabezado y Pie de Pagina
        Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "")
        Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

        ' Exportar
        Exportar.TableToExcel(xlsResultado)
        Exportar.SaveToClient(Response)
    End Sub
End Class
