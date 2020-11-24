using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration.Models
{
   public class ECommerceStoreViewModel
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AccessToken { get; set; }

        
        public string ProviderName { get; set; }
        public string StoreId { get; set; }
        public string StoreEmail { get; set; }




    }
}
