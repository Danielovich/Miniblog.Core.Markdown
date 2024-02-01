namespace Miniblog.Core.Tests
{
    using System;

    using Miniblog.Core.Markdown;

    public class UriExtensionsTests
    {
        [Fact]
        public void When_Uris_Shoud_Be_Markdown_Return_Only_Markdown_Uris() {
            var fixture = new Fixture();

            var uris = new List<Uri>
            {
                new Uri("https://some.dk/domain/file.md"),
                new Uri("https://some.dk/domain/file.markdown"),
                new Uri("https://some.dk/domain/file1.markdown"),
                new Uri("https://some.dk/domain/file"),
                new Uri("https://some.dk/domain/file.jpg"),            };

            var validUris = uris.ByValidFileExtensions(MarkdownFileProperties.ValidFileExtensions);

            Assert.True(validUris.Count() == 3);
        }
    }
}
