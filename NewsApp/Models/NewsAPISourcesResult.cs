using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    internal class NewsAPISourcesResult
    {
        public string Status { get; set; }
        public List<Source> Sources { get; set; }
    }
}
