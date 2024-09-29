//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Brokers.Storages;
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Services.Foundations;
using Moq;
using System.ComponentModel.DataAnnotations;
using Tynamix.ObjectFiller;

namespace LMS.Web.Unit.Tests.Project.Services.Foundations.UserServices
{
    public partial class UserServiceTest
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IUserService userServise;

        public UserServiceTest()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.userServise = new  UserService(this.storageBrokerMock.Object);
        }

        private User CreateRandomUser()
        {
            return CreateFilteredUser(GetRandomDateTimeOffset()).Create();
        }

        private static DateTimeOffset GetRandomDateTimeOffset()
        {
            return new DateTimeRange( new DateTime()).GetValue();
        }

        private Filler<User> CreateFilteredUser(DateTimeOffset date)
        {
           var filler=new Filler<User>();

            filler.Setup().OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
