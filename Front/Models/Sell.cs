using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public class Sell
    {
        public long? SellId { get; set; }
        public string SellNumver { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string BuyerName { get; set; }
        public string BuyerDoc { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }
        public virtual ICollection<SellItem> SellItems { get; set; }
    }
}