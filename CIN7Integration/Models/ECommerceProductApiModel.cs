using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration.Models
{
    public class ECommerceProductApiModel
    {
        public ECommerceProductApiModel()
        {
            categories = new List<ECommerceCategoryApiModel>();
            variants = new List<ECommerceProductVariant>();
        }
        public string _id { get; set; }
       public string AccountId { get; set; }
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Vendor { get; set; }
        public string ECommerceProviderName { get; set; }
        public string ProviderStoreId { get; set; }
        public string ProviderProductId { get; set; }
        public string ProviderProductName { get; set; }
        public string ProductType { get; set; }
        public string tags { get; set; }
        public List<ECommerceProductVariant> variants { get; set; }
        public List<ECommerceCategoryApiModel> categories { get; set; }
        public string Image { get; set; }
        public string ProductHandle { get; set; }
    }

    public class ECommerceProductVariant
    {
        public string ProviderVariantId { get; set; }
        public string Name { get; set; }
        public string sku { get; set; }
        public string price { get; set; }
    }
}
