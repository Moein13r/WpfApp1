using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Chat.Models
{
    public partial class Message
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("message")]
        public string message { get; set; } = null!;

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("parentMessageId")]
        public int? ParentMessageId { get; set; }

        [JsonPropertyName("roomId")]
        public int RoomId { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
