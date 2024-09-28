using Bill_system_API.DTOs;
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
            CompanyDTO? companyFromReq = validationContext.ObjectInstance as CompanyDTO;


            // get Course  from the requst 
            var db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            // get the course from dbcontext and compare

            if (companyFromReq.Id == 0)
            {
                Company? companyFromDb = db.Companies.FirstOrDefault(c => c.Name == companyName);

                if (companyFromDb == null) return ValidationResult.Success;
                else return new ValidationResult($"{companyName} Already Exists in Company Names , Enter Different Name!");
            }
            else
            {
                Company? companyFromDb = db.Companies.FirstOrDefault(c => c.Name == companyName && c.Id != companyFromReq.Id);

                if (companyFromDb == null) return ValidationResult.Success;
                else return new ValidationResult($"{companyName} Already Exists in Company Names , Enter Different Name!");
            }

            
        }
    }
}
