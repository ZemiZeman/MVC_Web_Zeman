namespace MVC_Web.Models.Order
{
    public class ChangeOrderStateViewModel
    {
        public int Id { get; set; }
        public DateTime ProcessingDate { get; set; }
        public string OrderState { get; set; }
        public float TotalPrice { get; set; }

        public ChangeOrderStateViewModel(int id, DateTime processingDate, string orderState, float totalPrice)
        {
            Id = id;
            ProcessingDate = processingDate;
            OrderState = orderState;
            TotalPrice = totalPrice;
        }

        public ChangeOrderStateViewModel()
        {
            Id = 0;
            ProcessingDate = DateTime.Now;
            OrderState = string.Empty;
            TotalPrice = 0;
        }
    }
}
