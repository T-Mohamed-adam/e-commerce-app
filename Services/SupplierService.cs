using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Supplier;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public SupplierService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper)
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<SupplierResponse>> GetAllSuppliers()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();
            var suppliers = await _dbContext.Suppliers
                .Where(s => s.MembershipNumber == membershipNumber && s.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<SupplierResponse>>(suppliers);
        }

        public async Task<SupplierResponse?> GetSupplierById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();
            var supplier = await _dbContext.Suppliers
                .Where(s => s.MembershipNumber == membershipNumber && s.IsDeleted == false)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supplier is null) 
            {
                return null;
            }

            return _mapper.Map<SupplierResponse>(supplier);
        }

        public async Task<SupplierResponse?> AddSupplier(SupplierAddRequest supplierAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Supplier supplier = new Supplier() 
            {
                FirstName = supplierAddRequest.FirstName,
                LastName = supplierAddRequest.LastName,
                PhoneNumber = supplierAddRequest.PhoneNumber,
                Email = supplierAddRequest.Email,
                Address = supplierAddRequest.Address,
                CityId = supplierAddRequest.CityId,
                TIN = supplierAddRequest.TIN,
                CommercialRegister  = supplierAddRequest.CommercialRegister,
                MembershipNumber = membershipNumber,
            };

            await _dbContext.Suppliers.AddAsync(supplier);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SupplierResponse>(supplier);
        }

        public async Task<SupplierResponse?> UpdateSupplier(int id, SupplierUpdateRequest supplierUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();
            var supplier = await _dbContext.Suppliers
                .Where(s => s.MembershipNumber == membershipNumber && s.IsDeleted == false)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supplier is null)
            {
                return null;
            }

            supplier.FirstName = supplierUpdateRequest.FirstName;
            supplier.LastName = supplierUpdateRequest.LastName;
            supplier.PhoneNumber = supplierUpdateRequest.PhoneNumber;
            supplier.Email = supplierUpdateRequest.Email;
            supplier.Address = supplierUpdateRequest.Address;
            supplier.CityId = supplierUpdateRequest.CityId;
            supplier.TIN = supplierUpdateRequest.TIN;
            supplier.CommercialRegister = supplierUpdateRequest.CommercialRegister;
            supplier.IsActive = supplierUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SupplierResponse>(supplier);

        }

        public async Task<SupplierResponse?> DeleteSupplier(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();
            var supplier = await _dbContext.Suppliers
                .Where(s => s.MembershipNumber == membershipNumber && s.IsDeleted == false)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supplier is null)
            {
                return null;
            }

            _dbContext.Suppliers.Remove(supplier);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SupplierResponse>(supplier);

        }
    }
}
