using Bill_system_API.Validation;

namespace Bill_system_API.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        [UniqueCompany]
        public string Name { get; set; }
        public string? Notes { get; set; }
    }
}
