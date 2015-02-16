// Console WebAPI by Mozilla Contributors is licensed under CC-BY-SA 2.5.
// https://developer.mozilla.org/en-US/docs/Web/API/Console

using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The console object provides access to the browser's debugging console (e.g. the Web Console in Firefox). The specifics of how it works vary from browser to browser, but there is a de facto set of features that are typically provided.
    /// </summary>
    [Ignore]
    [Name("console")]
    public static class Console
    {
        [Name("log")]
        public static void WriteLine(string message)
        {
        }

        [Template("console.log(Bridge.String.format({message}, {args}))")]
        public static void WriteLine(string message, params object[] args)
        {
        }

        /// <summary>
        /// Log a message and stack trace to console if first argument is false.
        /// </summary>
        /// <param name="expression">If false then message is shown</param>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        /// <param name="args">JavaScript objects with which to replace substitution strings within msg. This gives you additional control over the format of the output.</param>
        public static void Assert(bool expression, object message, params object[] args)
        {
        }

        /// <summary>
        /// Log a message and stack trace to console if first argument is false.
        /// </summary>
        /// <param name="expression">If false then message is shown</param>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        public static void Assert(bool expression, object message)
        {
        }

        /// <summary>
        /// Log a message and stack trace to console if first argument is false.
        /// </summary>
        /// <param name="expression">If false then message is shown</param>
        public static void Assert(bool expression)
        {
        }

        /// <summary>
        /// Outputs a message to the Web Console.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        public static void Log(object message)
        {
        }

        /// <summary>
        /// Outputs a message to the Web Console.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        /// <param name="args">JavaScript objects with which to replace substitution strings within msg. This gives you additional control over the format of the output.</param>
        public static void Log(object message, params object[] args)
        {
        }

        /// <summary>
        /// An alias for log(); this was added to improve compatibility with existing sites already using debug(). However, you should use console.log() instead.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        public static void Debug(object message)
        {
        }

        /// <summary>
        /// An alias for log(); this was added to improve compatibility with existing sites already using debug(). However, you should use console.log() instead.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        /// <param name="args"></param>
        public static void Debug(object message, params object[] args)
        {
        }

        /// <summary>
        /// Outputs an informational message to the Web Console. In Firefox, a small "i" icon is displayed next to these items in the Web Console's log.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        public static void Info(object message)
        {
        }

        /// <summary>
        /// Outputs an informational message to the Web Console. In Firefox, a small "i" icon is displayed next to these items in the Web Console's log.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        /// <param name="args">JavaScript objects with which to replace substitution strings within msg. This gives you additional control over the format of the output.</param>
        public static void Info(object message, params object[] args)
        {
        }

        /// <summary>
        /// Outputs a warning message to the Web Console.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        public static void Warn(object message)
        {
        }

        /// <summary>
        /// Outputs a warning message to the Web Console.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        /// <param name="args">JavaScript objects with which to replace substitution strings within msg. This gives you additional control over the format of the output.</param>
        public static void Warn(object message, params object[] args)
        {
        }

        /// <summary>
        /// Outputs an error message to the Web Console.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        public static void Error(object message)
        {
        }

        /// <summary>
        /// Outputs an error message to the Web Console.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        /// <param name="args">JavaScript objects with which to replace substitution strings within msg. This gives you additional control over the format of the output.</param>
        public static void Error(object message, params object[] args)
        {
        }

        /// <summary>
        /// Clears the console.
        /// </summary>
        public static void Clear()
        {
        }

        /// <summary>
        /// Outputs a stack trace. 
        /// </summary>
        public static void Trace()
        {
        }

        /// <summary>
        /// Creates a new inline group, indenting all following output by another level. To move back out a level, call groupEnd().
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        public static void Group(object message)
        {
        }

        /// <summary>
        /// Creates a new inline group, indenting all following output by another level. To move back out a level, call groupEnd().
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        /// <param name="args">JavaScript objects with which to replace substitution strings within msg.</param>
        public static void Group(object message, params object[] args)
        {
        }

        /// <summary>
        /// Creates a new inline group, indenting all following output by another level; unlike group(), this starts with the inline group collapsed, requiring the use of a disclosure button to expand it. To move back out a level, call groupEnd(). See Using groups in the console.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        public static void GroupCollapsed(object message)
        {
        }

        /// <summary>
        /// Creates a new inline group, indenting all following output by another level; unlike group(), this starts with the inline group collapsed, requiring the use of a disclosure button to expand it. To move back out a level, call groupEnd(). See Using groups in the console.
        /// </summary>
        /// <param name="message">A JavaScript string containing zero or more substitution strings.</param>
        /// <param name="args">JavaScript objects with which to replace substitution strings within msg.</param>
        public static void GroupCollapsed(object message, params object[] args)
        {
        }

        /// <summary>
        /// Exits the current inline group.
        /// </summary>
        public static void GroupEnd()
        {
        }

        /// <summary>
        /// Starts a timer with a name specified as an input parameter. Up to 10,000 simultaneous timers can run on a given page.
        /// </summary>
        /// <param name="name">The name to give the new timer. This will identify the timer; use the same name when calling console.timeEnd() to stop the timer and get the time output to the console.</param>
        public static void Time(object name)
        {
        }

        /// <summary>
        /// Stops the specified timer and logs the elapsed time in seconds since its start. 
        /// </summary>
        /// <param name="name">The name of the timer to stop. Once stopped, the elapsed time is automatically displayed in the Web Console.</param>
        public static void TimeEnd(object name)
        {
        }

        /// <summary>
        /// This method adds an event to the Timeline during a recording session. This lets you visually correlate your code generated time stamp to other events, such as screen layout and paints, that are automatically added to the Timeline.
        /// </summary>
        /// <param name="name">Stamp name</param>
        public static void TimeStamp(object name)
        {
        }

        /// <summary>
        /// Starts the JavaScript profiler. You can specify an optional label for the profile.
        /// </summary>
        public static void Profile()
        {
        }

        /// <summary>
        /// Starts the JavaScript profiler. You can specify an optional label for the profile.
        /// </summary>
        /// <param name="profileLabel"></param>
        public static void Profile(string profileLabel)
        {
        }

        /// <summary>
        /// Stops the profiler.
        /// </summary>
        public static void ProfileEnd()
        {
        }

        /// <summary>
        /// Logs the number of times that this particular call to count() has been called. This function takes an optional argument label.
        /// If label is supplied, this function logs the number of times count() has been called with that particular label.
        /// If label is omitted, the function logs the number of times count() has been called at this particular line.
        /// </summary>
        public static void Count()
        {
        }

        /// <summary>
        /// Logs the number of times that this particular call to count() has been called. This function takes an optional argument label.
        /// If label is supplied, this function logs the number of times count() has been called with that particular label.
        /// If label is omitted, the function logs the number of times count() has been called at this particular line.
        /// </summary>
        /// <param name="label">Label value</param>
        public static void Count(string label)
        {
        }

        /// <summary>
        /// Displays an interactive listing of the properties of a specified JavaScript object. This listing lets you use disclosure triangles to examine the contents of child objects.
        /// </summary>
        /// <param name="obj">A JavaScript object whose properties should be output.</param>
        public static void Dir(object obj)
        {
        }
    }
}
