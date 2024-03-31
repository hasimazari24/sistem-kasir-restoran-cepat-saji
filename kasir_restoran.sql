-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 27 Jun 2022 pada 10.17
-- Versi server: 10.4.21-MariaDB
-- Versi PHP: 7.4.23

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `kasir_restoran`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_kategori`
--

CREATE TABLE `tb_kategori` (
  `kode_kategori` varchar(20) NOT NULL,
  `kategori` varchar(20) NOT NULL,
  `status` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tb_kategori`
--

INSERT INTO `tb_kategori` (`kode_kategori`, `kategori`, `status`) VALUES
('KTG1606220001', 'KFC Food', 'Aktif'),
('KTG1606220002', 'Fast Food', 'Tidak Aktif'),
('KTG2106220001', 'Paket Hemat', 'Aktif'),
('KTG2106220002', 'Ice Cream', 'Aktif'),
('KTG2106220003', 'Fast Drink', 'Tidak Aktif');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_menu`
--

CREATE TABLE `tb_menu` (
  `kode_menu` varchar(20) NOT NULL,
  `nama_menu` varchar(100) NOT NULL,
  `harga` int(11) NOT NULL,
  `stok` int(11) NOT NULL,
  `kode_kategori` varchar(20) NOT NULL,
  `image` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tb_menu`
--

INSERT INTO `tb_menu` (`kode_menu`, `nama_menu`, `harga`, `stok`, `kode_kategori`, `image`) VALUES
('MNU1706220001', 'Hamburger Bigfries Chesseburger', 20000, 18, 'KTG1606220002', '59431-king-whopper-hamburger-big-fries-cheeseburger-french.jpg'),
('MNU2006220001', 'Chicken KFC', 22000, 18, 'KTG1606220001', 'kfc_food_PNG67.png'),
('MNU2106220001', 'Paket Burger, nugget, Coca cola', 52000, 3, 'KTG2106220001', '61311-king-hamburger-nugget-cheeseburger-fries-french-burger.jpg'),
('MNU2106220002', 'Burger', 20000, 8, 'KTG1606220002', '17-hamburger-burger-png-image.jpg'),
('MNU2606220001', 'Burger Super', 110000, 50, 'KTG1606220001', '61288-king-whopper-hamburger-cheeseburger-veggie-burger-buffalo.png');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_pembayaran`
--

CREATE TABLE `tb_pembayaran` (
  `kode_bayar` varchar(20) NOT NULL,
  `kode_pesan` varchar(20) NOT NULL,
  `tgl_bayar` date NOT NULL,
  `total_bayar` int(11) NOT NULL,
  `jml_bayar` int(11) NOT NULL,
  `kembalian` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tb_pembayaran`
--

INSERT INTO `tb_pembayaran` (`kode_bayar`, `kode_pesan`, `tgl_bayar`, `total_bayar`, `jml_bayar`, `kembalian`) VALUES
('TRX2606220001', 'ORD2606220002', '2022-06-26', 40000, 50000, 10000),
('TRX2606220002', 'ORD2606220001', '2022-06-26', 40000, 40000, 0),
('TRX2606220003', 'ORD2606220002', '2022-06-26', 40000, 50000, 10000),
('TRX2606220004', 'ORD2606220002', '2022-06-26', 40000, 70000, 30000),
('TRX2606220005', 'ORD2606220002', '2022-06-26', 40000, 60000, 20000),
('TRX2606220006', 'ORD2606220002', '2022-06-26', 40000, 50000, 10000),
('TRX2606220007', 'ORD2606220001', '2022-06-26', 40000, 50000, 10000),
('TRX2606220008', 'ORD2606220001', '2022-06-26', 40000, 60000, 20000),
('TRX2606220009', 'ORD2606220002', '2022-06-26', 40000, 50000, 10000),
('TRX2606220010', 'ORD2606220002', '2022-06-26', 40000, 50000, 10000),
('TRX2606220011', 'ORD2606220002', '2022-06-26', 40000, 50000, 10000),
('TRX2606220012', 'ORD2606220003', '2022-06-26', 130000, 150000, 20000),
('TRX2606220013', 'ORD2606220004', '2022-06-26', 20000, 50000, 30000),
('TRX2706220001', 'ORD2706220001', '2022-06-27', 128000, 130000, 2000),
('TRX2706220002', 'ORD2706220004', '2022-06-27', 72000, 72000, 0),
('TRX2706220003', 'ORD2706220005', '2022-06-27', 72000, 80000, 8000);

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_pemesanan`
--

CREATE TABLE `tb_pemesanan` (
  `kode_pesan` varchar(20) NOT NULL,
  `no_meja` varchar(20) NOT NULL,
  `nama_pemesan` varchar(50) NOT NULL,
  `tgl_pesan` date NOT NULL,
  `total_harga` int(11) NOT NULL,
  `total_diskon` int(11) NOT NULL,
  `total_subharga` int(11) NOT NULL,
  `kode_pengguna` varchar(20) NOT NULL,
  `status_pesanan` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tb_pemesanan`
--

