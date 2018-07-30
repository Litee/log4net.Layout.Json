# log4net.Layout.Json
Very simple implementation of log4net Layout that outputs JSON. Can be used, for example, to push logs into Logstash.

# Usage
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <log4net>
	    <appender name="ConsoleAppender" type="log4net.Appender.ConsoleAppender">
		    <layout type="log4net.Layout.PatternLayout">
			    <conversionPattern value="%date [%thread] %-5level %logger - %message%newline" />
		    </layout>
        </appender>
        <appender name="FileAppender" type="log4net.Appender.RollingFileAppender">
            <param name="File" value="log/log" />
            <param name="AppendToFile" value="true" />
            <param name="DatePattern" value="_yyyyMMddHH&quot;.log&quot;" />
            <param name="RollingStyle" value="Date" />
            <param name="StaticLogFileName" value="false" />
            <layout type="log4net.Layout.Json.JsonLayout"></layout>
        </appender>
        <root>
            <level value="DEBUG"/>
            <appender-ref ref="FileAppender"/>
	        <appender-ref ref="ConsoleAppender"/>
        </root>
    </log4net>
</configuration>
```
