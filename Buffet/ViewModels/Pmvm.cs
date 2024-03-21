using Microsoft.AspNetCore.Mvc;

namespace Buffet.ViewModels
{
    public class Pmvm : Controller
    {
        public string CourseName { get; set; } = null!;

        public double CoursePrice { get; set; }

        public string CourseType { get; set; } = null!;

        public string BookId { get; set; } = null!;

        public string CusId { get; set; } = null!;

        public string ResId { get; set; } = null!;

        public int CourseId { get; set; }

        public string BookStatus { get; set; } = null!;

        public string TableId { get; set; } = null!;

        public int? BookSeat { get; set; }

        public DateTime? SelectDate { get; set; }

        public DateTime? BookDate { get; set; }

    }
}
