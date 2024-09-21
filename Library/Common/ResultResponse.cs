namespace Library.Common
{
    public class ResultResponse<T> : RequestResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public List<T> Items { get; set; }
    }
}
