using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration
{
    public class StartUp
    {
        public bool StartSync(string cinUserName,string cinAPiKey ,string crmAPIKey ,string crmAccountId,string crmUserName, DateTime? fromDate)
        {
            try
            {
                var product = new ProductsSync(cinUserName, cinAPiKey, crmAPIKey, crmAccountId, crmUserName, fromDate);
                var category = new CategoriesSync(cinUserName, cinAPiKey, crmAPIKey, crmAccountId, crmUserName, fromDate);
                var contact = new ContactsSync(cinUserName, cinAPiKey, crmAPIKey, crmAccountId, crmUserName, fromDate);
                var order = new OrderSync(cinUserName, cinAPiKey, crmAPIKey, crmAccountId, crmUserName, fromDate);

                category.Start();
                product.Start();
                contact.Start();
                order.Start();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

    }
}
