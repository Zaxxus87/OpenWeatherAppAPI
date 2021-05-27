using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace OpenWeatherAppAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            //OpenWeather API key from my account
            var api_key = "7c3cf82676daffdc4ba0600d4d157ee1";

            //User enters the city
            Console.Write("Enter the City name: ");
            var city_name = Console.ReadLine();

            //Openweather API url with needed parameters
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={city_name}&units=imperial&appid={api_key}";

            //Creates the JSON Object
            var weather = "";
            try
            {
                weather = client.GetStringAsync(url).Result;
            }
            catch (AggregateException)
            {
                Console.WriteLine("City name not found - using Huntsville instead");
                city_name = "Huntsville";
                url = $"http://api.openweathermap.org/data/2.5/weather?q={city_name}&units=imperial&appid={api_key}";
                weather = client.GetStringAsync(url).Result;
            }

            //Parses the JSON data to get juet the temp
            var response = JObject.Parse(weather).GetValue("main").ToString();
            var temp = JObject.Parse(response).GetValue("temp");

            Console.WriteLine($"The current temperature in {city_name} is {temp} degrees Fahrenheit.");
        }
    }
}
