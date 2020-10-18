using System;

namespace RecordFactoryDemo
{
    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime GetCurrentDateTimeUtc() => DateTime.UtcNow;
        public DateTime GetCurrentDateTimeLocal() => DateTime.Now;
        public DateTimeOffset GetCurrentDateTimeOffset() => DateTimeOffset.Now;
    }

}
