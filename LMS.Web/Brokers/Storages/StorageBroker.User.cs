﻿//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Models.Foundations.Users;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<User> Users { get; set; }

        public async ValueTask<User> InsertUserAsync(User user)=>
             await InsertAsync(user);
    }
}