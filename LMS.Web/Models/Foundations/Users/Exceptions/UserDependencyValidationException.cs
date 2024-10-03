//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Xeptions;

namespace LMS.Web.Models.Foundations.Users.Exceptions
{
    public class UserDependencyValidationException:Xeption
    {
        public UserDependencyValidationException(string message, Xeption innerException)
            :base(message, innerException)
        { }   
    }
}
