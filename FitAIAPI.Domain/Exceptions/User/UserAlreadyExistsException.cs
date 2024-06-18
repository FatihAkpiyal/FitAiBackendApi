using System.Net;

namespace FitAIAPI
{
    public class UserAlreadyExistsException : BaseException
    {
        public UserAlreadyExistsException(string property) : base($"User with given email {property} already exists.", HttpStatusCode.Conflict)
        {

        }
    }
}