using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Entities
{
    [Table("tbRozpisObjednavek")]
    public class OrdersDetail
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("vyrobekId")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Column("objednavkaId")]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Column("pocet_vyrobku")]
        public int ProductsCount { get; set; }
        [Column("celkova_cena")]
        public float TotalPrice { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public OrdersDetail(int id, int productId, Product product, int orderId, int productsCount, float totalPrice, Order order)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            OrderId = orderId;
            ProductsCount = productsCount;
            TotalPrice = totalPrice;
            Order = order;
        }

        public OrdersDetail()
        {
            Id = 0;
            ProductId = 0;
            Product = new Product();
            OrderId = 0;
            ProductsCount = 0;
            TotalPrice = 0;
            Order = new Order();
        }
    }
}
