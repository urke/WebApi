using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.Models.DTO
{
    public class CategoryDTO
    {
        public CategoryDTO() { }
        public CategoryDTO(int id,
                           string description,
                           double meanPrice,
                           int numberOfAsignedProducts
                           )
        {
            this.Id = id;
            this.Description = description;
            this.MeanPrice = meanPrice;
            this.NumberOfAsignedProducts = numberOfAsignedProducts;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public double MeanPrice { get; set; }
        public int NumberOfAsignedProducts { get; set; }
    }
}
