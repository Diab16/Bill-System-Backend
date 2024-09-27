namespace Bill_system_API.DTOs
{
    public class InvoiceItemDTO
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public double SellingPrice { get; set; }
        public double Quantity { get; set; }
        public double TotalValue { get; set; }
    }
}
