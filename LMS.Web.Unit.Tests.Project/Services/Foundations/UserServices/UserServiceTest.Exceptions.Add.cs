//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using EFxceptions.Models.Exceptions;
using FluentAssertions;
using LMS.Web.Models.Foundations.Users;
using Microsoft.Data.SqlClient;
using Moq;

namespace LMS.Web.Unit.Tests.Project.Services.Foundations.UserServices
{
    public partial class UserServiceTest
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccuresAndLogItAsync()
        {
            //given
            User someUser = CreateRandomUser();
            SqlException sqlException = GetSqlException();

            var failedUserStorageException =
                new FailedUserStorageException(
                    message: "User storage error occurred,please contact support",
                    innerException: sqlException);

            UserDependencyException expectedUserDependencyException =
                new UserDependencyException(
                    message: "User dependency exception error occurred,please contact support",
                    innerException: failedUserStorageException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertUserAsync(someUser)).ThrowsAsync(sqlException);

            //when
            ValueTask<User> addUserTask =
                this.userServise.AddUserAsync(someUser);

            UserDependencyException actualUserDependencyException =
                await Assert.ThrowsAsync<UserDependencyException>(addUserTask.AsTask);

            //then
            actualUserDependencyException.Should().BeEquivalentTo(expectedUserDependencyException);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertUserAsync(It.IsAny<User>()), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(expectedUserDependencyException))),
            Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowExceptionAddIfDublicateKeyErrorOccurres()
        {
            //given
            User someUser = CreateRandomUser();
            string someString = GetRandomString();
            var duplicateKeyException =
                new DuplicateKeyException(someString);

            var alreadyExistUserException =
                new AlreadyExistUserException(
                    message: "User already exist,please try again",
                    innerException: duplicateKeyException);

            var expectedUserDependencyValidationException =
                new UserDependencyValidationException(
                    message: "User dependency error occurred,fix errors and try again",
                    innerException: alreadyExistUserException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertUserAsync(someUser)).ThrowsAsync(duplicateKeyException);

            //when
            ValueTask<User> addUserTask = this.userServise.AddUserAsync(someUser);
            UserDependencyValidationException actualUserDependencyValidationException =
                await Assert.ThrowsAnyAsync<UserDependencyValidationException>(addUserTask.AsTask);

            //then
            actualUserDependencyValidationException.Should().
                BeEquivalentTo(expectedUserDependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertUserAsync(It.IsAny<User>()), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(expectedUserDependencyValidationException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowExceptionOnAddIfServiceErrorOccurs()
        {
            //given
            User someUser = CreateRandomUser();
            var exception = new Exception();
            var failedUserServiceException =
                new FailedUserServiceException(
                    message: "Unexpected error of user occured",
                    innerException: exception);

            UserDependencyServiceException expectedUserDependencyServiceException =
              new UserDependencyServiceException(
                  message: "Unexpected service error occured,contact support",
                  innerException: failedUserServiceException);

            this.storageBrokerMock.Setup(broker=>
            broker.InsertUserAsync(someUser)).ThrowsAsync(exception);

            //when
            ValueTask<User> addUserTask=this.userServise.AddUserAsync(someUser);
            UserDependencyServiceException actualUserDependencyServiceException =
               await Assert.ThrowsAsync<UserDependencyServiceException>(addUserTask.AsTask);

            //then
            actualUserDependencyServiceException.Should().
                BeEquivalentTo(expectedUserDependencyServiceException);
            this.storageBrokerMock.Verify(broker=>
            broker.InsertUserAsync(It.IsAny<User>()),Times.Once);

            this.loggingBrokerMock.Verify(broker=>
            broker.LogError(It.Is(SameExceptionAs(expectedUserDependencyServiceException))),Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
