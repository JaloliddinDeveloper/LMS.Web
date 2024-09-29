//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Models.Foundations.Users;

namespace LMS.Web.Services.Foundations
{
    public interface IUserService
    {
        ValueTask<User> AddUserAsync(User user);
    }
}
