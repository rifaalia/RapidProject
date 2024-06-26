
﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RapidProject.ConsoleApp.Models;
using ConsoleTables;

namespace RapidProject.ConsoleApp 
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting application...");
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<VehicleDbContext>();
                /*optionsBuilder.UseSqlServer("Server=.;Database=RentVehicleDB;Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=true;")*/
                ;
                optionsBuilder.UseSqlServer("Server=127.0.0.1,1435;Database=VehicleDb;uid=sa;pwd=Password#123;TrustServerCertificate=True;");
                using (var context = new VehicleDbContext(optionsBuilder.Options))
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Menu:");
                        Console.WriteLine("1. Masukkan data kendaraan baru");
                        Console.WriteLine("2. Tampilkan data kendaaraan yang tersedia");
                        Console.WriteLine("3. Tampilkan data mobil yang tersewa");
                        Console.WriteLine("4. Tampilkan seluruh kendaraan");
                        Console.WriteLine("5. Update data kendaraan yang sudah ada");
                        Console.WriteLine("6. Delete data kendaraan yang ada");
                        Console.WriteLine("7. Exit");
                        Console.Write("Pilih opsi: ");

                        var input = Console.ReadLine();

                        switch (input)
                        {
                            case "1":
                                AddNewVehicle(context);
                                break;
                            case "2":
                                DisplayVehiclesAvailable(context);
                                break;
                            case "3":
                                DisplayVehiclesRented(context);
                                break;
                            case "4":
                                DisplayAllVehicles(context);
                                break;
                            case "5":
                                UpdateVehicle(context);
                                break;
                            case "6":
                                DeleteVehicle(context);
                                break;
                            case "7":
                                return;
                            default:
                                Console.WriteLine("Opsi tidak valid. Silakan coba lagi.");
                                break;
                        }

                        Console.WriteLine("Tekan tombol apapun untuk melanjutkan...");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
                Console.WriteLine("Tekan tombol apapun untuk keluar...");
                Console.ReadKey();
            }
        }

        static void AddNewVehicle(VehicleDbContext context)
        {
            Console.Clear();
            var vehicle = new Vehicle();

            Console.Write("Masukkan Make: ");
            vehicle.Make = Console.ReadLine();

            Console.Write("Masukkan Model: ");
            vehicle.Model = Console.ReadLine();

            Console.Write("Masukkan Year: ");
            vehicle.Year = Console.ReadLine();

            Console.Write("Masukkan RentalPrice: ");
            if (!decimal.TryParse(Console.ReadLine(), out var rentalPrice))
            {
                Console.WriteLine("Harga sewa tidak valid.");
                return;
            }
            vehicle.RentalPrice = rentalPrice;

            Console.Write("Masukkan AvailabilityStatus: (0/1) ");
            if (!int.TryParse(Console.ReadLine(), out var availabilityStatus) || (availabilityStatus != 0 && availabilityStatus != 1))
            {
                Console.WriteLine("Status ketersediaan tidak valid.");
                return;
            }
            vehicle.AvailabilityStatus = availabilityStatus;

            Console.Write("Masukkan VehicleTypeName: ");
            string vehicleTypeName = Console.ReadLine();

            var vehicleType = context.VehicleTypes.FirstOrDefault(vt => vt.VehicleType1 == vehicleTypeName);
            if (vehicleType == null)
            {
                Console.WriteLine();
                Console.WriteLine("VehicleType tidak ditemukan. Menambahkan VehicleType baru...");
                vehicleType = new VehicleType { VehicleType1 = vehicleTypeName };
                context.VehicleTypes.Add(vehicleType);
                context.SaveChanges();
                Console.WriteLine("VehicleType baru berhasil ditambahkan.");
            }

            vehicle.VehicleTypeId = vehicleType.VehicleTypeId;
            vehicle.VehicleType = vehicleType;

            context.Vehicles.Add(vehicle);
            context.SaveChanges();
            Console.WriteLine("Data kendaraan berhasil ditambahkan.");
        }

        static void DisplayVehiclesAvailable(VehicleDbContext context)
        {
            Console.Clear();
            try
            {
                var vehicles = context.Vehicles
                    .Include(v => v.VehicleType)
                    .Where(v => v.AvailabilityStatus == 1).ToList();

                if (vehicles.Count == 0)
                {
                    Console.WriteLine("Tidak ada kendaraan yang tersedia.");
                    return;
                }

                var table = new ConsoleTable("ID", "Make", "Model", "Year", "Rental Price", "Type", "Status");
                foreach (var vehicle in vehicles)
                {
                    table.AddRow(vehicle.VehicleId, vehicle.Make, vehicle.Model, vehicle.Year, vehicle.RentalPrice, vehicle.VehicleType.VehicleType1, vehicle.AvailabilityStatus == 1 ? "Available" : "Not Available");
                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat menampilkan data kendaraan: {ex.Message}");
            }
        }

        static void DisplayVehiclesRented(VehicleDbContext context)
        {
            Console.Clear();
            try
            {
                var vehicles = context.Vehicles
                    .Include(v => v.VehicleType)
                    .Where(v => v.AvailabilityStatus == 0).ToList();

                if (vehicles.Count == 0)
                {
                    Console.WriteLine("Tidak ada kendaraan yang tersewa.");
                    return;
                }

                var table = new ConsoleTable("ID", "Make", "Model", "Year", "Rental Price", "Type", "Status");
                foreach (var vehicle in vehicles)
                {
                    table.AddRow(vehicle.VehicleId, vehicle.Make, vehicle.Model, vehicle.Year, vehicle.RentalPrice, vehicle.VehicleType.VehicleType1, vehicle.AvailabilityStatus == 1 ? "Available" : "Not Available");
                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat menampilkan data kendaraan: {ex.Message}");
            }
        }

        static void DisplayAllVehicles(VehicleDbContext context)
        {
            Console.Clear();
            try
            {
                var vehicles = context.Vehicles
                    .Include(v => v.VehicleType)
                    .ToList();

                if (vehicles.Count == 0)
                {
                    Console.WriteLine("Tidak ada kendaraan yang tersedia.");
                    return;
                }

                var table = new ConsoleTable("ID", "Make", "Model", "Year", "Rental Price", "Type", "Status");
                foreach (var vehicle in vehicles)
                {
                    table.AddRow(vehicle.VehicleId, vehicle.Make, vehicle.Model, vehicle.Year, vehicle.RentalPrice, vehicle.VehicleType.VehicleType1, vehicle.AvailabilityStatus == 1 ? "Available" : "Not Available");
                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat menampilkan data kendaraan: {ex.Message}");
            }
        }

        static void UpdateVehicle(VehicleDbContext context)
        {
            DisplayAllVehicles(context);

            Console.Write("Masukkan ID kendaraan yang ingin di-update: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID tidak valid.");
                return;
            }

            var vehicle = context.Vehicles.Include(v => v.VehicleType).FirstOrDefault(v => v.VehicleId == id);
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
            vehicle.Year = Console.ReadLine();

            Console.Write("Masukkan RentalPrice baru: ");
            if (!decimal.TryParse(Console.ReadLine(), out var rentalPrice))
            {
                Console.WriteLine("Harga sewa tidak valid.");
                return;
            }
            vehicle.RentalPrice = rentalPrice;

            Console.Write("Masukkan AvailabilityStatus baru: ");
            if (!int.TryParse(Console.ReadLine(), out var availabilityStatus) || (availabilityStatus != 0 && availabilityStatus != 1))
            {
                Console.WriteLine("Status ketersediaan tidak valid.");
                return;
            }
            vehicle.AvailabilityStatus = availabilityStatus;

            Console.Write("Masukkan VehicleTypeName baru: ");
            string vehicleTypeName = Console.ReadLine();
            var vehicleType = context.VehicleTypes.FirstOrDefault(vt => vt.VehicleType1 == vehicleTypeName);
            if (vehicleType == null)
            {
                Console.WriteLine("VehicleType tidak ditemukan. Menambahkan VehicleType baru...");
                vehicleType = new VehicleType { VehicleType1 = vehicleTypeName };
                context.VehicleTypes.Add(vehicleType);
                context.SaveChanges();
                Console.WriteLine("VehicleType baru berhasil ditambahkan.");
            }

            vehicle.VehicleTypeId = vehicleType.VehicleTypeId;
            vehicle.VehicleType = vehicleType;

            context.SaveChanges();
            Console.WriteLine("Data kendaraan berhasil di-update.");
        }

        static void DeleteVehicle(VehicleDbContext context)
        {
            DisplayAllVehicles(context);
            Console.Write("Masukkan ID kendaraan yang ingin dihapus: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID tidak valid.");
                return;
            }

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



