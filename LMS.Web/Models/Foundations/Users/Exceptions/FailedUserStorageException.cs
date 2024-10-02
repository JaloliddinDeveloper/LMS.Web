﻿//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Xeptions;

namespace LMS.Web.Models.Foundations.Users.Exceptions
{
    public class FailedUserStorageException:Xeption
    {
        public FailedUserStorageException(string message, Exception innerException)
            :base(message, innerException) 
        { }
    }
}
