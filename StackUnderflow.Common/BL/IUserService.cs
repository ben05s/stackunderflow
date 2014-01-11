﻿using StackUnderflow.Common.DAL;
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
        Boolean RegisterUser(int user_id);
        Boolean LoginUser(string username, string password);
        Boolean UpdatePassword(int user_id, string newpassword, string oldpassword);

        User GetUser(int id);
        User GetUser(string username);

        IQueryable<User> GetAllUsers(int page);
        IQueryable<User> SearchForUsers(string query, int page);
        IQueryable<Question> GetAllQuestions(int page);
        IQueryable<Question> SearchForQuestions(string query, int page);

        Boolean CreateQuestion(int user_id, string title, string content);
        Boolean CreateAnswer(int user_id, int question_id, string content);

        int RateUpAnswer(int answer_id);
        int RateDownAnswer(int answer_id);
    }
}
