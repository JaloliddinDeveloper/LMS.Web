//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Brokers.Storages;
using LMS.Web.Models.Foundations.Users;

namespace LMS.Web.Services.Foundations
{
    public class UserService: IUserService
    {
        private readonly IStorageBroker storageBroker;

        public UserService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<User> AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
