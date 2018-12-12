using System.Collections.Generic;
using SomeGameAPI.Entities;
using SomeGameAPI.Models;

namespace SomeGameAPI.Services
{
    public interface IUserService
    {
        User Login(LoginModel model);

        User Signin(SigninModel model);

        bool UpdateUser(User user);

        bool DeleteUser(int id);

        IEnumerable<User> GetAll();
    }
}
