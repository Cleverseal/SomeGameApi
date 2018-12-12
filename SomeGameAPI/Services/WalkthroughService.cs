using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SomeGameAPI.Entities;
using SomeGameAPI.Helpers;
using SomeGameAPI.Models;

namespace SomeGameAPI.Services
{
    public interface IWalktroughService
    {
        IEnumerable<Walkthrough> GetUserWalkthroughs(int userId);

        IEnumerable<LeaderboardPosition> GetLeaderBoard();

        void AddWalkthrough(Walkthrough walkthrough);
    }

    public class WalkthroughService : IWalktroughService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private readonly DataContext context;

        private readonly AppSettings appSettings;

        public WalkthroughService(DataContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
        }

        public void AddWalkthrough(Walkthrough walkthrough)
        {
            walkthrough.Id = this.context.Walkthroughs.Max(x => x.Id) + 1;
            this.context.Walkthroughs.Add(walkthrough);
            this.context.SaveChanges();
        }

        public IEnumerable<LeaderboardPosition> GetLeaderBoard()
        {
            return this.context.Users
                .Select(user => new LeaderboardPosition
                {
                    Name = user.Username,
                    Score = this.context.Walkthroughs.Where(walkth => walkth.UserId == user.Id).Max(walkth => walkth.Score)
                })
                .OrderByDescending(x => x.Score).AsEnumerable();
        }

        public IEnumerable<Walkthrough> GetUserWalkthroughs(int userId)
        {
            return this.context.Walkthroughs.Where(x => x.UserId == userId).AsEnumerable();
        }
    }
}
