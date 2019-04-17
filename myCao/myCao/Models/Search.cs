using System;
using System.Collections.Generic;
using System.Text;

namespace myCao.Models
{
    public class Search
    {
        public string TextSearch { get; set; } = null;
        public Subject Subject1 { get; set; } = null;
        public Subject Subject2 { get; set; } = null;
        public Subject Subject3 { get; set; } = null;
        public int MinPoints { get; set; } = 0;
        public int MaxPoints { get; set; } = 0;
    }
}
