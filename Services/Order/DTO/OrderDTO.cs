namespace Order.DTO
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public long BookId { get; set; }
        public DateTime checkoutDate { get; set; }
    }
}
