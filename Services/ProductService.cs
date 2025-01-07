using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Product;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileHelper _fileHelper;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext dbContext, FileHelper fileHelper,
            MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _fileHelper = fileHelper;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var products = await _dbContext.Products
                .Where(p => p.MembershipNumber == membershipNumber && p.IsDeleted == false)
                .Include(p => p.Category).Include(p => p.Tax)
                .ToListAsync();

            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task<ProductResponse?> GetProductById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var product = await _dbContext.Products
                .Where(p => p.Id == id && p.MembershipNumber == membershipNumber && p.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (product is null) 
            {
                return null;
            }

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Product product = new Product() 
            {
                NameAr = productAddRequest.NameAr,
                NameEn = productAddRequest.NameEn,
                Description = productAddRequest.Description,
                BrandId = productAddRequest.BrandId,
                CategoryId = productAddRequest.CategoryId,
                TaxId = productAddRequest.TaxId,
                UnitId = productAddRequest.UnitId,
                Price = productAddRequest.Price,
                PriceIncludeTax = await GetPriceIncludeTaxAsync(productAddRequest.Price, productAddRequest.UnitId),
                MembershipNumber = membershipNumber,
            };

            if (productAddRequest.Image != null) 
            {
                product.ImageUrl = await _fileHelper.SaveImageAsync(productAddRequest.Image);
            }

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(product);
        }


        public async Task<ProductResponse?> UpdateProduct(int id, ProductUpdateRequest productUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var product = await _dbContext.Products
                .Where(p => p.Id == id && p.MembershipNumber == membershipNumber && p.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (product is null)
            {
                return null;
            }

            product.NameAr = productUpdateRequest.NameAr;
            product.NameEn = productUpdateRequest.NameEn;
            product.Description = productUpdateRequest.Description;
            product.BrandId = productUpdateRequest.BrandId;
            product.CategoryId = productUpdateRequest.CategoryId;
            product.TaxId = productUpdateRequest.TaxId;
            product.UnitId = productUpdateRequest.UnitId;
            product.Price = productUpdateRequest.Price;
            product.PriceIncludeTax = await GetPriceIncludeTaxAsync(productUpdateRequest.Price, productUpdateRequest.TaxId);
            product.IsActive = productUpdateRequest.IsActive;

            if (productUpdateRequest.Image != null)
            {
                product.ImageUrl = await _fileHelper.SaveImageAsync(productUpdateRequest.Image);
            }

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse?> DeleteProduct(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var product = await _dbContext.Products
                .Where(p => p.Id == id && p.MembershipNumber == membershipNumber && p.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (product is null)
            {
                return null;
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(product);
        }

        /// <summary>
        /// Calculate price include tax from given price and tax value
        /// </summary>
        /// <param name="price"></param>
        /// <param name="taxId"></param>
        /// <returns>
        /// Return price include tax
        /// </returns>
        private async Task<decimal> GetPriceIncludeTaxAsync(decimal price, int taxId) 
        {
            // Fetch tax data
            var tax = await _dbContext.Taxes.FindAsync(taxId);

            // Check if tax data not found price is 0
            if (tax is null || price < 0) 
            {
                return 0;
            }

            // Calculate tax value
            decimal taxValue = price * tax.Value / 100;

            // Calculate price include tax
            decimal priceIncludeTax = price + taxValue;

            // return price include tax
            return priceIncludeTax;
        }
    }
}
