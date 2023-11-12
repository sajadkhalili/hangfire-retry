using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Headers;
using System.Net.Http;

namespace test
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public async Task Success_add_Category_as_category_child()
        {
            HttpClient _httpClient = new HttpClient();

       


            using var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Get;

            httpRequestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            httpRequestMessage.RequestUri = new Uri($"https://localhost:7049/WeatherForecast", UriKind.RelativeOrAbsolute);

            ArgumentNullException.ThrowIfNull(_httpClient);
            using var response = await _httpClient.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(1, 1);
        }

    }
}