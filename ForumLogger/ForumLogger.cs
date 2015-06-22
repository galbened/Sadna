﻿using System;
using System.IO;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace ForumLoggers
{

    /**
     * Logger class is singeltone 
     * use ForumLogger.GetInstance() to get the instance if the logger
     * use ForumLogger.GetInstance().Write(string type, string line) to write messages to logger
     * the type argument is 3 option field: ForumLogger.TYPE_INFO, ForumLogger.TYPE_ERROR, ForumLogger.TYPE_WARNING
     * the last to use the logger (the Driver class probably) should call ForumLogger.GetInstance().Shutdown();
     **/
    public class ForumLogger : Logger
    {
        /// <summary> 
        /// Initialize is guaranteed to be called by MSBuild at the start of the build 
        /// before any events are raised. 
        /// </summary> 
        public override void Initialize(IEventSource eventSource)
        {
            // The name of the log file should be passed as the first item in the 
            // "parameters" specification in the /logger switch.  It is required
            // to pass a log file to this logger. Other loggers may have zero or more than  
            // one parameters. 
            if (null == Parameters)
            {
                throw new LoggerException("Log file was not set.");
            }
            string[] parameters = Parameters.Split(';');

            string logFile = parameters[0];
            if (String.IsNullOrEmpty(logFile))
            {
                throw new LoggerException("Log file was not set.");
            }

            if (parameters.Length > 1)
            {
                throw new LoggerException("Too many parameters passed.");
            }

            try
            {
                // Open the file 
                this.streamWriter = new StreamWriter(logFile);
            }
            catch (Exception ex)
            {
                if
                (
                    ex is UnauthorizedAccessException
                    || ex is ArgumentNullException
                    || ex is PathTooLongException
                    || ex is DirectoryNotFoundException
                    || ex is NotSupportedException
                    || ex is ArgumentException
                    || ex is SecurityException
                    || ex is IOException
                )
                {
                    throw new LoggerException("Failed to create log file: " + ex.Message);
                }
                else
                {
                    // Unexpected failure 
                    throw;
                }
            }

            // For brevity, we'll only register for certain event types. Loggers can also 
            // register to handle TargetStarted/Finished and other events.
            eventSource.ProjectStarted += new ProjectStartedEventHandler(eventSource_ProjectStarted);
            eventSource.TaskStarted += new TaskStartedEventHandler(eventSource_TaskStarted);
            eventSource.MessageRaised += new BuildMessageEventHandler(eventSource_MessageRaised);
            eventSource.WarningRaised += new BuildWarningEventHandler(eventSource_WarningRaised);
            eventSource.ErrorRaised += new BuildErrorEventHandler(eventSource_ErrorRaised);
            eventSource.ProjectFinished += new ProjectFinishedEventHandler(eventSource_ProjectFinished);
        }

        public  void Initialize()
        {
            // The name of the log file should be passed as the first item in the 
            // "parameters" specification in the /logger switch.  It is required
            // to pass a log file to this logger. Other loggers may have zero or more than  
            // one parameters. 
            if (null == Parameters)
            {
                throw new LoggerException("Log file was not set.");
            }
            string[] parameters = Parameters.Split(';');

            string logFile = parameters[0];
            if (String.IsNullOrEmpty(logFile))
            {
                throw new LoggerException("Log file was not set.");
            }

            if (parameters.Length > 1)
            {
                throw new LoggerException("Too many parameters passed.");
            }

            try
            {
                // Open the file 
                this.streamWriter = new StreamWriter(logFile);
            }
            catch (Exception ex)
            {
                if
                (
                    ex is UnauthorizedAccessException
                    || ex is ArgumentNullException
                    || ex is PathTooLongException
                    || ex is DirectoryNotFoundException
                    || ex is NotSupportedException
                    || ex is ArgumentException
                    || ex is SecurityException
                    || ex is IOException
                )
                {
                    throw new LoggerException("Failed to create log file: " + ex.Message);
                }
                else
                {
                    // Unexpected failure 
                    throw;
                }
            }
        }

        void eventSource_ErrorRaised(object sender, BuildErrorEventArgs e)
        {
            // BuildErrorEventArgs adds LineNumber, ColumnNumber, File, amongst other parameters 
            string line = String.Format(": ERROR {0}({1},{2}): ", e.File, e.LineNumber, e.ColumnNumber);
            WriteLineWithSenderAndMessage(line, e);
        }

        void eventSource_WarningRaised(object sender, BuildWarningEventArgs e)
        {
            // BuildWarningEventArgs adds LineNumber, ColumnNumber, File, amongst other parameters 
            string line = String.Format(": Warning {0}({1},{2}): ", e.File, e.LineNumber, e.ColumnNumber);
            WriteLineWithSenderAndMessage(line, e);
        }

        void eventSource_MessageRaised(object sender, BuildMessageEventArgs e)
        {
            // BuildMessageEventArgs adds Importance to BuildEventArgs 
            // Let's take account of the verbosity setting we've been passed in deciding whether to log the message 
            if ((e.Importance == MessageImportance.High && IsVerbosityAtLeast(LoggerVerbosity.Minimal))
                || (e.Importance == MessageImportance.Normal && IsVerbosityAtLeast(LoggerVerbosity.Normal))
                || (e.Importance == MessageImportance.Low && IsVerbosityAtLeast(LoggerVerbosity.Detailed))
                )
            {
                WriteLineWithSenderAndMessage(String.Empty, e);
            }
        }

        void eventSource_TaskStarted(object sender, TaskStartedEventArgs e)
        {
            // TaskStartedEventArgs adds ProjectFile, TaskFile, TaskName 
            // To keep this log clean, this logger will ignore these events.
        }

        void eventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
        {
            // ProjectStartedEventArgs adds ProjectFile, TargetNames 
            // Just the regular message string is good enough here, so just display that.
            WriteLine(String.Empty, e);
            indent++;
        }

        void eventSource_ProjectFinished(object sender, ProjectFinishedEventArgs e)
        {
            // The regular message string is good enough here too.
            indent--;
            WriteLine(String.Empty, e);
        }

        /// <summary> 
        /// Write a line to the log, adding the SenderName and Message 
        /// (these parameters are on all MSBuild event argument objects) 
        /// </summary> 
        private void WriteLineWithSenderAndMessage(string line, BuildEventArgs e)
        {
            if (0 == String.Compare(e.SenderName, "MSBuild", true /*ignore case*/))
            {
                // Well, if the sender name is MSBuild, let's leave it out for prettiness
                WriteLine(line, e);
            }
            else
            {
                WriteLine(e.SenderName + ": " + line, e);
            }
        }

        /// <summary> 
        /// Just write a line to the log 
        /// </summary> 
        private void WriteLine(string line, BuildEventArgs e)
        {
            for (int i = indent; i > 0; i--)
            {
                streamWriter.Write("\t");
            }
            streamWriter.WriteLine(line + e.Message);
        }

        public void Write(string type, string line)
        {
            for (int i = indent; i > 0; i--)
            {
                streamWriter.Write("\t");
            }
            streamWriter.WriteLine(string.Format("{0:M/d/yyyy HH:mm:ss tt}", DateTime.Now ) + ". Type: " +  type + ": " + line);
        }

        public void WriteNoDate(string type, string line)
        {
            for (int i = indent; i > 0; i--)
            {
                streamWriter.Write("\t");
            }
            streamWriter.WriteLine( type + line);
        }

        /// <summary> 
        /// Shutdown() is guaranteed to be called by MSBuild at the end of the build, after all  
        /// events have been raised. 
        /// </summary> 
        public override void Shutdown()
        {
            // Done logging, let go of the file
            streamWriter.Close();
        }

        public static ForumLogger GetInstance()
        {
            if (instance == null)
            {
                instance = new ForumLogger();
                instance.Parameters = "log.txt";
                instance.Initialize();
            }
            return instance;
        }

        private StreamWriter streamWriter;
        private int indent;
        private static ForumLogger instance = null;
        public const string TYPE_INFO = "INFO";
        public const string TYPE_ERROR = "ERROR";
        public const string TYPE_WARNING = "WARNING";
    }
}