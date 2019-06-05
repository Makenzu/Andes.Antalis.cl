Public Class res_vta_x_interlocutor
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents txFecIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents plParams As System.Web.UI.WebControls.Panel
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents txFecTer As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvFecIni As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvFecTer As System.Web.UI.WebControls.RequiredFieldValidator

    Dim TotCantVta As Integer = 0
    Dim TotValVta As Double = 0

    Dim tUserInfo As usuario.t_Usuario
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cvFechas As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton

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


        If IsNothing(Session("USUARIO_AUTENTICADO")) = True Then
            Response.Redirect("logout.aspx")
            Response.End()
        Else
            tUserInfo = Session("USER_INFO")
        End If

        lbErrors.Text = ""
        btSend.Attributes.Add("onClick", "javascript:noDblClick(document.all.disbtn,this);")

        Dim cl As New System.Globalization.CultureInfo("en-US")

        If txFecIni.Text = "" Then
            txFecIni.Text = Date.Now.AddDays(-1).ToString("dd / MM / yyyy", cl)
        End If

        If txFecTer.Text = "" Then
            txFecTer.Text = Date.Now.ToString("dd / MM / yyyy", cl)
        End If

        If Page.IsPostBack Then

            Dim cod_cliente As String
            Dim cod_doc As String
            Dim fec_ini, fec_ter As Date
            Dim dtResult As DataTable


            If Date.Parse(txFecIni.Text) > Date.Parse(txFecTer.Text) Then
                cvFechas.Visible = True
            Else

                cvFechas.Visible = False
                Try

                    fec_ini = txFecIni.Text
                    fec_ter = txFecTer.Text

                    lbFecha.Text = " del " & fec_ini & " al " & fec_ter

                    dgResultado.DataSource = Nothing
                    dgResultado.DataBind()

                    dtResult = ventas.res_vta_x_interlocutor(tUserInfo.codigoSucursal, tUserInfo.codigoFilial, fec_ini, fec_ter, "")
                    dgResultado.DataSource = dtResult
                    dgResultado.DataBind()

                    If dtResult.Rows.Count > 0 Then
                        lbNota.Text = "Ultima Actualización: <b>" & dtResult.Rows(1).Item("ULT_ACTUALIZACION") & "</b>"
                        lbNota.Visible = True
                    Else
                        lbNota.Visible = False
                    End If


                Catch ex As Exception
                    lbErrors.Text = "ERROR: " & Err.Description
                    lbErrors.Visible = True
                    Err.Clear()
                    ' Throw ex
                Finally
                    dtResult.Dispose()

                End Try

            End If

        End If

    End Sub




    Private Sub dgResultado_SortCommand(ByVal source As Object, _
ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) _
Handles dgResultado.SortCommand


        TotCantVta = 0
        TotValVta = 0


        Try
            Dim ColumnToSort As String
            Dim SortExprs() As String

            SortExprs = Split(e.SortExpression, " ")
            ColumnToSort = SortExprs(0)

            If e.SortExpression.ToLower = Me.SortExpression.ToLower Then
                ' SortAscending = Not SortAscending
                Me.SortExpression = ColumnToSort & " ASC"
            Else
                'SortAscending = True
                Me.SortExpression = ColumnToSort & " DESC"
            End If

            Dim dv As DataView = New DataView(dgResultado.DataSource)
            dv.Sort = Me.SortExpression

            dgResultado.DataSource = dv
            dgResultado.DataBind()

        Catch ex As Exception
            lbErrors.Text = "ERROR: " & ex.Message
            Err.Clear()
        End Try

    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, _
ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound



        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then
            Dim imgUp1 As New System.Web.UI.WebControls.Image
            imgUp1.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp2 As New System.Web.UI.WebControls.Image
            imgUp2.ImageUrl = "images/sort_2arrows.gif"
            Dim imgUp3 As New System.Web.UI.WebControls.Image
            imgUp3.ImageUrl = "images/sort_2arrows.gif"
            e.Item.Cells(1).Controls.Add(imgUp1)
            e.Item.Cells(2).Controls.Add(imgUp2)
            e.Item.Cells(3).Controls.Add(imgUp3)
            e.Item.Cells(1).Attributes.Add("title", "Ordenar")
            e.Item.Cells(2).Attributes.Add("title", "Ordenar")
            e.Item.Cells(3).Attributes.Add("title", "Ordenar")
        End If


        ' DG  ITEM CODE
        Dim cl As New System.Globalization.CultureInfo("es-CL")

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If

            ' *** CODIGO PARA HIGHLIGHT  -END- ******
            e.Item.Attributes.Add("title", e.Item.Cells(1).Text().Trim)

            TotCantVta += e.Item.Cells(2).Text()
            TotValVta += e.Item.Cells(3).Text()

            If e.Item.Cells(2).Text().Trim() <> "" And e.Item.Cells(2).Text() <> "&nbsp;" Then
                e.Item.Cells(2).Text() = String.Format(cl, "{0:N0}", CInt(e.Item.Cells(2).Text()))
            End If

            If e.Item.Cells(3).Text().Trim() <> "" And e.Item.Cells(3).Text() <> "&nbsp;" Then
                e.Item.Cells(3).Text() = String.Format(cl, "{0:N0}", CDbl(e.Item.Cells(3).Text()))
            End If

        End If

        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(0).Text = "Totales : "
            e.Item.Cells(0).Attributes.Add("align", "left")

            e.Item.Cells(2).Attributes.Add("align", "right")
            e.Item.Cells(2).Text = String.Format(cl, "{0:N0}", TotCantVta)
            e.Item.Cells(3).Attributes.Add("align", "right")
            e.Item.Cells(3).Text = String.Format(cl, "{0:N0}", TotValVta)

        End If

    End Sub


    'The Page-level properties that write to ViewState
    Private Property SortExpression() As String
        Get
            Dim o As Object = viewstate("SortExpression")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return o.ToString
            End If
        End Get
        Set(ByVal Value As String)
            viewstate("SortExpression") = Value
        End Set
    End Property

    Private Property SortAscending() As Boolean
        Get
            Dim o As Object = viewstate("SortAscending")
            If o Is Nothing Then
                Return True
            Else
                Return Convert.ToBoolean(o)
            End If
        End Get
        Set(ByVal Value As Boolean)
            viewstate("SortAscending") = Value
        End Set
    End Property


End Class
