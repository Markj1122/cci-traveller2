<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WorkTraveller
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WorkTraveller))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CKB_visual = New System.Windows.Forms.CheckBox()
        Me.CKB_testing = New System.Windows.Forms.CheckBox()
        Me.Gen_Btn = New System.Windows.Forms.Button()
        Me.Setting_GrpBx = New System.Windows.Forms.GroupBox()
        Me.Auto_RdBtn = New System.Windows.Forms.RadioButton()
        Me.Manual_RdBtn = New System.Windows.Forms.RadioButton()
        Me.Container_Pnl = New System.Windows.Forms.Panel()
        Me.Pcbloading = New System.Windows.Forms.PictureBox()
        Me.Lot_Number_TxtBx = New System.Windows.Forms.TextBox()
        Me.Ln_Search_PicBx = New System.Windows.Forms.PictureBox()
        Me.Generate_GrpBx = New System.Windows.Forms.GroupBox()
        Me.Btn_potraveller = New System.Windows.Forms.Button()
        Me.Btn_worktraveller = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bgw1 = New System.ComponentModel.BackgroundWorker()
        Me.Bgw2 = New System.ComponentModel.BackgroundWorker()
        Me.Bgw3 = New System.ComponentModel.BackgroundWorker()
        Me.Bgw4 = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        Me.Setting_GrpBx.SuspendLayout()
        Me.Container_Pnl.SuspendLayout()
        CType(Me.Pcbloading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ln_Search_PicBx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Generate_GrpBx.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.CKB_visual)
        Me.Panel1.Controls.Add(Me.CKB_testing)
        Me.Panel1.Controls.Add(Me.Gen_Btn)
        Me.Panel1.Controls.Add(Me.Setting_GrpBx)
        Me.Panel1.Controls.Add(Me.Container_Pnl)
        Me.Panel1.Controls.Add(Me.Lot_Number_TxtBx)
        Me.Panel1.Controls.Add(Me.Ln_Search_PicBx)
        Me.Panel1.Controls.Add(Me.Generate_GrpBx)
        Me.Panel1.Controls.Add(Me.DataGridView1)
        Me.Panel1.Controls.Add(Me.DataGridView2)
        Me.Panel1.Location = New System.Drawing.Point(-4, -5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1020, 547)
        Me.Panel1.TabIndex = 7
        '
        'CKB_visual
        '
        Me.CKB_visual.AutoSize = True
        Me.CKB_visual.Location = New System.Drawing.Point(86, 24)
        Me.CKB_visual.Name = "CKB_visual"
        Me.CKB_visual.Size = New System.Drawing.Size(54, 17)
        Me.CKB_visual.TabIndex = 200
        Me.CKB_visual.Text = "Visual"
        Me.CKB_visual.UseVisualStyleBackColor = True
        '
        'CKB_testing
        '
        Me.CKB_testing.AutoSize = True
        Me.CKB_testing.Location = New System.Drawing.Point(19, 24)
        Me.CKB_testing.Name = "CKB_testing"
        Me.CKB_testing.Size = New System.Drawing.Size(61, 17)
        Me.CKB_testing.TabIndex = 199
        Me.CKB_testing.Text = "Testing"
        Me.CKB_testing.UseVisualStyleBackColor = True
        '
        'Gen_Btn
        '
        Me.Gen_Btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Gen_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Gen_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Gen_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Gen_Btn.ForeColor = System.Drawing.Color.White
        Me.Gen_Btn.Location = New System.Drawing.Point(292, 52)
        Me.Gen_Btn.Name = "Gen_Btn"
        Me.Gen_Btn.Size = New System.Drawing.Size(84, 31)
        Me.Gen_Btn.TabIndex = 193
        Me.Gen_Btn.Text = "Generate"
        Me.Gen_Btn.UseVisualStyleBackColor = False
        '
        'Setting_GrpBx
        '
        Me.Setting_GrpBx.Controls.Add(Me.Auto_RdBtn)
        Me.Setting_GrpBx.Controls.Add(Me.Manual_RdBtn)
        Me.Setting_GrpBx.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Setting_GrpBx.Location = New System.Drawing.Point(405, 41)
        Me.Setting_GrpBx.Name = "Setting_GrpBx"
        Me.Setting_GrpBx.Size = New System.Drawing.Size(156, 55)
        Me.Setting_GrpBx.TabIndex = 191
        Me.Setting_GrpBx.TabStop = False
        Me.Setting_GrpBx.Text = "Setting"
        Me.Setting_GrpBx.Visible = False
        '
        'Auto_RdBtn
        '
        Me.Auto_RdBtn.AutoSize = True
        Me.Auto_RdBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Auto_RdBtn.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Auto_RdBtn.Location = New System.Drawing.Point(6, 24)
        Me.Auto_RdBtn.Name = "Auto_RdBtn"
        Me.Auto_RdBtn.Size = New System.Drawing.Size(53, 21)
        Me.Auto_RdBtn.TabIndex = 186
        Me.Auto_RdBtn.TabStop = True
        Me.Auto_RdBtn.Text = "Auto"
        Me.Auto_RdBtn.UseVisualStyleBackColor = True
        '
        'Manual_RdBtn
        '
        Me.Manual_RdBtn.AutoSize = True
        Me.Manual_RdBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Manual_RdBtn.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Manual_RdBtn.Location = New System.Drawing.Point(81, 24)
        Me.Manual_RdBtn.Name = "Manual_RdBtn"
        Me.Manual_RdBtn.Size = New System.Drawing.Size(69, 21)
        Me.Manual_RdBtn.TabIndex = 187
        Me.Manual_RdBtn.TabStop = True
        Me.Manual_RdBtn.Text = "Manual"
        Me.Manual_RdBtn.UseVisualStyleBackColor = True
        '
        'Container_Pnl
        '
        Me.Container_Pnl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Container_Pnl.AutoScroll = True
        Me.Container_Pnl.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Container_Pnl.Controls.Add(Me.Pcbloading)
        Me.Container_Pnl.Font = New System.Drawing.Font("Segoe UI Semibold", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Container_Pnl.Location = New System.Drawing.Point(292, 108)
        Me.Container_Pnl.Name = "Container_Pnl"
        Me.Container_Pnl.Size = New System.Drawing.Size(707, 419)
        Me.Container_Pnl.TabIndex = 183
        '
        'Pcbloading
        '
        Me.Pcbloading.Image = Global.Traveller_Tray_Labels.My.Resources.Resources.spinnervlll
        Me.Pcbloading.Location = New System.Drawing.Point(0, -6)
        Me.Pcbloading.Name = "Pcbloading"
        Me.Pcbloading.Size = New System.Drawing.Size(707, 428)
        Me.Pcbloading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pcbloading.TabIndex = 0
        Me.Pcbloading.TabStop = False
        Me.Pcbloading.Visible = False
        '
        'Lot_Number_TxtBx
        '
        Me.Lot_Number_TxtBx.BackColor = System.Drawing.Color.White
        Me.Lot_Number_TxtBx.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Lot_Number_TxtBx.Location = New System.Drawing.Point(21, 56)
        Me.Lot_Number_TxtBx.Name = "Lot_Number_TxtBx"
        Me.Lot_Number_TxtBx.Size = New System.Drawing.Size(219, 25)
        Me.Lot_Number_TxtBx.TabIndex = 177
        '
        'Ln_Search_PicBx
        '
        Me.Ln_Search_PicBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Ln_Search_PicBx.Image = CType(resources.GetObject("Ln_Search_PicBx.Image"), System.Drawing.Image)
        Me.Ln_Search_PicBx.Location = New System.Drawing.Point(246, 57)
        Me.Ln_Search_PicBx.Name = "Ln_Search_PicBx"
        Me.Ln_Search_PicBx.Size = New System.Drawing.Size(24, 24)
        Me.Ln_Search_PicBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Ln_Search_PicBx.TabIndex = 182
        Me.Ln_Search_PicBx.TabStop = False
        '
        'Generate_GrpBx
        '
        Me.Generate_GrpBx.Controls.Add(Me.Btn_potraveller)
        Me.Generate_GrpBx.Controls.Add(Me.Btn_worktraveller)
        Me.Generate_GrpBx.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Generate_GrpBx.Location = New System.Drawing.Point(708, 34)
        Me.Generate_GrpBx.Name = "Generate_GrpBx"
        Me.Generate_GrpBx.Size = New System.Drawing.Size(260, 62)
        Me.Generate_GrpBx.TabIndex = 190
        Me.Generate_GrpBx.TabStop = False
        Me.Generate_GrpBx.Text = "Generate"
        '
        'Btn_potraveller
        '
        Me.Btn_potraveller.BackColor = System.Drawing.Color.Teal
        Me.Btn_potraveller.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_potraveller.ForeColor = System.Drawing.Color.White
        Me.Btn_potraveller.Location = New System.Drawing.Point(4, 22)
        Me.Btn_potraveller.Name = "Btn_potraveller"
        Me.Btn_potraveller.Size = New System.Drawing.Size(125, 35)
        Me.Btn_potraveller.TabIndex = 201
        Me.Btn_potraveller.Text = "PO Traveller"
        Me.Btn_potraveller.UseVisualStyleBackColor = False
        '
        'Btn_worktraveller
        '
        Me.Btn_worktraveller.BackColor = System.Drawing.Color.SteelBlue
        Me.Btn_worktraveller.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_worktraveller.ForeColor = System.Drawing.Color.White
        Me.Btn_worktraveller.Location = New System.Drawing.Point(133, 22)
        Me.Btn_worktraveller.Name = "Btn_worktraveller"
        Me.Btn_worktraveller.Size = New System.Drawing.Size(125, 35)
        Me.Btn_worktraveller.TabIndex = 202
        Me.Btn_worktraveller.Text = "Work Traveller"
        Me.Btn_worktraveller.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.DimGray
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Column1, Me.DataGridViewTextBoxColumn2})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(21, 108)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(243, 419)
        Me.DataGridView1.TabIndex = 178
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "PN"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 150
        '
        'Column1
        '
        Me.Column1.HeaderText = "Lotnumber"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 110
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Recieved Date"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.BackgroundColor = System.Drawing.Color.DimGray
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.Column2})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView2.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView2.Location = New System.Drawing.Point(21, 108)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(134, 419)
        Me.DataGridView2.TabIndex = 192
        Me.DataGridView2.Visible = False
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "PN"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Visible = False
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "LN"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 110
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "id"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        '
        'Column2
        '
        Me.Column2.HeaderText = "position_index"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        '
        'Bgw1
        '
        '
        'Bgw2
        '
        '
        'Bgw3
        '
        '
        'Bgw4
        '
        '
        'WorkTraveller
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1011, 537)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "WorkTraveller"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Work Traveller"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Setting_GrpBx.ResumeLayout(False)
        Me.Setting_GrpBx.PerformLayout()
        Me.Container_Pnl.ResumeLayout(False)
        CType(Me.Pcbloading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ln_Search_PicBx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Generate_GrpBx.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Gen_Btn As System.Windows.Forms.Button
    Friend WithEvents Setting_GrpBx As System.Windows.Forms.GroupBox
    Friend WithEvents Auto_RdBtn As System.Windows.Forms.RadioButton
    Friend WithEvents Manual_RdBtn As System.Windows.Forms.RadioButton
    Friend WithEvents Container_Pnl As System.Windows.Forms.Panel
    Friend WithEvents Lot_Number_TxtBx As System.Windows.Forms.TextBox
    Friend WithEvents Ln_Search_PicBx As System.Windows.Forms.PictureBox
    Friend WithEvents Generate_GrpBx As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bgw1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Bgw2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Pcbloading As System.Windows.Forms.PictureBox
    Friend WithEvents Bgw3 As System.ComponentModel.BackgroundWorker
    Friend WithEvents CKB_visual As System.Windows.Forms.CheckBox
    Friend WithEvents CKB_testing As System.Windows.Forms.CheckBox
    Friend WithEvents Btn_potraveller As System.Windows.Forms.Button
    Friend WithEvents Btn_worktraveller As System.Windows.Forms.Button
    Friend WithEvents Bgw4 As System.ComponentModel.BackgroundWorker

End Class
