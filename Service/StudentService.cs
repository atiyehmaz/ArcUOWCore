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
    public class StudentService : BaseService<Student>, IStudentService
    {
        private readonly IBaseRepository<Student> studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork, IBaseRepository<Student> studentRepository)
                              :base( unitOfWork, studentRepository)
        {
            this._unitOfWork = unitOfWork;
            this.studentRepository = studentRepository;
        }

        public IEnumerable<Student> GetStudents()
        {
            return studentRepository.GetAll();
        }

        public Student GetById(Student id)
        {
            return studentRepository.GetById(id);
        }
        
        public void Insert(Student student)
        {
            studentRepository.Insert(student);
        }

        public new void Delete(int id)
        {
            studentRepository.Delete(id);
        }

        //public void Save()
        //{
        //    customerRepository.Save();
        //}

        public new void Update(Student student)
        {
            studentRepository.Update(student);
        }

       
    }
}
