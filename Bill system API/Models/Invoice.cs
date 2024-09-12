namespace Bill_system_API.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int BillNumber { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public virtual Client? Client { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<InvoiceItem>? InvoiceItems { get; set; } = new List<InvoiceItem>();

        //  store the discount and paid amount
        public double PercentageDiscount { get; set; }
        public double ValueDiscount { get; set; }
        public double PaidUp { get; set; }

    }
}
