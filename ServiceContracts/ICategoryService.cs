using TagerProject.Models.Dtos.Category;

namespace TagerProject.ServiceContracts
{
    public interface ICategoryService
    {
        public Task<List<CategoryResponse>> GetAllCategories();
        public Task<CategoryResponse?> GetCategoryById(int id);
        public Task<CategoryResponse?> AddCategory(CategoryAddRequest categoryAddRequest);
        public Task<CategoryResponse?> UpdateCategory(int id, CategoryUpdateRequest categoryUpdateRequest);
        public Task<CategoryResponse?> DeleteCategory(int id);

    }
}
