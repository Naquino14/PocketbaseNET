using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Tests
{
    internal static class Program
    {
        internal static async Task Main(string[] args)
        {
            var client = new HttpClient();
            string loginCrud = "/api/collections/users/auth-with-password", url = "http://127.0.0.1:8090";

            var loginObject = new
            {
                identity = "test@test.com",
                password = "test1234"
            };
            var jsonString = JsonSerializer.Serialize(loginObject);
            Console.WriteLine($"JSON String: {jsonString}");
            Console.WriteLine($"URL: {url}{loginCrud}");
            var httpReq = new StringContent(jsonString, Encoding.UTF8, "application/json");
            Console.WriteLine($"Posting... {httpReq}");
            var result = await client.PostAsync($"{url}{loginCrud}", httpReq);
            Console.WriteLine($"Result: {(int)result.StatusCode} {result.StatusCode}: {await result.Content.ReadAsStringAsync()}");
        }
    }
}