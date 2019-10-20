using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data.Repository;
using Data;
using Data.UnitOfWork;
using Service.BaseService;

namespace Service
{
    public class CourseService : BaseService<Course>, ICourseService
    {
        private readonly IBaseRepository<Course> courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork, IBaseRepository<Course> courseRepository)
                              : base(unitOfWork, courseRepository)
        {
            this.courseRepository = courseRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Course> GetCourse()
        {
            return courseRepository.GetAll();
        }

        public Course GetById(Course id)
        {
            return courseRepository.GetById(id);
        }

        public void Insert(Course course)
        {
            courseRepository.Insert(course);
        }

        public void Delete(int id)
        {
            courseRepository.Delete(id);
        }

        public void Update(Course course)
        {
            courseRepository.Update(course);
        }


    }
}
