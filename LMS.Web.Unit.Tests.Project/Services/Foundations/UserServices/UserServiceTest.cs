//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Brokers.DateTimes;
using LMS.Web.Brokers.Loggings;
using LMS.Web.Brokers.Storages;
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Services.Foundations;
using LMS.Web.Services.Foundations.Users;
using Microsoft.Data.SqlClient;
using Moq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xeptions;

namespace LMS.Web.Unit.Tests.Project.Services.Foundations.UserServices
{
    public partial class UserServiceTest
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;

        private readonly IUserService userServise;

        public UserServiceTest()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.userServise = new  UserService(
                this.storageBrokerMock.Object,
                this.loggingBrokerMock.Object,
                this.dateTimeBrokerMock.Object);
        }

        private static int GetRandomNumber() =>
           new IntRange(min: 2, max: 10).GetValue();
        private static IQueryable<User> CreateRandomUsers()
        {
            return CreateUserFiller(dates: GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber()).AsQueryable();
        }


        private static User CreateRandomUser() =>
        CreateUserFiller(GetRandomDateTimeOffset()).Create();

        public static User CreateRandomUser(DateTimeOffset date) =>
         CreateUserFiller(date).Create();
        private static DateTimeOffset GetRandomDateTimeOffset() =>
          new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedException)=>
             actualException=> actualException.SameExceptionAs(expectedException);

        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static string GetRandomString()=>
            new MnemonicString().GetValue().ToString();


        private static Filler<User> CreateUserFiller(DateTimeOffset dates)
        {
            var filler = new Filler<User>();

            filler.Setup()
               .OnType<DateTimeOffset>().Use(dates);
            return filler;
        }
    }
}
