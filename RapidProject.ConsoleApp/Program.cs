
﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RapidProject.ConsoleApp.Models;

namespace RapidProject.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VehicleDbContext>();
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1435;Database=VehicleDb;uid=sa;pwd=Password#123;TrustServerCertificate=True;");

            using (var context = new VehicleDbContext(optionsBuilder.Options))
            {
                while (true)
                {
                    Console.WriteLine("Menu:");
                    Console.WriteLine("1. Masukkan data baru");
                    Console.WriteLine("2. Tampilkan data yang ada");
                    Console.WriteLine("3. Update data yang sudah ada");
                    Console.WriteLine("4. Delete data yang ada");
                    Console.WriteLine("5. Exit");
                    Console.Write("Pilih opsi: ");

                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            AddNewVehicle(context);
                            break;
                        case "2":
                            DisplayVehicles(context);
                            break;
                        case "3":
                            UpdateVehicle(context);
                            break;
                        case "4":
                            DeleteVehicle(context);
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Opsi tidak valid. Silakan coba lagi.");
                            break;
                    }
                }
            }
        }

        static void AddNewVehicle(VehicleDbContext context)
        {
            var vehicle = new Vehicle();

            Console.Write("Masukkan Make: ");
            vehicle.Make = Console.ReadLine();

            Console.Write("Masukkan Model: ");
            vehicle.Model = Console.ReadLine();

            Console.Write("Masukkan Year: ");
            vehicle.Year = int.Parse(Console.ReadLine());

            Console.Write("Masukkan RentalPrice: ");
            vehicle.RentalPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Masukkan AvailabilityStatus: (0/1) ");
            vehicle.AvailabilityStatus = int.Parse(Console.ReadLine());

            Console.Write("Masukkan VehicleTypeId: ");
            vehicle.VehicleTypeId = int.Parse(Console.ReadLine());

            context.Vehicles.Add(vehicle);
            context.SaveChanges();
            Console.WriteLine("Data kendaraan berhasil ditambahkan.");
        }


        static void DisplayVehicles(VehicleDbContext context)
        {
            try
            {
                var vehicles = context.Vehicles
                    .Include(v => v.VehicleType) // Properti navigasi
                    .ToList();
                if (vehicles.Count == 0)
                {
                    Console.WriteLine(vehicles.Count);
                    return;
                }

                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine($"ID: {vehicle.VehicleId}, Merk: {vehicle.Make}, Model: {vehicle.Model}, Tahun: {vehicle.Year}, Harga Sewa: {vehicle.RentalPrice}, Tipe: {vehicle.VehicleType.VehicleTypeName}, Status: {(vehicle.AvailabilityStatus == 1 ? "Available" : "Not Available")}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat menampilkan data kendaraan: {ex.Message}");
            }
        }




        static void UpdateVehicle(VehicleDbContext context)
        {
            DisplayVehicles(context);
            Console.Write("Masukkan ID kendaraan yang ingin di-update: ");
            var id = int.Parse(Console.ReadLine());

            var vehicle = context.Vehicles.Find(id);
            if (vehicle == null)
            {
                Console.WriteLine("Kendaraan tidak ditemukan.");
                return;
            }

            Console.Write("Masukkan Make baru: ");
            vehicle.Make = Console.ReadLine();

            Console.Write("Masukkan Model baru: ");
            vehicle.Model = Console.ReadLine();

            Console.Write("Masukkan Year baru: ");
            vehicle.Year = int.Parse(Console.ReadLine());

            Console.Write("Masukkan RentalPrice baru: ");
            vehicle.RentalPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Masukkan AvailabilityStatus baru: ");
            vehicle.AvailabilityStatus = int.Parse(Console.ReadLine());

            Console.Write("Masukkan VehicleTypeId baru: ");
            vehicle.VehicleTypeId = int.Parse(Console.ReadLine());

            context.SaveChanges();
            Console.WriteLine("Data kendaraan berhasil di-update.");
        }

        static void DeleteVehicle(VehicleDbContext context)
        {
            DisplayVehicles(context);
            Console.Write("Masukkan ID kendaraan yang ingin dihapus: ");
            var id = int.Parse(Console.ReadLine());

            var vehicle = context.Vehicles.Find(id);
            if (vehicle == null)
            {
                Console.WriteLine("Kendaraan tidak ditemukan.");
                return;
            }

            context.Vehicles.Remove(vehicle);
            context.SaveChanges();
            Console.WriteLine("Data kendaraan berhasil dihapus.");
        }
    }
}



