//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using LMS.Web.Models.Foundations.Users;

namespace LMS.Web.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> AddUserAsync(User user);
        ValueTask<IQueryable<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByIdAsync(Guid userId);
    }
}
