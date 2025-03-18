using Newtonsoft.Json;

namespace ChatMessAPI.Infrastructure.Entities.Models
{
    public class Room
    {
        #region constructor
        public Room() { }
        #endregion
        #region attributes
        [JsonProperty(PropertyName ="room")]
        public string NmRoom { get; set; }
        #endregion
    }
}
