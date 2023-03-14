using example.Data;
using example.Entities;
using Microsoft.EntityFrameworkCore;

namespace example.Repository;

public class ProductRepository : IRepository<Product>
{
    private readonly MyDbContext _dbContext;

    public ProductRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Product> GetAll()
    {
        return _dbContext.Products;
    }

    public Product? GetById(int id)
    {
        return _dbContext.Products.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product entity)
    {
        _dbContext.Products.Add(entity);
    }

    public bool Update(Product entity)
    {
        try
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return true;
        }
        catch (Exception e)
        {
           return false;
        }
    }

    public bool Delete(Product entity)
    {
        try
        {
            _dbContext.Products.Remove(entity);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}