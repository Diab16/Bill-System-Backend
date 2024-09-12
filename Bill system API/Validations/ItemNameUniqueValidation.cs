using Bill_system_API.DTOs;
using Bill_system_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Bill_system_API.Validatioons
{
    public class ItemNameUniqueValidation:ValidationAttribute
    {
        private readonly ApplicationDbContext dbContext;


        public ItemNameUniqueValidation() 
        {
            
        }



        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;


            // the name that we gonna make check on 

            string? itemName = value?.ToString();

            // get itwm  from the requst 

            ItemDto items = (ItemDto)validationContext.ObjectInstance;

            // get the course from dbcontext and compare

            Item? itemfromdb = dbContext.Items.FirstOrDefault(c => c.Name == itemName);

            if (itemfromdb == null) return ValidationResult.Success;

            else return new ValidationResult($"{itemName} Title is Already Exist in The Category Selected  ");
        }
    }
}
