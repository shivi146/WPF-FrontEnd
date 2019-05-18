using System;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace WpfPoc
{
    public class SaveDetailsServiceClass
    {
        // Setting base address.
        readonly string baseUrl = ConfigurationManager.AppSettings["baseAddress"];
        readonly string url = "api/User/SaveUserDetails";
      
        HttpClient client = new HttpClient();
        /// <summary>
        /// Saving User Registration Details
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        
        public HttpResponseMessage SaveUserData(string jsonData)
        {
            using (client)
            {
                var stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");                
                client.BaseAddress = new Uri(baseUrl);                
                try
                {
                    return client.PostAsync(url, stringContent).Result;                                     
                }
                catch (Exception ex)
                {
                    throw new Exception("Some error occurred!!!",ex);
                }
            }
        }
    }
}
