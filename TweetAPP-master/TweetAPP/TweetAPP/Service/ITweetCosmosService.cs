using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TweetAPP.Service
{
    public interface ITweetCosmosService
    {
        Task<string> PostTweet(Models.Tweet tweet);
        Task<IEnumerable<Models.Tweet>> GetTweetsByUser(string username);
        Task<IEnumerable<Models.Tweet>> GetAllOtherTweets(string username);

    }
}
