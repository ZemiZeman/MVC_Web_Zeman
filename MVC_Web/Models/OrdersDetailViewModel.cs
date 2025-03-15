using MVC_Web.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Models
{
    public class OrdersDetailViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int ProductsCount { get; set; }
        public float TotalPrice => ProductsCount * Product.Price;
        //public virtual OrderViewModel Order { get; set; }
        public virtual ProductViewModel Product { get; set; }

        public OrdersDetailViewModel(int id, int productId, ProductViewModel product, int orderId, int productsCount/*, OrderViewModel order*/)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            OrderId = orderId;
            ProductsCount = productsCount;
            //Order = order;
        }

        public OrdersDetailViewModel()
        {
            Id = 0;
            ProductId = 0;
            Product = new ProductViewModel();
            OrderId = 0;
            ProductsCount = 0;
            //Order = new OrderViewModel();
        }
    }
}
