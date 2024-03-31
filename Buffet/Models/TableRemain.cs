using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class TableRemain
{
    public string TableId { get; set; } = null!;

    public int RemainS { get; set; }

    public int RemainM { get; set; }

    public int RemainL { get; set; }
}
