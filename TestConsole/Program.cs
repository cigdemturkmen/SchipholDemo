using Newtonsoft.Json;
using SchipholDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        private Airport[] airports;
        
        static void Main(string[] args)
        {
            Consume_WebAPI().Wait();
        }

        static async Task Consume_WebAPI()
        {
            
              FlightScheduleModel flight;
            string errorString;
            List<string> flightListStr = new List<string>();

            string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"); // "today" doesnt work.(Flights that have passed are not listed.)
            string threeDaysLater = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd"); // First request's "toScheduleDate"
            string fourDaysLater = DateTime.Now.AddDays(4).ToString("yyyy-MM-dd"); // Second request's "fromScheduleDate"
            string sixDaysLater = DateTime.Now.AddDays(6).ToString("yyyy-MM-dd"); // Second request's "toScheduleDate"
            string sevenDaysLater = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"); // last request's "scheduleDate"

            string[] uri = new string[2];

            uri[0] = "https://api.schiphol.nl/public-flights/flights?includedelays=false&page=0&sort=%2BscheduleTime&fromScheduleDate=" + $"{tomorrow}&toScheduleDate={threeDaysLater}";

            uri[1] = "https://api.schiphol.nl/public-flights/flights?includedelays=false&page=0&sort=%2BscheduleTime&fromScheduleDate=" + $"{fourDaysLater}&toScheduleDate={sixDaysLater}";

            //uri[2] = ("https://api.schiphol.nl/public-flights/flights?scheduleDate=" + $"{sevenDaysLater}&includedelays=false&page=0&sort=%2BscheduleTime"); // bu uri patlıyor

            foreach (var item in uri)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(item);
                request.Method = HttpMethod.Get;
                request.Headers.Add("app_id", "db0e442e");
                request.Headers.Add("app_key", "5a1c9f393f6cd1ffcbc693b0d5061bda");
                request.Headers.Add("ResourceVersion", "v4");

                HttpResponseMessage response = await httpClient.SendAsync(request); //burada siteye gidiyor.

                var responseString = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;

                flightListStr.Add(responseString);

                //var client = _clientFactory.CreateClient();

                //List<Flight> flightList = JsonConvert.DeserializeObject<List<Flight>>(responseString);

                if (response.IsSuccessStatusCode)
                {
                    flight = await response.Content.ReadFromJsonAsync<FlightScheduleModel>(); //20 flights 

                    //var dataSet = JsonConvert.DeserializeObject<DataSet>(responseString);
                    //var table = dataSet.Tables[0];

                    //var table = JsonConvert.DeserializeObject<DataSet>(responseString);

                    //DataTable dt = (DataTable)JsonConverter.DeserializeObject(result, (typeof(DataTable))); // -_-'
                    // DataTable dt = (DataTable)JsonConvert.DeserializeObject(responseString, (typeof(DataTable))); // +

                }
                else
                {
                    errorString = $"There was an error. : {response.ReasonPhrase}";
                }
            }
             
            
        }
    }
}
