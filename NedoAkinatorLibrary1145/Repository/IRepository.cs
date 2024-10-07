using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NedoAkinatorLibrary1145.Repository
{
    internal interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetList();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
