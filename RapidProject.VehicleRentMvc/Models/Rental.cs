﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RapidProject.VehicleRentMvc.Models;

public partial class Rental
{
    [Key]
    public int RentalId { get; set; }

    public int UserId { get; set; }

    public int VehicleId { get; set; }

    public DateOnly RentalDate { get; set; }

    public DateOnly ReturnDate { get; set; }

    public byte Status { get; set; }

    [InverseProperty("Rental")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("UserId")]
    [InverseProperty("Rentals")]
    public virtual User User { get; set; }

    [ForeignKey("VehicleId")]
    [InverseProperty("Rentals")]
    public virtual Vehicle Vehicle { get; set; }
}