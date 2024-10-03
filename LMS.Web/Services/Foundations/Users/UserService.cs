//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Brokers.DateTimes;
using LMS.Web.Brokers.Loggings;
using LMS.Web.Brokers.Storages;
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Services.Foundations.Users;

namespace LMS.Web.Services.Foundations
{
    public partial class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public UserService(
            IStorageBroker storageBroker, 
            ILoggingBroker loggingBroker, 
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<User> AddUserAsync(User user) =>
             TryCatch(async () =>
            {
                ValidateUserOnAdd(user);
                return await this.storageBroker.InsertUserAsync(user);
            });

        public IQueryable<User> RetrieveAllUsers()=>
            this.storageBroker.SelectAllUsers();  
    }
}
