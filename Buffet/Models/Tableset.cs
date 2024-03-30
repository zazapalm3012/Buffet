using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Tableset
{
    public int TablesetIds { get; set; }

    public string Ssize { get; set; } = null!;

    public string Msize { get; set; } = null!;

    public string Lsize { get; set; } = null!;

    public string? Total { get; set; }
}
