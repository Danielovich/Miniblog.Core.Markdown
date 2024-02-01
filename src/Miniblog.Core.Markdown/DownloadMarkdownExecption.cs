namespace Miniblog.Core.Markdown;
public class DownloadMarkdownExecption : Exception
{
    public DownloadMarkdownExecption(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
