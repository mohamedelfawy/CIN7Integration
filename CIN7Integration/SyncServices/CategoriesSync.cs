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
        public string _ProviderName { get; set; }

        public string _CRM_UserName { get; set; }


        #endregion
        #region Ctor 
        public CategoriesSync(string cinUserName, string cinAPiKey, string crmAPIKey, string providerName,string crmUserName,DateTime? fromDate)
        {
            this._CIN7_ApiKey = cinAPiKey;
            this._CIN7_UsereName = cinUserName;
            this._CRM_ApiKey = crmAPIKey;
            this._DateFrom = fromDate;
            this._ProviderName = providerName;
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
                    
                    CreatedOn = DateTime.Now,
                    ECommerceProviderName = _ProviderName,
                    ProviderCategoryId = item.Id.ToString(),
                    ProviderStoreId = this._CIN7_UsereName,
                    Title = item.Name,
                    Description = item.Description,
                    ProviderCategoryHandle = item.Name

                });
                var parentId = item.ParentId;
                while(parentId != 0)
                {
                    if (CRMCategoryList.Where(c => c.ProviderCategoryId == parentId.ToString()).Count() < 1)
                    {
                        var cat = cin7_api.ProductCategories.Find(parentId);
                        CRMCategoryList.Add(new ECommerceCategoryApiModel()
                        {
                            CreatedOn = DateTime.Now,
                            ECommerceProviderName = _ProviderName,
                            ProviderCategoryId = cat.Id.ToString(),
                            ProviderStoreId = this._CIN7_UsereName,
                            Title = cat.Name,
                            Description = cat.Description,


                        });
                        parentId = cat.ParentId;
                    }
                    else
                        parentId = 0;

                }
            }

            // 2- post data to CRM
            //POST 
            var url = "/api/1.0/Categories/Save/"+_ProviderName+"/" + this._CIN7_UsereName;

          
            var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url,JsonFormatbody: JsonConvert.SerializeObject(CRMCategoryList));

        }
        #endregion

    }
}
