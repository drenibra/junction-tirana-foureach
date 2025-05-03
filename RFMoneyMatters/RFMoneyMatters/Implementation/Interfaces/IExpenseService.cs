using Microsoft.AspNetCore.Mvc;
using RFMoneyMatters.DTOs;

namespace RFMoneyMatters.Implementation.Interfaces
{
    public interface IExpenseService
    {
        Task<ActionResult<List<ExpenseDto>>> GetUserExpensesAsync(string personId);
        Task<ActionResult> CreateExpenseAsync(CreateExpenseDto dto);
        Task<ActionResult<ExpenseSummaryDto>> GetExpenseSummaryAsync(string personId);
        Task<ActionResult> DeleteExpenseAsync(int id, string personId);
    }
}