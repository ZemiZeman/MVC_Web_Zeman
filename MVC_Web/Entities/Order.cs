using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Entities
{
    [Table("tbObjednavky")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("datum_zpracovani")]
        public DateTime ProcessingDate { get; set; }
        [Column("jmeno")]
        public string CustomerFirstName { get; set; }
        [Column("prijmeni")]
        public string CustomerLastName { get; set; }

        [Column("telefon")]
        public string CustomerPhone { get; set; }
        [Column("email")]
        public string CustomerEmail { get; set; }
        [Column("mesto")]
        public string City { get; set; }
        [Column("adresa")]
        public string Address { get; set; }
        [Column("psc")]
        public string PSC {  get; set; }
        [Column("doruceni")]
        [ForeignKey("Delivery")]
        public int DeliveryId {  get; set; }
        [Column("platba")]
        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        [Column("stav_objednavky")]
        public string OrderState { get; set; }
        [Column("customerId")]
        public int? CustomerId { get; set; }

        [NotMapped]
        public virtual Payment Payment { get; set; }
        [NotMapped]
        public virtual Delivery Delivery { get; set; }

        public Order(int id, DateTime processingDate, string customerFirstName, string customerLastName, string city,string customerPhone,string customerEmail, string address, string pSC, int deliveryId, int paymentId, string orderState, Payment payment, Delivery delivery,int customerId)
        {
            Id = id;
            ProcessingDate = processingDate;
            CustomerFirstName = customerFirstName;
            CustomerLastName = customerLastName;
            City = city;
            CustomerPhone = customerPhone;
            CustomerEmail = customerEmail;
            Address = address;
            PSC = pSC;
            DeliveryId = deliveryId;
            PaymentId = paymentId;
            OrderState = orderState;
            Payment = payment;
            Delivery = delivery;
            CustomerId = customerId;
        }

        public Order()
        {
            Id = 0;
            ProcessingDate = DateTime.Now;
            CustomerFirstName = string.Empty;
            CustomerLastName = string.Empty;
            City = string.Empty;
            CustomerPhone = string.Empty;
            CustomerEmail = string.Empty;
            Address = string.Empty;
            PSC = string.Empty;
            PaymentId = 0;
            Payment = new Payment();
            DeliveryId = 0;
            Delivery = new Delivery();
            OrderState = "nová";
            CustomerId = null;
        }
    }
}
