//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Models.Foundations.Users.Exceptions;

namespace LMS.Web.Services.Foundations
{
    public partial class UserService
    {
        private void ValidationUserNotNull(User user)
        {
            if(user is null)
            {
                throw new NullUserException("User is null");
            }
        }
    }
}
