using TagerProject.Models.Dtos.Product;

namespace TagerProject.ServiceContracts
{
    public interface IProductService
    {
        /// <summary>
        /// Fetch products list based on membership number
        /// </summary>
        /// <returns>
        /// Return list of products
        /// </returns>
        public Task<List<ProductResponse>> GetAllProducts();

        /// <summary>
        /// Fetch specific product data based on passed product id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Product data
        /// </returns>
        public Task<ProductResponse?> GetProductById(int id);

        /// <summary>
        /// Add new product to product list
        /// </summary>
        /// <param name="productAddRequest"></param>
        /// <returns>
        /// Return added product data 
        /// </returns>
        public Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);

        /// <summary>
        /// Update specific product data based on the passed product id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productUpdateRequest"></param>
        /// <returns>
        /// Return updated product data
        /// </returns>
        public Task<ProductResponse?> UpdateProduct(int id, ProductUpdateRequest productUpdateRequest);

        /// <summary>
        /// Delete specific product data based on the passed product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Return sccess message if product deleted sccessufully
        /// </returns>
        public Task<ProductResponse?> DeleteProduct(int id);
        
    }
}
