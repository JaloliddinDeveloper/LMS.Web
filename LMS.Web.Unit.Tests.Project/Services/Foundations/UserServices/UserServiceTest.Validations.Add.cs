//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Models.Foundations.Users.Exceptions;
using Moq;

namespace LMS.Web.Unit.Tests.Project.Services.Foundations.UserServices
{
    public partial class UserServiceTest
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfUserIsNullAndLogItAsync()
        {
            //given
            User nullUser= null;
            var nullUserException=new NullUserException("User is null");

            var expectedValidationException=
                new UserValidationException(
                    message: "User Validation error occurred,fix the errors and try again",
                    innerException:nullUserException);

            //when
            ValueTask<User> addUserTask=this.userServise.AddUserAsync(nullUser);

            //then
            await Assert.ThrowsAsync<UserValidationException>(() => addUserTask.AsTask());

            this.loggingBrokerMock.Verify(broker=>
            broker.LogError(It.Is(SameExeptionAs(expectedValidationException))),Times.Once());
            this.storageBrokerMock.Verify(broker =>
            broker.InsertUserAsync(It.IsAny<User>()),Times.Never());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
