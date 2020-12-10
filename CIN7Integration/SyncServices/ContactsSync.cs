using Cin7ApiWrapper.Common;
using Cin7ApiWrapper.Infrastructure;
using CIN7Integration.Models;
using CIN7Integration.SyncServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration
{
    #region model
    public class ContactList
    {
        public string id { get; set; }
        public DateTime createdDaten { get; set; }
        public DateTime modifiedDate { get; set; }
        public Boolean isActive { get; set; }
        public string company { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string jobTitle { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string mobile { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postCode { get; set; }
        public string country { get; set; }
        public string postalAddress1 { get; set; }
        public string postalAddress2 { get; set; }
        public string postalCity { get; set; }
        public string postalPostCode { get; set; }
        public string postalState { get; set; }
        public string postalCountry { get; set; }
        public string notes { get; set; }
        public string integrationRef { get; set; }
        public string customFields { get; set; }
        public SecondaryContact[] SecondaryContacts { get; set; }

        public class SecondaryContact
        {
            public string id { get; set; }
            public string company { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string jobTitle { get; set; }
            public string email { get; set; }
            public string mobile { get; set; }
            public string phone { get; set; }
        }
    }
    #endregion
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

        public async Task StartAsync()
        {
            //1- get data from Cin7
            // use CIN 7 API instead of the CIN7 dll 
            //just make a get request to https://api.cin7.com/api/Help/Api/GET-v1-Contacts_fields_where_order_page_rows
            // using basic Auth with this._CIN7_UsereName & this._CIN7_ApiKey
            // an example for get request with basic auth https://stackoverflow.com/questions/4334521/httpwebrequest-using-basic-authentication

            //string url = @"https://api.cin7.com/api/Help/Api/GET-v1-Contacts";
            ////string url = "/api.cin7.com/api/Help/Api/GET-v1-Contacts";
            //WebRequest request = WebRequest.Create(url);
            //request.Credentials = GetCredential(url);
            //request.PreAuthenticate = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.cin7.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Get  Http             
                HttpResponseMessage response = await client.GetAsync("https://api.cin7.com/api/Help/Api/GET-v1-Contacts");
                if (response.IsSuccessStatusCode)
                {
                    var CinContactList = await response.Content.ReadAsByteArrayAsync();
                    string m = JsonConvert.SerializeObject(CinContactList);
                    //string json = await CinContactList;
                }

                // post 
                var CRMContactList = new List<ECommerceCategoryApiModel>();
               

            }
            #region


            //var ContactsList = api.Contacts.Find<Cin7ApiWrapper.Models.Contact>();
            //var CRMContactList = new List<ECommerceContactApiModel>();
            //var url = "/api/1.0/Contacts/New/";

            //foreach (var Contact in ContactsList)
            //{
            //    var content = new FormUrlEncodedContent(new[]
            //    {
            //  new  KeyValuePair<string,string>("Name", _CRM_UserName),
            //  new  KeyValuePair<string,string>("Title", Contact.JobTitle),
            //  new  KeyValuePair<string,string>("Organization", Contact.Company),
            //  new  KeyValuePair<string,string>("ContactInfoSecondaryPhoneNumber", Contact.SecondaryContacts.ToString()),
            //  new  KeyValuePair<string,string>("ContactInfoPrimaryEmail", Contact.Email),
            //  new  KeyValuePair<string,string>("ContactInfoWebsite", Contact.Website),
            //  new  KeyValuePair<string,string>("ContactInfoAddressLine1", Contact.Address1),
            //  new  KeyValuePair<string,string>("ContactInfoAddressLine2",Contact.Address2 ),
            //  new  KeyValuePair<string,string>("ContactInfoAddressCity", Contact.City),
            //  new  KeyValuePair<string,string>("ContactInfoAddressPostalCode", Contact.PostalPostCode),
            //  new  KeyValuePair<string,string>("ContactInfoAddressCountry", Contact.PostalCountry),
            //  new  KeyValuePair<string,string>("ContactInfoAddressState", Contact.State),
            //  new  KeyValuePair<string,string>("CountryName", Contact.Country),

            //});
            //    var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, content);

            //}
            #endregion

        }
       
    }

    
}
#endregion