using System;
using API.Models;
using API.ViewModels;
using System.Collections.Generic;

namespace API.Repositories.Interface
{
    public interface IAccountRepository
    {
        ResponseLogin Login(Login login);

        int Register(Register register);

        int ChangePassword(string email, string oldPassword, string newPassword);

        int ForgotPassword(string email);
    }
}
