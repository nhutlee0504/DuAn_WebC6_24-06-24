﻿using Admin.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Model
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
