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
    [RoutePrefix("api/users")]
    [Authorize]
    public class UsersController : ApiController
    {
        private MessangerDbContext db;

        public UsersController()
        {
            db = new MessangerDbContext();
        }

        [Route("")]
        public IHttpActionResult GetUsers()
        {
            var users = db.Users.ToList();
            return Ok(users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                AvatarUrl = u.AvatarUrl
            }));
        }

        [Route("{id:int}")]
        [Authorize]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            UserViewModel userViewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                AvatarUrl = user.AvatarUrl
            };
            return Ok(userViewModel);
        }

        [Route("")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult RegisterUser(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Name = registerViewModel.Name,
                Password = registerViewModel.Password,
                AvatarUrl = registerViewModel.AvatarUrl
            };
            db.Users.Add(user);
            db.SaveChanges();

            return Ok(); // todo
        }

        [Route("{id:int}")]
        [HttpPut]
        [Authorize]
        public IHttpActionResult EditUser(int id, UserViewModel userViewModel)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Name = userViewModel.Name;
            user.AvatarUrl = userViewModel.AvatarUrl;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            var userFound = db.Users.Find(id);
            var userModel = new UserViewModel
            {
                Id = userFound.Id,
                Name = userFound.Name,
                AvatarUrl = userFound.AvatarUrl
            };
            return Ok(userModel);
        }

        [Route("{id:int}")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteUser(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            db.Entry(user).State = EntityState.Deleted;
            db.SaveChanges();
            return GetUsers(); // ?
        }
    }
}
