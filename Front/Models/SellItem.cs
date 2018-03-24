using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public class SellItem
    {
        public long? SellItemId { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }
        public long? ProductId { get; set; }
        public Product Product { get; set; }
        public long? SellId { get; set; }
        public Sell Sell { get; set; }
        public decimal TotalValue { get { return UnitPrice * Quantity; } }
    }
}