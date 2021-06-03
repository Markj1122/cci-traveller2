Imports MySql.Data.MySqlClient

Module Functions

    '---------------------------------------------------------------------------------------------------------------------------------------
    'DEVELOPMENT SERVER
    '---------------------------------------------------------------------------------------------------------------------------------------

    'Public server As String = "localhost"
    'Public user_id As String = "root"
    'Public password As String = ""

    '---------------------------------------------------------------------------------------------------------------------------------------
    'PRODUCTION SERVER
    '---------------------------------------------------------------------------------------------------------------------------------------

    Public server As String = "172.16.2.41"
    Public user_id As String = "sdjeff2"
    Public password As String = "sdjeff1"

    '---------------------------------------------------------------------------------------------------------------------------------------
    'MYSQL CONNECTION
    '---------------------------------------------------------------------------------------------------------------------------------------

    Public con As New MySqlConnection("server=" & server & ";user id=" & user_id & ";password=" & password & ";database=ccp_management_database;AllowZeroDatetime=TRUE")
    Public con_swp As New MySqlConnection("server=" & server & ";user id=" & user_id & ";password=" & password & ";database=ccp_swp;AllowZeroDatetime=TRUE")
    Public con_gl1_bgw1 As New MySqlConnection("server=" & server & ";user id=" & user_id & ";password=" & password & ";database=ccp_management_database;AllowZeroDatetime=TRUE")
    Public con_gl2_bgw1 As New MySqlConnection("server=" & server & ";user id=" & user_id & ";password=" & password & ";database=ccp_swp;AllowZeroDatetime=TRUE")
    Public con_gl1_bgw2 As New MySqlConnection("server=" & server & ";user id=" & user_id & ";password=" & password & ";database=ccp_cci;AllowZeroDatetime=TRUE")
    Public con_gl2_bgw2 As New MySqlConnection("server=" & server & ";user id=" & user_id & ";password=" & password & ";database=ccp_cci;AllowZeroDatetime=TRUE")
    Public con_gl1_bgw3 As New MySqlConnection("server=" & server & ";user id=" & user_id & ";password=" & password & ";database=ccp_management_database;AllowZeroDatetime=TRUE")
    Public con_gl2_bgw3 As New MySqlConnection("server=" & server & ";user id=" & user_id & ";password=" & password & ";database=ccp_swp;AllowZeroDatetime=TRUE")

    'Clear rows on a specified datagridview
    Public Delegate Sub SetDataGridViewRowsClearInvoker(ByVal datagridview As DataGridView)
    Public Sub SetDataGridViewRowsClear(ByVal datagridview As DataGridView)

        If datagridview.InvokeRequired Then
            datagridview.Invoke(New SetDataGridViewRowsClearInvoker(AddressOf SetDataGridViewRowsClear), datagridview)
        Else
            datagridview.Rows.Clear()
        End If

    End Sub

    'Sets the double buffer property of a specified control
    Public Sub SetDoubleBuffering(ByVal control As System.Windows.Forms.Control, ByVal value As Boolean)
        Dim controlProperty As System.Reflection.PropertyInfo = GetType(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
        controlProperty.SetValue(control, value, Nothing)
    End Sub

    'Gets the string value before a specified string
    Public Function Get_String_Before(ByVal value As String, ByVal a As String) As String

        Dim posA As Integer = value.IndexOf(a)
        If posA = -1 Then
            Return ""
        End If
        Return value.Substring(0, posA)

    End Function

    'Gets the string value after a specified string
    Public Function Get_String_After(ByVal value As String, ByVal a As String) As String

        Dim posA As Integer = value.LastIndexOf(a)
        If posA = -1 Then
            Return ""
        End If
        Dim adjustedPosA As Integer = posA + a.Length
        If adjustedPosA >= value.Length Then
            Return ""
        End If
        Return value.Substring(adjustedPosA)

    End Function

    'Method that releases COM objects used
    Public Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    'Gets pn 
    Public Function getPN(ByVal po As String) As String
        Dim pn As String = ""
        Dim dt As New DataTable
        Dim data As New MySqlDataAdapter("SELECT hct_PartsNumber FROM hct_info_cap_order WHERE hct_po_no = '" & po & "'", con_gl1_bgw2)
        data.Fill(dt)
        data.Dispose()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                pn = dt.Rows(i)("hct_PartsNumber")
            Next
        End If
        Return pn
    End Function

    Public Function getScantime(ByVal inoutid As Integer) As Integer
        Dim dt As New DataTable
        Dim data As New MySqlDataAdapter("SELECT comparedscan FROM cci_inout_assign WHERE inout_assign_id=" & inoutid & "", con_cci)
        data.Fill(dt)
        data.Dispose()
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return 0
        End If
    End Function

    Public Function getShipdate(ByVal inoutid As Integer) As String
        Dim dt As New DataTable
        Dim data As New MySqlDataAdapter("SELECT shipdate FROM cci_inout_assign WHERE inout_assign_id=" & inoutid & "", con_cci)
        Data.Fill(dt)
        Data.Dispose()
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return ""
        End If
    End Function
    Delegate Sub addDGVRow(dgv As DataGridView, row As DataGridViewRow)
    Sub doRowAdd(dgv As DataGridView, row As DataGridViewRow)
        Try
            If dgv.InvokeRequired Then
                dgv.Invoke(New addDGVRow(AddressOf doRowAdd), dgv, row)
            Else
                dgv.Rows.Add(row)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function pnToitemtype(ByVal lotno As String) As String
        Dim pn As String = ""
        Dim dt As New DataTable
        Dim data As New MySqlDataAdapter("SELECT DISTINCT(parstnumber) FROM  cci_stocknew_lotstatus WHERE lot_no LIKE '%" & lotno & "%' AND received_date > '" & "1990-01-01" & "' " & _
                                           "AND zerobalance <> 1 AND ngconfirmed <> 1 AND ngpulledout <> 1 AND lostwafer <> 1 ORDER BY received_date DESC", con_cci)
        data.Fill(dt)
        data.Dispose()
        If dt.Rows.Count > 0 Then
            pn = dt.Rows(0)(0)
        Else
            Dim dt2 As New DataTable
            Dim data2 As New MySqlDataAdapter("SELECT DISTINCT(Partsnumber) FROM ccd_received_wafer_tables WHERE lotnumber LIKE '%" & lotno & "%' AND date_received > '1990-01-01' AND " & _
                                              "cci_received <> 1 AND NGConfirmed <> 1 AND NGPulledOut <> 1 AND NG <> 1 ORDER BY date_received DESC", ccp_management_db)
            data2.Fill(dt2)
            data2.Dispose()
            If dt2.Rows.Count > 0 Then
                pn = dt2.Rows(0)(0)
            Else
                pn = ""
            End If
        End If
        Return pn
    End Function
End Module
