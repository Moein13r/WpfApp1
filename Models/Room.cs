using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Chat.Models
{
    public partial class Room
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("unSeenedMessages")]
        public int UnSeenedMessages { get; set; }

        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }
        [JsonPropertyName("type")]
        public int Type { get; set; }
    }
}