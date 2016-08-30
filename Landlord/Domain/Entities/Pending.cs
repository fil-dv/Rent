using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pending
    {
        public int PendingID { get; set; }
        public int AreaID { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
    }
}