INSERT INTO `tb_pemesanan` (`kode_pesan`, `no_meja`, `nama_pemesan`, `tgl_pesan`, `total_harga`, `total_diskon`, `total_subharga`, `kode_pengguna`, `status_pesanan`) VALUES
('ORD2606220001', 'coba 1', 'saya', '2022-06-26', 42000, 2000, 40000, 'USR2006220001', 'dibayar'),
('ORD2606220002', 'cobaa', 'sayaa', '2022-06-26', 40000, 0, 40000, 'USR2006220001', 'selesai'),
('ORD2606220003', 'TB023', 'Hasim', '2022-06-26', 130000, 0, 130000, 'USR2006220001', 'selesai'),
('ORD2606220004', 'ssiii', 'haaa', '2022-06-26', 20000, 0, 20000, 'USR2006220001', 'dibayar'),
('ORD2706220001', 'huss01', 'hass', '2022-06-27', 130000, 2000, 128000, 'USR2006220001', 'selesai'),
('ORD2706220002', '454', 'fdsgfd', '2022-06-27', 50000, 0, 50000, 'USR2006220001', 'dibatalkan'),
('ORD2706220003', '001', 'sayaa', '2022-06-27', 0, 0, 0, 'USR2006220001', 'diproses'),
('ORD2706220004', '943', 'saya jam ini', '2022-06-27', 72000, 0, 72000, 'USR2006220001', 'selesai'),
('ORD2706220005', 'coba1', 'coba lagii', '2022-06-27', 72000, 0, 72000, 'USR2006220001', 'selesai');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_pemesanandetail`
--

CREATE TABLE `tb_pemesanandetail` (
  `id` int(11) NOT NULL,
  `kode_pesan` varchar(20) NOT NULL,
  `kode_menu` varchar(20) NOT NULL,
  `harga` int(11) NOT NULL,
  `jumlah` int(11) NOT NULL,
  `diskon` int(11) NOT NULL,
  `subharga` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tb_pemesanandetail`
--

INSERT INTO `tb_pemesanandetail` (`id`, `kode_pesan`, `kode_menu`, `harga`, `jumlah`, `diskon`, `subharga`) VALUES
(1, 'ORD2606220001', 'MNU2106220002', 20000, 1, 0, 20000),
(2, 'ORD2606220001', 'MNU2006220001', 22000, 1, 2000, 20000),
(3, 'ORD2606220002', 'MNU1706220001', 20000, 1, 0, 20000),
(4, 'ORD2606220002', 'MNU2106220002', 20000, 1, 0, 20000),
(5, 'ORD2606220003', 'MNU2606220001', 110000, 1, 0, 110000),
(6, 'ORD2606220003', 'MNU2106220002', 20000, 1, 0, 20000),
(7, 'ORD2606220004', 'MNU2106220002', 20000, 1, 0, 20000),
(8, 'ORD2706220001', 'MNU2606220001', 110000, 1, 0, 110000),
(9, 'ORD2706220001', 'MNU2106220002', 20000, 1, 2000, 18000),
(10, 'ORD2706220002', 'MNU2106220001', 50000, 1, 0, 50000),
(11, 'ORD2706220003', 'MNU2106220002', 20000, 1, 0, 20000),
(12, 'ORD2706220003', 'MNU2106220001', 52000, 1, 0, 52000),
(13, 'ORD2706220004', 'MNU2106220002', 20000, 1, 0, 20000),
(14, 'ORD2706220004', 'MNU2106220001', 52000, 1, 0, 52000),
(17, 'ORD2706220005', 'MNU2106220001', 52000, 1, 0, 52000),
(18, 'ORD2706220005', 'MNU1706220001', 20000, 1, 0, 20000);

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_pengguna`
--

CREATE TABLE `tb_pengguna` (
  `kode_pengguna` varchar(20) NOT NULL,
  `nama_lengkap` varchar(100) NOT NULL,
  `email` varchar(50) NOT NULL,
  `password` varchar(20) NOT NULL,
  `status` varchar(20) NOT NULL,
  `hak_akses` varchar(20) NOT NULL,
  `no_hp` varchar(14) NOT NULL,
  `alamat` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tb_pengguna`
--

INSERT INTO `tb_pengguna` (`kode_pengguna`, `nama_lengkap`, `email`, `password`, `status`, `hak_akses`, `no_hp`, `alamat`) VALUES
('USR2006220001', 'Hasim Azari', 'hasim@gmail.com', 'hsm123', 'Aktif', 'Admin', '087775668551', 'disana disini kesana kemari Indonesia'),
('USR2106220001', 'Drupadi Cantik', 'drupadi@gmail.com', 'drupadi', 'Aktif', 'Kasir', '087654123', 'Jln. Solo Barat'),
('USR2706220001', 'alwi', 'alwi@gmail.com', 'dsfhdfh', 'Aktif', 'Kasir', '463', 'ergert'),
('USR2706220002', 'saya', 'saya@gmail.com', '123', 'Aktif', 'Pelayan', '4365456', 'hfghfd');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `tb_kategori`
--
ALTER TABLE `tb_kategori`
  ADD PRIMARY KEY (`kode_kategori`);

--
-- Indeks untuk tabel `tb_menu`
--
ALTER TABLE `tb_menu`
  ADD PRIMARY KEY (`kode_menu`),
  ADD KEY `kode_kategori` (`kode_kategori`);

--
-- Indeks untuk tabel `tb_pemesanandetail`
--
ALTER TABLE `tb_pemesanandetail`
  ADD PRIMARY KEY (`id`),
  ADD KEY `kode_pesan` (`kode_pesan`),
  ADD KEY `kode_menu` (`kode_menu`);

--
-- Indeks untuk tabel `tb_pengguna`
--
ALTER TABLE `tb_pengguna`
  ADD PRIMARY KEY (`kode_pengguna`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `tb_pemesanandetail`
--
ALTER TABLE `tb_pemesanandetail`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
