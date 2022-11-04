using AutoMapper;
using eCommerce.Application.Carts;
using eCommerce.Application.Customers;
using eCommerce.Application.Products.Dtos;
using eCommerce.Domain.Carts;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Purchases;

namespace eCommerce.Application
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<Cart, CartDto>();
            CreateMap<CartProduct, CartProductDto>();

            CreateMap<Purchase, CheckOutResultDto>()
                .ForMember(x => x.PurchaseId,
                           options => options.MapFrom(x => x.Id));

          CreateMap<CreditCard, CreditCardDto>();
          CreateMap<Customer, CustomerDto>();

          CreateMap<Product, ProductDto>()
                .ForMember(x => x.CategoryId,
                           options => options.MapFrom(x => x.Category.Id))
                 .ForMember(x => x.CategoryName,
                           options => options.MapFrom(x => x.Category.Name));

            CreateMap<Category, Category>();
            CreateMap<CustomerPurchaseHistoryReadModel, CustomerPurchaseHistoryDto>();
       //   CreateMap<DomainEventRecord, EventDto>();
        } 
    }
}
