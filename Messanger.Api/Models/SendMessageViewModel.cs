using System.ComponentModel.DataAnnotations;

namespace Messanger.Api.Models
{
    public class SendMessageViewModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public int ReceiverId { get; set; }
    }
}