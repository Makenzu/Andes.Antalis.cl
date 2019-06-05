Imports System.Data.SqlTypes
Imports Exportador

Public Class analisisCarteraClientePromotora
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
    Protected WithEvents ddlArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlConcepto As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btConsulta As System.Web.UI.WebControls.ImageButton
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents tblFamilias As System.Web.UI.WebControls.Table
    Protected WithEvents tblDetalleAnalisis As System.Web.UI.WebControls.Table
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents ddlPeriodo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lb1 As System.Web.UI.WebControls.Label
    Protected WithEvents lb3 As System.Web.UI.WebControls.Label
    Protected WithEvents lb5 As System.Web.UI.WebControls.Label
    Protected WithEvents lb2 As System.Web.UI.WebControls.Label
    Protected WithEvents lb4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlFiltroCliente As System.Web.UI.WebControls.DropDownList
    Protected WithEvents filaFiltroCliente As System.Web.UI.HtmlControls.HtmlTableRow

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
        Dim codAreaLnk As String
        Dim codAreaDdl As String
        Dim codFamilia As String
        Dim codSubfamilia As String
        Dim codConcepto As String
        Dim verSubfamilia As String
        Dim codFiltroCli As String
    End Structure

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        'Put user code to initialize the page here
        Dim userSelection As stUserSelection

        filaFiltroCliente.Visible = CType(Session(Constantes.CTE_ANDES_INFO_USUARIO), usuario.t_Usuario).codigoFilial = "PER"

        If Session("TMP_ACCP_USER_SELECTION") Is Nothing Then
            With userSelection
                .codAreaLnk = Nothing
                .codAreaDdl = Nothing
                .codConcepto = Nothing
                .codFamilia = Nothing
                .codPromotora = Nothing
                .codSubfamilia = Nothing
                .periodo = Nothing
                .codFiltroCli = Nothing
            End With
            Session("TMP_ACCP_USER_SELECTION") = userSelection
        End If

        If Page.IsPostBack Then
            With userSelection
                .periodo = Request("ddlPeriodo")
                .codPromotora = Request("ddlPromotoras")
                .codAreaDdl = Request("ddlArea")
                .codAreaLnk = Request("ddlArea")
                '.codFamilia = Request("")
                '.codSubfamilia = Request("")
                .codConcepto = Request("ddlConcepto")
                .codFiltroCli = Request("ddlFiltroCliente")
            End With

            Session("TMP_ACCP_USER_SELECTION") = userSelection

            despliegaArbolFamilias()
        ElseIf Request("accion") = "refrezca" Then
            'Seteamos los valores por defecto para los controles de búsqueda
            With userSelection
                .periodo = Request("periodo")
                .codPromotora = Request("promotora")
                If Request("arealnk") = "*" Then
                    .codAreaLnk = Nothing
                Else
                    .codAreaLnk = Request("arealnk")
                End If
                .codAreaDdl = Request("areaddl")
                .codFamilia = Request("familia")
                .codSubfamilia = Request("subfamilia")
                .codConcepto = Request("concepto")
                .verSubfamilia = Request("versubfam")
                .codFiltroCli = Request("filtrocli")
            End With

            Session("TMP_ACCP_USER_SELECTION") = userSelection
            despliegaArbolFamilias()
            btConsulta_Click(Nothing, Nothing)
        End If

        With userSelection
            cargaPeriodos(.periodo)
            cargaAreas(.codAreaDdl)
            cargaConceptos(.codConcepto)
            cargaPromotoras(.codPromotora)
            cargaFiltroCliente(.codFiltroCli)
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
        End If

    End Sub
#End Region

#Region " cargaAreas "
    Private Sub cargaAreas(ByVal codigoArea As String)
        ddlArea.Items.Clear()
        ddlArea.Items.Add(New ListItem("PAPELES", "PAP"))
        ddlArea.Items.Add(New ListItem("COM VISUAL", "CVI"))
        ddlArea.Items.Add(New ListItem("EQUIPOS E INSUMOS", "EII"))
        ddlArea.Items.Add(New ListItem("OTROS", "ZZZ"))
        ddlArea.Items.Add(New ListItem("* TODOS *", "*"))

        If Not codigoArea Is Nothing Then
            ddlArea.ClearSelection()
            ddlArea.Items.FindByValue(codigoArea).Selected = True
        Else
            ddlArea.ClearSelection()
            ddlArea.Items.FindByValue("*").Selected = True
        End If
    End Sub
