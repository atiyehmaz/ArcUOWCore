using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Context;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository
{
    public class BaseRepository<T>: IBaseRepository<T> , IDisposable where T : class
    {
        private DbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;
        public DbContext Context { get; set; }
        protected virtual DbSet<T> Entities => _entities ?? (_entities = Context.Set<T>());

        public BaseRepository(DbContext context)
        {
            _isDisposed = false;
            Context = context;
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                Entities.Add(entity);
                Context.Entry(entity).State = EntityState.Added;
                if (Context == null || _isDisposed)
                {
                    //var optionsBuilder = new DbContextOptionsBuilder<UniversityDbContext>();
                    //optionsBuilder.UseSqlServer(Configuration.GetConnectionStringSecureValue("DefaultConnection"));
                    //Context = new UniversityDbContext(optionsBuilder.Options);

                }
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be 
                //called with Unit of work
            }
            catch (DbUpdateException dbEx)
            {
                foreach (var validationErrors in dbEx.Message)
                    foreach (var validationError in validationErrors.ToString())
                        _errorMessage += string.Format("Property: {0}", validationError.ToString()) + Environment.NewLine;
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }
                //Context.Configuration.AutoDetectChangesEnabled = false;
                Context.Set<T>().AddRange(entities);
                Context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                foreach (var validationErrors in dbEx.Message)
                {
                    foreach (var validationError in validationErrors.ToString())
                    {
                        _errorMessage += string.Format("Property: {0}", validationError.ToString(),
                        validationError.ToString()) + Environment.NewLine;
                    }
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    //Context = new UniversityDbContext();
                SetEntryModified(entity);
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (DbUpdateException dbEx)
            {
                foreach (var validationErrors in dbEx.Message)
                    foreach (var validationError in validationErrors.ToString())
                        _errorMessage += Environment.NewLine + string.Format("Property: {0}", validationError.ToString());
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public virtual void Delete(object id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException("id");
                //if (Context == null || _isDisposed)
                    //Context = new UniversityDbContext();
                T entity = Entities.Find(id);
                if (entity !=null)
                {
                    Entities.Remove(entity);
                }
            }

            catch (DbUpdateException dbEx)
            {
                foreach (var validationErrors in dbEx.Message)
                    foreach (var validationError in validationErrors.ToString())
                        _errorMessage += Environment.NewLine + string.Format("Property: {0}", validationError.ToString());
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
