//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Web.Models.Views.UserViews;
using LMS.Web.Services.Views.UserViews;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;

namespace LMS.Web.Components.Users
{
    public partial class AllUsersComponent
    {
        [Inject]
        public IUserViewService UserViewService { get; set; }

        public List<UserView> UserViews { get; set; }
        public SfGrid<UserView> Grid { get; set; }
        public AddUserDialogComponent AddUserDialogComponent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.UserViews =
                await this.UserViewService
                    .RetrieveAllUserViewsAsync();

            this.StateHasChanged();
        }

        private void ShowAddDialog() =>
            this.AddUserDialogComponent.ShowDialog();

        private async Task AddCreatedUserToGridAsync(UserView userView)
        {
            UserView retrievedUserView =
                await this.UserViewService.RetrieveUserViewByIdAsync
                    (userView.Id);

            this.UserViews.Add(retrievedUserView);

            await this.Grid.Refresh();
            StateHasChanged();
        }
    }
}
