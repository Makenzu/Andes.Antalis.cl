Public Class vta_subfamilias
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList

    Dim totMes(12) As Integer

    Dim ano_periodo As Int16
    Dim mes_periodo As Int16

    Dim tUserInfo As t_Usuario
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton

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

        Page.Server.ScriptTimeout = 90

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
    
        Dim cod_prodmanager As Object

        lbErrors.Text = ""
        btSend.Attributes.Add("onClick", "javascript:noDblClick(document.all.disbtn,this);")



        If Not Page.IsPostBack Then

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

            'ano_periodo = Year(Date.Now)
            'mes_periodo = Month(Date.Now)

        Else

            ano_periodo = CInt(ddlAno.SelectedItem.Value)
            mes_periodo = CInt(ddlMes.SelectedItem.Value)

            lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo

            Try


                If tUserInfo.perfil.Trim = P_PRODMANAGER Then
                    cod_prodmanager = tUserInfo.codigo
                    lbNota.Visible = True
                    lbNota.Text = "* Mostrando solo Subfamilias propias."
                ElseIf tUserInfo.perfil.Trim = P_EJECUTIVO Then
                    cod_prodmanager = DBNull.Value
                Else
                    ' USER NO AUTORIZADO
                    Response.Redirect("default.aspx", True)
                End If


                Dim cod_filial As String = tUserInfo.codigoFilial
                Dim cod_sucursal As String = tUserInfo.codigoSucursal

                dgResultado.DataSource = Nothing
                dgResultado.DataBind()

                dgResultado.DataSource = ventas.vta_x_subfamilia("CAN", cod_prodmanager, mes_periodo, ano_periodo, cod_filial, cod_sucursal)
                dgResultado.DataBind()

            Catch ex As Exception
                lbErrors.Text = "ERRORES EN PAGINA: " & Err.Description
                Err.Clear()
                ' Throw ex
            Finally
            End Try

        End If



  
    End Sub


    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, _
ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound

        Dim i As Int16

        If e.Item.ItemType = ListItemType.Header Then
            ' ENCABEZADO DE UTIMOS 12 MESES
            Dim x As Int16
            x = 11
            For i = 1 To 12
                If (mes_periodo - i) >= 1 Then
                    e.Item.Cells(i).Text = MonthName(mes_periodo - i, True)
                Else
                    e.Item.Cells(i).Text = MonthName(mes_periodo + x, True)
                End If
                x -= 1
            Next
            'RESET TOTALES
            For i = 0 To totMes.Length - 1
                totMes(i) = 0
            Next

        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            ' CALCULAR TOTALES
            For i = 1 To 12
                totMes(i) += e.Item.Cells(i).Text
            Next


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

        If e.Item.ItemType = ListItemType.Footer Then
            ' SHOW TOTALES
            For i = 1 To 12
                e.Item.Cells(i).Text = totMes(i)
            Next
        End If

    End Sub


End Class
