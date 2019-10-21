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
    public class StudentController : Controller
    {
        private readonly RestClient client = new RestClient("http://localhost:9090/api/");

        public IActionResult Index()
        {
            RestRequest request = new RestRequest("Student/GetStudents", Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
            };
            IRestResponse<List<Student>> response = client.Execute<List<Student>>(request);

            var entity = JsonConvert.DeserializeObject<List<Student>>(response.Content);
            return View(entity);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateStudent(Student student)
        {
            var request = new RestRequest("Student/CreateStudent", Method.POST) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(student), "application/json", ParameterType.RequestBody);

            var response = client.Execute(request);
            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult StudentDetails(int id)
        {
            var request = new RestRequest("Student/GetStudentById", Method.GET) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddParameter("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(id), "application/json", ParameterType.RequestBody);

            IRestResponse<Student> response = client.Execute<Student>(request);
            var entity = JsonConvert.DeserializeObject<Student>(response.Content);

            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var request = new RestRequest("Student/GetStudentById", Method.GET) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddParameter("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(id), "application/json", ParameterType.RequestBody);

            IRestResponse<Student> response = client.Execute<Student>(request);
            var entity = JsonConvert.DeserializeObject<Student>(response.Content);

            return View(entity);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteStudent(Student student)
        {

            var request = new RestRequest("Student/DeleteStudent", Method.POST) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(student), "application/json", ParameterType.RequestBody);

            var response = client.Execute(request);

            return RedirectToAction("Index");
        }
    }
}