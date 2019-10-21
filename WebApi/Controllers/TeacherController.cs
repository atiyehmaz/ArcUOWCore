using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Data.UnitOfWork;
using Service;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ITeacherService _teacherService;

        public TeacherController(IUnitOfWork unitOfWork, ITeacherService teacherService)
        {
            this._unitOfWork = unitOfWork;
            this._teacherService = teacherService;
        }

        [HttpGet]
        [HttpOptions]
        [Route("GetTeachers")]
        public IActionResult GetTeachers()
        {
            var list = _teacherService.GetAll();
            return Ok(list);
        }

        [HttpPost]
        [Route("CreateTeacher")]
        [EnableCors("AllowOrigin")]
        public IActionResult CreateTeacher([FromBody]Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CreateTransaction();
                    _teacherService.Insert(teacher);
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

        [HttpGet]
        [HttpOptions]
        [Route("GetTeacherById")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _teacherService.GetById(id);
            return Ok(teacher);
        }


        [HttpPost]
        [Route("DeleteTeacher")]
        [EnableCors("AllowOrigin")]
        public IActionResult DeleteTeacher([FromBody]Teacher teacher)
        {
            if (teacher.Id != 0)
            {
                try
                {
                    _unitOfWork.CreateTransaction();
                    _teacherService.Delete(teacher.Id);
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