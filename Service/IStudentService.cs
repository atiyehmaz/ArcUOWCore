using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Service.BaseService;

namespace Service
{
    public interface IStudentService : IBaseService<Student>
    {
        IEnumerable<Student> GetAll();

        Student GetById(Student id);

        void Insert(Student customer);

        void Update(Student customer);

        void Delete(int id);
    }
}
