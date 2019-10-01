using System;
using System.Reflection;
using Sara.Logging;

namespace Sara.LogReader.Service.Core.Pattern
{
    public static class Common
    {
        /// <summary>
        /// If the message contains the search string, the value after the search string is returned as a double in the result parameter
        /// </summary>
        public static bool GetDoubleFromLog(string message, string search, ref double result)
        {
            try
            {
                if (!message.Contains(search))
                    return false;

                double value;
                if (
                    double.TryParse(
                        message.Substring(message.IndexOf(search, StringComparison.Ordinal) + search.Length),
                        out value))
                    result = value;
                else result = -0.1;
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteError(typeof(Common).FullName, MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        /// <summary>
        /// If the message contains the search string, the value after the search string is returned in the result
        /// </summary>
        public static bool GetStringFromLog(string message, string search, ref string result)
        {
            try
            {
                if (!message.Contains(search))
                    return false;

                result = message.Substring(message.IndexOf(search, StringComparison.Ordinal) + search.Length); 
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteError(typeof(Common).FullName, MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public static string EscapeFormat(string value)
        {
            value = value.Replace("{", "{{");
            value = value.Replace("}", "}}");
            return value;
        }
    }
}