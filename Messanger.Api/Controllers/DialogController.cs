using Messanger.Api.Models;
using Messanger.Domain;
using Messanger.Infra.DataContexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Messanger.Api.Controllers
{
    [RoutePrefix("api/users")]
    public class DialogController : ApiController
    {
        private MessangerDbContext dbContext;

        public DialogController()
        {
            dbContext = new MessangerDbContext();
        }

        [Route("{userId:int}/dialogs")]
        public HttpResponseMessage GetDialogsByUser(int userId)
        {
            var user = dbContext.Users.Find(userId);
            if(user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User not found");
            }
            var dialogsByUser = user.Dialogs;
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                count = dialogsByUser.Count,
                dialogs = dialogsByUser.Select(d => new DialogViewModel
                {
                    Id = d.DialogId,
                    LastMessage = d.Messages.Select(m => new MessageViewModel
                    {
                        Id = m.MessageId,
                        Text = m.Text,
                        Sender = new UserViewModel
                        {
                            Id = m.UserId,
                            Name = m.User.Name,
                            AvatarUrl = m.User.AvatarUrl
                        }
                    }).LastOrDefault()
                })
            });
        }

        [Route("{userId:int}/dialogs/{dialogId:int}/messages")]
        public HttpResponseMessage GetMessages(int userId, int dialogId)
        {
            var user = dbContext.Users.Find(userId);
            if(user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User not found");
            }
            var dialogByUser = user.Dialogs.SingleOrDefault(d => d.DialogId == dialogId);
            if(dialogByUser == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Dialog not found");
            }
            var messagesByDialog = dialogByUser.Messages;
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                count = messagesByDialog.Count,
                messages = messagesByDialog.Select(m => new MessageViewModel
                {
                    Id = m.MessageId,
                    Text = m.Text,
                    Sender = new UserViewModel
                    {
                        Id = m.UserId,
                        Name = m.User.Name,
                        AvatarUrl = m.User.AvatarUrl
                    }
                })
            });
        }

        [Route("{userId:int}/send_message")]
        [HttpPost]
        public HttpResponseMessage SendMessage(int userId, SendMessageViewModel model)
        {
            var sender = dbContext.Users.Find(userId);
            if (sender == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User not found");
            }
            var receiver = dbContext.Users.Find(model.ReceiverId);
            if (receiver == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User not found");
            }
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var message = new Message
            {
                Text = model.Text,
                User = sender,
                UserId = sender.UserId
            };
            // find dialog
            var dialog = sender.Dialogs.SingleOrDefault(d => d.Users.Contains(receiver));
            if (dialog == null)
            {
                dialog = new Dialog();
                //dbContext.Dialogs.Add(dialog);
                //dbContext.SaveChanges();
            }
            dialog.Users.Add(sender);
            dialog.Users.Add(receiver);
            dialog.Messages.Add(message);
            //dbContext.Entry(dialog).State = EntityState.Modified;
            message.Dialog = dialog;
            message.DialogId = dialog.DialogId;
            dbContext.Messages.Add(message);
            dbContext.SaveChanges();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new
            {
                status = "delivered"
            });
            return response;
        }
    }
}
