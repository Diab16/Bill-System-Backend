namespace Bill_system_API.DTOs
{
    public class FormDto
    {

        public List<ItemCompanyDto>? Companies { get; set; } = new List<ItemCompanyDto>();
        public List<itemTypeDto>? Types { get; set; } = new List<itemTypeDto>();
        public List<itemUnitDto>? Units { get; set; } = new List<itemUnitDto>();
    }
}
