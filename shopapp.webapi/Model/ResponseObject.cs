namespace shopapp.webapi.Model
{
    public class ResponseObject
    {
        public string? Message { get; set; }
        public bool IsSuccsess { get; set; }
        public List<string>? Errors { get; set; }
    }
}