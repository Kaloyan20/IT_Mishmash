using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public partial class Motherboard
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Brand { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? Chipset { get; set; }

    [Column("Socket_type")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SocketType { get; set; }

    [Column("Form_factor")]
    [StringLength(255)]
    [Unicode(false)]
    public string? FormFactor { get; set; }

    [Column("Memory_support")]
    [StringLength(255)]
    [Unicode(false)]
    public string? MemorySupport { get; set; }

    public double? Price { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Color { get; set; }

    [JsonIgnore]
    public string? Image { get; set; }
}
