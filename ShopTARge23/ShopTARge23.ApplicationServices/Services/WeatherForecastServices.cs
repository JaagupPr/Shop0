using Nancy.Json;
using ShopTARge23.Core.Dto.WeatherDtos.AccuWeatherDtos;
using System.Net;
namespace ShopTARge23.ApplicationServices.Services
{
    public class WeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            string accuApiKey = "2LBA498kppCQjyb9ZAh5IgNuMYgZZDEr";
            string url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={accuApiKey}&q={dto.CityName}";
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                AccuLocationRootDto accuResult = new JavaScriptSerializer().Deserialize<AccuLocationRootDto>(json);
            }
            return dto;
        }
    }
}