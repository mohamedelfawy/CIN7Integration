using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration.Models
{
   public class ECommerceCategoryApiModel
    {
        public string _id { get; set; }
        public string AccountId { get; set; }
        public string ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string ECommerceProviderName { get; set; }
        public string ProviderStoreId { get; set; }
        public string ProviderCategoryHandle { get; set; }
        public string ProviderCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
