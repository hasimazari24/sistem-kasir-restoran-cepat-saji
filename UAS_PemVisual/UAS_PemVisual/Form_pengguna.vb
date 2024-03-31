Imports System.Data.Odbc

Public Class Form_pengguna
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

    Sub tampiluser()
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select * from tb_pengguna order by kode_pengguna asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5), row(6), row(7))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox("Menampilkan data GAGAL")
        End Try
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Sub bersih()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.Text = ""
        TextBox5.Clear()
        TextBox6.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        TextBox1.Select()
    End Sub

    Sub kode()
        Call koneksi()
        cmd = New OdbcCommand("select kode_pengguna from tb_pengguna order by kode_pengguna desc", con)
        dr = cmd.ExecuteReader
        dr.Read()

        If Not dr.HasRows Then
            TextBox7.Text = "USR" + Format(Today, "ddMMyy") + "0001"
        Else
            'jika sudah ada kode di tanggal yg sama maka tinggal tambah 1 di urutan digit terakhir'
            '..MID menghitung string di tengah2 nya'
            '..Right menghitung string dari kanan string'
            If Microsoft.VisualBasic.Mid(dr.Item("kode_pengguna"), 4, 6) = Format(Today, "ddMMyy") Then
                TextBox7.Text = "USR" + Format(Today, "ddMMyy") + Format(Microsoft.VisualBasic.Right(dr.Item("kode_pengguna"), 4) + 1, "0000")
            Else
                'jika belum ada kode sama sekali di tanggal hari ini'
                TextBox7.Text = "USR" + Format(Today, "ddMMyy") + "0001"
            End If
        End If
    End Sub

    Private Sub Form_pengguna_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        koneksi()
        Me.WindowState = FormWindowState.Maximized
        tampiluser()
        kode()
        bersih()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        bersih()
        kode()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        bersih()
        kode()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox1.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        tampiluser()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'cek radio button utk ambil nilai status'
        Dim status As String
        If RadioButton1.Checked Then
            status = RadioButton1.Text
        Else
            status = RadioButton2.Text
        End If

        If TextBox1.Text = "" Then
            MsgBox("Silahkan lengkapi data!", MsgBoxStyle.Critical, "PERINGATAN")
        Else
            'cek apakah kode kategori ada jika ada maka update jika tidak maka insert'
            cmd = New OdbcCommand("select count(*) as ada from tb_pengguna where kode_pengguna = '" & TextBox7.Text & "'", con)
            dr = cmd.ExecuteReader
            dr.Read()
            Dim cek As Integer = dr.Item("ada")
            If cek > 0 Then
                Try
                    cmd = New OdbcCommand("update tb_pengguna set nama_lengkap = '" & TextBox1.Text & "', email = '" & TextBox2.Text & "', password = '" & TextBox3.Text & "', status = '" & status & "', hak_akses = '" & ComboBox1.Text & "', no_hp = '" & TextBox5.Text & "', alamat = '" & TextBox6.Text & "' where kode_pengguna = '" & TextBox7.Text & "'", con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Mengubah data BERHASIL", vbInformation, "INFORMASI")
                    tampiluser()
                    bersih()
                    kode()
                Catch ex As Exception
                    MsgBox("Mengubah data GAGAL", vbInformation, "PERINGATAN")
                End Try
            Else
                Try
                    'cek apakah kode kategori ada jika ada maka update jika tidak maka insert'
                    cmd = New OdbcCommand("select count(*) as ada from tb_pengguna where email = '" & TextBox2.Text & "'", con)
                    dr = cmd.ExecuteReader
                    dr.Read()
                    Dim email As Integer = dr.Item("ada")
                    If email > 0 Then
                        MsgBox("E-Mail sudah terdaftar!", MsgBoxStyle.Critical, "PERINGATAN")
                        TextBox2.Focus()
                    Else
                        cmd = New OdbcCommand("insert into tb_pengguna values('" & TextBox7.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & status & "','" & ComboBox1.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "')", con)
                        cmd.ExecuteNonQuery()
                        MsgBox("Menyimpan data BERHASIL", vbInformation, "INFORMASI")
                        tampiluser()
                        bersih()
                        kode()
                    End If
                Catch ex As Exception
                    MsgBox("Menyimpan data GAGAL", vbInformation, "PERINGATAN")
                End Try
            End If
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim a As String = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        If a = "" Then
            MsgBox("Data Menu yang dihapus belum DIPILIH")
        Else
            If (MessageBox.Show("Anda yakin menghapus data dengan Kode Pengguna = " & a &
           "?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) =
           Windows.Forms.DialogResult.OK) Then
                koneksi()
                cmd = New OdbcCommand("delete from tb_pengguna where kode_pengguna='" & a & "'", con)
                cmd.ExecuteNonQuery()
                MsgBox("Menghapus data BERHASIL", vbInformation, "INFORMASI")
                con.Close()
                tampiluser()
                bersih()
                kode()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox7.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        If DataGridView1.Rows(e.RowIndex).Cells(4).Value = RadioButton1.Text Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If
        ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        TextBox6.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select * from tb_pengguna where kode_pengguna LIKE '%" & TextBox4.Text & "%' OR nama_lengkap LIKE '%" & TextBox4.Text & "%' OR email LIKE '%" & TextBox4.Text & "%' OR status LIKE '%" & TextBox4.Text & "%' OR hak_akses LIKE '%" & TextBox4.Text & "%' OR no_hp LIKE '%" & TextBox4.Text & "%' OR alamat LIKE '%" & TextBox4.Text & "%' order by kode_pengguna asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5), row(6), row(7))
            Next
            dt.Rows.Clear()
            If DataGridView1.CurrentCell Is Nothing Then
                MsgBox("Data Tidak ditemukan!")
            End If
        Catch ex As Exception
            MsgBox(String.Format("Pencarian data GAGAL. {0}", ex.Message), vbExclamation, "Pencarian data GAGAL")
        End Try
    End Sub
End Class