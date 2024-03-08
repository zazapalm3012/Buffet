using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Restaurant
{
    public int ResId { get; set; }

    public string ResName { get; set; } = null!;

    public string ResPhone { get; set; } = null!;

    public string ResSeat { get; set; } = null!;

    public string ResLocation { get; set; } = null!;

    public string ResAvg { get; set; } = null!;

    public int TypeId { get; set; }
}
