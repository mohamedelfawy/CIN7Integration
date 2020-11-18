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
    public class ProductsSync
    {
        #region Fields 
        public string _CIN7_UsereName { get; set; }
        public string _CIN7_ApiKey { get; set; }
        public string _CRM_ApiKey { get; set; }
        public DateTime? _DateFrom { get; set; }
        public string _CRMAccountId{get;set;}
        public string _CRM_UserName { get; set; }
        #endregion
        #region Ctor 
        public ProductsSync(string cinUserName, string cinAPiKey, string crmAPIKey, string crmAccountId,string crmUserName, DateTime? fromDate)
        {
            this._CIN7_ApiKey = cinAPiKey;
            this._CIN7_UsereName = cinUserName;
            this._CRM_ApiKey = crmAPIKey;
            this._DateFrom = fromDate;
            this._CRMAccountId = crmAccountId;
            this._CIN7_UsereName = crmUserName;
        }
        #endregion
        #region Functions 
        public void Start()
        {
            //1- get data from Cin7
            var api = new Cin7Api(new ApiUser(this._CIN7_UsereName, this._CIN7_ApiKey));
            var ProductsList = api.Products.Find(modifiedSince: this._DateFrom);

            var CRMProductList = new List<ECommerceProductApiModel>();
            foreach (var Product in ProductsList)
            {
                CRMProductList.Add(new ECommerceProductApiModel()
                {
                    AccountId = this._CRMAccountId,
                    CreatedOn = DateTime.Now,
                    ECommerceProviderName = "CIN7",
                    ProviderStoreId = this._CRM_UserName,
                    ProviderProductId = Product.Id.ToString(),
                    ProviderProductName = Product.Name,
                    ProductType = Product.ProductType,
                });
            }
            // 2- post data to CRM
            var url = "" + this._CRM_UserName;
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("products",JsonConvert.SerializeObject(CRMProductList))
            });
            var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, content);
        }
        #endregion

    }
}
