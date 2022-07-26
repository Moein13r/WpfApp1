using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Dtos
{
    public class MessageCreateDto
    {
        public string message { get; set; }
        public int UserId { get; set; }
        public int? ParentMessageId { get; set; }
        public int RoomId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
