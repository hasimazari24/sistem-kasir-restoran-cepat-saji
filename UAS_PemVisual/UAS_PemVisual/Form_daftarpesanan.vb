Imports System.Data.Odbc

Public Class Form_daftarpesanan
    Dim con As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim dt As DataTable
    Dim cmd As OdbcCommand

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

    Sub tampilheader()
        DataGridView4.Rows.Clear()
        Try
            koneksi()
            Dim tgldari As New String(Format(DateTimePicker1.Value, "yyyy-MM-dd"))
            Dim tglsampai As New String(Format(DateTimePicker2.Value, "yyyy-MM-dd"))
            da = New OdbcDataAdapter("select status_pesanan, kode_pesan, DATE_FORMAT(tgl_pesan, '%d-%m-%Y'), no_meja, nama_pemesan, total_harga, total_diskon, total_subharga from tb_pemesanan where (kode_pesan like '%" & TextBox3.Text & "%' OR no_meja like '%" & TextBox3.Text & "%' OR nama_pemesan like '%" & TextBox3.Text & "%' OR status_pesanan like '%" & TextBox3.Text & "%') AND (tgl_pesan BETWEEN '" & tgldari & "' AND '" & tglsampai & "') order by kode_pesan desc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView4.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5), row(6), row(7))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox(String.Format("Menampilkan data GAGAL. {0}", ex.Message), MsgBoxStyle.Critical, "PERINGATAN")
        End Try
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Sub updatestok(ByVal kodepesan As String)
        koneksi()
        da = New OdbcDataAdapter("select * from tb_pemesanandetail where kode_pesan = '" & kodepesan & "'", con)
        dt = New DataTable
        da.Fill(dt)
        For i As Integer = 0 To dt.Rows.Count - 1
            Try
                Dim jum As Integer = dt.Rows(i).Item(4)
                cmd = New OdbcCommand("update tb_menu set stok = stok-" & jum & " where kode_menu ='" & dt.Rows(i).Item(2) & "'", con)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(String.Format("UPDATE STOK GAGAL. {0}", ex.Message), MsgBoxStyle.Critical, "PERINGATAN")
            End Try
        Next
        For Each row In dt.Rows
            
        Next
    End Sub

    Private Sub Form_daftarpesanan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        tampilheader()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        tampilheader()
    End Sub

    Private Sub DataGridView4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView4.Click
        Dim kodepesan As String = DataGridView4.Item(1, DataGridView4.CurrentRow.Index).Value
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select dt.kode_menu, mn.nama_menu, dt.harga, dt.jumlah, dt.diskon, dt.subharga from tb_pemesanandetail dt JOIN tb_menu mn ON dt.kode_menu=mn.kode_menu where dt.kode_pesan = '" & kodepesan & "' order by dt.kode_menu asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox(String.Format("Menampilkan data GAGAL. {0}", ex.Message), MsgBoxStyle.Critical, "PERINGATAN")
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If (DataGridView4.SelectedRows.Count = 1) Then
            Dim i As Integer = DataGridView4.SelectedRows(0).Index
            Dim kodepesan As String = DataGridView4.Rows(i).Cells(1).Value
            Form_laporan.Show()
            Form_laporan.cetakpesan(kodepesan)
        Else
            MsgBox("Pilih data pesanan terlebih dahulu!", MsgBoxStyle.Critical, "PERINGATAN")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        tampilheader()
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If (DataGridView4.SelectedRows.Count = 1) Then
            Dim i As Integer = DataGridView4.SelectedRows(0).Index
            Dim kodepesan As String = DataGridView4.Rows(i).Cells(1).Value
            koneksi()
            cmd = New OdbcCommand("select * from tb_pemesanan where kode_pesan = '" & kodepesan & "'", con)
            dr = cmd.ExecuteReader
            dr.Read()
            Dim cek As String = dr.Item("status_pesanan")
            If (cek = "selesai") Then
                MsgBox("Pesanan sudah SELESAI!", MsgBoxStyle.Critical, "PERINGATAN")
            ElseIf (cek = "diproses") Then
                MsgBox("Pesanan masih diproses kasir atau belum dibayar atau klik Refresh untuk memuat ulang data!", MsgBoxStyle.Critical, "PERINGATAN")
            ElseIf (cek = "dibatalkan") Then
                MsgBox("Pesanan telah DIBATALKAN Kasir!", MsgBoxStyle.Critical, "PERINGATAN")
            ElseIf (cek = "dibayar") Then
                cmd = New OdbcCommand("update tb_pemesanan set status_pesanan='selesai' where kode_pesan ='" & kodepesan & "'", con)
                cmd.ExecuteNonQuery()
                MsgBox("Selesai Pesanan Berhasil.", MsgBoxStyle.Information, "INFORMASI")
                tampilheader()
                updatestok(kodepesan)
            End If
        Else
            MsgBox("Pilih data pesanan terlebih dahulu!", MsgBoxStyle.Critical, "PERINGATAN")
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If (DataGridView4.SelectedRows.Count = 1) Then
            Dim i As Integer = DataGridView4.SelectedRows(0).Index
            Dim kodepesan As String = DataGridView4.Rows(i).Cells(1).Value
            Form_laporan.Show()
            Form_laporan.cetakdapur(kodepesan)
        Else
            MsgBox("Pilih data pesanan terlebih dahulu!", MsgBoxStyle.Critical, "PERINGATAN")
        End If
        
    End Sub
End Class