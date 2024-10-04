//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using System;

namespace LMS.Web.Brokers.DateTimes
{
    public class DateTimeBroker: IDateTimeBroker
    {
        public DateTimeOffset GetCurrentDateTimeOffset() =>
          DateTimeOffset.UtcNow;
    }
}
