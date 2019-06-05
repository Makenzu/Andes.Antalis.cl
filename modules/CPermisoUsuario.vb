Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections

Public Class CPermisoUsuario

    Public Enum eTipoAccion
        lectura
        modificacion
        creacion
        eliminacion
    End Enum

    Private tbPermisos As DataTable

    Public Sub New(ByVal codigoSociedad As String, ByVal username As String)
        Me.recupera(codigoSociedad, username)
    End Sub

    Public Function validaPermiso(ByVal modulo As String, ByVal tipoAccion As eTipoAccion) As Boolean
        Dim tienePermiso As Boolean = False
        Dim dv As DataView = New DataView(tbPermisos)
        dv.RowFilter = String.Format("cod_modulo='{0}'", modulo)
        If dv.Count > 0 Then
            Select Case tipoAccion
                Case eTipoAccion.creacion
                    tienePermiso = dv(0).Item("crea")
                Case eTipoAccion.eliminacion
                    tienePermiso = dv(0).Item("elimina")
                Case eTipoAccion.lectura
                    tienePermiso = dv(0).Item("lee")
                Case eTipoAccion.modificacion
                    tienePermiso = dv(0).Item("modifica")
            End Select
        End If
        dv = Nothing
        Return tienePermiso
    End Function

    Private Sub recupera(ByVal codigoSociedad As String, ByVal username As String)
        Const spName = "ido_sel_modulos_usuario"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@username", username).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

            tbPermisos = New DataTable
            daSql.Fill(tbPermisos)

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try


    End Sub
End Class
