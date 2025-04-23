using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

[Table("SSDs")]
public partial class Ssd
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    public int? Storage { get; set; }

    public double? Price { get; set; }

    public string? Image { get; set; }
}
