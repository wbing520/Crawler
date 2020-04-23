using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Crawler.Web.M
{
    public class WebCrawler : IWebCrawler
    {
        private readonly HtmlDocument htmlDoc;

        public WebCrawler(string url)
        {
            HtmlWeb web = new HtmlWeb();
            htmlDoc = web.Load(url);
        }

        public IList<HtmlNode> GetSelectedNodesFromURL(string nodevalue, string subnodevalue)
        {
            if (string.IsNullOrEmpty(nodevalue)) return null;

            try
            {
                var classNodes = htmlDoc.DocumentNode.SelectNodes(nodevalue);
                List<HtmlNode> selectedNodes = new List<HtmlNode>();

                int i = 0, j = 0;
                foreach (HtmlNode node in classNodes)
                {
                    HtmlNodeCollection childNodes = node.ChildNodes;

                    //Get the first subnodevalue position
                    for (i = 0; i < childNodes.Count; i++)
                    {
                        if (childNodes[i].Name == subnodevalue)
                        {
                            break;
                        }
                    }

                    //From the first subnodevalue, add the following siblings node onto selected node.
                    //until the next subnodevalue
                    for (j = i + 1; j < childNodes.Count; j++)
                    {
                        if (childNodes[j].Name != subnodevalue)
                        {
                            foreach (var cn in childNodes[j].ChildNodes)
                            {
                                if (cn.ChildNodes.Count <= 1)
                                {
                                    selectedNodes.Add(cn);
                                }
                                else
                                {
                                    //consider one more deeper layer for words under the pictures
                                    foreach (var ccn in cn.ChildNodes)
                                    {
                                        selectedNodes.Add(ccn);
                                    }
                                }
                            }
                        }
                        else if (childNodes[j].Name == subnodevalue)
                        {
                            break;
                        }
                    }
                }

                return selectedNodes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        
        public string GetWordsStringFromHtmlNodes(IList<HtmlNode> htmlNodes, char delimiter)
        {
            try
            {
                if (htmlNodes.Count <= 0) return string.Empty;

                //Get the inner text of the selected nodes
                StringBuilder sb = new StringBuilder();
                foreach (var node in htmlNodes)
                {
                    sb.Append(node.GetDirectInnerText()).Append(delimiter);
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        
        public string[] GetWordArrayFromString(string s, char delimiter)
        {
            try
            {
                //null or empty or whitespace check
                if (string.IsNullOrWhiteSpace(s)) return new string[] { };

                // return the string array split by the delimiter
                return s.Split(delimiter).Select(w => w.Trim()).ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
