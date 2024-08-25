using System;
using UnityEngine;

namespace GameSdk.Core.Loggers
{
    [JetBrains.Annotations.UsedImplicitly]
    public class SystemLog
    {
        private static ISystemLogger _systemLogger;

        /// <summary>
        /// Singleton instance of SystemLogger
        /// </summary>
        public static ISystemLogger Logger => _systemLogger ?? new SystemLogger(Debug.unityLogger);

        /// <summary>
        /// Log a message to the console.
        /// </summary>
        /// <param name="tag">Tag of the message.</param>
        /// <param name="message">Message to log.</param>
        public static void Log(string tag, object message)
            => Logger.Log(LogType.Log, tag, message);

        /// <summary>
        /// Log a message of type LogType.Warning to the console.
        /// </summary>
        /// <param name="tag">Tag of the message.</param>
        /// <param name="message">Message to log.</param>
        public static void LogWarning(string tag, object message)
            => Logger.Log(LogType.Warning, tag, message);

        /// <summary>
        /// Log a message of type LogType.Error to the console.
        /// </summary>
        /// <param name="tag">Tag of the message.</param>
        /// <param name="message">Message to log.</param>
        public static void LogError(string tag, object message)
            => Logger.Log(LogType.Error, tag, message);

        /// <summary>
        /// Log an exception to the console.
        /// </summary>
        /// <param name="exception">Exception to log.</param>
        public static void LogException(Exception exception)
            => Logger.LogException(exception);

        /// <summary>
        /// Log a message to the console with a format string.
        /// </summary>
        /// <param name="logType">Log type of the message.</param>
        /// <param name="tag">Tag of the message.</param>
        /// <param name="format">Format string of the message.</param>
        /// <param name="args">Arguments to format the message with.</param>
        public static void LogFormat(LogType logType, string tag, string format, params object[] args) =>
            Logger.LogFormat(logType, tag, format, args);

        /// <summary>
        /// Log a message to the console with a specific log type.
        /// </summary>
        /// <param name="logType">Log type of the message.</param>
        /// <param name="tag">Tag of the message.</param>
        /// <param name="message">Message to log.</param>
        public static void Log(LogType logType, string tag, object message)
            => Logger.Log(logType, tag, message);
    }
}
