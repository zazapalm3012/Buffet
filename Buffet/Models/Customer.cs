using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buffet.Models;

public partial class Customer
{
    [Key]
    public string CusId { get; set; } = null!;

    public string CusName { get; set; } = null!;

    public string CusPass { get; set; } = null!;

    public string CusEmail { get; set; } = null!;

    public string CusPhone { get; set; } = null!;

    public string? CusImg { get; set; }
}
