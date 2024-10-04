//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using System.Threading.Tasks;
using LMS.Web.Models.Views.UserViews;
using LMS.Web.Services.Views.UserViews;
using Microsoft.AspNetCore.Components;

namespace LMS.Web.Components.Users
{
    public partial class AddUserComponent
    {
        [Inject]
        public IUserViewService UserViewService { get; set; }

        public UserView UserView { get; set; }
        public bool IsVisible { get; set; }

        protected override void OnInitialized()
        {
            UserView = new UserView();
        }

        public void ShowDialog()
        {
            this.IsVisible = true;
        }

        public void HideDialog()
        {
            this.IsVisible = false;
        }

        private async Task CreateUserViewAsync()
        {
            await this.UserViewService
                .AddUserViewAsync(UserView);

            HideDialog();
        }
    }
}
