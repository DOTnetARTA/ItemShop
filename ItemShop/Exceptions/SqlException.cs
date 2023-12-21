namespace ItemShop.Exceptions
{
    public class SqlException : Exception
    {
        public SqlException() : base("Sql exception") { }

        public SqlException(string message) : base(message) { }

        public SqlException(string message, Exception innerException) : base(message, innerException) { }
    }
}
