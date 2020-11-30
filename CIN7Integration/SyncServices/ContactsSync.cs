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
    public class ContactsSync
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
        public ContactsSync(string cinUserName, string cinAPiKey, string crmAPIKey, string crmAccountId,string crmUserName, DateTime? fromDate)
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
            // use CIN 7 API instead of the CIN7 dll 
            //just make a get request to https://api.cin7.com/api/Help/Api/GET-v1-Contacts_fields_where_order_page_rows
            // using basic Auth with this._CIN7_UsereName & this._CIN7_ApiKey
            // an example for get request with basic auth https://stackoverflow.com/questions/4334521/httpwebrequest-using-basic-authentication

            var api = new Cin7Api(new ApiUser(this._CIN7_UsereName, this._CIN7_ApiKey));
            var ContactsList = api.Contacts.Find<Cin7ApiWrapper.Models.Contact>();
            var CRMContactList = new List<ECommerceContactApiModel>();
            var url = "/api/1.0/Contacts/New/";

            foreach (var Contact in ContactsList)
            {
                var content = new FormUrlEncodedContent(new[]
                {
              new  KeyValuePair<string,string>("Name", _CRM_UserName),
              new  KeyValuePair<string,string>("Title", Contact.JobTitle),
              new  KeyValuePair<string,string>("Organization", Contact.Company),
              new  KeyValuePair<string,string>("ContactInfoSecondaryPhoneNumber", Contact.SecondaryContacts.ToString()),
              new  KeyValuePair<string,string>("ContactInfoPrimaryEmail", Contact.Email),
              new  KeyValuePair<string,string>("ContactInfoWebsite", Contact.Website),
              new  KeyValuePair<string,string>("ContactInfoAddressLine1", Contact.Address1),
              new  KeyValuePair<string,string>("ContactInfoAddressLine2",Contact.Address2 ),
              new  KeyValuePair<string,string>("ContactInfoAddressCity", Contact.City),
              new  KeyValuePair<string,string>("ContactInfoAddressPostalCode", Contact.PostalPostCode),
              new  KeyValuePair<string,string>("ContactInfoAddressCountry", Contact.PostalCountry),
              new  KeyValuePair<string,string>("ContactInfoAddressState", Contact.State),
              new  KeyValuePair<string,string>("CountryName", Contact.Country),

            });
                var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, content);

            }
           
              }
        #endregion
    }
}
