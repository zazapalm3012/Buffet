using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class ResTableTotal
{
    public string TTotalId { get; set; } = null!;

    public int Stotal { get; set; }

    public int Mtotal { get; set; }

    public int Ltotal { get; set; }
}
