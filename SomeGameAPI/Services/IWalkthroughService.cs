using System.Collections.Generic;
using SomeGameAPI.Entities;
using SomeGameAPI.Models;

namespace SomeGameAPI.Services
{
    public interface IWalktroughService
    {
        IEnumerable<Walkthrough> GetUserWalkthroughs(int userId);

        IEnumerable<LeaderboardPosition> GetLeaderBoard();

        void AddWalkthrough(Walkthrough walkthrough);
    }
}
