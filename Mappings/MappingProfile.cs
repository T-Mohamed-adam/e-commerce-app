using AutoMapper;
using TagerProject.Models.Dtos.Brand;
using TagerProject.Models.Dtos.Category;
using TagerProject.Models.Dtos.City;
using TagerProject.Models.Dtos.Coupon;
using TagerProject.Models.Dtos.Customer;
using TagerProject.Models.Dtos.Discount;
using TagerProject.Models.Dtos.Employee;
using TagerProject.Models.Dtos.Expense;
using TagerProject.Models.Dtos.ExpenseItem;
using TagerProject.Models.Dtos.Owner;
using TagerProject.Models.Dtos.Package;
using TagerProject.Models.Dtos.Product;
using TagerProject.Models.Dtos.Subscription;
using TagerProject.Models.Dtos.Supplier;
using TagerProject.Models.Dtos.Tax;
using TagerProject.Models.Dtos.Unit;
using TagerProject.Models.Entities;

namespace TagerProject.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // City mapping
            CreateMap<CityAddRequest, City>();
            CreateMap<CityUpdateRequest, City>();
            CreateMap<City, CityResponse>();

            // Package mapping
            CreateMap<PackageAddRequest, Package>();
            CreateMap<PackageUpdateRequest, Package>();
            CreateMap<Package, PackageResponse>();

            // Owner mapping
            CreateMap<OwnerAddRequest, Owner>();
            CreateMap<OwnerUpdateRequest, Owner>();
            CreateMap<Owner, OwnerResponse>();

            // Subscription mapping
            CreateMap<Subscription, SubscriptionResponse>();


            // Employee mapping
            CreateMap<EmployeeAddRequest, Employee>();
            CreateMap<EmployeeUpdateRequest, Employee>();
            CreateMap<Employee, EmployeeResponse>();

            // Customer mapping
            CreateMap<CustomerAddRequest, Customer>();
            CreateMap<CustomerUpdateRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();

            // Supplier mapping
            CreateMap<SupplierAddRequest, Supplier>();
            CreateMap<SupplierUpdateRequest, Supplier>();
            CreateMap<Supplier, SupplierResponse>();


            // Brand mapping
            CreateMap<BrandAddRequest, Brand>();
            CreateMap<BrandUpdateRequest, Brand>();
            CreateMap<Brand, BrandResponse>();

            // Category mapping
            CreateMap<CategoryAddRequest, Category>();
            CreateMap<CategoryUpdateRequest, Category>();
            CreateMap<Category, CategoryResponse>();

            // Tax mapping
            CreateMap<TaxAddRequest, Tax>();
            CreateMap<TaxUpdateRequest, Tax>();
            CreateMap<Tax, TaxResponse>();

            // Unit mapping
            CreateMap<UnitAddRequest, Unit>();
            CreateMap<UnitUpdateRequest, Unit>();
            CreateMap<Unit, UnitResponse>();

            // Product mapping
            CreateMap<ProductAddRequest, Product>();
            CreateMap<ProductUpdateRequest, Product>();
            CreateMap<Product, ProductResponse>();


            // Expense item mapping
            CreateMap<ExpenseItemAddRequest, ExpenseItem>();
            CreateMap<ExpenseItemUpdateRequest, ExpenseItem>();
            CreateMap<ExpenseItem, ExpenseItemResponse>();

            // Expense mapping
            CreateMap<ExpenseAddRequest, Expense>();
            CreateMap<ExpenseUpdateRequest, Expense>();
            CreateMap<Expense, ExpenseResponse>();


            // Discount mapping
            CreateMap<DiscountAddRequest, Discount>();
            CreateMap<DiscountUpdateRequest, Discount>();
            CreateMap<Discount, DiscountResponse>();


            // Coupon mapping
            CreateMap<CouponAddRequest, Coupon>();
            CreateMap<CouponUpdateRequest, Coupon>();
            CreateMap<Coupon, CouponResponse>();
        }
    }
}
