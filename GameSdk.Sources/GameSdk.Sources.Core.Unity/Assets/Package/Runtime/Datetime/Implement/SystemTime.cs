using System;

namespace GameSdk.Sources.Core.Datetime
{
    [JetBrains.Annotations.UsedImplicitly]
    public class SystemTime : ISystemTime
    {
        private static readonly ISystemTime _systemTime;

        /// <summary>
        /// Singleton instance of SystemTime
        /// </summary>
        public static ISystemTime Time => _systemTime ?? new SystemTime();

        private Func<DateTime> _dateTimeNowFn;
        private Func<DateTimeOffset> _dateTimeOffsetFn;

        private ISystemTime Self => this;

        public TimeSpan Offset { get; private set; }
        public DateTime Now => _dateTimeNowFn.Invoke();
        public DateTimeOffset NowOffset => _dateTimeOffsetFn.Invoke();

        public SystemTime()
        {
            _dateTimeNowFn = Self.DefautNow;
            _dateTimeOffsetFn = Self.DefaultNowOffset;
        }

        public void SetOffset(TimeSpan offset)
        {
            Offset = offset;
        }

        public void SetDateTime(Func<DateTime> dateTimeNowFn)
        {
            _dateTimeNowFn = dateTimeNowFn;
        }

        public void SetDateTimeOffset(Func<DateTimeOffset> dateTimeOffsetFn)
        {
            _dateTimeOffsetFn = dateTimeOffsetFn;
        }

        public void ResetOffset()
        {
            Offset = TimeSpan.Zero;
        }

        public void ResetDateTime()
        {
            _dateTimeNowFn = Self.DefautNow;
        }

        public void ResetDateTimeOffset()
        {
            _dateTimeOffsetFn = Self.DefaultNowOffset;
        }
    }
}
