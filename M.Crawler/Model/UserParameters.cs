using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Web.M.Model
{
    /// <summary>
    /// class model for the user entered parameters.
    /// </summary>
    public class UserParameters
    {
        // Top K value
        public int TopK { get; set; }

        // Excluded words
        public List <string> ExcludedWords { get; set; }
    }
}
