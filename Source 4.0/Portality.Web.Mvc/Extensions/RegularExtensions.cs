using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portality.Web.Mvc
{
    public static class RegularExtensions
    {
        /// <summary>
        /// Transforms a string into a url-compatible parameter. 
        /// For example: This is; my name! => This_is_my_name
        /// </summary>
        /// <param name="replaceChar">The character to replace elements with (by default: -).</param>
        /// <returns>Returns a string which is compatible as a url-parameter.</returns>
        public static string ToUrlParameter(this string value, string replaceChar = "-")
        {
            var returnValue = new StringBuilder(value);

            // Replace special characters
            var listOfReplaceables = new string[] {
                " ", // space
                ",", // comma
                ".", // dot
                ";", // semicolon                
            };

            // Excludable characters
            var listOfExcludables = new string[] {
                "~", // tilde
                "!", // exclamation
                "@", // at-sign
                "#", // hash
                "$", // dollar
                "%", // percentage 
                "^", // rooftop
                //"&", // and-sign
                "*", // asterisk
                "(", // open brace
                ")", // close brace
                "{", // open brace
                "}", // close brace
            };

            foreach (var rc in listOfReplaceables)
                returnValue.Replace(rc, replaceChar);

            foreach (var ex in listOfExcludables)
                returnValue.Replace(ex, string.Empty);

            return returnValue.ToString().Trim();
        }
    }
}
