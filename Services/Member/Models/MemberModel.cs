using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Member.Models
{
    [Table("members")]
    public class MemberModel
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
