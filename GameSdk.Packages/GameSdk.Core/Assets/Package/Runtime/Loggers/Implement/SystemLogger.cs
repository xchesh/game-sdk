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

        public void Log(LogType logType, string tag, object message)
        {
            if (IsLogAllowed(logType, tag))
            {
                Logger.Log(logType, tag, message);
            }
        }


        public void Log(LogType logType, string tag, Func<string> messageFn)
        {
            if (IsLogAllowed(logType, tag))
            {
                Logger.Log(logType, tag, messageFn?.Invoke());
            }
        }

        public void LogFormat(LogType logType, string tag, string format, params object[] args)
        {
            if (IsLogAllowed(logType, tag))
            {
                Logger.LogFormat(logType, tag + ": " + format, args);
            }
        }

        public void LogFormat(LogType logType, string tag, Func<string> formatFn, params object[] args)
        {
            if (IsLogAllowed(logType, tag))
            {
                Logger.LogFormat(logType, tag + ": " + formatFn?.Invoke(), args);
            }
        }

        public void LogException(Exception exception)
        {
            if (IsLogAllowed(LogType.Exception, null))
            {
                Logger.LogException(exception);
            }
        }

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
