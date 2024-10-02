//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Xeptions;

namespace LMS.Web.Models.Foundations.Users.Exceptions
{
    public class UserDependencyException:Xeption
    {
        public UserDependencyException(string message,Xeption innerException)
            :base(message, innerException)
        { }   
    }
}
