Imports System.Web.Services
Imports System.Data.SqlClient


<System.Web.Services.WebService(Namespace:="http://tempuri.org/app/wsEjecutivoComercial")> _
Public Class wsEjecutivoComercial
    Inherits System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

    Structure stRegistro
        Public codigoSociedad As String
        Public codigoEjecCom As String
        Public nombreEjecCom As String
        Public nombreEjecCom2 As String
    End Structure

    ' WEB SERVICE EXAMPLE
    ' The HelloWorld() example service returns the string Hello World.
    ' To build, uncomment the following lines then save and build the project.
    ' To test this web service, ensure that the .asmx file is the start page
    ' and press F5.
    '
    <WebMethod()> _
    Public Function obtieneEjecutivosComerciales(ByVal codigoSociedad As String) As String
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
            daSql.SelectCommand.ExecuteNonQuery()

            daSql.Fill(dtResult)

            Dim arr(dtResult.Rows.Count - 1) As stRegistro

            For i As Integer = 0 To dtResult.Rows.Count - 1
                Dim dr As DataRow = dtResult.Rows(i)
                arr(i).codigoSociedad = Trim(dr("cod_sociedad"))
                arr(i).codigoEjecCom = Trim(dr("cod_ejec_com"))
                arr(i).nombreEjecCom = Trim(dr("nom_ejec_com"))
                arr(i).nombreEjecCom2 = Trim(dr("nom_ejec_com_2"))
            Next

            Return AjaxPro.JavaScriptSerializer.Serialize(arr)

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            spCall = Nothing
            daSql = Nothing
            dtResult = Nothing
        End Try
    End Function

End Class
