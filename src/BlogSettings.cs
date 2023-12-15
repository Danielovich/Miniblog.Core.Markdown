namespace Miniblog.Core
{
    public class BlogSettings
    {
        public int CommentsCloseAfterDays { get; set; } = 10;

        public string MarkdownUrl { get; set; } = default!;

        public bool DisplayComments
        {
            get
            {
                // if we are serving content by markdown we are by design not able to support comments 
                if (!string.IsNullOrWhiteSpace(this.MarkdownUrl))
                {
                    return false;
                }

                return true;
            }
        }

        public PostListView ListView { get; set; } = PostListView.TitlesAndExcerpts;

        public string Owner { get; set; } = "The Owner";

        public int PostsPerPage { get; set; } = 4;
    }
}
