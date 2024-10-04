//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
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

        public async ValueTask<User> AddUserAsync(User user) =>
            await this.storageBroker.InsertUserAsync(user);

        public async ValueTask<IQueryable<User>> RetrieveAllUsersAsync() =>
            await this.storageBroker.SelectAllUsersAsync();
    }
}
