namespace Messanger.Domain
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int DialogId { get; set; }
        public virtual Dialog Dialog { get; set; }
    }
}