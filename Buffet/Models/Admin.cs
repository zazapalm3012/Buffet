using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string AdminName { get; set; } = null!;

    public string AdminEmail { get; set; } = null!;

    public string AdminPass { get; set; } = null!;

    public string AdminPhone { get; set; } = null!;

    public string AdminImg { get; set; } = null!;
}
