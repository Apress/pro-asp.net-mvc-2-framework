using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Services
{
    public class FakeOrderSubmitter : IOrderSubmitter
    {
        public void SubmitOrder(Cart cart, ShippingDetails shippingDetails)
        {
            // Do nothing
        }
    }
}