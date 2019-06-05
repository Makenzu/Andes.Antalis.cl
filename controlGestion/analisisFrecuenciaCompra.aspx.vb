Imports System.Data.SqlTypes
Imports Exportador

Public Class analisisFrecuenciaCompra
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ddlPromotoras As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlConcepto As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btConsulta As System.Web.UI.WebControls.ImageButton
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents ddlPeriodo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFrecuencia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlDependencia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents FreqCli As System.Web.UI.WebControls.DataGrid
    Protected WithEvents FreqProd As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Structure stUserSelection
        Dim periodo As String
        Dim codPromotora As String
        Dim codFrecuencia As String
        Dim numDependencia As Integer
    End Structure

    Dim userSelection As stUserSelection
    Dim valVtaPromTotFreq1, valVtaPromTotFreq2, valVtaPromTotFreq3, valVtaPromTotal As Double
    Dim valVtaProdsFreq1, valVtaProdsFreq2, valVtaProdsFreq3 As Double


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        'Put user code to initialize the page here
        If Session("TMP_ACCP_USER_SELECTION") Is Nothing Then
            With userSelection
                .codPromotora = Nothing
                .periodo = Nothing
            End With
            Session("TMP_ACCP_USER_SELECTION") = userSelection
        End If

        If Page.IsPostBack Then
            With userSelection
                .periodo = Request("ddlPeriodo")
                .codPromotora = Request("ddlPromotoras")
                .codFrecuencia = Request("ddlFrecuencia")
                .numDependencia = Request("ddlDependencia")
            End With

            Session("TMP_ACCP_USER_SELECTION") = userSelection
        End If

        With userSelection
            cargaPeriodos(.periodo)
            cargaPromotoras(.codPromotora)
            cargaDependencias(.numDependencia)
        End With
    End Sub

#Region " cargaPeriodos "
    Private Sub cargaPeriodos(ByVal periodo As String)
        Dim fecha As DateTime = Now

        ddlPeriodo.Items.Clear()
        Do
            ddlPeriodo.Items.Add(New ListItem(Format(fecha, "MMM ""/"" yyyy"), Format(fecha, "yyyyMM")))
            fecha = fecha.AddMonths(-1)
        Loop Until fecha.Year = 2004 And fecha.Month = 12

        If Not periodo Is Nothing Then
            ddlPeriodo.ClearSelection()
            ddlPeriodo.Items.FindByValue(periodo).Selected = True
        Else
            ddlPeriodo.Items(0).Selected = True
        End If
    End Sub
#End Region

#Region " cargaPromotoras "
    Private Sub cargaPromotoras(ByVal codigoPromotora As String)
        Dim tUserInfo As usuario.t_Usuario
        Dim cod_promotora, nom_promotora As String
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        With ddlPromotoras
            .DataSource = Utiles.ObtienePromotoras(tUserInfo.codigoFilial)
            .DataTextField = "col2"
            .DataValueField = "col1"
            .DataBind()
        End With

        ddlPromotoras.Items.Add(New ListItem("- Todas -", "*"))
        ddlPromotoras.Items.FindByValue("*").Selected = True

        If tUserInfo.perfil.Trim = P_PROMOTORA Then
            ddlPromotoras.ClearSelection()
            ddlPromotoras.Items.FindByValue(tUserInfo.codigoPromo).Selected = True
            ddlPromotoras.Enabled = False
        Else
            If Not codigoPromotora Is Nothing Then
                ddlPromotoras.ClearSelection()
                ddlPromotoras.Items.FindByValue(codigoPromotora).Selected = True
            End If
        End If
    End Sub
