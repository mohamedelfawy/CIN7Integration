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
            var api = new Cin7Api(new ApiUser(this._CIN7_UsereName, this._CIN7_ApiKey));
            var ContactsList = api.Contacts.Find(modifiedSince: this._DateFrom);
            var CRMContactList = new List<ECommerceContactApiModel>();

            foreach (var Contact in ContactsList)
            {
                CRMContactList.Add(new ECommerceContactApiModel()
                {
                    Name = this._CRM_UserName,
                    CreatedOnUtc = DateTime.Now,
                    Title = Contact.JobTitle,
                    Organization = Contact.Company,
                    ContactInfoSecondaryPhoneNumber =Contact.SecondaryContacts.ToString(),
                    ContactInfoPrimaryEmail = Contact.Email,
                    ContactInfoWebsite = Contact.Website,
                    ContactInfoAddressLine1 = Contact.Address1,
                    ContactInfoAddressLine2 = Contact.Address2 ,
                    ContactInfoAddressCity = Contact.City,
                    ContactInfoAddressPostalCode = Contact.PostalPostCode,
                    ContactInfoAddressCountry = Contact.PostalCountry,
                    ContactInfoAddressState = Contact.State,
                    CountryName = Contact.Country,
                });
            }
           
            // 2- post data to CRM
            var url = "/api/1.0/Contacts/SaveExternalBatch/CIN7/"+_CIN7_UsereName;
            var content = new FormUrlEncodedContent(new[] 
            {
              new  KeyValuePair<string,string>("", JsonConvert.SerializeObject(CRMContactList))
            });
            var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, content);
        }
        #endregion
    }
}
