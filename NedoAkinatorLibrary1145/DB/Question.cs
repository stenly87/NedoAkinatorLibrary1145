using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NedoAkinatorLibrary1145.DB;

public partial class Question
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public virtual ICollection<Cross> Crosses { get; set; } = new List<Cross>();

    [NotMapped]
    public double Rank { get; set; } = 0;
}

