namespace ItemShop.Dtos
{
    public class JsonPlaceHolderResult<T> where T : class
    {
        public bool IsSuccessful { get; set; }

        public string? ErrorMessage { get; set; }

        public T? Data { get; set; }
    }
}
