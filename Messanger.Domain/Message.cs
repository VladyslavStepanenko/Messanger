namespace Messanger.Domain
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public int DialogId { get; set; }
        public virtual Dialog Dialog { get; set; }
    }
}