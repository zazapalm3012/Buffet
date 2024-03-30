using System.ComponentModel.DataAnnotations;

namespace Buffet.ViewModels
{
    public class Pdvm
    {
        public string ResId { get; set; } = null!;

        public string ResName { get; set; } = null!;

        public string ResPhone { get; set; } = null!;

        public string ResLocation { get; set; } = null!;

        public string ResAvg { get; set; } = null!;

        public string TypeId { get; set; } = null!;

        public string ResDtl { get; set; } = null!;

        public string TableId { get; set; } = null!;

        public string TablesetIds { get; set; } = null!;

        public int CourseId { get; set; }


        public string CourseName { get; set; } = null!;

        public double CoursePrice { get; set; }

        public string CourseType { get; set; } = null!;

        public string CourseDtl { get; set; } = null!;


    }
}
