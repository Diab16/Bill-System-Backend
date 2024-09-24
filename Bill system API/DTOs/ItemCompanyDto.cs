namespace Bill_system_API.DTOs
{
    public class ItemCompanyDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual List<itemTypeDto>? Types { get; set; }

    }
}
