using M.Web.Crawler;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestCrawler
{
    /// <summary>
    /// Test method of the TopK words class.
    /// </summary>
    public class TopKWordsTests
    {
        private ITopKWords topKWords;

        public TopKWordsTests()
        {
            topKWords = new TopKWords();
        }
        
        /// <summary>
        /// If the words is empty, return should be null.
        /// </summary>
        [Fact]
        public void GetTopKWords_NullCheck_Test()
        {
            string[] words = new string[] { };

            Assert.Null(topKWords.GetTopKWords(words));
        }

        /// <summary>
        /// If the words has value, return the correct one.
        /// </summary>
        [Fact]
        public void GetTopKWords_WithValue_Test()
        {
            string[] words = new string[] 
            {"M", "M", "M", "Good", "Good", "Company", "haha"};

            List<KeyValuePair<string, int>> expected = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("M", 3),
                new KeyValuePair<string, int>("Good", 2),
                new KeyValuePair<string, int>("Company", 1),
                new KeyValuePair<string, int>("haha", 1)
            };

            var actual = new List<KeyValuePair<string, int>>(topKWords.GetTopKWords(words));

            Assert.True(expected.SequenceEqual(actual));
        }

        /// <summary>
        /// If the words has value with topK, return the correct Top K.
        /// </summary>
        [Fact]
        public void GetTopKWords_WithValue_Top2_Test()
        {
            string[] words = new string[]
            {"M", "M", "M", "Good", "Good", "Company", "haha"};

            List<KeyValuePair<string, int>> expected = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("M", 3),
                new KeyValuePair<string, int>("Good", 2)
            };

            int topK = 2;

            var actual = new List<KeyValuePair<string, int>>(topKWords.GetTopKWords(words, topK));

            Assert.True(expected.SequenceEqual(actual));
        }

        /// <summary>
        /// If the words has value with topK, return the correct Top K.
        /// </summary>
        [Fact]
        public void GetTopKWords_WithValue_ExcludedWord_Test()
        {
            string[] words = new string[]
            {"M", "M", "M", "Good", "Good", "Company", "haha"};

            List<KeyValuePair<string, int>> expected = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("M", 3),
                new KeyValuePair<string, int>("Good", 2),
                new KeyValuePair<string, int>("Company", 1)
            };

            int topK = 3;
            var excludedWord = new List<string>()
            {
                "haha"
            };
            
            var actual = new List<KeyValuePair<string, int>>(topKWords.GetTopKWords(words, topK, excludedWord));

            Assert.True(expected.SequenceEqual(actual));
        }

        /// <summary>
        /// If the words has value with topK, return the correct Top K.
        /// </summary>
        [Fact]
        public void GetTopKWords_WithValue_ExcludedWords_Test()
        {
            string[] words = new string[]
            {"M", "M", "M", "Good", "Good", "Company", "haha"};

            List<KeyValuePair<string, int>> expected = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("M", 3),
                new KeyValuePair<string, int>("Good", 2)
            };

            int topK = 3;
            var excludedWord = new List<string>()
            {
                "Company", "haha"
            };

            var actual = new List<KeyValuePair<string, int>>(topKWords.GetTopKWords(words, topK, excludedWord));

            Assert.True(expected.SequenceEqual(actual));
        }

    }
}
