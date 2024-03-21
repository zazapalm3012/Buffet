namespace Buffet.ViewModels
{
    public class Pdvm
    {
        public int CourseId { get; set; }
        public string ResId { get; set; } = null!;

        public string ResName { get; set; } = null!;

        public string ResPhone { get; set; } = null!;

        public string ResLocation { get; set; } = null!;

        public string ResAvg { get; set; } = null!;

        public string TypeId { get; set; } = null!;

        public string? TableId { get; set; }

        public string? ThemeId { get; set; }

        public string? ResImg { get; set; }

        public string? ResDtl { get; set; }

        public string CourseName { get; set; } = null!;

        public double CoursePrice { get; set; }

        public string CourseType { get; set; } = null!;

        public string? CourseDtl { get; set; }

    }
}
