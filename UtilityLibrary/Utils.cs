using System;
using System.Collections.Generic;
using System.Text;
using NetScriptFramework;
using NetScriptFramework.SkyrimSE;
using NetScriptFramework.Tools;
using Main = NetScriptFramework.Main;

namespace UtilityLibrary
{
    internal static class Utils
    {
        private static readonly LogFile LogFile;

        static Utils()
        {
            //optional to also use LogFileFlags.IncludeTimestampInFileName
            LogFile = new LogFile(Plugin.PluginName, LogFileFlags.AppendFile | LogFileFlags.AutoFlush | LogFileFlags.IncludeTimestampInLine);
        }

        internal static void Log(string s)
        {
            LogFile.AppendLine(s);
            //uncomment if you want to display the log message on the HUD, useful when for debugging
            //NetScriptFramework.SkyrimSE.MenuManager.ShowHUDMessage(s, null, true);
        }

        internal static void Do<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable) action(item);
        }

        internal static string ToDebugString(this IMemoryObject obj)
        {
            return obj.TypeInfo.Info.Name;
        }

        internal static string ToDebugString(this TESForm form)
        {
            return form == null ? "Form is null!" : $"Form: {form.FormType} Name: {form.Name}";
        }

        internal static string ToDebugString(this CPURegisters ctx)
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            try
            {
                sb.AppendLine($"AX: {ctx.AX}");
                sb.AppendLine($"BX: {ctx.BX}");
                sb.AppendLine($"CX: {ctx.CX}");
                sb.AppendLine($"DX: {ctx.DX}");
                sb.AppendLine($"DI: {ctx.DI}");
                sb.AppendLine($"SI: {ctx.SI}");
                sb.AppendLine($"BP: {ctx.BP}");
                sb.AppendLine($"SP: {ctx.SP}");
                sb.AppendLine($"FLAGS: {ctx.FLAGS}");
                sb.AppendLine($"IP: {ctx.IP}");
                sb.AppendLine($"Hook: {ctx.Hook}");

                sb.AppendLine($"XMM0f: {ctx.XMM0f}");
                sb.AppendLine($"XMM1f: {ctx.XMM1f}");
                sb.AppendLine($"XMM2f: {ctx.XMM2f}");
                sb.AppendLine($"XMM3f: {ctx.XMM3f}");
                sb.AppendLine($"XMM4f: {ctx.XMM4f}");
                sb.AppendLine($"XMM5f: {ctx.XMM5f}");
                sb.AppendLine($"XMM6f: {ctx.XMM6f}");
                sb.AppendLine($"XMM7f: {ctx.XMM7f}");
                sb.AppendLine($"XMM8f: {ctx.XMM8f}");
                sb.AppendLine($"XMM9f: {ctx.XMM9f}");
                sb.AppendLine($"XMM10f: {ctx.XMM10f}");
                sb.AppendLine($"XMM11f: {ctx.XMM11f}");
                sb.AppendLine($"XMM12f: {ctx.XMM12f}");
                sb.AppendLine($"XMM13f: {ctx.XMM13f}");
                sb.AppendLine($"XMM14f: {ctx.XMM14f}");
                sb.AppendLine($"XMM15f: {ctx.XMM15f}");

                sb.AppendLine($"XMM0: {ctx.XMM0}");
                sb.AppendLine($"XMM1: {ctx.XMM1}");
                sb.AppendLine($"XMM2: {ctx.XMM2}");
                sb.AppendLine($"XMM3: {ctx.XMM3}");
                sb.AppendLine($"XMM4: {ctx.XMM4}");
                sb.AppendLine($"XMM5: {ctx.XMM5}");
                sb.AppendLine($"XMM6: {ctx.XMM6}");
                sb.AppendLine($"XMM7: {ctx.XMM7}");
                sb.AppendLine($"XMM8: {ctx.XMM8}");
                sb.AppendLine($"XMM9: {ctx.XMM9}");
                sb.AppendLine($"XMM10: {ctx.XMM10}");
                sb.AppendLine($"XMM11: {ctx.XMM11}");
                sb.AppendLine($"XMM12: {ctx.XMM12}");
                sb.AppendLine($"XMM13: {ctx.XMM13}");
                sb.AppendLine($"XMM14: {ctx.XMM14}");
                sb.AppendLine($"XMM15: {ctx.XMM15}");

                sb.AppendLine($"R8: {ctx.R8}");
                sb.AppendLine($"R9: {ctx.R9}");
                sb.AppendLine($"R10: {ctx.R10}");
                sb.AppendLine($"R11: {ctx.R11}");
                sb.AppendLine($"R12: {ctx.R12}");
                sb.AppendLine($"R13: {ctx.R13}");
                sb.AppendLine($"R14: {ctx.R14}");
                sb.AppendLine($"R15: {ctx.R15}");

                sb.AppendLine($"Depth: {ctx.Depth}");

                if (Main.Is64Bit)
                {
                    sb.AppendLine($"STCount: {ctx.STCount}");
                    sb.AppendLine($"ST0: {ctx.ST0}");
                    sb.AppendLine($"ST1: {ctx.ST1}");
                    sb.AppendLine($"ST2: {ctx.ST2}");
                    sb.AppendLine($"ST3: {ctx.ST3}");
                    sb.AppendLine($"ST4: {ctx.ST4}");
                    sb.AppendLine($"ST5: {ctx.ST5}");
                    sb.AppendLine($"ST6: {ctx.ST6}");
                    sb.AppendLine($"ST7: {ctx.ST7}");
                }
            }
            catch (Exception e)
            {
                Log($"Exception while reading something: {e}");
            }

            return sb.ToString();
        }
    }
}
