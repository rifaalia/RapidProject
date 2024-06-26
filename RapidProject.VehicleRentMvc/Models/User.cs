﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RapidProject.VehicleRentMvc.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [StringLength(25)]
    [Unicode(false)]
    public string Email { get; set; }

    [Required]
    [StringLength(25)]
    [Unicode(false)]
    public string Password { get; set; }

    public byte Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    [InverseProperty("User")]
    public virtual UserProfile UserProfile { get; set; }
}