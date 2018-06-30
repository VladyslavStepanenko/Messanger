using Messanger.Api.Models;
using Messanger.Infra.DataContexts;
using System;
using System.Collections.Generic;
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

    }
}
