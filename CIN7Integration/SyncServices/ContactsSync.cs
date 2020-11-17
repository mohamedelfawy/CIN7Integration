using Cin7ApiWrapper.Common;
using Cin7ApiWrapper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
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



            // 2- post data to CRM

        }
        #endregion


    }
}