#End Region

#Region " cargaConceptos "
    Private Sub cargaConceptos(ByVal codigoConcepto As String)
        ddlConcepto.Items.Clear()
        ddlConcepto.Items.Add(New ListItem("VTA USD", "VGUSD"))
        ddlConcepto.Items.Add(New ListItem("MG USD", "MGUSD"))
        ddlConcepto.Items.Add(New ListItem("VOLUMEN", "VOL"))
        ddlConcepto.Items.Add(New ListItem("U.M.BASE", "UMB"))

        If Not codigoConcepto Is Nothing Then
            ddlConcepto.ClearSelection()
            ddlConcepto.Items.FindByValue(codigoConcepto).Selected = True
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

        ddlPromotoras.Items.Add(New ListItem("- Seleccione -", ""))
        ddlPromotoras.Items.FindByValue("").Selected = True

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

#Region " cargaFiltroCliente"
    Private Sub cargaFiltroCliente(ByVal codigoFiltroCliente As String)
        ddlFiltroCliente.Items.Clear()
        ddlFiltroCliente.Items.Add(New ListItem("TODOS", "TODOS"))
        ddlFiltroCliente.Items.Add(New ListItem("CARTERA", "CARTERA"))

        If (codigoFiltroCliente Is Nothing) Or (codigoFiltroCliente = "") Then
            ddlFiltroCliente.ClearSelection()
            ddlFiltroCliente.Items.FindByValue("TODOS").Selected = True
        Else
            ddlFiltroCliente.ClearSelection()
            ddlFiltroCliente.Items.FindByValue(codigoFiltroCliente).Selected = True
        End If
    End Sub
#End Region

    Private Sub btConsulta_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btConsulta.Click
        Dim userInfo As usuario.t_Usuario
        Dim cod_promotora, nom_promotora As String
        userInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        despliegaArbolFamilias()
        ibExportar.Visible = True

        Dim userSelection As stUserSelection
        userSelection = Session("TMP_ACCP_USER_SELECTION")

        If userSelection.codPromotora Is Nothing Or userSelection.codPromotora = "" Then
            userSelection.codPromotora = userInfo.codigoPromo
        End If

        Dim dsDatos As DataSet
        With userSelection
            dsDatos = obtieneAnalisisCarteraPromotora_V2(.periodo.Substring(0, 4), _
                                                .periodo.Substring(4, 2), _
                                                userInfo.codigoFilial, _
                                                .codPromotora, _
                                                .codAreaLnk, _
                                                .codFamilia, _
                                                .codSubfamilia, _
                                                .codConcepto, _
                                                .codFiltroCli)
            'Table(0) contiene fechas de cada periodo para el rango 12 meses.
            'Table(1) contiene datos del perdiodo consultado
        End With

        'Generamos el encabezado de la tabla del informe
        generaEncabezadoDetalleAnalisis(dsDatos.Tables(0))
        cargaDetalleAnalisis(dsDatos.Tables(1))

        Session("mytable") = tblDetalleAnalisis
        lb1.Visible = True
        lb2.Visible = True
        lb3.Visible = True
        lb4.Visible = True
        lb5.Visible = True
    End Sub

