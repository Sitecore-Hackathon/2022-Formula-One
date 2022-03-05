using Hack2022.Foundation.SitecoreSend.Services;
using Hack2022.Foundation.SitecoreSend.Settings;
using Newtonsoft.Json;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hack2022.Foundation.SitecoreSend.Services
{
    public partial class SitecoreSendService : ISendService
    {
        public bool Subscribe(string name, string emailAddress, string contactNumber, List<string> customFields)
        {
            var model = new MooSendData
            {
                Name = name,
                Email = emailAddress,
                Mobile = contactNumber,
                CustomFields = customFields
                //CustomFields = new List<string>() { "custom=" + custom }
            };

            Task<bool> executePost = Task.Run(async () => await ExecuteSend(JsonConvert.SerializeObject(model)));
            return executePost.Result;
        }
        
        /// <summary>
        /// Async Call to Send API for subscription using form data captured from front end
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> ExecuteSend(string data)
        {
            try
            {
                using (var httpClient = new HttpClient { BaseAddress = new Uri(SendSettings.MailServerUrl) })
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                    using (var content = new StringContent(data, Encoding.Default, "application/json"))
                    {
                        //using (var response = await httpClient.PostAsync($"subscribers/{SendSettings.MemberListId}/subscribe.json?apiKey={SendSettings.MooSendApiKey}", content))
                        using (var response = await httpClient.PostAsync($"subscribers/{SendSettings.MemberListId}/subscribe.json?apiKey={SendSettings.MooSendApiKey}", content))
                        {
                            string responseData = await response.Content.ReadAsStringAsync();
                            if (string.IsNullOrEmpty(responseData))
                            {
                                Log.Error("Unexpected response from Sitecore Send", responseData);
                                return false;
                            }
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected exception occured while executing post to Sitecore Send", ex.StackTrace);
                return false;
            }
        }

        public class MooSendData
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public List<string> CustomFields { get; set; }
        }
    }
}
