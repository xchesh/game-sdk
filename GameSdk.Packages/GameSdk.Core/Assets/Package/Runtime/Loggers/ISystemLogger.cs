using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSdk.Core.Loggers
{
    /// <summary>
    /// Interface for SystemLogger. This is used to log messages to the console with different log types and tags.
    /// </summary>
    public interface ISystemLogger
    {
        /// <summary>
        /// Unity ILogger used to log messages to the console.
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// Enable or disable logging.
        /// </summary>
        bool IsLogEnabled { get; set; }

        /// <summary>
        /// Filter log type to only log messages of this type or lower.
        /// </summary>
        LogType FilterLogType { get; set; }

        /// <summary>
        /// Dictionary of tags to enable or disable logging for specific tags.
        /// </summary>
        Dictionary<string, bool> LogTags { get; set; }

        /// <summary>
        /// Log a message to the console with a specific log type.
        /// </summary>
        /// <param name="logType">Log type of the message.</param>
        /// <param name="tag">Tag of the message.</param>
        /// <param name="message">Message to log.</param>
        void Log(LogType logType, string tag, object message);

        /// <summary>
        /// Log a message to the console with a specific log type.
        /// </summary>
        /// <param name="logType">Log type of the message.</param>
        /// <param name="tag">Tag of the message.</param>
        /// <param name="messageFn">Function that returns the message to log.</param>
        void Log(LogType logType, string tag, Func<string> messageFn);

        /// <summary>
        /// Log a message to the console with a format string.
        /// </summary>
        /// <param name="logType">Log type of the message.</param>
        /// <param name="tag">Tag of the message.</param>
        /// <param name="format">Format string of the message.</param>
        /// <param name="args">Arguments to format the message with.</param>
        void LogFormat(LogType logType, string tag, string format, params object[] args);

        /// <summary>
        /// Log a message to the console with a format string.
        /// </summary>
        /// <param name="logType">Log type of the message.</param>
        /// <param name="tag">Tag of the message.</param>
        /// <param name="formatFn">Function that returns the format string to log.</param>
        /// <param name="args">Arguments to format the message with.</param>
        void LogFormat(LogType logType, string tag, Func<string> formatFn, params object[] args);

        /// <summary>
        /// Log an exception to the console.
        /// </summary>
        /// <param name="exception">Exception to log.</param>
        void LogException(Exception exception);

        /// <summary>
        /// Check if a log message is allowed to be logged.
        /// </summary>
        /// <param name="logType">Log type of the message.</param>
        /// <param name="tag">Tag of the message.</param>
        /// <returns>True if the message is allowed to be logged, false otherwise.</returns>
        bool IsLogAllowed(LogType logType, string tag);
    }
}
