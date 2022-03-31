// <copyright file="Tweet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TweetAPP.Models
{
    /// <summary>
    /// Tweet.
    /// </summary>
    public class Tweet
    {
        [JsonProperty(PropertyName = "id")]
        /// <summary>
        /// Gets or Sets Id.
        /// </summary>
        public string Id { get; set; }

        [JsonProperty(PropertyName = "userId")]
        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "username")]
        /// <summary>
        /// Gets or Sets Username.
        /// </summary>
        public string Username { get; set; }

        [Required(ErrorMessage = "Tweet is required")]
        [StringLength(144, ErrorMessage = "Tweet cannot be longer than 20 characters.")]
        [JsonProperty(PropertyName = "tweets")]
        /// <summary>
        /// Gets or Sets Tweets.
        /// </summary>
        public string Tweets { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        /// <summary>
        /// Gets or Sets FirstName.
        /// </summary>
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        /// <summary>
        /// Gets or Sets LastName.
        /// </summary>
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "tweetDate")]
        /// <summary>
        /// Gets or Sets TweetDate.
        /// </summary>
        public DateTime TweetDate { get; set; }

        [JsonProperty(PropertyName = "likes")]
        public int Likes { get; set; }
    }
}
