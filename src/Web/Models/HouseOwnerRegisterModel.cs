using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TenancyContract.Models
{
    public class HouseOwnerRegisterModel
    {
        [Required(ErrorMessage = "EmailRequiredError")]
        [EmailAddress(ErrorMessage = "EmailCheckError")]
        public string Email { get; set; }
        [Required(ErrorMessage = "NameError")] //  [a-zA-Z ]*$
        [RegularExpression(("[a-zA-Z ]*$"), ErrorMessage = "NameValError")]
        public string Name { get; set; }
        [Required(ErrorMessage = "MobileError")]
        [RegularExpression(("[0-9]*"), ErrorMessage = "PhnValError")]
        [StringLength(10, ErrorMessage = "PhnLengthError", MinimumLength = 10)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "NidError")]
        [RegularExpression(("[0-9]*"), ErrorMessage = "NidValError")]
        [StringLength(10, ErrorMessage = "NidLengthError", MinimumLength = 10)]

        public string NID { get; set; }

        [Required(ErrorMessage = "PassRequired")]
        [DataType(DataType.Password)]//Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "PassValMessage")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "PassMatchError")]

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
