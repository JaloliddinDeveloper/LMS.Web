//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Web.Models.Views.UserViews;

namespace LMS.Web.Services.Views.UserViews
{
    public interface IUserViewService
    {
        ValueTask<List<UserView>> RetrieveAllUserViewsAsync();
    }
}
