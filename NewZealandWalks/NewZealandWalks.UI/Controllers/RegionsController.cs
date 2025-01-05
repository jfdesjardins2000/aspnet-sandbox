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
            HttpClient client = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7252/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            HttpClient client = _httpClientFactory.CreateClient();
            RegionDto? response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7252/api/regions/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpRequestMessage httpRequestMessage = new()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7252/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
            if (respose is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                HttpResponseMessage httpResponseMessage = await client.DeleteAsync($"https://localhost:7252/api/regions/{request.Id.ToString()}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {

                //throw;
            }

            return View("Edit");

        }
    }
}
