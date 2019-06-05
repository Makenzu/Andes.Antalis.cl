Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Dim Cod_Filial As String
        Dim Cod_Sucursal As String
        Dim Ano_Periodo As Integer
        Dim Mes_Periodo As Integer
        Dim Cod_Promotora As String
        Dim Cod_Sector As String
        Dim Nombre_Report As String

        Try

            Cod_Filial = Request("cod_filial")
            Cod_Sucursal = Request("cod_sucursal")
            Ano_Periodo = CInt(Request("Ano_Periodo"))
            Mes_Periodo = CInt(Request("Mes_periodo"))
            Cod_Promotora = Request("Cod_Promotora")
            Cod_Sector = Request("Cod_Sector")
            Nombre_Report = Request("report_name")

            'Nombre_Report = "Matriz_Analisis_Venta-Cliente_new con lineas.rpt"

            Dim crDiskFileDestOpt As DiskFileDestinationOptions = New DiskFileDestinationOptions

            Dim crRepDoc As ReportDocument = New ReportDocument
            crRepDoc.Load(Request.PhysicalApplicationPath & "reports\" & _
                            "Matriz_Analisis_Venta-Cliente_new con lineas.rpt")

            ' Parametros Informe...
            crRepDoc.SetParameterValue("@ano_periodo", Ano_Periodo)
            crRepDoc.SetParameterValue("@mes_periodo", Mes_Periodo)
            crRepDoc.SetParameterValue("@Cod_Filial", Cod_Filial)
            crRepDoc.SetParameterValue("@Cod_Sucursal", Cod_Sucursal)
            crRepDoc.SetParameterValue("@Cod_Promotora", Cod_Promotora)
            crRepDoc.SetParameterValue("@Cod_Sector", Cod_Sector)
            crRepDoc.SetParameterValue("@errorMsg", ".")

            crRepDoc.SetDatabaseLogon("andes_user", "ant20*06")

            Dim ExpFile As String
            ExpFile = Session.SessionID & ".pdf"
            crDiskFileDestOpt.DiskFileName = Request.PhysicalApplicationPath & _
                "cache\" & ExpFile

            With crRepDoc.ExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .ExportDestinationOptions = crDiskFileDestOpt
            End With

            crRepDoc.Export()

            Response.Redirect("cache\" & ExpFile)

        Catch ex As Exception

            Label1.Text = "Error: " & ex.Message

        End Try
    End Sub

End Class
