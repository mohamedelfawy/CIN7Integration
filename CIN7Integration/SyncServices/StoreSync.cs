using Cin7ApiWrapper.Common;
using CIN7Integration.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public string _ProviderName { get; set; }

        public string _CRM_UserName { get; set; }
        public string _MainEmail { get; set; }

        #endregion
        #region Ctor 
        public StoreSync(string MainEmail,string cinUserName, string cinAPiKey, string crmAPIKey, string providername, string crmUserName, DateTime? fromDate)
        {
            this._CIN7_ApiKey = cinAPiKey;
            this._CIN7_UsereName = cinUserName;
            this._CRM_ApiKey = crmAPIKey;
            this._DateFrom = fromDate;
            this._ProviderName = providername;
            this._CRM_UserName = crmUserName;
            _MainEmail = MainEmail;
        }
        #endregion
        #region Functions 
        public void Start()
        {
            try {

                //Maram: post data to CRM
                //POST 
                var url = "/api/1.0/Stores/Save";

            var content = new FormUrlEncodedContent(new[]
            {
               new KeyValuePair<string, string>("AccessToken",_CIN7_ApiKey),
               new KeyValuePair<string, string>("ProviderName",_ProviderName),
               new KeyValuePair<string, string>("StoreEmail",_MainEmail),
               new KeyValuePair<string, string>("StoreId",_CIN7_UsereName),
               new KeyValuePair<string, string>("Title",_CIN7_UsereName),

            });
            var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, null,content);

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
