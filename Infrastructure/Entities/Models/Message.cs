using Newtonsoft.Json;

namespace ChatMessAPI.Infrastructure.Entities.Models
{
    public class Message
    {
        #region constructor
        public Message() { }
        #endregion
        #region attributes
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "room")]
        public string Room { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        #endregion
    }
}
