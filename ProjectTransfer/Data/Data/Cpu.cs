using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

[Table("CPUs")]
public partial class Cpu
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Brand { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    [Column("Socket_type")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SocketType { get; set; }

    public double? Price { get; set; }

    public string? Image { get; set; }
}
