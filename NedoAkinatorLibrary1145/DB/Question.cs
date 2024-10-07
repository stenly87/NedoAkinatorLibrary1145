using System;
using System.Collections.Generic;

namespace NedoAkinatorLibrary1145.DB;

public partial class Question
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public virtual ICollection<Cross> Crosses { get; set; } = new List<Cross>();
}

