using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Service.BaseService;

namespace Service
{
    public interface ITeacherService : IBaseService<Teacher>
    {
        IEnumerable<Teacher> GetAll();

        Teacher GetById(Teacher id);

        void Insert(Teacher teacher);


        void Update(Teacher teacher);

        void Delete(int id);
    }
}
