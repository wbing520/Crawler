using System;
using System.Collections.Generic;
using System.Text;

namespace M.Web.Crawler
{
    /// <summary>
    /// Interface to get the top k words 
    /// </summary>
    public interface ITopKWords
    {
        /// <summary>
        /// Get the top K words
        /// </summary>
        /// <param name="words">The string array includes all words</param>
        /// <param name="topK">The top k value, default as 10</param>
        /// <param name="excludedWord">The excluded word list, default as null</param>
        /// <returns>Return the dictionary of word as key and the # of occurance as value</returns>
        IList<KeyValuePair<string, int>> GetTopKWords(string[] words, int topK = 10, List<string> excludedWord = null);

    }
}
