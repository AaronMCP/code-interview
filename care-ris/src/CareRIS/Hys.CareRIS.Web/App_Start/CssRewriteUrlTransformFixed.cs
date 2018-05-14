using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;

namespace Hys.CareRIS.Web
{
    /// <summary>
    /// Fixes for the standard System.Web.Optimization.CssRewriteUrlTransform. 
    /// Now plays nice with:
    ///  * Data URIs, including svgs (https://aspnetoptimization.codeplex.com/workitem/88)
    ///  * URLs to other resources that are already absolute 
    ///  * Virtual directories (http://aspnetoptimization.codeplex.com/workitem/83)
    /// </summary>
    public class CssRewriteUrlTransformFixed : IItemTransform
    {
        private static string RebaseUrlToAbsolute(string baseUrl, string url)
        {
            if (url.StartsWith("data:") || string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(baseUrl) || url.StartsWith("/", StringComparison.OrdinalIgnoreCase)
                 || url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return url;
            }

            if (!baseUrl.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                baseUrl += "/";
            }

            return VirtualPathUtility.ToAbsolute(baseUrl + url);
        }
        private static string ConvertUrlsToAbsolute(string baseUrl, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return content;
            }

            var regex = new Regex("url\\s*\\(\\s*(['\"]?)(?<url>[^)]+?)\\1\\s*\\)");

            return regex.Replace(content, match => "url(\"" + RebaseUrlToAbsolute(baseUrl, match.Groups["url"].Value) + "\")");
        }
        public string Process(string includedVirtualPath, string input)
        {
            if (includedVirtualPath == null)
            {
                throw new ArgumentNullException("includedVirtualPath");
            }
            if (includedVirtualPath.Length < 1 || includedVirtualPath[0] != '~')
            {
                throw new ArgumentException("includedVirtualPath must be valid ( i.e. have a length and start with ~ )");
            }

            var directory = VirtualPathUtility.GetDirectory(includedVirtualPath);

            return ConvertUrlsToAbsolute(directory, input);
        }
    }
}