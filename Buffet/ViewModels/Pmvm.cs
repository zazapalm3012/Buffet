using Microsoft.AspNetCore.Mvc;

namespace Buffet.ViewModels
{
    public class Pmvm : Controller
    {
        public string CourseName { get; set; } = null!;

        public double CoursePrice { get; set; }

        public string CourseType { get; set; } = null!;

        public string CourseDtl { get; set; } = null!;

        public string BookId { get; set; } = null!;

        public string CusId { get; set; } = null!;

        public string ResId { get; set; } = null!;

        public string ResName { get; set; } = null!;

        public string ResDtl { get; set; } = null!;

        public int CourseId { get; set; }

        public string BookStatus { get; set; } = null!;

        public string TableId { get; set; } = null!;

        public int? BookSeat { get; set; }

        public DateTime? SelectDate { get; set; }

        public DateTime? BookDate { get; set; }

        public string PayId { get; set; } = null!;

        public string CardId { get; set; } = null!;

        public string CardExpire { get; set; } = null!;

        public string CcvNum { get; set; } = null!;

        public string PayType { get; set; } = null!;


    }
}
