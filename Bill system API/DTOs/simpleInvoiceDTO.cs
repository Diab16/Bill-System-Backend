namespace Bill_system_API.DTOs
{
    public class simpleInvoiceDTO
    {
        public int Id { get; set; }
        public int BillNumber { get; set; }
        public DateOnly Date { get; set; }
        public double PercentageDiscount { get; set; }
        public double BillTotal { get; set; }
        public double PaidUp { get; set; }
    }
}
