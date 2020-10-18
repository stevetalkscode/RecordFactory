using System;
// ReSharper disable UnusedMember.Global

namespace RecordFactoryDemo
{
    public interface IDateTimeWrapper
    {
        DateTime GetCurrentDateTimeUtc();
        DateTime GetCurrentDateTimeLocal();
        DateTimeOffset GetCurrentDateTimeOffset();
    }
}