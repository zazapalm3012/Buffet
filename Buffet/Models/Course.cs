﻿using System;
using System.Collections.Generic;

namespace Buffet.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public double CoursePrice { get; set; }

    public int CourseType { get; set; }
}
