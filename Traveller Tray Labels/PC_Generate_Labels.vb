Imports Excel = Microsoft.Office.Interop.Excel
Imports MessagingToolkit.QRCode.Codec
Imports MySql.Data.MySqlClient
Imports System.Data.OleDb

Public Class PC_Generate_Labels

    '---------------------------------------------------------------------------------------------------------------------------------------
    'CLASS VARIABLES
    '---------------------------------------------------------------------------------------------------------------------------------------

    Dim lot_number_src As AutoCompleteStringCollection
    Dim rows1 As List(Of DataGridViewRow)
    Dim metallized_ln_reference As String
    Dim flag As New BitArray(60, True)
    Dim pos_flag As New BitArray(60, True)
    
    '---------------------------------------------------------------------------------------------------------------------------------------
    'CONTROL EVENTS
    '---------------------------------------------------------------------------------------------------------------------------------------

    Private Sub PC_Generate_Labels_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetDoubleBuffering(Container_Pnl, True)
        Lbl_Pnl_Visible_False()
        Traveller_RdBtn.Checked = True
        Auto_RdBtn.Checked = True
    End Sub

    Private Sub Ln_Search_PicBx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ln_Search_PicBx.Click
        If Trim(Lot_Number_TxtBx.Text) = "" Then
            If Traveller_RdBtn.Checked = True Then
                MessageBox.Show("Please Input a PO Number On the Textbox", "NO PO", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Please Input a LOT Number On the Textbox", "NO LOT Number", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            If Traveller_RdBtn.Checked = True Then
                If Trim(Cmb_shipdate.Text) = "" Then
                    MessageBox.Show("Please Select a shipdate", "No Shipdate", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    metallized_ln_reference = Trim(Lot_Number_TxtBx.Text)
                    BackgroundWorker2.RunWorkerAsync()
                End If
            Else
                metallized_ln_reference = Trim(Lot_Number_TxtBx.Text)
                BackgroundWorker2.RunWorkerAsync()
            End If
        End If
    End Sub
    Sub getShipdate()
        Dim dt1 As New DataTable
        Dim data1 As New MySqlDataAdapter("SELECT hct_planshipdate FROM hct_info_cap_order_sub INNER JOIN hct_info_cap_order ON hct_info_cap_order.hct_id_cap_order = hct_info_cap_order_sub.hct_cap_order_id " & _
                                          "WHERE hct_po_no = '" & Lot_Number_TxtBx.Text & "' ORDER BY hct_planshipdate DESC", con_gl1_bgw2)
        data1.Fill(dt1)
        data1.Dispose()
        If dt1.Rows.Count > 0 Then

            For i As Integer = 0 To dt1.Rows.Count - 1

                Cmb_shipdate.Items.Add(dt1.Rows(i).Item(0))
            Next

        End If
    End Sub

    Private Sub Auto_RdBtn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Auto_RdBtn.CheckedChanged

        If Auto_RdBtn.Checked = True Then
            Pos_B_PicBx_True()
            Pos_PicBx_False()
            pos_flag.SetAll(True)
        End If

    End Sub

    Private Sub Manual_RdBtn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Manual_RdBtn.CheckedChanged
        If Manual_RdBtn.Checked = True Then
            Pos_B_PicBx_False()
            Pos_PicBx_True()
            pos_flag.SetAll(False)
        End If
    End Sub

    Private Sub Gen_Btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Gen_Btn.Click

        Dim QR_Generator As New QRCodeEncoder
        QR_Generator.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        QR_Generator.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
        QR_Generator.QRCodeVersion = 5
        QR_Generator.QRCodeScale = 2

        For c = 0 To DataGridView1.Rows.Count - 1

            If DataGridView1.Rows(c).Selected = True Then

                Dim add_lot_number_flag As Boolean = True
                Dim metallized_pn As String = DataGridView1.Rows(c).Cells(0).Value.ToString()
                Dim metallized_ln As String = DataGridView1.Rows(c).Cells(1).Value.ToString()
                Dim reference_id As Integer = DataGridView1.Rows(c).Cells(2).Value
                Dim base_metallized_ln As String = ""
                Dim metallized_wafer_number As Integer = 0

                If Traveller_RdBtn.Checked = True Then
                    'base_metallized_ln = Get_String_Before(metallized_ln, "-")
                    'metallized_wafer_number = Convert.ToInt32(Get_String_After(metallized_ln, "-"))
                End If

                'Duplicate filter
                If DataGridView2.Rows.Count > 0 Then

                    For j As Integer = 0 To DataGridView2.Rows.Count - 1

                        If DataGridView2.Rows(j).Cells(2).Value = reference_id Then
                            add_lot_number_flag = False
                            Exit For
                        End If

                    Next

                End If

                
                If add_lot_number_flag = True Then

                    If DataGridView2.Rows.Count < 60 Then

                        Dim qr_image As Image = QR_Generator.Encode(metallized_ln & "[" & metallized_pn & "]" & reference_id)

                        'Add to label view

                        For l As Integer = 1 To 60

                            Dim control() As Control

                            If pos_flag(l - 1) = True And flag(l - 1) = True Then

                                DataGridView2.Rows.Add(1)
                                DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(0).Value = metallized_pn
                                DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(1).Value = metallized_ln
                                DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(2).Value = reference_id

                                control = Me.Controls.Find("Lbl_PN_Lbl" & l, True)
                                Dim Lbl_PN_Lbl As Label = DirectCast(control(0), Label)
                                Lbl_PN_Lbl.Text = metallized_pn

                                control = Me.Controls.Find("Lbl_LN_Lbl" & l, True)
                                Dim Lbl_LN_Lbl As Label = DirectCast(control(0), Label)
                                Lbl_LN_Lbl.Text = metallized_ln

                                DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(3).Value = l - 1

                                control = Me.Controls.Find("Lbl_QR_PicBx" & l, True)
                                Dim Lbl_QR_PicBx As PictureBox = DirectCast(control(0), PictureBox)
                                Lbl_QR_PicBx.Image = qr_image

                                control = Me.Controls.Find("Pos_Pnl" & l, True)
                                Dim Pos_Pnl As Panel = DirectCast(control(0), Panel)
                                Pos_Pnl.Visible = False

                                control = Me.Controls.Find("Lbl_Pnl" & l, True)
                                Dim Lbl_Pnl As Panel = DirectCast(control(0), Panel)
                                Lbl_Pnl.Visible = True

                                flag(l - 1) = False

                                Exit For

                            End If

                        Next

                    End If

                End If

            End If

        Next

    End Sub
    Dim sdate As String
    Private Sub Ex_Btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ex_Btn.Click
        If BackgroundWorker3.IsBusy Then
        Else
            sdate = CDate(Cmb_shipdate.Text).ToString("MM/dd/yyyy")
            Ex_Btn.Enabled = False
            BackgroundWorker3.RunWorkerAsync()
        End If

    End Sub

    Private Sub Res_Btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Res_Btn.Click

        If Auto_RdBtn.Checked = True Then
            Pos_B_PicBx_True()
            Pos_PicBx_False()
            pos_flag.SetAll(True)
        Else
            Pos_B_PicBx_False()
            Pos_PicBx_True()
            pos_flag.SetAll(False)
        End If

        Reset()

    End Sub

    Private Sub Pos_PicBx1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx1.Click
        Pos_PicBx_Click(1)
    End Sub

    Private Sub Pos_PicBx2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx2.Click
        Pos_PicBx_Click(2)
    End Sub

    Private Sub Pos_PicBx3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx3.Click
        Pos_PicBx_Click(3)
    End Sub

    Private Sub Pos_PicBx4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx4.Click
        Pos_PicBx_Click(4)
    End Sub

    Private Sub Pos_PicBx5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx5.Click
        Pos_PicBx_Click(5)
    End Sub

    Private Sub Pos_PicBx6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx6.Click
        Pos_PicBx_Click(6)
    End Sub

    Private Sub Pos_PicBx7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx7.Click
        Pos_PicBx_Click(7)
    End Sub

    Private Sub Pos_PicBx8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx8.Click
        Pos_PicBx_Click(8)
    End Sub

    Private Sub Pos_PicBx9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx9.Click
        Pos_PicBx_Click(9)
    End Sub

    Private Sub Pos_PicBx10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx10.Click
        Pos_PicBx_Click(10)
    End Sub

    Private Sub Pos_PicBx11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx11.Click
        Pos_PicBx_Click(11)
    End Sub

    Private Sub Pos_PicBx12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx12.Click
        Pos_PicBx_Click(12)
    End Sub

    Private Sub Pos_PicBx13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx13.Click
        Pos_PicBx_Click(13)
    End Sub

    Private Sub Pos_PicBx14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx14.Click
        Pos_PicBx_Click(14)
    End Sub

    Private Sub Pos_PicBx15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx15.Click
        Pos_PicBx_Click(15)
    End Sub

    Private Sub Pos_PicBx16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx16.Click
        Pos_PicBx_Click(16)
    End Sub

    Private Sub Pos_PicBx17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx17.Click
        Pos_PicBx_Click(17)
    End Sub

    Private Sub Pos_PicBx18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx18.Click
        Pos_PicBx_Click(18)
    End Sub

    Private Sub Pos_PicBx19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx19.Click
        Pos_PicBx_Click(19)
    End Sub

    Private Sub Pos_PicBx20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx20.Click
        Pos_PicBx_Click(20)
    End Sub

    Private Sub Pos_PicBx21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx21.Click
        Pos_PicBx_Click(21)
    End Sub

    Private Sub Pos_PicBx22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx22.Click
        Pos_PicBx_Click(22)
    End Sub

    Private Sub Pos_PicBx23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx23.Click
        Pos_PicBx_Click(23)
    End Sub

    Private Sub Pos_PicBx24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx24.Click
        Pos_PicBx_Click(24)
    End Sub

    Private Sub Pos_PicBx25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx25.Click
        Pos_PicBx_Click(25)
    End Sub

    Private Sub Pos_PicBx26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx26.Click
        Pos_PicBx_Click(26)
    End Sub

    Private Sub Pos_PicBx27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx27.Click
        Pos_PicBx_Click(27)
    End Sub

    Private Sub Pos_PicBx28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx28.Click
        Pos_PicBx_Click(28)
    End Sub

    Private Sub Pos_PicBx29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx29.Click
        Pos_PicBx_Click(29)
    End Sub

    Private Sub Pos_PicBx30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx30.Click
        Pos_PicBx_Click(30)
    End Sub

    Private Sub Pos_PicBx31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx31.Click
        Pos_PicBx_Click(31)
    End Sub

    Private Sub Pos_PicBx32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx32.Click
        Pos_PicBx_Click(32)
    End Sub

    Private Sub Pos_PicBx33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx33.Click
        Pos_PicBx_Click(33)
    End Sub

    Private Sub Pos_PicBx34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx34.Click
        Pos_PicBx_Click(34)
    End Sub

    Private Sub Pos_PicBx35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx35.Click
        Pos_PicBx_Click(35)
    End Sub

    Private Sub Pos_PicBx36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx36.Click
        Pos_PicBx_Click(36)
    End Sub

    Private Sub Pos_PicBx37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx37.Click
        Pos_PicBx_Click(37)
    End Sub

    Private Sub Pos_PicBx38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx38.Click
        Pos_PicBx_Click(38)
    End Sub

    Private Sub Pos_PicBx39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx39.Click
        Pos_PicBx_Click(39)
    End Sub

    Private Sub Pos_PicBx40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx40.Click
        Pos_PicBx_Click(40)
    End Sub

    Private Sub Pos_PicBx41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx41.Click
        Pos_PicBx_Click(41)
    End Sub

    Private Sub Pos_PicBx42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx42.Click
        Pos_PicBx_Click(42)
    End Sub

    Private Sub Pos_PicBx43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx43.Click
        Pos_PicBx_Click(43)
    End Sub

    Private Sub Pos_PicBx44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx44.Click
        Pos_PicBx_Click(44)
    End Sub

    Private Sub Pos_PicBx45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx45.Click
        Pos_PicBx_Click(45)
    End Sub

    Private Sub Pos_PicBx46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx46.Click
        Pos_PicBx_Click(46)
    End Sub

    Private Sub Pos_PicBx47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx47.Click
        Pos_PicBx_Click(47)
    End Sub

    Private Sub Pos_PicBx48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx48.Click
        Pos_PicBx_Click(48)
    End Sub

    Private Sub Pos_PicBx49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx49.Click
        Pos_PicBx_Click(49)
    End Sub

    Private Sub Pos_PicBx50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx50.Click
        Pos_PicBx_Click(50)
    End Sub

    Private Sub Pos_PicBx51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx51.Click
        Pos_PicBx_Click(51)
    End Sub

    Private Sub Pos_PicBx52_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx52.Click
        Pos_PicBx_Click(52)
    End Sub

    Private Sub Pos_PicBx53_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx53.Click
        Pos_PicBx_Click(53)
    End Sub

    Private Sub Pos_PicBx54_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx54.Click
        Pos_PicBx_Click(54)
    End Sub

    Private Sub Pos_PicBx55_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx55.Click
        Pos_PicBx_Click(55)
    End Sub

    Private Sub Pos_PicBx56_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx56.Click
        Pos_PicBx_Click(56)
    End Sub

    Private Sub Pos_PicBx57_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx57.Click
        Pos_PicBx_Click(57)
    End Sub

    Private Sub Pos_PicBx58_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx58.Click
        Pos_PicBx_Click(58)
    End Sub

    Private Sub Pos_PicBx59_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx59.Click
        Pos_PicBx_Click(59)
    End Sub

    Private Sub Pos_PicBx60_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_PicBx60.Click
        Pos_PicBx_Click(60)
    End Sub

    Private Sub Pos_B_PicBx1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx1.Click
        Pos_B_PicBx_Click(1)
    End Sub

    Private Sub Pos_B_PicBx2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx2.Click
        Pos_B_PicBx_Click(2)
    End Sub

    Private Sub Pos_B_PicBx3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx3.Click
        Pos_B_PicBx_Click(3)
    End Sub

    Private Sub Pos_B_PicBx4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx4.Click
        Pos_B_PicBx_Click(4)
    End Sub

    Private Sub Pos_B_PicBx5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx5.Click
        Pos_B_PicBx_Click(5)
    End Sub

    Private Sub Pos_B_PicBx6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx6.Click
        Pos_B_PicBx_Click(6)
    End Sub

    Private Sub Pos_B_PicBx7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx7.Click
        Pos_B_PicBx_Click(7)
    End Sub

    Private Sub Pos_B_PicBx8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx8.Click
        Pos_B_PicBx_Click(8)
    End Sub

    Private Sub Pos_B_PicBx9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx9.Click
        Pos_B_PicBx_Click(9)
    End Sub

    Private Sub Pos_B_PicBx10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx10.Click
        Pos_B_PicBx_Click(10)
    End Sub

    Private Sub Pos_B_PicBx11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx11.Click
        Pos_B_PicBx_Click(11)
    End Sub

    Private Sub Pos_B_PicBx12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx12.Click
        Pos_B_PicBx_Click(12)
    End Sub

    Private Sub Pos_B_PicBx13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx13.Click
        Pos_B_PicBx_Click(13)
    End Sub

    Private Sub Pos_B_PicBx14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx14.Click
        Pos_B_PicBx_Click(14)
    End Sub

    Private Sub Pos_B_PicBx15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx15.Click
        Pos_B_PicBx_Click(15)
    End Sub

    Private Sub Pos_B_PicBx16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx16.Click
        Pos_B_PicBx_Click(16)
    End Sub

    Private Sub Pos_B_PicBx17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx17.Click
        Pos_B_PicBx_Click(17)
    End Sub

    Private Sub Pos_B_PicBx18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx18.Click
        Pos_B_PicBx_Click(18)
    End Sub

    Private Sub Pos_B_PicBx19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx19.Click
        Pos_B_PicBx_Click(19)
    End Sub

    Private Sub Pos_B_PicBx20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx20.Click
        Pos_B_PicBx_Click(20)
    End Sub

    Private Sub Pos_B_PicBx21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx21.Click
        Pos_B_PicBx_Click(21)
    End Sub

    Private Sub Pos_B_PicBx22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx22.Click
        Pos_B_PicBx_Click(22)
    End Sub

    Private Sub Pos_B_PicBx23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx23.Click
        Pos_B_PicBx_Click(23)
    End Sub

    Private Sub Pos_B_PicBx24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx24.Click
        Pos_B_PicBx_Click(24)
    End Sub

    Private Sub Pos_B_PicBx25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx25.Click
        Pos_B_PicBx_Click(25)
    End Sub

    Private Sub Pos_B_PicBx26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx26.Click
        Pos_B_PicBx_Click(26)
    End Sub

    Private Sub Pos_B_PicBx27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx27.Click
        Pos_B_PicBx_Click(27)
    End Sub

    Private Sub Pos_B_PicBx28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx28.Click
        Pos_B_PicBx_Click(28)
    End Sub

    Private Sub Pos_B_PicBx29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx29.Click
        Pos_B_PicBx_Click(29)
    End Sub

    Private Sub Pos_B_PicBx30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx30.Click
        Pos_B_PicBx_Click(30)
    End Sub

    Private Sub Pos_B_PicBx31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx31.Click
        Pos_B_PicBx_Click(31)
    End Sub

    Private Sub Pos_B_PicBx32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx32.Click
        Pos_B_PicBx_Click(32)
    End Sub

    Private Sub Pos_B_PicBx33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx33.Click
        Pos_B_PicBx_Click(33)
    End Sub

    Private Sub Pos_B_PicBx34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx34.Click
        Pos_B_PicBx_Click(34)
    End Sub

    Private Sub Pos_B_PicBx35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx35.Click
        Pos_B_PicBx_Click(35)
    End Sub

    Private Sub Pos_B_PicBx36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx36.Click
        Pos_B_PicBx_Click(36)
    End Sub

    Private Sub Pos_B_PicBx37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx37.Click
        Pos_B_PicBx_Click(37)
    End Sub

    Private Sub Pos_B_PicBx38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx38.Click
        Pos_B_PicBx_Click(38)
    End Sub

    Private Sub Pos_B_PicBx39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx39.Click
        Pos_B_PicBx_Click(39)
    End Sub

    Private Sub Pos_B_PicBx40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx40.Click
        Pos_B_PicBx_Click(40)
    End Sub

    Private Sub Pos_B_PicBx41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx41.Click
        Pos_B_PicBx_Click(41)
    End Sub

    Private Sub Pos_B_PicBx42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx42.Click
        Pos_B_PicBx_Click(42)
    End Sub

    Private Sub Pos_B_PicBx43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx43.Click
        Pos_B_PicBx_Click(43)
    End Sub

    Private Sub Pos_B_PicBx44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx44.Click
        Pos_B_PicBx_Click(44)
    End Sub

    Private Sub Pos_B_PicBx45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx45.Click
        Pos_B_PicBx_Click(45)
    End Sub

    Private Sub Pos_B_PicBx46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx46.Click
        Pos_B_PicBx_Click(46)
    End Sub

    Private Sub Pos_B_PicBx47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx47.Click
        Pos_B_PicBx_Click(47)
    End Sub

    Private Sub Pos_B_PicBx48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx48.Click
        Pos_B_PicBx_Click(48)
    End Sub

    Private Sub Pos_B_PicBx49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx49.Click
        Pos_B_PicBx_Click(49)
    End Sub

    Private Sub Pos_B_PicBx50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx50.Click
        Pos_B_PicBx_Click(50)
    End Sub

    Private Sub Pos_B_PicBx51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx51.Click
        Pos_B_PicBx_Click(51)
    End Sub

    Private Sub Pos_B_PicBx52_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx52.Click
        Pos_B_PicBx_Click(52)
    End Sub

    Private Sub Pos_B_PicBx53_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx53.Click
        Pos_B_PicBx_Click(53)
    End Sub

    Private Sub Pos_B_PicBx54_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx54.Click
        Pos_B_PicBx_Click(54)
    End Sub

    Private Sub Pos_B_PicBx55_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx55.Click
        Pos_B_PicBx_Click(55)
    End Sub

    Private Sub Pos_B_PicBx56_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx56.Click
        Pos_B_PicBx_Click(56)
    End Sub

    Private Sub Pos_B_PicBx57_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx57.Click
        Pos_B_PicBx_Click(57)
    End Sub

    Private Sub Pos_B_PicBx58_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx58.Click
        Pos_B_PicBx_Click(58)
    End Sub

    Private Sub Pos_B_PicBx59_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx59.Click
        Pos_B_PicBx_Click(59)
    End Sub

    Private Sub Pos_B_PicBx60_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_B_PicBx60.Click
        Pos_B_PicBx_Click(60)
    End Sub

    Private Sub Pos_Lbl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl1.Click
        Pos_Lbl_Click(1)
    End Sub

    Private Sub Pos_Lbl2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl2.Click
        Pos_Lbl_Click(2)
    End Sub

    Private Sub Pos_Lbl3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl3.Click
        Pos_Lbl_Click(3)
    End Sub

    Private Sub Pos_Lbl4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl4.Click
        Pos_Lbl_Click(4)
    End Sub

    Private Sub Pos_Lbl5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl5.Click
        Pos_Lbl_Click(5)
    End Sub

    Private Sub Pos_Lbl6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl6.Click
        Pos_Lbl_Click(6)
    End Sub

    Private Sub Pos_Lbl7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl7.Click
        Pos_Lbl_Click(7)
    End Sub

    Private Sub Pos_Lbl8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl8.Click
        Pos_Lbl_Click(8)
    End Sub

    Private Sub Pos_Lbl9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl9.Click
        Pos_Lbl_Click(9)
    End Sub

    Private Sub Pos_Lbl10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl10.Click
        Pos_Lbl_Click(10)
    End Sub

    Private Sub Pos_Lbl11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl11.Click
        Pos_Lbl_Click(11)
    End Sub

    Private Sub Pos_Lbl12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl12.Click
        Pos_Lbl_Click(12)
    End Sub

    Private Sub Pos_Lbl13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl13.Click
        Pos_Lbl_Click(13)
    End Sub

    Private Sub Pos_Lbl14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl14.Click
        Pos_Lbl_Click(14)
    End Sub

    Private Sub Pos_Lbl15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl15.Click
        Pos_Lbl_Click(15)
    End Sub

    Private Sub Pos_Lbl16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl16.Click
        Pos_Lbl_Click(16)
    End Sub

    Private Sub Pos_Lbl17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl17.Click
        Pos_Lbl_Click(17)
    End Sub

    Private Sub Pos_Lbl18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl18.Click
        Pos_Lbl_Click(18)
    End Sub

    Private Sub Pos_Lbl19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl19.Click
        Pos_Lbl_Click(19)
    End Sub

    Private Sub Pos_Lbl20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl20.Click
        Pos_Lbl_Click(20)
    End Sub

    Private Sub Pos_Lbl21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl21.Click
        Pos_Lbl_Click(21)
    End Sub

    Private Sub Pos_Lbl22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl22.Click
        Pos_Lbl_Click(22)
    End Sub

    Private Sub Pos_Lbl23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl23.Click
        Pos_Lbl_Click(23)
    End Sub

    Private Sub Pos_Lbl24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl24.Click
        Pos_Lbl_Click(24)
    End Sub

    Private Sub Pos_Lbl25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl25.Click
        Pos_Lbl_Click(25)
    End Sub

    Private Sub Pos_Lbl26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl26.Click
        Pos_Lbl_Click(26)
    End Sub

    Private Sub Pos_Lbl27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl27.Click
        Pos_Lbl_Click(27)
    End Sub

    Private Sub Pos_Lbl28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl28.Click
        Pos_Lbl_Click(28)
    End Sub

    Private Sub Pos_Lbl29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl29.Click
        Pos_Lbl_Click(29)
    End Sub

    Private Sub Pos_Lbl30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl30.Click
        Pos_Lbl_Click(30)
    End Sub

    Private Sub Pos_Lbl31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl31.Click
        Pos_Lbl_Click(31)
    End Sub

    Private Sub Pos_Lbl32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl32.Click
        Pos_Lbl_Click(32)
    End Sub

    Private Sub Pos_Lbl33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl33.Click
        Pos_Lbl_Click(33)
    End Sub

    Private Sub Pos_Lbl34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl34.Click
        Pos_Lbl_Click(34)
    End Sub

    Private Sub Pos_Lbl35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl35.Click
        Pos_Lbl_Click(35)
    End Sub

    Private Sub Pos_Lbl36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl36.Click
        Pos_Lbl_Click(36)
    End Sub

    Private Sub Pos_Lbl37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl37.Click
        Pos_Lbl_Click(37)
    End Sub

    Private Sub Pos_Lbl38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl38.Click
        Pos_Lbl_Click(38)
    End Sub

    Private Sub Pos_Lbl39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl39.Click
        Pos_Lbl_Click(39)
    End Sub

    Private Sub Pos_Lbl40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl40.Click
        Pos_Lbl_Click(40)
    End Sub

    Private Sub Pos_Lbl41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl41.Click
        Pos_Lbl_Click(41)
    End Sub

    Private Sub Pos_Lbl42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl42.Click
        Pos_Lbl_Click(42)
    End Sub

    Private Sub Pos_Lbl43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl43.Click
        Pos_Lbl_Click(43)
    End Sub

    Private Sub Pos_Lbl44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl44.Click
        Pos_Lbl_Click(44)
    End Sub

    Private Sub Pos_Lbl45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl45.Click
        Pos_Lbl_Click(45)
    End Sub

    Private Sub Pos_Lbl46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl46.Click
        Pos_Lbl_Click(46)
    End Sub

    Private Sub Pos_Lbl47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl47.Click
        Pos_Lbl_Click(47)
    End Sub

    Private Sub Pos_Lbl48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl48.Click
        Pos_Lbl_Click(48)
    End Sub

    Private Sub Pos_Lbl49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl49.Click
        Pos_Lbl_Click(49)
    End Sub

    Private Sub Pos_Lbl50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl50.Click
        Pos_Lbl_Click(50)
    End Sub

    Private Sub Pos_Lbl51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl51.Click
        Pos_Lbl_Click(51)
    End Sub

    Private Sub Pos_Lbl52_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl52.Click
        Pos_Lbl_Click(52)
    End Sub

    Private Sub Pos_Lbl53_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl53.Click
        Pos_Lbl_Click(53)
    End Sub

    Private Sub Pos_Lbl54_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl54.Click
        Pos_Lbl_Click(54)
    End Sub

    Private Sub Pos_Lbl55_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl55.Click
        Pos_Lbl_Click(55)
    End Sub

    Private Sub Pos_Lbl56_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl56.Click
        Pos_Lbl_Click(56)
    End Sub

    Private Sub Pos_Lbl57_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl57.Click
        Pos_Lbl_Click(57)
    End Sub

    Private Sub Pos_Lbl58_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl58.Click
        Pos_Lbl_Click(58)
    End Sub

    Private Sub Pos_Lbl59_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl59.Click
        Pos_Lbl_Click(59)
    End Sub

    Private Sub Pos_Lbl60_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pos_Lbl60.Click
        Pos_Lbl_Click(60)
    End Sub

    Private Sub Lbl_X_PicBx1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx1.Click
        Lbl_X_PicBx_Click(1)
    End Sub

    Private Sub Lbl_X_PicBx2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx2.Click
        Lbl_X_PicBx_Click(2)
    End Sub

    Private Sub Lbl_X_PicBx3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx3.Click
        Lbl_X_PicBx_Click(3)
    End Sub

    Private Sub Lbl_X_PicBx4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx4.Click
        Lbl_X_PicBx_Click(4)
    End Sub

    Private Sub Lbl_X_PicBx5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx5.Click
        Lbl_X_PicBx_Click(5)
    End Sub

    Private Sub Lbl_X_PicBx6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx6.Click
        Lbl_X_PicBx_Click(6)
    End Sub

    Private Sub Lbl_X_PicBx7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx7.Click
        Lbl_X_PicBx_Click(7)
    End Sub

    Private Sub Lbl_X_PicBx8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx8.Click
        Lbl_X_PicBx_Click(8)
    End Sub

    Private Sub Lbl_X_PicBx9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx9.Click
        Lbl_X_PicBx_Click(9)
    End Sub

    Private Sub Lbl_X_PicBx10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx10.Click
        Lbl_X_PicBx_Click(10)
    End Sub

    Private Sub Lbl_X_PicBx11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx11.Click
        Lbl_X_PicBx_Click(11)
    End Sub

    Private Sub Lbl_X_PicBx12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx12.Click
        Lbl_X_PicBx_Click(12)
    End Sub

    Private Sub Lbl_X_PicBx13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx13.Click
        Lbl_X_PicBx_Click(13)
    End Sub

    Private Sub Lbl_X_PicBx14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx14.Click
        Lbl_X_PicBx_Click(14)
    End Sub

    Private Sub Lbl_X_PicBx15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx15.Click
        Lbl_X_PicBx_Click(15)
    End Sub

    Private Sub Lbl_X_PicBx16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx16.Click
        Lbl_X_PicBx_Click(16)
    End Sub

    Private Sub Lbl_X_PicBx17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx17.Click
        Lbl_X_PicBx_Click(17)
    End Sub

    Private Sub Lbl_X_PicBx18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx18.Click
        Lbl_X_PicBx_Click(18)
    End Sub

    Private Sub Lbl_X_PicBx19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx19.Click
        Lbl_X_PicBx_Click(19)
    End Sub

    Private Sub Lbl_X_PicBx20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx20.Click
        Lbl_X_PicBx_Click(20)
    End Sub

    Private Sub Lbl_X_PicBx21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx21.Click
        Lbl_X_PicBx_Click(21)
    End Sub

    Private Sub Lbl_X_PicBx22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx22.Click
        Lbl_X_PicBx_Click(22)
    End Sub

    Private Sub Lbl_X_PicBx23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx23.Click
        Lbl_X_PicBx_Click(23)
    End Sub

    Private Sub Lbl_X_PicBx24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx24.Click
        Lbl_X_PicBx_Click(24)
    End Sub

    Private Sub Lbl_X_PicBx25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx25.Click
        Lbl_X_PicBx_Click(25)
    End Sub

    Private Sub Lbl_X_PicBx26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx26.Click
        Lbl_X_PicBx_Click(26)
    End Sub

    Private Sub Lbl_X_PicBx27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx27.Click
        Lbl_X_PicBx_Click(27)
    End Sub

    Private Sub Lbl_X_PicBx28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx28.Click
        Lbl_X_PicBx_Click(28)
    End Sub

    Private Sub Lbl_X_PicBx29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx29.Click
        Lbl_X_PicBx_Click(29)
    End Sub

    Private Sub Lbl_X_PicBx30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx30.Click
        Lbl_X_PicBx_Click(30)
    End Sub

    Private Sub Lbl_X_PicBx31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx31.Click
        Lbl_X_PicBx_Click(31)
    End Sub

    Private Sub Lbl_X_PicBx32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx32.Click
        Lbl_X_PicBx_Click(32)
    End Sub

    Private Sub Lbl_X_PicBx33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx33.Click
        Lbl_X_PicBx_Click(33)
    End Sub

    Private Sub Lbl_X_PicBx34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx34.Click
        Lbl_X_PicBx_Click(34)
    End Sub

    Private Sub Lbl_X_PicBx35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx35.Click
        Lbl_X_PicBx_Click(35)
    End Sub

    Private Sub Lbl_X_PicBx36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx36.Click
        Lbl_X_PicBx_Click(36)
    End Sub

    Private Sub Lbl_X_PicBx37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx37.Click
        Lbl_X_PicBx_Click(37)
    End Sub

    Private Sub Lbl_X_PicBx38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx38.Click
        Lbl_X_PicBx_Click(38)
    End Sub

    Private Sub Lbl_X_PicBx39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx39.Click
        Lbl_X_PicBx_Click(39)
    End Sub

    Private Sub Lbl_X_PicBx40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx40.Click
        Lbl_X_PicBx_Click(40)
    End Sub

    Private Sub Lbl_X_PicBx41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx41.Click
        Lbl_X_PicBx_Click(41)
    End Sub

    Private Sub Lbl_X_PicBx42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx42.Click
        Lbl_X_PicBx_Click(42)
    End Sub

    Private Sub Lbl_X_PicBx43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx43.Click
        Lbl_X_PicBx_Click(43)
    End Sub

    Private Sub Lbl_X_PicBx44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx44.Click
        Lbl_X_PicBx_Click(44)
    End Sub

    Private Sub Lbl_X_PicBx45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx45.Click
        Lbl_X_PicBx_Click(45)
    End Sub

    Private Sub Lbl_X_PicBx46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx46.Click
        Lbl_X_PicBx_Click(46)
    End Sub

    Private Sub Lbl_X_PicBx47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx47.Click
        Lbl_X_PicBx_Click(47)
    End Sub

    Private Sub Lbl_X_PicBx48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx48.Click
        Lbl_X_PicBx_Click(48)
    End Sub

    Private Sub Lbl_X_PicBx49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx49.Click
        Lbl_X_PicBx_Click(49)
    End Sub

    Private Sub Lbl_X_PicBx50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx50.Click
        Lbl_X_PicBx_Click(50)
    End Sub

    Private Sub Lbl_X_PicBx51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx51.Click
        Lbl_X_PicBx_Click(51)
    End Sub

    Private Sub Lbl_X_PicBx52_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx52.Click
        Lbl_X_PicBx_Click(52)
    End Sub

    Private Sub Lbl_X_PicBx53_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx53.Click
        Lbl_X_PicBx_Click(53)
    End Sub

    Private Sub Lbl_X_PicBx54_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx54.Click
        Lbl_X_PicBx_Click(54)
    End Sub

    Private Sub Lbl_X_PicBx55_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx55.Click
        Lbl_X_PicBx_Click(55)
    End Sub

    Private Sub Lbl_X_PicBx56_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx56.Click
        Lbl_X_PicBx_Click(56)
    End Sub

    Private Sub Lbl_X_PicBx57_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx57.Click
        Lbl_X_PicBx_Click(57)
    End Sub

    Private Sub Lbl_X_PicBx58_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx58.Click
        Lbl_X_PicBx_Click(58)
    End Sub

    Private Sub Lbl_X_PicBx59_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx59.Click
        Lbl_X_PicBx_Click(59)
    End Sub

    Private Sub Lbl_X_PicBx60_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lbl_X_PicBx60.Click
        Lbl_X_PicBx_Click(60)
    End Sub

    '---------------------------------------------------------------------------------------------------------------------------------------
    'BACKGROUNDWORKER
    '---------------------------------------------------------------------------------------------------------------------------------------

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        'If BackgroundWorker1.CancellationPending Then
        '    e.Cancel = True
        '    Exit Sub
        'End If

        'Dim dt1 As New DataTable

        'If Traveller_RdBtn.Checked = True Then

        '    Dim data1 As New MySqlDataAdapter("SELECT metallized_ln FROM management_main ORDER BY metallized_ln ASC", con_gl1_bgw1)
        '    data1.Fill(dt1)
        '    data1.Dispose()

        'Else

        '    Dim data1 As New MySqlDataAdapter("SELECT pcard_lot_number FROM swp_pcard_main ORDER BY pcard_lot_number ASC", con_gl2_bgw1)
        '    data1.Fill(dt1)
        '    data1.Dispose()

        'End If

        'If dt1.Rows.Count > 0 Then

        '    lot_number_src = New AutoCompleteStringCollection

        '    For i As Integer = 0 To dt1.Rows.Count - 1

        '        If BackgroundWorker1.CancellationPending Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If

        '        Dim metallized_ln_display As String

        '        If Traveller_RdBtn.Checked = True Then
        '            metallized_ln_display = Get_String_Before(dt1.Rows(i).Item(0).ToString(), "-")
        '        Else
        '            metallized_ln_display = dt1.Rows(i).Item(0).ToString()
        '        End If

        '        If Not lot_number_src.Contains(metallized_ln_display) Then
        '            lot_number_src.Add(metallized_ln_display)
        '        End If

        '    Next

        'End If

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Lot_Number_TxtBx.AutoCompleteMode = AutoCompleteMode.Suggest
        Lot_Number_TxtBx.AutoCompleteSource = AutoCompleteSource.CustomSource
        Lot_Number_TxtBx.AutoCompleteCustomSource = lot_number_src
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork

        SetDataGridViewRowsClear(DataGridView1)
        rows1 = New List(Of DataGridViewRow)()
        Dim partsnumberallowaudit = "AMS4R7J1P-TA-CS01-PL-B"
        Dim dt1 As New DataTable

        If Traveller_RdBtn.Checked = True Then
            'Dim data1 As New MySqlDataAdapter("SELECT lotno, po_no, inout_assign_id, shipdate FROM cci_inout_assign WHERE po_no LIKE '%" & metallized_ln_reference & "%' AND shipdate = '" & CDate(shipdate).ToString("yyyy-MM-dd") & "' AND partsno = '" & partsnumberallowaudit & "' AND deleted = 0 ORDER BY shipdate ASC", con_gl1_bgw2)

            Dim data1 As New MySqlDataAdapter("SELECT lotno, po_no, inout_assign_id, shipdate FROM cci_inout_assign WHERE po_no LIKE '%" & metallized_ln_reference & "%' AND shipdate = '" & CDate(shipdate).ToString("yyyy-MM-dd") & "' AND deleted = 0 ORDER BY shipdate ASC", con_gl1_bgw2)
            data1.Fill(dt1)
            data1.Dispose()

        Else
            'Dim data1 As New MySqlDataAdapter("SELECT inout_assign_id, partsno, lotno, shipdate FROM  cci_inout_assign WHERE lotno LIKE '%" & metallized_ln_reference & "%' AND shipdate > '" & "1990-01-01" & "' AND partsno = '" & partsnumberallowaudit & "' AND deleted = 0 ORDER BY inout_assign_id ASC", con_gl2_bgw2)

            Dim data1 As New MySqlDataAdapter("SELECT inout_assign_id, partsno, lotno, shipdate FROM  cci_inout_assign WHERE lotno LIKE '%" & metallized_ln_reference & "%' AND shipdate > '" & "1990-01-01" & "' AND deleted = 0 ORDER BY inout_assign_id ASC", con_gl2_bgw2)
            data1.Fill(dt1)
            data1.Dispose()

        End If

        If dt1.Rows.Count > 0 Then

            For i As Integer = 0 To dt1.Rows.Count - 1

                Dim metallized_ln As String = dt1.Rows(i).Item(0)
                Dim metallized_pn As String = dt1.Rows(i).Item(1)
                Dim management_id As Integer = dt1.Rows(i).Item(2)
                Dim row As DataGridViewRow = New DataGridViewRow()

                row.CreateCells(DataGridView1)
                row.Cells(0).Value = metallized_pn
                row.Cells(1).Value = metallized_ln
                row.Cells(2).Value = management_id
                rows1.Add(row)

            Next

        End If

    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        DataGridView1.Rows.AddRange(rows1.ToArray())
        rows1.Clear()
    End Sub

    Private Sub BackgroundWorker3_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork

        Dim excel_path As String = "C:\CMI-SD Application\Template\Traveller2.xls"

        'for qr code partsnumber
        Dim excel_pathpn As String = "C:\CMI-SD Application\Template\PN QR\TravellerPN.xls"

        'Dim excel_path As String = "\\172.16.2.41\Software Updater\List of Templates\CCI\Template\Traveller2.xls"
        Dim data_ln_pn_array(59, 1) As String
        Dim data_ln_pn_arraypn(59, 1) As String
        Dim qr_code_bmp_array(DataGridView2.Rows.Count - 1) As Bitmap
        Dim qr_code_bmp_arraypn(DataGridView2.Rows.Count - 1) As Bitmap
        Dim QR_Generator As New QRCodeEncoder
        QR_Generator.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        QR_Generator.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
        QR_Generator.QRCodeVersion = 5
        QR_Generator.QRCodeScale = 3

        If Traveller_RdBtn.Checked = True Then
            excel_path = "C:\CMI-SD Application\Template\Traveller2.xls"
            excel_pathpn = "C:\CMI-SD Application\Template\PN QR\TravellerPN.xls"
            'excel_path = "\\172.16.2.41\Software Updater\List of Templates\CCI\Template\Traveller2.xls"
        Else
            excel_path = "C:\CMI-SD Application\Template\Traveller2.xls"
             excel_pathpn = "C:\CMI-SD Application\Template\PN QR\TravellerPN.xls"
            'excel_path = "\\172.16.2.41\Software Updater\List of Templates\CCI\Template\Traveller2.xls"
        End If

        For i As Integer = 0 To DataGridView2.Rows.Count - 1

            Dim metallized_pn As String = DataGridView2.Rows(i).Cells(0).Value.ToString()
            Dim metallized_ln As String = DataGridView2.Rows(i).Cells(1).Value.ToString()
            Dim reference_id As Integer = DataGridView2.Rows(i).Cells(2).Value
            Dim position_index As Integer = DataGridView2.Rows(i).Cells(3).Value
            Dim base_metallized_ln As String = ""
            Dim metallized_wafer_number As Integer = 0
            Dim po_pn As String = getPN(DataGridView2.Rows(i).Cells(0).Value.ToString())
            If Traveller_RdBtn.Checked = True Then
                'base_metallized_ln = Get_String_Before(metallized_ln, "-")
                'metallized_wafer_number = Convert.ToInt32(Get_String_After(metallized_ln, "-"))
            End If

            Dim qr_code_image As Image = QR_Generator.Encode(metallized_ln & "[" & metallized_pn & "]" & reference_id)
            Dim qrcode_bmp As New Bitmap(qr_code_image)

            'for qr code partsnumber
            Dim qr_code_imagepn As Image = QR_Generator.Encode(po_pn)
            Dim qrcode_bmppn As New Bitmap(qr_code_imagepn)

            'Dim qrcode_bmp As New Bitmap(qr_code_image)
            'qr_code_bmp_array(i) = qrcode_bmp
            qr_code_bmp_array(i) = qrcode_bmp
            qr_code_bmp_arraypn(i) = qrcode_bmppn
            data_ln_pn_array(position_index, 0) = metallized_pn
            data_ln_pn_array(position_index, 1) = metallized_ln

            'for qr code partsnumber
            data_ln_pn_arraypn(position_index, 0) = po_pn
            data_ln_pn_arraypn(position_index, 1) = sdate 'uncommend if shipdate is deployed
        Next
        'for qr code traveller
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlWorkSheet_lbl As Excel.Worksheet

        'for qr code partsnumber  
        Dim xlApppn As Excel.Application
        Dim xlWorkBookpn As Excel.Workbook
        Dim xlWorkSheetpn As Excel.Worksheet
        Dim xlWorkSheet_lblpn As Excel.Worksheet

        'for qr code traveller
        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Open(excel_path)
        xlWorkSheet = xlWorkBook.Sheets("PrintItem")

        'for qr code partsnumber  
        xlApppn = New Excel.Application
        xlWorkBookpn = xlApppn.Workbooks.Open(excel_pathpn)
        xlWorkSheetpn = xlWorkBookpn.Sheets("PrintItemPN")

        If Traveller_RdBtn.Checked = True Then
            'for qr code traveller
            xlWorkSheet_lbl = xlWorkBook.Sheets("Traveller")
            'for qr code partsnumber  
            xlWorkSheet_lblpn = xlWorkBookpn.Sheets("TravellerPN")
        Else
            xlWorkSheet_lbl = xlWorkBook.Sheets("Tray")
            'for qr code partsnumber  
            xlWorkSheet_lblpn = xlWorkBookpn.Sheets("TravellerPN")
        End If
        'for qr code traveller
        xlWorkSheet.Range("C2:D61").Value = data_ln_pn_array

        'for qr code partsnumber  
        xlWorkSheetpn.Range("C2:D61").Value = data_ln_pn_arraypn

        'for qr code traveller
        'Get cells and paste QR Code
        Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & excel_path & ";Extended Properties=Excel 12.0;")
        Dim data2 As New OleDbDataAdapter("Select * from [qr$A1:A]", conn)
        Dim dt2 As New DataTable
        data2.Fill(dt2)
        data2.Dispose()
        'xlWorkSheet_lbl.Activate()
        For i As Integer = 0 To DataGridView2.Rows.Count - 1

            Dim position_index As Integer = DataGridView2.Rows(i).Cells(3).Value
            Dim cell As String = dt2.Rows(position_index).Item(0).ToString()
            Dim l As Integer = i


            BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qr_code_bmp_array(l))))
            xlWorkSheet_lbl.Range(cell).Select()
            Threading.Thread.Sleep(500)
            xlWorkSheet_lbl.PasteSpecial("Bitmap")

        Next

        'for qr code traveller
        Dim desktop_path As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Dim current_date As String = Date.Now.ToString("MMddyyyy-hhmmss")
        Dim file_name_save As String
        Dim file_name_savepn As String

        If Traveller_RdBtn.Checked = True Then
            file_name_save = desktop_path & "\Traveller " & current_date & ".xls"
            'file_name_savepn = desktop_path & "\TravellerPN " & current_date & ".xls"
        Else
            file_name_save = desktop_path & "\Tray " & current_date & ".xls"
            'file_name_savepn = desktop_path & "\TravellerPN " & current_date & ".xls"
        End If
        xlApp.Visible = True
        'xlWorkSheet.SaveAs(file_name_save)

        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
        releaseObject(xlWorkSheet_lbl)

        'for qr code partsnumber 
        'Get cells and paste QR Code
        Dim connpn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & excel_pathpn & ";Extended Properties=Excel 12.0;")
        Dim data2pn As New OleDbDataAdapter("Select * from [qr$A1:A]", connpn)
        Dim dt2pn As New DataTable
        data2pn.Fill(dt2pn)
        data2pn.Dispose()
        xlWorkSheet_lblpn.Activate()
        For i As Integer = 0 To DataGridView2.Rows.Count - 1

            Dim position_index As Integer = DataGridView2.Rows(i).Cells(3).Value
            Dim cell As String = dt2pn.Rows(position_index).Item(0).ToString()
            Dim l As Integer = i
            'MsgBox(cell)
            BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qr_code_bmp_arraypn(l))))
            xlWorkSheet_lblpn.Range(cell).Select()
            'MsgBox(xlWorkSheet_lblpn.Range(cell).Select())
            Threading.Thread.Sleep(500)
            xlWorkSheet_lblpn.PasteSpecial("Bitmap")

        Next
        'for qr code traveller
        'Dim desktop_path As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        'Dim current_date As String = Date.Now.ToString("MMddyyyy-hhmmss")
        'Dim file_name_save As String

        'for qr code partsnumber
        'Dim desktop_pathpn As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        'Dim current_datepn As String = Date.Now.ToString("MMddyyyy-hhmmss")
        'Dim file_name_savepn As String

        If Traveller_RdBtn.Checked = True Then
            ' file_name_save = desktop_path & "\Traveller " & current_date & ".xls"
            file_name_savepn = desktop_path & "\TravellerPN " & current_date & ".xls"
        Else
            'file_name_save = desktop_path & "\Tray " & current_date & ".xls"
            file_name_savepn = desktop_path & "\TravellerPN " & current_date & ".xls"
        End If

        'View and save workbook
        'xlApp.Visible = True
        'xlWorkSheet.SaveAs(file_name_save)

        'for partsnumber
        xlApppn.Visible = True
        ' xlWorkSheetpn.SaveAs(file_name_savepn)

        'Release Excel COM objects
        'releaseObject(xlApp)
        'releaseObject(xlWorkBook)
        'releaseObject(xlWorkSheet)
        'releaseObject(xlWorkSheet_lbl)

        'for partsnumber
        releaseObject(xlApppn)
        releaseObject(xlWorkBookpn)
        releaseObject(xlWorkSheetpn)
        releaseObject(xlWorkSheet_lblpn)

    End Sub

    '---------------------------------------------------------------------------------------------------------------------------------------
    'CLASS FUNCTIONS
    '---------------------------------------------------------------------------------------------------------------------------------------

    Public Sub Initialize()
        Lot_Number_TxtBx.Text = ""
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
    End Sub

    Public Sub Lbl_Pnl_Visible_False()

        Dim panel() As Control

        For i As Integer = 1 To 60

            panel = Me.Controls.Find("Lbl_Pnl" & i, True)
            Dim Lbl_Pnl As Panel = DirectCast(panel(0), Panel)
            Lbl_Pnl.Visible = False

        Next

    End Sub

    Public Sub Pos_B_PicBx_True()

        Dim control() As Control

        For i As Integer = 1 To 60

            control = Me.Controls.Find("Pos_B_PicBx" & i, True)
            Dim Pos_B_PicBx As PictureBox = DirectCast(control(0), PictureBox)
            Pos_B_PicBx.Visible = True

        Next

    End Sub

    Public Sub Pos_B_PicBx_False()

        Dim control() As Control

        For i As Integer = 1 To 60

            control = Me.Controls.Find("Pos_B_PicBx" & i, True)
            Dim Pos_B_PicBx As PictureBox = DirectCast(control(0), PictureBox)
            Pos_B_PicBx.Visible = False

        Next

    End Sub

    Public Sub Pos_PicBx_True()

        Dim control() As Control

        For i As Integer = 1 To 60

            control = Me.Controls.Find("Pos_PicBx" & i, True)
            Dim Pos_PicBx As PictureBox = DirectCast(control(0), PictureBox)
            Pos_PicBx.Visible = True

        Next

    End Sub

    Public Sub Pos_PicBx_False()

        Dim control() As Control

        For i As Integer = 1 To 60

            control = Me.Controls.Find("Pos_PicBx" & i, True)
            Dim Pos_PicBx As PictureBox = DirectCast(control(0), PictureBox)
            Pos_PicBx.Visible = False

        Next

    End Sub

    Public Sub Lbl_X_PicBx_Visible_True()

        Dim picturebox() As Control

        For i As Integer = 1 To 60

            picturebox = Me.Controls.Find("Lbl_X_PicBx" & i, True)
            Dim Lbl_X_PicBx As PictureBox = DirectCast(picturebox(0), PictureBox)
            Lbl_X_PicBx.Visible = True

        Next

    End Sub

    Public Sub Lbl_X_PicBx_Visible_False()

        Dim picturebox() As Control

        For i As Integer = 1 To 60

            picturebox = Me.Controls.Find("Lbl_X_PicBx" & i, True)
            Dim Lbl_X_PicBx As PictureBox = DirectCast(picturebox(0), PictureBox)
            Lbl_X_PicBx.Visible = False

        Next

    End Sub

    Public Sub Reset()

        Dim panel() As Control

        For i As Integer = 1 To 60

            panel = Me.Controls.Find("Pos_Pnl" & i, True)
            Dim Pos_Pnl As Panel = DirectCast(panel(0), Panel)
            Pos_Pnl.Visible = True

            panel = Me.Controls.Find("Lbl_Pnl" & i, True)
            Dim Lbl_Pnl As Panel = DirectCast(panel(0), Panel)
            Lbl_Pnl.Visible = False

            flag(i - 1) = True

        Next

        DataGridView2.Rows.Clear()

    End Sub

    Public Sub Pos_Lbl_Click(ByVal num As Integer)
        If pos_flag(num - 1) = False Then
            Pos_PicBx_Click(num)
        Else
            Pos_B_PicBx_Click(num)
        End If
    End Sub

    Public Sub Pos_PicBx_Click(ByVal num As Integer)

        Dim control() As Control

        control = Me.Controls.Find("Pos_B_PicBx" & num, True)
        Dim Pos_B_PicBx As PictureBox = DirectCast(control(0), PictureBox)
        Pos_B_PicBx.Visible = True

        control = Me.Controls.Find("Pos_PicBx" & num, True)
        Dim Pos_PicBx As PictureBox = DirectCast(control(0), PictureBox)
        Pos_PicBx.Visible = False

        pos_flag(num - 1) = True
        'MsgBox(num - 1)
    End Sub

    Public Sub Pos_B_PicBx_Click(ByVal num As Integer)

        Dim control() As Control

        control = Me.Controls.Find("Pos_B_PicBx" & num, True)
        Dim Pos_B_PicBx As PictureBox = DirectCast(control(0), PictureBox)
        Pos_B_PicBx.Visible = False

        control = Me.Controls.Find("Pos_PicBx" & num, True)
        Dim Pos_PicBx As PictureBox = DirectCast(control(0), PictureBox)
        Pos_PicBx.Visible = True

        pos_flag(num - 1) = False

    End Sub

    Public Sub Lbl_X_PicBx_Click(ByVal num As Integer)

        Dim control() As Control

        control = Me.Controls.Find("Pos_Pnl" & num, True)
        Dim Pos_Pnl As Panel = DirectCast(control(0), Panel)
        Pos_Pnl.Visible = True

        control = Me.Controls.Find("Lbl_Pnl" & num, True)
        Dim Lbl_Pnl As Panel = DirectCast(control(0), Panel)
        Lbl_Pnl.Visible = False

        flag(num - 1) = True

        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            If DataGridView2.Rows(i).Cells(3).Value = num - 1 Then
                DataGridView2.Rows.RemoveAt(i)
                Exit For
            End If
        Next

    End Sub

    Private Sub Tray_RdBtn_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Tray_RdBtn.CheckedChanged
        'Panel3.Visible = False
        'Container_Pnl.Visible = False
        'Ex_Btn.Visible = False
        'Res_Btn.Visible = False
        Me.Hide()
        WorkTraveller.initialize()
        WorkTraveller.Show()
    End Sub

    Private Sub Traveller_RdBtn_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Traveller_RdBtn.CheckedChanged
        Panel3.Visible = True
        Container_Pnl.Visible = True
        Ex_Btn.Visible = True
        Res_Btn.Visible = True
        Me.Show()
        WorkTraveller.Hide()
    End Sub

    Private Sub Lot_Number_TxtBx_TextChanged(sender As Object, e As System.EventArgs) Handles Lot_Number_TxtBx.TextChanged
        If Traveller_RdBtn.Checked = True Then
            Cmb_shipdate.Visible = True
            If Trim(Lot_Number_TxtBx.Text) = "" Then
                Cmb_shipdate.Visible = False
            Else
                Cmb_shipdate.Visible = True
                getShipdate()
            End If
        Else
            Cmb_shipdate.Visible = False
        End If
    End Sub
    Dim shipdate As String
    Private Sub Cmb_shipdate_TextChanged(sender As Object, e As System.EventArgs) Handles Cmb_shipdate.TextChanged
        shipdate = CDate(Cmb_shipdate.Text).ToString("yyy-MM-dd")
    End Sub
    Sub iniatilize()
        Traveller_RdBtn.Checked = True
    End Sub

    Private Sub BackgroundWorker3_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker3.RunWorkerCompleted
        Ex_Btn.Enabled = True
    End Sub

    Private Sub Btn_compare_Click(sender As System.Object, e As System.EventArgs) Handles Btn_compare.Click
        comparelabel.ShowDialog()
        comparelabel.BringToFront()
    End Sub
End Class