using System;

namespace GameSdk.Core.Datetime
{
    /// <summary>
    /// Used for getting DateTime.Now or DateOffset.Now. Time is changeable for unit testing
    /// </summary>
    public interface ISystemTime
    {
        /// <summary>
        /// Normally this is a pass-through to DateTime.Now, but it can be overridden with SetDateTime( .. ) for unit testing and debugging.
        /// </summary>
        Func<DateTime> Now { get; }

        /// <summary>
        /// Normally this is a pass-through to DateTimeOffset.Now, but it can be overridden with SetDateTime( .. ) for unit testing and debugging.
        /// </summary>
        Func<DateTimeOffset> OffsetNow { get; }

        /// <summary>
        /// Set time to return when ISystemTime.Now() is called.
        /// </summary>
        void SetDateTime(DateTime dateTimeNow);

        /// <summary>
        /// Set time func to return when ISystemTime.Now() is called.
        /// </summary>
        void SetDateTime(Func<DateTime> dateTimeNowFn);

        /// <summary>
        /// Set time to return when ISystemTime.OffsetNow() is called.
        /// </summary>
        void SetDateTimeOffset(DateTimeOffset dateTimeOffset);

        /// <summary>
        /// Set time func to return when ISystemTime.OffsetNow() is called.
        /// </summary>
        void SetDateTimeOffset(Func<DateTimeOffset> dateTimeOffsetFn);

        /// <summary>
        /// Resets ISystemTime.Now() and ISystemTime.OffsetNow() to return DateTime.Now and DateTimeOffset.Now respectively.
        /// </summary> 
        void ResetDateTime();
    }
}