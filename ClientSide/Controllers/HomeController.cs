using ClientSide.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace ClientSide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string baseAddress;

        public HomeController(ILogger<HomeController> logger)
        {
            this.baseAddress = "https://localhost:7140";
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
            
        }
        #region GET ACTIONS without parameters
        public async Task<IActionResult> GetTemperature()
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            var requestUrl = "WeatherForecast";
            var response = await client.GetAsync(requestUrl);
            var responseString = await response.Content.ReadAsStringAsync();
            var myDeserializedClass = JsonConvert.DeserializeObject<List<WeatherClass>>(responseString);
            return View(myDeserializedClass);
        }

        public async Task<IActionResult> GetValue()
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            var response = await client.GetAsync("api/actions/GetValue");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            ResponseModel<int>? intResponse = JsonConvert.DeserializeObject<ResponseModel<int>>(responseString);
            return Content(responseString);
        }
        public async Task<IActionResult> GetList()
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            var response = await client.GetAsync("api/actions/GetList");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel<List<int>>>(responseString);
            return Ok(responseString);
        }
        public async Task<IActionResult> GetObject()
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            var response = await client.GetAsync("api/actions/getobject");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel<Student>>(responseString);
            return Ok(result);
        }
        public async Task<IActionResult> GetObjectList()
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            var response = await client.GetAsync("api/actions/getobjectlist");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel<List<Student>>> (responseString);
            return Ok(result);
        }
        #endregion

        #region GET ACTIONS with parameters
        public async Task<IActionResult> GetNextNumber()
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            var response = await client.GetAsync($"api/actions/GetNextNumber?number={10}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel<int>> (responseString);
            return Ok(result);
        }

        public async Task<IActionResult> GetSortedList()
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            List<int> numbers = new List<int>() { 6,5,8,4,6,5,7,2,1,5};
            string numberString = JsonConvert.SerializeObject(numbers);
            string urlEncodedJson = Uri.EscapeDataString(numberString);
            var response = await client.GetAsync($"api/actions/GetSortedList?numbers={urlEncodedJson}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel<List<int>>>(responseString); 
            return Ok(result);
        }
        public async Task<IActionResult> GetNormalizedStudent()
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Action","1");
            client.BaseAddress = new Uri(baseAddress);
            var studentObject = new Student() { Id = 1, Name = "Irfan", Description = "Test Description" };
            var studentSerializedString = JsonConvert.SerializeObject(studentObject);
            string urlEncodedJson = Uri.EscapeDataString(studentSerializedString);
            var response = await client.GetAsync($"api/actions/GetNormalizedStudent?student={urlEncodedJson}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel<Student>>(responseString);
            return Ok(result);
        }
        #endregion

        #region POST ACTIONS
        public async Task<IActionResult> PostNumber()
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            var numberSerialized = JsonConvert.SerializeObject(101);
            var content = new StringContent(numberSerialized, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/actions/SaveNumber", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel<int>>(responseString);
            return Ok(result);
        }


        #endregion




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
