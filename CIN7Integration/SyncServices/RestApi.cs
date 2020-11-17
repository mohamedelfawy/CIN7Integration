using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CIN7Integration.SyncServices
{
   public class RestApi
    {
        public const string BaseURL = "https://app.revampcrm.com/api/1.0/";
        public static HttpResponseMessage PostRequest(string userName,string CRMApiKey, string url, HttpContent content = null)
        {
            var parameter = new Dictionary<string, string>();

            parameter.Add("Class/Method", "RestServices/PostRequest");
                try
                {
                    var authData = string.Format("{0}:{1}", userName, CRMApiKey);
                    var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
                    var client = new HttpClient();
                    var baseUri = new Uri(BaseURL);
                    client.BaseAddress = new Uri(baseUri, BaseURL + url);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                    if (content == null)
                    {
                        HttpResponseMessage response = client.PostAsync(client.BaseAddress, null).Result;
                        return response;

                    }
                    else
                    {

                        HttpResponseMessage response = client.PostAsync(client.BaseAddress, content).Result;
                        return response;



                    }

                }
                catch (Exception e)
                {
                    parameter.Add("Tip", "Post request user name :" + userName + " , Url:" + url);
                    return null;
                }

        }

    }
}
