using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using MVCApp.Models;
using Newtonsoft.Json;
using MVCApp.ViewModels;

namespace MVCApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly RestClient client = new RestClient("http://localhost:9090/api/");

        public IActionResult Index()
        {
            RestRequest request = new RestRequest("Course/GetCourses", Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
            };
            IRestResponse<List<Course>> response = client.Execute<List<Course>>(request);

            var entity = JsonConvert.DeserializeObject<List<Course>>(response.Content);
            return View(entity);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RestRequest request = new RestRequest("Teacher/GetTeachers", Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
            };
            IRestResponse<List<Teacher>> response = client.Execute<List<Teacher>>(request);

            var entity = JsonConvert.DeserializeObject<List<Teacher>>(response.Content);
            CourseViewModel course = new CourseViewModel
            {
                Teachers = entity
            };
            return View(course);
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

        [HttpGet]
        public IActionResult Details(int id)
        {
            var request = new RestRequest("Course/GetCourseById", Method.GET) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddParameter("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(id), "application/json", ParameterType.RequestBody);

            IRestResponse<Course> response = client.Execute<Course>(request);
            var entity = JsonConvert.DeserializeObject<Course>(response.Content);

            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var request = new RestRequest("Course/GetCourseById", Method.GET) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddParameter("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(id), "application/json", ParameterType.RequestBody);

            IRestResponse<Course> response = client.Execute<Course>(request);
            var entity = JsonConvert.DeserializeObject<Course>(response.Content);

            return View(entity);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteCourse(Course course)
        {

            var request = new RestRequest("Course/DeleteCourse", Method.POST) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(course), "application/json", ParameterType.RequestBody);

            var response = client.Execute(request);

            return RedirectToAction("Index");
        }
    }
}