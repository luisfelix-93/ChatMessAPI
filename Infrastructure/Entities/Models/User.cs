using Newtonsoft.Json;

namespace ChatMessAPI.Infrastructure.Entities.Models
{
    public class User
    {
        #region constructor
        public User() { }
        #endregion
        #region attributes
        [JsonProperty(PropertyName = "socketId")]
        public string SocketId { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "room")]
        public string Room { get; set; }
        #endregion
    }
}
