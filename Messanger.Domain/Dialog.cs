﻿using System.Collections.Generic;

namespace Messanger.Domain
{
    public class Dialog
    {
        public int Id { get; set; }
        public Message LastMessage  { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Message> Messages { get; set; }

        public Dialog()
        {
            Messages = new HashSet<Message>();
        }
    }
}