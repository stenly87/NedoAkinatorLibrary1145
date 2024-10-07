using System;
using System.Collections.Generic;

namespace NedoAkinatorLibrary1145.DB;

public partial class Character
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
