namespace MVC_Web.Models.Order
{
    public class MyOrderViewModel
    {
        public int Id { get; set; }
        public DateTime ProcessingDate { get; set; }
        public string OrderState { get; set; }
        public float TotalPrice { get; set; }

        public virtual PaymentViewModel Payment { get; set; }
        public virtual DeliveryViewModel Delivery { get; set; }

        public MyOrderViewModel(int id,DateTime processingDate, string orderState, float totalPrice, PaymentViewModel payment, DeliveryViewModel delivery)
        {
            Id = id;
            ProcessingDate = processingDate;
            OrderState = orderState;
            TotalPrice = totalPrice;
            Payment = payment;
            Delivery = delivery;
        }

        public MyOrderViewModel()
        {
            Id =0;
            ProcessingDate = DateTime.Now;
            OrderState = string.Empty;
            TotalPrice = 0;
            Payment = new PaymentViewModel();
            Delivery = new DeliveryViewModel();
        }
    }
}
