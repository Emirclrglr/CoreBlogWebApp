using BlogApiDemo.DataAccessLayer;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Text;

namespace CoreBlogWebApp.Controllers
{
    public class EmployeeTestController : Controller
    {
        public async Task<IActionResult> EmployeeList()
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("http://localhost:5150/api/Default");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Class1>>(jsonString);
            return View(values);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Class1 class1)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(class1);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("http://localhost:5150/api/Default", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("EmployeeList", "EmployeeTest");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync($"https://localhost:7183/api/Default/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonEmployee = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Class1>(jsonEmployee);
                return View(values);
            }
            return RedirectToAction("EmployeeList", "EmployeeTest");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(Class1 class1)
        {
            HttpClient httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(class1);
            var content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync("https://localhost:7183/api/Default/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("EmployeeList", "EmployeeTest");
            }
            return View(class1);
            
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync($"https://localhost:7183/api/Default/{id}");
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("EmployeeList", "EmployeeTest");
            }
            return View();
        }
    }

    public class Class1
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

