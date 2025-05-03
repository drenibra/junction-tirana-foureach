using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Implementation.Interfaces;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("user/{personId}")]
        public async Task<ActionResult<List<ExpenseDto>>> GetUserExpenses(string personId)
            => await _expenseService.GetUserExpensesAsync(personId);

        [HttpPost]
        public async Task<ActionResult> CreateExpense(CreateExpenseDto dto)
            => await _expenseService.CreateExpenseAsync(dto);

        [HttpGet("user/{personId}/summary")]
        public async Task<ActionResult<ExpenseSummaryDto>> GetExpenseSummary(string personId)
            => await _expenseService.GetExpenseSummaryAsync(personId);

        [HttpDelete("{id}/user/{personId}")]
        public async Task<ActionResult> DeleteExpense(int id, string personId)
            => await _expenseService.DeleteExpenseAsync(id, personId);
    }
}