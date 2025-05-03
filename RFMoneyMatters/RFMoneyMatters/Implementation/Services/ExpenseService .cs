using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Implementation.Interfaces;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Implementation.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public ExpenseService(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<ExpenseDto>>> GetUserExpensesAsync(string personId)
        {
            var expenses = await _context.Expenses
                .Where(e => e.PersonId.Equals(personId))
                .OrderByDescending(e => e.Date)
                .ToListAsync();

            return new OkObjectResult(_mapper.Map<List<ExpenseDto>>(expenses));
        }

        public async Task<ActionResult> CreateExpenseAsync(CreateExpenseDto dto)
        {
            var expense = _mapper.Map<Expense>(dto);
            expense.Date = DateTime.SpecifyKind(expense.Date, DateTimeKind.Utc);

            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();

            return new OkObjectResult("Expense successfully added");
        }

        public async Task<ActionResult<ExpenseSummaryDto>> GetExpenseSummaryAsync(string personId)
        {
            var expenses = await _context.Expenses
                .Where(e => e.PersonId.Equals(personId))
                .ToListAsync();

            if (!expenses.Any())
            {
                return new OkObjectResult(new ExpenseSummaryDto
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

            return new OkObjectResult(summary);
        }

        public async Task<ActionResult> DeleteExpenseAsync(int id, string personId)
        {
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.PersonId.Equals(personId));

            if (expense == null)
                return new NotFoundObjectResult("Expense not found or you don't have access to delete it");

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Expense successfully deleted");
        }
    }
}