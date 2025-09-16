
namespace CollegeFootball.Domain.Exceptions
{
    public class SearchValueMissingException : Exception
    {
        public SearchValueMissingException()
        {
        }

        public SearchValueMissingException(string message) : base(message)
        {
        }
    }
}
