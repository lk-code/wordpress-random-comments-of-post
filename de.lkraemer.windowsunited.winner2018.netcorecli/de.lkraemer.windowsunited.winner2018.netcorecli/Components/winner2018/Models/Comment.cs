using System;

namespace de.lkraemer.windowsunited.winner2018.netcorecli.Components.winner2018.Models
{
    public class Comment
    {
        public int author { get; set; }
        public string author_name { get; set; }
        public string author_url { get; set; }
        public DateTime date { get; set; }
    }
}