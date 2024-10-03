//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
namespace LMS.Web.Brokers.DateTimes
{
    public class DateTimeBroker: IDateTimeBroker
    {
        public DateTimeOffset GetCurrentDateTimeOffset() =>
          DateTimeOffset.UtcNow;
    }
}
