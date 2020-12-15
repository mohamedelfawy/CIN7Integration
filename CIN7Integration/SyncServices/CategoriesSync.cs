using Cin7ApiWrapper.Common;
using Cin7ApiWrapper.Infrastructure;
using CIN7Integration.Models;
using CIN7Integration.SyncServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            try { 
            //1- get data from Cin7
            var cin7_api = new Cin7Api(new ApiUser(this._CIN7_UsereName, this._CIN7_ApiKey));
            var CategoriesList = cin7_api.ProductCategories.Find().ToList();

                var listCount = CategoriesList.Count();
                int pagenum = 2;
                if (listCount == 50)
                {
                    do
                    {
                        var rows = cin7_api.ProductCategories.Find(page: pagenum, rows: 50);
                        listCount = rows.Count();
                        foreach (var item in rows)
                        {
                            CategoriesList.Add(item);
                        }
                        pagenum = pagenum + 1;
                        
                    } while (listCount == 50);

                }


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
            catch (Exception ex)
            {
                string filePath = @"C:\Crashes.txt";

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
            }
        }
        #endregion

    }
}
