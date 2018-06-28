namespace Messanger.Api.Models
{
    public class DialogViewModel
    {
        public int Id { get; set; }
        public MessageViewModel LastMessage { get; set; }
        public int MessagesCount { get; set; }
    }
}