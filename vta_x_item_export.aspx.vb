Public Class vta_x_item_export
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents lbData As System.Web.UI.WebControls.Label
    Protected WithEvents plDatos As System.Web.UI.WebControls.Panel
    Protected WithEvents rbCodFam As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txFamilia As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbSubfam As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txSubfam As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbCodProd As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txCodProd As System.Web.UI.WebControls.TextBox
    Protected WithEvents plParams As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid

    Dim tUserInfo As usuario.t_Usuario
    Protected WithEvents lbExport As System.Web.UI.WebControls.LinkButton

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

        Page.Server.ScriptTimeout = 90

        If IsNothing(Session("USUARIO_AUTENTICADO")) = True Then
            Response.Redirect("logout.aspx")
            Response.End()
        Else
            tUserInfo = Session("USER_INFO")
        End If

        Dim ano_periodo As Int16
        Dim mes_periodo As Int16

        txFamilia.Attributes.Add("onClick", "javascript:wasClicked(this);")
        txSubfam.Attributes.Add("onClick", "javascript:wasClicked(this);")
        txCodProd.Attributes.Add("onClick", "javascript:wasClicked(this);")

        rbCodFam.Attributes.Add("onClick", "javascript:wasClicked(this);")
        rbSubfam.Attributes.Add("onClick", "javascript:wasClicked(this);")
        rbCodProd.Attributes.Add("onClick", "javascript:wasClicked(this);")


        If Not Page.IsPostBack Then

            'Llenar DorpDownList con Fechas
            ddlAno.Items.Add(Year(Date.Now))
            ddlAno.Items.Add(Year(Date.Now.AddYears(-1)))

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
                ddlMes.Items.Add(newListItem)

                If Month(Date.Now) - i = Month(Date.Now) Then ddlMes.Items(i).Selected = True
                x -= 1
            Next

            ano_periodo = Year(Date.Now)
            mes_periodo = Month(Date.Now)

        Else
            mes_periodo = CInt(ddlMes.SelectedItem.Value)
            ano_periodo = CInt(ddlAno.SelectedItem.Value)

            Dim cod_filial As String = tUserInfo.codigoFilial
            Dim cod_sucursal As String = tUserInfo.codigoSucursal

            Dim dtMarVta As New DataTable

            Try


                Dim cod_producto = txCodProd.Text.Trim
                Dim cod_subfamilia = txSubfam.Text.Trim
                Dim cod_familia = txFamilia.Text.Trim

                plDatos.Visible = True
                lbErrors.Text = ""



                ' LOAD DATAGRID
                dtMarVta = ventas.vta_x_item_12meses(cod_producto, cod_subfamilia, cod_familia, mes_periodo, ano_periodo, cod_filial, cod_sucursal)
                If dtMarVta.Rows.Count <= 0 Then
                    Err.Description = "No se encontraron datos para esta consulta."
                    Err.Raise(vbObjectError + 512 + 10, "no_results", Err.Description)
                End If
                dgResultado.DataSource = dtMarVta
                dgResultado.DataBind()



            Catch ex As Exception
                ' SHOW ERROR
                lbErrors.Text = "ERROR: " & Err.Description
                lbErrors.Visible = True
                Err.Clear()
                ' HIDE DATA
                plDatos.Visible = False
            Finally
                dtMarVta.Dispose()
            End Try


        End If

        lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo


    End Sub


    Private Sub lbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbExport.Click
        'export to excel

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        Page.EnableViewState = False

        Dim oStringWriter As New System.IO.StringWriter
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

        ' Me.ClearControls(dgResultado)
        dgResultado.RenderControl(oHtmlTextWriter)

        Response.Write(oStringWriter.ToString())

        Response.End()

    End Sub
End Class
