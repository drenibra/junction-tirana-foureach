using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public ExpensesController(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("user/{personId}")]
        public async Task<ActionResult<List<ExpenseDto>>> GetUserExpenses(int personId)
        {
            var expenses = await _context.Expenses
                .Where(e => e.PersonId.Equals(personId))
                .OrderByDescending(e => e.Date)
                .ToListAsync();

            return Ok(_mapper.Map<List<ExpenseDto>>(expenses));
        }

        [HttpPost]
        public async Task<ActionResult> CreateExpense(CreateExpenseDto dto)
        {
            var expense = _mapper.Map<Expense>(dto);
            expense.Date = DateTime.SpecifyKind(expense.Date, DateTimeKind.Utc);

            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();

            return Ok("Expense successfully added");
        }

        [HttpGet("user/{personId}/summary")]
        public async Task<ActionResult<ExpenseSummaryDto>> GetExpenseSummary(int personId)
        {
            var expenses = await _context.Expenses
                .Where(e => e.PersonId.Equals(personId))
                .ToListAsync();

            if (!expenses.Any())
            {
                return Ok(new ExpenseSummaryDto
                {
                    Total = 0,
                    Needs = 0,
                    Wants = 0,
                    NeedsPercentage = 0,
                    WantsPercentage = 0
                });
            }

            var total = expenses.Sum(e => e.Amount);
            var needs = expenses.Where(e => e.Category == ExpenseCategory.Need).Sum(e => e.Amount);
            var wants = expenses.Where(e => e.Category == ExpenseCategory.Want).Sum(e => e.Amount);

            var summary = new ExpenseSummaryDto
            {
                Total = total,
                Needs = needs,
                Wants = wants,
                NeedsPercentage = Math.Round((double)(needs / total) * 100, 2),
                WantsPercentage = Math.Round((double)(wants / total) * 100, 2)
            };

            return Ok(summary);
        }

        [HttpDelete("{id}/user/{personId}")]
        public async Task<ActionResult> DeleteExpense(int id, int personId)
        {
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.PersonId.Equals(personId));

            if (expense == null)
                return NotFound("Expense not found or you don't have access to delete it");

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return Ok("Expense successfully deleted");
        }

    }
}
