//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using FluentAssertions;
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
            var nullUserException=new
                NullUserException("User is null");

            var expectedValidationException=
                new UserValidationException(
                    message: "User Validation error occurred,fix the errors and try again",
                    innerException:nullUserException);

            //when
            ValueTask<User> addUserTask=this.userServise.AddUserAsync(nullUser);

            //then
            await Assert.ThrowsAsync<UserValidationException>(() => addUserTask.AsTask());

            this.loggingBrokerMock.Verify(broker=>
            broker.LogError(It.Is(SameExceptionAs(expectedValidationException))),Times.Once());
            this.storageBrokerMock.Verify(broker =>
            broker.InsertUserAsync(It.IsAny<User>()),Times.Never());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfUserIsInvalidDataAndLogItAsync(string invalidData)
        {
            // given
            var invalidUser = new User()
            {
                FirstName = invalidData
            };

            var invalidUserException =
                new InvalidUserException(message: "User is invalid");

            invalidUserException.AddData(key: nameof(User.Id),
                values: "Id is required");

            invalidUserException.AddData(key: nameof(User.FirstName),
                values: "Text is required");


            invalidUserException.AddData(key: nameof(User.LastName),
                 values: "Text is required");

            invalidUserException.AddData(key: nameof(User.Email),
                 values: "Text is required");

            invalidUserException.AddData(key: nameof(User.Password),
                 values: "Text is required");

            invalidUserException.AddData(key: nameof(User.CreatedDate),
                 values: "Data is required");
            invalidUserException.AddData(key: nameof(User.UpdatedDate),
                 values: "Data is required");

            var expectedUserValidationExpected =
                new UserValidationException(
                    message: "User Validation error occurred,fix the errors and try again",
                innerException: invalidUserException);

            // when
            ValueTask<User> addUser = 
               this.userServise.AddUserAsync(invalidUser);

            UserValidationException actualUserValidationException =
                await Assert.ThrowsAsync<UserValidationException>(addUser.AsTask);

            // then
            actualUserValidationException.Should().BeEquivalentTo(expectedUserValidationExpected);

            this.loggingBrokerMock.Verify(broker =>
              broker.LogError(It.Is(SameExceptionAs(expectedUserValidationExpected))),
              Times.Once());

            this.storageBrokerMock.Verify(broker =>
              broker.InsertUserAsync(It.IsAny<User>()),
              Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
