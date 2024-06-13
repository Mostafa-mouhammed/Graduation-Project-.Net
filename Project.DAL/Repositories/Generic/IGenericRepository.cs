namespace Project.DAL.Repositories.Generic;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T>? Getone(int id);
    Task Add(T t);
    Task AddRange(IEnumerable<T> t);
    void Update(T t);
    void Delete(T t);
}
