namespace Miniblog.Core.Services
{
    using Microsoft.AspNetCore.Http;

    using System;
    using System.Globalization;

    public static class MarkdownPropertyExtensions
    {
        public static DateTime ParseToDate( this MarkdownProperty markdownProperty)
        {
            if (DateTime.TryParseExact(
                markdownProperty.Value,
                Constants.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
            {
                return date;
            }

            return DateTime.MinValue;
        }

        public static bool ParseToBool(this MarkdownProperty markdownProperty)
        {
            return bool.TryParse(markdownProperty.Value, out bool result) ? result : false;
        }
    }
}
