using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace TweetAPP.Service
{
    public class TweetCosmoDbService : ITweetCosmoDBService
    {
        private Container _container;
        public TweetCosmoDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task<string> Register(Models.User user)
        {

            var result = this.ValidateEmailId(user.EmailId);
            if (result != null)
            {
                await _container.CreateItemAsync(user, new PartitionKey(user.Id));
                return "Registered";
            }
            return "Emaild Already taken";
        }

        private async Task<Models.User> ValidateEmailId(string emailId)
        {
            FeedIterator<Models.User> setIterator = _container.GetItemLinqQueryable<Models.User>()
                      .Where(b => b.EmailId == emailId)
                      .ToFeedIterator<Models.User>();
            {
                while (setIterator.HasMoreResults)
                {
                    foreach (var item in await setIterator.ReadNextAsync())
                    {
                        {
                            return item;
                        }
                    }
                }
            }
            return null;
        }

        public async Task<Models.User> UserLogin(string Username, string password)
        {
            FeedIterator<Models.User> setIterator = _container.GetItemLinqQueryable<Models.User>()
                      .Where(b => b.Username == Username && b.Password==password)
                      .ToFeedIterator<Models.User>();
            {
                while (setIterator.HasMoreResults)
                {
                    foreach (var item in await setIterator.ReadNextAsync())
                    {
                        {
                            return item;
                        }
                    }
                }
            }
            return null;
        }

        public async Task UpdatePassword(string userId, string oldpassword, string newPassword)
        {
            Task<Models.User> result = this.UserLogin(userId, oldpassword);
            if(result.Result != null)
            {
                result.Result.Password = newPassword;
                await _container.UpsertItemAsync(result.Result, new PartitionKey(result.Result.Id));
            }
            
        }

        public async Task ForgotPassword(string emailId, string password)
        {
            Task<Models.User> result = this.ValidateEmailId(emailId);
            if (result.Result != null)
            {
                result.Result.Password = password;
                await _container.UpsertItemAsync(result.Result, new PartitionKey(result.Result.Id));
            }

        }


    }
}
