using AutoMapper;
using eCommerce.Application.Carts.Dtos;
using eCommerce.Application.Carts.Dtos.Requests;
using eCommerce.Application.Carts.Dtos.Responses;
using eCommerce.Application.Customers.Dtos.Requests;
using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Application.Products.Dtos.Requests;
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
            CreateMap<Cart, CartResponseDto>();
            CreateMap<CartProduct, AddProductToCartRequest>();

            CreateMap<Purchase, CheckoutResultDto>()
                .ForMember(x => x.PurchaseId,
                           options => options.MapFrom(x => x.Id));

            CreateMap<CreditCard, CreateCreditCardRequest>();
        //    CreateMap<Customer, CustomerResponseDto>();
            CreateMap<Product, CreateProductRequestDto>();

            CreateMap<CustomerPurchaseHistoryReadModel, CustomerPurchaseHistoryDto>();
            //   CreateMap<DomainEventRecord, EventDto>();
        }
    }
}
