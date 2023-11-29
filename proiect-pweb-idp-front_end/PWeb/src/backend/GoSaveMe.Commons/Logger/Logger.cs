using NLog;
using NLog.Config;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace GoSaveMe.Commons.Logger
{
    public class Logger : ILogger
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static NLog.Logger _log;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private readonly string _logSection;

        public Logger(string logSection)
        {
            _log = LogManager.GetCurrentClassLogger();

            _logSection = logSection.Trim();
        }

        public Logger(string logName, string logSection)
        {
            _log = LogManager.GetLogger(string.IsNullOrEmpty(logName) ? "App" : logName.Trim());

            _logSection = logSection.Trim();
        }

        public void Shutdown()
        {
            try
            {
                LogManager.Configuration = null;
            }
            catch (Exception)
            {
            }
        }

        private void Log(LogLevel level, string? method = null, string? message = null, params object[] args) => Log(level, method, null, message, args);

        private void Log(
          LogLevel level,
          string? method = null,
          Exception? exception = null,
          string? message = null,
          params object[] args)
        {
            if (exception != null && !_log.IsErrorEnabled)
                return;

            LogEventInfo logEventInfo = new(level, _log.Name, CultureInfo.CurrentUICulture, message, args, exception);

            method = (string.IsNullOrWhiteSpace(_logSection) ? "" : _logSection) + "." + method;
            logEventInfo.Properties[nameof(method)] = method;
            logEventInfo.Properties["callsite"] = method;

            _log.Log(logEventInfo);
        }

        public void SessionLog(string? session, string? username, string? message = null) => SessionLog(LogLevel.Trace, session, username, message);

        public void SessionLog(
          LogLevel level,
          string? session,
          string? username,
          string? message = null,
          params object[] args)
        {
            if (string.IsNullOrWhiteSpace(session))
                return;

            _log.Log(new LogEventInfo(level, _log.Name, CultureInfo.CurrentUICulture, message, args)
            {
                Properties = {
                      [nameof (session)] = session,
                      ["user"] = string.IsNullOrWhiteSpace(username) ?  "?" : username
                    }
            });
        }

        private string GetCallingMethodName(MethodBase? currentMethod)
        {
            string str = string.Empty;

            try
            {
                StackTrace stackTrace = new(true);

                for (int index = 0; index < stackTrace.FrameCount; ++index)
                {
                    StackFrame? frame = stackTrace.GetFrame(index);

                    if (frame == null) continue;

                    if (frame.GetMethod() == currentMethod)
                    {
                        if (index + 1 <= stackTrace.FrameCount)
                        {
                            StackFrame? nextFrame = stackTrace.GetFrame(index + 1);

                            if (nextFrame == null) continue;

                            MethodBase? method = nextFrame.GetMethod();

                            if (method == null) continue;

                            str = method.ReflectedType?.ToString() + "." + method.Name;

                            break;
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {
                return "-NULL-METHOD-NAME-";
            }

            return str;
        }

        public void Exception(
          string? method = null,
          Exception? exception = null,
          string? message = null,
          params object[] args)
        {
            if (!_log.IsErrorEnabled)
                return;

            if (string.IsNullOrEmpty(method))
                method = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Error, method, exception, message, args);
        }

        public void Exception(Exception? exception = null, string? message = null, params object[] args)
        {
            if (!_log.IsErrorEnabled)
                return;

            string callingMethodName = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Error, callingMethodName, exception, message, args);
        }

        public void Error(string? message = null, params object[] args)
        {
            if (!_log.IsErrorEnabled)
                return;

            string callingMethodName = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Error, callingMethodName, null, message, args);
        }

        public void Error(string? method = null, string? message = null, params object[] args)
        {
            if (!_log.IsErrorEnabled)
                return;

            if (string.IsNullOrEmpty(method))
                method = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Error, method, null, message, args);
        }

        public void Fatal(string? message = null, params object[] args)
        {
            if (!_log.IsErrorEnabled)
                return;

            string callingMethodName = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Fatal, callingMethodName, null, message, args);
        }

        public void Fatal(string? method = null, string? message = null, params object[] args)
        {
            if (!_log.IsErrorEnabled)
                return;

            if (string.IsNullOrEmpty(method))
                method = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Fatal, method, null, message, args);
        }

        public void Warn(string? message = null, params object[] args)
        {
            if (!_log.IsWarnEnabled)
                return;

            string callingMethodName = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Warn, callingMethodName, null, message, args);
        }

        public void Warn(string? method = null, string? message = null, params object[] args)
        {
            if (!_log.IsWarnEnabled)
                return;

            if (string.IsNullOrEmpty(method))
                method = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Warn, method, null, message, args);
        }

        public void Info(string? message = null, params object[] args)
        {
            if (!_log.IsInfoEnabled)
                return;

            string callingMethodName = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Info, callingMethodName, null, message, args);
        }

        public void Info(string? method = null, string? message = null, params object[] args)
        {
            if (!_log.IsInfoEnabled)
                return;

            if (string.IsNullOrEmpty(method))
                method = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Info, method, null, message, args);
        }

        public void Debug(string? message = null, params object[] args)
        {
            if (!_log.IsDebugEnabled)
                return;

            string callingMethodName = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Debug, callingMethodName, null, message, args);
        }

        public void Debug(string? method = null, string? message = null, params object[] args)
        {
            if (!_log.IsDebugEnabled)
                return;

            if (string.IsNullOrEmpty(method))
                method = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Debug, method, null, message, args);
        }

        public void Trace(string? message = null, params object[] args)
        {
            if (!_log.IsDebugEnabled)
                return;

            string callingMethodName = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Trace, callingMethodName, null, message, args);
        }

        public void Trace(string? method = null, string? message = null, params object[] args)
        {
            if (!_log.IsDebugEnabled)
                return;

            if (string.IsNullOrEmpty(method))
                method = GetCallingMethodName(MethodBase.GetCurrentMethod());

            Log(LogLevel.Trace, method, null, message, args);
        }
    }
}
