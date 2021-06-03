<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class comparelabel
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(comparelabel))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Dgv = New System.Windows.Forms.DataGridView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Pcbqc_mismatch = New System.Windows.Forms.PictureBox()
        Me.Pcbqc_match = New System.Windows.Forms.PictureBox()
        Me.Lblqc_mismatch = New System.Windows.Forms.Label()
        Me.Lblqc_match = New System.Windows.Forms.Label()
        Me.Lblqc_result = New System.Windows.Forms.Label()
        Me.Pcbqc_result = New System.Windows.Forms.PictureBox()
        Me.Lblqc_lnshipdateqr = New System.Windows.Forms.Label()
        Me.Lblqc_pnshipdateqr = New System.Windows.Forms.Label()
        Me.Lblqc_poqrcodeln = New System.Windows.Forms.Label()
        Me.Lblqc_poqrcodepn = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Txtbx_qcpnshipdate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txtbx_qcpoqrcode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Lbl_pnqrlotno = New System.Windows.Forms.Label()
        Me.Lbl_pnqrcode = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Txtbx_pnshipdateqr = New System.Windows.Forms.TextBox()
        Me.Lbl_res = New System.Windows.Forms.Label()
        Me.Lbl_mismatch = New System.Windows.Forms.Label()
        Me.Lbl_match = New System.Windows.Forms.Label()
        Me.Pcb_mismatch = New System.Windows.Forms.PictureBox()
        Me.Pcb_match = New System.Windows.Forms.PictureBox()
        Me.Lblprod_lotno = New System.Windows.Forms.Label()
        Me.lblprod_pn = New System.Windows.Forms.Label()
        Me.Lblqc_lotno = New System.Windows.Forms.Label()
        Me.Lblqc_pn = New System.Windows.Forms.Label()
        Me.Lblpo_lotno = New System.Windows.Forms.Label()
        Me.lblpo_pn = New System.Windows.Forms.Label()
        Me.Pcb_qmark = New System.Windows.Forms.PictureBox()
        Me.Btn_confirm = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txtbx_qcworktraveller = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txtbx_prodworkqrcode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txtbx_potraveller = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Bgw1 = New System.ComponentModel.BackgroundWorker()
        Me.Pcbloading = New System.Windows.Forms.PictureBox()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.Dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pcbqc_mismatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pcbqc_match, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pcbqc_result, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pcb_mismatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pcb_match, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pcb_qmark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pcbloading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Pcbloading)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Dgv)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Pcbqc_mismatch)
        Me.Panel1.Controls.Add(Me.Pcbqc_match)
        Me.Panel1.Controls.Add(Me.Lblqc_mismatch)
        Me.Panel1.Controls.Add(Me.Lblqc_match)
        Me.Panel1.Controls.Add(Me.Lblqc_result)
        Me.Panel1.Controls.Add(Me.Pcbqc_result)
        Me.Panel1.Controls.Add(Me.Lblqc_lnshipdateqr)
        Me.Panel1.Controls.Add(Me.Lblqc_pnshipdateqr)
        Me.Panel1.Controls.Add(Me.Lblqc_poqrcodeln)
        Me.Panel1.Controls.Add(Me.Lblqc_poqrcodepn)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Txtbx_qcpnshipdate)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Txtbx_qcpoqrcode)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Lbl_pnqrlotno)
        Me.Panel1.Controls.Add(Me.Lbl_pnqrcode)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Txtbx_pnshipdateqr)
        Me.Panel1.Controls.Add(Me.Lbl_res)
        Me.Panel1.Controls.Add(Me.Lbl_mismatch)
        Me.Panel1.Controls.Add(Me.Lbl_match)
        Me.Panel1.Controls.Add(Me.Pcb_mismatch)
        Me.Panel1.Controls.Add(Me.Pcb_match)
        Me.Panel1.Controls.Add(Me.Lblprod_lotno)
        Me.Panel1.Controls.Add(Me.lblprod_pn)
        Me.Panel1.Controls.Add(Me.Lblqc_lotno)
        Me.Panel1.Controls.Add(Me.Lblqc_pn)
        Me.Panel1.Controls.Add(Me.Lblpo_lotno)
        Me.Panel1.Controls.Add(Me.lblpo_pn)
        Me.Panel1.Controls.Add(Me.Pcb_qmark)
        Me.Panel1.Controls.Add(Me.Btn_confirm)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Txtbx_qcworktraveller)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Txtbx_prodworkqrcode)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Txtbx_potraveller)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(957, 571)
        Me.Panel1.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(627, 55)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(166, 13)
        Me.Label13.TabIndex = 43
        Me.Label13.Text = "Already Scanned and Match"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.DarkOrange
        Me.Label12.Location = New System.Drawing.Point(561, 50)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 23)
        Me.Label12.TabIndex = 42
        '
        'Dgv
        '
        Me.Dgv.AllowUserToAddRows = False
        Me.Dgv.AllowUserToDeleteRows = False
        Me.Dgv.BackgroundColor = System.Drawing.Color.White
        Me.Dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column3, Me.Column1, Me.Column2, Me.Column4, Me.Column5})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Dgv.DefaultCellStyle = DataGridViewCellStyle1
        Me.Dgv.Location = New System.Drawing.Point(560, 80)
        Me.Dgv.Name = "Dgv"
        Me.Dgv.ReadOnly = True
        Me.Dgv.RowHeadersVisible = False
        Me.Dgv.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White
        Me.Dgv.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.Dgv.RowTemplate.ReadOnly = True
        Me.Dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Dgv.Size = New System.Drawing.Size(383, 471)
        Me.Dgv.TabIndex = 41
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 306)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 13)
        Me.Label11.TabIndex = 40
        Me.Label11.Text = "QC"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 43)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(41, 13)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "PROD"
        '
        'Pcbqc_mismatch
        '
        Me.Pcbqc_mismatch.Image = Global.Traveller_Tray_Labels.My.Resources.Resources._error
        Me.Pcbqc_mismatch.Location = New System.Drawing.Point(224, 386)
        Me.Pcbqc_mismatch.Name = "Pcbqc_mismatch"
        Me.Pcbqc_mismatch.Size = New System.Drawing.Size(107, 76)
        Me.Pcbqc_mismatch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pcbqc_mismatch.TabIndex = 38
        Me.Pcbqc_mismatch.TabStop = False
        Me.Pcbqc_mismatch.Visible = False
        '
        'Pcbqc_match
        '
        Me.Pcbqc_match.Image = Global.Traveller_Tray_Labels.My.Resources.Resources.oklogo
        Me.Pcbqc_match.Location = New System.Drawing.Point(224, 386)
        Me.Pcbqc_match.Name = "Pcbqc_match"
        Me.Pcbqc_match.Size = New System.Drawing.Size(107, 76)
        Me.Pcbqc_match.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pcbqc_match.TabIndex = 37
        Me.Pcbqc_match.TabStop = False
        Me.Pcbqc_match.Visible = False
        '
        'Lblqc_mismatch
        '
        Me.Lblqc_mismatch.AutoSize = True
        Me.Lblqc_mismatch.Location = New System.Drawing.Point(239, 367)
        Me.Lblqc_mismatch.Name = "Lblqc_mismatch"
        Me.Lblqc_mismatch.Size = New System.Drawing.Size(69, 13)
        Me.Lblqc_mismatch.TabIndex = 36
        Me.Lblqc_mismatch.Text = "MISMATCH"
        Me.Lblqc_mismatch.Visible = False
        '
        'Lblqc_match
        '
        Me.Lblqc_match.AutoSize = True
        Me.Lblqc_match.Location = New System.Drawing.Point(250, 367)
        Me.Lblqc_match.Name = "Lblqc_match"
        Me.Lblqc_match.Size = New System.Drawing.Size(47, 13)
        Me.Lblqc_match.TabIndex = 35
        Me.Lblqc_match.Text = "MATCH"
        Me.Lblqc_match.Visible = False
        '
        'Lblqc_result
        '
        Me.Lblqc_result.AutoSize = True
        Me.Lblqc_result.Location = New System.Drawing.Point(248, 367)
        Me.Lblqc_result.Name = "Lblqc_result"
        Me.Lblqc_result.Size = New System.Drawing.Size(50, 13)
        Me.Lblqc_result.TabIndex = 34
        Me.Lblqc_result.Text = "RESULT"
        '
        'Pcbqc_result
        '
        Me.Pcbqc_result.Image = Global.Traveller_Tray_Labels.My.Resources.Resources.questionmark
        Me.Pcbqc_result.Location = New System.Drawing.Point(224, 386)
        Me.Pcbqc_result.Name = "Pcbqc_result"
        Me.Pcbqc_result.Size = New System.Drawing.Size(107, 76)
        Me.Pcbqc_result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pcbqc_result.TabIndex = 33
        Me.Pcbqc_result.TabStop = False
        '
        'Lblqc_lnshipdateqr
        '
        Me.Lblqc_lnshipdateqr.AutoSize = True
        Me.Lblqc_lnshipdateqr.Location = New System.Drawing.Point(16, 543)
        Me.Lblqc_lnshipdateqr.Name = "Lblqc_lnshipdateqr"
        Me.Lblqc_lnshipdateqr.Size = New System.Drawing.Size(55, 13)
        Me.Lblqc_lnshipdateqr.TabIndex = 32
        Me.Lblqc_lnshipdateqr.Text = "LOT NO:"
        Me.Lblqc_lnshipdateqr.Visible = False
        '
        'Lblqc_pnshipdateqr
        '
        Me.Lblqc_pnshipdateqr.AutoSize = True
        Me.Lblqc_pnshipdateqr.Location = New System.Drawing.Point(15, 524)
        Me.Lblqc_pnshipdateqr.Name = "Lblqc_pnshipdateqr"
        Me.Lblqc_pnshipdateqr.Size = New System.Drawing.Size(27, 13)
        Me.Lblqc_pnshipdateqr.TabIndex = 31
        Me.Lblqc_pnshipdateqr.Text = "PN:"
        '
        'Lblqc_poqrcodeln
        '
        Me.Lblqc_poqrcodeln.AutoSize = True
        Me.Lblqc_poqrcodeln.Location = New System.Drawing.Point(12, 420)
        Me.Lblqc_poqrcodeln.Name = "Lblqc_poqrcodeln"
        Me.Lblqc_poqrcodeln.Size = New System.Drawing.Size(55, 13)
        Me.Lblqc_poqrcodeln.TabIndex = 30
        Me.Lblqc_poqrcodeln.Text = "LOT NO:"
        '
        'Lblqc_poqrcodepn
        '
        Me.Lblqc_poqrcodepn.AutoSize = True
        Me.Lblqc_poqrcodepn.Location = New System.Drawing.Point(12, 401)
        Me.Lblqc_poqrcodepn.Name = "Lblqc_poqrcodepn"
        Me.Lblqc_poqrcodepn.Size = New System.Drawing.Size(27, 13)
        Me.Lblqc_poqrcodepn.TabIndex = 29
        Me.Lblqc_poqrcodepn.Text = "PN:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 446)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(172, 13)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "QR CODE PN AND SHIPDATE"
        '
        'Txtbx_qcpnshipdate
        '
        Me.Txtbx_qcpnshipdate.Location = New System.Drawing.Point(15, 464)
        Me.Txtbx_qcpnshipdate.Multiline = True
        Me.Txtbx_qcpnshipdate.Name = "Txtbx_qcpnshipdate"
        Me.Txtbx_qcpnshipdate.Size = New System.Drawing.Size(197, 52)
        Me.Txtbx_qcpnshipdate.TabIndex = 27
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 327)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "PO QR CODE"
        '
        'Txtbx_qcpoqrcode
        '
        Me.Txtbx_qcpoqrcode.Location = New System.Drawing.Point(12, 343)
        Me.Txtbx_qcpoqrcode.Multiline = True
        Me.Txtbx_qcpoqrcode.Name = "Txtbx_qcpoqrcode"
        Me.Txtbx_qcpoqrcode.Size = New System.Drawing.Size(197, 52)
        Me.Txtbx_qcpoqrcode.TabIndex = 25
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(3, 298)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(394, 5)
        Me.Label7.TabIndex = 24
        '
        'Lbl_pnqrlotno
        '
        Me.Lbl_pnqrlotno.AutoSize = True
        Me.Lbl_pnqrlotno.Location = New System.Drawing.Point(12, 278)
        Me.Lbl_pnqrlotno.Name = "Lbl_pnqrlotno"
        Me.Lbl_pnqrlotno.Size = New System.Drawing.Size(55, 13)
        Me.Lbl_pnqrlotno.TabIndex = 23
        Me.Lbl_pnqrlotno.Text = "LOT NO:"
        Me.Lbl_pnqrlotno.Visible = False
        '
        'Lbl_pnqrcode
        '
        Me.Lbl_pnqrcode.AutoSize = True
        Me.Lbl_pnqrcode.Location = New System.Drawing.Point(12, 257)
        Me.Lbl_pnqrcode.Name = "Lbl_pnqrcode"
        Me.Lbl_pnqrcode.Size = New System.Drawing.Size(27, 13)
        Me.Lbl_pnqrcode.TabIndex = 22
        Me.Lbl_pnqrcode.Text = "PN:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 185)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(172, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "QR CODE PN AND SHIPDATE"
        '
        'Txtbx_pnshipdateqr
        '
        Me.Txtbx_pnshipdateqr.Location = New System.Drawing.Point(15, 201)
        Me.Txtbx_pnshipdateqr.Multiline = True
        Me.Txtbx_pnshipdateqr.Name = "Txtbx_pnshipdateqr"
        Me.Txtbx_pnshipdateqr.Size = New System.Drawing.Size(197, 52)
        Me.Txtbx_pnshipdateqr.TabIndex = 20
        '
        'Lbl_res
        '
        Me.Lbl_res.AutoSize = True
        Me.Lbl_res.Location = New System.Drawing.Point(252, 105)
        Me.Lbl_res.Name = "Lbl_res"
        Me.Lbl_res.Size = New System.Drawing.Size(50, 13)
        Me.Lbl_res.TabIndex = 19
        Me.Lbl_res.Text = "RESULT"
        '
        'Lbl_mismatch
        '
        Me.Lbl_mismatch.AutoSize = True
        Me.Lbl_mismatch.Location = New System.Drawing.Point(243, 105)
        Me.Lbl_mismatch.Name = "Lbl_mismatch"
        Me.Lbl_mismatch.Size = New System.Drawing.Size(69, 13)
        Me.Lbl_mismatch.TabIndex = 18
        Me.Lbl_mismatch.Text = "MISMATCH"
        Me.Lbl_mismatch.Visible = False
        '
        'Lbl_match
        '
        Me.Lbl_match.AutoSize = True
        Me.Lbl_match.Location = New System.Drawing.Point(254, 105)
        Me.Lbl_match.Name = "Lbl_match"
        Me.Lbl_match.Size = New System.Drawing.Size(47, 13)
        Me.Lbl_match.TabIndex = 17
        Me.Lbl_match.Text = "MATCH"
        Me.Lbl_match.Visible = False
        '
        'Pcb_mismatch
        '
        Me.Pcb_mismatch.Image = Global.Traveller_Tray_Labels.My.Resources.Resources._error
        Me.Pcb_mismatch.Location = New System.Drawing.Point(226, 129)
        Me.Pcb_mismatch.Name = "Pcb_mismatch"
        Me.Pcb_mismatch.Size = New System.Drawing.Size(107, 76)
        Me.Pcb_mismatch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pcb_mismatch.TabIndex = 16
        Me.Pcb_mismatch.TabStop = False
        Me.Pcb_mismatch.Visible = False
        '
        'Pcb_match
        '
        Me.Pcb_match.Image = Global.Traveller_Tray_Labels.My.Resources.Resources.oklogo
        Me.Pcb_match.Location = New System.Drawing.Point(226, 129)
        Me.Pcb_match.Name = "Pcb_match"
        Me.Pcb_match.Size = New System.Drawing.Size(107, 76)
        Me.Pcb_match.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pcb_match.TabIndex = 15
        Me.Pcb_match.TabStop = False
        Me.Pcb_match.Visible = False
        '
        'Lblprod_lotno
        '
        Me.Lblprod_lotno.AutoSize = True
        Me.Lblprod_lotno.Location = New System.Drawing.Point(352, 222)
        Me.Lblprod_lotno.Name = "Lblprod_lotno"
        Me.Lblprod_lotno.Size = New System.Drawing.Size(55, 13)
        Me.Lblprod_lotno.TabIndex = 14
        Me.Lblprod_lotno.Text = "LOT NO:"
        '
        'lblprod_pn
        '
        Me.lblprod_pn.AutoSize = True
        Me.lblprod_pn.Location = New System.Drawing.Point(352, 199)
        Me.lblprod_pn.Name = "lblprod_pn"
        Me.lblprod_pn.Size = New System.Drawing.Size(27, 13)
        Me.lblprod_pn.TabIndex = 13
        Me.lblprod_pn.Text = "PN:"
        '
        'Lblqc_lotno
        '
        Me.Lblqc_lotno.AutoSize = True
        Me.Lblqc_lotno.Location = New System.Drawing.Point(351, 470)
        Me.Lblqc_lotno.Name = "Lblqc_lotno"
        Me.Lblqc_lotno.Size = New System.Drawing.Size(55, 13)
        Me.Lblqc_lotno.TabIndex = 12
        Me.Lblqc_lotno.Text = "LOT NO:"
        '
        'Lblqc_pn
        '
        Me.Lblqc_pn.AutoSize = True
        Me.Lblqc_pn.Location = New System.Drawing.Point(351, 449)
        Me.Lblqc_pn.Name = "Lblqc_pn"
        Me.Lblqc_pn.Size = New System.Drawing.Size(27, 13)
        Me.Lblqc_pn.TabIndex = 11
        Me.Lblqc_pn.Text = "PN:"
        '
        'Lblpo_lotno
        '
        Me.Lblpo_lotno.AutoSize = True
        Me.Lblpo_lotno.Location = New System.Drawing.Point(12, 159)
        Me.Lblpo_lotno.Name = "Lblpo_lotno"
        Me.Lblpo_lotno.Size = New System.Drawing.Size(55, 13)
        Me.Lblpo_lotno.TabIndex = 10
        Me.Lblpo_lotno.Text = "LOT NO:"
        '
        'lblpo_pn
        '
        Me.lblpo_pn.AutoSize = True
        Me.lblpo_pn.Location = New System.Drawing.Point(12, 138)
        Me.lblpo_pn.Name = "lblpo_pn"
        Me.lblpo_pn.Size = New System.Drawing.Size(27, 13)
        Me.lblpo_pn.TabIndex = 9
        Me.lblpo_pn.Text = "PN:"
        '
        'Pcb_qmark
        '
        Me.Pcb_qmark.Image = Global.Traveller_Tray_Labels.My.Resources.Resources.questionmark
        Me.Pcb_qmark.Location = New System.Drawing.Point(226, 129)
        Me.Pcb_qmark.Name = "Pcb_qmark"
        Me.Pcb_qmark.Size = New System.Drawing.Size(107, 76)
        Me.Pcb_qmark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pcb_qmark.TabIndex = 8
        Me.Pcb_qmark.TabStop = False
        '
        'Btn_confirm
        '
        Me.Btn_confirm.BackColor = System.Drawing.Color.Green
        Me.Btn_confirm.Enabled = False
        Me.Btn_confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_confirm.ForeColor = System.Drawing.Color.White
        Me.Btn_confirm.Location = New System.Drawing.Point(408, 275)
        Me.Btn_confirm.Name = "Btn_confirm"
        Me.Btn_confirm.Size = New System.Drawing.Size(140, 51)
        Me.Btn_confirm.TabIndex = 7
        Me.Btn_confirm.Text = "CONFIRM"
        Me.Btn_confirm.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(347, 378)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(193, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "QR CODE QC WORK TRAVELLER"
        '
        'Txtbx_qcworktraveller
        '
        Me.Txtbx_qcworktraveller.Location = New System.Drawing.Point(350, 394)
        Me.Txtbx_qcworktraveller.Multiline = True
        Me.Txtbx_qcworktraveller.Name = "Txtbx_qcworktraveller"
        Me.Txtbx_qcworktraveller.Size = New System.Drawing.Size(197, 52)
        Me.Txtbx_qcworktraveller.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(348, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(208, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "QR CODE PROD WORK TRAVELLER"
        '
        'Txtbx_prodworkqrcode
        '
        Me.Txtbx_prodworkqrcode.Location = New System.Drawing.Point(351, 143)
        Me.Txtbx_prodworkqrcode.Multiline = True
        Me.Txtbx_prodworkqrcode.Name = "Txtbx_prodworkqrcode"
        Me.Txtbx_prodworkqrcode.Size = New System.Drawing.Size(197, 52)
        Me.Txtbx_prodworkqrcode.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "PO QR CODE"
        '
        'Txtbx_potraveller
        '
        Me.Txtbx_potraveller.Location = New System.Drawing.Point(12, 82)
        Me.Txtbx_potraveller.Multiline = True
        Me.Txtbx_potraveller.Name = "Txtbx_potraveller"
        Me.Txtbx_potraveller.Size = New System.Drawing.Size(197, 52)
        Me.Txtbx_potraveller.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(-3, 561)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(964, 10)
        Me.Label2.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(-3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(964, 40)
        Me.Label1.TabIndex = 0
        '
        'Bgw1
        '
        '
        'Pcbloading
        '
        Me.Pcbloading.Image = Global.Traveller_Tray_Labels.My.Resources.Resources.spinnervlll
        Me.Pcbloading.Location = New System.Drawing.Point(560, 76)
        Me.Pcbloading.Name = "Pcbloading"
        Me.Pcbloading.Size = New System.Drawing.Size(383, 473)
        Me.Pcbloading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pcbloading.TabIndex = 44
        Me.Pcbloading.TabStop = False
        Me.Pcbloading.Visible = False
        '
        'Column3
        '
        Me.Column3.HeaderText = "LOT NO"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Shipdate"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "PO NO"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "MATCH"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "SCANNED COUNT"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Visible = False
        '
        'comparelabel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(957, 571)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "comparelabel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "comparelabel"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Dgv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pcbqc_mismatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pcbqc_match, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pcbqc_result, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pcb_mismatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pcb_match, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pcb_qmark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pcbloading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Pcb_qmark As System.Windows.Forms.PictureBox
    Friend WithEvents Btn_confirm As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Txtbx_qcworktraveller As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Txtbx_prodworkqrcode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Txtbx_potraveller As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Pcb_match As System.Windows.Forms.PictureBox
    Friend WithEvents Lblprod_lotno As System.Windows.Forms.Label
    Friend WithEvents lblprod_pn As System.Windows.Forms.Label
    Friend WithEvents Lblqc_lotno As System.Windows.Forms.Label
    Friend WithEvents Lblqc_pn As System.Windows.Forms.Label
    Friend WithEvents Lblpo_lotno As System.Windows.Forms.Label
    Friend WithEvents lblpo_pn As System.Windows.Forms.Label
    Friend WithEvents Pcb_mismatch As System.Windows.Forms.PictureBox
    Friend WithEvents Lbl_res As System.Windows.Forms.Label
    Friend WithEvents Lbl_mismatch As System.Windows.Forms.Label
    Friend WithEvents Lbl_match As System.Windows.Forms.Label
    Friend WithEvents Lbl_pnqrlotno As System.Windows.Forms.Label
    Friend WithEvents Lbl_pnqrcode As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Txtbx_pnshipdateqr As System.Windows.Forms.TextBox
    Friend WithEvents Pcbqc_mismatch As System.Windows.Forms.PictureBox
    Friend WithEvents Pcbqc_match As System.Windows.Forms.PictureBox
    Friend WithEvents Lblqc_mismatch As System.Windows.Forms.Label
    Friend WithEvents Lblqc_match As System.Windows.Forms.Label
    Friend WithEvents Lblqc_result As System.Windows.Forms.Label
    Friend WithEvents Pcbqc_result As System.Windows.Forms.PictureBox
    Friend WithEvents Lblqc_lnshipdateqr As System.Windows.Forms.Label
    Friend WithEvents Lblqc_pnshipdateqr As System.Windows.Forms.Label
    Friend WithEvents Lblqc_poqrcodeln As System.Windows.Forms.Label
    Friend WithEvents Lblqc_poqrcodepn As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Txtbx_qcpnshipdate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Txtbx_qcpoqrcode As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Dgv As System.Windows.Forms.DataGridView
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Bgw1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Pcbloading As System.Windows.Forms.PictureBox
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
