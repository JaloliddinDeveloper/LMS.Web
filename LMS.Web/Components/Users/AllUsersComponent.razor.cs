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
using Syncfusion.Blazor.Navigations;

namespace LMS.Web.Components.Users
{
    public partial class AllUsersComponent
    {
        [Inject]
        public IUserViewService UserViewService { get; set; }

        public List<UserView> UserViews { get; set; }
        public AddUserComponent AddUserComponent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.UserViews =
                await this.UserViewService
                    .RetrieveAllUserViewsAsync();

            this.AddUserComponent.ShowDialog();

            this.StateHasChanged();
        }

        public void ToolbarClickHandler(ClickEventArgs args)
        {
            if (args.Item.Text == "Add")
            {
                this.AddUserComponent.ShowDialog();
            }
        }

        public string[] ToolbarItems = new string[] { "Add", "Edit", "Delete", "Update", "Cancel" };
    }
}
