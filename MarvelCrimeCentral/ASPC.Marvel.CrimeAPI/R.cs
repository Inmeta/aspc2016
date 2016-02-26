using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ASPC.Marvel.CrimeAPI
{
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class PredictML
    {
        public int GetCrimeNumberByCoordAndYear(double latitude, double longitude, int year)
        {
            var resInvoke = InvokeRequestResponseService(latitude, longitude, year);
            resInvoke.Wait();

            var str = resInvoke.Result;
            var dataRes = JObject.Parse(str);
            var result = ParseAzureMLResult(dataRes);
            return result;
        }

        string apiKey = "l1OZgy5WTk/IBn6TiyoftHuK7OHi3EXWX6mZEiw78EEW0ljp/WMNTrxVQbF6Hv5cVLCzvDrQUctuR1RFInTZNQ==";
        string ServiceUri = @"https://europewest.services.azureml.net/workspaces/3c6b4af31e1140ef837db12b21a1149e/services/e48944a576204c77af8e681be173a5ca/execute?api-version=2.0&details=true";

        internal async Task<string> InvokeRequestResponseService(double latitude, double longitude, int year)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                    {
                        "input1",
                        new StringTable()
                        {
                            ColumnNames = new string[] {"lat", "long", "N", "Y"},
                            Values = new string[,] {  { latitude.ToString(), longitude.ToString(), "0", year.ToString() }  }
                        }
                    },
                },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri(ServiceUri);
                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
            }
        }

        private int ParseAzureMLResult(JObject response)
        {
            var res = 0;
            if (response["Results"] == null || response["Results"]["output1"] == null || response["Results"]["output1"]["value"] == null)
            {
                return res;
            }

            var result = response["Results"]["output1"]["value"];
            var resStr = result["Values"][0][4].ToString();

            res = (int)Math.Round(double.Parse(resStr, CultureInfo.InvariantCulture), 0);
            return res;
        }
    }
}