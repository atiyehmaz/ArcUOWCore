using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Service.BaseService;
using Data.Repository;
using Data;
using Data.UnitOfWork;

namespace Service
{
    public class TeacherService : BaseService<Teacher>, ITeacherService
    {
        private readonly IBaseRepository<Teacher> teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeacherService(IUnitOfWork unitOfWork, IBaseRepository<Teacher> teacherRepository)
                              : base(unitOfWork, teacherRepository)
        {
            this.teacherRepository = teacherRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Teacher> GetTeacher()
        {
            return teacherRepository.GetAll();
        }

        public Teacher GetById(Teacher id)
        {
            return teacherRepository.GetById(id);
        }

        public void Insert(Teacher teacher)
        {
            teacherRepository.Insert(teacher);
        }

        public void Delete(int id)
        {
            teacherRepository.Delete(id);
        }

        public void Update(Teacher teacher)
        {
            teacherRepository.Update(teacher);
        }

    }
}
