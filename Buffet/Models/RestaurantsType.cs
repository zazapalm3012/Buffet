using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class RestaurantsType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string TypeDtl { get; set; } = null!;
}
