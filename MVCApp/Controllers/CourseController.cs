using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using MVCApp.Models;
using Newtonsoft.Json;

namespace MVCApp.Controllers
{
    public class CourseController : Controller
    {
        private RestClient client = new RestClient("http://localhost:9090/api/");

        public IActionResult Index()
        {
            RestRequest request = new RestRequest("Course/GetCourses", Method.GET);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            IRestResponse<List<Course>> response = client.Execute<List<Course>>(request);

            var entity = JsonConvert.DeserializeObject<List<Course>>(response.Content);
            return View(entity);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateCourse(Course course)
        {
            var request = new RestRequest("Course/CreateCourse", Method.POST) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(course), "application/json", ParameterType.RequestBody);

            var response = client.Execute(request);
            return RedirectToAction("Index");

        }

        public IActionResult DeleteCourse(int id)
        {
            var request = new RestRequest("Course/DeleteCourse", Method.POST) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddParameter("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(id), "application/json", ParameterType.RequestBody);

            var response = client.Execute(request);
            return View();
        }
    }
}