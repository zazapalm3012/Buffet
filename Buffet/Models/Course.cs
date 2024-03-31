using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public double CoursePrice { get; set; }

    public string CourseType { get; set; } = null!;

    public string CourseDtl { get; set; } = null!;

    public string ResId { get; set; } = null!;

    public string? CourseImg { get; set; }
}
