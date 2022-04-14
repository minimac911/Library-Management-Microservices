using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Models
{
    [Table("orders")]
    public class OrderModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long MemberId { get; set; }

        [Required]
        public long BookId { get; set; }

        public DateTime checkoutDate { get; set; }
    }
}
