using Bill_system_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Bill_system_API.Validation
{
    public class UniqueCompanyAttribute:ValidationAttribute
    {
        public UniqueCompanyAttribute() { }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            // the name that we gonna make check on 

            string? companyName = value?.ToString();


            // get Course  from the requst 
            var db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));


            // get the course from dbcontext and compare

            //  InitialCompanyContext context = new InitialCompanyContext();
            Company? companyFromDb = db.Companies.FirstOrDefault(c => c.Name == companyName);

            if (companyFromDb == null) return ValidationResult.Success;
            else return new ValidationResult($"{companyName} Already Exists in Company Names , Enter Different Name!");
        }
    }
}
