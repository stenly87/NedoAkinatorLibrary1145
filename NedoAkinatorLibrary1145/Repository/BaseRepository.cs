using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NedoAkinatorLibrary1145.DB;
using NedoAkinatorLibrary1145.Model;

namespace NedoAkinatorLibrary1145.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected static DB.User01Context? GetDB()
        {
            return Init.app.Services.GetService<DB.User01Context>();
        }

        public abstract void Create(T item);
        public abstract void Delete(int id);

        public void Dispose()
        {

        }

        public abstract T Get(int id);
        public abstract IEnumerable<T> GetList();

        public void Save()
        {
            var db = GetDB();
            db.SaveChanges();
        }

        public abstract void Update(T item);
    }
}