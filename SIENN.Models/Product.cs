using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SIENN.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public int Price { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime DelivaryDate { get; set; }


        [Required]
        public int UnitID { get; set; }
        public Unit Unit { get; set; }

        public int? TypeId { get; set; }
        public Type Type { get; set; }


        public IList<ProductCategory> ProductCategories { get; set; }
    }
}
