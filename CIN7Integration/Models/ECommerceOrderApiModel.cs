using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration.Models
{
    public class ECommerceOrderApiModel
    {
        public string _id { get; set; }
        public string AccountId { get; set; }
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string EcommProviderName { get; set; }
        public string ProviderStoreId { get; set; }
        public string ProviderOrderId { get; set; }
        public string ProviderOrderReference { get; set; }
        public string OrderNote { get; set; }
        public string OrderCustomData { get; set; }
        public int SubTotalPrice { get; set; }
        public int TotalTax { get; set; }
        public int TotalDiscount { get; set; }
        public int ShippingPrice { get; set; }
        public int TotalPrice { get; set; }
        public int TotalRefund { get; set; }
        public int TotalAdjustments { get; set; }
        public int TotalRefundedQuantity { get; set; }
        public int NumOfLines { get; set; }
        public int NumOfItems { get; set; }
        public string FinancialStatus { get; set; }
        public string ShippingStatus { get; set; }
        public string Currency { get; set; }
        public string CancelReason { get; set; }
        public string coupon { get; set; }
        // write all in model Schema??
    
    }
}
