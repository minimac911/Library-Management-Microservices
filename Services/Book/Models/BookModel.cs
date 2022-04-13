using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.Data
{
    [Table("books")]
    public class BookModel
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
    }
}
