using System;

namespace GameSdk.Core.Datetime
{
    public class SystemTime : ISystemTime
    {
        private static ISystemTime _systemTime;

        /// <summary>
        /// Singleton instance of SystemTime
        /// </summary>
        public static ISystemTime Time => _systemTime ?? new SystemTime();

        public Func<DateTime> Now { get; private set; } = () => DateTime.Now;
        public Func<DateTimeOffset> OffsetNow { get; private set; } = () => DateTimeOffset.Now;


        public void SetDateTime(DateTime dateTimeNow)
        {
            Now = () => dateTimeNow;
        }

        public void SetDateTime(Func<DateTime> dateTimeNowFn)
        {
            Now = dateTimeNowFn;
        }

        public void SetDateTimeOffset(DateTimeOffset dateTimeOffset)
        {
            OffsetNow = () => dateTimeOffset;
        }

        public void SetDateTimeOffset(Func<DateTimeOffset> dateTimeOffsetFn)
        {
            OffsetNow = dateTimeOffsetFn;
        }

        public void ResetDateTime()
        {
            Now = () => DateTime.Now;
            OffsetNow = () => DateTimeOffset.Now;
        }
    }
}
