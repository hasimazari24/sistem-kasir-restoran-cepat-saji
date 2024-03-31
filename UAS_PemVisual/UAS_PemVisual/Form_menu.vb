Imports System.Data.Odbc
Imports System.IO 'untuk membaca dan menulis file'
Imports System.Data 'untuk keperluan data'

Public Class Form_menu
    Dim con As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim dt As DataTable
    Dim cmd As OdbcCommand

    'untuk keperluan menyimpan gambar'
    Dim fileSavedPath As String 'lokasi gambar disimpan'
    Dim fileName As String 'nama file'
    Dim gambar As String ' gambar yg diklik di datagridview'
    Dim fileSource As String 'asal/sumber gambar yg ingin disimpan'

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

    'sub tampil menu digunakan u menampilkan data menu kedalam data grid view'
    Sub tampilmenu()
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select mn.kode_menu,mn.nama_menu,mn.harga, mn.stok, kt.kategori, mn.image from tb_menu mn JOIN tb_kategori kt ON mn.kode_kategori=kt.kode_kategori order by mn.kode_menu asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox("Menampilkan data GAGAL")
        End Try
    End Sub

    Sub bersih()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.Text = ""
        TextBox1.Focus()
        fileSavedPath = String.Empty
        fileName = String.Empty
        fileSource = String.Empty
        PictureBox1.Image = Nothing
    End Sub

    Sub kode()
        'perintah select pada odbccommand untuk menampilkan kode menu'
        Call koneksi()
        cmd = New OdbcCommand("select kode_menu from tb_menu order by kode_menu desc", con)
        dr = cmd.ExecuteReader
        dr.Read()

        'jika data menu kosong'
        If Not dr.HasRows Then
            'kode baru'
            TextBox5.Text = "MNU" + Format(Today, "ddMMyy") + "0001"
        Else
            'jika sudah ada kode di tanggal yg sama maka tinggal tambah 1 di urutan digit terakhir'

            If Microsoft.VisualBasic.Mid(dr.Item("kode_menu"), 4, 6) = Format(Today, "ddMMyy") Then
                TextBox5.Text = "MNU" + Format(Today, "ddMMyy") + Format(Microsoft.VisualBasic.Right(dr.Item("kode_menu"), 4) + 1, "0000")
            Else
                'jika belum ada kode sama sekali di tanggal hari ini, buat kode baru'
                TextBox5.Text = "MNU" + Format(Today, "ddMMyy") + "0001"
                'textbox5.text ini diset readonly hanya dapat dibaca'
            End If
        End If
    End Sub

    'sub tampil ktegori digunakan u menampilkan data menu kedalam combobox1'
    Sub tampilkategori()
        da = New OdbcDataAdapter("select * from tb_kategori where status='Aktif' order by kode_kategori desc", con)
        ds = New DataSet
        da.Fill(ds, "tb_kategori")
        ComboBox1.DataSource = ds.Tables("tb_kategori")
        ComboBox1.ValueMember = "kode_kategori"
        ComboBox1.DisplayMember = "kategori"
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Private Sub Form2_menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        koneksi()
        Me.WindowState = FormWindowState.Maximized 'mengubah form menjadi maksimal'
        tampilmenu() 'memanggil tampil menu'
        tampilkategori()
        kode()
        bersih()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click 'klik = u buka open dialog'

        'Deklarasi Variabel u simpan Gambar'
        Dim saveDirectory As String = Application.StartupPath & "\SavedImages"

        'perintah u mengakses file dari open dialog'
        Using OpenFileDialog1 As OpenFileDialog = New OpenFileDialog()
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then 'JIka Open Dialog terbuka = True'
                'memilih file akan diambil alamat gambar secra lengkap'
                Dim pic_file As New FileStream(OpenFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Read)
                'menaruhnya di picturbox'
                PictureBox1.Image = Image.FromStream(pic_file)
                PictureBox1.Tag = Path.GetFileName(OpenFileDialog1.FileName)
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
                pic_file.Close()

                If Not Directory.Exists(saveDirectory) Then 'Jika tdk terdapat \SavedImages, maka akan dibuat'
                    Directory.CreateDirectory(saveDirectory)
                End If
                fileSource = New String(OpenFileDialog1.FileName) 'lokasi sumber gambar yg dipilih'
                fileName = New String(Path.GetFileName(OpenFileDialog1.FileName)) 'nama file yg akan disimpan didatabase'
                fileSavedPath = New String(Path.Combine(saveDirectory, fileName)) 'menggabungkan nama lokasi saveDirectory dengan nama gambar '
            End If
        End Using
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Or PictureBox1.Image Is Nothing Then 'validasi input, jika kosong'
            MsgBox("Silahkan lengkapi data!", MsgBoxStyle.Critical, "PERINGATAN")
        Else
            Dim idkategori As String = ComboBox1.SelectedValue 'mndptkan kategori dr CB1'

            'cek apakah kode menu ada jika ada maka update jika tidak maka insert'
            cmd = New OdbcCommand("select count(*) as ada from tb_menu where kode_menu = '" & TextBox5.Text & "'", con)
            dr = cmd.ExecuteReader
            dr.Read()
            Dim cek As Integer = dr.Item("ada")
            If cek > 0 Then 'jika ada, maka update'
                Try
                    If Not fileSource = String.Empty Then
                        If File.Exists(gambar) Then
                            File.Delete(gambar)
                            File.Copy(fileSource, fileSavedPath, True)
                        Else
                            File.Copy(fileSource, fileSavedPath, True)
                        End If
                    End If

                    'perintah update menu'
                    cmd = New OdbcCommand("update tb_menu set nama_menu = '" & TextBox1.Text & "', harga = '" & TextBox2.Text & "', stok = '" & TextBox3.Text & "', kode_kategori = '" & idkategori & "', image = '" & PictureBox1.Tag.ToString & "'  where kode_menu = '" & TextBox5.Text & "'", con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Mengubah data BERHASIL", vbInformation, "INFORMASI")
                    tampilmenu()
                    bersih()
                    kode()
                Catch ex As Exception
                    MsgBox(String.Format("Mengubah data GAGAL {0}", ex.Message), MsgBoxStyle.Critical, "PERINGATAN")
                End Try
                'jikatdk ada,insert data baru'
            Else
                Try
                    File.Copy(fileSource, fileSavedPath, True) 'gambar dari filesource dicopy ke filesavedpath'
                    Dim sql As String = "insert into tb_menu values('" & TextBox5.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & idkategori & "', '" & fileName & "')"
                    cmd = New OdbcCommand(sql, con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Menyimpan data BERHASIL", vbInformation, "INFORMASI")
                    tampilmenu()
                    bersih()
                    kode()
                Catch ex As Exception
                    MsgBox(String.Format("Menyimpan data GAGAL {0}", ex.Message), vbExclamation, "PERINGATAN")
                    'MsgBox("", MsgBoxStyle.Critical, "PERINGATAN")
                End Try
            End If
        End If
    End Sub


    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            gambar = New String(Application.StartupPath & "\SavedImages\" & DataGridView1.Rows(e.RowIndex).Cells(5).Value)
            PictureBox1.Image = Nothing
            If File.Exists(gambar) Then
                Dim pic_file As New FileStream(gambar, FileMode.Open)
                PictureBox1.Image = Image.FromStream(pic_file)
                PictureBox1.Tag = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
                pic_file.Close()
            End If

        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select mn.kode_menu,mn.nama_menu,mn.harga, mn.stok, kt.kategori, mn.image from tb_menu mn JOIN tb_kategori kt ON mn.kode_kategori=kt.kode_kategori where mn.kode_menu LIKE '%" & TextBox4.Text & "%' OR mn.nama_menu LIKE '%" & TextBox4.Text & "%'  OR kt.kategori LIKE '%" & TextBox4.Text & "%' order by mn.kode_menu asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5))
            Next
            dt.Rows.Clear()
            If DataGridView1.CurrentCell Is Nothing Then
                MsgBox("Data Tidak ditemukan!")
            End If
        Catch ex As Exception
            MsgBox(String.Format("Pencarian data GAGAL. {0}", ex.Message), vbExclamation, "Pencarian data GAGAL")
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        bersih()
        kode()
        TextBox1.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox1.Focus()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        bersih()
        kode()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'validasi jika data di dgv blum dipilih'
        Dim a As String = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        If a = "" Then
            MsgBox("Data Menu yang dihapus belum DIPILIH")
        Else
            'kalau sudah dipilih muncul konfirmasi jika ya maka hapus'
            If (MessageBox.Show("Anda yakin menghapus data dengan Kode Menu = " & a &
           "?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) =
           Windows.Forms.DialogResult.OK) Then
                koneksi()
                cmd = New OdbcCommand("delete from tb_menu where kode_Menu='" & a & "'", con)
                cmd.ExecuteNonQuery()
                MsgBox("Menghapus data BERHASIL", vbInformation, "INFORMASI")
                con.Close()
                File.Delete(gambar)
                tampilmenu()
                bersih()
                kode()
            End If
        End If
    End Sub

End Class