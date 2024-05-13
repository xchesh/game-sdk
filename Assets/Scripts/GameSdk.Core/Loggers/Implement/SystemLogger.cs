using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSdk.Core.Loggers
{
    [JetBrains.Annotations.UsedImplicitly]
    public class SystemLogger : ISystemLogger
    {
        public ILogger Logger { get; set; }

        public bool IsLogEnabled { get; set; } = true;
        public LogType FilterLogType { get; set; } = LogType.Exception;
        public Dictionary<string, bool> LogTags { get; set; } = new();

        [UnityEngine.Scripting.RequiredMember]
        public SystemLogger(ILogger logger)
        {
            Logger = logger;
            
            if (Debug.isDebugBuild || Application.isEditor)
            {
                FilterLogType = LogType.Log;    
            }
        }

        /// <summary>
        /// Logs a message with a specific log type and tag.
        /// </summary>
        /// <param name="logType">The type of the log message. This can be used to filter log messages.</param>
        /// <param name="tag">The tag associated with the log message. This can be used to categorize log messages.</param>
        /// <param name="message">The message to log.</param>
        public void Log(LogType logType, string tag, object message)
        {
            if (IsLogAllowed(logType, tag))
            {
                Logger.Log(logType, tag, message);
            }
        }

        /// <summary>
        /// Logs a formatted message with a specific log type and tag.
        /// </summary>
        /// <param name="logType">The type of the log message. This can be used to filter log messages.</param>
        /// <param name="tag">The tag associated with the log message. This can be used to categorize log messages.</param>
        /// <param name="format">A composite format string that contains text intermixed with zero or more format items, which correspond to objects in the args array.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public void LogFormat(LogType logType, string tag, string format, params object[] args)
        {
            if (IsLogAllowed(logType, tag))
            {
                Logger.LogFormat(logType, tag + ": " + format, args);
            }
        }

        /// <summary>
        /// Logs an exception.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        public void LogException(Exception exception)
        {
            if (IsLogAllowed(LogType.Exception, null))
            {
                Logger.LogException(exception);
            }
        }

        /// <summary>
        /// Determines whether a log message of a specific type and tag is allowed to be logged.
        /// </summary>
        /// <param name="logType">The type of the log message. This can be used to filter log messages.</param>
        /// <param name="tag">The tag associated with the log message. This can be used to categorize log messages.</param>
        /// <returns>True if the log message is allowed to be logged, false otherwise.</returns>
        public bool IsLogAllowed(LogType logType, string tag)
        {
            // If logging is disabled, do not log anything
            if (IsLogEnabled is false)
            {
                return false;
            }

            // Always allow exceptions to be logged
            if (logType == LogType.Exception)
            {
                return true;
            }

            // Allow the log message if its type is less than or equal to the filter log type
            // and if the tag is not in the log tags dictionary or if it is and its value is true
            return logType <= FilterLogType && (LogTags == null || LogTags.ContainsKey(tag) is false || LogTags[tag]);
        }
    }
}