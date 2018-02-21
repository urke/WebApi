using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIENN.Models
{
    public class Unit
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }


        public ICollection<Product> Products { get; set; }
    }
}
