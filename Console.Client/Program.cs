using IdentityModel.Client;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Konsolen.Client
{
    class Program
    {
        //private static string baseAddress = "localhost";
        private static string baseAddress = "jb-thesisproject.ddns.net";

        static async Task Main(string[] args)
        {
            Console.Title = "Timer Konsolen Client";

            Stopwatch stopwatch = new Stopwatch();

            var tokenResponse = await GetAccessToken();

            var apiClient = new HttpClient();

            apiClient.SetBearerToken(tokenResponse.AccessToken);

            while (true)
            {
                var startTime = DateTime.Now;
                stopwatch.Start();
                var response = await apiClient.GetAsync($"https://{baseAddress}/weatherdata/weatherforecast");
                stopwatch.Stop();
                var endTime = DateTime.Now;
                if (response.StatusCode.Equals(HttpStatusCode.TooManyRequests))
                {
                    Console.WriteLine($"Request failed; Status Code: {response.StatusCode}; Throttling at Proxy works!");
                }
                else if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Request failed; Status Code: {response.StatusCode}");
                    var newToken = await GetAccessToken();
                    apiClient.SetBearerToken(newToken.AccessToken);
                }
                else
                {
                    var timeSpan = stopwatch.Elapsed;
                    Console.WriteLine($"Request Status Code: {response.StatusCode}; Start-Time: {startTime.ToString("hh:mm:ss.fff")}; End-Time: {endTime.ToString("hh:mm:ss.fff")}; Request Duration: {timeSpan.TotalSeconds} seconds");
                }
                stopwatch.Reset();
                // Waiting for 2 Milliseconds
                Thread.Sleep(2000);
            }

        }
        public static async Task<TokenResponse> GetAccessToken()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = $"https://{baseAddress}/identity"
            });

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "Konsolen.Client",
                ClientSecret = "secret",
                Scope = "weatherdata.read"
            });

            if (tokenResponse.IsError)
            {
                Console.Write(tokenResponse.Error);
            }
            return tokenResponse;
        }
    }
}