#End Region

    Private Sub cargaDependencias(ByVal numeroDependencia As Integer)
        Dim tUserInfo As usuario.t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        With ddlDependencia
            .DataSource = obtieneDependenciasFreq(ddlPeriodo.SelectedValue.Substring(0, 4), ddlPeriodo.SelectedValue.Substring(4, 2), tUserInfo.codigoFilial, tUserInfo.codigoSucursal)
            .DataTextField = "num_dependencia"
            .DataValueField = "num_dependencia"
            .DataBind()
        End With

        ddlDependencia.Items.Add(New ListItem("- Todas -", "999"))
        ddlDependencia.Items.FindByValue("999").Selected = True

        If numeroDependencia <> 0 Then
            ddlDependencia.ClearSelection()
            ddlDependencia.Items.FindByValue(numeroDependencia).Selected = True
        End If
    End Sub

    Private Sub btConsulta_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btConsulta.Click
        Dim userInfo As usuario.t_Usuario
        userInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        ibExportar.Visible = True
        FreqProd.Visible = False
        valVtaPromTotal = 0

        Dim userSelection As stUserSelection
        userSelection = Session("TMP_ACCP_USER_SELECTION")

        If userSelection.codPromotora Is Nothing Or userSelection.codPromotora = "" Then
            userSelection.codPromotora = userInfo.codigoPromo
        End If

        Dim dsDatos As DataSet
        With userSelection
            dsDatos = obtieneAnalisisFrecuenciaCompra(.periodo.Substring(0, 4), _
                                                .periodo.Substring(4, 2), _
                                                userInfo.codigoFilial, _
                                                userInfo.codigoSucursal, _
                                                .codPromotora, _
                                                .codFrecuencia, _
                                                .numDependencia)
        End With

        Me.FreqCli.DataSource = dsDatos
        Me.FreqCli.DataBind()
    End Sub

    Private Sub FreqCli_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles FreqCli.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                valVtaPromTotFreq1 += CDbl(e.Item.Cells(7).Text)
                valVtaPromTotFreq2 += CDbl(e.Item.Cells(8).Text)
                valVtaPromTotFreq3 += CDbl(e.Item.Cells(9).Text)
                valVtaPromTotal += CDbl(e.Item.Cells(10).Text)
            Case ListItemType.Footer
                e.Item.Cells(0).Text = "Total Ventas Promedio"
                e.Item.Cells(0).ColumnSpan = 4
                e.Item.Cells(1).Visible = False
                e.Item.Cells(2).Visible = False
                e.Item.Cells(3).Visible = False
                e.Item.Cells(7).Text = Format(valVtaPromTotFreq1, "#,##0.00")
                e.Item.Cells(8).Text = Format(valVtaPromTotFreq2, "#,##0.00")
                e.Item.Cells(9).Text = Format(valVtaPromTotFreq3, "#,##0.00")
                e.Item.Cells(10).Text = Format(valVtaPromTotal, "#,##0.00")
        End Select
    End Sub

    Private Sub FreqCli_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles FreqCli.ItemCommand
        Dim userInfo As usuario.t_Usuario
        userInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim userSelection As stUserSelection
        userSelection = Session("TMP_ACCP_USER_SELECTION")

        FreqProd.Visible = True
        FreqProd.DataSource = obtieneDetalleFrecuanciaProductos(userSelection.periodo.Substring(0, 4), _
                                            userSelection.periodo.Substring(4, 2), _
                                            userInfo.codigoFilial, _
                                            userInfo.codigoSucursal, _
                                            e.Item.Cells(0).Text, _
                                            userSelection.codFrecuencia, _
                                            userSelection.numDependencia, _
                                            e.Item.Cells(2).Text)
        FreqProd.DataBind()
    End Sub

    Private Sub FreqProd_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles FreqProd.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                valVtaProdsFreq1 += CDbl(e.Item.Cells(3).Text)
                valVtaProdsFreq2 += CDbl(e.Item.Cells(4).Text)
                valVtaProdsFreq3 += CDbl(e.Item.Cells(5).Text)
            Case ListItemType.Footer
                e.Item.Cells(0).Text = "Totales"
                e.Item.Cells(0).ColumnSpan = 2
                e.Item.Cells(1).Visible = False
                e.Item.Cells(3).Text = Format(valVtaProdsFreq1, "#,##0.00")
                e.Item.Cells(4).Text = Format(valVtaProdsFreq2, "#,##0.00")
                e.Item.Cells(5).Text = Format(valVtaProdsFreq3, "#,##0.00")
        End Select
    End Sub

#Region " EXPORTA XLS "
    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click

        Dim sTableHeader As String = "Análisis Cartera Ejec. Comercial "

        ' Nueva instancia del Informe
        Dim xlsResultado As Table = Session("mytable")

        ' Agregar encabezado del informe
        Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

        Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

        ' Configuracion de impresion
        Exportar.PageScale = 80
        Exportar.PageLayout = "Landscape"

        ' Encabezado y Pie de Pagina
        Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;Listado de Precios - ")
        Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

        ' Exportar
        Exportar.TableToExcel(xlsResultado)
        Exportar.SaveToClient(Response)
    End Sub
#End Region
End Class
