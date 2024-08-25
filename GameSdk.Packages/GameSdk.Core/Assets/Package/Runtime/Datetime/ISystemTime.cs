using System;

namespace GameSdk.Core.Datetime
{
    /// <summary>
    /// Used for getting DateTime.Now or DateOffset.Now. Time is changeable for unit testing
    /// </summary>
    public interface ISystemTime
    {
        /// <summary>
        /// Offset to be added to DateTime.Now and DateTimeOffset.Now
        /// </summary>
        TimeSpan Offset { get; }

        /// <summary>
        /// Normally this is a pass-through to DateTime.Now, but it can be overridden with SetDateTime( .. ) for unit testing and debugging.
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Normally this is a pass-through to DateTimeOffset.Now, but it can be overridden with SetDateTimeOffset( .. ) for unit testing and debugging.
        /// </summary>
        DateTimeOffset NowOffset { get; }

        /// <summary>
        /// Set the offset to be added to DateTime.Now and DateTimeOffset.Now
        /// </summary>
        void SetOffset(TimeSpan offset);

        /// <summary>
        /// Set time func to return when ISystemTime.Now() is called.
        /// </summary>
        void SetDateTime(Func<DateTime> dateTimeNowFn);

        /// <summary>
        /// Set time func to return when ISystemTime.OffsetNow() is called.
        /// </summary>
        void SetDateTimeOffset(Func<DateTimeOffset> dateTimeOffsetFn);

        /// <summary>
        /// Resets ISystemTime.Offset to TimeSpan.Zero.
        /// </summary>
        void ResetOffset();

        /// <summary>
        /// Resets ISystemTime.Now() to return DateTime.Now.
        /// </summary>
        void ResetDateTime();

        /// <summary>
        /// Resets ISystemTime.OffsetNow() to return DateTimeOffset.Now.
        /// </summary>
        void ResetDateTimeOffset();

        /// <summary>
        /// Default DateTime.Now + Offset
        /// </summary>
        DateTime DefautNow() => DateTime.Now + Offset;

        /// <summary>
        /// Default DateTimeOffset.Now + Offset
        /// </summary>
        DateTimeOffset DefaultNowOffset() => DateTimeOffset.Now + Offset;
    }
}
