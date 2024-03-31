Imports System.Data.Odbc
Imports System.IO
Imports System.Data

Public Class Form_pemesanan
    Dim con As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim dt As DataTable
    Dim cmd As OdbcCommand
    Dim kodemenu As String
    Dim gambar As String

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

    Protected Overrides ReadOnly Property CreateParams() As Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Sub tampilmenu()
        DataGridView2.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select mn.kode_menu, mn.nama_menu, mn.harga, mn.stok, kt.kategori, mn.image from tb_menu mn JOIN tb_kategori kt ON mn.kode_kategori=kt.kode_kategori order by mn.kode_menu asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView2.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox("Menampilkan data GAGAL")
        End Try
    End Sub

    Sub bersih()
        kodemenu = String.Empty
        TextBox2.Text = ""
        TextBox7.Text = ""
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Text = ""
        harga = 0
        jumlah = 0
        subharga = 0
        diskon = 0
        PictureBox1.Image = Nothing
    End Sub

    Function kode() As String
        Call koneksi()
        cmd = New OdbcCommand("select kode_pesan from tb_pemesanan order by kode_pesan desc", con)
        dr = cmd.ExecuteReader
        dr.Read()
        Dim kodepesan As String

        If Not dr.HasRows Then
            kodepesan = "ORD" + Format(Today, "ddMMyy") + "0001"
        Else
            'jika sudah ada kode di tanggal yg sama maka tinggal tambah 1 di urutan digit terakhir'
            '..MID menghitung string di tengah2 nya'
            '..Right menghitung string dari kanan string'
            If Microsoft.VisualBasic.Mid(dr.Item("kode_pesan"), 4, 6) = Format(Today, "ddMMyy") Then
                kodepesan = "ORD" + Format(Today, "ddMMyy") + Format(Microsoft.VisualBasic.Right(dr.Item("kode_pesan"), 4) + 1, "0000")
            Else
                'jika belum ada kode sama sekali di tanggal hari ini'
                kodepesan = "ORD" + Format(Today, "ddMMyy") + "0001"
            End If
        End If
        Return kodepesan
    End Function

    Private Sub Form3_kasir_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.MinimumSize = New Size(240, 0)
        TextBox5.MinimumSize = New Size(240, 0)
        Me.WindowState = FormWindowState.Maximized
        tampilmenu()

    End Sub


    Private Sub DataGridView2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            kodemenu = New String(DataGridView2.Rows(e.RowIndex).Cells(0).Value)
            TextBox2.Text = DataGridView2.Rows(e.RowIndex).Cells(1).Value
            TextBox7.Text = DataGridView2.Rows(e.RowIndex).Cells(2).Value
            TextBox8.Text = 1
            TextBox9.Text = 0
            TextBox10.Text = CSng(TextBox7.Text) * CInt(TextBox8.Text)
            gambar = New String(Application.StartupPath & "\SavedImages\" & DataGridView2.Rows(e.RowIndex).Cells(5).Value)
            PictureBox1.Image = Nothing
            If File.Exists(gambar) Then
                Dim pic_file As New FileStream(gambar, FileMode.Open)
                PictureBox1.Image = Image.FromStream(pic_file)
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
                pic_file.Close()
            End If

        End If
    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox9_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox9.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox10_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Dim harga As Single
    Dim jumlah As Integer
    Dim subharga As Single
    Dim diskon As Single
    Dim tot_harga As Single
    Dim tot_dikon As Single
    Dim tot_subharga As Single

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If TextBox10.Text = "" Then
            MsgBox("Silahkan pilih menu dulu!", MsgBoxStyle.Critical, "PERINGATAN")
        ElseIf TextBox8.Text = "" Or TextBox9.Text = "" Then
            MsgBox("Masukkan data dengan benar!", MsgBoxStyle.Critical, "PERINGATAN")
        Else
            DataGridView1.Rows.Add()
            Dim Row As Integer = DataGridView1.Rows.Count - 1
            DataGridView1.Rows(Row).Cells(0).Value = kodemenu
            DataGridView1.Rows(Row).Cells(1).Value = TextBox2.Text
            DataGridView1.Rows(Row).Cells(2).Value = TextBox7.Text
            DataGridView1.Rows(Row).Cells(3).Value = TextBox8.Text
            DataGridView1.Rows(Row).Cells(4).Value = TextBox9.Text
            DataGridView1.Rows(Row).Cells(5).Value = TextBox10.Text
            tot_harga += (DataGridView1.Rows(Row).Cells(2).Value * DataGridView1.Rows(Row).Cells(3).Value)
            tot_dikon += DataGridView1.Rows(Row).Cells(4).Value
            tot_subharga += DataGridView1.Rows(Row).Cells(5).Value
            Label14.Text = Decimal.Parse(tot_subharga.ToString).ToString("C")
            'hitung total'


            TextBox8.Text = String.Empty
            TextBox9.Text = String.Empty
            bersih()
        End If
        
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        'otomatis isi sub harga'

        If Integer.TryParse(TextBox8.Text, jumlah) And Single.TryParse(TextBox7.Text, harga) Then
            subharga = harga * jumlah
            TextBox10.Text = subharga
        Else
            TextBox10.Text = subharga
        End If

    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        'otomatis memotong subharga'

        If Integer.TryParse(TextBox9.Text, diskon) Then
            TextBox10.Text = subharga - diskon
        Else
            TextBox10.Text = subharga
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim tgl As New String(Format(DateTimePicker1.Value, "yyyy-MM-dd"))
        Dim kodepesan As String
        If TextBox1.Text = "" Or TextBox5.Text = "" Or DataGridView1.Rows.Count < 1 Then
            MsgBox("Silahkan lengkapi data!", MsgBoxStyle.Critical, "PERINGATAN")
        Else
            Try
                koneksi()
                kodepesan = kode()
                cmd = New OdbcCommand("INSERT INTO tb_pemesanan values ('" & kodepesan & "','" & TextBox1.Text & "','" & TextBox5.Text & "','" & tgl & "'," & tot_harga & "," & tot_dikon & "," & tot_subharga & ",'" & Form_utama.LabelKodePengguna.Text & "','diproses' )", con)
                cmd.ExecuteNonQuery()
                For Each row In DataGridView1.Rows
                    Using cmd As New OdbcCommand("INSERT INTO tb_pemesanandetail(kode_pesan, kode_menu, harga, jumlah, diskon, subharga) VALUES('" & kodepesan & "','" & row.Cells(0).Value & "', " & row.Cells(2).Value & ", " & row.Cells(3).Value & ", " & row.Cells(4).Value & "," & row.Cells(5).Value & ")", con)
                        cmd.ExecuteNonQuery()
                    End Using
                Next
                MsgBox("CHECKOUT BERHASIL, Silahkan cetak nota untuk pembayaran di kasir", vbInformation, "INFORMASI")
                Form_laporan.Show()
                Form_laporan.cetakpesan(kodepesan)
                DataGridView1.Rows.Clear()
                bersih()
                Label14.Text = 0
            Catch ex As Exception
                MsgBox(String.Format("Simpan data gagal. {0}", ex.Message), vbExclamation, "Terjadi Kesalahan")
            End Try
            
        End If
        
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (DataGridView1.SelectedRows.Count = 1) Then
            Dim i As Integer = DataGridView1.SelectedRows(0).Index
            tot_harga -= (DataGridView1.Rows(i).Cells(2).Value * DataGridView1.Rows(i).Cells(3).Value)
            tot_dikon -= DataGridView1.Rows(i).Cells(4).Value
            tot_subharga -= DataGridView1.Rows(i).Cells(5).Value
            Label14.Text = Decimal.Parse(tot_subharga.ToString).ToString("C")
            DataGridView1.Rows.RemoveAt(i)
        Else
            MsgBox("Data pesanan yang dihapus belum DIPILIH")
        End If
        
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (MessageBox.Show("Anda yakin membatalkan semua pesanan ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) =
           Windows.Forms.DialogResult.OK) Then
            DataGridView1.Rows.Clear()
            bersih()
            Label14.Text = 0
            tot_harga = 0
            tot_dikon = 0
            tot_subharga = 0
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        DataGridView2.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select mn.kode_menu,mn.nama_menu,mn.harga, mn.stok, kt.kategori, mn.image from tb_menu mn JOIN tb_kategori kt ON mn.kode_kategori=kt.kode_kategori where mn.kode_menu LIKE '%" & TextBox3.Text & "%' OR mn.nama_menu LIKE '%" & TextBox3.Text & "%'  OR kt.kategori LIKE '%" & TextBox3.Text & "%' order by mn.kode_menu asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView2.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5))
            Next
            dt.Rows.Clear()
            If DataGridView2.CurrentCell Is Nothing Then
                MsgBox("Data Tidak ditemukan!")
            End If
        Catch ex As Exception
            MsgBox(String.Format("Pencarian data GAGAL. {0}", ex.Message), vbExclamation, "Pencarian data GAGAL")
        End Try
    End Sub
End Class