#Region " despliegaArbolFamilias "
    Private Sub despliegaArbolFamilias()

        Session("ACP_NOMBRE_SUBFAMILIA") = ""
        Session("ACP_NOMBRE_FAMILIA") = ""

        Dim userSelection As stUserSelection = Session("TMP_ACCP_USER_SELECTION")

        tblFamilias.Rows.Clear()

        Dim myDataTable As DataTable
        Dim myUrl As String

        'Obtenemos listado de familias a partir de la seleccion de área desde UI.
        If userSelection.codAreaDdl <> "*" Then
            Dim codArea As SqlString = New SqlString(userSelection.codAreaDdl)
            myDataTable = Utiles.obtieneFamilias(codArea, SqlString.Null)
        Else
            myDataTable = Utiles.obtieneFamilias(SqlString.Null, SqlString.Null)
        End If

        Dim myTableCell As TableCell
        Dim myTablerow As TableRow
        Dim myDataRow As DataRow
        Dim lastArea As String = ""
        Dim myHyperLink As HyperLink

        For Each myDataRow In myDataTable.Rows
            'Si hay cambio de área en el listado, procedemos a incorporar la nueva área a tabla de informe.
            If (lastArea <> myDataRow.Item("cod_area")) Then
                myTableCell = New TableCell
                myTableCell.Wrap = False

                myHyperLink = New HyperLink
                With myHyperLink
                    .Text = Trim(myDataRow.Item("cod_area"))
                    .NavigateUrl = "analisisCarteraClientePromotora.aspx?accion=refrezca&" & _
                                            "&promotora=" & userSelection.codPromotora & _
                                            "&periodo=" & userSelection.periodo & _
                                            "&arealnk=" & myDataRow.Item("cod_area") & _
                                            "&areaddl=" & userSelection.codAreaDdl & _
                                            "&concepto=" & userSelection.codConcepto & _
                                            "&filtrocli=" & userSelection.codFiltroCli
                End With
                Dim a As String

                myTableCell.Controls.Add(myHyperLink)

                lastArea = myDataRow.Item("cod_area")
                myTablerow = New TableRow
                myTablerow.Cells.Add(myTableCell)
                tblFamilias.Rows.Add(myTablerow)
            End If


            'Agregamos código de familia y descripción de ésta a tabla de informe.
            myTablerow = New TableRow
            myTableCell = New TableCell
            myTableCell.Wrap = False

            myHyperLink = New HyperLink
            With myHyperLink
                .Text = Trim(myDataRow.Item("cod_familia")) & " - " & Trim(myDataRow.Item("des_familia"))
                .NavigateUrl = "analisisCarteraClientePromotora.aspx?accion=refrezca" & _
                                "&familia=" & Trim(myDataRow.Item("cod_familia")) & _
                                "&promotora=" & userSelection.codPromotora & _
                                "&periodo=" & userSelection.periodo & _
                                "&arealnk=" & myDataRow.Item("cod_area") & _
                                "&areaddl=" & userSelection.codAreaDdl & _
                                "&concepto=" & userSelection.codConcepto & _
                                "&filtrocli=" & userSelection.codFiltroCli

                If myDataRow.Item("cod_familia") = userSelection.codFamilia Then
                    .CssClass = "acp-familias-bold"
                Else
                    .CssClass = "acp-familias"
                End If

            End With
            myTableCell.Controls.Add(myHyperLink)


            'Si el usuario seleccionó un vínculo a nivel de familia, entonces se procede a expandir o contraer
            'el detalle de la familia (listado de subfamilias que le están asociadas).
            If myDataRow.Item("cod_familia") = userSelection.codFamilia Then

                Session("ACP_NOMBRE_FAMILIA") = myDataRow.Item("des_familia")

                'Session("ACP_DESC_FAMILIA_ACTIVA") = Trim(myDataRow.Item("cod_familia")) & " - " & Trim(myDataRow.Item("des_familia"))
                myTableCell.CssClass = "tbl-acp-familias2"

                'Se procede a desplegar las subfamilias asociadas a la familia seleccionada por el usuario
                If Request("versubfam") = "si" Then

                    myHyperLink = New HyperLink
                    With myHyperLink
                        .Text = "[-]"
                        .NavigateUrl = "analisisCarteraClientePromotora.aspx?accion=refrezca&" & _
                                                            "familia=" & Trim(myDataRow.Item("cod_familia")) & _
                                                            "&promotora=" & userSelection.codPromotora & _
                                                            "&periodo=" & userSelection.periodo & _
                                                            "&versubfam=no" & _
                                                            "&arealnk=" & myDataRow.Item("cod_area") & _
                                                            "&areaddl=" & userSelection.codAreaDdl & _
                                                            "&concepto=" & userSelection.codConcepto & _
                                                            "&filtrocli=" & userSelection.codFiltroCli
                        .CssClass = "acp-familias-bold"
                    End With
                    myTableCell.Controls.Add(myHyperLink)



                    Dim cod_familia As SqlString = New SqlString(Trim(myDataRow.Item("cod_familia")))
                    Dim tblSubfamilias As DataTable
                    Dim drSubfamilia As DataRow
                    Dim myCssClass As String

                    tblSubfamilias = Utiles.obtieneSubfamilias(cod_familia, SqlString.Null)


                    Dim tblDetail As Table = New Table
                    Dim tblRow As TableRow
                    Dim tblCell As TableCell


                    With tblDetail
                        .BorderWidth = New Unit(1, UnitType.Pixel)
                        .Width = New Unit(100, UnitType.Percentage)
                        .CellPadding = 1
                        .CellSpacing = 2
                    End With


                    For Each drSubfamilia In tblSubfamilias.Rows

                        tblRow = New TableRow

                        'Agregamos celda con codigo de subfamilia
                        tblCell = New TableCell
                        myHyperLink = New HyperLink
                        With myHyperLink
                            .Text = Trim(drSubfamilia.Item("cod_subfamilia"))
                            .NavigateUrl = "analisisCarteraClientePromotora.aspx?accion=refrezca" & _
                                                            "&familia=" & Trim(myDataRow.Item("cod_familia")) & _
                                                            "&promotora=" & userSelection.codPromotora & _
                                                            "&periodo=" & userSelection.periodo & _
                                                            "&versubfam=si&" & _
                                                            "&subfamilia=" & Trim(drSubfamilia.Item("cod_subfamilia")) & _
                                                            "&arealnk=" & myDataRow.Item("cod_area") & _
                                                            "&areaddl=" & userSelection.codAreaDdl & _
                                                            "&concepto=" & userSelection.codConcepto & _
                                                            "&filtrocli=" & userSelection.codFiltroCli

                            If (Trim(drSubfamilia.Item("cod_subfamilia")) = userSelection.codSubfamilia) Then
                                Session("ACP_NOMBRE_SUBFAMILIA") = drSubfamilia.Item("des_subfamilia")
                                '    'Session("ACP_DESC_SUBFAMILIA_ACTIVA") = Trim(drSubfamilia.Item("cod_subfamilia")) & " - " & Trim(drSubfamilia.Item("des_subfamilia"))
                                .CssClass = "acp-subfamilia-bold"
                                tblCell.CssClass = "tbl-acp-subfamilia2"
                            Else
                                .CssClass = "acp-subfamilia"
                                tblCell.CssClass = "tbl-acp-subfamilia"
                            End If

                        End With
                        tblCell.Controls.Add(myHyperLink)
                        tblRow.Cells.Add(tblCell)


                        'Agregamos celda con descripcion de subfamilia
                        tblCell = New TableCell
                        myHyperLink = New HyperLink
                        With myHyperLink
                            .Text = Trim(drSubfamilia.Item("des_subfamilia"))
                            .NavigateUrl = "analisisCarteraClientePromotora.aspx?accion=refrezca" & _
                                                            "&familia=" & Trim(myDataRow.Item("cod_familia")) & _
                                                            "&promotora=" & userSelection.codPromotora & _
                                                            "&periodo=" & userSelection.periodo & _
                                                            "&versubfam=si&" & _
                                                            "&subfamilia=" & Trim(drSubfamilia.Item("cod_subfamilia")) & _
                                                            "&arealnk=" & myDataRow.Item("cod_area") & _
                                                            "&areaddl=" & userSelection.codAreaDdl & _
                                                            "&concepto=" & userSelection.codConcepto & _
                                                            "&filtrocli=" & userSelection.codFiltroCli

                            If (Trim(drSubfamilia.Item("cod_subfamilia")) = userSelection.codSubfamilia) Then
                                '    'Session("ACP_DESC_SUBFAMILIA_ACTIVA") = Trim(drSubfamilia.Item("cod_subfamilia")) & " - " & Trim(drSubfamilia.Item("des_subfamilia"))
                                .CssClass = "acp-subfamilia-bold"
                                tblCell.CssClass = "tbl-acp-subfamilia2"
                            Else
                                .CssClass = "acp-subfamilia"
                                tblCell.CssClass = "tbl-acp-subfamilia"
                            End If

                        End With
                        tblCell.Controls.Add(myHyperLink)
                        tblRow.Cells.Add(tblCell)


                        tblDetail.Rows.Add(tblRow)
                    Next

                    myTableCell.Controls.Add(tblDetail)

                Else
                    myHyperLink = New HyperLink
                    With myHyperLink
                        .Text = "[+]"
                        .NavigateUrl = "analisisCarteraClientePromotora.aspx?accion=refrezca" & _
                                                                    "&familia=" & Trim(myDataRow.Item("cod_familia")) & _
                                                                    "&promotora=" & userSelection.codPromotora & _
                                                                    "&periodo=" & userSelection.periodo & _
                                                                    "&versubfam=si" & _
                                                                    "&arealnk=" & myDataRow.Item("cod_area") & _
                                                                    "&areaddl=" & userSelection.codAreaDdl & _
                                                                    "&concepto=" & userSelection.codConcepto & _
                                                                    "&filtrocli=" & userSelection.codFiltroCli
                        .CssClass = "acp-familias-bold"
                    End With
                    myTableCell.Controls.Add(myHyperLink)
                End If


                'Session("ACP_DESC_FAMILIA_ACTIVA") = Trim(myDataRow.Item("cod_familia")) & " - " & Trim(myDataRow.Item("des_familia"))
            Else
                myTableCell.CssClass = "tbl-acp-familias"
            End If

            'myTableCell.CssClass = "tbl-acp-familias"
            myTablerow.Cells.Add(myTableCell)

            tblFamilias.Rows.Add(myTablerow)
        Next

        tblFamilias.BackColor = Color.FromArgb(&HB5, &HC7, &HDE)
    End Sub
