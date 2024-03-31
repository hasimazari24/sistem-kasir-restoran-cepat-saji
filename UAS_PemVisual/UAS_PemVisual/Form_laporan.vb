Imports System.Data
Imports System.Data.Odbc
Imports Microsoft.Reporting.WinForms

Public Class Form_laporan
    Dim con As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim dt As DataTable
    Dim cmd As OdbcCommand

    Sub koneksi()
        con = New OdbcConnection
        con.ConnectionString = "dsn=kasir_restoran"
        con.Open()
    End Sub

    Public Sub tampilmenu()
        koneksi()
        da = New OdbcDataAdapter("select mn.kode_menu,mn.nama_menu,mn.harga, mn.stok, kt.kategori, mn.image from tb_menu mn JOIN tb_kategori kt ON mn.kode_kategori=kt.kode_kategori order by mn.kode_menu asc", con)
        dt = New DataTable
        da.Fill(dt)

        With Me.ReportViewer1.LocalReport
            .DataSources.Clear()
            .ReportPath = Application.StartupPath & "\Report1.rdlc"
            .EnableExternalImages = True
            .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        End With
        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Public Sub tampilkategori()
        koneksi()
        da = New OdbcDataAdapter("select * from tb_kategori order by kode_kategori asc", con)
        dt = New DataTable
        da.Fill(dt)

        With Me.ReportViewer1.LocalReport
            .DataSources.Clear()
            .ReportPath = Application.StartupPath & "\Report3.rdlc"
            .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt)
)
        End With
        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Public Sub tampilpengguna()
        koneksi()
        da = New OdbcDataAdapter("SELECT * FROM tb_pengguna order by kode_pengguna asc", con)
        dt = New DataTable
        da.Fill(dt)

        With Me.ReportViewer1.LocalReport
            .DataSources.Clear()
            .ReportPath = Application.StartupPath & "\Report2.rdlc"
            .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt)
)
        End With
        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Public Sub cetakpesan(ByVal kodepesan As String)
        koneksi()
        da = New OdbcDataAdapter("SELECT * FROM tb_pemesanan where kode_pesan = '" & kodepesan & "'", con)
        dt = New DataTable
        da.Fill(dt)

        With Me.ReportViewer1.LocalReport
            .DataSources.Clear()
            .ReportPath = Application.StartupPath & "\Report5_cetakpesan.rdlc"
            .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt)
            )
        End With
        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Public Sub cetakpembayaran(ByVal kodebyr As String)
        koneksi()
        da = New OdbcDataAdapter("SELECT pbr.kode_bayar,pbr.tgl_bayar, psn.nama_pemesan, psn.no_meja, mn.nama_menu, psn_dt.harga, psn_dt.jumlah, psn_dt.diskon, psn_dt.subharga, psn.total_harga, psn.total_diskon, psn.total_subharga, pbr.jml_bayar, pbr.kembalian FROM tb_pembayaran pbr JOIN tb_pemesanan psn ON pbr.kode_pesan = psn.kode_pesan JOIN tb_pemesanandetail psn_dt ON psn.kode_pesan = psn_dt.kode_pesan JOIN tb_menu mn ON psn_dt.kode_menu = mn.kode_menu WHERE pbr.kode_bayar = '" & kodebyr & "'", con)
        dt = New DataTable
        da.Fill(dt)

        With Me.ReportViewer1.LocalReport
            .DataSources.Clear()
            .ReportPath = Application.StartupPath & "\Report6_cetakbayar.rdlc"
            .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt)
            )
        End With
        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Public Sub cetakdapur(ByVal kodepsn As String)
        koneksi()
        da = New OdbcDataAdapter("select dt.kode_menu,mn.nama_menu, dt.jumlah, psn.no_meja,psn.nama_pemesan from tb_pemesanan psn JOIN tb_pemesanandetail dt ON psn.kode_pesan=dt.kode_pesan JOIN tb_menu mn ON dt.kode_menu=mn.kode_menu WHERE psn.kode_pesan = '" & kodepsn & "'", con)
        dt = New DataTable
        da.Fill(dt)

        With Me.ReportViewer1.LocalReport
            .DataSources.Clear()
            .ReportPath = Application.StartupPath & "\Report7_cetakdapur.rdlc"
            .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt)
            )
        End With
        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Public Sub laporantransaksi()
        koneksi()
        da = New OdbcDataAdapter("select psn.kode_pesan,psn.tgl_pesan, dt.kode_menu, mn.nama_menu, dt.harga, dt.jumlah, dt.diskon, dt.subharga from tb_pemesanan psn JOIN tb_pemesanandetail dt ON psn.kode_pesan=dt.kode_pesan JOIN tb_menu mn ON dt.kode_menu=mn.kode_menu", con)
        dt = New DataTable
        da.Fill(dt)

        With Me.ReportViewer1.LocalReport
            .DataSources.Clear()
            .ReportPath = Application.StartupPath & "\Report4.rdlc"
            .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt)
            )
        End With
        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub Form4_reportviewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        koneksi()
    End Sub

End Class

