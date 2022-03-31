// <copyright file="User.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TweetAPP.Models
{
    using System;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets FirstName.
        /// </summary>
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(20, ErrorMessage = "Firstname cannot be longer than 20 characters.")]
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets LastName.
        /// </summary>
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets ContactNumber.
        /// </summary>
        [Required(ErrorMessage = "The mobileno is required")]
        [JsonProperty(PropertyName = "contactNumber")]
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or Sets Username.
        /// </summary>
        [Required(ErrorMessage = "The username is required")]
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets EmailId.
        /// </summary>
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [JsonProperty(PropertyName = "emailId")]
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or Sets Password.
        /// </summary>
        [Required(ErrorMessage = "The password is required")]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets ImageName.
        /// </summary>
        [Required(ErrorMessage = "The Image is required")]
        [JsonProperty(PropertyName = "image")]
        public string ImageName { get; set; }
    }
}
