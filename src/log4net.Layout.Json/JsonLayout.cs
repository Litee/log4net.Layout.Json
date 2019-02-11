﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using log4net.Core;
using Newtonsoft.Json;

namespace log4net.Layout.Json
{
    public class JsonLayout : LayoutSkeleton
    {

        static readonly string ProcessSessionId = Guid.NewGuid().ToString();
        static readonly int ProcessId = Process.GetCurrentProcess().Id;
        static readonly string MachineName = Environment.MachineName;

        public override void ActivateOptions()
        {
        }

        /// <summary>
        /// IgnoresException. f this layout handles the exception object contained within LoggingEvent, then the layout should return false. Otherwise, if the layout ignores the exception object, then the layout should return true.
        /// </summary>
        public override bool IgnoresException
        {
            get
            {
                return false;
            }
        }

        public override void Format(TextWriter writer, LoggingEvent e)
        {
            var dic = new Dictionary<string, object>
            {
                ["processSessionId"] = ProcessSessionId,
                ["level"] = e.Level.DisplayName,
                ["messageObject"] = e.MessageObject,
                ["renderedMessage"] = e.RenderedMessage,
                ["timestampUtc"] = e.TimeStamp.ToUniversalTime().ToString("O"),
                ["logger"] = e.LoggerName,
                ["thread"] = e.ThreadName,
                ["exceptionObject"] = e.ExceptionObject,
                ["exceptionObjectString"] = e.ExceptionObject == null ? null : e.GetExceptionString(),
                ["userName"] = e.UserName,
                ["domain"] = e.Domain,
                ["identity"] = e.Identity,
                ["location"] = e.LocationInformation.FullInfo,
                ["pid"] = ProcessId,
                ["machineName"] = MachineName,
                ["workingSet"] = Environment.WorkingSet,
                ["osVersion"] = Environment.OSVersion.ToString(),
                ["is64bitOS"] = Environment.Is64BitOperatingSystem,
                ["is64bitProcess"] = Environment.Is64BitProcess,
                ["properties"] = e.GetProperties()
            };
            writer.Write(JsonConvert.SerializeObject(dic));
        }

    }
}
