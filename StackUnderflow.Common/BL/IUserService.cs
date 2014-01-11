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
        /*USER methods*/
        Boolean CreateUser(string username, string password, string email);
        Boolean RegisterUser(string username);
        Boolean LoginUser(string username, string password);
        Boolean UpdatePassword(string username, string newpassword, string oldpassword);

        IQueryable<User> GetAllUsers();
        IQueryable<User> SearchForUsers(string query);
        User GetUser(int user_id);

        /*QUESTION & ANSWER methods*/
        IQueryable<Question> GetAllQuestions();
        Boolean CreateQuestion(string username, string title, string content);
        IQueryable<Question> SearchForQuestions(string query);


        void Save();
    }
}
