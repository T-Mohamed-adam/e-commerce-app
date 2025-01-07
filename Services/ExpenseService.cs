using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Expense;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper; 

        public ExpenseService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<ExpenseResponse>> GetAllExpenses()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var expenses = await _dbContext.Expenses
                .Where(e => e.MembershipNumer == membershipNumber)
                .ToListAsync();

            return _mapper.Map<List<ExpenseResponse>>(expenses);
        }

        public async Task<ExpenseResponse?> GetExpenseById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var expense = await _dbContext.Expenses
                .Where(e => e.MembershipNumer == membershipNumber && e.Id == id)
                .FirstOrDefaultAsync();

            if (expense is null) 
            {
                return null;
            }

            return _mapper.Map<ExpenseResponse>(expense);
        }

        public async Task<ExpenseResponse?> AddExpense(ExpenseAddRequest expenseAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Expense expense = new Expense() 
           {
               ExpenseName = expenseAddRequest.ExpenseName,
               ExpenseItemId = expenseAddRequest.ExpenseItemId,
               Notes = expenseAddRequest.Notes,
               Amount = expenseAddRequest.Amount,
               Status = expenseAddRequest.Status!,
               Paid = expenseAddRequest.Paid,
               MembershipNumer = membershipNumber
           };

            await _dbContext.AddRangeAsync(expense);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ExpenseResponse>(expense);
        }

        public async Task<ExpenseResponse?> UpdateExpense(int id, ExpenseUpdateRequest expenseUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var expense = await _dbContext.Expenses
                .Where(e => e.MembershipNumer == membershipNumber && e.Id == id)
                .FirstOrDefaultAsync();

            if (expense is null)
            {
                return null;
            }

            expense.ExpenseName = expenseUpdateRequest.ExpenseName;
            expense.ExpenseItemId = expenseUpdateRequest.ExpenseItemId;
            expense.Notes = expenseUpdateRequest.Notes;
            expense.Amount = expenseUpdateRequest.Amount;
            expense.Paid = expenseUpdateRequest.Paid;
            expense.Status = expenseUpdateRequest.Status!;

           await _dbContext.SaveChangesAsync();

            return _mapper.Map<ExpenseResponse>(expense);
        }

        public async Task<ExpenseResponse?> DeleteExpense(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var expense = await _dbContext.Expenses
                .Where(e => e.MembershipNumer == membershipNumber && e.Id == id)
                .FirstOrDefaultAsync();

            if (expense is null)
            {
                return null;
            }

            _dbContext.Expenses.Remove(expense);    
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ExpenseResponse>(expense);
        }

    }
}
