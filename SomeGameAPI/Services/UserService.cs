using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SomeGameAPI.Entities;
using SomeGameAPI.Helpers;
using SomeGameAPI.Models;

namespace SomeGameAPI.Services
{
    public class UserService : IUserService
    {
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
                Id = this.context.Users.Max(x => x.Id) + 1,
                FirstName = model.Nickname,
                SecondName = model.Nickname,
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
                exist.SecondName = user.SecondName;
                exist.DepId = user.DepId;
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