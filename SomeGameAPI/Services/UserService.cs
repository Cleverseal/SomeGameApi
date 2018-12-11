using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SomeGameAPI.Entities;
using SomeGameAPI.Helpers;
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

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private readonly DataContext context;

        private readonly AppSettings appSettings;

        public UserService(DataContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
        }

        public User Login(LoginModel model)
        {
            var user = this.context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            user.Token = BearerTokenGenerator.GetToken(this.appSettings.Secret, user.Username);

            // remove password before returning
            user.Password = null;

            return user;
        }
        
        public User Signin(SigninModel model)
        {
            if (this.context.Users.Any(x => x.Username == model.Username)) return null;
            var user = new User()
            {
                Id = this.context.Users.Select(x => x.Id).Max() + 1,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Username = model.Username
            };

            user.Token = BearerTokenGenerator.GetToken(this.appSettings.Secret, user.Username);
            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public bool UpdateUser(User user)
        {
            var exist = this.context.Users.FirstOrDefault(x => x.Id == user.Id);
            if (exist != null)
            {
                exist.Username = user.Username;
                exist.Password = user.Password;
                exist.FirstName = user.FirstName;
                exist.LastName = user.LastName;
                this.context.Users.Update(exist);
                this.context.SaveChanges();
                return true;
            }

            return false;
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return this.context.Users.AsEnumerable<User>().Select(x => {
                x.Password = null;
                return x;
            });
        }

        public bool DeleteUser(int id)
        {
            var user = this.context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                this.context.Users.Remove(user);
                this.context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}