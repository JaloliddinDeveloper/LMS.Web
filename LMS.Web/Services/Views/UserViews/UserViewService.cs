//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Web.Brokers.DateTimes;
using LMS.Web.Migrations;
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Models.Views.UserViews;
using LMS.Web.Services.Foundations.Users;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Services.Views.UserViews
{
    public class UserViewService : IUserViewService
    {
        private readonly IUserService userService;
        private readonly IDateTimeBroker dateTimeBroker;

        public UserViewService(
            IUserService userService, 
            IDateTimeBroker dateTimeBroker)
        {
            this.userService = userService;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async ValueTask<List<UserView>> RetrieveAllUserViewsAsync()
        {
            IQueryable<User> users =
                await this.userService.RetrieveAllUsersAsync();

            return await MapUsersToUserViewsAsync(users);
        }


        public async ValueTask<UserView> AddUserViewAsync(UserView userView)
        {
            User user = MapToUser(userView);

            User addedUser =
                await this.userService.AddUserAsync(user);

            return MapToUserView(addedUser);
        }


        public async ValueTask<UserView> RetrieveUserViewByIdAsync(Guid userId)
        {
            User user = await this.userService
                .RetrieveUserByIdAsync(userId);

            return MapToUserView(user);
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

        private User MapToUser(UserView userView)
        {
            DateTimeOffset currentDate =
                this.dateTimeBroker.GetCurrentDateTimeOffset();

            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = userView.FirstName,
                LastName = userView.LastName,
                Email = userView.Email,
                Role = (Role)userView.Role,
                CreatedDate = currentDate,
                UpdatedDate = currentDate
            };
        }

        private UserView MapToUserView(User addedUser)
        {
            return new UserView
            {
                Id = addedUser.Id,
                FirstName = addedUser.FirstName,
                LastName = addedUser.LastName,
                Email = addedUser.Email,
                Role = (ViewRole)addedUser.Role,
                CreatedDate = addedUser.CreatedDate,
                UpdatedDate = addedUser.UpdatedDate
            };
        }
    }
}
