namespace Bill_system_API.DTOs
{
    public class FormDto
    {

        public List<CompanyDTO>? Companies { get; set; } = new List<CompanyDTO>();
        public List<CompanyDTO>? Types { get; set; } = new List<CompanyDTO>();
        public List<UnitDto>? Units { get; set; } = new List<UnitDto>();
    }
}
