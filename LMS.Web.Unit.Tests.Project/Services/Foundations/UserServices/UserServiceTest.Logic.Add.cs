//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using FluentAssertions;
using LMS.Web.Models.Foundations.Users;
using Moq;

namespace LMS.Web.Unit.Tests.Project.Services.Foundations.UserServices
{
    public partial class UserServiceTest
    {
        [Fact]
        public async Task ShouldAddUserAsync()
        {
            //given
            User randomUser=CreateRandomUser();
            User inputUser = randomUser;
            User storageUser = inputUser;
            User expected=storageUser;

            this.storageBrokerMock.Setup(broker =>
            broker.InsertUserAsync(inputUser)).ReturnsAsync(storageUser);
             //when
             User actualUser=await this.userServise.AddUserAsync(inputUser);
            //then
            actualUser.Should().BeEquivalentTo(expected);

            this.storageBrokerMock.Verify(broker=>
            broker.InsertUserAsync(inputUser), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
