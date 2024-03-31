Imports System.Data.Odbc

Public Class Form_Kategori
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

    Sub tampilkategori()
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select * from tb_kategori order by kode_kategori asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2))
            Next
        Catch ex As Exception
            MsgBox(String.Format("Menampilkan data Gagal. {0}", ex.Message), vbExclamation, "Menampilkan data Gagal")
        End Try
    End Sub

    Sub bersih()
        TextBox1.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        TextBox1.Select()
    End Sub

    Sub kode()
        Call koneksi()
        cmd = New OdbcCommand("select kode_kategori from tb_kategori order by kode_kategori desc", con)
        dr = cmd.ExecuteReader
        dr.Read()

        If Not dr.HasRows Then
            TextBox2.Text = "KTG" + Format(Today, "ddMMyy") + "0001"
        Else
            'jika sudah ada kode di tanggal yg sama maka tinggal tambah 1 di urutan digit terakhir'
            '..MID menghitung string di tengah2 nya'
            '..Right menghitung string dari kanan string'
            If Microsoft.VisualBasic.Mid(dr.Item("kode_kategori"), 4, 6) = Format(Today, "ddMMyy") Then
                TextBox2.Text = "KTG" + Format(Today, "ddMMyy") + Format(Microsoft.VisualBasic.Right(dr.Item("kode_kategori"), 4) + 1, "0000")
            Else
                'jika belum ada kode sama sekali di tanggal hari ini'
                TextBox2.Text = "KTG" + Format(Today, "ddMMyy") + "0001"
            End If
        End If
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Private Sub Form_Kategori_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        koneksi()
        tampilkategori()
        bersih()
        kode()
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        bersih()
        kode()
        TextBox1.Select()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        bersih()
        kode()
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
            cmd = New OdbcCommand("select count(*) as ada from tb_kategori where kode_kategori = '" & TextBox2.Text & "'", con)
            dr = cmd.ExecuteReader
            dr.Read()
            Dim cek As Integer = dr.Item("ada")
            If cek > 0 Then
                Try
                    cmd = New OdbcCommand("update tb_kategori set kategori = '" & TextBox1.Text & "', status = '" & status & "' where kode_kategori = '" & TextBox2.Text & "'", con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Mengubah data BERHASIL", vbInformation, "INFORMASI")
                    tampilkategori()
                    bersih()
                    kode()
                Catch ex As Exception
                    MsgBox("Mengubah data GAGAL", vbInformation, "PERINGATAN")
                End Try
            Else
                Try
                    cmd = New OdbcCommand("insert into tb_kategori values('" & TextBox2.Text & "','" & TextBox1.Text & "','" & status & "')", con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Menyimpan data BERHASIL", vbInformation, "INFORMASI")
                    tampilkategori()
                    bersih()
                    kode()
                Catch ex As Exception
                    MsgBox("Menyimpan data GAGAL", vbInformation, "PERINGATAN")
                End Try
            End If
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox1.Focus()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        DataGridView1.Rows.Clear()
        Try
            'proses pencarian berdasarkan apa yg dimasukkan di textbox 4'
            koneksi()
            da = New OdbcDataAdapter("select * from tb_kategori where kode_kategori LIKE '%" & TextBox4.Text & "%' OR kategori LIKE '%" & TextBox4.Text & "%' OR status LIKE '%" & TextBox4.Text & "%' order by kode_kategori asc", con)
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

    Private Sub DataGridView1_CellClick_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            If DataGridView1.Rows(e.RowIndex).Cells(2).Value = RadioButton1.Text Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim a As String = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        If a = "" Then
            MsgBox("Data Kategori yang dihapus belum DIPILIH")
        Else
            If (MessageBox.Show("Anda yakin menghapus data dengan Kode Kategori = " & a &
           "?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) =
           Windows.Forms.DialogResult.OK) Then
                koneksi()
                cmd = New OdbcCommand("delete from tb_kategori where kode_kategori='" & a & "'", con)
                cmd.ExecuteNonQuery()
                MsgBox("Menghapus data BERHASIL", vbInformation, "INFORMASI")
                con.Close()
                tampilkategori()
                bersih()
                kode()
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        tampilkategori()
    End Sub
End Class