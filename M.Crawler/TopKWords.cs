using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using M.Web.Crawler;

namespace M.Web.Crawler
{
    public class TopKWords : ITopKWords
    {
        //Time complexity: O(nlogn), since it uses the orderby, all others would combine to O(nlogn)
        //Space compexity: O(n), since it creates a dictionary to take n elements. 
        public IList<KeyValuePair<string, int>> GetTopKWords(string[] words, int topK = 10, List<string> excludedWord = null)
        {
            if (words == null || words.Length <= 0) return null;

            try
            {
                //Declare a case-insensitive dictionary
                Dictionary<string, int>
                    wordDic = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

                //Save the word into the dictionary as key and the currence as value; 
                for (int i = 0; i < words.Length; i++)
                {
                    if (wordDic.ContainsKey(words[i].ToLower()))
                    {
                        wordDic[words[i].ToLower()]++;
                    }
                    else
                    {
                        wordDic.Add(words[i], 1);
                    }
                }

                var res = new List<KeyValuePair<string, int>>();
                if (excludedWord == null || excludedWord.Count <= 0)
                {
                    res = wordDic.OrderByDescending(w => w.Value)  //Descending order by # of Ocurrence
                        .ThenBy(w => w.Key)                            //then by the order of character                       
                        .Take(topK)                                    //Take the top k word
                        .ToList();                                     // output as a list
                }
                else
                {
                    res = wordDic.OrderByDescending(w => w.Value)  //Descending order by # of Ocurrence
                        .ThenBy(w => w.Key)                            //then by the order of character                                        
                        .Where(w => !excludedWord.Contains(
                            w.Key,StringComparer.OrdinalIgnoreCase))   //excluded the pair with the excludedwords
                        .Take(topK)                                    //Take the top k word
                        .ToList();                                     // output as a list
                }

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
