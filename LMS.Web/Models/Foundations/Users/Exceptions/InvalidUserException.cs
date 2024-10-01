//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Xeptions;

namespace LMS.Web.Models.Foundations.Users.Exceptions
{
    public class InvalidUserException:Xeption
    {
        public InvalidUserException(string message)
            :base(message)
        { }
    }
}
