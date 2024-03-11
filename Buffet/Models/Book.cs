using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Book
{
    public int BookId { get; set; }

    public int CusId { get; set; }

    public int ResId { get; set; }

    public int CourseId { get; set; }

    public DateTime BookDate { get; set; }

    public int BookStatus { get; set; }

    public string? TableId { get; set; }
}
