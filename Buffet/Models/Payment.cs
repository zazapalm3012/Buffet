using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Payment
{
    public string? CardId { get; set; }

    public string? CardExpire { get; set; }

    public string? CcvNum { get; set; }
}
