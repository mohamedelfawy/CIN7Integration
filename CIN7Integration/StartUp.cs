using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration
{
    public class StartUp
    {
        public bool StartSync(string cinUserName,string cinAPiKey ,string crmAPIKey , DateTime? fromDate)
        {
            try
            {
                var product = new ProductsSync(cinUserName, cinAPiKey, crmAPIKey, fromDate);
                var category = new CategoriesSync(cinUserName, cinAPiKey, crmAPIKey, fromDate);
                var contact = new ContactsSync(cinUserName, cinAPiKey, crmAPIKey, fromDate);
                var order = new OrderSync(cinUserName, cinAPiKey, crmAPIKey, fromDate);

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
