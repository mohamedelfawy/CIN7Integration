using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration.Models
{
    public class ECommerceOrderApiModel
    {

        public ECommerceOrderApiModel()
        {
            LineItems = new List<ECommerceCheckOutLineApiModel>();
        }
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

        public double SubTotalPrice { get; set; }

        public double TotalTax { get; set; }

        public double TotalDiscount { get; set; }

        public double ShippingPrice { get; set; }

        public double TotalPrice { get; set; }
        public double TotalRefund { get; set; }
        public double TotalAdjustments { get; set; }
        public int TotalRefundedQuantity { get; set; }

        public int NumOfLines { get; set; }

        public int NumOfItems { get; set; }

        public string FinancialStatus { get; set; }

        public string ShippingStatus { get; set; }

        public string Currency { get; set; }

        public string CancelReason { get; set; }

        public string coupon { get; set; }

        public List<ECommerceCheckOutLineApiModel> LineItems { get; set; }

        public ECommerceOrderCampaignApiModel OrderCampaign { get; set; }

        public ContactCSVApiModel Customer { get; set; }

        public string SourceName { get; set; }
        public string Tags { get; set; }

    }
    public class ECommerceCheckOutLineApiModel
    {
        public double Quantity { get; set; }

        public double Price { get; set; }

        public string ProviderProductVariantId { get; set; }

        public string ProviderProductId { get; set; }
    }
    public class ECommerceOrderCampaignApiModel
    {
        public string source { get; set; }
        public string medium { get; set; }
        public string campaign { get; set; }

    }

    public class ContactCSVApiModel
    {
        public ContactCSVApiModel()
        {
            CreatedOn = DateTime.Now;
        }
   
        public string Name { get; set; }
     
        public string Gender { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
        public string Organization { get; set; }
        public string Tags { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Lists { get; set; }
        public string Description { get; set; }
        public string ContactInfoAddressLine1 { get; set; }
        public string ContactInfoAddressLine2 { get; set; }
        public string ContactInfoAddressPostalCode { get; set; }
        public string ContactInfoAddressCity { get; set; }
        public string ContactInfoAddressCountry { get; set; }
        public string ContactInfoPrimaryEmail { get; set; }
        public string ContactInfoSecondaryEmail { get; set; }
        public string ContactInfoTwitter { get; set; }
        public string ContactInfoskype { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }

        public string ContactInfoFacebook { get; set; }
        public string ContactInfoAddressState { get; set; }
        public string ContactInfoLinkedIn { get; set; }
        public string ContactInfoSecondaryPhoneNumber { get; set; }
        public string ContactInfoPrimaryPhoneNumber { get; set; }
        public string ContactInfoWebsite { get; set; }
        public string Subscribed { get; set; }
        public string ContactSource { get; set; }

        public int EcommerceScore { get; set; }
        public string EmailValue { get; set; }

        public string ExternalProviderName { get; set; }
        public string ExternalProviderId { get; set; }

    }



}
