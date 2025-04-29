using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

[Table("RAM")]
public partial class Ram
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    [Column("Memory_Size")]
    public int? MemorySize { get; set; }

    [Column("Memory_Type")]
    [StringLength(255)]
    [Unicode(false)]
    public string? MemoryType { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Color { get; set; }

    public double? Price { get; set; }

    [JsonIgnore]
    public string? Image { get; set; }
}
