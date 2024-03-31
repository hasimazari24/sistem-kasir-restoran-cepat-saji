Public Class Form_utama
    Sub otomatisclose()
        'form terkini otomatis close ketika buka form child lain'
        For Each f As Form In Me.MdiChildren
            f.Close()
        Next
    End Sub


    Private Sub TToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TToolStripMenuItem.Click
        otomatisclose()
        Form_menu.MdiParent = Me
        Form_menu.Show()

    End Sub

    Private Sub DaftarKategoriToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DaftarKategoriToolStripMenuItem.Click
        otomatisclose()
        Form_Kategori.MdiParent = Me
        Form_Kategori.Show()
    End Sub

    Private Sub DataPenggunaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataPenggunaToolStripMenuItem.Click
        otomatisclose()
        Form_pengguna.MdiParent = Me
        Form_pengguna.Show()
    End Sub

    Private Sub PembayaranToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PembayaranToolStripMenuItem.Click
        otomatisclose()
        Form_pembayaran.MdiParent = Me
        Form_pembayaran.Show()

    End Sub

    Private Sub Form_utama_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetStyle(ControlStyles.Opaque Or ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, False)
    End Sub

    Private Sub ToolStripStatusLabel8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel8.Click
        Close()
        Form_login.Show()
        Form_login.TextBox1.Focus()
    End Sub

    Private Sub HomeToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HomeToolStripMenuItem.Click
        otomatisclose()
    End Sub

    Private Sub DataMenuToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataMenuToolStripMenuItem.Click
        Form_laporan.Show()
        Form_laporan.tampilmenu()
    End Sub

    Private Sub LapPenggunaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LapPenggunaToolStripMenuItem.Click
        Form_laporan.Show()
        Form_laporan.tampilpengguna()
    End Sub

    Private Sub LapKatiToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LapKatiToolStripMenuItem1.Click
        Form_laporan.Show()
        Form_laporan.tampilkategori()
    End Sub

    Private Sub DaftarPesananToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DaftarPesananToolStripMenuItem.Click
        otomatisclose()
        Form_daftarpesanan.MdiParent = Me
        Form_daftarpesanan.Show()
    End Sub

    Private Sub PemesananToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PemesananToolStripMenuItem.Click
        otomatisclose()
        Form_pemesanan.MdiParent = Me
        Form_pemesanan.Show()
    End Sub


    Private Sub LapTrxToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LapTrxToolStripMenuItem1.Click
        Form_laporan.Show()
        Form_laporan.laporantransaksi()
    End Sub

    Private Sub DaftarPembayaranToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DaftarPembayaranToolStripMenuItem.Click
        otomatisclose()
        Form_daftarpembayaran.MdiParent = Me
        Form_daftarpembayaran.Show()
    End Sub

    Private Sub TentangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TentangToolStripMenuItem.Click
        otomatisclose()
        Form_tentang.MdiParent = Me
        Form_tentang.Show()
    End Sub
End Class
