using StackUnderflow.Common.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackUnderflow.Models
{
    public class UserViewModel
    {
        public int id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0} darf maximal {1} Zeichen lang sein")]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "{0} darf maximal {1} Zeichen lang sein.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string email { get; set; }

        [StringLength(10, ErrorMessage = "{0} darf maximal {1} Zeichen lang sein.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string newpassword { get; set; }
        public string oldpassword { get; set; }

        public Boolean registered { get; set; }
        public Boolean isAdmin { get; set; }

        public UserViewModel() { }

        public UserViewModel(User obj)
        {
            this.id = obj.user_id;
            this.username = obj.user_name;
            this.email = obj.email;
            this.registered = obj.registered;
            this.isAdmin = obj.admin;
            this.oldpassword = obj.password;
        }
    }
}