using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public partial class Cooler
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    [Column("AIO")]
    public bool? Aio { get; set; }

    [Column("MiniITX_Compatibility")]
    public bool? MiniItxCompatibility { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Color { get; set; }

    public double? Price { get; set; }

    [JsonIgnore]
    public string? Image { get; set; }
}
