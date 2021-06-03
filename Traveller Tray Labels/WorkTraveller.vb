Imports MySql.Data.MySqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Imports ThoughtWorks.QRCode.Codec
Imports System.IO
Imports System.Data.OleDb
Public Class WorkTraveller
    Dim localDrive As String = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 2)
    Dim source As String
    Dim dest As String
    Dim itemtype As String
    Private Sub Form1_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        PC_Generate_Labels.Close()
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Ln_Search_PicBx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ln_Search_PicBx.Click
        DataGridView1.Rows.Clear()
        Dim dt As New DataTable
        Dim data1 As New MySqlDataAdapter("SELECT parstnumber, lot_no, received_date FROM  cci_stocknew_lotstatus WHERE lot_no LIKE '%" & Lot_Number_TxtBx.Text & "%' AND received_date > '" & "1990-01-01" & "' " & _
                                           "AND zerobalance <> 1 AND ngconfirmed <> 1 AND ngpulledout <> 1 AND lostwafer <> 1 ORDER BY received_date DESC", con_cci)
        data1.Fill(dt)
        data1.Dispose()

        If dt.Rows.Count > 0 Then
            'MsgBox("stocknew")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row1 As New DataGridViewRow
                'DataGridView1.Rows.Add(1)
                'DataGridView1.Rows(i).Cells(0).Value = dt(i)(0)
                'DataGridView1.Rows(i).Cells(1).Value = dt(i)(1)
                'DataGridView1.Rows(i).Cells(2).Value = CDate(dt(i)(2)).ToString("yyyy-MM-dd")
                row1.Cells.Add(New DataGridViewTextBoxCell With {.Value = dt(i)(0)})
                row1.Cells.Add(New DataGridViewTextBoxCell With {.Value = dt(i)(1)})
                row1.Cells.Add(New DataGridViewTextBoxCell With {.Value = CDate(dt(i)(2)).ToString("yyyy-MM-dd")})
                doRowAdd(DataGridView1, row1)
            Next
        Else

        End If
        Dim dt2 As New DataTable
        Dim data2 As New MySqlDataAdapter("SELECT Partsnumber, lotnumber, date_received FROM ccd_received_wafer_tables WHERE lotnumber LIKE '%" & Lot_Number_TxtBx.Text & "%' AND date_received > '1990-01-01' AND " & _
                                          "cci_received <> 1 AND NGConfirmed <> 1 AND NGPulledOut <> 1 AND NG <> 1 ORDER BY date_received DESC", ccp_management_db)
        data2.Fill(dt2)
        data2.Dispose()
        If dt2.Rows.Count > 0 Then
            'MsgBox("ccd_received_wafer")
            For i2 As Integer = 0 To dt2.Rows.Count - 1
                Dim row2 As New DataGridViewRow
                'DataGridView1.Rows.Add(1)
                'DataGridView1.Rows(i2).Cells(0).Value = dt2(i2)(0)
                'DataGridView1.Rows(i2).Cells(1).Value = dt2(i2)(1)
                'DataGridView1.Rows(i2).Cells(2).Value = CDate(dt2(i2)(2)).ToString("yyyy-MM-dd")
                row2.Cells.Add(New DataGridViewTextBoxCell With {.Value = dt2(i2)(0)})
                row2.Cells.Add(New DataGridViewTextBoxCell With {.Value = dt2(i2)(1)})
                row2.Cells.Add(New DataGridViewTextBoxCell With {.Value = CDate(dt2(i2)(2)).ToString("yyyy-MM-dd")})
                doRowAdd(DataGridView1, row2)
            Next
        End If

        If DataGridView1.Rows.Count > 0 Then
            detItemtype(DataGridView1.Rows(0).Cells(0).Value)
        End If
    End Sub
    Sub detItemtype(ByVal pn As String)
        If Trim(pn) <> "" Then
            If pn.Substring(0, 3).ToUpper() = "HTR" Or pn.Substring(0, 3).ToUpper() = "HDI" Or pn.Substring(0, 3).ToUpper() = "PZM" Or pn.Substring(0, 3) = "760" Or pn.Substring(0, 4) = "2142" Or pn.Substring(0, 4) = "3610" Or pn.Substring(0, 2).ToUpper() = "CC" Or pn.Substring(0, 2).ToUpper() = "CA" _
                Or pn.Substring(0, 3).ToUpper() = "TCS" Or pn.Substring(0, 2).ToUpper() = "CS" Or pn.Substring(0, 3).ToUpper() = "TTS" Or pn.Substring(0, 3).ToUpper() = "TSB" Or pn.Substring(0, 2).ToUpper() = "HZ" Or pn.Substring(0, 4).ToUpper() = "USMM" Or pn.Substring(0, 3).ToUpper() = "SAW" Or pn.Substring(0, 5).ToUpper() = "L2SAW" _
                Or pn.Substring(0, 3).ToUpper() = "TGB" Or pn.Substring(0, 3).ToUpper() = "MBW" Or pn.Substring(0, 2).ToUpper() = "PZ" Then
                itemtype = "cus"
            Else
                itemtype = "stdhk"
            End If
        End If
      
    End Sub
    Private Sub Gen_Btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Gen_Btn.Click
        initialized_path()
        'processType()
        geneExcel()
        deleteFile(split_driveLetter(localDrive) & ":\Users\" & Environment.UserName & "\Desktop\QR_ProcessCard\Polishing QR Code")
    End Sub
    Public Sub deleteFile(ByVal directoryName As String)
        For Each files In Directory.GetFiles(directoryName, "*.jpg*", SearchOption.TopDirectoryOnly)
            File.Delete(files)
        Next
    End Sub
    Sub geneExcel()
        If itemtype = "cus" Then
            If CKB_visual.Checked = True And CKB_testing.Checked = True Then
                If Bgw3.IsBusy = True Then
                Else
                    Pcbloading.Visible = True
                    Bgw3.RunWorkerAsync()
                End If
            ElseIf CKB_visual.Checked = True Then
                If Bgw3.IsBusy = True Then
                Else
                    Pcbloading.Visible = True
                    Bgw3.RunWorkerAsync()
                End If
            ElseIf CKB_testing.Checked = True Then
                'MsgBox("enter")
                If Bgw4.IsBusy = True Then
                Else
                    'MsgBox("enter process")
                    Pcbloading.Visible = True
                    Bgw4.RunWorkerAsync()
                End If
            End If
        Else
            If CKB_visual.Checked = True And CKB_testing.Checked = True Then
                If Bgw1.IsBusy = True Then
                Else
                    Pcbloading.Visible = True
                    Bgw1.RunWorkerAsync()
                End If
            ElseIf CKB_visual.Checked = True Then
                If Bgw1.IsBusy = True Then
                Else
                    Pcbloading.Visible = True
                    Bgw1.RunWorkerAsync()
                End If
            ElseIf CKB_testing.Checked = True Then
                If Bgw2.IsBusy = True Then
                Else
                    Pcbloading.Visible = True
                    Bgw2.RunWorkerAsync()
                End If
            End If
           
        End If
        'If Rdb_prod.Checked = True Then
        '    'processType()
        '    If Bgw1.IsBusy = True Then
        '    Else
        '        Pcbloading.Visible = True
        '        Bgw1.RunWorkerAsync()
        '    End If
        'ElseIf Rdb_Qc.Checked = True Then
        '    'toExcelQc()
        '    If Bgw2.IsBusy = True Then
        '    Else
        '        Pcbloading.Visible = True
        '        Bgw2.RunWorkerAsync()
        '    End If
        'Else
        '    If Bgw3.IsBusy = True Then
        '    Else
        '        Pcbloading.Visible = True
        '        Bgw3.RunWorkerAsync()
        '    End If
        'End If
    End Sub
    Sub processType()
        Dim x1 As Integer = 2
        Dim x2 As Integer = 3

        Dim y1 As Integer = 40
        Dim y2 As Integer = 40
        Dim cntLine As Integer = 1

        Dim qrX As Integer = 2
        Dim qrY As Integer = 37
        Dim cellletter As String

        Dim Loca As Integer() = {10, 172, 337, 500, 662, 825}
        Dim excel_path As String = "\\172.16.2.41\Software Updater\List of Templates\CCI\Template\CCIFMQA03002WorkTravellerOfficialedit2.xls"
        Dim qr_code_bmp_array(DataGridView1.Rows.Count - 1) As Bitmap
        Dim QR_Generator As New QRCodeEncoder
        QR_Generator.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        QR_Generator.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
        QR_Generator.QRCodeVersion = 3
        QR_Generator.QRCodeScale = 2

        Dim apxl As Excel.Application
        Dim wbxl As Excel.Workbook
        Dim wsxl As Excel.Worksheet
        'Dim wsxlqr As Excel.Worksheet
        Dim loc As Integer = 0
        Dim loc2 As Integer = 1

        Dim qrcon(DataGridView1.Rows.Count - 1) As Bitmap
        'apxl = CreateObject("Excel.Application")
        'apxl.Visible = True
        ''wbxl = apxl.Workbooks.Open("C:\Users\chmg01\Desktop\cwp_data\W18A001-grace1")
        apxl = New Excel.Application
        wbxl = apxl.Workbooks.Open("\\172.16.2.41\Software Updater\List of Templates\CCI\Template\CCIFMQA03002WorkTravellerOfficialedit2.xls")
        wsxl = wbxl.Worksheets("Revised(3)")
        'wsxlqr = wbxl.Worksheets("Revised(3)")

        '************************** - 1 - **************************
        wsxl.Activate()
        For n As Integer = 0 To DataGridView1.Rows.Count - 1
            'Dim dum As String
            'cellletter = "AK" & qrX.ToString()
            wsxl.Cells(x1, y1).Value() = DataGridView1.Rows(n).Cells(0).Value
            wsxl.Cells(x2, y2).Value() = DataGridView1.Rows(n).Cells(1).Value

            'dum = cellletter
            Dim qr_code_image As Image = QR_Generator.Encode(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]")
            Dim qrcode_bmp As New Bitmap(qr_code_image)
            qrcon(n) = qrcode_bmp
            'MsgBox(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]")
            'qr_code_bmp_array(n) = qrcode_bmp
            'Dim r As Excel.Range
            'Dim bArr As Byte() = imgToByteConverter(qr_code_image)
            'Dim ms As New System.IO.MemoryStream(bArr)
            'Dim im As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
            'Dim h As String = dest & "\CCI TRAVELLER\" & DataGridView1.Rows(n).Cells(1).Value & ".jpg"
            ''MsgBox(h)
            'im.Save(h, Imaging.ImageFormat.Jpeg)
            'BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcode_bmp)))
            'If dum = "AK2" Then
            '    MessageBox.Show("GENERATING QR", "GENERATING QR PLEASE WAIT!..." & dum, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If

            'wsxlqr.Range(dum).Select()
            ''Threading.Thread.Sleep(100)
            'wsxlqr.PasteSpecial("Bitmap")
            'dum = wsxl.ToString()

            'r = wsxl.Cells(qrX, qrY)
            'wsxl.Shapes.AddPicture(h, _
            '     Microsoft.Office.Core.MsoTriState.msoCTrue, _
            '     Microsoft.Office.Core.MsoTriState.msoCTrue, r.Left, r.Top, 30, 30)

            'wsxl.Shapes.AddPicture(h, _
            '     Microsoft.Office.Core.MsoTriState.msoCTrue, _
            '     Microsoft.Office.Core.MsoTriState.msoCTrue, r.Left, r.Top, 30, 30)
            'Microsoft.Office.Core.MsoTriState.msoCTrue, qrX, Loca(loc), 30, 30)

            If loc2 = 4 Then
                x1 += 13 + 1
                x2 += 13 + 1
                'qrX += 13 + 1
                loc2 = 1
            Else
                x1 += 13
                x2 += 13
                'qrX += 13

                loc2 += 1
            End If




            'If loc = 5 Then
            '    loc = 0
            'Else
            '    loc += 1
            'End If

        Next

        loc2 = 1
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Dim l As String = i
            Dim dum As String
            cellletter = "AK" & qrX.ToString()
            dum = cellletter
            BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcon(l))))
            If dum = "AK2" Then
                MessageBox.Show("GENERATING QR", "GENERATING QR PLEASE WAIT!..." & dum, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            'MsgBox(l)
            If dum = cellletter Then
                wsxl.Range(dum).Select()
                Threading.Thread.Sleep(500)
                wsxl.PasteSpecial("Bitmap")

                'wsxl.Range("AK2", "AK2").PasteSpecial(Excel.XlPasteType.xlPasteAll, _
                'Excel.XlPasteSpecialOperation.xlPasteSpecialOperationAdd, _
                'False, False)

                'wsxl.Cells(dum, dum).PasteSpecial("Bitmap")
            End If
            'wsxlqr.Range(dum).Select()
            'Threading.Thread.Sleep(500)
            'wsxlqr.PasteSpecial("Bitmap")

            If loc2 = 4 Then
                qrX += 13 + 1
                loc2 = 1
            Else
                qrX += 13

                loc2 += 1
            End If
        Next
        apxl.Visible = True

        releaseObject(apxl)
        releaseObject(wbxl)
        releaseObject(wsxl)
        'releaseObject(wsxlqr)
    End Sub
    Sub toExcelQc()
        Dim x1 As Integer = 1
        Dim x2 As Integer = 2

        Dim y1 As Integer = 2
        Dim y2 As Integer = 2
        Dim cntLine As Integer = 1

        Dim qrX As Integer = 1
        Dim qrY As Integer = 37
        Dim cellletter As String

        Dim Loca As Integer() = {10, 172, 337, 500, 662, 825}
        Dim excel_path As String = "\\172.16.2.41\Software Updater\List of Templates\CCI\Template\CCI FM-PP-001-129 04.xlsx"
        Dim qr_code_bmp_array(DataGridView1.Rows.Count - 1) As Bitmap
        Dim QR_Generator As New QRCodeEncoder
        QR_Generator.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        QR_Generator.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
        QR_Generator.QRCodeVersion = 5
        QR_Generator.QRCodeScale = 1

        Dim apxl As Excel.Application
        Dim wbxl As Excel.Workbook
        Dim wsxl As Excel.Worksheet
        Dim wsxlqr As Excel.Worksheet
        Dim loc As Integer = 0
        Dim loc2 As Integer = 1

        'apxl = CreateObject("Excel.Application")
        'apxl.Visible = True
        ''wbxl = apxl.Workbooks.Open("C:\Users\chmg01\Desktop\cwp_data\W18A001-grace1")
        apxl = New Excel.Application
        wbxl = apxl.Workbooks.Open("\\172.16.2.41\Software Updater\List of Templates\CCI\Template\CCI FM-PP-001-129 04.xlsx")
        wsxl = wbxl.Worksheets("Revise-7-5-2020")
        wsxlqr = wbxl.Worksheets("Revise-7-5-2020")

        '************************** - 1 - **************************
        wsxlqr.Activate()
        For n As Integer = 0 To DataGridView1.Rows.Count - 1
            Dim dum As String
            cellletter = "H" & qrX.ToString()
            wsxl.Cells(x1, y1).Value() = DataGridView1.Rows(n).Cells(0).Value
            wsxl.Cells(x2, y2).Value() = DataGridView1.Rows(n).Cells(1).Value

            dum = cellletter
            Dim qr_code_image As Image = QR_Generator.Encode(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]")
            Dim qrcode_bmp As New Bitmap(qr_code_image)
            BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcode_bmp)))
            If dum = "H1" Then
                MessageBox.Show("GENERATING QR", "GENERATING QR PLEASE WAIT!...", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            wsxlqr.Range(dum).Select()
            'Threading.Thread.Sleep(500)
            wsxlqr.PasteSpecial("Bitmap")


            If loc2 = 4 Then
                x1 += 13 + 1
                x2 += 13 + 1
                qrX += 13 + 1
                loc2 = 1
            Else
                x1 += 13
                x2 += 13
                qrX += 13

                loc2 += 1
            End If




            'If loc = 5 Then
            '    loc = 0
            'Else
            '    loc += 1
            'End If

        Next
        apxl.Visible = True

        releaseObject(apxl)
        releaseObject(wbxl)
        releaseObject(wsxl)
        releaseObject(wsxlqr)
    End Sub
    Sub initialized_path()
        source = "\\172.16.2.41\Software Updater\QR_ProcessCard"
        dest = split_driveLetter(localDrive) & ":\Users\" & Environment.UserName & "\Desktop\QR_ProcessCard"
        copyDirectory(source, dest)
    End Sub
    Sub copyDirectory(ByVal sourcePath As String, ByVal destinationpath As String)
        Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(sourcePath)
        If Not System.IO.Directory.Exists(destinationpath) Then
            System.IO.Directory.CreateDirectory(destinationpath)
        End If

        Dim fileSystemInfo As System.IO.FileSystemInfo
        For Each fileSystemInfo In sourceDirectoryInfo.GetFileSystemInfos
            Dim destinationFileName As String = System.IO.Path.Combine(destinationpath, fileSystemInfo.Name)

            ' Now check whether its a file or a folder and take action accordingly
            If TypeOf fileSystemInfo Is System.IO.FileInfo Then
                System.IO.File.Copy(fileSystemInfo.FullName, destinationFileName, True)
            Else
                ' Recursively call the mothod to copy all the neste folders
                copyDirectory(fileSystemInfo.FullName, destinationFileName)
            End If
        Next

        IO.File.SetAttributes(destinationpath, IO.FileAttributes.Hidden)
    End Sub

    Private Sub Ex_Btn_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Traveller_RdBtn_CheckedChanged(sender As System.Object, e As System.EventArgs)
        Me.Hide()
        PC_Generate_Labels.iniatilize()
        PC_Generate_Labels.Show()
    End Sub

    Private Sub Tray_RdBtn_CheckedChanged(sender As System.Object, e As System.EventArgs)

    End Sub
    Sub initialize()
        'Tray_RdBtn.Checked = True
        'Rdb_prod.Checked = True
        CKB_testing.Checked = True
    End Sub

    Private Sub Bgw1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Bgw1.DoWork
        Dim x1 As Integer = 2
        Dim x2 As Integer = 4

        Dim y1 As Integer = 40
        Dim y2 As Integer = 40
        Dim cntLine As Integer = 1

        Dim qrX As Integer = 3
        Dim qrY As Integer = 37
        Dim cellletter As String

        Dim Loca As Integer() = {10, 172, 337, 500, 662, 825}
        Dim excel_path As String = "\\172.16.2.41\Software Updater\List of Templates\CCI\Template\CCIFMQA03002WorkTravellerOfficialedit3new.xls"
        Dim qr_code_bmp_array(DataGridView1.Rows.Count - 1) As Bitmap
        Dim QR_Generator As New QRCodeEncoder
        QR_Generator.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        QR_Generator.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
        QR_Generator.QRCodeVersion = 4
        QR_Generator.QRCodeScale = 2

        Dim apxl As Excel.Application
        Dim wbxl As Excel.Workbook
        Dim wsxl As Excel.Worksheet
        'Dim wsxlqr As Excel.Worksheet
        Dim loc As Integer = 0
        Dim loc2 As Integer = 1

        Dim qrcon(DataGridView1.Rows.Count - 1) As Bitmap
        'apxl = CreateObject("Excel.Application")
        'apxl.Visible = True
        ''wbxl = apxl.Workbooks.Open("C:\Users\chmg01\Desktop\cwp_data\W18A001-grace1")
        apxl = New Excel.Application
        wbxl = apxl.Workbooks.Open("\\172.16.2.41\Software Updater\List of Templates\CCI\Template\CCIFMQA03002WorkTravellerOfficialedit3new.xls")
        wsxl = wbxl.Worksheets("Revised(3)")
        'wsxlqr = wbxl.Worksheets("Revised(3)")

        '************************** - 1 - **************************
        wsxl.Activate()
        For n As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(n).Selected = True Then
                wsxl.Cells(x1, y1).Value() = DataGridView1.Rows(n).Cells(0).Value
                wsxl.Cells(x2, y2).Value() = DataGridView1.Rows(n).Cells(1).Value

                Dim qr_code_image As Image = QR_Generator.Encode(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]030")
                Dim qrcode_bmp As New Bitmap(qr_code_image)
                qrcon(n) = qrcode_bmp

                If loc2 = 4 Then
                    x1 += 13 + 1
                    x2 += 13 + 1
                    'qrX += 13 + 1
                    loc2 = 1
                Else
                    'x1 += 13
                    'x2 += 13
                    x1 += 14
                    x2 += 14
                    'qrX += 13

                    loc2 += 1
                End If
            End If
            ''Dim dum As String
            ''cellletter = "AK" & qrX.ToString()
            'wsxl.Cells(x1, y1).Value() = DataGridView1.Rows(n).Cells(0).Value
            'wsxl.Cells(x2, y2).Value() = DataGridView1.Rows(n).Cells(1).Value

            ''dum = cellletter
            'Dim qr_code_image As Image = QR_Generator.Encode(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]030")
            'Dim qrcode_bmp As New Bitmap(qr_code_image)
            'qrcon(n) = qrcode_bmp
            ''MsgBox(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]")
            ''qr_code_bmp_array(n) = qrcode_bmp
            ''Dim r As Excel.Range
            ''Dim bArr As Byte() = imgToByteConverter(qr_code_image)
            ''Dim ms As New System.IO.MemoryStream(bArr)
            ''Dim im As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
            ''Dim h As String = dest & "\CCI TRAVELLER\" & DataGridView1.Rows(n).Cells(1).Value & ".jpg"
            ' ''MsgBox(h)
            ''im.Save(h, Imaging.ImageFormat.Jpeg)
            ''BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcode_bmp)))
            ''If dum = "AK2" Then
            ''    MessageBox.Show("GENERATING QR", "GENERATING QR PLEASE WAIT!..." & dum, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ''End If

            ''wsxlqr.Range(dum).Select()
            ' ''Threading.Thread.Sleep(100)
            ''wsxlqr.PasteSpecial("Bitmap")
            ''dum = wsxl.ToString()

            ''r = wsxl.Cells(qrX, qrY)
            ''wsxl.Shapes.AddPicture(h, _
            ''     Microsoft.Office.Core.MsoTriState.msoCTrue, _
            ''     Microsoft.Office.Core.MsoTriState.msoCTrue, r.Left, r.Top, 30, 30)

            ''wsxl.Shapes.AddPicture(h, _
            ''     Microsoft.Office.Core.MsoTriState.msoCTrue, _
            ''     Microsoft.Office.Core.MsoTriState.msoCTrue, r.Left, r.Top, 30, 30)
            ''Microsoft.Office.Core.MsoTriState.msoCTrue, qrX, Loca(loc), 30, 30)

            'If loc2 = 4 Then
            '    x1 += 13 + 1
            '    x2 += 13 + 1
            '    'qrX += 13 + 1
            '    loc2 = 1
            'Else
            '    'x1 += 13
            '    'x2 += 13
            '    x1 += 14
            '    x2 += 14
            '    'qrX += 13

            '    loc2 += 1
            'End If




            ''If loc = 5 Then
            ''    loc = 0
            ''Else
            ''    loc += 1
            ''End If

        Next

        loc2 = 1
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Selected = True Then
                Dim l As String = i
                Dim dum As String
                cellletter = "AJ" & qrX.ToString()
                dum = cellletter
                BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcon(l))))
                If dum = "AJ3" Then
                    MessageBox.Show("GENERATING QR", "GENERATING QR PLEASE WAIT!..." & dum, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                If dum = cellletter Then
                    wsxl.Range(dum).Select()
                    Threading.Thread.Sleep(500)
                    wsxl.PasteSpecial("Bitmap")
                End If

                If loc2 = 4 Then
                    qrX += 13 + 1
                    loc2 = 1
                Else
                    qrX += 14
                    loc2 += 1
                End If
            End If
            'Dim l As String = i
            'Dim dum As String
            'cellletter = "AJ" & qrX.ToString()
            'dum = cellletter
            'BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcon(l))))
            'If dum = "AJ3" Then
            '    MessageBox.Show("GENERATING QR", "GENERATING QR PLEASE WAIT!..." & dum, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If
            ''MsgBox(l)
            'If dum = cellletter Then
            '    wsxl.Range(dum).Select()
            '    Threading.Thread.Sleep(500)
            '    wsxl.PasteSpecial("Bitmap")

            '    'wsxl.Range("AK2", "AK2").PasteSpecial(Excel.XlPasteType.xlPasteAll, _
            '    'Excel.XlPasteSpecialOperation.xlPasteSpecialOperationAdd, _
            '    'False, False)

            '    'wsxl.Cells(dum, dum).PasteSpecial("Bitmap")
            'End If
            ''wsxlqr.Range(dum).Select()
            ''Threading.Thread.Sleep(500)
            ''wsxlqr.PasteSpecial("Bitmap")

            'If loc2 = 4 Then
            '    qrX += 13 + 1
            '    loc2 = 1
            'Else
            '    'qrX += 13
            '    qrX += 14
            '    loc2 += 1
            'End If
        Next
        apxl.Visible = True

        releaseObject(apxl)
        releaseObject(wbxl)
        releaseObject(wsxl)
        'releaseObject(wsxlqr)
    End Sub

    Private Sub Bgw2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Bgw2.DoWork
        Dim x1 As Integer = 2
        Dim x2 As Integer = 3

        Dim y1 As Integer = 2
        Dim y2 As Integer = 2
        Dim cntLine As Integer = 1

        Dim qrX As Integer = 2
        Dim qrY As Integer = 37
        Dim cellletter As String

        Dim Loca As Integer() = {10, 172, 337, 500, 662, 825}
        Dim excel_path As String = "\\172.16.2.41\Software Updater\List of Templates\CCI\Template\CCI FM-PP-001-129 04 03new.xls"
        Dim qr_code_bmp_array(DataGridView1.Rows.Count - 1) As Bitmap
        Dim QR_Generator As New QRCodeEncoder
        QR_Generator.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        QR_Generator.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
        QR_Generator.QRCodeVersion = 4
        QR_Generator.QRCodeScale = 2

        Dim apxl As Excel.Application
        Dim wbxl As Excel.Workbook
        Dim wsxl As Excel.Worksheet
        Dim wsxlqr As Excel.Worksheet
        Dim loc As Integer = 0
        Dim loc2 As Integer = 1

        'apxl = CreateObject("Excel.Application")
        'apxl.Visible = True
        ''wbxl = apxl.Workbooks.Open("C:\Users\chmg01\Desktop\cwp_data\W18A001-grace1")
        apxl = New Excel.Application
        wbxl = apxl.Workbooks.Open("\\172.16.2.41\Software Updater\List of Templates\CCI\Template\CCI FM-PP-001-129 04 03new.xls")
        wsxl = wbxl.Worksheets("Revise-7-5-2020")
        wsxlqr = wbxl.Worksheets("Revise-7-5-2020")

        '************************** - 1 - **************************
        wsxlqr.Activate()
        For n As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(n).Selected = True Then
                Dim dum As String
                cellletter = "I" & qrX.ToString()
                wsxl.Cells(x1, y1).Value() = DataGridView1.Rows(n).Cells(0).Value
                wsxl.Cells(x2, y2).Value() = DataGridView1.Rows(n).Cells(1).Value

                dum = cellletter
                Dim qr_code_image As Image = QR_Generator.Encode(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]129")
                Dim qrcode_bmp As New Bitmap(qr_code_image)
                BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcode_bmp)))
                If dum = "I2" Then
                    MessageBox.Show("GENERATING QR " & dum, "GENERATING QR PLEASE WAIT!...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                wsxlqr.Range(dum).Select()
                wsxlqr.PasteSpecial("Bitmap")


                If loc2 = 4 Then
                    x1 += 13 + 1
                    x2 += 13 + 1
                    qrX += 13 + 1
                    loc2 = 1
                Else
                    x1 += 14
                    x2 += 14
                    qrX += 14

                    loc2 += 1
                End If
            End If
            'Dim dum As String
            'cellletter = "I" & qrX.ToString()
            'wsxl.Cells(x1, y1).Value() = DataGridView1.Rows(n).Cells(0).Value
            'wsxl.Cells(x2, y2).Value() = DataGridView1.Rows(n).Cells(1).Value

            'dum = cellletter
            'Dim qr_code_image As Image = QR_Generator.Encode(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]129")
            'Dim qrcode_bmp As New Bitmap(qr_code_image)
            'BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcode_bmp)))
            'If dum = "I2" Then
            '    MessageBox.Show("GENERATING QR " & dum, "GENERATING QR PLEASE WAIT!...", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If
            'wsxlqr.Range(dum).Select()
            ''MsgBox(dum)
            ''Threading.Thread.Sleep(500)
            'wsxlqr.PasteSpecial("Bitmap")


            'If loc2 = 4 Then
            '    x1 += 13 + 1
            '    x2 += 13 + 1
            '    qrX += 13 + 1
            '    loc2 = 1
            'Else
            '    x1 += 14
            '    x2 += 14
            '    qrX += 14

            '    loc2 += 1
            'End If




            'If loc = 5 Then
            '    loc = 0
            'Else
            '    loc += 1
            'End If

        Next
        apxl.Visible = True

        releaseObject(apxl)
        releaseObject(wbxl)
        releaseObject(wsxl)
        releaseObject(wsxlqr)
    End Sub

    Private Sub Bgw2_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Bgw2.RunWorkerCompleted
        Pcbloading.Visible = False
    End Sub

    Private Sub Bgw1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Bgw1.RunWorkerCompleted
        Pcbloading.Visible = False
        If CKB_testing.Checked = True Then
            If Bgw2.IsBusy = True Then
            Else
                Pcbloading.Visible = True
                Bgw2.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub Bgw3_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Bgw3.DoWork
        Dim x1 As Integer = 2
        Dim x2 As Integer = 4

        Dim y1 As Integer = 26
        Dim y2 As Integer = 26
        Dim cntLine As Integer = 1

        Dim qrX As Integer = 3
        Dim qrY As Integer = 37
        Dim cellletter As String

        Dim Loca As Integer() = {10, 172, 337, 500, 662, 825}
        Dim excel_path As String = "\\172.16.2.41\Software Updater\List of Templates\CCI\Template\CCI FM-QA-045 02 00.xls"
        Dim qr_code_bmp_array(DataGridView1.Rows.Count - 1) As Bitmap
        Dim QR_Generator As New QRCodeEncoder
        QR_Generator.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        QR_Generator.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
        QR_Generator.QRCodeVersion = 3
        QR_Generator.QRCodeScale = 2

        Dim apxl As Excel.Application
        Dim wbxl As Excel.Workbook
        Dim wsxl As Excel.Worksheet
        'Dim wsxlqr As Excel.Worksheet
        Dim loc As Integer = 0
        Dim loc2 As Integer = 1

        Dim qrcon(DataGridView1.Rows.Count - 1) As Bitmap
        'apxl = CreateObject("Excel.Application")
        'apxl.Visible = True
        ''wbxl = apxl.Workbooks.Open("C:\Users\chmg01\Desktop\cwp_data\W18A001-grace1")
        apxl = New Excel.Application
        wbxl = apxl.Workbooks.Open("\\172.16.2.41\Software Updater\List of Templates\CCI\Template\CCI FM-QA-045 02 00.xls")
        wsxl = wbxl.Worksheets("rev02_5.02.2013")
        'wsxlqr = wbxl.Worksheets("Revised(3)")

        '************************** - 1 - **************************
        wsxl.Activate()
        For n As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(n).Selected = True Then
                wsxl.Cells(x1, y1).Value() = DataGridView1.Rows(n).Cells(0).Value
                wsxl.Cells(x2, y2).Value() = DataGridView1.Rows(n).Cells(1).Value

                Dim qr_code_image As Image = QR_Generator.Encode(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]045")
                Dim qrcode_bmp As New Bitmap(qr_code_image)
                qrcon(n) = qrcode_bmp

                If loc2 = 4 Then
                    x1 += 14 + 1
                    x2 += 14 + 1

                    loc2 = 1
                Else
                   
                    x1 += 14
                    x2 += 14


                    loc2 += 1
                End If
            End If
            ''Dim dum As String
            ''cellletter = "AK" & qrX.ToString()
            'wsxl.Cells(x1, y1).Value() = DataGridView1.Rows(n).Cells(0).Value
            'wsxl.Cells(x2, y2).Value() = DataGridView1.Rows(n).Cells(1).Value

            ''dum = cellletter
            'Dim qr_code_image As Image = QR_Generator.Encode(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]045")
            'Dim qrcode_bmp As New Bitmap(qr_code_image)
            'qrcon(n) = qrcode_bmp

            'If loc2 = 4 Then
            '    x1 += 14 + 1
            '    x2 += 14 + 1
            '    'qrX += 13 + 1
            '    loc2 = 1
            'Else
            '    'x1 += 13
            '    'x2 += 13
            '    x1 += 14
            '    x2 += 14
            '    'qrX += 13

            '    loc2 += 1
            'End If




            ''If loc = 5 Then
            ''    loc = 0
            ''Else
            ''    loc += 1
            ''End If

        Next

        loc2 = 1
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Selected = True Then
                Dim l As String = i
                Dim dum As String
                cellletter = "B" & qrX.ToString()
                dum = cellletter
                BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcon(l))))
                If dum = "B4" Then
                    MessageBox.Show("GENERATING QR", "GENERATING QR PLEASE WAIT!..." & dum, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                If dum = cellletter Then
                    wsxl.Range(dum).Select()
                    Threading.Thread.Sleep(100)
                    wsxl.PasteSpecial("Bitmap")
                End If

                If loc2 = 4 Then
                    qrX += 14 + 1
                    loc2 = 1
                Else
                    qrX += 14
                    loc2 += 1
                End If
            End If
            'Dim l As String = i
            'Dim dum As String
            'cellletter = "B" & qrX.ToString()
            'dum = cellletter
            'BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcon(l))))
            'If dum = "B3" Then
            '    MessageBox.Show("GENERATING QR", "GENERATING QR PLEASE WAIT!..." & dum, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If
            ''MsgBox(l)
            'If dum = cellletter Then
            '    wsxl.Range(dum).Select()
            '    Threading.Thread.Sleep(100)
            '    wsxl.PasteSpecial("Bitmap")
            'End If

            'If loc2 = 4 Then
            '    qrX += 14 + 1
            '    loc2 = 1
            'Else
            '    'qrX += 13
            '    qrX += 14
            '    loc2 += 1
            'End If
        Next
        apxl.Visible = True

        releaseObject(apxl)
        releaseObject(wbxl)
        releaseObject(wsxl)
    End Sub

    Private Sub Bgw3_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Bgw3.RunWorkerCompleted
        Pcbloading.Visible = False
        If CKB_testing.Checked = True Then
            If Bgw4.IsBusy Then
            Else
                Pcbloading.Visible = True
                Bgw4.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub Btn_potraveller_Click(sender As System.Object, e As System.EventArgs) Handles Btn_potraveller.Click
        Me.Hide()
        PC_Generate_Labels.iniatilize()
        PC_Generate_Labels.Show()
    End Sub

    Private Sub Btn_worktraveller_Click(sender As System.Object, e As System.EventArgs) Handles Btn_worktraveller.Click

    End Sub

 
    Private Sub Bgw4_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Bgw4.DoWork
        Dim x1 As Integer = 1
        Dim x2 As Integer = 3

        Dim y1 As Integer = 2
        Dim y2 As Integer = 2
        Dim cntLine As Integer = 1

        Dim qrX As Integer = 2
        Dim qrY As Integer = 37
        Dim cellletter As String

        Dim Loca As Integer() = {10, 172, 337, 500, 662, 825}
        Dim excel_path As String = "\\172.16.2.41\Software Updater\List of Templates\CCI\Template\QA_QC1 FM-PP-001-539 02 QC Testing Traveler.xlsx"
        Dim qr_code_bmp_array(DataGridView1.Rows.Count - 1) As Bitmap
        Dim QR_Generator As New QRCodeEncoder
        QR_Generator.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        QR_Generator.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
        QR_Generator.QRCodeVersion = 3
        QR_Generator.QRCodeScale = 2

        Dim apxl As Excel.Application
        Dim wbxl As Excel.Workbook
        Dim wsxl As Excel.Worksheet
        Dim loc As Integer = 0
        Dim loc2 As Integer = 1

        Dim qrcon(DataGridView1.Rows.Count - 1) As Bitmap
        apxl = New Excel.Application
        wbxl = apxl.Workbooks.Open("\\172.16.2.41\Software Updater\List of Templates\CCI\Template\QA_QC1 FM-PP-001-539 02 QC Testing Traveler.xlsx")
        wsxl = wbxl.Worksheets("CUS Traveler(3)")

        '************************** - 1 - **************************
        wsxl.Activate()
        For n As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(n).Selected = True Then
                wsxl.Cells(x1, y1).Value() = DataGridView1.Rows(n).Cells(0).Value
                wsxl.Cells(x2, y2).Value() = DataGridView1.Rows(n).Cells(1).Value

                Dim qr_code_image As Image = QR_Generator.Encode(DataGridView1.Rows(n).Cells(0).Value & "[" & DataGridView1.Rows(n).Cells(1).Value & "]539")
                Dim qrcode_bmp As New Bitmap(qr_code_image)
                qrcon(n) = qrcode_bmp

                If loc2 = 3 Then
                    x1 += 18
                    x2 += 18

                    loc2 = 1
                Else

                    x1 += 19
                    x2 += 19


                    loc2 += 1
                End If
            End If

        Next

        loc2 = 1
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Selected = True Then
                Dim l As String = i
                Dim dum As String
                cellletter = "G" & qrX.ToString()
                dum = cellletter
                BeginInvoke(New Action(Sub() Clipboard.SetDataObject(qrcon(l))))
                If dum = "G2" Then
                    MessageBox.Show("GENERATING QR", "GENERATING QR PLEASE WAIT!..." & dum, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                If dum = cellletter Then
                    wsxl.Range(dum).Select()
                    Threading.Thread.Sleep(100)
                    wsxl.PasteSpecial("Bitmap")
                End If

                If loc2 = 3 Then
                    qrX += 18
                    loc2 = 1
                Else
                    qrX += 19
                    loc2 += 1
                End If
            End If
        Next
        apxl.Visible = True

        releaseObject(apxl)
        releaseObject(wbxl)
        releaseObject(wsxl)
    End Sub

    Private Sub Bgw4_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Bgw4.RunWorkerCompleted
        Pcbloading.Visible = False
    End Sub
End Class
