using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Payment
{
    public string PayId { get; set; } = null!;

    public string CardId { get; set; } = null!;

    public string CardExpire { get; set; } = null!;

    public string CcvNum { get; set; } = null!;

    public string PayType { get; set; } = null!;

    public string BookId { get; set; } = null!;
}
