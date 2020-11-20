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
            IsCustomer = true;
        }
       public string Name { get; set; }

       public string Title { get; set; }
       public string Organization { get; set; }

        public string ContactInfoSecondaryPhoneNumber { get; set; }
  public string ContactInfoSecondaryEmail { get; set; }

         public string ContactInfoPrimaryPhoneNumber { get; set; }

         public string ContactInfoPrimaryEmail { get; set; }

          public string ContactInfoskype { get; set; }

        public string ContactInfoFacebook { get; set; }

 public string ContactInfoWebsite { get; set; }

        public string ContactInfoTwitter { get; set; }
  public string ContactInfoAddressLine1 { get; set; }

        public string ContactInfoAddressLine2 { get; set; }
  public string ContactInfoAddressCity { get; set; }
  public string ContactInfoAddressPostalCode { get; set; }
  public string ContactInfoAddressCountry { get; set; }

 public string ContactInfoAddressState { get; set; }


        public DateTime CreatedOnUtc { get; set; }
  public DateTime? BirthDate { get; set; }

        public DateTime UpdateOnUtc { get; set; }
   public string CountryName { get; set; }
        public ExternalProvider[] ExternalProviders { get; set; }
    
        public string Description { get; set; }

      
      public string ContactInfoLinkedIn { get; set; }

       public string Gender { get; set; }
  public string Tags { get; set; }
       public int Score { get; set; }
     public string ContactStage { get; set; }
      
        public string EmailValue { get; set; }

        public int rating_contact { get; set; }

     
        public bool IsCustomer { get; set; }
      
        public string ContactSource { get; set; }

    }
    public class ExternalProvider
    {
        public string ExternalId { get; set; }

        public string ProviderName { get; set; }

        // source email : the current user email.
        public string SourceEmail { get; set; }
    }
}
