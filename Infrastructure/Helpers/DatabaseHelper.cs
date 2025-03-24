namespace ChatMessAPI.Infrastructure.Helpers
{
    public class DatabaseHelper
    {
        #region constructor
        public DatabaseHelper() { }
        #endregion
        #region Attributes
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? MessageCollection { get; set; }
        public string? UserCollection { get; set; }
        public string? RoomCollection { get; set; }
        #endregion

    }
}
