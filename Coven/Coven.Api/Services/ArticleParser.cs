using System;
using System.Text.RegularExpressions;

namespace Coven.Api.Services
{
    public class ArticleParser
    {
        public static string RemoveBBCode(string input)
        {
            // Check if input is null
            if (input == null)
            {
                return null;
            }

            // Patterns for different BBCode elements and other patterns
            string[] patterns = {
                @"\[/?b\]",                // Matches [b] and [/b]
                @"\[/?i\]",                // Matches [i] and [/i]
                @"\[/?u\]",                // Matches [u] and [/u]
                @"\[/?quote.*?\]",         // Matches [quote] and [/quote]
                @"\[/?size.*?\]",          // Matches [size] and [/size]
                @"\[url:?.*?\].*?\[/url\]",// Matches [url] and [/url] including the content in between
                @"\[img:.*?\]",            // Matches [img] and [/img] including attributes
                @"\[/?col.*?\]",           // Matches [col] and [/col]
                @"\[/?row.*?\]",           // Matches [row] and [/row]
                @"\[/?h[1-6]\]",           // Matches [h1] to [h6]
                @"\[/?ul\]",               // Matches [ul] and [/ul]
                @"\[/?li\]",               // Matches [li] and [/li]
                @"\[small\]",              // Matches [small]
                @"\[/small\]",             // Matches [/small]
                @"\r\n|\n|\r",             // Matches carriage return and new line
                @"\[img:.*?\|size\|\d+\]", // Matches [img] tags with size attribute
            };

            string result = input;

            // Apply all patterns
            foreach (string pattern in patterns)
            {
                result = Regex.Replace(result, pattern, " ");
            }

            // Replace multiple spaces with a single space and trim the result
            result = Regex.Replace(result, @"\s+", " ").Trim();

            return result;
        }

        public static string GetArticleTypeFromUrl(string url)
        {
            // Parse the URL and get the segments
            Uri uri = new Uri(url);
            string path = uri.AbsolutePath;

            // Get the last segment of the path
            string lastSegment = path.Split('/').LastOrDefault();

            if (string.IsNullOrEmpty(lastSegment))
            {
                return null; // or throw an exception, depending on how you want to handle this case
            }

            // The last segment might contain a hyphen to separate words, get the last word
            string lastWord = lastSegment.Split('-').LastOrDefault();

            // If the last word is only numbers, or only has one character, return null
            if (string.IsNullOrEmpty(lastWord) || lastWord.All(char.IsDigit) || lastWord.Length == 1)
            {
                return "article";
            }

            return lastWord;
        }


    }
}

