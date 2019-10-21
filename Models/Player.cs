using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DraftKings.Models
{
    public class Player
    {
        public int pid { get; set; }
        public int pcode { get; set; }
        public int tsid { get; set; }
        public string fn { get; set; }
        public string ln { get; set; }
        public string fnu { get; set; }
        public string lnu { get; set; }
        public string pn { get; set; }
        public int tid { get; set; }
        public int htid { get; set; }
        public int atid { get; set; }
        public string htabbr { get; set; }
        public string atabbr { get; set; }
        public int posid { get; set; }
        public int? slo { get; set; }
        public bool IsDisabledFromDrafting { get; set; }
        public IEnumerable<string> ExceptionalMessages { get; set; }
        public int s { get; set; }
        public string ppg { get; set; }
        public int or { get; set; }
        public bool swp { get; set; }
        public int pp { get; set; }
        public string i { get; set; }
        public int news { get; set; }
        public decimal? ep { get; set; }


    }
}
