using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Data.Repository;

namespace Data.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _context;
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _objTran;
        private Dictionary<string, object> _repositories;
        public bool Successful { get; set; }


        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void CreateTransaction()
        {
            _objTran = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _objTran.Commit();
        }

        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }


        public void Save()
        {
            try
            {

                int zeroOrOne = _context.SaveChanges();
                if (zeroOrOne == 1)
                {
                    Successful = true;
                }

            }
            catch (DbUpdateException dbEx)
            {
                foreach (var validationErrors in dbEx.Message)
                    foreach (var validationError in validationErrors.ToString())
                        _errorMessage += string.Format("Property: {0}", validationError.ToString()) + Environment.NewLine;
                throw new Exception(_errorMessage, dbEx);
            }
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }

        public BaseRepository<T> GenericRepository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, object>();
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<T>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (BaseRepository<T>)_repositories[type];
        }

    }
}
