namespace Bill_system_API.DTOs
{
    public class ClientDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public List<InvoiceDTO> Invoices { get; set; } = new List<InvoiceDTO>();
    }

}
