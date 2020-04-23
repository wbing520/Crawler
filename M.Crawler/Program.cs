using System;
using System.Net;
using System.Xml;
using System.IO;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;
using Crawler.Web.M.Model;
using Crawler.Web.M;
using System.Runtime.CompilerServices;
using M.Web.Crawler;

namespace M.Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("/****************************************************************/");
            Console.WriteLine("/****CreatedBy:Weibing Zheng; CreatedOn:04/02/2020***************/");
            Console.WriteLine("/****************************************************************/");

            try
            {
                Console.WriteLine("/****************************************************************/");
                Console.WriteLine("/***********Web Crawler & Get topK word**************************/");
                Console.WriteLine("/****************************************************************/");
                //run once, quit if entered 'q'
                char q = 'a';

                //start a watch
                var watch = Stopwatch.StartNew();

                //run the web crawler to get the word array
                string[] words = WebCrawlerClient();

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("The time to get the words array by web crawler: {0}", elapsedMs);
                Console.WriteLine("/****************************************************************/");

                //user configuration of topK and excluded words.
                while (q != 'q')
                {
                    //User configuration
                    UserParameters paras = GetUserConfiguration();

                    watch = Stopwatch.StartNew();
                    ////Get the top K words
                    ITopKWords topKWords = new TopKWords();
                    var sortedWords = topKWords.GetTopKWords(words, paras.TopK, paras.ExcludedWords);
                    watch.Stop();
                    var elapsedMs2 = watch.ElapsedMilliseconds;
                    Console.WriteLine("/****************************************************************/");
                    Console.WriteLine("The time to get the top K with excluded words (O(nlogn)): {0}", elapsedMs2);
                    Console.WriteLine("/****************************************************************/");

                    FormatOutput(sortedWords);

                    Console.WriteLine();
                    Console.WriteLine("/****************************************************************/");
                    //try again or quit
                    Console.WriteLine("Enter 'q' to quit, others to try again.");
                    var input = Console.ReadKey();
                    q = input.KeyChar;
                    Console.WriteLine("/****************************************************************/");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        #region private methods for Program
        /// <summary>
        /// The private method to use the web crawler client
        /// </summary>
        /// <returns>Return the word array</returns>
        private static string[] WebCrawlerClient()
        {
            var baseUrl = $"https://en.wikipedia.org/wiki/Microsoft";
            IWebCrawler webCrawler = new WebCrawler(baseUrl);
            var nodevalue = "//div[@class='mw-parser-output']";
            var subnodevalue = "h2";
            char demiliter = ' ';
            // Get the required html nodes.
            var htmlNodes = webCrawler.GetSelectedNodesFromURL(nodevalue, subnodevalue);
            //get the words from the html nodes
            string wordsStr = webCrawler.GetWordsStringFromHtmlNodes(htmlNodes, demiliter);
            //Removed the special characters.
            wordsStr = Regex.Replace(wordsStr, @"[^a-zA-Z0-9_']+", " ", RegexOptions.Compiled);
            //Get the words array, split by ' '
            string[] words = webCrawler.GetWordArrayFromString(wordsStr, demiliter);

            return words;
        }

        /// <summary>
        /// Get the user configured topK and exclued words
        /// </summary>
        /// <returns>The configured User Parameter(k value and excluded words)</returns>
        private static UserParameters GetUserConfiguration()
        {
            Console.WriteLine();
            Console.WriteLine("/****************************************************************/");
            Console.WriteLine("/************User configuration of topK and excluded words.******/");
            //assign the default value
            UserParameters paras = new UserParameters()
            {
                TopK = 10,
                ExcludedWords = null
            };

            //Get the top k value
            Console.WriteLine("Please enter the integer value of top K word (default would be 10): (Enter to next)");
            var topK = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(topK))
            {
                if (int.TryParse(topK, out int result))
                {
                    paras.TopK = result;
                }
                else
                {
                    Console.WriteLine("Please enter the valid value. (default to 10)");
                }
            }

            //Get the ExcludedWords list
            Console.WriteLine("Pleae enter the words you want to exclude from the count, (default to null)");
            Console.WriteLine(" If you want more than one word, please use comma(,) to separate each word (Enter to go next)");
            var ewords = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ewords))
            {
                paras.ExcludedWords = ewords.Split(",").Select(w => w.Trim()).ToList();
            }
            else
            {
                Console.WriteLine("Default to null");
            }

            return paras;
        }

        /// <summary>
        /// Output the formated and sorted words list
        /// </summary>
        /// <param name="sortedWords"> the sorted words list</param>
        private static void FormatOutput(IList<KeyValuePair<string, int>> sortedWords)
        {
            //format the output
            string format = "{0, 10}|{1, 10}";
            Console.WriteLine(format, " ", "#of Occurences");
            foreach (var r in sortedWords)
            {
                Console.WriteLine(format, r.Key, r.Value);
            }
        }

        #endregion
    }
}
