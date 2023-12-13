namespace Miniblog.Core.Services
{
    using System;

    public static class MarkdownPropertyExtensions
    {
        public static DateTime ParseToDate( this MarkdownProperty markdownProperty)
        {
            if(DateTime.TryParse(markdownProperty.Value, out var date))
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
