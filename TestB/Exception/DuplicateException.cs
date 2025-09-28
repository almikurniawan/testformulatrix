namespace TestB.Exception
{
    public class DuplicateException : IOException
    {
        public DuplicateException(string message) : base(message)
        {
        }
    }
}
