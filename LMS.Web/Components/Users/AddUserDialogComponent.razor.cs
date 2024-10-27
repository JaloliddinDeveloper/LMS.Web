//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using System.Threading.Tasks;
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Models.Views.UserViews;
using LMS.Web.Services.Views.UserViews;
using Microsoft.AspNetCore.Components;

namespace LMS.Web.Components.Users
{
    public partial class AddUserDialogComponent
    {
        [Inject]
        public IUserViewService UserViewService { get; set; }

        [Parameter]
        public EventCallback<UserView> OnCreateUser { get; set; }

        public UserView UserView { get; set; }
        public bool IsVisible { get; set; }

        protected override void OnInitialized() =>
            InitializNewUser();

        private void InitializNewUser() =>
            this.UserView = new UserView();

        public void ShowDialog()
        {
            this.IsVisible = true;
            StateHasChanged();
        }

        public void HideDialog()
        {
            this.IsVisible = false;
            InitializNewUser();
            StateHasChanged();
        }

        private async Task CreateUserViewAsync()
        {
            UserView addedUserView = 
                await this.UserViewService
                    .AddUserViewAsync(this.UserView);

            await this.OnCreateUser
                .InvokeAsync(addedUserView);

            HideDialog();
        }
    }
}
