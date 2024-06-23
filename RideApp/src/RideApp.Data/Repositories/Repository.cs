using Microsoft.EntityFrameworkCore;
using RideApp.Data.Context;
using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain;

public class Repository<T> : IRepository<T> where T: Entity
{
    protected readonly AppDbContext Context;

    protected Repository(AppDbContext context)
    {
        Context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<T> Create(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public T Update(T entity)
    {
        Context.Update(entity);
        Context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
        return entity;
    }
}