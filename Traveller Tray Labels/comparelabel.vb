Imports MySql.Data.MySqlClient
Public Class comparelabel

    Private Sub Btn_confirm_Click(sender As System.Object, e As System.EventArgs) Handles Btn_confirm.Click
        MessageBox.Show("CONFIRMED AND RECORDED", "RECORDED", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Lbl_res.Visible = True
        Lbl_match.Visible = False
        Lbl_mismatch.Visible = False
        Pcb_qmark.Visible = True
        Pcb_match.Visible = False
        Pcb_mismatch.Visible = False
        Pcbqc_mismatch.Visible = False
        Pcbqc_match.Visible = False
        Pcbqc_result.Visible = True
        Btn_confirm.Enabled = False
        saveMatchscan()
        Txtbx_potraveller.Focus()
        Txtbx_potraveller.Clear()
        Txtbx_prodworkqrcode.Clear()
        Txtbx_qcworktraveller.Clear()
        Txtbx_pnshipdateqr.Clear()
        Txtbx_qcpoqrcode.Clear()
        Txtbx_qcpnshipdate.Clear()
        Lblpo_lotno.Text = "LOT NO:"
        lblpo_pn.Text = "PN:"
        Lbl_pnqrcode.Text = "PN:"
        Lblprod_lotno.Text = "LOT NO:"
        lblprod_pn.Text = "PN:"
        Lblqc_lotno.Text = "LOT NO:"
        Lblqc_pn.Text = "PN:"
        Lblqc_poqrcodepn.Text = "PN:"
        Lblqc_poqrcodeln.Text = "LOT NO:"
        Lblqc_pnshipdateqr.Text = "PN:"
        Lblprod_lotno.ForeColor = Color.Black
        Lblqc_lotno.ForeColor = Color.Black
        lblprod_pn.ForeColor = Color.Black
        Lblqc_pn.ForeColor = Color.Black
        Lbl_pnqrcode.ForeColor = Color.Black
        Lblpo_lotno.ForeColor = Color.Black
        lblpo_pn.ForeColor = Color.Black
        Lblqc_poqrcodepn.ForeColor = Color.Black
        Lblqc_poqrcodeln.ForeColor = Color.Black
        Lblqc_pnshipdateqr.ForeColor = Color.Black
    End Sub
    Dim potravlotno As String
    Dim potravpn As String
    Dim inoutid As String
    Dim inoutidqc As String
    Dim pono As String
    Dim poprodpnnotsplit As String
    Dim pnprodpnnotsplit As String
    Dim workprodpnnotsplit As String
    Dim poqcpnnotsplit As String
    Dim pnqcpnnotsplit As String
    Dim workqcpnnotsplit As String
    Private Sub Txtbx_potraveller_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Txtbx_potraveller.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim splitpoqr() As String
            Dim splitid() As String
            Dim pntosplit As String
            If Trim(Txtbx_potraveller.Text) <> "" Then
                splitpoqr = (Txtbx_potraveller.Text).Split("[")
                Try
                    potravlotno = splitpoqr(0)
                    splitid = (splitpoqr(1)).Split("]")
                    pono = splitid(0)
                    pntosplit = getPN(splitid(0))
                    poprodpnnotsplit = pntosplit
                    potravpn = pntosplit.Substring(0, 6)
                    inoutid = splitid(1)
                    Lblpo_lotno.Text = "LOT NO:" & potravlotno
                    lblpo_pn.Text = "PN:" & pntosplit
                    getAppliedLot()
                    If Trim(Txtbx_pnshipdateqr.Text) = "" Then
                        Txtbx_pnshipdateqr.Focus()
                    ElseIf Trim(Txtbx_prodworkqrcode.Text) <> "" And Txtbx_pnshipdateqr.Text <> "" Then
                        'If verifyorderPN(pntosplit) <> 0 Then
                        '    comparetomatch(3)
                        'Else
                        '    comparetomatch(1)
                        'End If
                        comparetomatch(1)
                    End If
                Catch ex As Exception
                    MessageBox.Show("PLEASE SCAN THE VALID QR FOR PO TRAVELLER", "INVALID QR CODE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Txtbx_potraveller.Clear()
                End Try

            End If
        End If
    End Sub
    Function assDocnoprod(ByVal inoutID As String, ByVal loc As String) As String
        Dim val As String = ""
        Dim dt As New DataTable
        Dim data As New MySqlDataAdapter("SELECT itemtype_processcard FROM cci_itemtype WHERE itemtype_id IN(SELECT itemtype FROM cci_inout_assign WHERE inout_assign_id = '" & inoutID & "')", con_cci)
        data.Fill(dt)
        data.Dispose()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If loc = "PROD" Then
                    If dt.Rows(i)("itemtype_processcard") = "custom" Then
                        val = "045"
                    Else
                        val = "030"
                    End If
                ElseIf loc = "QC" Then
                    If dt.Rows(i)("itemtype_processcard") = "custom" Then
                        val = "539"
                    Else
                        val = "129"
                    End If
                End If
            Next
        End If
        Return val
    End Function
    Dim qctravpn As String
    Dim qctravlotno As String
    Dim qcdocno As String
    Private Sub Txtbx_qcworktraveller_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Txtbx_qcworktraveller.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim splitqr() As String
            Dim splitqrlot() As String
            Dim pntosplit As String
            If Trim(Txtbx_qcworktraveller.Text) <> "" Then
                splitqr = (Txtbx_qcworktraveller.Text).Split("[")
                Try
                    pntosplit = splitqr(0)
                    splitqrlot = (splitqr(1)).Split("] ")
                    qcdocno = splitqrlot(1)
                    qctravlotno = splitqrlot(0)
                    workqcpnnotsplit = pntosplit
                    qctravpn = pntosplit.Substring(0, 6)
                    Lblqc_pn.Text = "PN:" & pntosplit
                    Lblqc_lotno.Text = "LOT NO:" & qctravlotno
                    If assDocnoprod(inoutidqc, "QC") <> qcdocno Then
                        MessageBox.Show("THIS IS NOT THE QC WORK TRAVELLER", "INVALID QR CODE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Txtbx_qcworktraveller.Clear()
                        Txtbx_qcworktraveller.Focus()
                    Else
                        If Trim(Txtbx_prodworkqrcode.Text) <> "" And Txtbx_pnshipdateqr.Text <> "" Then
                            'If verifyorderPN(poqcpnnotsplit) <> 0 Then
                            '    comparetomatch(4)
                            'Else
                            '    comparetomatch(2)
                            'End If
                            comparetomatch(2)
                        End If
                    End If
                   
                Catch ex As Exception
                    MessageBox.Show("PLEASE SCAN THE VALID QR FOR QC WORK TRAVELLER", "INVALID QR CODE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Txtbx_qcworktraveller.Clear()
                    Txtbx_qcworktraveller.Focus()
                End Try

            End If
        End If
        
    End Sub
    Dim prodtravpn As String
    Dim prodtravlotno As String
    Dim proddocno As String
    Private Sub Txtbx_prodworkqrcode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Txtbx_prodworkqrcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim splitqr() As String
            Dim splitqrlot() As String
            Dim pntosplit As String
            If Trim(Txtbx_prodworkqrcode.Text) <> "" Then
                splitqr = (Txtbx_prodworkqrcode.Text).Split("[")
                Try
                    pntosplit = splitqr(0)
                    splitqrlot = (splitqr(1)).Split("]")
                    proddocno = splitqrlot(1)
                    workprodpnnotsplit = pntosplit
                    prodtravlotno = splitqrlot(0)
                    prodtravpn = pntosplit.Substring(0, 6)
                    lblprod_pn.Text = "PN:" & pntosplit
                    Lblprod_lotno.Text = "LOT NO:" & prodtravlotno
                    If assDocnoprod(inoutid, "PROD") <> proddocno Then
                        MessageBox.Show("THIS IS NOT THE PROD WORK TRAVELLER", "INVALID QR CODE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Txtbx_prodworkqrcode.Clear()
                        Txtbx_prodworkqrcode.Focus()
                    Else
                        If Trim(Txtbx_potraveller.Text) <> "" And Trim(Txtbx_pnshipdateqr.Text) <> "" Then
                            'If verifyorderPN(poprodpnnotsplit) <> 0 Then
                            '    comparetomatch(3)
                            'Else
                            '    comparetomatch(1)
                            'End If
                            comparetomatch(1)
                        End If
                    End If

                Catch ex As Exception
                    MessageBox.Show("PLEASE SCAN THE VALID QR FOR PROD WORK TRAVELLER", "INVALID QR CODE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Txtbx_prodworkqrcode.Clear()
                    Txtbx_prodworkqrcode.Focus()
                End Try

            End If
        End If
       
    End Sub
    Dim pnqrpn As String
    Private Sub Txtbx_pnshipdateqr_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Txtbx_pnshipdateqr.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim pntosplit As String
            If Trim(Txtbx_pnshipdateqr.Text) <> "" Then
                pntosplit = Txtbx_pnshipdateqr.Text
                pnprodpnnotsplit = pntosplit
                pnqrpn = pntosplit.Substring(0, 6)
                Lbl_pnqrcode.Text = "PN:" & pntosplit
                If Trim(Txtbx_prodworkqrcode.Text) = "" Then
                    Txtbx_prodworkqrcode.Focus()
                ElseIf Trim(Txtbx_prodworkqrcode.Text) <> "" And Trim(Txtbx_potraveller.Text) <> "" Then
                    'If verifyorderPN(poprodpnnotsplit) <> 0 Then
                    '    comparetomatch(3)
                    'Else
                    '    comparetomatch(1)
                    'End If
                    comparetomatch(1)
                End If
            End If
        End If
    End Sub
    Dim matchres As Integer
    Dim matchresprod As Integer
    Dim matchresqc As Integer
    Sub comparetomatch(ByVal sec As Integer)
        Select Case sec
            Case 1
                If potravlotno = prodtravlotno And getverPN(poprodpnnotsplit, workprodpnnotsplit) And getverPN(poprodpnnotsplit, pnprodpnnotsplit) Then 'potravpn = prodtravpn And potravpn = pnqrpn Then
                    Lblprod_lotno.ForeColor = Color.Black
                    lblprod_pn.ForeColor = Color.Black
                    Lbl_pnqrcode.ForeColor = Color.Black
                    Lblpo_lotno.ForeColor = Color.Black
                    lblpo_pn.ForeColor = Color.Black
                    Pcb_qmark.Visible = False
                    Pcb_match.Visible = True
                    Pcb_mismatch.Visible = False
                    'Btn_confirm.Visible = True
                    Lbl_res.Visible = False
                    Lbl_match.Visible = True
                    Lbl_mismatch.Visible = False
                    matchresprod = 1
                    Txtbx_qcpoqrcode.Focus()
                Else
                    If potravlotno <> prodtravlotno Then
                        Lblprod_lotno.ForeColor = Color.Red
                        Lblpo_lotno.ForeColor = Color.Red
                    End If
                    If getverPN(poprodpnnotsplit, workprodpnnotsplit) = False Then
                        lblpo_pn.ForeColor = Color.Red
                        lblprod_pn.ForeColor = Color.Red
                    End If
                    If getverPN(poprodpnnotsplit, pnprodpnnotsplit) = False Then
                        lblpo_pn.ForeColor = Color.Red
                        Lbl_pnqrcode.ForeColor = Color.Red
                    End If
                    Pcb_qmark.Visible = False
                    Pcb_match.Visible = False
                    Pcb_mismatch.Visible = True
                    'Btn_confirm.Visible = True
                    Lbl_res.Visible = False
                    Lbl_match.Visible = False
                    Lbl_mismatch.Visible = True
                    matchresprod = 0
                    Txtbx_qcpoqrcode.Focus()
                End If
                'If potravlotno = prodtravlotno And potravpn = prodtravpn And potravpn = pnqrpn Then
                '    Lblprod_lotno.ForeColor = Color.Black
                '    lblprod_pn.ForeColor = Color.Black
                '    Lbl_pnqrcode.ForeColor = Color.Black
                '    Lblpo_lotno.ForeColor = Color.Black
                '    lblpo_pn.ForeColor = Color.Black
                '    Pcb_qmark.Visible = False
                '    Pcb_match.Visible = True
                '    Pcb_mismatch.Visible = False
                '    'Btn_confirm.Visible = True
                '    Lbl_res.Visible = False
                '    Lbl_match.Visible = True
                '    Lbl_mismatch.Visible = False
                '    matchresprod = 1
                '    Txtbx_qcpoqrcode.Focus()
                'Else
                '    If potravlotno <> prodtravlotno Then
                '        Lblprod_lotno.ForeColor = Color.Red
                '        Lblpo_lotno.ForeColor = Color.Red
                '    End If
                '    If potravpn <> prodtravpn Then
                '        lblpo_pn.ForeColor = Color.Red
                '        lblprod_pn.ForeColor = Color.Red
                '    End If
                '    If potravpn <> pnqrpn Then
                '        lblpo_pn.ForeColor = Color.Red
                '        Lbl_pnqrcode.ForeColor = Color.Red
                '    End If
                '    Pcb_qmark.Visible = False
                '    Pcb_match.Visible = False
                '    Pcb_mismatch.Visible = True
                '    'Btn_confirm.Visible = True
                '    Lbl_res.Visible = False
                '    Lbl_match.Visible = False
                '    Lbl_mismatch.Visible = True
                '    matchresprod = 0
                '    Txtbx_qcpoqrcode.Focus()
                'End If

            Case 2
                If potravlotnoqc = qctravlotno And getverPN(poqcpnnotsplit, workqcpnnotsplit) And getverPN(poqcpnnotsplit, pnqcpnnotsplit) And inoutid = inoutidqc Then 'potravpnqc = qctravpn And potravpnqc = qcqrpn And inoutid = inoutidqc Then
                    Lblqc_poqrcodeln.ForeColor = Color.Black
                    Lblqc_pn.ForeColor = Color.Black
                    Lblqc_pnshipdateqr.ForeColor = Color.Black
                    Lblqc_lotno.ForeColor = Color.Black
                    Lblqc_poqrcodepn.ForeColor = Color.Black
                    Txtbx_potraveller.BackColor = Color.White
                    Txtbx_qcpoqrcode.BackColor = Color.White
                    Pcbqc_result.Visible = False
                    Pcbqc_match.Visible = True
                    Pcbqc_mismatch.Visible = False
                    'Btn_confirm.Visible = True
                    Lblqc_result.Visible = False
                    Lblqc_match.Visible = True
                    Lblqc_mismatch.Visible = False
                    matchresqc = 1
                Else
                    If potravlotnoqc <> qctravlotno Then
                        Lblqc_poqrcodeln.ForeColor = Color.Red
                        Lblqc_lotno.ForeColor = Color.Red
                    End If
                    If getverPN(poqcpnnotsplit, workqcpnnotsplit) = False Then
                        Lblqc_pn.ForeColor = Color.Red
                        Lblqc_poqrcodepn.ForeColor = Color.Red
                    End If
                    If getverPN(poqcpnnotsplit, pnqcpnnotsplit) = False Then
                        Lblqc_pnshipdateqr.ForeColor = Color.Red
                        Lblqc_poqrcodepn.ForeColor = Color.Red
                    End If
                    If inoutid <> inoutidqc Then
                        Txtbx_potraveller.BackColor = Color.Red
                        Txtbx_qcpoqrcode.BackColor = Color.Red
                    End If
                    Pcbqc_result.Visible = False
                    Pcbqc_match.Visible = False
                    Pcbqc_mismatch.Visible = True
                    'Btn_confirm.Visible = True
                    Lblqc_result.Visible = False
                    Lblqc_match.Visible = False
                    Lblqc_mismatch.Visible = True
                    matchresqc = 0
                End If
                'If potravlotnoqc = qctravlotno And potravpnqc = qctravpn And potravpnqc = qcqrpn And inoutid = inoutidqc Then
                '    Lblqc_poqrcodeln.ForeColor = Color.Black
                '    Lblqc_pn.ForeColor = Color.Black
                '    Lblqc_pnshipdateqr.ForeColor = Color.Black
                '    Lblqc_lotno.ForeColor = Color.Black
                '    Lblqc_poqrcodepn.ForeColor = Color.Black
                '    Txtbx_potraveller.BackColor = Color.White
                '    Txtbx_qcpoqrcode.BackColor = Color.White
                '    Pcbqc_result.Visible = False
                '    Pcbqc_match.Visible = True
                '    Pcbqc_mismatch.Visible = False
                '    'Btn_confirm.Visible = True
                '    Lblqc_result.Visible = False
                '    Lblqc_match.Visible = True
                '    Lblqc_mismatch.Visible = False
                '    matchresqc = 1
                'Else
                '    If potravlotnoqc <> qctravlotno Then
                '        Lblqc_poqrcodeln.ForeColor = Color.Red
                '        Lblqc_lotno.ForeColor = Color.Red
                '    End If
                '    If potravpnqc <> qctravpn Then
                '        Lblqc_pn.ForeColor = Color.Red
                '        Lblqc_poqrcodepn.ForeColor = Color.Red
                '    End If
                '    If potravpnqc <> qcqrpn Then
                '        Lblqc_pnshipdateqr.ForeColor = Color.Red
                '        Lblqc_poqrcodepn.ForeColor = Color.Red
                '    End If
                '    If inoutid <> inoutidqc Then
                '        Txtbx_potraveller.BackColor = Color.Red
                '        Txtbx_qcpoqrcode.BackColor = Color.Red
                '    End If
                '    Pcbqc_result.Visible = False
                '    Pcbqc_match.Visible = False
                '    Pcbqc_mismatch.Visible = True
                '    'Btn_confirm.Visible = True
                '    Lblqc_result.Visible = False
                '    Lblqc_match.Visible = False
                '    Lblqc_mismatch.Visible = True
                '    matchresqc = 0
                'End If
                matchingResult(matchresprod, matchresqc)

                'Case 3 'order pn is def in applied pn prod area
                '    If potravlotno = prodtravlotno And comparePN(If(verifyorderPN(poprodpnnotsplit) = 1, poprodpnnotsplit.Substring(0, 9), poprodpnnotsplit.Substring(0, 10)), workprodpnnotsplit) And _
                '        comparePN(If(verifyorderPN(poprodpnnotsplit) = 1, poprodpnnotsplit.Substring(0, 9), poprodpnnotsplit.Substring(0, 10)), pnprodpnnotsplit) Then
                '        Lblprod_lotno.ForeColor = Color.Black
                '        lblprod_pn.ForeColor = Color.Black
                '        Lbl_pnqrcode.ForeColor = Color.Black
                '        Lblpo_lotno.ForeColor = Color.Black
                '        lblpo_pn.ForeColor = Color.Black
                '        Pcb_qmark.Visible = False
                '        Pcb_match.Visible = True
                '        Pcb_mismatch.Visible = False
                '        'Btn_confirm.Visible = True
                '        Lbl_res.Visible = False
                '        Lbl_match.Visible = True
                '        Lbl_mismatch.Visible = False
                '        matchresprod = 1
                '        Txtbx_qcpoqrcode.Focus()
                '    Else
                '        If potravlotno <> prodtravlotno Then
                '            Lblprod_lotno.ForeColor = Color.Red
                '            Lblpo_lotno.ForeColor = Color.Red
                '        End If
                '        If comparePN(If(verifyorderPN(poprodpnnotsplit) = 1, poprodpnnotsplit.Substring(0, 9), poprodpnnotsplit.Substring(0, 10)), workprodpnnotsplit) = False Then
                '            lblpo_pn.ForeColor = Color.Red
                '            lblprod_pn.ForeColor = Color.Red
                '        End If
                '        If comparePN(If(verifyorderPN(poprodpnnotsplit) = 1, poprodpnnotsplit.Substring(0, 9), poprodpnnotsplit.Substring(0, 10)), pnprodpnnotsplit) = False Then
                '            lblpo_pn.ForeColor = Color.Red
                '            Lbl_pnqrcode.ForeColor = Color.Red
                '        End If
                '        Pcb_qmark.Visible = False
                '        Pcb_match.Visible = False
                '        Pcb_mismatch.Visible = True
                '        'Btn_confirm.Visible = True
                '        Lbl_res.Visible = False
                '        Lbl_match.Visible = False
                '        Lbl_mismatch.Visible = True
                '        matchresprod = 0
                '        Txtbx_qcpoqrcode.Focus()
                '    End If
                'Case 4 'order pn is def in applied pn prod area
                '    If potravlotnoqc = qctravlotno And comparePN(If(verifyorderPN(poqcpnnotsplit) = 1, poqcpnnotsplit.Substring(0, 9), poqcpnnotsplit.Substring(0, 10)), workqcpnnotsplit) And _
                '        comparePN(If(verifyorderPN(poqcpnnotsplit) = 1, poqcpnnotsplit.Substring(0, 9), poqcpnnotsplit.Substring(0, 10)), pnqcpnnotsplit) And inoutid = inoutidqc Then
                '        Lblqc_poqrcodeln.ForeColor = Color.Black
                '        Lblqc_pn.ForeColor = Color.Black
                '        Lblqc_pnshipdateqr.ForeColor = Color.Black
                '        Lblqc_lotno.ForeColor = Color.Black
                '        Lblqc_poqrcodepn.ForeColor = Color.Black
                '        Txtbx_potraveller.BackColor = Color.White
                '        Txtbx_qcpoqrcode.BackColor = Color.White
                '        Pcbqc_result.Visible = False
                '        Pcbqc_match.Visible = True
                '        Pcbqc_mismatch.Visible = False
                '        'Btn_confirm.Visible = True
                '        Lblqc_result.Visible = False
                '        Lblqc_match.Visible = True
                '        Lblqc_mismatch.Visible = False
                '        matchresqc = 1
                '    Else
                '        If potravlotnoqc <> qctravlotno Then
                '            Lblqc_poqrcodeln.ForeColor = Color.Red
                '            Lblqc_lotno.ForeColor = Color.Red
                '        End If
                '        If comparePN(If(verifyorderPN(poqcpnnotsplit) = 1, poqcpnnotsplit.Substring(0, 9), poqcpnnotsplit.Substring(0, 10)), workqcpnnotsplit) Then
                '            Lblqc_pn.ForeColor = Color.Red
                '            Lblqc_poqrcodepn.ForeColor = Color.Red
                '        End If
                '        If comparePN(If(verifyorderPN(poqcpnnotsplit) = 1, poqcpnnotsplit.Substring(0, 9), poqcpnnotsplit.Substring(0, 10)), pnqcpnnotsplit) Then
                '            Lblqc_pnshipdateqr.ForeColor = Color.Red
                '            Lblqc_poqrcodepn.ForeColor = Color.Red
                '        End If
                '        If inoutid <> inoutidqc Then
                '            Txtbx_potraveller.BackColor = Color.Red
                '            Txtbx_qcpoqrcode.BackColor = Color.Red
                '        End If
                '        Pcbqc_result.Visible = False
                '        Pcbqc_match.Visible = False
                '        Pcbqc_mismatch.Visible = True
                '        'Btn_confirm.Visible = True
                '        Lblqc_result.Visible = False
                '        Lblqc_match.Visible = False
                '        Lblqc_mismatch.Visible = True
                '        matchresqc = 0
                '    End If
                '    matchingResult(matchresprod, matchresqc)
        End Select

    End Sub
    Sub matchingResult(ByVal resprod As Integer, ByVal resqc As Integer)
        If resprod = resqc Then
            matchres = 1
            Btn_confirm.Enabled = True
        Else
            matchres = 0
            Btn_confirm.Enabled = True
        End If
    End Sub
    Sub saveMatchscan()
        Dim query As String
        query = "UPDATE cci_inout_assign SET comparedscan=@comparedscan,match_res=@matchres WHERE inout_assign_id='" & inoutid & "'"
        Dim cmd As New MySqlCommand(query, con_cci)
        cmd.Parameters.AddWithValue("@comparedscan", getScantime(inoutid) + 1)
        cmd.Parameters.AddWithValue("@matchres", matchres)
        con_cci.Open()
        cmd.ExecuteNonQuery()
        con_cci.Close()
    End Sub

    Dim potravlotnoqc As String
    Dim potravpnqc As String
    'Dim inoutidqc As String
    Private Sub Txtbx_qcpoqrcode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Txtbx_qcpoqrcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim splitpoqr() As String
            Dim splitid() As String
            Dim pntosplit As String
            If Trim(Txtbx_qcpoqrcode.Text) <> "" Then
                splitpoqr = (Txtbx_qcpoqrcode.Text).Split("[")
                Try
                    potravlotnoqc = splitpoqr(0)
                    splitid = (splitpoqr(1)).Split("]")
                    pntosplit = getPN(splitid(0))
                    poqcpnnotsplit = pntosplit
                    potravpnqc = pntosplit.Substring(0, 6)
                    inoutidqc = splitid(1)
                    Lblqc_poqrcodeln.Text = "LOT NO:" & potravlotnoqc
                    Lblqc_poqrcodepn.Text = "PN:" & pntosplit
                    If Trim(Txtbx_qcpnshipdate.Text) = "" Then
                        Txtbx_qcpnshipdate.Focus()
                    ElseIf Trim(Txtbx_qcworktraveller.Text) <> "" And Txtbx_qcpnshipdate.Text <> "" Then
                        'If verifyorderPN(poqcpnnotsplit) <> 0 Then
                        '    comparetomatch(4)
                        'Else
                        '    comparetomatch(2)
                        'End If
                        comparetomatch(2)
                    End If
                Catch ex As Exception
                    MessageBox.Show("PLEASE SCAN THE VALID QR FOR PO TRAVELLER", "INVALID QR CODE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Txtbx_qcpoqrcode.Clear()
                End Try

            End If
        End If
    End Sub
    Dim qcqrpn As String
    Private Sub Txtbx_qcpnshipdate_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Txtbx_qcpnshipdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim pntosplit As String
            If Trim(Txtbx_qcpnshipdate.Text) <> "" Then
                pntosplit = Txtbx_qcpnshipdate.Text
                pnqcpnnotsplit = pntosplit
                qcqrpn = pntosplit.Substring(0, 6)
                Lblqc_pnshipdateqr.Text = "PN:" & pntosplit
                If Trim(Txtbx_qcworktraveller.Text) = "" Then
                    Txtbx_qcworktraveller.Focus()
                ElseIf Trim(Txtbx_qcworktraveller.Text) <> "" And Trim(Txtbx_qcpoqrcode.Text) <> "" Then
                    'If verifyorderPN(poqcpnnotsplit) <> 0 Then
                    '    comparetomatch(4)
                    'Else
                    '    comparetomatch(2)
                    'End If
                    comparetomatch(2)
                End If
            End If
        End If
    End Sub
    Dim shipdate As String
    Sub getAppliedLot()
        shipdate = If(Not (Trim(getShipdate(inoutid)) = ""), CDate(getShipdate(inoutid)).ToString("yyyy-MM-dd"), "")
        If Bgw1.IsBusy Then
        Else
            Pcbloading.Visible = True
            Dgv.Rows.Clear()
            Bgw1.RunWorkerAsync()
        End If
    End Sub

    Private Sub Bgw1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Bgw1.DoWork
        Dim dt As New DataTable
        Dim data As New MySqlDataAdapter("SELECT po_no,lotno,shipdate,match_res,comparedscan FROM cci_inout_assign WHERE po_no='" & pono & "' AND shipdate = '" & shipdate & "' AND deleted = 0", con_cci)
        data.Fill(dt)
        data.Dispose()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim rows As New DataGridViewRow
                rows.Cells.Add(New DataGridViewTextBoxCell With {.Value = dt.Rows(i)("lotno")})
                rows.Cells.Add(New DataGridViewTextBoxCell With {.Value = CDate(dt.Rows(i)("shipdate")).ToString("yyyy-MM-dd")})
                rows.Cells.Add(New DataGridViewTextBoxCell With {.Value = dt.Rows(i)("po_no")})
                rows.Cells.Add(New DataGridViewTextBoxCell With {.Value = If((dt.Rows(i)("match_res") = 1), "Yes", If((dt.Rows(i)("comparedscan") > 0 And dt.Rows(i)("match_res") = 0), "No", ""))})
                rows.Cells.Add(New DataGridViewTextBoxCell With {.Value = dt.Rows(i)("comparedscan")})
                'Dgv.Rows(i).DefaultCellStyle.BackColor = Color.Aquamarine
                'Dgv.Rows(0).DefaultCellStyle.BackColor = Color.DarkOrange

                doRowAdd(Dgv, rows)
            Next
        End If
    End Sub

    Private Sub Bgw1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Bgw1.RunWorkerCompleted
        For i As Integer = 0 To Dgv.Rows.Count - 1
            If Dgv.Rows(i).Cells(3).Value = "Yes" Then
                Dgv.Rows(i).DefaultCellStyle.BackColor = Color.DarkOrange
                Dgv.Rows(i).DefaultCellStyle.ForeColor = Color.White
            Else
                Dgv.Rows(i).DefaultCellStyle.BackColor = Color.White
                Dgv.Rows(i).DefaultCellStyle.ForeColor = Color.Black
            End If
        Next
        Pcbloading.Visible = False
    End Sub
    Function verifyorderPN(ByVal orderPN As String) As Integer
        If orderPN.Length >= 9 Then
            Return If(orderPN.Substring(0, 9) = "AMS101K2P" Or orderPN.Substring(0, 9) = "BMS120K2B" Or orderPN.Substring(0, 9) = "BMS120M2B", 1, If(orderPN.Length > 10, If(orderPN.Substring(0, 10) = "BMS12.0M3F", 2, 0), 0))
        Else
            Return 0
        End If
    End Function

    Function comparePN(ByVal orderPN As String, ByVal appliedPN As String) As Boolean
        Dim result As Boolean = False
        'MsgBox(orderPN & ">>applied:" & appliedPN)
        Select Case orderPN
            Case "AMS101K2P"
                result = appliedPN.Substring(0, 11) = "AMS100.0K3A" Or orderPN.Substring(0, 6) = appliedPN.Substring(0, 6)
                'MsgBox(result)
            Case "BMS120K2B"
                result = appliedPN.Substring(0, 10) = "BMS12.0K3F" Or orderPN.Substring(0, 6) = appliedPN.Substring(0, 6)
            Case "BMS120M2B"
                result = appliedPN.Substring(0, 9) = "BMS120K2B" Or appliedPN.Substring(0, 10) = "BMS12.0K3F" Or appliedPN.Substring(0, 10) = "BMS12.0M3F" Or orderPN.Substring(0, 6) = appliedPN.Substring(0, 6)
            Case "BMS12.0M3F"
                result = appliedPN.Substring(0, 10) = "BMS12.0K3F" Or appliedPN.Substring(0, 9) = "BMS120K2B" Or appliedPN.Substring(0, 9) = "BMS120M2B" Or orderPN.Substring(0, 6) = appliedPN.Substring(0, 6)
            Case "TGB0250130"
                result = appliedPN.Substring(0, 7) = "TGB1005" Or orderPN.Substring(0, 6) = appliedPN.Substring(0, 6)
                'Case Else
                '    result = orderPN = appliedPN
        End Select
        Return result
    End Function

    Function getverPN(ByVal orderpn As String, ByVal appliedpn As String) As Boolean 'comparing labels
        Dim result As Boolean = False
        Dim dt As New DataTable
        Dim data As New MySqlDataAdapter("SELECT order_pn FROM cci_order_pn", con_cci)
        data.Fill(dt)
        data.Dispose()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If result = True Then
                    Exit For
                End If
                If orderpn.Length >= dt.Rows(i)("order_pn").Length Then
                    If dt.Rows(i)("order_pn") = orderpn.Substring(0, dt.Rows(i)("order_pn").Length) Then
                        Dim dt2 As New DataTable
                        Dim data2 As New MySqlDataAdapter("SELECT orderpn_appliedpn FROM cci_order_pn_sub INNER JOIN cci_order_pn WHERE order_pn = '" & orderpn.Substring(0, dt.Rows(i)("order_pn").Length) & "'", con_cci)
                        data2.Fill(dt2)
                        data2.Dispose()
                        If dt2.Rows.Count > 0 Then
                            For i2 As Integer = 0 To dt2.Rows.Count - 1
                                If appliedpn.Length >= dt2.Rows(i2)("orderpn_appliedpn").Length Then
                                    If dt2.Rows(i2)("orderpn_appliedpn") = appliedpn.Substring(0, dt2.Rows(i2)("orderpn_appliedpn").Length) Then
                                        result = True
                                        Exit For
                                    Else
                                        If orderpn.Substring(0, 6) = appliedpn.Substring(0, 6) Then
                                            result = True
                                            Exit For
                                        Else
                                            result = False
                                        End If
                                    End If
                                Else
                                    If orderpn.Substring(0, 6) = appliedpn.Substring(0, 6) Then
                                        result = True
                                        Exit For
                                    Else
                                        result = False
                                    End If
                                End If
                            Next
                        Else
                            If orderpn.Substring(0, 6) = appliedpn.Substring(0, 6) Then
                                result = True
                                Exit For
                            Else
                                result = False
                            End If
                        End If

                    Else
                        If orderpn.Substring(0, 6) = appliedpn.Substring(0, 6) Then
                            result = True
                            Exit For
                        Else
                            result = False
                        End If
                    End If
                Else
                    If orderpn.Substring(0, 6) = appliedpn.Substring(0, 6) Then
                        result = True
                        Exit For
                    Else
                        result = False
                    End If
                End If

            Next
        Else
            If orderpn.Substring(0, 6) = appliedpn.Substring(0, 6) Then
                result = True
            Else
                result = False
            End If
        End If
        Return result
    End Function

End Class