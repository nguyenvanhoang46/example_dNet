namespace example.Repository;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    T? GetById(int id);
    void Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
}