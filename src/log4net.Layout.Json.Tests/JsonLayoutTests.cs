using System;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace log4net.Layout.Json.Tests
{
    public class JsonLayoutTests
    {
        [Fact]
        public void ShouldCaptureBasicMessage()
        {
            var repositoryId = Guid.NewGuid().ToString();
            var hierarchy = LogManager.CreateRepository(repositoryId) as Hierarchy;
            var appender = new TestAppender
            {
                Layout = new JsonLayout()
            };
            hierarchy.Root.AddAppender(appender);
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
            var log = LogManager.GetLogger(repositoryId, nameof(JsonLayoutTests));
            log.Info("DummyText");
            //            log.Info(new {IntProperty = 123, StringProperty = "ABC", DateTimeProperty = new DateTime(2000, 1, 2)});
            //          log.Error("", new Exception("Dummy message"));

            dynamic obj = JsonConvert.DeserializeObject(appender.RecordedMessages[0]);
            Assert.Matches("[0-9a-z\\-]+", obj["processSessionId"].ToString());
        }
    }
}
