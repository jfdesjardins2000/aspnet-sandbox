using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.UI.Models.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace NewZealandWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new();

            try
            {
                //Get All Regions from Web API
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.GetAsync("https://localhost:7252/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                //var stringResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                //ViewBag.Response = stringResponseBody;

                //IEnumerable<RegionDto>? response = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>();
                //return Ok(response);

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());


            }
            catch (Exception ex)
            {
                // log the ex...
                throw;
            }


            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7252/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }
    }
}
