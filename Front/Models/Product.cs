using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public class Product
    {
        public long? ProductId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public long? CategoryId { get; set; }
        public Category Category { get; set; }
        public long? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public virtual ICollection<SellItem> SellItems { get; set; }
        public bool Deleted { get; set; }
    }
}