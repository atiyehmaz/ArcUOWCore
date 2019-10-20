using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using Data.Repository;
using Data.UnitOfWork;

namespace Service.BaseService
{
    public class BaseService<T> : IBaseService<T> where T : class
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IBaseRepository<T> baseRepository;

        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<T> baseRepository)
        {
            this.unitOfWork = unitOfWork;
            this.baseRepository = baseRepository;
        }

        public void Delete(object id)
        {
            baseRepository.Delete(id);
        }

        public IEnumerable<T> GetAll()
        {
            return baseRepository.GetAll();
        }

        public T GetById(object id)
        {
           return baseRepository.GetById(id);
        }

        public void Insert(T entity)
        {
            baseRepository.Insert(entity);
        }

        public void Update(T entity)
        {
            baseRepository.Update(entity);
        }
    }
}
