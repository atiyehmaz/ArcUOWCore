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
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICourseService _courseService;

        public CourseController(IUnitOfWork unitOfWork, ICourseService courseService)
        {
            this._unitOfWork = unitOfWork;
            this._courseService = courseService;
        }

        [HttpGet]
        [HttpOptions]
        [Route("GetCourses")]
        public IActionResult GetCourses()

        {
            var list = _courseService.GetAll();
            return Ok(list);
        }

        [HttpPost]
        [Route("CreateCourse")]
        [EnableCors("AllowOrigin")]
        public IActionResult CreateCourse([FromBody]Course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CreateTransaction();
                    _courseService.Insert(course);
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
        [Route("DeleteCourse")]
        public IActionResult DeleteCourse(int id)
        {
            if (id != 0)
            {
                try
                {
                    _unitOfWork.CreateTransaction();
                    _courseService.Delete(id);
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