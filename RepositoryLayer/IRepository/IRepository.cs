using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;
namespace RepositoryLayer.IRepository
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        Task<bool> SaveChangesAsync();
    }
}
