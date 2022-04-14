using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Review.Models
{
    [Table("reviews")]
    public class ReviewModel
    {
        [Key]
        public long Id { get; set; }
        public long MemberId { get; set; }
        public long BookId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}
