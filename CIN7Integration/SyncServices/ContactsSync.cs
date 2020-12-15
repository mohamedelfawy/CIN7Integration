using Cin7ApiWrapper.Common;
using Cin7ApiWrapper.Infrastructure;
using CIN7Integration.Models;
using CIN7Integration.SyncServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public string _ProviderName { get; set; }

        public string _CRM_UserName { get; set; }


        #endregion
        #region Ctor 
        public ContactsSync(string cinUserName, string cinAPiKey, string crmAPIKey, string providerName,string crmUserName, DateTime? fromDate)
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
                //1- get data from Cin7
                var cin7_api = new Cin7Api(new ApiUser(this._CIN7_UsereName, this._CIN7_ApiKey));
                var ContactList = cin7_api.Contacts.Find().ToList();

                var listCount = ContactList.Count();
                int pagenum = 2;
                if (listCount == 50)
                {
                    do
                    {
                        var rows = cin7_api.Contacts.Find(page: pagenum, rows: 50);
                        listCount = rows.Count();
                        foreach (var item in rows)
                        {
                            ContactList.Add(item);
                        }
                        pagenum = pagenum + 1;

                    } while (listCount == 50);

                }


                var CRMContactList = new List<ECommerceContactApiModel>();
                foreach (var item in ContactList)
                {
                    var tempContact = new ECommerceContactApiModel()
                    {
                        //Name="",

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
                        //ContactInfoskype = "",
                        //ContactInfoFacebook = "",
                        //ContactInfoTwitter = "",
                        //Description = "",
                        //ContactInfoLinkedIn = "",
                        //Gender = "",
                        //Tags = "",
                        //Score = 0,
                        //ContactStage = "",
                        //rating_contact = 0,
                        //ContactSource = "",


                    };

                    CRMContactList.Add(tempContact);

                }



                // 2- post data to CRM
                var url = "/api/1.0/Contacts/SaveExternalBatch/WooCommerce/" + this._CIN7_UsereName;


                var response = RestApi.PostRequest(this._CRM_UserName, this._CRM_ApiKey, url, JsonConvert.SerializeObject(CRMContactList));

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
