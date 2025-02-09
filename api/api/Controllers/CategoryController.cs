using api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data.Models;
namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var cat = await _context.categories.ToListAsync();
            return Ok(cat);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(string name)
        {
            Category c = new() { Name = name };
            await _context.categories.AddAsync(c);
            _context.SaveChanges();
            return Ok(c);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            var c = await _context.categories.SingleOrDefaultAsync(x => x.Id == category.Id);
            if (c == null)
            {
                return NoContent();
            }
            c.Name = category.Name;
            _context.SaveChanges();
            return Ok(c);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteCategory(int id)
        {
            var c = await _context.categories.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null)
            {
                return NotFound();
            }
            _context.categories.Remove(c);
            _context.SaveChanges();
            return Ok(c);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>Getonecat(int id) {
            var s = await _context.categories.SingleOrDefaultAsync(f => f.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            return Ok(s);
        }

    }
}

