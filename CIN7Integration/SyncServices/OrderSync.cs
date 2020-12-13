using Cin7ApiWrapper.Common;
using Cin7ApiWrapper.Infrastructure;
using CIN7Integration.Models;
using CIN7Integration.SyncServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace CIN7Integration
{
    public class OrderSync
    {
        #region Fields 
        public string _CIN7_UsereName { get; set; }
        public string _CIN7_ApiKey { get; set; }
        public string _CRM_ApiKey { get; set; }
        public DateTime? _DateFrom { get; set; }
        public string _CRM_AccountId { get; set; }
        public string _CRM_UserName { get; set; }

        #endregion
        #region Ctor 
        public OrderSync(string cinUserName, string cinAPiKey, string crmAPIKey, string crmAccountId,string crmuserName, DateTime? fromDate)
        {
            this._CIN7_ApiKey = cinAPiKey;
            this._CIN7_UsereName = cinUserName;
            this._CRM_ApiKey = crmAPIKey;
            this._DateFrom = fromDate;
            this._CRM_AccountId = crmAccountId;
            this._CRM_UserName = crmuserName;
        }
        #endregion
        #region Functions 
        public void Start()
        {
            //1- get data from Cin7
            var api = new Cin7Api(new ApiUser(this._CIN7_UsereName, this._CIN7_ApiKey));
            var SalesOrderList = api.SalesOrders.Find(modifiedSince: this._DateFrom);

            var CRMOrderList = new List<ECommerceOrderApiModel>();

            foreach(var SalesOrder in SalesOrderList)
            {
                CRMOrderList.Add(new ECommerceOrderApiModel()
                {
                    AccountId = this._CRM_AccountId,
                    CreatedOn = DateTime.Now,
                    EcommProviderName = "WooCommerce",
                    ProviderStoreId = this._CRM_UserName,
                    ProviderOrderId = SalesOrder.Id.ToString(),
                    OrderCustomData = SalesOrder.CustomerOrderNo,
                    TotalTax = double.Parse(SalesOrder.Total.ToString()),
                    TotalDiscount = double.Parse(SalesOrder.DiscountTotal.ToString()),
                    NumOfLines = int.Parse(SalesOrder.LineItems.ToString()),
                    ShippingStatus = SalesOrder.DeliveryState,
                    Currency = SalesOrder.CurrencyCode,
                });
          
            }


            // 2- post data to CRM
            var url = "/api/1.0/Orders/Save/WooCommerce/" + this._CIN7_UsereName;

            var content = new FormUrlEncodedContent(new[]
            {
               new KeyValuePair<string, string>("orders",JsonConvert.SerializeObject(CRMOrderList)),
            });
           // var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, content);

        }
        #endregion


    }
}
