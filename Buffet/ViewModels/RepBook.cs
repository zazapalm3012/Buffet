namespace Buffet.ViewModels
{
    public class RepBook
    {
        public string BookId { get; set; } = null!;

        public string CusId { get; set; } = null!;

        public string ResId { get; set; } = null!;

        public int CourseId { get; set; }

        public DateTime BookDate { get; set; }

        public string BookStatus { get; set; } = null!;

        public string TableId { get; set; } = null!;

        public int BookSeat { get; set; }

        public DateTime SelectDate { get; set; }
    }
}
