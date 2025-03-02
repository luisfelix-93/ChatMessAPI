namespace ChatMessAPI.Infrastructure.Entities.DTO
{
    public class ReturnBaseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ReturnDTO : ReturnBaseDTO
    {
        public object ResultObject { get; set; }
    }
}
