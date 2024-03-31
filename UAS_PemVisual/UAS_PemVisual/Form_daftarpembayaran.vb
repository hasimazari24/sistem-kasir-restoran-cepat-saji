Imports System.Data.Odbc

Public Class Form_daftarpembayaran
    Dim con As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim dt As DataTable
    Dim cmd As OdbcCommand

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

    Sub tampildata_bayar()
        DataGridView3.Rows.Clear()
        Try
            koneksi()
            Dim tgldari As New String(Format(DateTimePicker1.Value, "yyyy-MM-dd"))
            Dim tglsampai As New String(Format(DateTimePicker2.Value, "yyyy-MM-dd"))
            da = New OdbcDataAdapter("select kode_bayar,kode_pesan,DATE_FORMAT(tgl_bayar, '%d-%m-%Y'),total_bayar,jml_bayar,kembalian from tb_pembayaran where (kode_bayar like '%" & TextBox4.Text & "%' OR kode_pesan like '%" & TextBox4.Text & "%') AND (tgl_bayar BETWEEN '" & tgldari & "' AND '" & tglsampai & "') order by kode_bayar desc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView3.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox(String.Format("Menampilkan data GAGAL. {0}", ex.Message), MsgBoxStyle.Critical, "PERINGATAN")
        End Try
    End Sub

    Private Sub Form_daftarpembayaran_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        tampildata_bayar()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        tampildata_bayar()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If (DataGridView3.SelectedRows.Count = 1) Then
            Dim i As Integer = DataGridView3.SelectedRows(0).Index
            Form_laporan.Show()
            Dim kodebyr As String = DataGridView3.Rows(i).Cells(0).Value
            Form_laporan.cetakpembayaran(kodebyr)
        Else
            MsgBox("Pilih datanya dulu!", MsgBoxStyle.Critical, "PERINGATAN")
        End If
    End Sub
End Class