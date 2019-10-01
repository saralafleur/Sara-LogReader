using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sara.LogReader.Common
{
    public struct MatchRange
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Value { get; set; }
        public bool Success { get; set; }
    }

    // ReSharper disable once InconsistentNaming
    public class iLineMatch
    {
        // ReSharper disable once InconsistentNaming
        public int iLine { get; set; }
        public string Line { get; set; }
        public string Value { get; set; }
        public object model { get; set; }
    }

    public static class RegularExpression
    {
        /// <summary>
        /// Returns the text between two words
        /// </summary>
        public static string GetTextBetweenTwoWords(string firstWord, string secondWord, string value)
        {                                                              // .+?
            var match = Regex.Match(value, $@"(?s)(?<={firstWord}).*?(?={secondWord})", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : null;
        }

        /// <summary>
        /// Returns the text between two characters
        /// </summary>
        public static string GetTextBetweenTwoCharacters(string firstCharacter, string secondCharacter, string value)
        {
            if (value == null) return null;
            var match = Regex.Match(value, $@"(?<=\{firstCharacter})(.*?)(?=\{secondCharacter})", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : null;
        }

        /// <summary>
        /// Returns all text after the word
        /// </summary>
        /// <param name="firstWord"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEverythingAfterWord(string firstWord, string value)
        {
            var match = Regex.Match(value, $@"(?<={firstWord}).*", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : null;
        }

        /// <summary>
        /// Returns a DateTime using the Regular Expression provided
        /// </summary>
        public static DateTime? GetDateTime(string regularExpression, string value)
        {
            if (regularExpression == null) return null;

            var match = Regex.Match(value, regularExpression, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                if (match.Captures.Count == 0)
                    return null;
                if (DateTime.TryParse(match.Captures[0].Value, out DateTime test))
                    return test;
            }
            return null;
        }
        /// <summary>
        /// Returns the value found
        /// </summary>
        public static string GetValue(string regularExpression, string value)
        {
            if (regularExpression == null) return "";

            var match = Regex.Match(value, regularExpression, RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : "";
        }

        public static string ReplaceDateBetweenTwoWordsIncludingTheWords(string value, string firstWord, string secondWord, string replaceWord)
        {
            return Regex.Replace(value, $@"(?<=\{firstWord})([0-9][0-9]\/[0-9][0-9]\/.*?)(?=\{secondWord})", ".").Replace(
                $"{firstWord}.{secondWord}", replaceWord);
        }
        public static MatchRange FindDateBetweenTwoWordsIncludingTheWords(string value, string firstWord, string secondWord)
        {
            var match = Regex.Match(value, $@"(?<=\{firstWord})([0-9][0-9]\/[0-9][0-9]\/.*?)(?=\{secondWord})");
            if (match.Success)
                return new MatchRange
                {
                    Start = match.Index - 1,
                    End = match.Index + match.Length + 1,
                    Value = match.Value,
                    Success = true
                };
            return new MatchRange();
        }
        public static MatchRange FindEverythingBetweenTwoWordsIncludingTheWords(string value, string firstWord, string secondWord)
        {
            var match = Regex.Match(value, $@"(?<=\{firstWord})(.*?)(?=\{secondWord})");
            if (match.Success)
                return new MatchRange
                {
                    Start = match.Index - 1,
                    End = match.Index + match.Length + 1,
                    Value = match.Value,
                    Success = true
                };
            return new MatchRange();
        }
        public static MatchRange FindNumberBetweenTwoWordsIncludingTheWords(string value, string firstWord, string secondWord)
        {
            var match = Regex.Match(value, $@"(?<=\{firstWord})(\d*?)(?=\{secondWord})");
            if (match.Success)
                return new MatchRange
                {
                    Start = match.Index - 1,
                    End = match.Index + match.Length + 1,
                    Value = match.Value,
                    Success = true
                };
            return new MatchRange();
        }
        public static MatchRange FindEverythingBetweenTwoWordsIncludingTheFirstWords(string value, string firstWord, string secondWord)
        {
            //var match = Regex.Match(value, string.Format(@"(?s)(?<={0})[A-Za-z\t ._]+?(?={1})", Regex.Escape(firstWord), Regex.Escape(secondWord)));
            var match = Regex.Match(value, $@"(?<={Regex.Escape(firstWord)})(.*?)(?={Regex.Escape(secondWord)})");
            if (match.Success)
                return new MatchRange
                {
                    Start = match.Index - firstWord.Length,
                    End = match.Index + match.Length,
                    Value = match.Value,
                    Success = true
                };
            return new MatchRange();
        }
        public static string ReplaceEverythingBetweenTwoWordsIncludingTheWords(string value, string firstWord, string secondWord, string replaceWord)
        {
            return Regex.Replace(value, $@"(?<=\{firstWord})(.*?)(?=\{secondWord})", ".").Replace(
                $"{firstWord}.{secondWord}", replaceWord);
        }
        public static string ReplaceEveryNumberBetweenTwoWordsIncludingTheWords(string value, string firstWord, string secondWord, string replaceWord)
        {
            return Regex.Replace(value, $@"(?<=\{firstWord})(\d*?)(?=\{secondWord})", ".").Replace(
                $"{firstWord}.{secondWord}", replaceWord);
        }

        public static string ReplaceEverythingBetweenTwoWordsIncludingTheFirstWords(string value, string firstWord, string secondWord, string replaceWord)
        {
            return Regex.Replace(value, $@"(?s)(?<={firstWord})[A-Za-z\t ._]+?(?={secondWord})", ".").Replace(
                $"{firstWord}.{secondWord}", $"{replaceWord}{secondWord.TrimStart()}");
        }

        public static bool HasClass(string line, string @class)
        {
            return Regex.IsMatch(line, $@"(Class: {@class})");
        }

        public static bool HasMethod(string line, string method)
        {
            return Regex.IsMatch(line, $@"(Method: {method})");
        }

        /// <summary>
        /// Find the method in the line provided
        /// </summary>
        public static string GetMethod(string line)
        {
            return GetTextBetweenTwoWords("Method: ", " ", line);
        }

        /// <summary>
        /// Find the Class in the line provided
        /// </summary>
        public static string GetClass(string line)
        {
            return GetTextBetweenTwoWords("Class: ", " ", line);
        }

        public static string FindMatch(string value, string expression)
        {
            if (expression == null || String.IsNullOrEmpty(expression))
                return null;
            try
            {
                var match = Regex.Match(value, expression);
                return match.Value;
            }
            // ReSharper disable EmptyGeneralCatchClause

            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
                return null;
            }
        }
        public static bool HasMatch(string value, string expression)
        {
            if (expression == null || String.IsNullOrEmpty(expression))
                return false;
            try
            {
                var match = Regex.Match(value, expression, RegexOptions.IgnoreCase);
                return match.Success;
            }
            // ReSharper disable EmptyGeneralCatchClause

            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
                return false;
            }
        }
        // filters control characters but allows only properly-formed surrogate sequences
        private static readonly Regex InvalidXMLChars = new Regex(
            @"(?<![\uD800-\uDBFF])[\uDC00-\uDFFF]|[\uD800-\uDBFF](?![\uDC00-\uDFFF])|[\x00-\x08\x0B\x0C\x0E-\x1F\x7F-\x9F\uFEFF\uFFFE\uFFFF]",
            RegexOptions.Compiled);

        /// <summary>
        /// removes any unusual unicode characters that can't be encoded into XML
        /// </summary>
        public static string RemoveInvalidXMLChars(this string text)
        {
            return String.IsNullOrEmpty(text) ? "" : InvalidXMLChars.Replace(text, "");
        }

        public static string GetDateToString(string line)
        {
            return GetTextBetweenTwoCharacters("<", ">", line);
        }

        public static Match GetFirstMatch(string line, string expression)
        {
            return Regex.Match(line, expression, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Returns the first Match value
        /// </summary>
        public static string GetFirstValue(string line, string expression)
        {

            var match = GetFirstMatch(line, expression);
            return match.Success ? match.Groups.Count > 1 ? match.Groups[1].Value : match.Value : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Only use this for a one time call, if you are running a group then use GetLines
        /// Performance Issues !!!
        /// </remarks>
        public static int GetLine(string value, Capture match)
        {
            var count = 0;
            var length = match.Index + match.Length - 1;
            for (var x = 0; x < length; x++)
            {
                if (value[x] == '\n')
                    count += 1;
            }
            return count;
        }

        public static MatchCollection GetMatches(string value, string expression)
        {
            return Regex.Matches(value, expression, RegexOptions.IgnoreCase);
        }
        public static List<int> NewLines(string value)
        {
            var result = new List<int>();
            var matches = Regex.Matches(value, "\n", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                result.Add(match.Index);
            }
            return result;
        }
        /// <summary>
        /// Returns all Matches for the given expression
        /// </summary>
        public static List<iLineMatch> GetLines(string expression,
                                                string text,
                                                object model = null,
                                                List<int> cachedNewLines = null)
        {
            var lines = new List<iLineMatch>();
            var result = Regex.Matches(text, expression, RegexOptions.IgnoreCase);

            if (result.Count == 0)
                return lines;

            var newLines = cachedNewLines == null ? NewLines(text) : cachedNewLines;
            // Ensure we are the only ones using the NewLines Match - Thread Saftey - Sara
            // I was getting an index out of bounds on newLines which may have been caused by another thread accessing the object
            // instead of trying to track this down, I simple placed a lock here for now - Sara

            lock (newLines)
            {
                #region Single Line
                if (newLines.Count == 0)
                {
                    foreach (Match match1 in result)
                    {
                        lines.Add(new iLineMatch
                        {
                            iLine = GetLine(text, match1),
                            Line = text,
                            Value = result[0].Groups.Count == 2 ? result[0].Groups[1].ToString() : "",
                            model = model
                        });
                    }
                    return lines;
                }
                #endregion

                #region Multi-Line
                var matchIndex = 0;
                var iLine = 0;
                var match = result[matchIndex];
                var i = 0;
                foreach (var index in newLines)
                {
                    if (index > match.Index)
                    {
                        var startIndex = i == 0 ? 0 : newLines[i - 1] + 1;
                        lines.Add(new iLineMatch
                        {
                            iLine = iLine,
                            Line = text.Substring(startIndex, newLines[i] - startIndex - 2),
                            Value = match.Groups.Count == 2 ? match.Groups[1].ToString() : "",
                            model = model
                        });
                        matchIndex++;
                        if (matchIndex == result.Count)
                            break;
                        match = result[matchIndex];
                    }
                    iLine++;
                    i++;
                }
                return lines;
                #endregion
            }
        }
    }
}
