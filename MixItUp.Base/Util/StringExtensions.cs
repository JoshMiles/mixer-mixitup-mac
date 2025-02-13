﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MixItUp.Base.Util
{
    public static class StringExtensions
    {
        private const char Comma = ',';
        private const char Decimal = '.';

        public static string ToFilePathString(this string source)
        {
            string directory = null;
            string filename = source;

            int lastSlash = source.LastIndexOf('\\');
            if (lastSlash > -1 && lastSlash < source.Length)
            {
                directory = source.Substring(0, lastSlash);
                filename = source.Substring(lastSlash + 1);
            }

            if (filename != null)
            {
                char[] invalidChars = Path.GetInvalidFileNameChars();
                filename = new string(filename.Select(c => invalidChars.Contains(c) ? '_' : c).ToArray());
            }

            if (directory != null)
            {
                char[] invalidChars = Path.GetInvalidPathChars();
                directory = new string(directory.Select(c => invalidChars.Contains(c) ? '_' : c).ToArray());
                return Path.Combine(directory, filename);
            }

            return filename;
        }

        public static string Shuffle(this string str)
        {
            return new string(str.ToCharArray().Shuffle().ToArray());
        }

        public static string AddNewLineEveryXCharacters(this string str, int lineLength)
        {
            List<string> newStringParts = new List<string>();
            foreach (string split in str.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                string newString = string.Empty;
                int x = 0;
                for (int i = 0; i < split.Length; i++)
                {
                    x++;
                    if (x >= lineLength && split[i] == ' ')
                    {
                        newString += Environment.NewLine;
                        x = 0;
                    }
                    else
                    {
                        newString += split[i];
                    }
                }
                newStringParts.Add(newString);
            }
            return string.Join(Environment.NewLine, newStringParts);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }

        public static bool ParseCurrency(this string str, out double result)
        {
            // First try the current culture and then the invariant culture if that fails.
            if (!double.TryParse(str, NumberStyles.Currency, NumberFormatInfo.CurrentInfo, out result))
            {
                return double.TryParse(str, NumberStyles.Currency, NumberFormatInfo.InvariantInfo, out result);
            }
            return true;
        }

        public static string ToCurrencyString(this int number) { return ((double)number).ToCurrencyString(); }

        public static string ToCurrencyString(this double number) { return string.Format("{0:C}", Math.Round(number, 2)); }

        public static string ToNumberDisplayString(this int number) { return number.ToString("N0"); }
    }
}
