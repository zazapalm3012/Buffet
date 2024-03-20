using Microsoft.AspNetCore.Mvc;

namespace Buffet.ViewModels
{
    public class Universal : Controller
    {
        public int BookId { get; set; }

        public string CusId { get; set; } = null!;

        public string ResId { get; set; } = null!;

        public int CourseId { get; set; }

        public DateTime BookDate { get; set; }

        public int BookStatus { get; set; }

        public string? TableId { get; set; }

        public int? BookSeat { get; set; }

        public DateTime? SelectDate { get; set; }
    }
}
