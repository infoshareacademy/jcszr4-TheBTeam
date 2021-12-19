using System.Collections.Generic;
using TheBTeam.BLL.DAL.Entities;

namespace TheBTeam.BLL.DAL.Repository
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}