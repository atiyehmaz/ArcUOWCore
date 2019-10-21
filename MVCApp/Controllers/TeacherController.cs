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
    public class TeacherController : Controller
    {
        private RestClient client = new RestClient("http://localhost:9090/api/");

        public IActionResult Index()
        {
            RestRequest request = new RestRequest("Teacher/GetTeachers", Method.GET);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            IRestResponse<List<Teacher>> response = client.Execute<List<Teacher>>(request);

            var entity = JsonConvert.DeserializeObject<List<Teacher>>(response.Content);
            return View(entity);
        }

        [HttpGet]
        public IActionResult Create()
        {        
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateTeacher(Teacher teacher)
        {
            var request = new RestRequest("Teacher/CreateTeacher", Method.POST) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(teacher), "application/json", ParameterType.RequestBody);

            var response = client.Execute(request);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var request = new RestRequest("Teacher/GetTeacherById", Method.GET) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddParameter("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(id), "application/json", ParameterType.RequestBody);

            IRestResponse<Teacher> response = client.Execute<Teacher>(request);
            var entity = JsonConvert.DeserializeObject<Teacher>(response.Content);

            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var request = new RestRequest("Teacher/GetTeacherById", Method.GET) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddParameter("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(id), "application/json", ParameterType.RequestBody);

            IRestResponse<Teacher> response = client.Execute<Teacher>(request);
            var entity = JsonConvert.DeserializeObject<Teacher>(response.Content);

            return View(entity);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteTeacher(Teacher teacher)
        {

            var request = new RestRequest("Teacher/DeleteTeacher", Method.POST) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(teacher), "application/json", ParameterType.RequestBody);

            var response = client.Execute(request);

            return RedirectToAction("Index");
        }
    }
}