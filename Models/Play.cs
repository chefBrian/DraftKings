using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DraftKings.Models
{
    public class Play
    {
        public IEnumerable<Player> playerList { get; set; }
    }
}
