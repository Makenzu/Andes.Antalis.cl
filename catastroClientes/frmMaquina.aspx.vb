Public Class ingresaMaquina
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents bGrabaMaquina As System.Web.UI.WebControls.Button
    Protected WithEvents bCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents hfAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lMensajeError As System.Web.UI.WebControls.Label
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents fhIdRegistro As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hfIdRegistro As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents lCliente As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMaquina As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTamanhoMaquina As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tbNumeroMaquinas As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbCuerposImpresores As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkbTorreBarniz As System.Web.UI.WebControls.CheckBox
    Protected WithEvents tbConsumoPapel As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbConsumoPlanchas As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbConsumoProceso As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbConsumoPantone As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbConsumoMantillas As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbConsumoBarniz As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTamanoPlancha As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTamanoMantilla As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim msgError As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            lCliente.Text = Session("catastroMaquinasSeleccionCliente_codCliente") & " - " & Session("catastroMaquinasSeleccionCliente_razonSocial")

            bCancelar.Attributes.Add("onClick", "window.close()")

            cargaValoresControles()
            hfAccion.Value = Request("accion")

            If hfAccion.Value = "editar" Then
                'recuperamos datos de la máquina
                Dim idRegistro As Integer = Request("idRegistro")
                Dim wsAndes As cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv = New cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv

                Dim ds As DataSet = wsAndes.obtieneRegistroMaquinaCliente(idRegistro, msgError)

                Try
                    With ds.Tables(0).Rows(0)

                        If (Not IsNothing(ddlMaquina.Items.FindByValue(.Item("id_maquina")))) Then
                            ddlMaquina.Items.FindByValue(.Item("id_maquina")).Selected = True
                        Else
                            ddlMaquina.Items.FindByValue("-1").Selected = True
                        End If


                        If Not IsNothing(ddlTamanhoMaquina.Items.FindByValue(.Item("id_tamanho_maquina"))) Then
                            ddlTamanhoMaquina.Items.FindByValue(.Item("id_tamanho_maquina")).Selected = True
                        End If

                        If Not .Item("cant_maquinas") Is DBNull.Value Then
                            tbNumeroMaquinas.Text = .Item("cant_maquinas")
                        Else
                            tbNumeroMaquinas.Text = "0"
                        End If

                        If Not .Item("num_cuerpos_impresores") Is DBNull.Value Then
                            tbCuerposImpresores.Text = .Item("num_cuerpos_impresores")
                        Else
                            tbCuerposImpresores.Text = "0"
                        End If

                        chkbTorreBarniz.Checked = .Item("con_torre_barniz")

                        If Not IsNothing(ddlTamanoPlancha.Items.FindByValue(.Item("id_tamanho_plancha"))) Then
                            ddlTamanoPlancha.Items.FindByValue(.Item("id_tamanho_plancha")).Selected = True
                        End If

                        If Not IsNothing(ddlTamanoMantilla.Items.FindByValue(.Item("id_tamanho_mantilla"))) Then
                            ddlTamanoMantilla.Items.FindByValue(.Item("id_tamanho_mantilla")).Selected = True
                        End If


                        hfIdRegistro.Value = idRegistro

                        If Not .Item("consumo_papel") Is DBNull.Value Then
                            tbConsumoPapel.Text = CType(.Item("consumo_papel"), Double).ToString("#,##0.0")
                        Else
                            tbConsumoPapel.Text = "0"
                        End If

                        If Not .Item("consumo_planchas") Is DBNull.Value Then
                            tbConsumoPlanchas.Text = CType(.Item("consumo_planchas"), Double).ToString("#,##0")
                        Else
                            tbConsumoPlanchas.Text = "0"
                        End If

                        If Not .Item("consumo_tintas_proceso") Is DBNull.Value Then
                            tbConsumoProceso.Text = CType(.Item("consumo_tintas_proceso"), Double).ToString("#,##0.0")
                        Else
                            tbConsumoProceso.Text = "0"
                        End If

                        If Not .Item("consumo_tintas_pantone") Is DBNull.Value Then
                            tbConsumoPantone.Text = CType(.Item("consumo_tintas_pantone"), Double).ToString("#,##0.0")
                        Else
                            tbConsumoPantone.Text = "0"
                        End If

                        If Not .Item("consumo_barniz") Is DBNull.Value Then
                            tbConsumoBarniz.Text = CType(.Item("consumo_barniz"), Double).ToString("#,##0.0")
                        Else
                            tbConsumoBarniz.Text = "0"
                        End If

                        If Not .Item("consumo_mantillas") Is DBNull.Value Then
                            tbConsumoMantillas.Text = CType(.Item("consumo_mantillas"), Double).ToString("#,##0")
                        Else
                            tbConsumoMantillas.Text = "0"
                        End If

                    End With

                Catch ex As Exception

                End Try

                wsAndes = Nothing

            End If

        End If

        lMensajeError.Text = ""

    End Sub

    Private Sub bGrabaMaquina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGrabaMaquina.Click

        If ddlMaquina.SelectedValue = "-1" Then
            lMensajeError.Text = "Seleccione máquina!!"
            Return
        End If


        Dim codigoCliente As String = Session("catastroMaquinasSeleccionCliente_codCliente")

        Dim idMaquina As String = ddlMaquina.SelectedValue
        Dim idTamanhoMaquina As String = ddlTamanhoMaquina.SelectedValue

        Dim numCuerposImpresores As Integer
        If IsNumeric(tbCuerposImpresores.Text.Trim) Then
            numCuerposImpresores = Integer.Parse(tbCuerposImpresores.Text.Trim)
        Else
            numCuerposImpresores = 0
        End If

        Dim conTorreBarniz As Boolean = chkbTorreBarniz.Checked
        Dim idTamanhoPlancha As String = ddlTamanoPlancha.SelectedValue
        Dim idTamanhoMantilla As String = ddlTamanoMantilla.SelectedValue

        Dim cantidadMaquinas As Integer
        If IsNumeric(tbNumeroMaquinas.Text) Then
            cantidadMaquinas = Integer.Parse(tbNumeroMaquinas.Text)
        Else
            'Cantidad -1 se traduce en inserción de valor NULL en campo de número de máquinas
            cantidadMaquinas = 0
        End If

        Dim consumoPapel As Double
        Dim consumoPlanchas As Double
        Dim consumoTintasProceso As Double
        Dim consumoTintasPantone As Double
        Dim consumoBarniz As Double
        Dim consumoMantillas As Double

        If IsNumeric(tbConsumoPapel.Text) Then
            consumoPapel = CType(tbConsumoPapel.Text, Double)
        Else
            consumoPapel = 0
        End If

        If IsNumeric(tbConsumoPlanchas.Text) Then
            consumoPlanchas = CType(tbConsumoPlanchas.Text, Double)
        Else
            consumoPlanchas = 0
        End If

        If IsNumeric(tbConsumoProceso.Text) Then
            consumoTintasProceso = CType(tbConsumoProceso.Text, Double)
        Else
            consumoTintasProceso = 0
        End If

        If IsNumeric(tbConsumoPantone.Text) Then
            consumoTintasPantone = CType(tbConsumoPantone.Text, Double)
        Else
            consumoTintasPantone = 0
        End If

        If IsNumeric(tbConsumoBarniz.Text) Then
            consumoBarniz = CType(tbConsumoBarniz.Text, Double)
        Else
            consumoBarniz = 0
        End If

        If IsNumeric(tbConsumoMantillas.Text) Then
            consumoMantillas = CType(tbConsumoMantillas.Text, Double)
        Else
            consumoMantillas = 0
        End If


        Dim wsAndes As cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv = New cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv
        msgError = ""

        msgError = ""
        Dim idRegistro As Integer

        Dim registro As cl.gms.andes.ws.catastroMaquinas.stRegistroMaquina = New cl.gms.andes.ws.catastroMaquinas.stRegistroMaquina
        With registro
            .codigoSociedad = "GMSC"
            .codigoCliente = codigoCliente
            .idMaquina = idMaquina
            .idTamanhoMaquina = idTamanhoMaquina
            .cantidadMaquinas = cantidadMaquinas
            .numCuerposImpresores = numCuerposImpresores
            .conTorreBarniz = conTorreBarniz
            .idTamanhoPlancha = idTamanhoPlancha
            .idTamanhoMantilla = idTamanhoMantilla
            .observaciones = ""

            .consumoPapel = consumoPapel
            .consumoPlanchas = consumoPlanchas
            .consumoTintasProceso = consumoTintasProceso
            .consumoTintasPantone = consumoTintasPantone
            .consumoBarniz = consumoBarniz
            .consumoMantillas = consumoMantillas
        End With

        If hfAccion.Value = "ingresar" Then

            hfIdRegistro.Value = wsAndes.ingresaNuevoRegistroMaquinaCliente(registro, msgError)

            If msgError.Length > 0 Then
                lMensajeError.Text = msgError
            Else
                lMensajeError.Text = "Datos grabados correctamente."
            End If

        ElseIf hfAccion.Value = "editar" Then
            msgError = ""

            registro.idRegistro = hfIdRegistro.Value

            If Not (wsAndes.actualizaRegistroMaquinaCliente(registro, msgError)) Then
                If msgError.Length > 0 Then
                    lMensajeError.Text = msgError
                End If
            Else
                lMensajeError.Text = "Datos actualizados correctamente."
            End If
        End If

        wsAndes = Nothing

        Literal1.Text = "<script type=""text/javascript"">" & vbCrLf
        Literal1.Text &= "<!--" & vbCrLf
        Literal1.Text &= "parent.opener.document.location = '/catastroClientes/maquinasV2.aspx?vista=xc&cc=" & codigoCliente & "';" & vbCrLf
        Literal1.Text &= "//-->" & vbCrLf
        Literal1.Text &= "</script>" & vbCrLf


    End Sub

    Private Sub cargaValoresControles()
        Dim wsAndes As cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv = New cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv

        'Cargamos tipos de máquinas
        With ddlMaquina
            .DataSource = wsAndes.obtieneMaquinas("GMSC", msgError)
            .DataTextField = "dmc_maquina"
            .DataValueField = "id_maquina"
            .DataBind()
            .Items.Add(New ListItem("", "-1"))
        End With

        'Cargamos tamaños de máquinas
        With ddlTamanhoMaquina
            .DataSource = wsAndes.obtieneTamanhoMaquinas("GMSC", 1, msgError)
            .DataTextField = "dmc_tamanho_maquina"
            .DataValueField = "id_tamanho_maquina"
            .DataBind()
        End With

        'Cargamos tamaños de planchas
        With ddlTamanoPlancha
            .DataSource = wsAndes.obtieneTamanhosPlanchas("GMSC", 1, msgError)
            .DataTextField = "dmc_tamanho_plancha"
            .DataValueField = "id_tamanho_plancha"
            .DataBind()
        End With

        'Cargamos tamaños de mantillas
        With ddlTamanoMantilla
            .DataSource = wsAndes.obtieneTamanhosMantillas("GMSC", 1, msgError)
            .DataTextField = "dmc_tamanho_mantilla"
            .DataValueField = "id_tamanho_mantilla"
            .DataBind()
        End With

        wsAndes = Nothing

    End Sub
End Class
