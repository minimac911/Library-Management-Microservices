namespace Review.DTO
{
    public class ReviewDTO
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public long BookId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}
