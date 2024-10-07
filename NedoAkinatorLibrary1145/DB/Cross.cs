using System;
using System.Collections.Generic;

namespace NedoAkinatorLibrary1145.DB;

public partial class Cross
{
    public int IdHistory { get; set; }

    public int IdQuestion { get; set; }

    public int? Reaction { get; set; }

    public virtual History IdHistoryNavigation { get; set; } = null!;

    public virtual Question IdQuestionNavigation { get; set; } = null!;
}
