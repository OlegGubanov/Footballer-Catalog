namespace Footballer_Catalog.Repositories;

public interface IRepository<T>
{
    public Task<bool> Exists(int id);
    public Task<List<T>> GetAll();
    public Task<T> Get(int id);
    public Task Add(T item);
    public Task Edit(T item);
    public Task Delete(int id);
    public Task SaveChanges();
}