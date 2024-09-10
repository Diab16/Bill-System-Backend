namespace Bill_system_API.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int BillNumber { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Client Client { get; set; }
        public Employee Employee { get; set; }
        public ICollection<invoiceItem> InvoiceItems { get; set; } = new List<invoiceItem>();

    }
}
