namespace Bill_system_API.DTOs
{
    public class FormDto
    {

        public List<ItemCompanyDto>? Companies { get; set; } = new List<ItemCompanyDto>();
        public List<itemUnitDto>? Units { get; set; } = new List<itemUnitDto>();
    }
}
