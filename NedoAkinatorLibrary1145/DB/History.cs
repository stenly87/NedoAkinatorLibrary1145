using System;
using System.Collections.Generic;

namespace NedoAkinatorLibrary1145.DB;

public partial class History
{
    public int Id { get; set; }

    public int? IdCharacter { get; set; }

    public virtual ICollection<Cross> Crosses { get; set; } = new List<Cross>();

    public virtual Character? IdCharacterNavigation { get; set; }
}
