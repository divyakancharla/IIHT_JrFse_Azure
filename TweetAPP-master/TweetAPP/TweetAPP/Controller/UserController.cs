using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using TweetAPP.Models;
using TweetAPP.Service;

namespace TweetAPP.Controller
{
    /// <summary>
    /// UserController.
    /// </summary>
    [Route("api/v1.0/tweets/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITweetCosmoDBService _cosmosDbService;
       
        /// <summary>
        /// create the object of usercontroller.
        /// </summary>
        /// <param name="service">service dependenc injection.</param>
        public UserController(ITweetCosmoDBService cosmoDbService)
        {
            _cosmosDbService = cosmoDbService ?? throw new ArgumentNullException(nameof(cosmoDbService));
        }

        /// <summary>
        /// user login
        /// </summary>
        /// <param name="userId">based on userId.</param>
        /// <param name="password">based on password</param>
        /// <returns>returns the status message.</returns>

        [Route("UserLogin/{username}/{password}")]
        [HttpGet]
        public Models.User UserLogin(string username, string password)
        {
            try
            {
               if(username!=null && password != null)
                {
                    var result = this._cosmosDbService.UserLogin(username, password);
                    if(result != null)
                    {
                        return result.Result;
                    }

                }
            }

            catch (TweetException ex)
            {
                throw new TweetException(BadRequest("error in userLogin") + ex.Message);
            }
            return null;


        }

        /// <summary>
        /// user registration.
        /// </summary>
        /// <param name="user">userRegiastration.</param>
        /// <returns>returns the status message of user register.</returns>
        [Route("UserRegister")]
        [HttpPost]
        public IActionResult UserRegister([FromBody] Models.User user)
        {
            try
            {
                if (user != null)
                {
                    user.Id = Guid.NewGuid().ToString();
                     _cosmosDbService.Register(user);
                    return Ok(" Registered ");
                }
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in user register" + ex.Message);
            }
            return Ok(new { status = "Failed" });


        }

        /// <summary>
        /// Updates the user password.
        /// </summary>
        /// <param name="username">based on userId.</param>
        /// <param name="newPassword">updates the new password.</param>
        /// <returns>returns the status message whether the password is updated or not.</returns>
        [Route("resetpassword/{username}/{oldpassword}/{newPassword}")]
        [HttpPut]
        public IActionResult PasswordUpdate(string username, string oldpassword, string newPassword)
        {
            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(newPassword))
                {

                    var result = this._cosmosDbService.UpdatePassword(username, oldpassword, newPassword);
                    return Ok(new
                    {
                        status = "Updated"
                    });;

                }
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in password update" + ex.Message);
            }

            return Ok(new { status = "Update Failed" });
        }

        /// <summary>
        /// ForgotPassword
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [Route("ForgotPassword/{emailId}/{newPassword}")]
        [HttpPut]
        public IActionResult ForgotPassword(string emailId, string newPassword)
        {
            try
            {
                if (!string.IsNullOrEmpty(emailId) && !string.IsNullOrEmpty(newPassword))
                {
                    var result = this._cosmosDbService.ForgotPassword(emailId, newPassword);
                    

                    return Ok(new { status = "Reset Success" });

                }
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in password update" + ex.Message);
            }

            return Ok(new { status = "Failed to Reset" });
        }
    }
}
