namespace tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
   

    public class MarkdownBlogpostParserTestFixture
    {
        public string MarkdownContent { get; set; } = default!;

        public MarkdownBlogpostParserTestFixture()
        {
            var markdownFile = Path.Combine(Environment.CurrentDirectory, "assets", "post.md");

            MarkdownContent = File.ReadAllText(markdownFile);
        }
    }

    public class MarkdownBlogpostParserTests : IClassFixture<MarkdownBlogpostParserTestFixture>
    {
        private readonly MarkdownBlogpostParserTestFixture markdownBlogpostParserTestFixture;

        public MarkdownBlogpostParserTests(MarkdownBlogpostParserTestFixture markdownBlogpostParserTestFixture)
        {
            this.markdownBlogpostParserTestFixture = markdownBlogpostParserTestFixture;
        }

        [Fact]
        public async Task Parse_Comments_As_Blog_Properties()
        {
            string comments =
                  "[//]: # \"title: hugga bugga ulla johnson\"\n"+
                  "[//]: # \"johnny: hugga bugga ulla johnson\"";

            var markDownBlogpostParser = new MarkdownBlogpostParser(comments);
            await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

            Assert.Equal("hugga bugga ulla johnson", markDownBlogpostParser.Post.Title);
        }

        [Fact]
        public async Task Parse_All_Comments_To_Post_Properties()
        {
            string comments =
                  "[//]: # \"title: hugga bugga ulla johnson\" \n" +
                  "[//]: # \"slug: hulla bulla\" \n" +
                  "[//]: # \"pubDate: 2017-10-13 18:59:01\"\n" +
                  "[//]: # \"lastModified: 2017-10-13 23:59:01\"\n" +
                  "[//]: # \"excerpt: an excerpt you would never imagine \"\n" +
                  "[//]: # \"categories: cars, coding, personal, recipes \"\n" +
                  "[//]: # \"isPublished: true \"";

            var markDownBlogpostParser = new MarkdownBlogpostParser(comments);
            await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

            Assert.True(markDownBlogpostParser.Post.Title != string.Empty);
            Assert.True(markDownBlogpostParser.Post.Slug != string.Empty);
            Assert.True(markDownBlogpostParser.Post.PubDate > DateTime.MinValue);
            Assert.True(markDownBlogpostParser.Post.LastModified > DateTime.MinValue);
            Assert.True(markDownBlogpostParser.Post.Excerpt != string.Empty);
            Assert.True(markDownBlogpostParser.Post.Categories.Count() == 4);
            Assert.True(markDownBlogpostParser.Post.IsPublished);
        }

        [Fact]
        public async Task Parse_Different_Comment_Format_As_Blog_Properties()
        {
            string comments =
                  "[//]: #   \"title: hugga bugga ulla johnson\" \n" +
                  "[//]:# \"slug: hulla bulla\"";

            var markDownBlogpostParser = new MarkdownBlogpostParser(comments);
            await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

            Assert.True(markDownBlogpostParser.Post.Title != string.Empty);
            Assert.True(markDownBlogpostParser.Post.Slug != string.Empty);
        }

        [Fact]
        public async Task Parse_Comments_Fails()
        {
            string comments =
                  "[//]:   #\"title: hugga bugga ulla johnson\" \n" +
                  "[//]:  # \"slug: hulla bulla\" \n";

            var markDownBlogpostParser = new MarkdownBlogpostParser(comments);
            await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

            Assert.True(markDownBlogpostParser.Post.Title == string.Empty);
            Assert.True(markDownBlogpostParser.Post.Slug == string.Empty);
        }

        [Fact]
        public async Task Parse_Comments_As_Dates_Fails()
        {
            string comments =
                  "[//]: # \"pubDate: 20171-10-13 18:59:01\"\n" +
                  "[//]: # \"lastModified: 20217-10-13 23:59:01\"";

            var markDownBlogpostParser = new MarkdownBlogpostParser(comments);
            await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

            Assert.True(markDownBlogpostParser.Post.PubDate == DateTime.MinValue);
            Assert.True(markDownBlogpostParser.Post.LastModified == DateTime.MinValue);
        }

        [Fact]
        public async Task Parse_Comments_To_A_Date()
        {
            var date = "2017-10-13 18:59:01";
            string comments = $"[//]: # \"pubDate: {date}\"\n";
            var markDownBlogpostParser = new MarkdownBlogpostParser(comments);
            await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

            Assert.Equal(Convert.ToDateTime(date), markDownBlogpostParser.Post.PubDate);
        }

        [Fact]
        public async Task Parse_Comments_To_Categories()
        {
            string comments = $"[//]: # \"categories: web,code,johnny,cars\"\n";
            var markDownBlogpostParser = new MarkdownBlogpostParser(comments);
            await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

            Assert.True(markDownBlogpostParser.Post.Categories.Count() == 4);
        }

        [Fact]
        public async Task Parse_Comments_To_Categories_Is_Empty()
        {
            string comments = $"[//]: # \"categories\"";
            var markDownBlogpostParser = new MarkdownBlogpostParser(comments);
            await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

            Assert.True(markDownBlogpostParser.Post.Categories.Count() == 0);
        }

        [Fact]
        public async Task Parse_As_Content()
        {
            var markDownBlogpostParser = new MarkdownBlogpostParser(this.markdownBlogpostParserTestFixture.MarkdownContent);
            await markDownBlogpostParser.ParseContent();

            Assert.True(markDownBlogpostParser.Post.Content != string.Empty);

        }

    }
}
