<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.pbox_screen = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.vrp_x = New System.Windows.Forms.TextBox()
        Me.vrp_y = New System.Windows.Forms.TextBox()
        Me.vrp_z = New System.Windows.Forms.TextBox()
        Me.vpn_x = New System.Windows.Forms.TextBox()
        Me.vpn_y = New System.Windows.Forms.TextBox()
        Me.vpn_z = New System.Windows.Forms.TextBox()
        Me.vup_x = New System.Windows.Forms.TextBox()
        Me.vup_y = New System.Windows.Forms.TextBox()
        Me.vup_z = New System.Windows.Forms.TextBox()
        Me.cop_x = New System.Windows.Forms.TextBox()
        Me.cop_y = New System.Windows.Forms.TextBox()
        Me.cop_z = New System.Windows.Forms.TextBox()
        Me.u_min = New System.Windows.Forms.TextBox()
        Me.v_min = New System.Windows.Forms.TextBox()
        Me.u_max = New System.Windows.Forms.TextBox()
        Me.v_max = New System.Windows.Forms.TextBox()
        Me.front_plane = New System.Windows.Forms.TextBox()
        Me.back_plane = New System.Windows.Forms.TextBox()
        Me.btn_generate = New System.Windows.Forms.Button()
        Me.btn_clear = New System.Windows.Forms.Button()
        Me.list_vertices = New System.Windows.Forms.ListBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btn_default_view = New System.Windows.Forms.Button()
        Me.list_matrixt1t2 = New System.Windows.Forms.ListBox()
        Me.list_matrixt3 = New System.Windows.Forms.ListBox()
        Me.list_matrixt4 = New System.Windows.Forms.ListBox()
        Me.list_matrixt5 = New System.Windows.Forms.ListBox()
        Me.lbox_test = New System.Windows.Forms.ListBox()
        CType(Me.pbox_screen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbox_screen
        '
        Me.pbox_screen.BackColor = System.Drawing.Color.White
        Me.pbox_screen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbox_screen.Location = New System.Drawing.Point(0, 0)
        Me.pbox_screen.Name = "pbox_screen"
        Me.pbox_screen.Size = New System.Drawing.Size(400, 400)
        Me.pbox_screen.TabIndex = 0
        Me.pbox_screen.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(406, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "VRP"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(406, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "VPN"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(406, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "VUP"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(406, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "COP"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(406, 130)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Window min"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(406, 161)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Window max"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(406, 190)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(20, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "FP"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(406, 222)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(21, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "BP"
        '
        'vrp_x
        '
        Me.vrp_x.Location = New System.Drawing.Point(508, 6)
        Me.vrp_x.Name = "vrp_x"
        Me.vrp_x.Size = New System.Drawing.Size(39, 20)
        Me.vrp_x.TabIndex = 13
        '
        'vrp_y
        '
        Me.vrp_y.Location = New System.Drawing.Point(553, 6)
        Me.vrp_y.Name = "vrp_y"
        Me.vrp_y.Size = New System.Drawing.Size(39, 20)
        Me.vrp_y.TabIndex = 14
        '
        'vrp_z
        '
        Me.vrp_z.Location = New System.Drawing.Point(598, 6)
        Me.vrp_z.Name = "vrp_z"
        Me.vrp_z.Size = New System.Drawing.Size(39, 20)
        Me.vrp_z.TabIndex = 15
        '
        'vpn_x
        '
        Me.vpn_x.Location = New System.Drawing.Point(508, 34)
        Me.vpn_x.Name = "vpn_x"
        Me.vpn_x.Size = New System.Drawing.Size(39, 20)
        Me.vpn_x.TabIndex = 16
        '
        'vpn_y
        '
        Me.vpn_y.Location = New System.Drawing.Point(553, 34)
        Me.vpn_y.Name = "vpn_y"
        Me.vpn_y.Size = New System.Drawing.Size(39, 20)
        Me.vpn_y.TabIndex = 17
        '
        'vpn_z
        '
        Me.vpn_z.Location = New System.Drawing.Point(598, 34)
        Me.vpn_z.Name = "vpn_z"
        Me.vpn_z.Size = New System.Drawing.Size(39, 20)
        Me.vpn_z.TabIndex = 18
        '
        'vup_x
        '
        Me.vup_x.Location = New System.Drawing.Point(508, 64)
        Me.vup_x.Name = "vup_x"
        Me.vup_x.Size = New System.Drawing.Size(39, 20)
        Me.vup_x.TabIndex = 19
        '
        'vup_y
        '
        Me.vup_y.Location = New System.Drawing.Point(553, 64)
        Me.vup_y.Name = "vup_y"
        Me.vup_y.Size = New System.Drawing.Size(39, 20)
        Me.vup_y.TabIndex = 20
        '
        'vup_z
        '
        Me.vup_z.Location = New System.Drawing.Point(598, 64)
        Me.vup_z.Name = "vup_z"
        Me.vup_z.Size = New System.Drawing.Size(39, 20)
        Me.vup_z.TabIndex = 21
        '
        'cop_x
        '
        Me.cop_x.Location = New System.Drawing.Point(508, 96)
        Me.cop_x.Name = "cop_x"
        Me.cop_x.Size = New System.Drawing.Size(39, 20)
        Me.cop_x.TabIndex = 22
        '
        'cop_y
        '
        Me.cop_y.Location = New System.Drawing.Point(553, 96)
        Me.cop_y.Name = "cop_y"
        Me.cop_y.Size = New System.Drawing.Size(39, 20)
        Me.cop_y.TabIndex = 23
        '
        'cop_z
        '
        Me.cop_z.Location = New System.Drawing.Point(598, 96)
        Me.cop_z.Name = "cop_z"
        Me.cop_z.Size = New System.Drawing.Size(39, 20)
        Me.cop_z.TabIndex = 24
        '
        'u_min
        '
        Me.u_min.Location = New System.Drawing.Point(508, 127)
        Me.u_min.Name = "u_min"
        Me.u_min.Size = New System.Drawing.Size(39, 20)
        Me.u_min.TabIndex = 25
        '
        'v_min
        '
        Me.v_min.Location = New System.Drawing.Point(553, 127)
        Me.v_min.Name = "v_min"
        Me.v_min.Size = New System.Drawing.Size(39, 20)
        Me.v_min.TabIndex = 26
        '
        'u_max
        '
        Me.u_max.Location = New System.Drawing.Point(508, 158)
        Me.u_max.Name = "u_max"
        Me.u_max.Size = New System.Drawing.Size(39, 20)
        Me.u_max.TabIndex = 27
        '
        'v_max
        '
        Me.v_max.Location = New System.Drawing.Point(553, 158)
        Me.v_max.Name = "v_max"
        Me.v_max.Size = New System.Drawing.Size(39, 20)
        Me.v_max.TabIndex = 28
        '
        'front_plane
        '
        Me.front_plane.Location = New System.Drawing.Point(508, 187)
        Me.front_plane.Name = "front_plane"
        Me.front_plane.Size = New System.Drawing.Size(39, 20)
        Me.front_plane.TabIndex = 29
        '
        'back_plane
        '
        Me.back_plane.Location = New System.Drawing.Point(508, 219)
        Me.back_plane.Name = "back_plane"
        Me.back_plane.Size = New System.Drawing.Size(39, 20)
        Me.back_plane.TabIndex = 30
        '
        'btn_generate
        '
        Me.btn_generate.Location = New System.Drawing.Point(527, 270)
        Me.btn_generate.Name = "btn_generate"
        Me.btn_generate.Size = New System.Drawing.Size(116, 48)
        Me.btn_generate.TabIndex = 31
        Me.btn_generate.Text = "Generate view"
        Me.btn_generate.UseVisualStyleBackColor = True
        '
        'btn_clear
        '
        Me.btn_clear.Location = New System.Drawing.Point(406, 270)
        Me.btn_clear.Name = "btn_clear"
        Me.btn_clear.Size = New System.Drawing.Size(115, 48)
        Me.btn_clear.TabIndex = 33
        Me.btn_clear.Text = "Clear Screen"
        Me.btn_clear.UseVisualStyleBackColor = True
        '
        'list_vertices
        '
        Me.list_vertices.FormattingEnabled = True
        Me.list_vertices.Location = New System.Drawing.Point(666, 34)
        Me.list_vertices.Name = "list_vertices"
        Me.list_vertices.Size = New System.Drawing.Size(531, 160)
        Me.list_vertices.TabIndex = 34
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(663, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 13)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "Vertices Location"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(663, 219)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 13)
        Me.Label10.TabIndex = 36
        Me.Label10.Text = "Matrix T1_T2"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(964, 219)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 13)
        Me.Label12.TabIndex = 38
        Me.Label12.Text = "Matrix T3"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(663, 359)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(51, 13)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "Matrix T4"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(964, 359)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(51, 13)
        Me.Label16.TabIndex = 42
        Me.Label16.Text = "Matrix T5"
        '
        'btn_default_view
        '
        Me.btn_default_view.Location = New System.Drawing.Point(409, 324)
        Me.btn_default_view.Name = "btn_default_view"
        Me.btn_default_view.Size = New System.Drawing.Size(234, 48)
        Me.btn_default_view.TabIndex = 44
        Me.btn_default_view.Text = "Default View"
        Me.btn_default_view.UseVisualStyleBackColor = True
        '
        'list_matrixt1t2
        '
        Me.list_matrixt1t2.FormattingEnabled = True
        Me.list_matrixt1t2.Location = New System.Drawing.Point(666, 235)
        Me.list_matrixt1t2.Name = "list_matrixt1t2"
        Me.list_matrixt1t2.Size = New System.Drawing.Size(255, 108)
        Me.list_matrixt1t2.TabIndex = 45
        '
        'list_matrixt3
        '
        Me.list_matrixt3.FormattingEnabled = True
        Me.list_matrixt3.Location = New System.Drawing.Point(927, 235)
        Me.list_matrixt3.Name = "list_matrixt3"
        Me.list_matrixt3.Size = New System.Drawing.Size(270, 108)
        Me.list_matrixt3.TabIndex = 46
        '
        'list_matrixt4
        '
        Me.list_matrixt4.FormattingEnabled = True
        Me.list_matrixt4.Location = New System.Drawing.Point(666, 375)
        Me.list_matrixt4.Name = "list_matrixt4"
        Me.list_matrixt4.Size = New System.Drawing.Size(255, 108)
        Me.list_matrixt4.TabIndex = 47
        '
        'list_matrixt5
        '
        Me.list_matrixt5.FormattingEnabled = True
        Me.list_matrixt5.Location = New System.Drawing.Point(927, 375)
        Me.list_matrixt5.Name = "list_matrixt5"
        Me.list_matrixt5.Size = New System.Drawing.Size(270, 108)
        Me.list_matrixt5.TabIndex = 48
        '
        'lbox_test
        '
        Me.lbox_test.FormattingEnabled = True
        Me.lbox_test.Location = New System.Drawing.Point(0, 406)
        Me.lbox_test.Name = "lbox_test"
        Me.lbox_test.Size = New System.Drawing.Size(400, 186)
        Me.lbox_test.TabIndex = 49
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1204, 609)
        Me.Controls.Add(Me.lbox_test)
        Me.Controls.Add(Me.list_matrixt5)
        Me.Controls.Add(Me.list_matrixt4)
        Me.Controls.Add(Me.list_matrixt3)
        Me.Controls.Add(Me.list_matrixt1t2)
        Me.Controls.Add(Me.btn_default_view)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.list_vertices)
        Me.Controls.Add(Me.btn_clear)
        Me.Controls.Add(Me.btn_generate)
        Me.Controls.Add(Me.back_plane)
        Me.Controls.Add(Me.front_plane)
        Me.Controls.Add(Me.v_max)
        Me.Controls.Add(Me.u_max)
        Me.Controls.Add(Me.v_min)
        Me.Controls.Add(Me.u_min)
        Me.Controls.Add(Me.cop_z)
        Me.Controls.Add(Me.cop_y)
        Me.Controls.Add(Me.cop_x)
        Me.Controls.Add(Me.vup_z)
        Me.Controls.Add(Me.vup_y)
        Me.Controls.Add(Me.vup_x)
        Me.Controls.Add(Me.vpn_z)
        Me.Controls.Add(Me.vpn_y)
        Me.Controls.Add(Me.vpn_x)
        Me.Controls.Add(Me.vrp_z)
        Me.Controls.Add(Me.vrp_y)
        Me.Controls.Add(Me.vrp_x)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pbox_screen)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.TopMost = True
        CType(Me.pbox_screen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pbox_screen As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents vrp_x As TextBox
    Friend WithEvents vrp_y As TextBox
    Friend WithEvents vrp_z As TextBox
    Friend WithEvents vpn_x As TextBox
    Friend WithEvents vpn_y As TextBox
    Friend WithEvents vpn_z As TextBox
    Friend WithEvents vup_x As TextBox
    Friend WithEvents vup_y As TextBox
    Friend WithEvents vup_z As TextBox
    Friend WithEvents cop_x As TextBox
    Friend WithEvents cop_y As TextBox
    Friend WithEvents cop_z As TextBox
    Friend WithEvents u_min As TextBox
    Friend WithEvents v_min As TextBox
    Friend WithEvents u_max As TextBox
    Friend WithEvents v_max As TextBox
    Friend WithEvents front_plane As TextBox
    Friend WithEvents back_plane As TextBox
    Friend WithEvents btn_generate As Button
    Friend WithEvents btn_clear As Button
    Friend WithEvents list_vertices As ListBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents btn_default_view As Button
    Friend WithEvents list_matrixt1t2 As ListBox
    Friend WithEvents list_matrixt3 As ListBox
    Friend WithEvents list_matrixt4 As ListBox
    Friend WithEvents list_matrixt5 As ListBox
    Friend WithEvents lbox_test As ListBox
End Class
