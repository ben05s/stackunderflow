using StackUnderflow.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackUnderflow.Models
{
    public class QuestionViewModel
    {
        public int id { get; private set; }
        public string title { get; set; }
        public string content { get; set; }
        public string author { get; set; }
        public DateTime created { get; set; }
        public ICollection<Answer> answers { get; set; }

        public QuestionViewModel(Question obj)
        {
            this.id = obj.question_id;
            this.title = obj.title;
            this.content = obj.content;
            this.author = obj.User.user_name;
            this.created = obj.created;
            this.answers = obj.Answer;
        }

        public Boolean hasAnswers()
        {
            return this.answers.Count > 0;
        }
    }
}