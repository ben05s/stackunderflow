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
            var user = _dal.User.SingleOrDefault(i => i.user_id == id);
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

        public Boolean RegisterUser(string user_name)
        {
            var user = GetUser(user_name);
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
            if (user != null && user.password == password && user.registered == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean UpdatePassword(int user_id, string newpassword)
        {
            var user = GetUser(user_id); 
            if (user != null)
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

        public Boolean ForgotPassword(string user_name, string email, string newpassword)
        {
            var user = GetUser(user_name);
            if (user != null && user.email == email)
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

        public Question GetQuestion(int id)
        {
            return _dal.Question.Find(id);
        }

        public IQueryable<Question> SearchForQuestions(string query, int page)
        {
            return _dal.Question
                .Where(i => i.title.ToLower().Contains(query.ToLower()))
                .OrderBy(i => i.created)
                .Skip(page * 50)
                .Take(50);
        }

        public IQueryable<Answer> GetAllAnswers(int question_id, int page)
        {
            return _dal.Answer
                .Where(i => i.question_id == question_id)
                .OrderByDescending(i => i.rating)
                .Skip(page * 50)
                .Take(50);
        }

        public Boolean CreateQuestion(string username, string title, string content)
        {
            var question = new Question();
            question.title = title;
            question.content = content;
            question.user_id = GetUser(username).user_id;
            question.created = DateTime.Now;

            _dal.Question.Add(question);
            Save();
            return true;
        }

        public Boolean CreateAnswer(string username, int question_id, string content)
        {
            var answer = new Answer();
            answer.content = content;
            answer.question_id = question_id;
            answer.user_id = GetUser(username).user_id;
            answer.rating = 0;
            answer.created = DateTime.Now;

            _dal.Answer.Add(answer);
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

        public Boolean SaveUser(int user_id, string username, string email, Boolean registered, Boolean isAdmin)
        {
            var dbuser = GetUser(user_id);
            dbuser.user_name = username;
            dbuser.email = email;
            dbuser.registered = registered;
            dbuser.admin = isAdmin;
            
            Save();
            return true;
        }

        private void Save()
        {
            _dal.SaveChanges();
        }
    }
}
