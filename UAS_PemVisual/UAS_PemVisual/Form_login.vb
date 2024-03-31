'Program ini menggunakan Odbc untuk terhubung ke database MySQL'
'Deklrasikan perintah di bawah ini'
Imports System.Data.Odbc


Public Class Form_login
    'Deklarasi variabel u keperluan koneksi dan pengelolaan database'
    Dim con As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim dt As DataTable
    Dim cmd As OdbcCommand

    'Membuat Sub Koneksi : terdapat disetiap form '
    Public Sub koneksi()
        Try
            'Disini menggunakan DSN dengan nama : kasir_restoran // cek Odbc'
            con = New OdbcConnection("dsn=kasir_restoran")

            'cek apakah koneksi tertutup, jika ya maka open'
            If con.State = ConnectionState.Closed Then
                con.Open()
                'Koneksi Terbuka /Berhasil' 

            End If
        Catch ex As Exception 'Jika Koneksi Gagal muncul pesan'
            MsgBox(String.Format("Koneksi Gagal. {0}", ex.Message), vbExclamation, "Koneksi Gagal")
        End Try
    End Sub

    'saat mengatur background pada form, saat dijalankan program akan berkedip-kedip karena melakukan banyak render sehingga program mengalami lag/lambat'
    'untuk mengatasinya dapat menggunakan Property CreateParams() sehingga form yg memiliki banyak gambar atau style dapat ditampilkan lebih halus dan rendernya menjadi lebih cepat'
    Protected Overrides ReadOnly Property CreateParams() As Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Private Sub Form_login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Form_login_Load memanggil koneksi'
        koneksi()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'cek apakah user tersedia di sistem'
        cmd = New OdbcCommand("select count(*) as ada, usr.* from tb_pengguna usr where email = '" & TextBox1.Text & "' AND password = '" & TextBox2.Text & "'", con)
        dr = cmd.ExecuteReader
        dr.Read()
        Dim cek As Integer = dr.Item("ada")
        Dim hakakses As String = dr.Item("hak_akses").ToString()
        Dim stts As String = dr.Item("status").ToString()
        If cek > 0 And stts = "Aktif" Then 'Jika user ada dan status aktif, Login berhasil'
            MsgBox("Login Berhasil.", MsgBoxStyle.Information, "INFORMASI")
            Form_utama.LabelNama.Text = dr.Item("nama_lengkap")
            Form_utama.LabelEmail.Text = dr.Item("email")
            Form_utama.LabelHakakses.Text = dr.Item("hak_akses")
            Form_utama.LabelKodePengguna.Text = dr.Item("kode_pengguna")

            'Pembagian Hak Akses'
            If (hakakses = "Admin") Then 'Admin = Semua Menu Tampil'
                Form_utama.Show()
            ElseIf (hakakses = "Kasir") Then 'Kasir = Menu Pelayan dan Menu Master disembuyikan'
                Form_utama.MenuMaster.Visible = False
                Form_utama.MenuPelayan.Visible = False

                'membatasi laporan yg bisa diakses'
                Form_utama.DataMenuToolStripMenuItem.Visible = False
                Form_utama.LapPenggunaToolStripMenuItem.Visible = False
                Form_utama.LapKatiToolStripMenuItem1.Visible = False
                Form_utama.Show()
            ElseIf (hakakses = "Pelayan") Then 'Pelayan = Menu Master, MKasir, MLaporan disembunyikan'
                Form_utama.MenuMaster.Visible = False
                Form_utama.MenuKasir.Visible = False
                Form_utama.MenuLaporan.Visible = False
                Form_utama.Show()
            End If
            Me.Hide()
        ElseIf stts = "Tidak Aktif" Then 'Kondisi jika status tidak aktif'
            MsgBox("Login Gagal, pengguna telah dinonaktifkan!", MsgBoxStyle.Critical, "PERINGATAN")
        Else
            MsgBox("Login Gagal!", MsgBoxStyle.Critical, "PERINGATAN")
        End If

        TextBox1.Clear()
        TextBox2.Clear()
    End Sub
End Class