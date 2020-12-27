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
    public class ProductsSync
    {
        #region Fields 
        public string _CIN7_UsereName { get; set; }
        public string _CIN7_ApiKey { get; set; }
        public string _CRM_ApiKey { get; set; }
        public DateTime? _DateFrom { get; set; }
        public string _ProviderName{get;set;}
        public string _CRM_UserName { get; set; }
        #endregion
        #region Ctor 
        public ProductsSync(string cinUserName, string cinAPiKey, string crmAPIKey, string providerName,string crmUserName, DateTime? fromDate)
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
            try
            {
                //Maram:1- get data from Cin7
                var api = new Cin7Api(new ApiUser(this._CIN7_UsereName, this._CIN7_ApiKey));
               // var ProductsList = api.Products.Find(modifiedSince: this._DateFrom).ToList();

                var listCount =0;
                int pagenum = 1;
               
                    do
                    {
                        var ProductsList = api.Products.Find(page: pagenum, rows: 50, modifiedSince: this._DateFrom);
                        listCount = ProductsList.Count();
                        pagenum = pagenum + 1;
                    var CRMProductList = new List<ECommerceProductApiModel>();
                    foreach (var Product in ProductsList)
                    {
                        var tempProduct = new ECommerceProductApiModel()
                        {
                            CreatedOn = Product.CreatedDate.HasValue ? Product.CreatedDate.Value : DateTime.Now,
                            ECommerceProviderName = _ProviderName,
                            ProviderStoreId = this._CIN7_UsereName,
                            ProviderProductId = Product.Id.ToString(),
                            ProviderProductName = Product.Name,
                            ProductType = Product.ProductType,
                            ProductHandle = Product.Name,
                            UpdatedOn = Product.CreatedDate.HasValue ? Product.CreatedDate.Value : DateTime.Now
                        };
                        if (Product.ProductOptions != null && Product.ProductOptions.ToList().Count > 0)
                        {
                            foreach (var item in Product.ProductOptions)
                            {
                                tempProduct.variants.Add(new ECommerceProductVariant()
                                {
                                    Name = Product.Name,
                                    price = item.WholesalePrice.HasValue ? item.WholesalePrice.Value.ToString() : "0",
                                    ProviderVariantId = item.Id.ToString(),
                                    sku = item.Code
                                });
                            }
                        }
                        if (Product.CategoryIdArray != null)
                        {
                            foreach (var cat in Product.CategoryIdArray)
                            {
                                var category = api.ProductCategories.Find(cat);
                                tempProduct.categories.Add(new ECommerceCategoryApiModel()
                                {
                                    ProviderCategoryId = category.Id.ToString(),
                                    CreatedOn = DateTime.Now,
                                    Description = category.Description,
                                    ECommerceProviderName = _ProviderName,
                                    ProviderCategoryHandle = category.Name,
                                    Title = category.Name,
                                    UpdatedOn = DateTime.Now,
                                    ProviderStoreId = _CIN7_UsereName
                                });

                            }
                        }
                        CRMProductList.Add(tempProduct);

                    }

                    //Maram: 2- post data to CRM
                    var url = "/api/1.0/Products/Save/WooCommerce/" + this._CIN7_UsereName;
                    var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, JsonConvert.SerializeObject(CRMProductList));


                } while (listCount == 50);

                


              
            } catch(Exception ex)
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
