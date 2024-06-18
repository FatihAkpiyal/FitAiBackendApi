using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI
{
    public class UserPasswordIncorrectException : BaseException
    {
        public UserPasswordIncorrectException(string property) : base($"Wrong password for user with email {property}.", HttpStatusCode.Unauthorized)
        {

        }
    }
}
