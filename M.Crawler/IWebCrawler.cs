using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Web.M
{
    /// <summary>
    /// Web crawler class to get the words
    /// </summary>
    public interface IWebCrawler
    {
        /// <summary>
        /// Get the selected nodes from the given URL
        /// </summary>
        /// <param name="nodevalue">The given node value to get selected</param>
        /// <param name="subnodevalue">The sub node value</param>
        /// <returns>Selected html nodes</returns>
        IList<HtmlNode> GetSelectedNodesFromURL(string nodevalue, string subnodevalue);

        /// <summary>
        /// Return the inner text string
        /// </summary>
        /// <param name="htmlNodes">The selected htmlnodes</param>
        /// <param name="delimiter">Added between the node text</param>
        /// <returns></returns>
        string GetWordsStringFromHtmlNodes(IList<HtmlNode> htmlNodes, char delimiter);

        /// <summary>
        /// Get the word array from string
        /// </summary>
        /// <param name="s">input string</param>
        /// <param name="delimiter">delimiter to split the string</param>
        /// <returns>Word array</returns>
        string[] GetWordArrayFromString(string s, char delimiter);
    }
}
