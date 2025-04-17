using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace EvoHotel
{
    class Program
    {
        static string connectionString = "Server=localhost; Database=EvoHotel; UID=dhonan; Password=Dhonan12";
            static void Main(string[] args)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== Menu Manajemen Ruangan ===");
                    Console.WriteLine("1. Tambah Ruangan");
                    Console.WriteLine("2. Lihat Ruangan");
                    Console.WriteLine("3. Edit Ruangan");
                    Console.WriteLine("4. Hapus Ruangan");
                    Console.WriteLine("5. Keluar");
                    Console.Write("Pilih opsi: ");
                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "1": TambahRuangan(); break;
                        case "2": LihatRuangan(); break;
                        case "3": EditRuangan(); break;
                        case "4": HapusRuangan(); break;
                        case "5": return;
                        default: Console.WriteLine("Pilihan tidak valid."); break;
                    }
                    Console.WriteLine("\nTekan ENTER untuk kembali ke menu.");
                    Console.ReadLine();
                }
            }

            static void TambahRuangan()
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    Console.Write("Nama Ruangan: ");
                    string nama = Console.ReadLine();
                    Console.Write("Kapasitas: ");
                    int kapasitas = int.Parse(Console.ReadLine());
                    Console.Write("Harga Per Jam: ");
                    decimal hargaPerJam = decimal.Parse(Console.ReadLine());
                    Console.Write("Harga Per Hari: ");
                    decimal hargaPerHari = decimal.Parse(Console.ReadLine());
                    Console.Write("Fasilitas: ");
                    string fasilitas = Console.ReadLine();

                    string query = "INSERT INTO Ruangan (NamaRuangan, Kapasitas, HargaPerJam, HargaPerHari, Fasilitas) VALUES (@nama, @kapasitas, @hargaPerJam, @hargaPerHari, @fasilitas)";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@kapasitas", kapasitas);
                    cmd.Parameters.AddWithValue("@hargaPerJam", hargaPerJam);
                    cmd.Parameters.AddWithValue("@hargaPerHari", hargaPerHari);
                    cmd.Parameters.AddWithValue("@fasilitas", fasilitas);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Ruangan berhasil ditambahkan!");
                }
            }

            static void LihatRuangan()
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT * FROM Ruangan", conn);
                    var reader = cmd.ExecuteReader();

                    Console.WriteLine("=== Data Ruangan ===");
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["RuanganID"]}, Nama: {reader["NamaRuangan"]}, Kapasitas: {reader["Kapasitas"]}, Status: {reader["Status"]}");
                    }
                }
            }

            static void EditRuangan()
            {
                Console.Write("Masukkan ID Ruangan yang ingin diedit: ");
                int id = int.Parse(Console.ReadLine());

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    Console.Write("Nama Ruangan baru: ");
                    string nama = Console.ReadLine();
                    Console.Write("Kapasitas baru: ");
                    int kapasitas = int.Parse(Console.ReadLine());
                    Console.Write("Harga Per Jam baru: ");
                    decimal hargaPerJam = decimal.Parse(Console.ReadLine());
                    Console.Write("Harga Per Hari baru: ");
                    decimal hargaPerHari = decimal.Parse(Console.ReadLine());
                    Console.Write("Fasilitas baru: ");
                    string fasilitas = Console.ReadLine();
                    Console.Write("Status baru (Tersedia/Dalam Perbaikan/Tidak Tersedia): ");
                    string status = Console.ReadLine();

                    string query = "UPDATE Ruangan SET NamaRuangan=@nama, Kapasitas=@kapasitas, HargaPerJam=@hargaPerJam, HargaPerHari=@hargaPerHari, Fasilitas=@fasilitas, Status=@status WHERE RuanganID=@id";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@kapasitas", kapasitas);
                    cmd.Parameters.AddWithValue("@hargaPerJam", hargaPerJam);
                    cmd.Parameters.AddWithValue("@hargaPerHari", hargaPerHari);
                    cmd.Parameters.AddWithValue("@fasilitas", fasilitas);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Ruangan berhasil diperbarui!");
                }
            }

            static void HapusRuangan()
            {
                Console.Write("Masukkan ID Ruangan yang ingin dihapus: ");
                int id = int.Parse(Console.ReadLine());

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Ruangan WHERE RuanganID=@id";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Ruangan berhasil dihapus!");
                }
            }
        }
    }