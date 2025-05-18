namespace ServiceAbstraction.IService
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IBasketService  BasketService{ get; }
        IAuthenticationService  AuthenticationService{ get; }
        IOrderService OrderService { get; }
        IPaymentService PaymentService{ get; }
    }
}
