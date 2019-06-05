Imports System.Data
Imports System.Data.SqlClient

Public Class WebForm3
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents LCodigoProd As System.Web.UI.WebControls.Label
    Protected WithEvents lNombreProd As System.Web.UI.WebControls.Label


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim codigoSociedad As String = "GMSC"
    Dim totalCantidadUMB As Double
    Dim totalCantidadSolicitada As Double
    Dim codigoUMB As String = ""
    Dim maestroMateriales As cl.gms.andes.ws.materiales.materialesSrv = New cl.gms.andes.ws.materiales.materialesSrv

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim codigoMaterial As String = Request.QueryString("codigoMaterial")

        Dim dsMaterial As DataSet = maestroMateriales.obtieneMaterial("CHI", codigoMaterial)
        LCodigoProd.Text = codigoMaterial & " - "
        lNombreProd.Text = dsMaterial.Tables(0).Rows(0).Item("des_producto")
        totalCantidadUMB = 0
        dgResultado.DataSource = obtieneDetalleOC(codigoSociedad, codigoMaterial)
        dgResultado.DataBind()

    End Sub

    Private Function obtieneDetalleOC(ByVal codigoSociedad As String, _
                                            ByVal codigoMaterial As String) As DataTable


        Const spName = "ido_sel_acs_oc_x_material"
        Dim dbConn As SqlConnection = New SqlConnection
        Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)
        Dim daSql As SqlDataAdapter = New SqlDataAdapter

        Try
            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()
            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_material", codigoMaterial).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

            Dim resultDT As DataTable = New DataTable
            daSql.Fill(resultDT)
            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
        End Try

    End Function


    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
        ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(2).Text = String.Format("{0} {1}", CType(e.Item.DataItem("cant_solicitada"), Double).ToString("#,##0.00"), e.Item.DataItem("cod_unid_med"))
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(4).Text = String.Format("{0} {1}", ctype(e.Item.DataItem("cant_solicitada_umb"), Double).ToString ("#,##0"), e.Item.DataItem("umb_cant_solicitada"))
            e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right

            totalCantidadUMB = totalCantidadUMB + e.Item.DataItem("cant_solicitada_umb")

            If codigoUMB = "" Then
                codigoUMB = e.Item.Cells(5).Text
            End If


        ElseIf e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(2).Text = "TOTAL:"
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(4).Text = totalCantidadUMB.ToString("#,##0") & " " & codigoUMB
            e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right

        End If

    End Sub
End Class