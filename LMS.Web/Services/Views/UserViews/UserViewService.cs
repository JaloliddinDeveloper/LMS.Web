//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Models.Views.UserViews;
using LMS.Web.Services.Foundations.Users;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Services.Views.UserViews
{
    public class UserViewService : IUserViewService
    {
        private readonly IUserService userService;

        public UserViewService(IUserService userService) =>
            this.userService = userService;

        public async ValueTask<List<UserView>> RetrieveAllUserViewsAsync()
        {
            IQueryable<User> users =
                await this.userService.RetrieveAllUsersAsync();

            return await MapUsersToUserViewsAsync(users);
        }

        private static async ValueTask<List<UserView>> MapUsersToUserViewsAsync(
            IQueryable<User> users)
        {
            return await users
                .Select(user => new UserView
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = (ViewRole)user.Role,
                    CreatedDate = user.CreatedDate,
                    UpdatedDate = user.UpdatedDate
                })
                .ToListAsync();
        }
    }
}
