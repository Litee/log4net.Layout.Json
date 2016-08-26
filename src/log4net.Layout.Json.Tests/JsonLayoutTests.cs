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
            Assert.NotNull(hierarchy);
            var appender = new TestAppender
            {
                Layout = new JsonLayout()
            };
            hierarchy.Root.AddAppender(appender);
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
            var log = LogManager.GetLogger(repositoryId, nameof(JsonLayoutTests));
            log.Info("DummyText");

            dynamic obj = JsonConvert.DeserializeObject(appender.RecordedMessages[0]);
            Assert.Matches("[0-9a-z\\-]+", obj["processSessionId"].ToString());
        }

        [Fact]
        public void ShouldCaptureGlobalContext()
        {
            var repositoryId = Guid.NewGuid().ToString();
            var hierarchy = LogManager.CreateRepository(repositoryId) as Hierarchy;
            Assert.NotNull(hierarchy);
            var appender = new TestAppender
            {
                Layout = new JsonLayout()
            };
            hierarchy.Root.AddAppender(appender);
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
            var log = LogManager.GetLogger(repositoryId, nameof(JsonLayoutTests));
            GlobalContext.Properties["key"] = "value";
            log.Info("Message");

            dynamic obj = JsonConvert.DeserializeObject(appender.RecordedMessages[0]);
            var value = (string)obj["properties"]["key"];
            Assert.True("value".Equals( value));
            Assert.Matches("[0-9a-z\\-]+", obj["processSessionId"].ToString());
            var renderedMessage = (string) obj["renderedMessage"];
            Assert.True("Message".Equals(renderedMessage));
        }
    }
}
