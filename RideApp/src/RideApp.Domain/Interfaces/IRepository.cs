using RideApp.Domain.Entities;

namespace RideApp.Domain.Interfaces;

public interface IRepository<T> where T: Entity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(Guid id);
    Task<T> Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}