using System.Collections.Generic;
using System.IO;
using log4net.Appender;
using log4net.Core;

namespace log4net.Layout.Json.Tests
{
    public class TestAppender : IAppender
    {
        public void Close()
        {
        }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            StringWriter writer = new StringWriter();
            if (Layout == null)
            {
                loggingEvent.WriteRenderedMessage(writer);
            }
            else
            {
                Layout.Format(writer, loggingEvent);
            }
            RecordedMessages.Add(writer.ToString());
        }

        public string Name { get; set; }

        public List<string> RecordedMessages { get; } = new List<string>();

        public JsonLayout Layout { get; set; }
    }
}