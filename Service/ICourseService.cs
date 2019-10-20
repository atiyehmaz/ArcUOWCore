using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Service.BaseService;

namespace Service
{
    public interface ICourseService: IBaseService<Course>
    {
        IEnumerable<Course> GetAll();

        Course GetById(Course id);

        void Insert(Course deposit);


        void Update(Course deposit);

        void Delete(int id);
    }
}
