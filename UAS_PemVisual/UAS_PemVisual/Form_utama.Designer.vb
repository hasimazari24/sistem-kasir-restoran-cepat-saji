<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_utama
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DaftarKategoriToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataPenggunaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuPelayan = New System.Windows.Forms.ToolStripMenuItem()
        Me.PemesananToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DaftarPesananToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuKasir = New System.Windows.Forms.ToolStripMenuItem()
        Me.PembayaranToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DaftarPembayaranToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BantuanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuLaporan = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataMenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LapPenggunaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LapKatiToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LapTrxToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TentangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LabelKodePengguna = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LabelNama = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LabelEmail = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LabelHakakses = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel7 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel8 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AutoSize = False
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HomeToolStripMenuItem, Me.MenuMaster, Me.MenuPelayan, Me.MenuKasir, Me.BantuanToolStripMenuItem, Me.MenuLaporan, Me.TentangToolStripMenuItem})
        Me.MenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(858, 36)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HomeToolStripMenuItem
        '
        Me.HomeToolStripMenuItem.Image = Global.UAS_PemVisual.My.Resources.Resources.icons8_home_96
        Me.HomeToolStripMenuItem.Name = "HomeToolStripMenuItem"
        Me.HomeToolStripMenuItem.Size = New System.Drawing.Size(78, 32)
        Me.HomeToolStripMenuItem.Text = "Home"
        '
        'MenuMaster
        '
        Me.MenuMaster.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TToolStripMenuItem, Me.DaftarKategoriToolStripMenuItem, Me.DataPenggunaToolStripMenuItem})
        Me.MenuMaster.Image = Global.UAS_PemVisual.My.Resources.Resources.icons8_invoice_96__1_
        Me.MenuMaster.Name = "MenuMaster"
        Me.MenuMaster.Size = New System.Drawing.Size(84, 32)
        Me.MenuMaster.Text = "Master"
        '
        'TToolStripMenuItem
        '
        Me.TToolStripMenuItem.Name = "TToolStripMenuItem"
        Me.TToolStripMenuItem.Size = New System.Drawing.Size(172, 24)
        Me.TToolStripMenuItem.Text = "Daftar Menu"
        '
        'DaftarKategoriToolStripMenuItem
        '
        Me.DaftarKategoriToolStripMenuItem.Name = "DaftarKategoriToolStripMenuItem"
        Me.DaftarKategoriToolStripMenuItem.Size = New System.Drawing.Size(172, 24)
        Me.DaftarKategoriToolStripMenuItem.Text = "Daftar Kategori"
        '
        'DataPenggunaToolStripMenuItem
        '
        Me.DataPenggunaToolStripMenuItem.Name = "DataPenggunaToolStripMenuItem"
        Me.DataPenggunaToolStripMenuItem.Size = New System.Drawing.Size(172, 24)
        Me.DataPenggunaToolStripMenuItem.Text = "Data Pengguna"
        '
        'MenuPelayan
        '
        Me.MenuPelayan.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PemesananToolStripMenuItem, Me.DaftarPesananToolStripMenuItem})
        Me.MenuPelayan.Image = Global.UAS_PemVisual.My.Resources.Resources.icons8_receipt_approved_96
        Me.MenuPelayan.Name = "MenuPelayan"
        Me.MenuPelayan.Size = New System.Drawing.Size(87, 32)
        Me.MenuPelayan.Text = "Pelayan"
        '
        'PemesananToolStripMenuItem
        '
        Me.PemesananToolStripMenuItem.Name = "PemesananToolStripMenuItem"
        Me.PemesananToolStripMenuItem.Size = New System.Drawing.Size(170, 24)
        Me.PemesananToolStripMenuItem.Text = "Pemesanan"
        '
        'DaftarPesananToolStripMenuItem
        '
        Me.DaftarPesananToolStripMenuItem.Name = "DaftarPesananToolStripMenuItem"
        Me.DaftarPesananToolStripMenuItem.Size = New System.Drawing.Size(170, 24)
        Me.DaftarPesananToolStripMenuItem.Text = "Daftar Pesanan"
        '
        'MenuKasir
        '
        Me.MenuKasir.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PembayaranToolStripMenuItem, Me.DaftarPembayaranToolStripMenuItem})
        Me.MenuKasir.Image = Global.UAS_PemVisual.My.Resources.Resources.icons8_accounting_96
        Me.MenuKasir.Name = "MenuKasir"
        Me.MenuKasir.Size = New System.Drawing.Size(70, 32)
        Me.MenuKasir.Text = "Kasir"
        '
        'PembayaranToolStripMenuItem
        '
        Me.PembayaranToolStripMenuItem.Name = "PembayaranToolStripMenuItem"
        Me.PembayaranToolStripMenuItem.Size = New System.Drawing.Size(195, 24)
        Me.PembayaranToolStripMenuItem.Text = "Pembayaran"
        '
        'DaftarPembayaranToolStripMenuItem
        '
        Me.DaftarPembayaranToolStripMenuItem.Name = "DaftarPembayaranToolStripMenuItem"
        Me.DaftarPembayaranToolStripMenuItem.Size = New System.Drawing.Size(195, 24)
        Me.DaftarPembayaranToolStripMenuItem.Text = "Daftar Pembayaran"
        '
        'BantuanToolStripMenuItem
        '
        Me.BantuanToolStripMenuItem.Image = Global.UAS_PemVisual.My.Resources.Resources.icons8_help_90
        Me.BantuanToolStripMenuItem.Name = "BantuanToolStripMenuItem"
        Me.BantuanToolStripMenuItem.Size = New System.Drawing.Size(92, 32)
        Me.BantuanToolStripMenuItem.Text = "Bantuan"
        '
        'MenuLaporan
        '
        Me.MenuLaporan.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DataMenuToolStripMenuItem, Me.LapPenggunaToolStripMenuItem, Me.LapKatiToolStripMenuItem1, Me.LapTrxToolStripMenuItem1})
        Me.MenuLaporan.Image = Global.UAS_PemVisual.My.Resources.Resources.icons8_purchase_order_96
        Me.MenuLaporan.Name = "MenuLaporan"
        Me.MenuLaporan.Size = New System.Drawing.Size(91, 32)
        Me.MenuLaporan.Text = "Laporan"
        '
        'DataMenuToolStripMenuItem
        '
        Me.DataMenuToolStripMenuItem.Name = "DataMenuToolStripMenuItem"
        Me.DataMenuToolStripMenuItem.Size = New System.Drawing.Size(172, 24)
        Me.DataMenuToolStripMenuItem.Text = "Data Menu"
        '
        'LapPenggunaToolStripMenuItem
        '
        Me.LapPenggunaToolStripMenuItem.Name = "LapPenggunaToolStripMenuItem"
        Me.LapPenggunaToolStripMenuItem.Size = New System.Drawing.Size(172, 24)
        Me.LapPenggunaToolStripMenuItem.Text = "Data Pengguna"
        '
        'LapKatiToolStripMenuItem1
        '
        Me.LapKatiToolStripMenuItem1.Name = "LapKatiToolStripMenuItem1"
        Me.LapKatiToolStripMenuItem1.Size = New System.Drawing.Size(172, 24)
        Me.LapKatiToolStripMenuItem1.Text = "Data Kategori"
        '
        'LapTrxToolStripMenuItem1
        '
        Me.LapTrxToolStripMenuItem1.Name = "LapTrxToolStripMenuItem1"
        Me.LapTrxToolStripMenuItem1.Size = New System.Drawing.Size(172, 24)
        Me.LapTrxToolStripMenuItem1.Text = "Data Transaksi"
        '
        'TentangToolStripMenuItem
        '
        Me.TentangToolStripMenuItem.Image = Global.UAS_PemVisual.My.Resources.Resources.icons8_about_90
        Me.TentangToolStripMenuItem.Name = "TentangToolStripMenuItem"
        Me.TentangToolStripMenuItem.Size = New System.Drawing.Size(90, 32)
        Me.TentangToolStripMenuItem.Text = "Tentang"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.Honeydew
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel2, Me.LabelKodePengguna, Me.LabelNama, Me.ToolStripStatusLabel3, Me.LabelEmail, Me.ToolStripStatusLabel5, Me.LabelHakakses, Me.ToolStripStatusLabel7, Me.ToolStripStatusLabel8})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 443)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(858, 35)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.ToolStripStatusLabel2.Image = Global.UAS_PemVisual.My.Resources.Resources._1200px_Font_Awesome_5_solid_user_circle1
        Me.ToolStripStatusLabel2.Margin = New System.Windows.Forms.Padding(8, 8, 5, 8)
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(93, 19)
        Me.ToolStripStatusLabel2.Text = "Pengguna :"
        '
        'LabelKodePengguna
        '
        Me.LabelKodePengguna.Name = "LabelKodePengguna"
        Me.LabelKodePengguna.Size = New System.Drawing.Size(92, 30)
        Me.LabelKodePengguna.Text = "kode_pengguna"
        Me.LabelKodePengguna.Visible = False
        '
        'LabelNama
        '
        Me.LabelNama.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.LabelNama.Name = "LabelNama"
        Me.LabelNama.Size = New System.Drawing.Size(75, 30)
        Me.LabelNama.Text = "nama_user"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(12, 30)
        Me.ToolStripStatusLabel3.Text = "|"
        '
        'LabelEmail
        '
        Me.LabelEmail.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.LabelEmail.Name = "LabelEmail"
        Me.LabelEmail.Size = New System.Drawing.Size(41, 30)
        Me.LabelEmail.Text = "Email"
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(10, 30)
        Me.ToolStripStatusLabel5.Text = "|"
        '
        'LabelHakakses
        '
        Me.LabelHakakses.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.LabelHakakses.Name = "LabelHakakses"
        Me.LabelHakakses.Size = New System.Drawing.Size(66, 30)
        Me.LabelHakakses.Text = "Hakakses"
        '
        'ToolStripStatusLabel7
        '
        Me.ToolStripStatusLabel7.Name = "ToolStripStatusLabel7"
        Me.ToolStripStatusLabel7.Size = New System.Drawing.Size(10, 30)
        Me.ToolStripStatusLabel7.Text = "|"
        '
        'ToolStripStatusLabel8
        '
        Me.ToolStripStatusLabel8.Image = Global.UAS_PemVisual.My.Resources.Resources._1024px_Font_Awesome_5_solid_sign_out_alt_svg
        Me.ToolStripStatusLabel8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolStripStatusLabel8.Margin = New System.Windows.Forms.Padding(5, 3, 0, 2)
        Me.ToolStripStatusLabel8.Name = "ToolStripStatusLabel8"
        Me.ToolStripStatusLabel8.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.ToolStripStatusLabel8.Size = New System.Drawing.Size(79, 30)
        Me.ToolStripStatusLabel8.Text = "LOGOUT"
        '
        'Form_utama
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.UAS_PemVisual.My.Resources.Resources.halamannewfix
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(858, 478)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form_utama"
        Me.Text = "MENU UTAMA"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents HomeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuKasir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PembayaranToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DaftarPembayaranToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuLaporan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataMenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LapPenggunaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DaftarKategoriToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataPenggunaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LapKatiToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BantuanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TentangToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents LabelNama As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents LabelEmail As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel5 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents LabelHakakses As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel7 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel8 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents LabelKodePengguna As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents LapTrxToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuPelayan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PemesananToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DaftarPesananToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
