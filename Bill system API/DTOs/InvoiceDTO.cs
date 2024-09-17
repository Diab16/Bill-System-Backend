namespace Bill_system_API.DTOs
{
    public class InvoiceDTO
    {
            public int Id { get; set; }
            public int BillNumber { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public int ClientId { get; set; }
            public int EmployeeId { get; set; }
            public double PercentageDiscount { get; set; }
            public double ValueDiscount { get; set; }
            public double PaidUp { get; set; }
            public List<InvoiceItemDTO> InvoiceItems { get; set; } = new List<InvoiceItemDTO>();
    }
}
