using System;

namespace GameSdk.Core.Datetime
{
    [JetBrains.Annotations.UsedImplicitly]
    public class SystemTime : ISystemTime
    {
        private static readonly ISystemTime _systemTime;

        /// <summary>
        /// Singleton instance of SystemTime
        /// </summary>
        public static ISystemTime Time => _systemTime ?? new SystemTime();

        private Func<DateTime> _dateTimeNowFn = ISystemTime.DefautNow;
        private Func<DateTimeOffset> _dateTimeOffsetFn = ISystemTime.DefaultNowOffset;

        public DateTime Now => _dateTimeNowFn.Invoke();
        public DateTimeOffset NowOffset => _dateTimeOffsetFn.Invoke();

        public void SetDateTime(Func<DateTime> dateTimeNowFn)
        {
            _dateTimeNowFn = dateTimeNowFn;
        }

        public void SetDateTimeOffset(Func<DateTimeOffset> dateTimeOffsetFn)
        {
            _dateTimeOffsetFn = dateTimeOffsetFn;
        }

        public void ResetDateTime()
        {
            _dateTimeNowFn = ISystemTime.DefautNow;
        }

        public void ResetDateTimeOffset()
        {
            _dateTimeOffsetFn = ISystemTime.DefaultNowOffset;
        }
    }
}
