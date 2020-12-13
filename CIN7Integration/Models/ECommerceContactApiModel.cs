using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration.Models
{
    public class ECommerceContactApiModel
    {
        public ECommerceContactApiModel()
        {
            CreatedOn = DateTime.Now;
        }
        //public string EntityId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
        public string Organization { get; set; }
        //public string LeadSource { get; set; }
        //public bool WasALead { get; set; }
        public string Tags { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Lists { get; set; }
        public string Description { get; set; }
        public string ContactInfoAddressLine1 { get; set; }
        public string ContactInfoAddressLine2 { get; set; }
        public string ContactInfoAddressPostalCode { get; set; }
        public string ContactInfoAddressCity { get; set; }
        public string ContactInfoAddressCountry { get; set; }
        public string ContactInfoPrimaryEmail { get; set; }
        public string ContactInfoSecondaryEmail { get; set; }
        public string ContactInfoTwitter { get; set; }
        public string ContactInfoskype { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }

        public string ContactInfoFacebook { get; set; }
        public string ContactInfoAddressState { get; set; }
        public string ContactInfoLinkedIn { get; set; }
        public string ContactInfoSecondaryPhoneNumber { get; set; }
        public string ContactInfoPrimaryPhoneNumber { get; set; }
        public string ContactInfoWebsite { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }
        public string CustomField6 { get; set; }
        public string CustomField7 { get; set; }
        public string CustomField8 { get; set; }
        public string CustomField9 { get; set; }
        public string CustomField10 { get; set; }


        public double CustomField101 { get; set; }
        public double CustomField102 { get; set; }
        public double CustomField103 { get; set; }
        public double CustomField104 { get; set; }
        public double CustomField105 { get; set; }
        public double CustomField106 { get; set; }
        public double CustomField107 { get; set; }
        public double CustomField108 { get; set; }
        public double CustomField109 { get; set; }
        public double CustomField110 { get; set; }



        public DateTime? CustomField201 { get; set; }
        public DateTime? CustomField202 { get; set; }
        public DateTime? CustomField203 { get; set; }
        public DateTime? CustomField204 { get; set; }
        public DateTime? CustomField205 { get; set; }
        public DateTime? CustomField206 { get; set; }
        public DateTime? CustomField207 { get; set; }
        public DateTime? CustomField208 { get; set; }
        public DateTime? CustomField209 { get; set; }
        public DateTime? CustomField210 { get; set; }




        public string ContactStage { get; set; }
        public string Subscribed { get; set; }
        public string ContactSource { get; set; }

        public int EcommerceScore { get; set; }
        public string EmailValue { get; set; }

        public string ExternalProviderName { get; set; }
        public string ExternalProviderId { get; set; }

        public double OrdersSummaryTotalOrdersValue { get; set; }
        public double OrdersSummaryAverageOrdersValue { get; set; }
        public double OrdersSummaryTotalOrdersCount { get; set; }
        public double OrdersSummaryTotalProductsOrdered { get; set; }
        public double OrdersSummaryAverageProductsOrdered { get; set; }
        public DateTime OrdersSummaryLastOrderDate { get; set; }
    }
}
