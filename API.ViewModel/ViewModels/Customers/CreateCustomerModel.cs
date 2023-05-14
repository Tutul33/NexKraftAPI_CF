using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ViewModel.ViewModels.Customers
{
    public class CreateCustomerModel
    {
        [Required]
        [MinLength(1)]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Country { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
    }
    public class CustomerModel: CreateCustomerModel
    {
        public int? CustomerID { get; set; }
        public int? RoleId { get; set; }
    }
}
