using AutoMapper;
using eCommerce.Domain.Carts;
using eCommerce.Domain.Carts.Specifications;
using eCommerce.Domain.Common;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Purchases;
using eCommerce.Domain.Services;
using System;

namespace eCommerce.Application.Carts
{
    public class CartService : ICartService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TaxService _taxDomainService;
        private readonly CheckoutService _checkoutDomainService;
        private readonly IMapper _mapper;

        public CartService(IRepository<Customer> customerRepository,
            IRepository<Product> productRepository, IRepository<Cart> cartRepository,
            IUnitOfWork unitOfWork, TaxService taxDomainService, CheckoutService checkoutDomainService, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _taxDomainService = taxDomainService ?? throw new ArgumentNullException(nameof(taxDomainService));
            _checkoutDomainService = checkoutDomainService ?? throw new ArgumentNullException(nameof(checkoutDomainService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public CartDto Add(Guid customerId, CartProductDto productDto)
        {
            Customer customer = _customerRepository.FindById(customerId);

            if (customer == null) throw new Exception(string.Format("Customer was not found with this Id: {0}", customerId));

            Cart cart = _cartRepository.FindOne(new CustomerCartSpec(customerId));

            if (cart == null)
            {
                cart = Cart.Create(customer);
                _cartRepository.Add(cart);
            }

            Product product = _productRepository.FindById(productDto.ProductId);
            ValidateProduct(product.Id, product);

            //Double Dispatch Pattern
            cart.Add(CartProduct.Create(customer, cart, product, productDto.Quantity, _taxDomainService));

            CartDto cartDto = _mapper.Map<Cart, CartDto>(cart);
            _unitOfWork.Commit();
            return cartDto;
        }

        public CartDto Remove(Guid customerId, Guid productId)
        {
            Cart cart = _cartRepository.FindOne(new CustomerCartSpec(customerId));

            ValidateCart(customerId, cart);

            Product product = _productRepository.FindById(productId);
            ValidateProduct(productId, product);

            cart.Remove(product);

            CartDto cartDto = _mapper.Map<Cart, CartDto>(cart);

            _unitOfWork.Commit();

            return cartDto;
        }

        public CartDto Get(Guid customerId)
        {
            Cart cart = _cartRepository.FindOne(new CustomerCartSpec(customerId));
            ValidateCart(customerId, cart);

            return _mapper.Map<Cart, CartDto>(cart);
        }

        public CheckOutResultDto CheckOut(Guid customerId)
        {
            CheckOutResultDto checkOutResultDto = new CheckOutResultDto();

            Cart cart = _cartRepository.FindOne(new CustomerCartSpec(customerId));

            ValidateCart(customerId, cart);

            Customer customer = _customerRepository.FindById(cart.CustomerId);

            CheckOutIssue? checkOutIssue = _checkoutDomainService.CanCheckOut(customer, cart);

            if (!checkOutIssue.HasValue)
            {
                Purchase purchase = _checkoutDomainService.Checkout(customer, cart);
                checkOutResultDto = _mapper.Map<Purchase, CheckOutResultDto>(purchase);
                _unitOfWork.Commit();
            }
            else
            {
                checkOutResultDto.CheckOutIssue = checkOutIssue;
            }

            return checkOutResultDto;
        }

        private void ValidateCart(Guid customerId, Cart cart)
        {
            if (cart == null) throw new Exception(string.Format("Customer was not found with this Id: {0}", customerId));
        }

        private void ValidateProduct(Guid productId, Product product)
        {
            if (product == null) throw new Exception(string.Format("Product was not found with this Id: {0}", productId));
        }
    }
}
