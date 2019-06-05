Public Class frmPlotter
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lCliente As System.Web.UI.WebControls.Label
    Protected WithEvents bGrabaMaquina As System.Web.UI.WebControls.Button
    Protected WithEvents bCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents hfAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents tbNumeroMaquinas As System.Web.UI.WebControls.TextBox
    Protected WithEvents lMensajeError As System.Web.UI.WebControls.Label
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents fhIdRegistro As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hfIdRegistro As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents ddlPlotter As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipoTinta As System.Web.UI.WebControls.DropDownList

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
            lCliente.Text = Session("catastroPlottersSeleccionCliente_codCliente") & " - " & Session("catastroPlottersSeleccionCliente_razonSocial")

            bCancelar.Attributes.Add("onClick", "window.close()")

            cargaValoresControles()
            hfAccion.Value = Request("accion")

            If hfAccion.Value = "editar" Then
                'recuperamos datos de la máquina
                Dim idRegistro As Integer = Request("idRegistro")
                Dim wsAndes As cl.gms.andes.ws.catastroPlotters.catastroPlottersSrv = New cl.gms.andes.ws.catastroPlotters.catastroPlottersSrv

                Dim ds As DataSet = wsAndes.obtieneRegistroMaquinaCliente(idRegistro, msgError)

                Try
                    With ds.Tables(0).Rows(0)

                        ddlPlotter.Items.FindByValue(.Item("id_plotter")).Selected = True
                        ddlTipoTinta.Items.FindByValue(.Item("id_tipo_tinta")).Selected = True

                        If Not .Item("cant_maquinas") Is DBNull.Value Then
                            tbNumeroMaquinas.Text = .Item("cant_maquinas")
                        Else
                            tbNumeroMaquinas.Text = "0"
                        End If

                        hfIdRegistro.Value = idRegistro

                    End With

                Catch ex As Exception

                End Try

                wsAndes = Nothing

            End If

        End If

        lMensajeError.Text = ""

    End Sub

    Private Sub bGrabaMaquina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGrabaMaquina.Click
        Dim codigoCliente As String = Session("catastroPlottersSeleccionCliente_codCliente")

        Dim idPlotter As Integer = ddlPlotter.SelectedValue
        Dim idTipoTinta As String = ddlTipoTinta.SelectedValue

        Dim cantidadMaquinas As Integer
        If IsNumeric(tbNumeroMaquinas.Text) Then
            cantidadMaquinas = Integer.Parse(tbNumeroMaquinas.Text)
        Else
            'Cantidad -1 se traduce en inserción de valor NULL en campo de número de máquinas
            cantidadMaquinas = 0
        End If

        Dim wsAndes As cl.gms.andes.ws.catastroPlotters.catastroPlottersSrv = New cl.gms.andes.ws.catastroPlotters.catastroPlottersSrv
        msgError = ""

        msgError = ""
        Dim idRegistro As Integer

        If hfAccion.Value = "ingresar" Then

            hfIdRegistro.Value = wsAndes.ingresaNuevoRegistroMaquinaCliente("GMSC", codigoCliente, idPlotter, cantidadMaquinas, idTipoTinta, "", msgError)

            If msgError.Length > 0 Then
                lMensajeError.Text = msgError
            Else
                lMensajeError.Text = "Datos grabados correctamente."
            End If

        ElseIf hfAccion.Value = "editar" Then
            msgError = ""

            idRegistro = hfIdRegistro.Value

            If Not (wsAndes.actualizaRegistroMaquinaCliente(idRegistro, "GMSC", codigoCliente, idPlotter, cantidadMaquinas, idTipoTinta, "", msgError)) Then
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
        Literal1.Text &= "parent.opener.document.location = '/catastroClientes/plotters.aspx?accion=buscar&codCliente=" & codigoCliente & "';" & vbCrLf
        Literal1.Text &= "//-->" & vbCrLf
        Literal1.Text &= "</script>" & vbCrLf


    End Sub

    Private Sub cargaValoresControles()
        Dim wsAndes As cl.gms.andes.ws.catastroPlotters.catastroPlottersSrv = New cl.gms.andes.ws.catastroPlotters.catastroPlottersSrv

        'Cargamos tipos de máquinas
        With ddlPlotter
            .DataSource = wsAndes.obtienePlotters("GMSC", msgError)
            .DataTextField = "dmc_plotter"
            .DataValueField = "id_plotter"
            .DataBind()
        End With

        'Cargamos tipos de tintas
        With ddlTipoTinta
            .DataSource = wsAndes.obtieneTiposTintas("GMSC", msgError)
            .DataTextField = "dmc_tipo_tinta"
            .DataValueField = "id_tipo_tinta"
            .DataBind()
        End With

        wsAndes = Nothing
    End Sub
End Class
