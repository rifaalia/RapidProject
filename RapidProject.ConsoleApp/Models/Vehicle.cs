﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RapidProject.ConsoleApp.Models;

public partial class Vehicle
{
    [Key]
    [Column("VehicleID")]
    public int VehicleId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Make { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Model { get; set; }

    public int Year { get; set; }

    [Column(TypeName = "money")]
    public decimal RentalPrice { get; set; }

    public int AvailabilityStatus { get; set; }

    public int VehicleTypeId { get; set; }

    public VehicleType? VehicleType { get; set; }
    
}