// <copyright file="ITweetCosmoDBService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TweetAPP.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TweetAPP.Models;

    /// <summary>
    /// Interface ITweetCosmoDBService.
    /// </summary>
    public interface ITweetCosmoDBService
    {
        /// <summary>
        /// Register.
        /// </summary>
        /// <param name="users">users.</param>
        /// <returns>response.</returns>
        Task<string> Register(Models.User users);

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="Username">emailId.</param>
        /// <param name="password">password.</param>
        /// <returns>response.</returns>
        Task<Models.User> UserLogin(string Username, string password);

        /// <summary>
        /// UpdatePassword.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="oldpassword">oldpassword.</param>
        /// <param name="newPassword">newPassword.</param>
        /// <returns>response.</returns>
        Task UpdatePassword(string userId, string oldpassword, string newPassword);

        /// <summary>
        /// ForgotPassword.
        /// </summary>
        /// <param name="userId">emailId.</param>
        /// <param name="password">password.</param>
        /// <returns>response.</returns>
        Task ForgotPassword(string emailId, string password);
    }
}
