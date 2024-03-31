using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Tableset
{
    public int TablesetIds { get; set; }

    public int Ssize { get; set; }

    public int Msize { get; set; }

    public int Lsize { get; set; }

    public string? Total { get; set; }
}
