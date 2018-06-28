using Messanger.Domain;
using Messanger.Infra.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Messanger.Api.Controllers
{
    public class UsersController : ApiController
    {
        private MessangerDbContext db;

        public UsersController()
        {
            db = new MessangerDbContext();
        }

        public IEnumerable<User> GetUsers()
        {
            return db.Users;
        }
    }
}
