// <copyright file="TweetAppController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TweetAPP.Controller
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using TweetAPP.Models;
    using TweetAPP.Service;

    /// <summary>
    /// TweetAppController.
    /// </summary>
    [Route("api/v1.0/tweets/")]
    [ApiController]
    public class TweetAppController : ControllerBase
    {
        private readonly ITweetCosmosService _cosmosDbService;

        /// <summary>
        /// Create the instance of the tweetController.
        /// </summary>
        /// <param name="service">service.</param>
        public TweetAppController(ITweetCosmosService cosmoDbService)
        {
            _cosmosDbService = cosmoDbService ?? throw new ArgumentNullException(nameof(cosmoDbService));
        }

        /// <summary>
        /// Adds the new tweet
        /// </summary>
        /// <param name="tweet">Tweet.</param>
        /// <returns>returns the status message.</returns>

        [Route("PostTweet")]
        [HttpPost]
        public IActionResult PostTweets([FromBody] Tweet tweet)
        {
            try
            {
                if (tweet != null)
                {
                    tweet.Id = Guid.NewGuid().ToString();
                    this._cosmosDbService.PostTweet(tweet);
                    return Ok(new { status = "Posted" });
                }

                return Ok(new { status = "Failed" });
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in post new tweet" + ex.Message);
            }
        }

        /// <summary>
        /// Views all user tweets.
        /// </summary>
        /// <param name="username">userId.</param>
        /// <returns>returns all the user tweets.</returns>
        [Route("UserTweets/{username}")]
        [HttpGet]
        public async Task<IActionResult> GetTweetsByUser(string username)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var tweets = await this._cosmosDbService.GetTweetsByUser(username);
                    return Ok(tweets);
                }
                return null;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in get  user tweets" + ex.Message);
            }


        }

        /// <summary>
        /// Views all user tweets.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>returns all the user tweets.</returns>
        [Route("AllUserTweets/{username}")]
        [HttpGet]
        public async Task<IActionResult> GetAllTweets(string username)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var tweets = await this._cosmosDbService.GetAllOtherTweets(username);
                    return Ok(tweets);
                }
                return null;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in get all tweets" + ex.Message);
            }


        }


    }
}