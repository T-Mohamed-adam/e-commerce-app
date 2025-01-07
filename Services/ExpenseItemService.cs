using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.ExpenseItem;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class ExpenseItemService : IExpenseItemService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public ExpenseItemService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<ExpenseItemResponse>> GetAllExpenseItems()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var expenseItems = await _dbContext.ExpenseItems
                .Where(e => e.MembershipNumber == membershipNumber && e.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<ExpenseItemResponse>>(expenseItems);
        }

        public async Task<ExpenseItemResponse?> GetExpenseItemById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var expenseItem = await _dbContext.ExpenseItems
                .Where(e => e.MembershipNumber == membershipNumber && e.IsDeleted == false && e.Id == id)
                .FirstOrDefaultAsync();
            if (expenseItem is null) 
            {
                return null;
            }

            return _mapper.Map<ExpenseItemResponse>(expenseItem);
        }

        public async Task<ExpenseItemResponse?> AddExpenseItem(ExpenseItemAddRequest expenseItemAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            ExpenseItem expenseItem = new ExpenseItem() 
            {
              NameAr = expenseItemAddRequest.NameAr,
              NameEn = expenseItemAddRequest.NameEn,
              MembershipNumber = membershipNumber,
            };

            await _dbContext.ExpenseItems.AddAsync(expenseItem);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ExpenseItemResponse>(expenseItem);

        }

        public async Task<ExpenseItemResponse?> UpdateExpenseItem(int id, ExpenseItemUpdateRequest expenseItemUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var expenseItem = await _dbContext.ExpenseItems
                .Where(e => e.MembershipNumber == membershipNumber && e.IsDeleted == false && e.Id == id)
                .FirstOrDefaultAsync();
            if (expenseItem is null)
            {
                return null;
            }

            expenseItem.NameAr = expenseItemUpdateRequest.NameAr;
            expenseItem.NameEn = expenseItemUpdateRequest.NameEn;
            expenseItem.IsActive = expenseItemUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ExpenseItemResponse>(expenseItem);
        }

        public async Task<ExpenseItemResponse?> DeleteExpenseItem(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var expenseItem = await _dbContext.ExpenseItems
                .Where(e => e.MembershipNumber == membershipNumber && e.IsDeleted == false && e.Id == id)
                .FirstOrDefaultAsync();
            if (expenseItem is null)
            {
                return null;
            }

            _dbContext.ExpenseItems.Remove(expenseItem);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ExpenseItemResponse>(expenseItem);
        }
    }
}
