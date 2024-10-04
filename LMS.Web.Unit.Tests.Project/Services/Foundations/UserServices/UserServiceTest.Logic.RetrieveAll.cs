//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using FluentAssertions;
using Force.DeepCloner;
using LMS.Web.Models.Foundations.Users;
using Moq;

namespace LMS.Web.Unit.Tests.Project.Services.Foundations.UserServices
{
    public partial class UserServiceTest
    {
        [Fact]
        public async Task ShouldRetrieveAllUsers()
        {
            //given
            IQueryable<User> randomUsers = CreateRandomUsers();
            IQueryable<User> storageUsers = randomUsers;
            IQueryable<User> expectedUsers=storageUsers;

            this.storageBrokerMock.Setup(broker=>
            broker.SelectAllUsers()).Returns(storageUsers);
            //when
            IQueryable<User> actualUsers=this.userServise.RetrieveAllUsers();

            //then
            actualUsers.Should().BeEquivalentTo(expectedUsers);

            this.storageBrokerMock.Verify(broker=>
            broker.SelectAllUsers(),Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
