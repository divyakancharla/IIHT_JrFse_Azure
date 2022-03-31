using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetAPP.Service
{
    public class TweetCosmosService : ITweetCosmosService
    {
        private Container _container;
        public TweetCosmosService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }


        public async Task<string> PostTweet(Models.Tweet tweet)
        {

            if (tweet != null)
            {
                await _container.CreateItemAsync(tweet, new PartitionKey(tweet.Id));
                return "Posted";
            }
            return "Failed";
        }

        public async Task<IEnumerable<Models.Tweet>> GetTweetsByUser(string username)
        {
            var tweetList = new List<Models.Tweet>();
            FeedIterator<Models.Tweet> setIterator = _container.GetItemLinqQueryable<Models.Tweet>()
                       .Where(b => b.Username == username)
                       .ToFeedIterator<Models.Tweet>();
            {
                while (setIterator.HasMoreResults)
                {
                    var response = await setIterator.ReadNextAsync();
                    tweetList.AddRange(response.ToList());
                }
                return tweetList;
            }
           
        }

        public async Task<IEnumerable<Models.Tweet>> GetAllOtherTweets(string username)
        {
            var tweetList = new List<Models.Tweet>();
            FeedIterator<Models.Tweet> setIterator = _container.GetItemLinqQueryable<Models.Tweet>()
                       .Where(b => b.Username != username)
                       .ToFeedIterator<Models.Tweet>();
            {
                while (setIterator.HasMoreResults)
                {
                    var response = await setIterator.ReadNextAsync();
                    tweetList.AddRange(response.ToList());
                }
                return tweetList;
            }
        }

    }
}
