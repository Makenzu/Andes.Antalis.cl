Imports System.Data.SqlClient
Imports System.Data

Public Class mcEjecutivoComercial
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Structure stRegistro
        Public codigoSociedad As String
        Public codigoEjecCom As String
        Public nombreEjecCom As String
        Public nombreEjecCom2 As String
    End Structure

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim codigoSociedad As String = Request("codigoSociedad")

        If codigoSociedad.Length > 0 Then
            Const spName As String = "aws_sel_ejec_com"

            Dim dbConn As SqlConnection = New SqlConnection
            Dim daSql As SqlDataAdapter = New SqlDataAdapter
            Dim dtResult As DataTable = New DataTable
            Dim msgError As String = ""
            Dim resultSQL As Integer
            Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)

            Try

                dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
                dbConn.Open()

                spCall.CommandType = CommandType.StoredProcedure

                spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input

                daSql.SelectCommand = spCall

                daSql.Fill(dtResult)

                Dim arr(dtResult.Rows.Count - 1) As stRegistro

                For i As Integer = 0 To dtResult.Rows.Count - 1
                    Dim dr As DataRow = dtResult.Rows(i)
                    arr(i).codigoSociedad = Trim(dr("cod_sociedad"))
                    arr(i).codigoEjecCom = Trim(dr("cod_ejec_com"))
                    arr(i).nombreEjecCom = Trim(dr("nom_ejec_com"))
                    arr(i).nombreEjecCom2 = Trim(dr("nom_ejec_com_2"))
                Next

                Response.Write(AjaxPro.JavaScriptSerializer.Serialize(arr))

            Catch ex As Exception
                Throw ex
            Finally
                dbConn.Close()
                spCall = Nothing
                daSql = Nothing
                dtResult = Nothing
            End Try
        End If


    End Sub

End Class
