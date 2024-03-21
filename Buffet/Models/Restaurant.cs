using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Restaurant
{
    public string ResId { get; set; } = null!;

    public string ResName { get; set; } = null!;

    public string ResPhone { get; set; } = null!;

    public string ResLocation { get; set; } = null!;

    public string ResAvg { get; set; } = null!;

    public string TypeId { get; set; } = null!;

    public string ThemeId { get; set; } = null!;

    public string TTotalId { get; set; } = null!;

    public string? ResImg { get; set; }

    public string? ResDtl { get; set; }

    public int CourseId { get; set; }
}
