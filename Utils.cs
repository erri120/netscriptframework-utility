using System;
using System.Collections.Generic;
using NetScriptFramework.Tools;

namespace PluginTemplate
{
    public static class Utils
    {
        private static readonly LogFile LogFile;

        static Utils()
        {
            //optional to also use LogFileFlags.IncludeTimestampInFileName
            LogFile = new LogFile(Plugin.PluginName, LogFileFlags.AppendFile | LogFileFlags.AutoFlush | LogFileFlags.IncludeTimestampInLine);
        }

        public static void Log(string s)
        {
            LogFile.AppendLine(s);
            //uncomment if you want to display the log message on the HUD, useful when for debugging
            //NetScriptFramework.SkyrimSE.MenuManager.ShowHUDMessage(s, null, true);
        }

        public static void Do<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable) action(item);
        }
    }
}
