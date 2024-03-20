namespace Buffet.ViewModels
{
    public class Pdvm
    {
        public string CourseId { get; set; } = null;
        public string ResId { get; set; } = null!;

        public string ResName { get; set; } = null!;

        public string ResPhone { get; set; } = null!;

        public string ResLocation { get; set; } = null!;

        public string ResAvg { get; set; } = null!;

        public string TypeId { get; set; } = null!;

        public string? TableId { get; set; }

        public string? ThemeId { get; set; }

        public string? ResImg { get; set; }

    }
}
