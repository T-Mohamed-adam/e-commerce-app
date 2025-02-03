using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Category;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileHelper _fileHelper;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext dbContext, FileHelper fileHelper,
            MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _fileHelper = fileHelper;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var categories = await _dbContext.Categories
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<CategoryResponse>>(categories);
        }

        public async Task<CategoryResponse?> GetCategoryById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var category = await _dbContext.Categories
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category is null) 
            {
                return null;
            }

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse?> AddCategory(CategoryAddRequest categoryAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Category category = new Category() 
            {
                NameAr = categoryAddRequest.NameAr,
                NameEn = categoryAddRequest.NameEn,
                Description = categoryAddRequest.Description,
                MembershipNumber = membershipNumber
            };

            if (categoryAddRequest.Image != null) 
            {
                category.ImageUrl = await _fileHelper.SaveImageAsync(categoryAddRequest.Image);
            }

            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(category);

        }

        public async Task<CategoryResponse?> UpdateCategory(int id, CategoryUpdateRequest categoryUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var category = await _dbContext.Categories
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
            {
                return null;
            }

            category.NameAr = categoryUpdateRequest.NameAr;
            category.NameEn = categoryUpdateRequest.NameEn;
            category.Description = categoryUpdateRequest.Description;
            category.IsActive = categoryUpdateRequest.IsActive;

            if (categoryUpdateRequest.Image != null) 
            {
                category.ImageUrl = await _fileHelper.SaveImageAsync(categoryUpdateRequest.Image);
            }

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse?> DeleteCategory(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var category = await _dbContext.Categories
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
            {
                return null;
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(category);
        }
    }
}
