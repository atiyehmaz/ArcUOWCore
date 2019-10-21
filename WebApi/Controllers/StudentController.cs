using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using Data.UnitOfWork;
using Service;
using Service.BaseService;
using Microsoft.AspNetCore.Cors;
using Domain;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IStudentService _studentService;

        public StudentController(IUnitOfWork unitOfWork, IStudentService studentService)
        {
            this._unitOfWork = unitOfWork;
            this._studentService = studentService;
        }

        [HttpGet]
        [HttpOptions]
        [Route("GetStudents")]
        public IActionResult GetStudents()
        {
            var list = _studentService.GetAll();
            return Ok(list);
        }

        [HttpGet]
        [HttpOptions]
        [Route("GetStudentById")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentService.GetById(id);
            return Ok(student);
        }

        [HttpPost]
        [Route("CreateStudent")]
        [EnableCors("AllowOrigin")]
        public IActionResult CreateStudent([FromBody]Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CreateTransaction();
                    _studentService.Insert(student);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();
                    if (_unitOfWork.Successful == true)
                    {
                        return Ok();
                    }

                }
                catch (Exception)
                {

                    _unitOfWork.Rollback();
                    return BadRequest();
                }

            }
            return BadRequest();
        }

        [HttpPost]
        [Route("DeleteStudent")]
        [EnableCors("AllowOrigin")]
        public IActionResult DeleteStudent([FromBody]Student student)
        {
            if (student.Id != 0)
            {
                try
                {
                    _unitOfWork.CreateTransaction();
                    _studentService.Delete(student.Id);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();
                    if (_unitOfWork.Successful == true)
                    {
                        return Ok();
                    }
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}