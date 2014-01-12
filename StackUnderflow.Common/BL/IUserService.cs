using StackUnderflow.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackUnderflow.Common.BL
{
    public interface IUserService
    {
        Boolean CreateUser(string username, string password, string email);
        Boolean RegisterUser(string user_name);
        Boolean LoginUser(string username, string password);
        Boolean UpdatePassword(int user_id, string newpassword);
        Boolean ForgotPassword(string user_name, string email, string newpassword);

        User GetUser(int id);
        User GetUser(string username);

        IQueryable<User> GetAllUsers(int page);
        IQueryable<User> SearchForUsers(string query, int page);
        IQueryable<Question> GetAllQuestions(int page);
        Question GetQuestion(int id);
        IQueryable<Question> SearchForQuestions(string query, int page);
        IQueryable<Answer> GetAllAnswers(int question_id, int page);

        Boolean CreateQuestion(string username, string title, string content);
        Boolean CreateAnswer(string username, int question_id, string content);

        int RateUpAnswer(int answer_id);
        int RateDownAnswer(int answer_id);

        Boolean SaveUser(int user_id, string username, string email, Boolean registered, Boolean isAdmin);
    }
}
