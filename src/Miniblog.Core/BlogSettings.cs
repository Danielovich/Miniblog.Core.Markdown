namespace Miniblog.Core
{
    public class BlogSettings
    {
        public int CommentsCloseAfterDays { get; set; } = 10;

        public string MarkdownUrl { get; set; } = default!;

        public bool MarkdownEnabled { get { return string.IsNullOrWhiteSpace(this.MarkdownUrl) ? false : true;  } }

        public bool DisplayComments { get; set; } = true;

        public PostListView ListView { get; set; } = PostListView.TitlesAndExcerpts;

        public string Owner { get; set; } = "The Owner";

        public int PostsPerPage { get; set; } = 4;
    }
}
