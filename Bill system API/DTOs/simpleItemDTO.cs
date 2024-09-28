using Bill_system_API.Validatioons;
using System.ComponentModel.DataAnnotations;

namespace Bill_system_API.DTOs
{
    public class simpleItemDTO
    {

        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public int ItemAvailableAmount { get; set; }
    }
}
