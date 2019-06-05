Imports System.Data
Imports System.Data.SqlClient

Public Class editaMaquina
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents bGrabaMaquina As System.Web.UI.WebControls.Button
    Protected WithEvents bCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents lMensajeError As System.Web.UI.WebControls.Label
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents fhIdRegistro As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents tbDmcMaquina As System.Web.UI.WebControls.TextBox
    Protected WithEvents hfAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Hidden2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hfIdMaquina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlTipoMaquina As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal

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

    Structure stMaquina
        Dim id As Integer
        Dim codTipoMaquina As String
        Dim denominacion As String
    End Structure

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

            bCancelar.Attributes.Add("onClick", "window.close()")
            hfAccion.Value = Request("accion")


            'Poblamos tipos de máquinas
            With ddlTipoMaquina
                .DataSource = Me.obtieneTiposMaquinas()
                .DataTextField = "dmc_tipo"
                .DataValueField = "cod_tipo"
                .DataBind()
                .Items.Add(New ListItem("* Seleccione *", "*"))
            End With

            If hfAccion.Value = "edita" Then
                'recuperamos datos de la máquina
                Dim idMaquina As Integer = Request("idMaquina")
                Try
                    Dim maquina As stMaquina = Me.obtieneMaquina(idMaquina)
                    tbDmcMaquina.Text = maquina.denominacion

                    If (Not IsNothing(ddlTipoMaquina.Items.FindByValue(maquina.codTipoMaquina))) Then
                        ddlTipoMaquina.Items.FindByValue(maquina.codTipoMaquina).Selected = True
                    End If

                    hfIdMaquina.Value = maquina.id
                    hfAccion.Value = "guardar cambios"
                    bGrabaMaquina.Text = "guardar cambios"
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try
            ElseIf (hfAccion.Value = "ingresa") Then
                bGrabaMaquina.Text = "guardar"
                hfAccion.Value = "guardar"
            End If

        End If

        lMensajeError.Text = ""

    End Sub

    

    Public Function guarda(ByVal codigoTipo As String, ByVal dmcMaquina As String) As Integer
        Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

        Dim spCall As SqlCommand = New SqlCommand("cma_ing_maquina", dbConn)

        Try
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output
            spCall.Parameters.Add("@cod_tipo", codigoTipo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@dmc_maquina", dmcMaquina).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()

            Return spCall.Parameters("@id").Value

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function

    Public Function guardaCambios(ByVal idMaquina As Integer, ByVal codigoTipo As String, ByVal dmcMaquina As String) As Integer
        Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

        Dim spCall As SqlCommand = New SqlCommand("cma_act_maquina", dbConn)

        Try
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id", idMaquina).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_tipo", codigoTipo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@dmc_maquina", dmcMaquina).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()

            Return spCall.Parameters("@id").Value

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function



    Public Function obtieneMaquina(ByVal idMaquina As Integer) As stMaquina
        Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

        Dim spCall As SqlCommand = New SqlCommand("cma_sel_maquina", dbConn)

        Try
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id", idMaquina).Direction = ParameterDirection.Input

            Dim daSql As SqlDataAdapter = New SqlDataAdapter
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            If (spCall.Parameters("@resultValue").Value >= 0) Then
                Dim resultDT As DataTable = New DataTable
                daSql.Fill(resultDT)

                If resultDT.Rows.Count = 1 Then
                    Dim sResult As stMaquina
                    sResult.denominacion = resultDT.Rows(0).Item("dmc_maquina")
                    sResult.id = resultDT.Rows(0).Item("id_maquina")
                    sResult.codTipoMaquina = resultDT.Rows(0).Item("cod_tipo")
                    Return sResult
                End If

            End If

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function

    Public Function obtieneTiposMaquinas() As DataTable
        Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

        Dim spCall As SqlCommand = New SqlCommand("cma_sel_tipos_maquinas", dbConn)

        Try
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            Dim daSql As SqlDataAdapter = New SqlDataAdapter
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            If (spCall.Parameters("@resultValue").Value >= 0) Then
                Dim resultDT As DataTable = New DataTable
                daSql.Fill(resultDT)

                Return resultDT
            End If

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function

    Private Sub bGrabaMaquina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGrabaMaquina.Click
        If (hfAccion.Value = "guardar cambios") Then
            'Actualizamos los datos de la máquina
            Dim idMaquina = Me.guardaCambios(hfIdMaquina.Value, ddlTipoMaquina.SelectedValue, tbDmcMaquina.Text)
            If idMaquina <= 0 Then
                Response.Write("No se pudo completar la operación.")
                Return
            Else
                Session("MENSAJE_USUARIO") = "Se han aplicado los cambios."
            End If
        ElseIf (hfAccion.Value = "guardar") Then
            'Ingresamos la nueva máquina
            Dim idMaquina = Me.guarda(ddlTipoMaquina.SelectedValue, tbDmcMaquina.Text)
            If idMaquina <= 0 Then
                Response.Write("No se pudo completar la operación.")
                Return
            Else
                Session("MENSAJE_USUARIO") = "Se ha grabado la nueva máquina."
            End If
        End If

        Literal1.Text = "<script type=""text/javascript"">" & vbCrLf
        Literal1.Text &= "<!--" & vbCrLf
        Literal1.Text &= "parent.opener.document.location = '/catastroClientes/manMaquinas.aspx';" & vbCrLf
        Literal1.Text &= "window.close();"
        Literal1.Text &= "//-->" & vbCrLf
        Literal1.Text &= "</script>" & vbCrLf

    End Sub
End Class
