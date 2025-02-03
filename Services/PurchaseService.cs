using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Pagination;
using TagerProject.Models.Dtos.Purchase;
using TagerProject.Models.Dtos.PurchaseItem;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ILogger<PurchaseService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public PurchaseService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper,
            IMapper mapper, ILogger<PurchaseService> logger) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<PurchaseResponse>> GetAllPurchases()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var purchases = await _dbContext.Purchases
                .Include(p => p.PurchaseItems)
                .Where(p => p.MembershipNumber == membershipNumber)
                .ToListAsync();

            return _mapper.Map<List<PurchaseResponse>>(purchases);
        }

        public async Task<PurchaseResponse?> GetPurchaseById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var purchase = await _dbContext.Purchases
                .Include(p => p.PurchaseItems)
                .Where(p => p.MembershipNumber == membershipNumber && p.Id == id)
                .FirstOrDefaultAsync();

            if (purchase is null)
            {
                return null;
            }
            return _mapper.Map<PurchaseResponse>(purchase);
        }

        public async Task<PurchaseResponse?> AddPurchase(PurchaseAddRequest purchaseAddRequest)
        {
            // Begin database transaction
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                decimal TotalAmount = 0;

                var purchase = CreatePurchase(purchaseAddRequest);
                await _dbContext.AddAsync(purchase);
                await _dbContext.SaveChangesAsync();

                List<PurchaseItemAddRequest>? purchaseItems = purchaseAddRequest.PurchaseItems;
                if (purchaseItems is null)
                {
                    return null;
                }

                List<PurchaseItem> purchaseItemEntities = new List<PurchaseItem>();

                List<Inventory> inventoryEntities = new List<Inventory>();

                foreach (var item in purchaseItems)
                {
                    PurchaseItem purchaseItem = new PurchaseItem()
                    {
                        PurchaseId = purchase.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        SalesUnitPrice = item.SalesUnitPrice,
                    };

                    TotalAmount += item.UnitPrice * item.Quantity;

                    purchaseItemEntities.Add(purchaseItem);

                    Inventory? inventory = await _dbContext.Inventories
                        .Where(i => i.MembershipNumber == purchase.MembershipNumber &&  i.ProductId == item.ProductId)
                        .FirstOrDefaultAsync();

                    if (inventory != null)
                    {
                        // Update existing inventory
                        inventory.Quantity += item.Quantity;
                        inventory.PurchaseUnitPrice = item.UnitPrice;
                        inventory.TotalPurchasePrice = inventory.Quantity * item.UnitPrice;
                        inventory.SalesUnitPrice = item.SalesUnitPrice;
                        inventory.TotalSalesPrice = inventory.Quantity * item.SalesUnitPrice;
                        inventory.ExpectedRevenue = inventory.TotalSalesPrice - inventory.TotalPurchasePrice;

                        inventoryEntities.Add(inventory);
                    }
                    else 
                    {
                        Inventory newInventory = new Inventory() 
                        {
                            ProductId= item.ProductId,
                            Quantity = item.Quantity,
                            PurchaseUnitPrice = item.UnitPrice,
                            SalesUnitPrice = item.SalesUnitPrice ,
                            TotalPurchasePrice = item.UnitPrice * item.Quantity,
                            TotalSalesPrice = item.SalesUnitPrice * item.Quantity,
                            ExpectedRevenue = (item.SalesUnitPrice * item.Quantity) - (item.UnitPrice * item.Quantity),
                            MembershipNumber = purchase.MembershipNumber,

                        };

                        inventoryEntities.Add(newInventory);
                    }
                }

                await _dbContext.AddRangeAsync(purchaseItemEntities);


                // Add or update inventory entities in the database
                _dbContext.Inventories.UpdateRange(inventoryEntities);

                purchase.TotalAmount = TotalAmount;

                await _dbContext.SaveChangesAsync();
                
                // Commit the transaction
                await transaction.CommitAsync();

                return _mapper.Map<PurchaseResponse>(purchase);

            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();

                _logger.LogError(ex, "An error occurred while adding the purchase.");
                // Optionally, log the error or rethrow it
                throw new ApplicationException("An error occurred while adding the purchase.", ex);
            }

        }

        private Purchase CreatePurchase(PurchaseAddRequest purchaseAddRequest) 
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Purchase purchase = new Purchase()
            {
                Guid = Guid.NewGuid(),
                DateTime = DateTime.Now,
                SuppplierId = purchaseAddRequest.SuppplierId,
                InvoiceNumber = purchaseAddRequest.InvoiceNumber,
                Notes = purchaseAddRequest.Notes,
                Status = purchaseAddRequest.Status,
                Paid = purchaseAddRequest.Paid,
                MembershipNumber = membershipNumber,
            };

            return purchase;
        }


        public async Task<PurchaseResponse?> DeletePurchase(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var purchase = await _dbContext.Purchases
                .Include(p => p.PurchaseItems)
                .Where(p => p.MembershipNumber == membershipNumber && p.Id == id)
                .FirstOrDefaultAsync();

            if (purchase is null) 
            {
                return null;
            }

            _dbContext.Purchases.Remove(purchase);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PurchaseResponse>(purchase);
        }

        public async Task<PaginatedResponse<PurchaseResponse>> GetPurchasesList(PaginationRequest request)
        {
            // Get Owner membership number 
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            // Fetch purchases list for owner
            var query = _dbContext.Purchases.Where(p => p.MembershipNumber == membershipNumber);

            // Count total records number 
            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(p => p.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            // Map items to response DTO
            var mappedItems = _mapper.Map<List<PurchaseResponse>>(items);

            // Return paginated response
            return new PaginatedResponse<PurchaseResponse>
            {
                Items = mappedItems,
                TotalCount = totalCount,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber
            };
        }
    }
}
