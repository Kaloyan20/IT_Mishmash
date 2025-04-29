using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

[Table("PSUs")]
public partial class Psu
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    public int? Wattage { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Rating { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Size { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Color { get; set; }

    public double? Price { get; set; }

    [JsonIgnore]
    public string? Image { get; set; }
}
