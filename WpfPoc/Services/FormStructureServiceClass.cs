using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;

namespace WpfPoc
{
   public class FormStructureServiceClass
    { 
        // Setting base address.
        readonly string baseUrl = ConfigurationManager.AppSettings["baseAddress"];
        readonly string url = "api/FormDetails/GetFormData?FormId=";

        HttpResponseMessage response ;
        HttpClient client = new HttpClient();
        public string GetJsonFileData(string FormId) 
        {
            using (client)
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    //Getting Form Structure data
                    response = client.GetAsync(url + FormId).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Some error occurred!!!", ex);
                }
            }
        }
    }
}