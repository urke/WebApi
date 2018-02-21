using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.Models.DTO
{
    public class ProductDTO
    {
        public ProductDTO() { }
        public ProductDTO(Product product)
        {
            this.Description = String.Format(" ({0}) {1}", product.Id, product.Description);
            this.IsAvailable = product.IsAvailable ? "Available" : "Unavailable";
            this.Price = product.Price.ToString("C2");
            this.DelivaryDate = $"{product.DelivaryDate.Day}.{product.DelivaryDate.Month}.{product.DelivaryDate.Year}";
            if (product.Type != null)
                this.TypeIdDescription = String.Format(" ({0}) {1}", product.TypeId, product.Type.Description);
            else
                this.TypeIdDescription = String.Format(" ({0}) null", product.TypeId);
            if (product.Unit != null)
                this.UnitIdDescription = String.Format(" ({0}) {1}", product.UnitID, product.Unit.Description);
            else
                this.UnitIdDescription = String.Format(" ({0}) null", product.UnitID);
        }

        public int Id { get; private set; }
        public string Description { get; set; }
        public string IsAvailable { get; set; }
        public string Price { get; set; }
        public string DelivaryDate { get; set; }
        public string TypeIdDescription { get; set; }
        public string UnitIdDescription { get; set; }
    }
}
