using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackUnderflow.Models
{
    public class HomeViewModel
    {
        public IEnumerable<UserViewModel> users { get; set; }
        public IEnumerable<QuestionViewModel> questions { get; set; }
        public UserViewModel currentUser { get; set; }

        public HomeViewModel(UserViewModel currentUser, IEnumerable<UserViewModel> users, IEnumerable<QuestionViewModel> questions)
        {
            this.users = users;
            this.questions = questions;
            this.currentUser = currentUser;
        }
    }
}