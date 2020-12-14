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
        public string _ProviderName { get; set; }
        public string _CRM_UserName { get; set; }

        #endregion
        #region Ctor 
        public OrderSync(string cinUserName, string cinAPiKey, string crmAPIKey, string providerName,string crmuserName, DateTime? fromDate)
        {
            this._CIN7_ApiKey = cinAPiKey;
            this._CIN7_UsereName = cinUserName;
            this._CRM_ApiKey = crmAPIKey;
            this._DateFrom = fromDate;
            this._ProviderName = providerName;
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
                var temp = new ECommerceOrderApiModel()
                {
                    CreatedOn = DateTime.Now,
                    EcommProviderName = _ProviderName,
                    ProviderStoreId = this._CRM_UserName,
                    ProviderOrderId = SalesOrder.Id.ToString(),
                    TotalTax = double.Parse(SalesOrder.Total.ToString()),
                    TotalDiscount = double.Parse(SalesOrder.DiscountTotal.ToString()),
                    NumOfLines = SalesOrder.LineItems.Count,
                    ShippingStatus = SalesOrder.DeliveryState,
                    Currency = SalesOrder.CurrencyCode,
                    CancelReason = "",
                    ContactId = SalesOrder.CustomerOrderNo,
                    coupon = "",
                    FinancialStatus = SalesOrder.BillingState,
                    ProviderOrderReference = SalesOrder.Reference,

                };
                var item = api.Contacts.Find(SalesOrder.MemberId);
                if(item != null)
                {
                    temp.Customer = new ContactCSVApiModel()
                    {

                        Title = item.JobTitle,
                        Organization = item.Company,
                        ContactInfoSecondaryPhoneNumber = item.Phone,
                        ContactInfoSecondaryEmail = item.Email,
                        ContactInfoPrimaryPhoneNumber = item.AccountsPhone,
                        ContactInfoPrimaryEmail = item.BillingEmail,
                        ContactInfoWebsite = item.Website,
                        ContactInfoAddressLine1 = item.Address1,
                        ContactInfoAddressLine2 = item.Address2,
                        ContactInfoAddressPostalCode = item.PostalPostCode,
                        ContactInfoAddressCountry = item.Country,
                        ContactInfoAddressState = item.State,
                        CreatedOn = item.CreatedDate,
                        UpdatedOn = item.ModifiedDate,
                        ContactInfoAddressCity = item.Country,
                        EmailValue = item.Email,
                        ExternalProviderId = item.Id.ToString(),
                        ExternalProviderName = _ProviderName

                    };
                }

                foreach(var product in SalesOrder.LineItems)
                {
                    temp.LineItems.Add(new ECommerceCheckOutLineApiModel()
                    {
                        Price = product.UnitPrice.HasValue ? (double)product.UnitPrice.Value : 0,
                        ProviderProductId = product.ProductId.ToString(),
                        ProviderProductVariantId = product.ProductOptionId.ToString(),
                        Quantity = (double)product.Quantity

                    });
                }
                CRMOrderList.Add(temp);

            }


            // 2- post data to CRM
            var url = "/api/1.0/Orders/Save/WooCommerce/" + this._CIN7_UsereName;
           
            
            var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, JsonConvert.SerializeObject(CRMOrderList));

        }
        #endregion


    }
}
