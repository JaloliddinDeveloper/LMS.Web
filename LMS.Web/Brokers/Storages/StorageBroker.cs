﻿//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Models.Foundations.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace LMS.Web.Brokers.Storages
{
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                this.configuration.GetConnectionString("DefaultConnectionString");

            optionsBuilder.UseSqlServer(connectionString);
        }

        private async ValueTask<T> InsertAsync<T>(T @object) where T : class
        {
            using var broker = new StorageBroker(this.configuration);
            broker.Entry<T>(@object).State = EntityState.Added;
            await broker.SaveChangesAsync();
            return @object;
        }
        private  IQueryable<T> SelectAll<T>() where T : class
        {
            using var broker = new StorageBroker(this.configuration);
            return broker.Set<T>();
        }
        public override void Dispose() { }
    }
}
