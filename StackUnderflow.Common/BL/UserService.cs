using StackUnderflow.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackUnderflow.Common.BL
{
    public class UserService : IUserService
    {
        private DAL.stackunderflowEntities _dal = new DAL.stackunderflowEntities();
        public UserService() { }

        public User GetUser(int id)
        {
            var user = _dal.User.Find(id);
            return user;
        }

        public User GetUser(string username)
        {
            return _dal.User.SingleOrDefault(i => i.user_name == username);
        }

        public Boolean CreateUser(string username, string password, string email)
        {
            if (GetUser(username) != null)
            {
                return false;
            }
            else
            {
                var user = new User();
                user.user_name = username;
                user.password = password;
                user.email = email;
                user.registered = false;
                user.admin = false;

                _dal.User.Add(user);
                Save();
                return true;
            }
        }

        public Boolean RegisterUser(int user_id)
        {
            var user = GetUser(user_id);
            if (user != null)
            {
                user.registered = true;
                Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean LoginUser(string username, string password)
        {
            var user = GetUser(username);
            if (user != null && user.password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean UpdatePassword(int user_id, string newpassword, string oldpassword)
        {
            var user = GetUser(user_id); 
            if (user != null && user.password == oldpassword && oldpassword != newpassword)
            {
                user.password = newpassword;
                Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<User> GetAllUsers(int page)
        {
            return _dal.User
                .OrderBy(i => i.user_name)
                .Skip(page * 50)
                .Take(50);
        }

        public IQueryable<User> SearchForUsers(string query, int page)
        {
            return _dal.User
                .Where(i => i.user_name.ToLower().Contains(query.ToLower()))
                .OrderBy(i => i.user_name)
                .Skip(page * 50)
                .Take(50);
        }

        public IQueryable<Question> GetAllQuestions(int page)
        {
            return _dal.Question
                .OrderBy(i => i.created)
                .Skip(page * 50)
                .Take(50);
        }

        public IQueryable<Question> SearchForQuestions(string query, int page)
        {
            return _dal.Question
                .Where(i => i.title.ToLower().Contains(query.ToLower()))
                .OrderBy(i => i.created)
                .Skip(page * 50)
                .Take(50);
        }

        public Boolean CreateQuestion(int user_id, string title, string content)
        {
            var question = new Question();
            question.title = title;
            question.content = content;
            question.user_id = user_id;
            Save();
            return true;
        }

        public Boolean CreateAnswer(int user_id, int question_id, string content)
        {
            var answer = new Answer();
            answer.content = content;
            answer.question_id = question_id;
            answer.user_id = user_id;
            answer.rating = 0;
            Save();
            return true;
        }

        public int RateUpAnswer(int answer_id)
        {
            var answer = _dal.Answer.Find(answer_id);
            answer.rating++;
            Save();
            return answer.rating.Value;
        }

        public int RateDownAnswer(int answer_id)
        {
            var answer = _dal.Answer.Find(answer_id);
            answer.rating--;
            Save();
            return answer.rating.Value;
        }

        private void Save()
        {
            _dal.SaveChanges();
        }
    }
}
