using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Services
{
    public interface IOrderSubmitter
    {
        void SubmitOrder(Cart cart, ShippingDetails shippingDetails);
    }
}