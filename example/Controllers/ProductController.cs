using example.Data;
using example.DTO;
using example.Entities;
using example.Repository;
using Microsoft.AspNetCore.Mvc;

namespace example.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly MyDbContext _context;

    public ProductController(ILogger<ProductController> logger, MyDbContext _context)
    {
        _logger = logger;
        this._context = _context;
    }

    // GET: api/<ToDoController>
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Lấy danh sách dữ liệu");
        var service = new ProductRepository(_context);
        var todoList = service.GetAll();
        return Ok(todoList);
    }

    // GET api/<ToDoController>/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation($"Lấy dữ liệu có ID là {id}");
        var service = new ProductRepository(_context);
        var products = service.GetAll();
        var product = products.FirstOrDefault(e => e.Id == id);
        if (product != null)
        {
            return Ok(product);
        }

        return NotFound();
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProductDto dto)
    {
        var service = new ProductRepository(_context);
        Product product = new Product()
        {
            Name = dto.Name,
            Image = dto.Image,
            Price = dto.Price,
            Quantity = dto.Quantity
        };
        service.Add(product);
        _context.SaveChanges();
        return Ok("Đã thêm đối tượng thành công");
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var service = new ProductRepository(_context);
        Product? product = service.GetById(id);
        if (product == null)
            return NotFound();
        bool isDelete = service.Delete(product);
        if (!isDelete)
            return BadRequest();
        _context.SaveChanges();
        return Ok("Xóa Thành Công");
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Put([FromRoute] int id, [FromBody] ProductDto dto)
    {
        var service = new ProductRepository(_context);
        var product = service.GetById(id);
        product.Name = dto.Name;
        product.Image = dto.Image;
        product.Price = dto.Price;
        product.Quantity = dto.Quantity;
        bool isUpdate = service.Update(product);
        if (!isUpdate)
            return BadRequest("Sửa thất bại");
        _context.SaveChanges();
        return Ok("Sửa thành công");
    }
}