using MVC_Web.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Models
{
    public class OrderViewModel
    {
        
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PSC { get; set; }
        public int DeliveryId { get; set; }
        public int PaymentId { get; set; }

        public virtual PaymentViewModel Payment { get; set; }
        public virtual DeliveryViewModel Delivery { get; set; }



        public OrderViewModel(string customerFirstName, string customerLastName, string city, string address, string pSC, int deliveryId, int paymentId, PaymentViewModel payment, DeliveryViewModel delivery)
        {
            CustomerFirstName = customerFirstName;
            CustomerLastName = customerLastName;
            City = city;
            Address = address;
            PSC = pSC;
            DeliveryId = deliveryId;
            PaymentId = paymentId;
            Payment = payment;
            Delivery = delivery;
        }

        public OrderViewModel()
        {
            CustomerFirstName = string.Empty;
            CustomerLastName = string.Empty;
            City = string.Empty;
            Address = string.Empty;
            PSC = string.Empty;
            DeliveryId = 0;
            PaymentId = 0;
            Payment = new PaymentViewModel();
            Delivery = new DeliveryViewModel();
        }
    }
}
