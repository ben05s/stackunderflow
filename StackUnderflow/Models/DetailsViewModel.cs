using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackUnderflow.Models
{
    public class DetailsViewModel
    {
        public UserViewModel currentUser { get; set; }
        public QuestionViewModel currentQuestion { get; set; }
        public IEnumerable<AnswerViewModel> answers { get; set; }

        public DetailsViewModel(UserViewModel currentUser, QuestionViewModel currentQuestion, IEnumerable<AnswerViewModel> answers)
        {
            this.currentUser = currentUser;
            this.currentQuestion = currentQuestion;
            this.answers = answers;
        }
    }
}