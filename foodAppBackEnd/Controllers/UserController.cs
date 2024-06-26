﻿using BAL.IBAL;
using foodAppBackEnd.GenerateJWTToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ResponseModel;
using Models.ViewModels;

namespace foodAppBackEnd.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private IUserBAL _user;
        private IConfiguration _configuration;
        public UserController(IUserBAL bAL,IConfiguration configuration)
        {
            this._user = bAL;
            this._configuration = configuration;
        }
        [HttpPost("UserRegister")]
        public async Task<IActionResult> post([FromBody] User user)
        {
            Response response = new Response();
            try
            {
                bool isValidate = true;
                User u = new User();
                if(string.IsNullOrEmpty(u.userName))
                {
                    response.success = false;
                    response.message = "User Name is required";
                    isValidate = false;
                }
                else if (string.IsNullOrEmpty(u.emailId))
                {
                    response.success = false;
                    response.message = "Email is required";
                    isValidate = false;
                }
                else if (string.IsNullOrEmpty(u.firstName))
                {
                    response.success = false;
                    response.message = "first Name is required";
                    isValidate = false;
                }
                else if (string.IsNullOrEmpty(u.password))
                {
                    response.success = false;
                    response.message = "password is required";
                    isValidate = false;
                }
                else if(string.IsNullOrEmpty(u.roleId))
                {
                    response.success = false;
                    response.message = "role is required";
                    isValidate = false;
                }
                if (isValidate)
                {
                    bool emailValidation = await _user.EmailValidation(u.emailId);
                    if(emailValidation) 
                    {
                        response.success = false;
                        response.message = $"this {u.emailId} is exists";
                    }
                    else
                    {
                        u = await _user.UserRegister(user);
                        if (u != null)
                        {
                            response.success = true;
                            response.message = "User Save successfully";
                            response.objectResponse = u;
                        }
                        else
                        {
                            response.success = false;
                            response.message = "something went wrong";
                        }
                    }
                }
            }
            catch(Exception e)
            {
                response.success = false;
                response.message = "something went wrong";
                response.objectResponse = e;
            }
            
            return Ok(response);
        }
        [HttpPost("UserLogin")]
        public async Task<IActionResult> post([FromBody] LoginUser user)
        {
            Response r = new Response();
            try 
            {
                bool isValidate = true;
                if(string.IsNullOrEmpty(user.email))
                {
                    r.success = false;
                    r.message = "Please enter Email";
                    isValidate = false;
                }
                else if(string.IsNullOrEmpty(user.password))
                {
                    r.success = false;
                    r.message = "Please enter Password";
                    isValidate = false;
                }
                if (isValidate)
                {
                    User u = await _user.UserLogin(user.email, user.password);
                    if (u != null && u.id != Guid.Empty)
                    {
                        r.success = true;
                        r.message = "User login successfully";
                        r.objectResponse = u;
                        tokenRequest request = new tokenRequest()
                        {
                            email = u.emailId,
                            id = u.id.ToString(),
                            Role = u.roleId
                        };
                        tokenGenerate tg = new tokenGenerate(_configuration);
                        r.token = tg.generateToken(request);
                    }
                    else
                    {
                        r.success = true;
                        r.message = "user email and password is incorrect";
                    }
                }
            }
            catch(Exception e)
            {
                r.success = false;
                r.message = "Something Went wron";
                r.objectResponse = e;
            }
            return Ok(r);
        }
    }
}
