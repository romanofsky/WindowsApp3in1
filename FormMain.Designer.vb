<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
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
        Me.gbMain = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnCheckOut = New System.Windows.Forms.Button()
        Me.BtnCustomers = New System.Windows.Forms.Button()
        Me.BtnDeli = New System.Windows.Forms.Button()
        Me.BtnManual = New System.Windows.Forms.Button()
        Me.BtnMenu = New System.Windows.Forms.Button()
        Me.BtnProducts = New System.Windows.Forms.Button()
        Me.PbExit = New System.Windows.Forms.PictureBox()
        Me.BtnOptions = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.gbMain.SuspendLayout()
        CType(Me.PbExit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbMain
        '
        Me.gbMain.Controls.Add(Me.Label3)
        Me.gbMain.Controls.Add(Me.Label2)
        Me.gbMain.Controls.Add(Me.Label1)
        Me.gbMain.Controls.Add(Me.BtnCheckOut)
        Me.gbMain.Controls.Add(Me.BtnCustomers)
        Me.gbMain.Controls.Add(Me.BtnDeli)
        Me.gbMain.Controls.Add(Me.BtnManual)
        Me.gbMain.Controls.Add(Me.BtnMenu)
        Me.gbMain.Controls.Add(Me.BtnProducts)
        Me.gbMain.Controls.Add(Me.PbExit)
        Me.gbMain.Controls.Add(Me.BtnOptions)
        Me.gbMain.Location = New System.Drawing.Point(12, 640)
        Me.gbMain.Name = "gbMain"
        Me.gbMain.Size = New System.Drawing.Size(1000, 116)
        Me.gbMain.TabIndex = 0
        Me.gbMain.TabStop = False
        Me.gbMain.Text = "Main"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(259, 25)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "www.Semiintegration.com"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(245, 25)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Deli Bodega Version 1.0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 25)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "3 in 1 POS"
        '
        'BtnCheckOut
        '
        Me.BtnCheckOut.Location = New System.Drawing.Point(902, 67)
        Me.BtnCheckOut.Name = "BtnCheckOut"
        Me.BtnCheckOut.Size = New System.Drawing.Size(80, 32)
        Me.BtnCheckOut.TabIndex = 7
        Me.BtnCheckOut.Text = "CheckOut"
        Me.BtnCheckOut.UseVisualStyleBackColor = True
        '
        'BtnCustomers
        '
        Me.BtnCustomers.Location = New System.Drawing.Point(289, 67)
        Me.BtnCustomers.Name = "BtnCustomers"
        Me.BtnCustomers.Size = New System.Drawing.Size(86, 32)
        Me.BtnCustomers.TabIndex = 6
        Me.BtnCustomers.Text = "Customers"
        Me.BtnCustomers.UseVisualStyleBackColor = True
        '
        'BtnDeli
        '
        Me.BtnDeli.Location = New System.Drawing.Point(394, 67)
        Me.BtnDeli.Name = "BtnDeli"
        Me.BtnDeli.Size = New System.Drawing.Size(75, 32)
        Me.BtnDeli.TabIndex = 5
        Me.BtnDeli.Text = "Deli"
        Me.BtnDeli.UseVisualStyleBackColor = True
        '
        'BtnManual
        '
        Me.BtnManual.Location = New System.Drawing.Point(495, 67)
        Me.BtnManual.Name = "BtnManual"
        Me.BtnManual.Size = New System.Drawing.Size(75, 32)
        Me.BtnManual.TabIndex = 4
        Me.BtnManual.Text = "Manual"
        Me.BtnManual.UseVisualStyleBackColor = True
        '
        'BtnMenu
        '
        Me.BtnMenu.Location = New System.Drawing.Point(596, 67)
        Me.BtnMenu.Name = "BtnMenu"
        Me.BtnMenu.Size = New System.Drawing.Size(75, 32)
        Me.BtnMenu.TabIndex = 3
        Me.BtnMenu.Text = "Menus"
        Me.BtnMenu.UseVisualStyleBackColor = True
        '
        'BtnProducts
        '
        Me.BtnProducts.Location = New System.Drawing.Point(700, 67)
        Me.BtnProducts.Name = "BtnProducts"
        Me.BtnProducts.Size = New System.Drawing.Size(75, 32)
        Me.BtnProducts.TabIndex = 2
        Me.BtnProducts.Text = "Products"
        Me.BtnProducts.UseVisualStyleBackColor = True
        '
        'PbExit
        '
        Me.PbExit.Image = Global.WindowsApp3in1.My.Resources.Resources.exiticon
        Me.PbExit.Location = New System.Drawing.Point(962, 21)
        Me.PbExit.Name = "PbExit"
        Me.PbExit.Size = New System.Drawing.Size(20, 20)
        Me.PbExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PbExit.TabIndex = 1
        Me.PbExit.TabStop = False
        '
        'BtnOptions
        '
        Me.BtnOptions.Location = New System.Drawing.Point(797, 67)
        Me.BtnOptions.Name = "BtnOptions"
        Me.BtnOptions.Size = New System.Drawing.Size(75, 32)
        Me.BtnOptions.TabIndex = 0
        Me.BtnOptions.Text = "Options"
        Me.BtnOptions.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Green
        Me.Panel2.Location = New System.Drawing.Point(544, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(450, 480)
        Me.Panel2.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LimeGreen
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(12, 15)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(997, 600)
        Me.Panel1.TabIndex = 2
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.HotTrack
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.gbMain)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FormMain"
        Me.Text = "FormMain"
        Me.gbMain.ResumeLayout(False)
        Me.gbMain.PerformLayout()
        CType(Me.PbExit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gbMain As GroupBox
    Friend WithEvents BtnCheckOut As Button
    Friend WithEvents BtnCustomers As Button
    Friend WithEvents BtnDeli As Button
    Friend WithEvents BtnManual As Button
    Friend WithEvents BtnMenu As Button
    Friend WithEvents BtnProducts As Button
    Friend WithEvents PbExit As PictureBox
    Friend WithEvents BtnOptions As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
End Class
