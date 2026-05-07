using CrudAppUsingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CrudAppUsingWebAPI.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7013/api/StudentAPI";
        private HttpClient client = new HttpClient(); //creating object of httpclient class

        [HttpGet]
        public IActionResult Index()
        {
            List<Student> studenttbl = new List<Student>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            //client ek object he is http client ka iski help se get request pass kar rhe he and url ki help se hit kar rhe , getasync httpclient ka method he

            if (response.IsSuccessStatusCode)
            {
                String result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Student>>(result);

                if(data != null)
                {
                    studenttbl = data;
                }
            }
            return View(studenttbl);
        }

//-----------------------------------get method------------------------------------------

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student std)
        {
            if (!ModelState.IsValid)
            {
                return Content("ModelState is INVALID");
            }

            string data = JsonConvert.SerializeObject(std);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await client.PostAsync(url, content);

            //Important: API ka response dekho
            //string result = await response.Content.ReadAsStringAsync();
            //return Content("Status: " + response.StatusCode + " | Response: " + result);

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "student added..";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Student std = new Student();

            HttpResponseMessage response = await client.GetAsync($"{url}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Student>(result);

                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student std)
        {
            if (!ModelState.IsValid)
            {
                return View(std);
            }

            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"{url}/{std.id}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Student updated successfully";
                return RedirectToAction("Index");
            }

            string result = await response.Content.ReadAsStringAsync();
            return Content("Error: " + result);
        }

        //-----------------details showing section----------
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Student std = new Student();

            HttpResponseMessage response = await client.GetAsync($"{url}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Student>(result);

                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }
        //---------------delete action section ------------------------
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Student std = new Student();

            HttpResponseMessage response = await client.GetAsync($"{url}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Student>(result);

                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {

            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result ;

            if (response.IsSuccessStatusCode)
            {
                TempData["delete_message"] = "Student deleted successfully";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
