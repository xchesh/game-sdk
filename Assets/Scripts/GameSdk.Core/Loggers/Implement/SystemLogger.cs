using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSdk.Core.Loggers
{
    public class SystemLogger : ISystemLogger
    {
        public ILogger Logger { get; set; }

        public bool IsLogEnabled { get; set; } = true;
        public LogType FilterLogType { get; set; } = LogType.Exception;
        public Dictionary<string, bool> LogTags { get; set; } = new();

        public SystemLogger(ILogger logger)
        {
            Logger = logger;
        }

        public void Log(LogType logType, string tag, object message)
        {
            if (IsLogAllowed(logType, tag))
            {
                Logger.Log(logType, tag, message);
            }
        }

        public void LogFormat(LogType logType, string tag, string format, params object[] args)
        {
            if (IsLogAllowed(logType, tag))
            {
                Logger.LogFormat(logType, tag + ": " + format, args);
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

            return logType <= FilterLogType && (LogTags == null || LogTags.ContainsKey(tag) is false || LogTags[tag]);
        }
    }
}
