using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DHS.WEB.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DHS.WEB.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Getall()
        {
            IEnumerable<Student> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44351/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Students");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Student>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else 
                {
              

                    students = Enumerable.Empty<Student>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return Json(students);
        }
        [HttpPost]
        public JsonResult Create(Student STD)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44351/api/");
              
                var responseTask = client.PostAsJsonAsync<Student>("Students",STD);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { success = true});

                }
                else 
                {

                    return Json(new { success = false});
                }
            }
        }
        [HttpPut]
        public JsonResult Edit(Student STD)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44351/api/");

                
                var putTask = client.PutAsJsonAsync<Student>("Students", STD);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return Json(new { success = true });

                }
            }
            return Json(new { success = false });
        }
        [HttpDelete]
        public JsonResult Delete(int ID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44351/api/");
                var responseTask = client.DeleteAsync("Students/"+ID.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { success = true });

                }
                else
                {

                    return Json(new { success = false });
                    //  ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
        }
    }
}