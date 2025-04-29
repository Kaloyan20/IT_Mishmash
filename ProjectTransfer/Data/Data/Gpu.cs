using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

[Table("GPUs")]
public partial class Gpu
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Brand { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    public int? Memory { get; set; }

    [Column("Power_Consumption")]
    public int? PowerConsumption { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Color { get; set; }

    public double? Price { get; set; }

    [JsonIgnore]
    public string? Image { get; set; }
}
