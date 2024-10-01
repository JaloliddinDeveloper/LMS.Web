//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Brokers.Loggings;
using LMS.Web.Brokers.Storages;
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Models.Foundations.Users.Exceptions;
using LMS.Web.Services.Foundations;
using LMS.Web.Services.Foundations.Users;
using Moq;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace LMS.Web.Unit.Tests.Project.Services.Foundations.UserServices
{
    public partial class UserServiceTest
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;

        private readonly IUserService userServise;

        public UserServiceTest()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.userServise = new  UserService(
                this.storageBrokerMock.Object,
                this.loggingBrokerMock.Object);
        }

        private User CreateRandomUser()
        {
            return CreateFilteredUser(GetRandomDateTimeOffset()).Create();
        }

        private static DateTimeOffset GetRandomDateTimeOffset()
        {
            return new DateTimeRange( new DateTime()).GetValue();
        }
        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedException)=>
             actualException=> actualException.SameExceptionAs(expectedException);
        
        private Filler<User> CreateFilteredUser(DateTimeOffset date)
        {
           var filler=new Filler<User>();

            filler.Setup().OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