#End Region

#Region " generaEncabezadoDetalleAnalisis "

    Private Sub generaEncabezadoDetalleAnalisis(ByVal tblHeader As DataTable)
        Dim myTableCell As TableCell
        Dim myTableRow As TableRow
        Dim myDataRow As DataRow
        Dim nombreFamilia As String
        Dim nombreSubfamilia As String
        Dim nombrePromotora As String

        nombreFamilia = Session("ACP_NOMBRE_FAMILIA")
        nombreSubfamilia = Session("ACP_NOMBRE_SUBFAMILIA")

        Dim tUserInfo As usuario.t_Usuario
        Dim cod_promotora, nom_promotora As String
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        Dim userSelection As stUserSelection = Session("TMP_ACCP_USER_SELECTION")

        nombrePromotora = get_promotora_name(tUserInfo.codigoFilial, _
                                                tUserInfo.codigoSucursal, _
                                                userSelection.codPromotora)

        myTableRow = New TableRow
        myTableCell = New TableCell

        With myTableCell
            .ColumnSpan = 18
            .Text = "EJEC: COMERCIAL: " & nombrePromotora & "<BR>"
            'AGS.23-03-2010 : De acuerdo a que click haya hecho el usuario, se muestra el area (en el DDL.areas o en arbol de navegacion)
            If (Request("ddlArea") = "*") Then
                .Text += "TODAS LAS AREAS / FAMILIAS / SUBFAMILIAS" & "<BR>"
            Else
                If Request("ddlArea") Is Nothing Then
                    .Text += "AREA: " & Request("arealnk") & "<BR>"
                Else
                    .Text += "AREA: " & Request("ddlArea") & "<BR>"
                End If
                .Text += "FAMILIA: " & nombreFamilia.ToUpper & "<BR>"
                If (nombreSubfamilia <> "") Then
                    .Text += "SUBFAMILIA: " & nombreSubfamilia.ToUpper
                End If

            End If

            .HorizontalAlign = HorizontalAlign.Left
            .CssClass = "tbl-DataGridHeader"
        End With
        myTableRow.Cells.Add(myTableCell)
        tblDetalleAnalisis.Rows.Add(myTableRow)

        'Generamos cabecera de tabla
        myTableRow = New TableRow
        myTableCell = New TableCell
        With myTableCell
            .Text = "CLI.ACTIVO"
            .HorizontalAlign = HorizontalAlign.Center
            .CssClass = "tbl-DataGridHeader"
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = "CODIGO"
            .HorizontalAlign = HorizontalAlign.Left
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = "RAZON SOCIAL"
            .HorizontalAlign = HorizontalAlign.Left
            .CssClass = "tbl-DataGridHeader"
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_12"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_11"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_10"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_09"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_08"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_07"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_06"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_05"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_04"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_03"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_02"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_01"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(tblHeader.Rows(0).Item("periodo_00"), "MMM/yy")
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = "TOTAL"
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = "PROM."
            .HorizontalAlign = HorizontalAlign.Right
            .Width = New Unit(50, UnitType.Pixel)
            .CssClass = "tbl-DataGridHeader"
            .Wrap = False
        End With
        myTableRow.Cells.Add(myTableCell)

        tblDetalleAnalisis.Rows.Add(myTableRow)

    End Sub

