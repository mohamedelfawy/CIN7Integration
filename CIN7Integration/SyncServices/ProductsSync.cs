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
            this._CRM_UserName = crmUserName;
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
                var tempProduct = new ECommerceProductApiModel()
                {
                    CreatedOn = Product.CreatedDate.HasValue ? Product.CreatedDate.Value : DateTime.Now,
                    ECommerceProviderName = "WooCommerce",
                    ProviderStoreId = this._CIN7_UsereName,
                    ProviderProductId = Product.Id.ToString(),
                    ProviderProductName = Product.Name,
                    ProductType = Product.ProductType,
                    ProductHandle = Product.Name,
                    UpdatedOn = Product.CreatedDate.HasValue ? Product.CreatedDate.Value : DateTime.Now
                };
                if(Product.ProductOptions != null && Product.ProductOptions.Count > 0)
                {
                    if(Product.ProductOptions[0].Image !=null)
                    {
                        tempProduct.Image = Product.ProductOptions[0].Image.Link;
                    }
                    foreach(var item in Product.ProductOptions)
                    {
                        tempProduct.variants.Add(new ECommerceProductVariant()
                        {
                            Name = Product.Name,
                            price = item.WholesalePrice.Value.ToString(),
                            ProviderVariantId = item.Id.ToString(),
                            sku = item.Code
                        });
                    }
                }
                if(Product.CategoryIdArray != null && Product.CategoryIdArray.Length> 0)
                {
                    tempProduct.categories.Add(new ECommerceCategoryApiModel()
                    {
                        Title = Product.Category,
                        ProviderCategoryId = Product.CategoryIdArray[0].ToString(),
                        ProviderStoreId = _CIN7_UsereName
                    });
                }
           
                CRMProductList.Add(tempProduct);
                
            }


            // 2- post data to CRM
            var url = "/api/1.0/Products/Save/WooCommerce/" + this._CIN7_UsereName;
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("products",JsonConvert.SerializeObject(CRMProductList))
            });
            var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, content);
        }
        #endregion

    }
}
