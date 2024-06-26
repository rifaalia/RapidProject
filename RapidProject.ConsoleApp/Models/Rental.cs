﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RapidProject.ConsoleApp.Models;

public partial class Rental
{
    [Key]
    [Column("RentalID")]
    public int RentalId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [Column("VehicleID")]
    public int VehicleId { get; set; }

    public DateOnly RentalDate { get; set; }

    public DateOnly ReturnDate { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; }
}