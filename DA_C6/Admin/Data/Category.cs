using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Data
{
    public class Category
    {
        [Key]
        public int IDCategory { get; set; }

        [Required]
        public string Name { get; set; }

        //public ICollection<Product> Products { get; set; }
    }
}
