using api.Data;
using api.Data.Models;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var item = await _context.items.ToListAsync();
            return Ok(item);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetItemById(int id)
        {
            var items = await _context.items.FirstOrDefaultAsync(x => x.Id == id);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }
        [HttpGet("GetItemByCategoryId/{idCategory}")]
        public async Task<IActionResult> GetItemByCategoryId(int idCategory)
        {
            var items = await _context.items.Where(x => x.CategoryId == idCategory).ToListAsync();
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromForm] itemmdls itemmdls)
        {
            using var stream = new MemoryStream();
            await itemmdls.Images.CopyToAsync(stream);
            var item = new Item
            {
                Name = itemmdls.Name,
                Price = itemmdls.Price,
                CategoryId = itemmdls.CategoryId,
                Notes = itemmdls.Notes,
                Images = stream.ToArray()
            };
            await _context.items.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok(item);

        }
        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] itemmdls ite)
        {
            var item = await _context.items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var isCategoryExist = await _context.categories.AnyAsync(x => x.Id == ite.CategoryId);
            if (isCategoryExist)
            {
                return NoContent();
            }
            if (ite.Images != null)
            {
                using var stream = new MemoryStream();
                await ite.Images.CopyToAsync(stream);
                item.Images = stream.ToArray();

            }
            item.Name = ite.Name;
            item.Price = ite.Price;
            item.Notes = ite.Notes;
            item.CategoryId = ite.CategoryId;
            _context.SaveChanges();
            return Ok(item);
        }
       
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var item = await _context.items.SingleOrDefaultAsync(x => x.Id == id);
                if (item == null) { return NotFound(); }
                _context.items.Remove(item);
                await _context.SaveChangesAsync();
                return Ok(item);

            }
        }
    }


