namespace Bill_system_API.DTOs
{
    public class InvoiceItemDTO
    {
        public int ItemId { get; set; }
        public double SellingPrice { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
    }
}
