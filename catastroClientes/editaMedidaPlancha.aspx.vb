Imports System.Data
Imports System.Data.SqlClient

Public Class editaMedidaPlancha
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents bGrabaMaquina As System.Web.UI.WebControls.Button
    Protected WithEvents bCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents lMensajeError As System.Web.UI.WebControls.Label
    Protected WithEvents hfAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hfIdMedidaPlancha As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents tbDmcMedidaPlancha As System.Web.UI.WebControls.TextBox

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

    Structure stMedidaPlancha
        Dim id As Integer
        Dim denominacion As String
    End Structure

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

            bCancelar.Attributes.Add("onClick", "window.close()")
            hfAccion.Value = Request("accion")

            If hfAccion.Value = "edita" Then
                'recuperamos datos de la máquina
                Dim idMedidaPlancha As Integer = Request("idMedidaPlancha")
                Try
                    Dim medidaPlancha As stMedidaPlancha = Me.obtieneMedidaPlancha(idMedidaPlancha)
                    tbDmcMedidaPlancha.Text = medidaPlancha.denominacion

                    hfIdMedidaPlancha.Value = medidaPlancha.id
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



    Public Function guarda(ByVal medidaPlancha As stMedidaPlancha) As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

        Dim spCall As SqlCommand = New SqlCommand("cma_ing_medida_plancha", dbConn)

        Try
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output
            spCall.Parameters.Add("@dmc_medida_plancha", medidaPlancha.denominacion).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()

            Return (spCall.Parameters("@resultValue").Value > 0)

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function

    Public Function guardaCambios(ByVal medidaPlancha As stMedidaPlancha) As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

        Dim spCall As SqlCommand = New SqlCommand("cma_act_medida_plancha", dbConn)

        Try
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id", medidaPlancha.id).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@dmc_medida_plancha", medidaPlancha.denominacion).Direction = ParameterDirection.Input

            spCall.ExecuteNonQuery()

            Return (spCall.Parameters("@resultValue").Value > 0)

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
        End Try
    End Function



    Public Function obtieneMedidaPlancha(ByVal idMedidaPlancha As Integer) As stMedidaPlancha
        Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

        Dim spCall As SqlCommand = New SqlCommand("cma_sel_medida_plancha", dbConn)

        Try
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@id", idMedidaPlancha).Direction = ParameterDirection.Input

            Dim daSql As SqlDataAdapter = New SqlDataAdapter
            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            If (spCall.Parameters("@resultValue").Value >= 0) Then
                Dim resultDT As DataTable = New DataTable
                daSql.Fill(resultDT)

                If resultDT.Rows.Count = 1 Then
                    Dim sResult As stMedidaPlancha
                    sResult.denominacion = resultDT.Rows(0).Item("dmc_tamanho_plancha")
                    sResult.id = resultDT.Rows(0).Item("id_tamanho_plancha")

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


    Private Sub bGrabaMaquina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGrabaMaquina.Click

        Dim medidaPlancha As stMedidaPlancha

        With medidaPlancha
            .denominacion = tbDmcMedidaPlancha.Text
        End With


        If (hfAccion.Value = "guardar cambios") Then
            'Actualizamos los datos de la medida de plancha
            medidaPlancha.id = hfIdMedidaPlancha.Value

            If Not (Me.guardaCambios(medidaPlancha)) Then
                Response.Write("No se pudo completar la operación.")
                Return
            Else
                Session("MENSAJE_USUARIO") = "Se han aplicado los cambios."
            End If
        ElseIf (hfAccion.Value = "guardar") Then
            'Ingresamos la nueva medida de plancha
            If (Not Me.guarda(medidaPlancha)) Then
                Response.Write("No se pudo completar la operación.")
                Return
            Else
                Session("MENSAJE_USUARIO") = "Se ha grabado la nueva medida de plancha."
            End If
        End If

        Literal1.Text = "<script type=""text/javascript"">" & vbCrLf
        Literal1.Text &= "<!--" & vbCrLf
        Literal1.Text &= "parent.opener.document.location = '/catastroClientes/manMedidasPlanchas.aspx';" & vbCrLf
        Literal1.Text &= "window.close();"
        Literal1.Text &= "//-->" & vbCrLf
        Literal1.Text &= "</script>" & vbCrLf

    End Sub
End Class
