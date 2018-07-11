using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace UnitTestAPI
{
    class Program
    {
        static string baseAddress = "http://localhost:3773/";
        static void Main(string[] args)
        {
            string userName = "phongnv3773@gmail.com";
            string password = "123456789";

            Dictionary<string, string> token = GetToken(userName, password);
            Console.WriteLine("");
            Console.WriteLine("Access Token:");
            Console.WriteLine(token);

            // Write each item in the dictionary out to the console:
            foreach (var kvp in token)
            {
                Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            }

            Console.WriteLine("");

            Console.WriteLine("Getting User Info:");
            Console.WriteLine(GetUserInfo(token["access_token"]));

            Console.Read();

        }

        static Dictionary<string, string> GetToken(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "password" ),
                            new KeyValuePair<string, string>( "username", userName ),
                            new KeyValuePair<string, string> ( "Password", password )
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync(baseAddress + "token", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize the JSON into a Dictionary<string, string>
                Dictionary<string, string> tokenDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                return tokenDictionary;
            }
        }

        static string GetUserInfo(string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync(baseAddress + "api/user").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
