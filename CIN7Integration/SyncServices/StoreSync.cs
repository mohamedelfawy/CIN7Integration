using Cin7ApiWrapper.Common;
using CIN7Integration.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration.SyncServices
{
   public class StoreSync
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
        public StoreSync(string cinUserName, string cinAPiKey, string crmAPIKey, string crmAccountId, string crmUserName, DateTime? fromDate)
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
            var CRMStore = new ECommerceStoreViewModel() {
                AccessToken = _CIN7_ApiKey,
                CreatedOn = DateTime.Now,
                ProviderName = "WooCommerce",
                StoreEmail = "smeligy@revampco.com",
                StoreId = _CIN7_UsereName,
                Title = _CIN7_UsereName,
                UpdatedOn = DateTime.Now
            };
            
            // 2- post data to CRM
            //POST 
            var url = "/api/1.0/Stores/Save";

            var content = new FormUrlEncodedContent(new[]
            {
               new KeyValuePair<string, string>("",JsonConvert.SerializeObject(CRMStore)),
            });
            var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, content);

        }
        #endregion
    }
}
