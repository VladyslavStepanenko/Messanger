using Messanger.Api.Models;
using Messanger.Domain;
using Messanger.Infra.DataContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Messanger.Api.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private MessangerDbContext dbContext;

        public AccountController()
        {
            dbContext = new MessangerDbContext();
        }

        [Route("{id:int}")]
        [Authorize]
        public HttpResponseMessage GetUser(int id)
        {
            var user = dbContext.Users.Find(id);
            HttpResponseMessage response = new HttpResponseMessage();
            if(user == null)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotFound, $"User with id = {id} not found");
                return response;
            }
            response = Request.CreateResponse(HttpStatusCode.OK, new
            {
                id = user.Id,
                name = user.Name,
                password = user.Password,
                avatarUrl = user.AvatarUrl
            });
            return response;
        }

        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Register(RegisterViewModel registerModel)
        {
            // check if user already exists
            var user = dbContext.Users.SingleOrDefault(u => u.Name == registerModel.Name);
            HttpResponseMessage response = new HttpResponseMessage();
            if(user != null)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, 
                    $"Name = {registerModel.Name} already in use");
                return response;
            }
            user = new User
            {
                Name = registerModel.Name,
                Password = registerModel.Password,
                AvatarUrl = registerModel.AvatarUrl
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            user = dbContext.Users.SingleOrDefault(u => u.Name == registerModel.Name);
            response = Request.CreateResponse(HttpStatusCode.OK, new
            {
                status = "registered",
                user = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password,
                    AvatarUrl = user.AvatarUrl
                }
            });
            return response;
        }

        [Route("change_password")]
        [Authorize]
        [HttpPut]
        public HttpResponseMessage ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var user = dbContext.Users.SingleOrDefault(u => u.Name == User.Identity.Name);
            if (!user.Password.Equals(model.CurrentPassword))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Wrong current password");
            }
            user.Password = model.NewPassword;
            dbContext.Entry(user).State = EntityState.Modified;
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                status = "updated"
            });
        }
    }
}