#End Region

#Region " cargaDetalleAnalisis "
    Private Sub cargaDetalleAnalisis(ByVal tblDetail As DataTable)
        Dim myTableCell As TableCell
        Dim myTableRow As TableRow
        Dim myDataRow As DataRow
        Dim fmtExpresion As String
        fmtExpresion = "#,##0.00"
        Dim i As Integer
        Dim myCSSClass As String
        Dim sum00, sum01, sum02, sum03, sum04, sum05, sum06, sum07, sum08, sum09, sum10, sum11, sum12 As Double
        Dim ecp_sum00, ecp_sum01, ecp_sum02, ecp_sum03, ecp_sum04, ecp_sum05, ecp_sum06, ecp_sum07, ecp_sum08, ecp_sum09, ecp_sum10, ecp_sum11, ecp_sum12 As Double
        Dim myValue, totalCliente As Double

        sum00 = 0
        sum01 = 0
        sum02 = 0
        sum03 = 0
        sum04 = 0
        sum05 = 0
        sum06 = 0
        sum07 = 0
        sum08 = 0
        sum09 = 0
        sum10 = 0
        sum11 = 0
        sum12 = 0

        i = 0
        For Each myDataRow In tblDetail.Rows
            If i Mod 2 = 0 Then
                If myDataRow.Item("ecp_m00") = "X" Then
                    myCSSClass = "tbl-DataGridItemAlternating"
                Else
                    myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                End If
            Else
                If myDataRow.Item("ecp_m00") = "X" Then
                    myCSSClass = "tbl-DataGridItem"
                Else
                    myCSSClass = "tbl-DataGridItem-nocliente"
                End If
            End If

            'Generamos cabecera de tabla
            myTableRow = New TableRow
            myTableCell = New TableCell
            With myTableCell
                If myDataRow.Item("ecp_m00") = "X" Then
                    .Text = "*"
                    .HorizontalAlign = HorizontalAlign.Center
                    .CssClass = myCSSClass
                Else
                    .Text = ""
                    .HorizontalAlign = HorizontalAlign.Center
                    .CssClass = myCSSClass
                End If
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                .Text = myDataRow.Item("cod_cliente")
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                .Text = myDataRow.Item("nom_cliente")
                .Wrap = False
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m12") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m12") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m12")), fmtExpresion)

                'If myDataRow.Item("ecp_m12") = "X" Then
                sum12 += myDataRow.Item("val_m12")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m11") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m11") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m11")), fmtExpresion)

                'If myDataRow.Item("ecp_m11") = "X" Then
                sum11 += myDataRow.Item("val_m11")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m10") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m10") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m10")), fmtExpresion)

                'If myDataRow.Item("ecp_m10") = "X" Then
                sum10 += myDataRow.Item("val_m10")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m09") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m09") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m09")), fmtExpresion)

                'If myDataRow.Item("ecp_m09") = "X" Then
                sum09 += myDataRow.Item("val_m09")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m08") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m08") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m08")), fmtExpresion)

                'If myDataRow.Item("ecp_m08") = "X" Then
                sum08 += myDataRow.Item("val_m08")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m07") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m07") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m07")), fmtExpresion)

                'If myDataRow.Item("ecp_m07") = "X" Then
                sum07 += myDataRow.Item("val_m07")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m06") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m06") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m06")), fmtExpresion)

                'If myDataRow.Item("ecp_m06") = "X" Then
                sum06 += myDataRow.Item("val_m06")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m05") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m05") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m05")), fmtExpresion)

                'If myDataRow.Item("ecp_m05") = "X" Then
                sum05 += myDataRow.Item("val_m05")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m04") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m04") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m04")), fmtExpresion)

                'If myDataRow.Item("ecp_m04") = "X" Then
                sum04 += myDataRow.Item("val_m04")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m03") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m03") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m03")), fmtExpresion)

                'If myDataRow.Item("ecp_m03") = "X" Then
                sum03 += myDataRow.Item("val_m03")
                ' End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m02") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m02") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m02")), fmtExpresion)

                'If myDataRow.Item("ecp_m02") = "X" Then
                sum02 += myDataRow.Item("val_m02")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m01") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m01") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m01")), fmtExpresion)

                'If myDataRow.Item("ecp_m01") = "X" Then
                sum01 += myDataRow.Item("val_m01")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                If i Mod 2 = 0 Then
                    If myDataRow.Item("ecp_m00") = "X" Then
                        myCSSClass = "tbl-DataGridItemAlternating"
                    Else
                        myCSSClass = "tbl-DataGridItemAlternating-nocliente"
                    End If
                Else
                    If myDataRow.Item("ecp_m00") = "X" Then
                        myCSSClass = "tbl-DataGridItem"
                    Else
                        myCSSClass = "tbl-DataGridItem-nocliente"
                    End If
                End If

                .Text = Format(Double.Parse(myDataRow.Item("val_m00")), fmtExpresion)

                'If myDataRow.Item("ecp_m00") = "X" Then
                sum00 += myDataRow.Item("val_m00")
                'End If

                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            If i Mod 2 = 0 Then
                myCSSClass = "tbl-DataGridItemAlternating"
            Else
                myCSSClass = "tbl-DataGridItem"
            End If

            totalCliente = myDataRow.Item("val_m00") + _
                           myDataRow.Item("val_m01") + _
                           myDataRow.Item("val_m02") + _
                           myDataRow.Item("val_m03") + _
                           myDataRow.Item("val_m04") + _
                           myDataRow.Item("val_m05") + _
                           myDataRow.Item("val_m06") + _
                           myDataRow.Item("val_m07") + _
                           myDataRow.Item("val_m08") + _
                           myDataRow.Item("val_m09") + _
                           myDataRow.Item("val_m10") + _
                           myDataRow.Item("val_m11") + _
                           myDataRow.Item("val_m12")

            myTableCell = New TableCell
            With myTableCell
                .Text = Format(totalCliente, fmtExpresion)
                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            myTableCell = New TableCell
            With myTableCell
                .Text = Format(totalCliente / 12, fmtExpresion)
                .HorizontalAlign = HorizontalAlign.Right
                .CssClass = myCSSClass
            End With
            myTableRow.Cells.Add(myTableCell)

            tblDetalleAnalisis.Rows.Add(myTableRow)
            i += 1
        Next

        'Agregamos pie de página
        myCSSClass = "tbl-DataGridFooter"
        myTableRow = New TableRow
        myTableCell = New TableCell
        With myTableCell
            .Text = ""
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = "TOTAL CARTERA:"
            .Wrap = False
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = ""
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum12, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum11, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum10, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum09, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum08, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum07, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum06, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum05, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum04, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum03, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum02, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum01, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum00, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)

        myTableCell = New TableCell
        With myTableCell
            .Text = Format(sum00 + sum01 + sum02 + sum03 + sum04 + sum05 + sum06 + sum07 + sum08 + sum09 + sum10 + sum11 + sum12, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)


        myTableCell = New TableCell
        With myTableCell
            .Text = Format((sum00 + sum01 + sum02 + sum03 + sum04 + sum05 + sum06 + sum07 + sum08 + sum09 + sum10 + sum11 + sum12) / 13, fmtExpresion)
            .HorizontalAlign = HorizontalAlign.Right
            .CssClass = myCSSClass
        End With
        myTableRow.Cells.Add(myTableCell)


        tblDetalleAnalisis.Rows.Add(myTableRow)
        Session("mytable") = tblDetalleAnalisis
    End Sub
#End Region

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
