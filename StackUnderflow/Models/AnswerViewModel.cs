using StackUnderflow.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackUnderflow.Models
{
    public class AnswerViewModel
    {
        public int id { get; private set; }
        public string content { get; set; }
        public int rating { get; set; }
        public string author { get; set; }
        public DateTime created { get; set; }
        public QuestionViewModel question { get; set; }

        public AnswerViewModel(Answer obj)
        {
            this.id = obj.answer_id;
            this.content = obj.content;
            this.rating = obj.rating.Value;
            this.author = obj.User.user_name;
            this.created = obj.created;
            this.question = new QuestionViewModel(obj.Question);
        }
    }
}