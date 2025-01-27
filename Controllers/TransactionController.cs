using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BudgetManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly BudgetContext _context;

        public TransactionController(BudgetContext context)
        {
            _context = context;
        }

        // get: api/Transaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.Include(testc=> testc.category).ToListAsync();
        }

        // GET: api/Transaction/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.Include(t => t.category).FirstOrDefaultAsync(t => t.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }
            return transaction;
        }

        //POST: api/transaction
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionDto transactionDto)
        {
            var transaction = new Transaction
            {
                Title = transactionDto.Title,
                Amount = transactionDto.Amount,
                Date = transactionDto.Date,
                CategoryId = transactionDto.CategoryId
            };
            
            
            
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }

        //PUT: api/transaction/{id}

        [HttpPut("{id}")]
        public async Task<IActionResult> putTransaction(int id, TransactionDto transactionDto)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            { 
                return NotFound();
            }
            transaction.Title = transactionDto.Title;
            transaction.Amount = transactionDto.Amount;
            transaction.Date = transactionDto.Date;
            transaction.CategoryId = transactionDto.CategoryId;

            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            { 
                if (!_context.Transactions.Any(t => t.Id ==id))
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

        //Delete: api/transaction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        { 
        var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}




