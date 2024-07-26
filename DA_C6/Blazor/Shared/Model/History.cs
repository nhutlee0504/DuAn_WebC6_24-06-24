using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Shared.Model
{
    public class History
    {
        [Key]
        public int IDHistory { get; set; }

        [Column(TypeName = "varchar(20)")]
        [ForeignKey("Account")]
        public string UserName { get; set; }

        [Column(TypeName = "ntext")]
        public string Describe { get; set; }

        public Account Account { get; set; }
    }
}
