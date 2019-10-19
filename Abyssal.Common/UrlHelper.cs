namespace Abyssal.Common
{
    /// <summary>
    ///     Utility methods that work with Universal Resource Locators (URLs).
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        ///     Creates a Markdown URL, with specified text leading to a URL link.
        /// </summary>
        /// <param name="content">The text to display.</param>
        /// <param name="url">The target URL of the link.</param>
        /// <returns>A formatted Markdown URL.</returns>
        public static string CreateMarkdownUrl(string content, string url)
        {
            return $"[{content}]({url})";
        }
    }
}
