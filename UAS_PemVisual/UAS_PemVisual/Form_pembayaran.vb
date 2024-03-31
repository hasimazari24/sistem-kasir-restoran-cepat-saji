Imports System.Data.Odbc
Imports System.IO
Imports System.Data

Public Class Form_pembayaran
    Dim con As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim dt As DataTable
    Dim cmd As OdbcCommand
    Dim kodemenu As String
    Dim gambar As String

    Protected Overrides ReadOnly Property CreateParams() As Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Public Sub koneksi()
        Try
            con = New OdbcConnection("dsn=kasir_restoran")
            If con.State = ConnectionState.Closed Then 'cek apakah koneksi tertutup, jika ya maka open'
                con.Open()
                'MsgBox("Koneksi Berhasil", vbInformation, "Koneksi Berhasil")'
            End If
        Catch ex As Exception
            MsgBox(String.Format("Koneksi Gagal. {0}", ex.Message), vbExclamation, "Koneksi Gagal")
        End Try
    End Sub

    Sub tampilpesanan(ByVal kodepesan As String)
        DataGridView4.Rows.Clear()
        Try
            koneksi()
            Dim sql As String
            If kodepesan = "" Or kodepesan = String.Empty Then
                sql = "select kode_pesan, DATE_FORMAT(tgl_pesan, '%d-%m-%Y'), no_meja, nama_pemesan from tb_pemesanan where status_pesanan = 'diproses'"
            Else
                sql = "select kode_pesan,DATE_FORMAT(tgl_pesan, '%d-%m-%Y'), no_meja, nama_pemesan from tb_pemesanan where kode_pesan = '" & kodepesan & "' AND status_pesanan = 'diproses'"
            End If
            da = New OdbcDataAdapter(sql, con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView4.Rows.Add(row(0), row(1), row(2), row(3))
            Next
            sql = String.Empty
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox(String.Format("Menampilkan data GAGAL {0}", ex.Message), vbExclamation, "PERINGATAN")
        End Try
    End Sub

    Function kodepembayaran() As String
        Call koneksi()
        cmd = New OdbcCommand("select kode_bayar from tb_pembayaran order by kode_bayar desc", con)
        dr = cmd.ExecuteReader
        dr.Read()
        Dim kodebayar As String

        If Not dr.HasRows Then
            kodebayar = "TRX" + Format(Today, "ddMMyy") + "0001"
        Else
            'jika sudah ada kode di tanggal yg sama maka tinggal tambah 1 di urutan digit terakhir'
            '..MID menghitung string di tengah2 nya'
            '..Right menghitung string dari kanan string'
            If Microsoft.VisualBasic.Mid(dr.Item("kode_bayar"), 4, 6) = Format(Today, "ddMMyy") Then
                kodebayar = "TRX" + Format(Today, "ddMMyy") + Format(Microsoft.VisualBasic.Right(dr.Item("kode_bayar"), 4) + 1, "0000")
            Else
                'jika belum ada kode sama sekali di tanggal hari ini'
                kodebayar = "TRX" + Format(Today, "ddMMyy") + "0001"
            End If
        End If
        Return kodebayar
    End Function

    Sub bersih()
        DataGridView3.Rows.Clear()
        jum_byr = 0
        tot_byr = 0
        kembali = 0
        TextBox13.Clear()
        TextBox4.Clear()
        TextBox11.Clear()
        TextBox1.Text = ""
        TextBox12.Clear()
        TextBox6.Clear()
    End Sub

    Private Sub Form_pembayaran_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Panel1.Visible = False
    End Sub


    Private Sub GroupBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox4.Enter
        Panel1.Visible = False
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Panel1.Visible = True
        tampilpesanan(TextBox4.Text)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Panel1.Visible = False
    End Sub

    Private Sub DataGridView4_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView4.DoubleClick
        
    End Sub

    Dim kodepesan As String

    Private Sub DataGridView4_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView4.CellDoubleClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            DataGridView3.Rows.Clear()
            Try
                koneksi()
                da = New OdbcDataAdapter("SELECT psn.kode_pesan, mn.nama_menu, psn_dt.harga, psn_dt.jumlah, psn_dt.diskon, psn_dt.subharga, psn.total_harga, psn.total_diskon, psn.total_subharga FROM tb_pemesanan psn JOIN tb_pemesanandetail psn_dt ON psn.kode_pesan = psn_dt.kode_pesan JOIN tb_menu mn ON psn_dt.kode_menu = mn.kode_menu WHERE psn.kode_pesan = '" & DataGridView4.Rows(e.RowIndex).Cells(0).Value & "'", con)
                dt = New DataTable
                da.Fill(dt)
                For Each row In dt.Rows
                    DataGridView3.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5))
                Next
                kodepesan = dt.Rows(0).Item(0).ToString
                TextBox1.Text = dt.Rows(0).Item(6).ToString
                TextBox12.Text = dt.Rows(0).Item(7).ToString
                TextBox11.Text = dt.Rows(0).Item(8).ToString
                dt.Rows.Clear()
                Panel1.Visible = False
            Catch ex As Exception
                MsgBox(String.Format("Menampilkan data GAGAL {0}", ex.Message), vbExclamation, "PERINGATAN")
            End Try
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim kodebyr As String
        Dim tgl As New String(Format(DateTimePicker2.Value, "yyyy-MM-dd"))
        If TextBox11.Text = "" Or TextBox12.Text = "" Or DataGridView3.Rows.Count < 1 Or TextBox6.Text = "" Then
            MsgBox("Silahkan lengkapi data!", MsgBoxStyle.Critical, "PERINGATAN")
        ElseIf Val(TextBox13.Text) < 0 Then
            MsgBox("jumlah bayar tidak boleh kurang dari total bayar!", MsgBoxStyle.Critical, "PERINGATAN")
        Else
            Try
                koneksi()
                kodebyr = kodepembayaran()
                cmd = New OdbcCommand("INSERT INTO tb_pembayaran values ('" & kodebyr & "','" & DataGridView3.Rows(0).Cells(0).Value.ToString & "','" & tgl & "'," & TextBox11.Text & "," & TextBox6.Text & ",'" & TextBox13.Text & "')", con)
                cmd.ExecuteNonQuery()
                cmd = New OdbcCommand("UPDATE tb_pemesanan set status_pesanan='dibayar' where kode_pesan='" & kodepesan & "'", con)
                cmd.ExecuteNonQuery()
                MsgBox("PEMBAYARAN BERHASIL, Silahkan cetak nota pembayaran", vbInformation, "INFORMASI")
                Form_laporan.Show()
                Form_laporan.cetakpembayaran(kodebyr)
                bersih()
                tampilpesanan(String.Empty)
            Catch ex As Exception
                MsgBox(String.Format("Simpan data gagal. {0}", ex.Message), vbExclamation, "Terjadi Kesalahan")
            End Try

        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        bersih()
    End Sub

    Dim jum_byr As Single
    Dim tot_byr As Single
    Dim kembali As Single

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        If Integer.TryParse(TextBox6.Text, jum_byr) And Single.TryParse(TextBox11.Text, tot_byr) Then
            kembali = jum_byr - tot_byr
            TextBox13.Text = kembali
        Else
            TextBox13.Text = kembali
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If DataGridView3.Rows.Count < 1 Then
            MsgBox("Data Pesanan belum dipilih!", MsgBoxStyle.Critical, "PERINGATAN")
        Else
            If (MessageBox.Show("Anda yakin ingin membatalkan pesanan dan transaksi dengan Kode Pesanan = " & kodepesan &
           "?", "Batal Transaksi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) =
           Windows.Forms.DialogResult.OK) Then
                koneksi()
                cmd = New OdbcCommand("update tb_pemesanan set status_pesanan='dibatalkan' where kode_pesan='" & kodepesan & "'", con)
                cmd.ExecuteNonQuery()
                MsgBox("Transaksi BERHASIL Dibatalkan", vbInformation, "INFORMASI")
                bersih()
            End If
        End If
    End Sub
End Class