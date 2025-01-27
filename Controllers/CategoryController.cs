using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetManagerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;





namespace BudgetManagerAPI.Controllers

{
    [ApiController]

    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
       private readonly BudgetContext _context;
       
       public CategoryController(BudgetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.Include(c => c.Transactions).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>>GetCategory(int id)
        {
            var category = await _context.Categories.Include(c => c.Transactions).FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }


        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            
            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryDto.Name;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Categories.Any(c=> c.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
           
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return NoContent(); 

            }












    }
}

