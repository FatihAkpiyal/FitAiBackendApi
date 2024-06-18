using System.Net;

namespace FitAIAPI
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException(string property) : base($"User with given property {property} not found.", HttpStatusCode.NotFound)
        {

        }
    }
}
