Imports Exportador
Imports System.Xml


Public Class vta_matriz_cliente_uni_fisicas
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents lbCodPromo As System.Web.UI.WebControls.Label
    Protected WithEvents lbNomPromo As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents ddlSector As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label

    Private nom_promotora As String
    Private cod_promotora As String
    Private cod_sector As String
    Private col_grupo(30) As String

    Dim tUserInfo As t_Usuario

    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tbResultado As System.Web.UI.WebControls.Table
    Protected WithEvents btAceptar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ddlPromotora As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSociedad As System.Web.UI.WebControls.DropDownList

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
        'Obtenemos promotoras para al sociedad


        If Not Page.IsPostBack Then
            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

            cargaAnos()
            cargaMeses()
            cargaSectores()
            ddlSociedad.ClearSelection()

            Select Case tUserInfo.codigoFilial
                Case "CHI"
                    ddlSociedad.Items.FindByValue("GMSC").Selected = True
                Case "PER"
                    ddlSociedad.Items.FindByValue("APER").Selected = True
                Case "BOL"
                    ddlSociedad.Items.FindByValue("ABOL").Selected = True
            End Select


            cargaEjecutivas(ddlSociedad.SelectedValue)

            lbFecha.Text = MonthName(Month(Date.Now())) & " , " & Year(Date.Now().ToString)


            If tUserInfo.perfil.Trim = P_PROMOTORA Then
                ' USER ES UNA PROMOTORA
                cod_promotora = tUserInfo.codigoPromo
                nom_promotora = tUserInfo.nombre

                If Not ddlPromotora.Items.FindByValue(cod_promotora) Is Nothing Then
                    ddlPromotora.SelectedValue = cod_promotora
                End If

                ddlPromotora.Enabled = False

                lbNota.Visible = True
                lbNota.Text = "* Solo Clientes Cartera"
            End If

        Else
            'doInforme()
            'lbFecha.Text = ddlMes.SelectedItem.Text & " , " & ddlAno.SelectedItem.Text
        End If

        'lbErrors.Text = ""
        'btAceptar.Attributes.Add("onClick", "javascript:noDblClick(document.all.disbtn,document.all.btAceptar);")

    End Sub


    Private Function wrapValue(ByVal c As String, ByVal v As String) As String
        wrapValue = "<TABLE cellSpacing=""0"" cellPadding=""0"" width=""90%"" border=""0""><TR><TD align=""right""><div style=""font-size:7pt;"">" & c & "</div></TD></TR><TR>" & _
              "<TD bgColor=""#20b2aa"" height=""1""><IMG src=""images/transparent.gif"" width=""20"" height=""1""></TD>" & _
                 "</TR><TR><TD align=""right""><div style=""font-size:7pt;"">" & v & "</div></TD></TR></TABLE>"
    End Function


    Private Function getSubValue(ByVal ventaMes As Integer, ByVal suma6Meses As Integer, ByVal promedio As Decimal, ByVal porcentaje As Decimal) As String

        Dim cl As New System.Globalization.CultureInfo("es-CL")

        If ventaMes <= 0 Then
            If suma6Meses > 0 Then

                Return String.Format(cl, "{0:N2}", promedio) & "p"

            ElseIf suma6Meses <= 0 Then

                Return "0"

            End If

        Else

            If suma6Meses > 0 Then

                Return String.Format(cl, "{0:N2}", porcentaje) & "%"

            ElseIf suma6Meses <= 0 Then

                Return "0"
            End If
        End If

    End Function

    Private Sub doInforme()

        Dim dtColumnas As DataTable
        Dim drColumnas As DataRow
        Dim dtDatos As DataTable
        Dim dsResult As New DataSet
        Dim dtBindTable As New DataTable

        Dim trSubRow As TableRow
        Dim tcVenta As TableCell


        Dim ano_periodo As Int16 = CInt(ddlAno.SelectedItem.Value)
        Dim mes_periodo As Int16 = CInt(ddlMes.SelectedItem.Value)
        Dim cod_filial As String = tUserInfo.codigoFilial
        Dim cod_sucursal As String = tUserInfo.codigoSucursal
        cod_promotora = ddlPromotora.SelectedValue


        Try


            cod_sector = ddlSector.SelectedValue.ToString

            If cod_promotora = "" Then
                Err.Description = "Faltan parametros para poder ejecutar la consulta."
                Err.Raise(vbObjectError + 512 + 10, "matriz_vta_fisica_cliente_prom", Err.Description)
            End If

            Dim tUserInfo As usuario.t_Usuario
            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
            cod_filial = tUserInfo.codigoFilial
            cod_sucursal = tUserInfo.codigoSucursal

            Dim url_report As String
            url_report = "\Crystal2005\Default.aspx"
            url_report = url_report & "?ano_periodo=" & CStr(ano_periodo)
            url_report = url_report & "&mes_periodo=" & CStr(mes_periodo)
            url_report = url_report & "&cod_filial=" & cod_filial
            url_report = url_report & "&cod_sucursal=" & cod_sucursal
            url_report = url_report & "&cod_promotora=" & cod_promotora
            url_report = url_report & "&cod_sector=" & cod_sector
            url_report = url_report & "&report_name=" & "Matriz_Analisis_Venta-Cliente_new con lineas.rpt"

            Response.Redirect(url_report)

        Catch ex As Exception
            lbErrors.Text = "ERROR: " & Err.Description
            lbErrors.Visible = True
            Err.Clear()
        End Try

    End Sub


    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click

        Dim sTableHeader As String = "Ejec. comercial: " & Me.lbCodPromo.Text.Trim & " - " & Me.lbNomPromo.Text.Trim

        ' Nueva instancia del Informe
        Dim xlsResultado As Table = tbResultado

        ' Agregar encabezado del informe
        Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

        Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

        ' Configuracion de impresion
        Exportar.PageScale = 80
        Exportar.PageLayout = "Landscape"
        Exportar.Linea_Header = 3

        ' Encabezado y Pie de Pagina
        Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DMatriz An&aacute;lisis Venta por Cliente - " & lbFecha.Text.Trim)
        Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

        ' Exportar
        Exportar.TableToExcel(xlsResultado)
        Exportar.SaveToClient(Response)


    End Sub

    Private Sub cargaAnos()
        ddlAno.Items.Add(Year(Date.Now))
        ddlAno.Items.Add(Year(Date.Now.AddYears(-1)))
    End Sub

    Private Sub cargaMeses()
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
    End Sub

    Private Sub cargaSectores()
        Dim i As Integer
        Dim dtSectores As DataTable = get_matriz_sector(ddlSociedad.SelectedValue)

        Dim alSectores = New ArrayList
        For i = 0 To dtSectores.Rows.Count - 1
            alSectores.Add(dtSectores.Rows(i).Item("cod_sector"))
        Next
        alSectores.TrimToSize()
        alSectores.Sort()

        ddlSector.DataSource = alSectores
        ddlSector.DataBind()
    End Sub

    Private Sub btAceptar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btAceptar.Click
        Response.Redirect("http://andes2.gms.cl/default.aspx?" & _
                        "ano_periodo=" & ddlAno.SelectedValue & "&" & _
                        "mes_periodo=" & ddlMes.SelectedValue & "&" & _
                        "cod_promotora=" & ddlPromotora.SelectedValue.Trim & "&" & _
                        "cod_sociedad=" & ddlSociedad.SelectedValue & "&" & _
                        "cod_sector=" & ddlSector.SelectedValue)

    End Sub

    Private Sub ddlSociedad_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlSociedad.SelectedIndexChanged
        cargaEjecutivas(ddlSociedad.SelectedValue)
    End Sub

    Private Sub cargaEjecutivas(ByVal codigoSociedad As String)
        Dim wsAndes As cl.gms.andes.ws.OrgSrv = New cl.gms.andes.ws.OrgSrv
        Dim dsPromotoras As DataSet = wsAndes.listaPromotoras(codigoSociedad)

        With ddlPromotora
            .Items.Clear()
            .DataSource = dsPromotoras.Tables(0)
            .DataTextField = "nom_promotora"
            .DataValueField = "cod_promotora"
            .DataBind()
        End With

        Dim dsVendedorasVirtual As DataSet = wsAndes.listaVendedorasVirtuales(codigoSociedad)

        If dsVendedorasVirtual.Tables.Count > 0 Then
            For Each myDataRow As DataRow In dsVendedorasVirtual.Tables(0).Rows
                ddlPromotora.Items.Add(New ListItem(Trim(myDataRow("nom_vendedora")), Trim(myDataRow("cod_vend_virtual"))))
            Next
        End If

        wsAndes = Nothing
        dsPromotoras = Nothing
    End Sub

End Class


