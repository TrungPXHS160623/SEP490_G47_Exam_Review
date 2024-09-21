namespace Library.Common
{
    public class ResultResponse<T> : RequestResponse
    {
        public List<T> Items { get; set; }

        public T Item { get; set; }
    }
}
