using Crawler.Web.M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace M.Web.CrawlerTests
{
    public class WebCrawlerTests
    {
        private IWebCrawler webCrawler;

        public WebCrawlerTests()
        {

            var baseUrl = $"https://en.wikipedia.org/wiki/Microsoft";
            webCrawler = new WebCrawler(baseUrl);
        }


        //null check test
        [Fact]
        public void GetWordArrayFromString_NullCheck_Test()
        {
            string s = string.Empty;
            Assert.Empty(webCrawler.GetWordArrayFromString(s, ' '));
        }

        [Fact]
        public void GetWordArrayFromString_Test()
        {
            string s = "The, M, word";
            char delimiter = ',';
            string [] expected = { "The", "M", "word" };
            string[] acutal = webCrawler.GetWordArrayFromString(s, delimiter);

            Assert.True(Enumerable.SequenceEqual(expected, acutal));
        }
    }
}
