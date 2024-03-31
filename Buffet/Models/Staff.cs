using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Staff
{
    public string StaffId { get; set; } = null!;

    public string StaffName { get; set; } = null!;

    public string StaffEmail { get; set; } = null!;

    public string StaffPass { get; set; } = null!;

    public string StaffPhone { get; set; } = null!;

    public string? StaffImg { get; set; }

    public string DutyId { get; set; } = null!;
}
