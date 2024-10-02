﻿//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using FluentAssertions;
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Models.Foundations.Users.Exceptions;
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
            ValueTask<User> addUserTask=
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
    }
}
