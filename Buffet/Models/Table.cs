using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Table
{
    public string TableId { get; set; } = null!;

    public int Seat { get; set; }
}
