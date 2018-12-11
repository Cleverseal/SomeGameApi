using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SomeGameAPI.Entities;
using SomeGameAPI.Models;
using SomeGameAPI.Helpers;

namespace SomeGameAPI.Services
{
    public interface IUserService
    {
        User Authenticate(LoginModel model);

        User Signin(SigninModel model);

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

        public User Authenticate(LoginModel model)
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

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return this.context.Users.AsEnumerable<User>().Select(x => {
                x.Password = null;
                return x;
            });
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
    }
}