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
    public class CategoriesSync
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
        public CategoriesSync(string cinUserName, string cinAPiKey, string crmAPIKey, string crmAccountId,string crmUserName,DateTime? fromDate)
        {
            this._CIN7_ApiKey = cinAPiKey;
            this._CIN7_UsereName = cinUserName;
            this._CRM_ApiKey = crmAPIKey;
            this._DateFrom = fromDate;
            this._CRM_AccountId = crmAccountId;
            this._CRM_UserName = crmUserName;
        }
        #endregion
        #region Functions 
        public void Start()
        {
            //1- get data from Cin7
            var cin7_api = new Cin7Api(new ApiUser(this._CIN7_UsereName, this._CIN7_ApiKey));
            var CategoriesList = cin7_api.ProductCategories.Find();
            
            var CRMCategoryList = new List<ECommerceCategoryApiModel>();
            foreach(var item in CategoriesList)
            {
                CRMCategoryList.Add(new ECommerceCategoryApiModel()
                {
                    AccountId = this._CRM_AccountId,
                    CreatedOn = DateTime.Now,
                    ECommerceProviderName = "CIN7",
                    ProviderCategoryId = item.Id.ToString(),
                    ProviderStoreId = this._CIN7_UsereName,
                    Title = item.Name,

                });
            }

            // 2- post data to CRM
            //POST 
            // lets say we need to add some code here then commit it to git hub ...
            var url = "/api/1.0/Categories/Save/CIN7/"+this._CIN7_UsereName;

            var content = new FormUrlEncodedContent(new[]
            {
               new KeyValuePair<string, string>("categories",JsonConvert.SerializeObject(CRMCategoryList)),
            });
            var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, content);

        }
        #endregion

    }
}
