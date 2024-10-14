using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NedoAkinatorLibrary1145.DB;

public partial class Character
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
    [NotMapped]
    public double Rank { get; set; }
}
