using StackUnderflow.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackUnderflow.Models
{
    public class UserViewModel
    {
        public int id { get; private set; }
        public string username { get; set; }
        public string email { get; set; }
        public string registered { get; set; }
        public Boolean isAdmin { get; set; }

        public UserViewModel() { }

        public UserViewModel(User obj)
        {
            this.id = obj.user_id;
            this.username = obj.user_name;
            this.email = obj.email;
            if(obj.registered)
            {
                this.registered = "Y";
            }
            else
            {
                this.registered = "N";
            }
            this.isAdmin = obj.admin;
        }
    }
}