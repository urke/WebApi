using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.Models.DTO
{
    public class SearchDTO
    {
        public SearchDTO() { }

        SearchDTO(int typeId,
                  int categoryId,
                  int productId,
                  string description,
                  int price,
                  bool isAvailable,
                  DateTime delivaryDate
                  )
        {
            this.SearchByTypeId = typeId;
            this.SearchByCategory = categoryId;
            this.SearchByProductId = productId;
            this.SearchByDescription = description;
            this.SearchByPrice = price;
            this.SearchByIsAvailable = isAvailable;
            this.SearchByDelivaryDate = delivaryDate;
        }

        public int SearchByTypeId { get; set; }
        public int SearchByCategory { get; set; }
        public int SearchByProductId { get; set; }
        public string SearchByDescription { get; set; }
        public int SearchByPrice { get; set; }
        public bool SearchByIsAvailable { get; set; }
        public DateTime SearchByDelivaryDate { get; set; }
    }
}
