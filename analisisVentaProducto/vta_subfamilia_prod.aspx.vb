Public Class vta_subfamilia_prod
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents hlVtaSubFam As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lbSubfamilia As System.Web.UI.WebControls.Label

    ' Para calculo de totales
    Dim totMesAct As Integer = 0
    Dim totMesUno As Integer = 0
    Dim totMesDos As Integer = 0
    Dim totMesTres As Integer = 0
    Dim totMesCuatro As Integer = 0
    Dim totMesCinco As Integer = 0
    Dim totMesSeis As Integer = 0
    Dim totMesSiete As Integer = 0
    Dim totMesOcho As Integer = 0
    Dim totMesNueve As Integer = 0
    Dim totMesDiez As Integer = 0
    Dim totMesOnce As Integer = 0
    Dim totMesDoce As Integer = 0

    Dim mes_periodo As Int16
    Dim ano_periodo As Int16
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label

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

        Dim nom_promotora
        lbErrors.Text = ""

        Dim tUserInfo As usuario.t_Usuario

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Try

            If Trim(Request("sf")) = "" Then
                Err.Description = "Faltan parametros para poder ejecutar la consulta."
                Err.Raise(vbObjectError + 512 + 10, "vta_x_subfamilia_item", Err.Description)
            End If

            mes_periodo = CStr(Request("ms"))
            ano_periodo = CStr(Request("an"))
            Dim cod_filial As String = tUserInfo.codigoFilial
            Dim cod_sucursal As String = tUserInfo.codigoSucursal

            lbSubfamilia.Text = Utiles.get_subfamilia_name(Trim(Request("sf")))

            dgResultado.DataSource = ventas.vta_x_subfamilia_item(Trim(Request("sf")), "CAN", mes_periodo, ano_periodo, cod_filial, cod_sucursal)
            dgResultado.DataBind()

            hlVtaSubFam.NavigateUrl = "vta_subfamilias.aspx"

            lbFecha.Text = MonthName(mes_periodo) & " , " & ano_periodo

        Catch ex As Exception
            lbErrors.Text = "ERRORES EN PAGINA: " & Err.Description
            Err.Clear()
            ' Throw ex
        Finally
        End Try
    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, _
ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound


        Dim cl As New System.Globalization.CultureInfo("es-CL")

        If e.Item.ItemType = ListItemType.Header Then
            ' ENCABEZADO DE UTIMOS 12 MESES
            Dim i, x As Int16
            x = 11
            For i = 1 To 12
                If (mes_periodo - i) >= 1 Then
                    e.Item.Cells(i).Text = MonthName(mes_periodo - i, True)
                Else
                    e.Item.Cells(i).Text = MonthName(mes_periodo + x, True)
                End If
                x -= 1
            Next
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

            totMesUno += CInt(e.Item.Cells(1).Text)
            totMesDos += e.Item.Cells(2).Text
            totMesTres += e.Item.Cells(3).Text
            totMesCuatro += e.Item.Cells(4).Text
            totMesCinco += e.Item.Cells(5).Text
            totMesSeis += e.Item.Cells(6).Text
            totMesSiete += e.Item.Cells(7).Text
            totMesOcho += e.Item.Cells(8).Text
            totMesNueve += e.Item.Cells(9).Text
            totMesDiez += e.Item.Cells(10).Text
            totMesOnce += e.Item.Cells(11).Text
            totMesDoce += e.Item.Cells(12).Text

            e.Item.Cells(1).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(1).Text))
            e.Item.Cells(2).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(2).Text))
            e.Item.Cells(3).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(3).Text))
            e.Item.Cells(4).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(4).Text))
            e.Item.Cells(5).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(5).Text))
            e.Item.Cells(6).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(6).Text))
            e.Item.Cells(7).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(7).Text))
            e.Item.Cells(8).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(8).Text))
            e.Item.Cells(9).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(9).Text))
            e.Item.Cells(10).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(10).Text))
            e.Item.Cells(11).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(11).Text))
            e.Item.Cells(12).Text = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(12).Text))
        End If

        If e.Item.ItemType = ListItemType.Footer Then

            e.Item.Cells(0).Text = "Totales : "
            e.Item.Cells(1).Text = String.Format(cl, "{0:N0}", CInt(totMesUno))
            e.Item.Cells(2).Text = String.Format(cl, "{0:N0}", CInt(totMesDos))
            e.Item.Cells(3).Text = String.Format(cl, "{0:N0}", CInt(totMesTres))
            e.Item.Cells(4).Text = String.Format(cl, "{0:N0}", CInt(totMesCuatro))
            e.Item.Cells(5).Text = String.Format(cl, "{0:N0}", CInt(totMesCinco))
            e.Item.Cells(6).Text = String.Format(cl, "{0:N0}", CInt(totMesSeis))
            e.Item.Cells(7).Text = String.Format(cl, "{0:N0}", CInt(totMesSiete))
            e.Item.Cells(8).Text = String.Format(cl, "{0:N0}", CInt(totMesOcho))
            e.Item.Cells(9).Text = String.Format(cl, "{0:N0}", CInt(totMesNueve))
            e.Item.Cells(10).Text = String.Format(cl, "{0:N0}", CInt(totMesDiez))
            e.Item.Cells(11).Text = String.Format(cl, "{0:N0}", CInt(totMesOnce))
            e.Item.Cells(12).Text = String.Format(cl, "{0:N0}", CInt(totMesDoce))

        End If

    End Sub

End Class
